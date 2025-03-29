//===================================================================
// 项目名 : Lean.Hbt.Infrastructure
// 文件名 : HbtSignalRHub.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : SignalR集线器
//===================================================================

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.IServices.SignalR;
using Lean.Hbt.Application.Services.RealTime;
using Lean.Hbt.Domain.Identity;
using Lean.Hbt.Application.Dtos.RealTime;
using Lean.Hbt.Domain.IServices.Security;
using System.Text.Json;
using System.Linq;
using System.Collections.Generic;
using Lean.Hbt.Infrastructure.Identity;
using Lean.Hbt.Domain.IServices.Identity;
using Lean.Hbt.Domain.Entities.RealTime;
namespace Lean.Hbt.Infrastructure.SignalR
{
    /// <summary>
    /// SignalR集线器
    /// </summary>
    public class HbtSignalRHub : Hub<IHbtSignalRClient>, IHbtSignalRHub
    {
        private readonly ILogger<HbtSignalRHub> _logger;
        private readonly IHbtSignalRUserService _signalRUserService;
        private readonly IHbtSingleSignOnService _singleSignOnService;
        private readonly IHbtDeviceIdGenerator _deviceIdGenerator;
        private readonly IHbtRepository<HbtOnlineUser> _repository;
        private readonly IHubContext<HbtSignalRHub> _hubContext;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSignalRHub(
            ILogger<HbtSignalRHub> logger,
            IHbtSignalRUserService signalRUserService,
            IHbtSingleSignOnService singleSignOnService,
            IHbtDeviceIdGenerator deviceIdGenerator,
            IHbtRepository<HbtOnlineUser> repository,
            IHubContext<HbtSignalRHub> hubContext)
        {
            _logger = logger;
            _signalRUserService = signalRUserService;
            _singleSignOnService = singleSignOnService;
            _deviceIdGenerator = deviceIdGenerator;
            _repository = repository;
            _hubContext = hubContext;
        }

        /// <summary>
        /// 客户端连接时
        /// </summary>
        public override async Task OnConnectedAsync()
        {
            try
            {
                var httpContext = Context.GetHttpContext();
                var deviceInfoJson = httpContext?.Request.Headers["X-Device-Info"].ToString();
                var userId = Context.UserIdentifier;
                var userName = Context.User?.Identity?.Name;

                _logger.LogInformation("收到新的连接请求: ConnectionId={ConnectionId}, UserId={UserId}, UserName={UserName}", 
                    Context.ConnectionId, userId, userName);

                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userName))
                {
                    _logger.LogWarning("用户未认证，拒绝连接: ConnectionId={ConnectionId}", Context.ConnectionId);
                    Context.Abort();
                    return;
                }

                // 生成设备ID和连接ID
                var (deviceId, connectionId) = _deviceIdGenerator.GenerateIds(deviceInfoJson, userId);
                _logger.LogInformation("生成设备ID和连接ID: DeviceId={DeviceId}, ConnectionId={ConnectionId}", deviceId, connectionId);

                // 处理新的登录连接
                await _singleSignOnService.HandleNewLoginAsync(userId, userName, connectionId);
                _logger.LogInformation("处理单点登录完成: UserId={UserId}, ConnectionId={ConnectionId}", userId, connectionId);

                // 保存在线用户信息
                var onlineUser = new HbtOnlineUser
                {
                    UserId = long.Parse(userId),
                    ConnectionId = connectionId,
                    DeviceId = deviceId,
                    ClientIp = httpContext?.Connection?.RemoteIpAddress?.ToString(),
                    UserAgent = httpContext?.Request?.Headers["User-Agent"].ToString(),
                    LastActivity = DateTime.Now,
                    LastHeartbeat = DateTime.Now,
                    OnlineStatus = 0
                };

                await _signalRUserService.SaveOnlineUserAsync(onlineUser);
                _logger.LogInformation("保存在线用户信息成功: UserId={UserId}, ConnectionId={ConnectionId}", userId, connectionId);

                await base.OnConnectedAsync();
                _logger.LogInformation("连接处理完成: UserId={UserId}, ConnectionId={ConnectionId}", userId, connectionId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理客户端连接时发生错误: ConnectionId={ConnectionId}", Context.ConnectionId);
                throw;
            }
        }

        /// <summary>
        /// 客户端断开连接时
        /// </summary>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                var connectionId = Context.ConnectionId;
                var userId = Context.UserIdentifier;
                var userName = Context.User?.Identity?.Name;

                _logger.LogInformation("收到断开连接请求: ConnectionId={ConnectionId}, UserId={UserId}, UserName={UserName}, Exception={Exception}", 
                    connectionId, userId, userName, exception?.Message);

