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
using Lean.Hbt.Domain.Entities.SignalR;
using Lean.Hbt.Domain.IServices.Security;
using Lean.Hbt.Domain.IServices.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace Lean.Hbt.Infrastructure.SignalR
{
    /// <summary>
    /// SignalR集线器
    /// </summary>
    public class HbtSignalRHub : Hub<IHbtSignalRClient>, IHbtSignalRHub
    {
        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly IHbtLogger _logger;

        private readonly IHbtSignalRUserService _userService;
        private readonly IHbtSignalRCacheService _cacheService;
        private readonly IHbtSingleSignOnService _singleSignOnService;
        private readonly IHbtDeviceIdGenerator _deviceIdGenerator;
        private readonly IHbtRepository<HbtOnlineUser> _repository;
        private readonly IHubContext<HbtSignalRHub> _hubContext;
        private readonly IConfiguration _configuration;
        private readonly SignalRConfig _signalRConfig;
        private static readonly SemaphoreSlim _connectionLock = new SemaphoreSlim(1, 1);

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSignalRHub(
            IHbtLogger logger,

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
            _signalRConfig = _configuration.GetSection("SignalR").Get<SignalRConfig>();
        }

        /// <summary>
        /// 客户端连接
        /// </summary>
        public override async Task OnConnectedAsync()
        {
            try
            {
                _logger.Info("新连接建立 - 连接ID: {ConnectionId}", Context.ConnectionId);

                // 获取设备信息
                var httpContext = Context.GetHttpContext();
                if (httpContext == null)
                {
                    _logger.Warn("无法获取HTTP上下文");
                    return;
                }

                var deviceInfo = new HbtSignalRDevice
                {
                    DeviceId = httpContext.Request.Headers["X-Device-Id"].ToString(),
                    DeviceName = httpContext.Request.Headers["X-Device-Name"].ToString(),
                    DeviceType = Enum.TryParse<HbtDeviceType>(httpContext.Request.Headers["X-Device-Type"].ToString(), out var deviceType) ? deviceType : HbtDeviceType.Other,
                    DeviceModel = httpContext.Request.Headers["X-Device-Model"].ToString(),
                    OsType = int.TryParse(httpContext.Request.Headers["X-OS-Type"].ToString(), out var osType) ? osType : 0,
                    OsVersion = httpContext.Request.Headers["X-OS-Version"].ToString(),
                    BrowserType = int.TryParse(httpContext.Request.Headers["X-Browser-Type"].ToString(), out var browserType) ? browserType : 0,
                    BrowserVersion = httpContext.Request.Headers["X-Browser-Version"].ToString(),
                    Resolution = httpContext.Request.Headers["X-Resolution"].ToString(),
                    Location = httpContext.Request.Headers["X-Location"].ToString(),
                    DeviceFingerprint = httpContext.Request.Headers["X-Device-Fingerprint"].ToString()
                };

                if (string.IsNullOrEmpty(deviceInfo.DeviceId))
                {
                    _logger.Warn("未找到设备ID");
                    return;
                }

                // 获取访问令牌
                var accessToken = httpContext.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
                if (string.IsNullOrEmpty(accessToken))
                {
                    _logger.Warn("未找到访问令牌");
                    return;
                }

                // 从token中获取用户ID
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(accessToken);
                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    _logger.Warn("未找到用户ID");
                    return;
                }

                _logger.Info("用户 {UserId} 正在连接，设备信息: {DeviceInfo}", userId, JsonSerializer.Serialize(deviceInfo));

                // 使用与登录时相同的设备ID生成方法
                var (deviceId, _) = _deviceIdGenerator.GenerateIds(JsonSerializer.Serialize(deviceInfo), userId);
                _logger.Info("生成的设备ID: {DeviceId}", deviceId);

                // 检查用户设备数量限制
                var userDevices = _userService.GetUserDevices(userId);
                if (userDevices.Count >= _signalRConfig.UserManagement.MaxDevicesPerUser)
                {
                    if (_signalRConfig.UserManagement.KickoutOldSession)
                    {
                        // 踢出最早的设备
                        var oldestDevice = userDevices.OrderBy(d => d.LastActivity).First();
                        await DisconnectClientAsync(oldestDevice.ConnectionId);
                    }
                    else
                    {
                        _logger.Warn("用户 {UserId} 已达到最大设备数量限制", userId);
                        return;
                    }
                }

                // 检查用户是否已存在
                HbtOnlineUser? existingUser = null;
                try
                {
                    existingUser = await _userService.GetOnlineUserByDeviceAsync(long.Parse(userId), deviceId);
                }
                catch (Exception ex)
                {
                    _logger.Info("未找到现有用户记录，准备创建新记录 - 用户ID: {UserId}, 设备ID: {DeviceId}", userId, deviceId, ex.Message);
                }

                if (existingUser != null)
                {
                    _logger.Info("找到现有用户记录，准备更新 - 用户ID: {UserId}, 设备ID: {DeviceId}", userId, deviceId);
                    // 更新现有用户
                    existingUser.ConnectionId = Context.ConnectionId;
                    existingUser.LastActivity = DateTime.Now;
                    existingUser.LastHeartbeat = DateTime.Now;
                    existingUser.OnlineStatus = 0;
                    await _userService.SaveOnlineUserAsync(existingUser);
                    _logger.Info("用户记录更新成功 - 用户ID: {UserId}, 连接ID: {ConnectionId}", userId, Context.ConnectionId);
                }
                else
                {
                    _logger.Info("未找到现有用户记录，准备创建新记录 - 用户ID: {UserId}, 设备ID: {DeviceId}", userId, deviceId);
                    // 创建新用户记录
                    var newUser = new HbtOnlineUser
                    {
                        UserId = long.Parse(userId),
                        DeviceId = deviceId,
                        ConnectionId = Context.ConnectionId,
                        LastActivity = DateTime.Now,
                        LastHeartbeat = DateTime.Now,
                        OnlineStatus = 0,
                        GroupId = _signalRConfig.UserManagement.DefaultGroupId,
                        ClientIp = Context.GetHttpContext()?.Connection?.RemoteIpAddress?.ToString(),
                        UserAgent = Context.GetHttpContext()?.Request.Headers["User-Agent"].ToString()
                    };
                    await _userService.SaveOnlineUserAsync(newUser);
                    _logger.Info("新用户记录创建成功 - 用户ID: {UserId}, 连接ID: {ConnectionId}", userId, Context.ConnectionId);
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
                    GroupId = _signalRConfig.DeviceManagement.DefaultGroupId,
                    IpAddress = Context.GetHttpContext()?.Connection?.RemoteIpAddress?.ToString(),
                    UserAgent = Context.GetHttpContext()?.Request.Headers["User-Agent"].ToString()
                };
                _logger.Info("准备添加设备信息 - 用户ID: {UserId}, 设备ID: {DeviceId}, 连接ID: {ConnectionId}",
                    device.UserId, device.DeviceId, device.ConnectionId);
                await _userService.AddUserDevice(userId, deviceId, Context.ConnectionId);
                _logger.Info("设备信息添加成功 - 用户ID: {UserId}, 设备ID: {DeviceId}", userId, deviceId);

                // 验证设备是否已添加
                var devices = _userService.GetUserDevices(userId);
                _logger.Info("验证设备信息 - 用户ID: {UserId}, 设备数量: {DeviceCount}", userId, devices.Count);

                await Clients.Client(Context.ConnectionId).ReceiveMessage("连接成功");
                _logger.Info("用户 {UserId} 连接成功，连接ID: {ConnectionId}", userId, Context.ConnectionId);
            }
            catch (Exception ex)
            {
                _logger.Error("连接处理失败", ex.Message);
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
                _logger.Error("SignalR断开连接失败: {ConnectionId}", Context.ConnectionId, ex.Message);
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
                    _logger.Warn("发送者未找到: ConnectionId={ConnectionId}", Context.ConnectionId);
                    return;
                }

                _logger.Info("开始发送消息 - 发送者: {SenderId}, 接收者: {UserId}, 内容: {Message}, 当前连接ID: {ConnectionId}",
                    currentUser.UserId, userId, message, Context.ConnectionId);

                // 将字符串类型的userId转换为long
                if (!long.TryParse(userId, out long targetUserId))
                {
                    _logger.Warn("无效的用户ID格式: {UserId}", userId);
                    return;
                }

                var devices = _userService.GetUserDevices(targetUserId.ToString());
                _logger.Info("找到接收者 {UserId} 的设备数量: {DeviceCount}", userId, devices.Count());

                if (!devices.Any())
                {
                    _logger.Warn("接收者 {UserId} 没有在线设备", userId);
                    return;
                }

                foreach (var device in devices)
                {
                    try
                    {
                        _logger.Info("正在向设备发送消息 - 设备ID: {DeviceId}, 连接ID: {ConnectionId}",
                            device.DeviceId, device.ConnectionId);

                        var client = Clients.Client(device.ConnectionId);
                        if (client == null)
                        {
                            _logger.Warn("无法获取客户端连接 - 设备ID: {DeviceId}, 连接ID: {ConnectionId}",
                                device.DeviceId, device.ConnectionId);
                            continue;
                        }

                        await client.ReceiveMessage(JsonSerializer.Serialize(new
                        {
                            SenderId = currentUser.UserId,
                            ReceiverId = userId,
                            Content = message,
                            Timestamp = DateTime.Now
                        }));

                        _logger.Info("消息发送成功 - 设备ID: {DeviceId}, 连接ID: {ConnectionId}",
                            device.DeviceId, device.ConnectionId);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("向设备发送消息失败 - 设备ID: {DeviceId}, 连接ID: {ConnectionId}",
                            device.DeviceId, device.ConnectionId, ex.Message);
                    }
                }

                _logger.Info("消息发送处理完成");
            }
            catch (Exception ex)
            {
                _logger.Error("发送消息过程中发生错误", ex.Message);
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
                _logger.Info("准备断开客户端连接: ConnectionId={ConnectionId}", connectionId);

                // 1. 发送强制下线消息
                try
                {
                    await Clients.Client(connectionId).ForceOffline("您的账号已在其他设备登录，如非本人操作，请及时修改密码！");
                    _logger.Info("已发送强制下线消息: ConnectionId={ConnectionId}", connectionId);
                }
                catch (Exception ex)
                {
                    _logger.Error("发送强制下线消息失败: ConnectionId={ConnectionId}", connectionId, ex.Message);
                }

                // 2. 等待一段时间让客户端处理消息
                await Task.Delay(1000);

                // 3. 断开连接
                try
                {
                    if (Context.ConnectionId == connectionId)
                    {
                        Context.Abort();
                        _logger.Info("已中止当前连接: ConnectionId={ConnectionId}", connectionId);
                    }
                    else
                    {
                        var hubContext = _hubContext.Clients.Client(connectionId);
                        if (hubContext != null)
                        {
                            await hubContext.SendAsync("ForceOffline", "您的账号已在其他设备登录，如非本人操作，请及时修改密码！");
                            _logger.Info("已发送强制下线消息给其他连接: ConnectionId={ConnectionId}", connectionId);
                        }
                        else
                        {
                            _logger.Warn("未找到连接: ConnectionId={ConnectionId}", connectionId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error("中止连接失败: ConnectionId={ConnectionId}", connectionId, ex.Message);
                }

                _logger.Info("已断开客户端连接: ConnectionId={ConnectionId}", connectionId);
            }
            catch (Exception ex)
            {
                _logger.Error("断开客户端连接时发生错误: ConnectionId={ConnectionId}", connectionId, ex.Message);
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
                _logger.Error("获取 Hub 上下文失败: ConnectionId={ConnectionId}", connectionId, ex.Message);
            }
            return null;
        }
    }

    // 添加配置类
    public class SignalRConfig
    {
        public bool EnableDetailedErrors { get; set; }
        public int ClientTimeoutInterval { get; set; }
        public int KeepAliveInterval { get; set; }
        public int HandshakeTimeout { get; set; }
        public int MaximumReceiveMessageSize { get; set; }
        public int StreamBufferCapacity { get; set; }
        public bool EnableMessagePack { get; set; }
        public TransportConfig Transport { get; set; }
        public AuthenticationConfig Authentication { get; set; }
        public UserManagementConfig UserManagement { get; set; }
        public DeviceManagementConfig DeviceManagement { get; set; }
    }

    public class TransportConfig
    {
        public WebSocketConfig WebSockets { get; set; }
        public ServerSentEventsConfig ServerSentEvents { get; set; }
        public LongPollingConfig LongPolling { get; set; }
    }

    public class WebSocketConfig
    {
        public int CloseTimeout { get; set; }
        public string SubProtocol { get; set; }
    }

    public class ServerSentEventsConfig
    {
        public int ClientTimeoutInterval { get; set; }
    }

    public class LongPollingConfig
    {
        public int PollTimeout { get; set; }
    }

    public class AuthenticationConfig
    {
        public bool RequireAuthentication { get; set; }
        public TokenValidationConfig TokenValidation { get; set; }
    }

    public class TokenValidationConfig
    {
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
    }

    public class UserManagementConfig
    {
        public int MaxDevicesPerUser { get; set; }
        public bool AllowMultipleConnections { get; set; }
        public bool KickoutOldSession { get; set; }
        public bool NotifyKickout { get; set; }
        public int DefaultGroupId { get; set; }
    }

    public class DeviceManagementConfig
    {
        public bool EnableDeviceTracking { get; set; }
        public int MaxDevicesPerUser { get; set; }
        public int DeviceTimeoutMinutes { get; set; }
        public int DefaultGroupId { get; set; }
    }
}