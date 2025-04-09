using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Domain.IServices.SignalR;
using Lean.Hbt.Domain.IServices.Security;
using Lean.Hbt.Common.Models;
using Microsoft.AspNetCore.SignalR;
using Lean.Hbt.Infrastructure.SignalR;
using Lean.Hbt.Domain.Entities;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.Entities.RealTime;
using Lean.Hbt.Domain.Entities.Identity;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.Infrastructure.Security
{
    /// <summary>
    /// 登录服务实现
    /// </summary>
    public class HbtSingleSignOnService : IHbtSingleSignOnService
    {
        private readonly ILogger<HbtSingleSignOnService> _logger;
        private readonly IHbtSignalRUserService _signalRUserService;
        private readonly IHbtSignalRClient _signalRClient;
        private readonly HbtSingleSignOnOptions _options;
        private readonly IHubContext<HbtSignalRHub> _hubContext;
        private readonly IHbtRepository<HbtUser> _userRepository;
        private readonly IHbtRepository<HbtOnlineUser> _onlineUserRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="signalRUserService">SignalR用户服务</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="options">单点登录配置选项</param>
        /// <param name="hubContext">SignalR hub context</param>
        /// <param name="userRepository">用户仓库</param>
        /// <param name="onlineUserRepository">在线用户仓库</param>
        public HbtSingleSignOnService(
            ILogger<HbtSingleSignOnService> logger,
            IHbtSignalRUserService signalRUserService,
            IHbtSignalRClient signalRClient,
            IOptions<HbtSingleSignOnOptions> options,
            IHubContext<HbtSignalRHub> hubContext,
            IHbtRepository<HbtUser> userRepository,
            IHbtRepository<HbtOnlineUser> onlineUserRepository)
        {
            _logger = logger;
            _signalRUserService = signalRUserService;
            _signalRClient = signalRClient;
            _options = options.Value;
            _hubContext = hubContext;
            _userRepository = userRepository;
            _onlineUserRepository = onlineUserRepository;
        }

        /// <summary>
        /// 检查用户是否可以登录
        /// </summary>
        public async Task<bool> CanUserLoginAsync(long userId, string userName)
        {
            var devices = _signalRUserService.GetUserDevices(userId.ToString());
            if (!devices.Any())
            {
                return true;
            }

            // 如果启用了单点登录，且已有设备登录，则不允许新设备登录
            if (_options.Enabled)
            {
                return false;
            }

            // 如果未启用单点登录，则检查是否超过最大设备数
            return devices.Count < _options.MaxDevices;
        }

        /// <summary>
        /// 处理新设备登录
        /// </summary>
        public async Task HandleNewLoginAsync(string userId, string userName, string deviceId)
        {
            var devices = _signalRUserService.GetUserDevices(userId);
            if (!devices.Any())
            {
                return;
            }

            // 如果启用了单点登录，踢出现有设备
            if (_options.Enabled && _options.KickoutOldSession)
            {
                await HandleKickoutAsync(devices.First());
                return;
            }

            // 如果超过最大设备数，踢出最早登录的设备
            if (devices.Count >= _options.MaxDevices)
            {
                var oldestDevice = devices.OrderBy(d => d.LastActivity).First();
                await HandleKickoutAsync(oldestDevice);
            }
        }

        /// <summary>
        /// 处理设备被踢出
        /// </summary>
        public async Task HandleKickoutAsync(HbtSignalRDevice device)
        {
            if (_options.NotifyKickout)
            {
                // TODO: 发送踢出通知
                _logger.LogInformation("用户 {UserId} 的设备 {DeviceId} 被踢出", device.UserId, device.DeviceId);
            }

            await _signalRUserService.RemoveUserDevice(device.UserId.ToString(), device.DeviceId);
        }

        /// <summary>
        /// 检查登录状态
        /// </summary>
        public async Task<(bool canLogin, string? existingDeviceInfo)> CheckLoginAsync(long userId, string userName, string deviceInfo)
        {
            var devices = _signalRUserService.GetUserDevices(userId.ToString());
            if (!devices.Any())
            {
                return (true, null);
            }

            // 如果启用了单点登录，且已有设备登录，则不允许新设备登录
            if (_options.Enabled)
            {
                var existingDevice = devices.First();
                return (false, $"用户已在 {existingDevice.DeviceName} 设备上登录");
            }

            // 如果超过最大设备数，不允许新设备登录
            if (devices.Count >= _options.MaxDevices)
            {
                return (false, $"已达到最大设备数限制 ({_options.MaxDevices})");
            }

            return (true, null);
        }

        /// <summary>
        /// 处理用户登录
        /// </summary>
        public async Task HandleUserLoginAsync(string userId, string connectionId)
        {
            // 如果未启用单点登录，直接返回
            if (_options.Enabled)
            {
                _logger.LogInformation("单点登录未启用，允许多点登录");
                return;
            }

            // 获取用户现有会话
            var userIdLong = long.Parse(userId);
            var exp = Expressionable.Create<HbtOnlineUser>();
            exp.And(u => u.UserId == userIdLong && u.OnlineStatus == 0);
            var existingOnlineUsers = await _onlineUserRepository.GetListAsync(exp.ToExpression());

            // 检查是否超过最大并发会话数
            if (existingOnlineUsers.Count >= _options.MaxDevices)
            {
                _logger.LogWarning("用户 {UserId} 已达到最大并发会话数 {MaxSessions}", userId, _options.MaxDevices);
                if (_options.KickoutOldSession)
                {
                    // 按最后活动时间排序，踢出最早的会话
                    var oldestSession = existingOnlineUsers.OrderBy(u => u.LastActivity).First();
                    var device = new HbtSignalRDevice
                    {
                        UserId = oldestSession.UserId,
                        DeviceId = oldestSession.DeviceId,
                        DeviceName = "离线设备"
                    };
                    await HandleKickoutAsync(device);
                }
                else
                {
                    return;
                }
            }

            // 检查是否超过每个用户最大连接数
            if (existingOnlineUsers.Count >= _options.MaxConnectionsPerUser)
            {
                _logger.LogWarning("用户 {UserId} 已达到最大连接数 {MaxConnections}", userId, _options.MaxConnectionsPerUser);
                if (_options.KickoutOldSession)
                {
                    // 按最后活动时间排序，踢出最早的连接
                    var oldestConnection = existingOnlineUsers.OrderBy(u => u.LastActivity).First();
                    var deviceToKick = new HbtSignalRDevice
                    {
                        UserId = oldestConnection.UserId,
                        DeviceId = oldestConnection.DeviceId,
                        DeviceName = "离线设备"
                    };
                    await HandleKickoutAsync(deviceToKick);
                }
                else
                {
                    return;
                }
            }

            if (existingOnlineUsers.Any())
            {
                // 如果配置了踢出旧会话
                if (_options.KickoutOldSession)
                {
                    foreach (var oldUser in existingOnlineUsers)
                    {
                        // 如果配置了通知
                        if (_options.NotifyKickout)
                        {
                            await _signalRClient.ForceOffline(_options.KickoutMessage);
                        }
                        // 关闭旧连接
                        await _hubContext.Clients.Client(oldUser.ConnectionId).SendAsync("ForceOffline", _options.KickoutMessage);
                    }
                }
            }
        }

        /// <summary>
        /// 处理用户登出
        /// </summary>
        public async Task HandleLogoutAsync(string userId, string connectionId)
        {
            await _signalRUserService.DeleteOnlineUserAsync(connectionId, userId);
            _logger.LogInformation("用户 {UserId} 已登出，连接 {ConnectionId} 已删除", userId, connectionId);
        }

        /// <summary>
        /// 检查用户是否已在其他设备登录
        /// </summary>
        public async Task<bool> IsUserLoggedInElsewhereAsync(string userId, string connectionId)
        {
            var userIdLong = long.Parse(userId);
            var exp = Expressionable.Create<HbtOnlineUser>();
            exp.And(u => u.UserId == userIdLong && u.OnlineStatus == 0 && u.ConnectionId != connectionId);
            var existingOnlineUsers = await _onlineUserRepository.GetListAsync(exp.ToExpression());
            return existingOnlineUsers.Any();
        }

        /// <summary>
        /// 获取用户的活跃连接数
        /// </summary>
        public async Task<int> GetActiveConnectionCountAsync(string userId)
        {
            var userIdLong = long.Parse(userId);
            var exp = Expressionable.Create<HbtOnlineUser>();
            exp.And(u => u.UserId == userIdLong && u.OnlineStatus == 0);
            var onlineUsers = await _onlineUserRepository.GetListAsync(exp.ToExpression());
            return onlineUsers.Count;
        }
    }

    /// <summary>
    /// 登录检查结果
    /// </summary>
    public class LoginCheckResult
    {
        /// <summary>
        /// 是否可以登录
        /// </summary>
        public bool CanLogin { get; set; }

        /// <summary>
        /// 已存在的设备列表
        /// </summary>
        public List<HbtSignalRDevice> ExistingDevices { get; set; } = new();
    }

    /// <summary>
    /// 设备信息
    /// </summary>
    public class DeviceInfo
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 最后活动时间
        /// </summary>
        public DateTime LastActivity { get; set; }
    }
} 