                if (!string.IsNullOrEmpty(userId))
                {
                    // 处理用户登出
                    await _singleSignOnService.HandleLogoutAsync(userId, connectionId);
                    _logger.LogInformation("处理用户登出完成: UserId={UserId}, ConnectionId={ConnectionId}", userId, connectionId);
                }

                await base.OnDisconnectedAsync(exception);
                _logger.LogInformation("断开连接处理完成: ConnectionId={ConnectionId}", connectionId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理客户端断开连接时发生错误: ConnectionId={ConnectionId}", Context.ConnectionId);
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
            var targetUsers = await _signalRUserService.GetMailReceivers(mailId);
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
            var targetUsers = await _signalRUserService.GetNoticeReceivers(noticeId);
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
            var targetUsers = await _signalRUserService.GetMailReceivers(mailId);
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
            var targetUsers = await _signalRUserService.GetNoticeReceivers(noticeId);
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
            await Clients.Group($"User_{userId}").ReceivePersonalNotice(notification);
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
            await Clients.Caller.ReceiveHeartbeat(DateTime.Now);
        }

        /// <summary>
        /// 获取设备唯一标识
        /// </summary>
        private string GetDeviceIdentifier(string userAgent, string clientIp)
        {
            // 解析User-Agent
            var browser = GetBrowserInfo(userAgent);
            var device = GetDeviceInfo(userAgent);
            var os = GetOSInfo(userAgent);
            
            // 组合设备标识（使用特定信息的组合作为设备标识）
            return $"{clientIp}_{browser}_{device}_{os}".GetHashCode().ToString("X8");
        }

        /// <summary>
        /// 获取浏览器信息
        /// </summary>
        private string GetBrowserInfo(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent)) return "Unknown";
            
            // 提取主要浏览器标识
            if (userAgent.Contains("Chrome/")) return "Chrome";
            if (userAgent.Contains("Firefox/")) return "Firefox";
            if (userAgent.Contains("Safari/") && !userAgent.Contains("Chrome/")) return "Safari";
            if (userAgent.Contains("Edge/") || userAgent.Contains("Edg/")) return "Edge";
            if (userAgent.Contains("MSIE") || userAgent.Contains("Trident/")) return "IE";
            
            return "Other";
        }

        /// <summary>
        /// 获取设备信息
        /// </summary>
        private string GetDeviceInfo(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent)) return "Unknown";
            
            // 提取设备类型
            if (userAgent.Contains("Mobile")) return "Mobile";
            if (userAgent.Contains("Tablet")) return "Tablet";
            if (userAgent.Contains("iPad")) return "iPad";
            if (userAgent.Contains("iPhone")) return "iPhone";
            if (userAgent.Contains("Android")) return "Android";
            
            return "Desktop";
        }

        /// <summary>
        /// 获取操作系统信息
        /// </summary>
        private string GetOSInfo(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent)) return "Unknown";
            
            // 提取操作系统
            if (userAgent.Contains("Windows")) return "Windows";
            if (userAgent.Contains("Mac OS")) return "MacOS";
            if (userAgent.Contains("Linux")) return "Linux";
            if (userAgent.Contains("Android")) return "Android";
            if (userAgent.Contains("iOS")) return "iOS";
            
            return "Other";
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
                try {
                    await Clients.Client(connectionId).ForceOffline("您已被管理员强制下线");
                    _logger.LogInformation("已发送强制下线消息: ConnectionId={ConnectionId}", connectionId);
                } catch (Exception ex) {
                    _logger.LogError(ex, "发送强制下线消息失败: ConnectionId={ConnectionId}", connectionId);
                }

                // 2. 等待一段时间让客户端处理消息
                await Task.Delay(1000);

                // 3. 断开连接
                try {
                    var context = Context.ConnectionId == connectionId ? Context : GetHubContext(connectionId);
                    if (context != null)
                    {
                        context.Abort();
                        _logger.LogInformation("已中止连接: ConnectionId={ConnectionId}", connectionId);
                    }
                    else
                    {
                        _logger.LogWarning("未找到连接上下文: ConnectionId={ConnectionId}", connectionId);
                    }
                } catch (Exception ex) {
                    _logger.LogError(ex, "中止连接失败: ConnectionId={ConnectionId}", connectionId);
                }

                _logger.LogInformation("已断开客户端连接: ConnectionId={ConnectionId}", connectionId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "断开客户端连接时发生错误: ConnectionId={ConnectionId}", connectionId);
                throw;
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