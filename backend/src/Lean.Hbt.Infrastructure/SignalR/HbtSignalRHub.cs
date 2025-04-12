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
using System.Security.Claims;

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
                _logger.LogInformation("新连接建立 - 连接ID: {ConnectionId}", Context.ConnectionId);

                // 获取设备信息
                var deviceInfo = Context.GetHttpContext()?.Request.Query["deviceInfo"].ToString();
                if (string.IsNullOrEmpty(deviceInfo))
                {
                    _logger.LogWarning("未找到设备信息");
                    return;
                }

                // 获取访问令牌
                var accessToken = Context.GetHttpContext()?.Request.Query["access_token"].ToString();
                if (string.IsNullOrEmpty(accessToken))
                {
                    _logger.LogWarning("未找到访问令牌");
                    return;
                }

                // 从token中获取用户ID
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(accessToken);
                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("未找到用户ID");
                    return;
                }

                _logger.LogInformation("用户 {UserId} 正在连接，设备信息: {DeviceInfo}", userId, deviceInfo);

                // 使用与登录时相同的设备ID生成方法
                var (deviceId, _) = _deviceIdGenerator.GenerateIds(deviceInfo, userId);
                _logger.LogInformation("生成的设备ID: {DeviceId}", deviceId);

                // 检查用户是否已存在
                var existingUser = await _userService.GetOnlineUserByDeviceAsync(long.Parse(userId), deviceId);
                if (existingUser != null)
                {
                    _logger.LogInformation("找到现有用户记录，准备更新 - 用户ID: {UserId}, 设备ID: {DeviceId}", userId, deviceId);
                    // 更新现有用户
                    existingUser.ConnectionId = Context.ConnectionId;
                    existingUser.LastActivity = DateTime.Now;
                    existingUser.LastHeartbeat = DateTime.Now;
                    existingUser.OnlineStatus = 0;
                    await _userService.SaveOnlineUserAsync(existingUser);
                    _logger.LogInformation("用户记录更新成功 - 用户ID: {UserId}, 连接ID: {ConnectionId}", userId, Context.ConnectionId);
                }
                else
                {
                    _logger.LogInformation("未找到现有用户记录，准备创建新记录 - 用户ID: {UserId}, 设备ID: {DeviceId}", userId, deviceId);
                    // 创建新用户记录
                    var newUser = new HbtOnlineUser
                    {
                        UserId = long.Parse(userId),
                        DeviceId = deviceId,
                        ConnectionId = Context.ConnectionId,
                        LastActivity = DateTime.Now,
                        LastHeartbeat = DateTime.Now,
                        OnlineStatus = 0,
                        TenantId = 0,  // 添加租户ID
                        GroupId = 0,   // 添加组ID
                        ClientIp = Context.GetHttpContext()?.Connection?.RemoteIpAddress?.ToString(),
                        UserAgent = Context.GetHttpContext()?.Request.Headers["User-Agent"].ToString()
                    };
                    await _userService.SaveOnlineUserAsync(newUser);
                    _logger.LogInformation("新用户记录创建成功 - 用户ID: {UserId}, 连接ID: {ConnectionId}", userId, Context.ConnectionId);
                }

                // 添加设备信息
                var device = new HbtSignalRDevice
                {
                    UserId = long.Parse(userId),
                    DeviceId = deviceId,
                    ConnectionId = Context.ConnectionId,
                    LastActivity = DateTime.Now,
                    LastHeartbeat = DateTime.Now,
                    OnlineStatus = 0,
                    TenantId = 0,
                    GroupId = 0,
                    IpAddress = Context.GetHttpContext()?.Connection?.RemoteIpAddress?.ToString(),
                    UserAgent = Context.GetHttpContext()?.Request.Headers["User-Agent"].ToString()
                };
                _logger.LogInformation("准备添加设备信息 - 用户ID: {UserId}, 设备ID: {DeviceId}, 连接ID: {ConnectionId}", 
                    device.UserId, device.DeviceId, device.ConnectionId);
                await _userService.AddUserDevice(userId, deviceId, Context.ConnectionId);
                _logger.LogInformation("设备信息添加成功 - 用户ID: {UserId}, 设备ID: {DeviceId}", userId, deviceId);

                // 验证设备是否已添加
                var devices = _userService.GetUserDevices(userId);
                _logger.LogInformation("验证设备信息 - 用户ID: {UserId}, 设备数量: {DeviceCount}", userId, devices.Count);

                await Clients.Client(Context.ConnectionId).ReceiveMessage("连接成功");
                _logger.LogInformation("用户 {UserId} 连接成功，连接ID: {ConnectionId}", userId, Context.ConnectionId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "连接处理失败");
                throw;
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
            try
            {
                // 获取当前连接的用户信息
                var currentUser = await _userService.GetOnlineUserAsync(Context.ConnectionId);
                if (currentUser == null)
                {
                    _logger.LogWarning("发送者未找到: ConnectionId={ConnectionId}", Context.ConnectionId);
                    return;
                }

                _logger.LogInformation("开始发送消息 - 发送者: {SenderId}, 接收者: {UserId}, 内容: {Message}, 当前连接ID: {ConnectionId}", 
                    currentUser.UserId, userId, message, Context.ConnectionId);
                
                // 将字符串类型的userId转换为long
                if (!long.TryParse(userId, out long targetUserId))
                {
                    _logger.LogWarning("无效的用户ID格式: {UserId}", userId);
                    return;
                }
                
                var devices = _userService.GetUserDevices(targetUserId.ToString());
                _logger.LogInformation("找到接收者 {UserId} 的设备数量: {DeviceCount}", userId, devices.Count());
                
                if (!devices.Any())
                {
                    _logger.LogWarning("接收者 {UserId} 没有在线设备", userId);
                    return;
                }

                foreach (var device in devices)
                {
                    try 
                    {
                        _logger.LogInformation("正在向设备发送消息 - 设备ID: {DeviceId}, 连接ID: {ConnectionId}", 
                            device.DeviceId, device.ConnectionId);
                        
                        var client = Clients.Client(device.ConnectionId);
                        if (client == null)
                        {
                            _logger.LogWarning("无法获取客户端连接 - 设备ID: {DeviceId}, 连接ID: {ConnectionId}", 
                                device.DeviceId, device.ConnectionId);
                            continue;
                        }

                        await client.ReceiveMessage(JsonSerializer.Serialize(new {
                            SenderId = currentUser.UserId,
                            ReceiverId = userId,
                            Content = message,
                            Timestamp = DateTime.Now
                        }));
                        
                        _logger.LogInformation("消息发送成功 - 设备ID: {DeviceId}, 连接ID: {ConnectionId}", 
                            device.DeviceId, device.ConnectionId);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "向设备发送消息失败 - 设备ID: {DeviceId}, 连接ID: {ConnectionId}", 
                            device.DeviceId, device.ConnectionId);
                    }
                }
                
                _logger.LogInformation("消息发送处理完成");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "发送消息过程中发生错误");
                throw;
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