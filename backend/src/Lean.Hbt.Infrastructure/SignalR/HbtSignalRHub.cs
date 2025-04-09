//===================================================================
// 项目名 : Lean.Hbt.Infrastructure
// 文件名 : HbtSignalRHub.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : SignalR集线器
//===================================================================

using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.RealTime;
using Lean.Hbt.Domain.IServices.Security;
using Lean.Hbt.Domain.IServices.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Lean.Hbt.Infrastructure.SignalR
{
    /// <summary>
    /// SignalR集线器
    /// </summary>
    public class HbtSignalRHub : Hub<IHbtSignalRClient>, IHbtSignalRHub
    {
        private readonly ILogger<HbtSignalRHub> _logger;
        private readonly IHbtSignalRUserService _userService;
        private readonly IHbtSignalRCacheService _cacheService;
        private readonly IHbtSingleSignOnService _singleSignOnService;
        private readonly IHbtDeviceIdGenerator _deviceIdGenerator;
        private readonly IHbtRepository<HbtOnlineUser> _repository;
        private readonly IHubContext<HbtSignalRHub> _hubContext;
        private readonly IConfiguration _configuration;
        private static readonly SemaphoreSlim _connectionLock = new SemaphoreSlim(1, 1);

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSignalRHub(
            ILogger<HbtSignalRHub> logger,
            IHbtSignalRUserService userService,
            IHbtSignalRCacheService cacheService,
            IHbtSingleSignOnService singleSignOnService,
            IHbtDeviceIdGenerator deviceIdGenerator,
            IHbtRepository<HbtOnlineUser> repository,
            IHubContext<HbtSignalRHub> hubContext,
            IConfiguration configuration)
        {
            _logger = logger;
            _userService = userService;
            _cacheService = cacheService;
            _singleSignOnService = singleSignOnService;
            _deviceIdGenerator = deviceIdGenerator;
            _repository = repository;
            _hubContext = hubContext;
            _configuration = configuration;
        }

        /// <summary>
        /// 客户端连接
        /// </summary>
        public override async Task OnConnectedAsync()
        {
            try
            {
                await _connectionLock.WaitAsync();
                var httpContext = Context.GetHttpContext();
                if (httpContext == null)
                {
                    throw new HubException("无法获取HTTP上下文");
                }

                var deviceInfoJson = httpContext.Request.Query["deviceInfo"].ToString() ?? string.Empty;
                var accessToken = httpContext.Request.Query["access_token"].ToString() ?? string.Empty;

                if (string.IsNullOrEmpty(deviceInfoJson) || string.IsNullOrEmpty(accessToken))
                {
                    throw new HubException("设备信息或访问令牌不能为空");
                }

                // URL 解码设备信息
                deviceInfoJson = Uri.UnescapeDataString(deviceInfoJson);

                // 从 token 中获取用户信息
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(accessToken);
                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
                var tenantId = jwtToken.Claims.FirstOrDefault(c => c.Type == "tid")?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    throw new HubException("无效的用户ID");
                }

                // 解析前端传来的设备信息
                var deviceInfo = JsonSerializer.Deserialize<HbtSignalRDevice>(deviceInfoJson);

                if (deviceInfo == null)
                {
                    throw new HubException("无效的设备信息");
                }

                // 使用与登录时相同的设备ID生成方法
                var (deviceId, connectionId) = _deviceIdGenerator.GenerateIds(JsonSerializer.Serialize(deviceInfo), userId);

                // 检查用户是否已存在
                var existingUser = await _userService.GetOnlineUserByDeviceAsync(long.Parse(userId), deviceId);
                if (existingUser != null)
                {
                    // 更新现有用户
                    existingUser.ConnectionId = Context.ConnectionId;
                    existingUser.LastActivity = DateTime.Now;
                    existingUser.LastHeartbeat = DateTime.Now;
                    existingUser.OnlineStatus = 0;
                    await _userService.SaveOnlineUserAsync(existingUser);
                }
                else
                {
                    // 创建新用户记录
                    var onlineUser = new HbtOnlineUser
                    {
                        TenantId = long.Parse(tenantId ?? "0"),
                        UserId = long.Parse(userId),
                        GroupId = 0, // 默认组
                        ConnectionId = Context.ConnectionId,
                        DeviceId = deviceId,
                        ClientIp = httpContext.Connection.RemoteIpAddress?.ToString(),
                        UserAgent = httpContext.Request.Headers["User-Agent"].ToString(),
                        LastActivity = DateTime.Now,
                        LastHeartbeat = DateTime.Now,
                        OnlineStatus = 0
                    };
                    await _userService.SaveOnlineUserAsync(onlineUser);
                }

                // 通知客户端连接成功
                await Clients.Client(Context.ConnectionId).ReceiveMessage("连接成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SignalR连接失败: {ConnectionId}", Context.ConnectionId);
                throw;
            }
            finally
            {
                _connectionLock.Release();
            }
        }

        /// <summary>
        /// 客户端断开连接
        /// </summary>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                await _connectionLock.WaitAsync();
                var httpContext = Context.GetHttpContext();
                var deviceId = httpContext.Request.Query["deviceId"].ToString();
                var accessToken = httpContext.Request.Query["access_token"].ToString();

                if (string.IsNullOrEmpty(deviceId) || string.IsNullOrEmpty(accessToken))
                {
                    return;
                }

                // 验证用户
                var user = await _userService.GetOnlineUserAsync(Context.ConnectionId);
                if (user == null)
                {
                    return;
                }

                // 更新在线用户状态
                user.OnlineStatus = 1;
                user.LastActivity = DateTime.Now;
                await _userService.SaveOnlineUserAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SignalR断开连接失败: {ConnectionId}", Context.ConnectionId);
            }
            finally
            {
                _connectionLock.Release();
            }
        }

        /// <summary>
        /// 发送消息给指定用户
        /// </summary>
        public async Task SendMessageAsync(string userId, string message)
        {
            var devices = _userService.GetUserDevices(userId);
            foreach (var device in devices)
            {
                await Clients.Client(device.ConnectionId).ReceiveMessage(message);
            }
        }

        /// <summary>
        /// 发送邮件状态更新
        /// </summary>
        public async Task SendMailStatus(long mailId, string status, string message)
        {
            var notification = new HbtRealTimeNotification
            {
                Type = HbtMessageType.System,  // 使用System类型，因为这是系统状态更新
                Title = "邮件状态更新",
                Content = $"邮件 {mailId} {status}: {message}",
                Timestamp = DateTime.Now,
                Data = new { mailId, status, message }
            };

            // 获取需要接收此邮件状态的用户（包括收件人和抄送人）
            var targetUsers = await _userService.GetMailReceivers(mailId);
            foreach (var userId in targetUsers)
            {
                await Clients.User(userId.ToString()).ReceiveMailStatus(notification);
            }
        }

        /// <summary>
        /// 发送通知状态更新
        /// </summary>
        public async Task SendNoticeStatus(long noticeId, string status, string message)
        {
            var notification = new HbtRealTimeNotification
            {
                Type = HbtMessageType.Notification,
                Title = "通知状态更新",
                Content = $"通知 {noticeId} {status}: {message}",
                Timestamp = DateTime.Now,
                Data = new { noticeId, status }
            };

            // 获取需要接收此通知的用户
            var targetUsers = await _userService.GetNoticeReceivers(noticeId);
            foreach (var userId in targetUsers)
            {
                await Clients.User(userId.ToString()).ReceiveNoticeStatus(notification);
            }
        }

        /// <summary>
        /// 发送邮件已读状态更新
        /// </summary>
        public async Task SendMailReadStatus(long mailId, long userId, bool isRead)
        {
            var notification = new HbtRealTimeNotification
            {
                Type = isRead ? HbtMessageType.MailRead : HbtMessageType.MailUnread,
                Title = "邮件读取状态更新",
                Content = $"邮件 {mailId} 已{(isRead ? "读" : "未读")}",
                Timestamp = DateTime.Now,
                Data = new { mailId, userId, isRead }
            };

            // 发送给邮件相关的用户
            var targetUsers = await _userService.GetMailReceivers(mailId);
            foreach (var targetUserId in targetUsers)
            {
                await Clients.User(targetUserId.ToString()).ReceiveMailStatus(notification);
            }
        }

        /// <summary>
        /// 发送通知已读状态更新
        /// </summary>
        public async Task SendNoticeReadStatus(long noticeId, long userId, bool isRead)
        {
            var notification = new HbtRealTimeNotification
            {
                Type = HbtMessageType.Notification,
                Title = "通知读取状态更新",
                Content = $"通知 {noticeId} 已{(isRead ? "读" : "未读")}",
                Timestamp = DateTime.Now,
                Data = new { noticeId, userId, isRead }
            };

            // 发送给通知相关的用户
            var targetUsers = await _userService.GetNoticeReceivers(noticeId);
            foreach (var targetUserId in targetUsers)
            {
                await Clients.User(targetUserId.ToString()).ReceiveNoticeStatus(notification);
            }
        }

        /// <summary>
        /// 发送定时任务状态更新
        /// </summary>
        public async Task SendTaskStatus(long taskId, string status, string message)
        {
            var notification = new HbtRealTimeNotification
            {
                Type = HbtMessageType.System,
                Title = "定时任务状态更新",
                Content = $"任务 {taskId} {status}: {message}",
                Timestamp = DateTime.Now
            };
            await Clients.All.ReceiveTaskStatus(notification);
        }

        /// <summary>
        /// 发送个人通知
        /// </summary>
        public async Task SendPersonalNotice(string userId, string title, string content)
        {
            var notification = new HbtRealTimeNotification
            {
                Type = HbtMessageType.User,
                Title = title,
                Content = content,
                Timestamp = DateTime.Now
            };
            await Clients.User(userId).ReceivePersonalNotice(notification);
        }

        /// <summary>
        /// 发送系统广播
        /// </summary>
        public async Task SendBroadcast(string title, string content)
        {
            var notification = new HbtRealTimeNotification
            {
                Type = HbtMessageType.System,
                Title = title,
                Content = content,
                Timestamp = DateTime.Now
            };
            await Clients.All.ReceiveBroadcast(notification);
        }

        /// <summary>
        /// 发送心跳
        /// </summary>
        public async Task SendHeartbeat()
        {
            await Clients.Client(Context.ConnectionId).ReceiveHeartbeat(DateTime.Now);
        }

        /// <summary>
        /// 断开客户端连接
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        public async Task DisconnectClientAsync(string connectionId)
        {
            try
            {
                _logger.LogInformation("准备断开客户端连接: ConnectionId={ConnectionId}", connectionId);

                // 1. 发送强制下线消息
                try
                {
                    await Clients.Client(connectionId).ForceOffline("您的账号已在其他设备登录，如非本人操作，请及时修改密码！");
                    _logger.LogInformation("已发送强制下线消息: ConnectionId={ConnectionId}", connectionId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "发送强制下线消息失败: ConnectionId={ConnectionId}", connectionId);
                }

                // 2. 等待一段时间让客户端处理消息
                await Task.Delay(1000);

                // 3. 断开连接
                try
                {
                    if (Context.ConnectionId == connectionId)
                    {
                        Context.Abort();
                        _logger.LogInformation("已中止当前连接: ConnectionId={ConnectionId}", connectionId);
                    }
                    else
                    {
                        var hubContext = _hubContext.Clients.Client(connectionId);
                        if (hubContext != null)
                        {
                            await hubContext.SendAsync("ForceOffline", "您的账号已在其他设备登录，如非本人操作，请及时修改密码！");
                            _logger.LogInformation("已发送强制下线消息给其他连接: ConnectionId={ConnectionId}", connectionId);
                        }
                        else
                        {
                            _logger.LogWarning("未找到连接: ConnectionId={ConnectionId}", connectionId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "中止连接失败: ConnectionId={ConnectionId}", connectionId);
                }

                _logger.LogInformation("已断开客户端连接: ConnectionId={ConnectionId}", connectionId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "断开客户端连接时发生错误: ConnectionId={ConnectionId}", connectionId);
                // 不再抛出异常，避免死循环
                return;
            }
        }

        /// <summary>
        /// 获取 Hub 上下文
        /// </summary>
        private HubCallerContext? GetHubContext(string connectionId)
        {
            try
            {
                var connection = _hubContext.Clients.Client(connectionId) as IClientProxy;
                if (connection != null)
                {
                    var contextField = connection.GetType().GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    return contextField?.GetValue(connection) as HubCallerContext;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取 Hub 上下文失败: ConnectionId={ConnectionId}", connectionId);
            }
            return null;
        }
    }
}