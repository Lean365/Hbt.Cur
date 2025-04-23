using Lean.Hbt.Common.Models;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.Entities.SignalR;
using Lean.Hbt.Domain.IServices.Security;
using Lean.Hbt.Domain.IServices.SignalR;
using Lean.Hbt.Infrastructure.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace Lean.Hbt.Infrastructure.Security
{
    /// <summary>
    /// 登录服务实现
    /// </summary>
    public class HbtSingleSignOnService : IHbtSingleSignOnService
    {
        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly IHbtLogger _logger;

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
            IHbtLogger logger,
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
            var devices = await Task.Run(() => _signalRUserService.GetUserDevices(userId.ToString()));
            if (!devices.Any())
            {
                return true;
            }

            // 如果启用了单点登录，且已有设备登录，则不允许新设备登录
            if (_options.Enabled && devices.Any())
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
                _logger.Info("用户 {UserId} 的设备 {DeviceId} 被踢出", device.UserId, device.DeviceId);
            }

            await _signalRUserService.RemoveUserDevice(device.UserId.ToString(), device.DeviceId);
        }

        /// <summary>
        /// 检查登录状态
        /// </summary>
        public async Task<(bool canLogin, string? existingDeviceInfo)> CheckLoginAsync(long userId, string userName, string deviceInfo)
        {
            var devices = await Task.Run(() => _signalRUserService.GetUserDevices(userId.ToString()));
            if (!devices.Any())
            {
                return (true, null);
            }

            // 如果启用了单点登录，且已有设备登录，则不允许新设备登录
            if (_options.Enabled && devices.Any())
            {
                var existingDevice = devices.First();
                return (false, $"用户已在 {existingDevice.DeviceName} 设备上登录");
            }

            // 如果未启用单点登录，则检查是否超过最大设备数
            if (!_options.Enabled && devices.Count >= _options.MaxDevices)
            {
                return (false, $"已达到最大设备数限制 ({_options.MaxDevices})");
            }

            return (true, null);
        }

        /// <summary>
        /// 处理用户登录
        /// </summary>
        public async Task<HbtApiResult> HandleLoginAsync(long userId, string deviceId, string connectionId, string ipAddress, string userAgent)
        {
            try
            {
                _logger.Info("开始处理用户登录 - 用户ID: {UserId}, 设备ID: {DeviceId}", userId, deviceId);

                // 检查是否启用单点登录
                if (!_options.Enabled)
                {
                    _logger.Info("单点登录未启用，允许用户 {UserId} 登录", userId);
                    return new HbtApiResult { Code = 200, Msg = "登录成功" };
                }

                // 获取用户所有设备
                var devices = _signalRUserService.GetUserDevices(userId.ToString());

                // 检查是否超过最大设备数限制
                if (devices.Count >= _options.MaxDevices)
                {
                    if (!_options.KickoutOldSession)
                    {
                        _logger.Warn("用户 {UserId} 已达到最大设备数限制 ({MaxDevices})", userId, _options.MaxDevices);
                        return new HbtApiResult { Code = 500, Msg = $"已达到最大设备数限制 ({_options.MaxDevices})" };
                    }

                    // 如果允许踢出旧会话，则踢出最早登录的设备
                    var oldestDevice = devices.OrderBy(d => d.LastActivity).First();

                    // 1. 更新数据库中的在线用户状态
                    var exp = Expressionable.Create<HbtOnlineUser>();
                    exp.And(u => u.UserId == userId && u.DeviceId == oldestDevice.DeviceId);
                    var oldUser = await _onlineUserRepository.GetFirstAsync(exp.ToExpression());
                    if (oldUser != null)
                    {
                        oldUser.OnlineStatus = 1; // 设置为离线
                        oldUser.LastActivity = DateTime.Now;
                        await _onlineUserRepository.UpdateAsync(oldUser);
                    }

                    // 2. 从内存中移除设备
                    await _signalRUserService.RemoveUserDevice(userId.ToString(), oldestDevice.DeviceId);

                    // 3. 发送踢出通知，包含具体原因
                    if (_options.NotifyKickout)
                    {
                        var kickoutReason = $"您的账号已在其他设备登录，当前设备已被强制下线。原因：已达到最大设备数限制 ({_options.MaxDevices})";
                        await _signalRClient.ForceOffline(kickoutReason);
                    }

                    _logger.Info("踢出用户 {UserId} 的最早登录设备 {DeviceId}", userId, oldestDevice.DeviceId);
                }

                // 检查用户是否已在当前设备登录
                var existingUser = await _signalRUserService.GetOnlineUserByDeviceAsync(userId, deviceId);
                if (existingUser != null)
                {
                    _logger.Info("用户 {UserId} 已在设备 {DeviceId} 登录，更新登录信息", userId, deviceId);
                }

                // 创建或更新在线用户记录
                var onlineUser = new HbtOnlineUser
                {
                    UserId = userId,
                    DeviceId = deviceId,
                    ConnectionId = connectionId,
                    ClientIp = ipAddress,
                    UserAgent = userAgent,
                    LastActivity = DateTime.Now,
                    LastHeartbeat = DateTime.Now,
                    OnlineStatus = 0, // 0表示在线状态
                    TenantId = 0,
                    GroupId = 0
                };

                await _signalRUserService.SaveOnlineUserAsync(onlineUser);
                _logger.Info("用户 {UserId} 登录成功", userId);

                return new HbtApiResult { Code = 200, Msg = "登录成功" };
            }
            catch (Exception ex)
            {
                _logger.Error("处理用户登录时发生错误 - 用户ID: {UserId}", userId);
                return new HbtApiResult { Code = 500, Msg = "处理登录请求时发生错误" };
            }
        }

        /// <summary>
        /// 处理用户登出
        /// </summary>
        public async Task HandleLogoutAsync(string userId, string connectionId)
        {
            try
            {
                _logger.Info("开始处理用户退出 - 用户ID: {UserId}, 连接ID: {ConnectionId}", userId, connectionId);

                // 删除在线用户记录
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.UserId == long.Parse(userId) && u.ConnectionId == connectionId);
                await _onlineUserRepository.DeleteAsync(exp.ToExpression());

                // 清理设备信息
                var devices = _signalRUserService.GetUserDevices(userId);
                foreach (var device in devices)
                {
                    await _signalRUserService.RemoveUserDevice(device.UserId.ToString(), device.DeviceId);
                }

                _logger.Info("用户 {UserId} 退出成功", userId);
            }
            catch (Exception ex)
            {
                _logger.Error("处理用户退出时发生错误 - 用户ID: {UserId}", userId);
            }
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

        /// <summary>
        /// 处理用户登录
        /// </summary>
        public async Task HandleUserLoginAsync(string userId, string connectionId)
        {
            try
            {
                _logger.Info("开始处理用户登录 - 用户ID: {UserId}, 连接ID: {ConnectionId}", userId, connectionId);

                // 检查是否启用单点登录
                if (!_options.Enabled)
                {
                    _logger.Info("单点登录未启用，允许用户 {UserId} 登录", userId);
                    return;
                }

                // 检查用户是否已登录
                var existingUser = await _signalRUserService.GetOnlineUserAsync(connectionId);
                if (existingUser != null)
                {
                    // 如果用户已登录，检查是否允许重复登录
                    if (!_options.KickoutOldSession)
                    {
                        _logger.Warn("用户 {UserId} 已在其他设备登录，且不允许重复登录", userId);
                        return;
                    }
                }

                _logger.Info("用户 {UserId} 登录成功", userId);
            }
            catch (Exception ex)
            {
                _logger.Error("处理用户登录时发生错误 - 用户ID: {UserId}", userId);
            }
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