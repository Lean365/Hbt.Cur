//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSignalRUserService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-24 10:00
// 版本号 : V0.0.1
// 描述    : SignalR用户服务实现
//===================================================================

using System.Collections.Concurrent;
using Hbt.Cur.Common.Models;
using Hbt.Cur.Domain.Entities.SignalR;
using Hbt.Cur.Domain.IServices.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace Hbt.Cur.Infrastructure.SignalR
{
    /// <summary>
    /// SignalR用户服务
    /// </summary>
    public class HbtSignalRUserService : IHbtSignalRUserService
    {
        private readonly IHubContext<HbtSignalRHub> _hubContext;

        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly IHbtLogger _logger;
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly IHbtRepositoryFactory _repositoryFactory;
        private static readonly ConcurrentDictionary<string, List<HbtSignalRDevice>> _userDevices = new();

        private IHbtRepository<HbtOnlineUser> Repository => _repositoryFactory.GetAuthRepository<HbtOnlineUser>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hubContext"></param>
        /// <param name="logger"></param>
        /// <param name="repositoryFactory"></param>
        public HbtSignalRUserService(
            IHubContext<HbtSignalRHub> hubContext,
            IHbtLogger logger,

            IHbtRepositoryFactory repositoryFactory)
        {
            _hubContext = hubContext;
            _logger = logger;
            _repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// 添加用户设备
        /// </summary>
        public async Task AddUserDevice(HbtSignalRDevice device)
        {
            try
            {
                _logger.Info("开始添加/更新用户设备 - UserId: {UserId}, DeviceId: {DeviceId}, ConnectionId: {ConnectionId}",
                    device.UserId, device.DeviceId, device.ConnectionId);

                var userId = device.UserId.ToString();
                if (!_userDevices.ContainsKey(userId))
                {
                    _userDevices[userId] = new List<HbtSignalRDevice>();
                }

                // 检查是否存在相同设备ID的设备
                var existingDevices = _userDevices[userId].Where(d => d.DeviceId == device.DeviceId).ToList();
                if (existingDevices.Any())
                {
                    _logger.Info("发现相同设备ID的设备，更新设备信息 - 设备ID: {DeviceId}", device.DeviceId);
                    // 更新所有相同设备ID的设备状态
                    foreach (var existingDevice in existingDevices)
                    {
                        existingDevice.ConnectionId = device.ConnectionId;
                        existingDevice.LastActivity = DateTime.Now;
                        existingDevice.LastHeartbeat = DateTime.Now;
                        existingDevice.OnlineStatus = device.OnlineStatus;
                    }
                }
                else
                {
                    _logger.Info("添加新设备 - 设备ID: {DeviceId}", device.DeviceId);
                    device.LastActivity = DateTime.Now;
                    device.LastHeartbeat = DateTime.Now;
                    _userDevices[userId].Add(device);
                }

                // 保存到数据库
                var onlineUser = new HbtOnlineUser
                {
                    UserId = device.UserId,
                    DeviceId = device.DeviceId,
                    ConnectionId = device.ConnectionId,
                    LastActivity = DateTime.Now,
                    LastHeartbeat = DateTime.Now,
                    OnlineStatus = device.OnlineStatus,

                    GroupId = device.GroupId,
                    ClientIp = device.IpAddress,
                    UserAgent = device.UserAgent
                };

                await SaveOnlineUserAsync(onlineUser);
                _logger.Info("设备信息已同步到数据库 - UserId: {UserId}, DeviceId: {DeviceId}", userId, device.DeviceId);

                _logger.Info("当前用户 {UserId} 的设备数量: {DeviceCount}", userId, _userDevices[userId].Count);
            }
            catch (Exception ex)
            {
                _logger.Error("添加用户设备时发生错误 - UserId: {UserId}, DeviceId: {DeviceId}, Error: {Error}",
                    device.UserId, device.DeviceId, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 获取用户设备列表
        /// </summary>
        public List<HbtSignalRDevice> GetUserDevices(string userId)
        {
            _logger.Info("开始获取用户 {UserId} 的设备列表", userId);

            // 先从内存中获取
            if (_userDevices.TryGetValue(userId, out var devices))
            {
                // 过滤掉已断开的设备
                devices = devices.Where(d => d.OnlineStatus == 0).ToList();
                if (devices.Any())
                {
                    _logger.Info("从内存中找到用户 {UserId} 的在线设备列表，数量: {DeviceCount}", userId, devices.Count);
                    return devices;
                }
                // 如果内存中没有在线设备，从字典中移除
                _userDevices.TryRemove(userId, out _);
            }

            // 如果内存中没有，从数据库查询
            var exp = Expressionable.Create<HbtOnlineUser>();
            exp.And(u => u.UserId == long.Parse(userId) && u.OnlineStatus == 0);
            var onlineUsers = Repository.GetListAsync(exp.ToExpression()).Result;

            if (onlineUsers != null && onlineUsers.Any())
            {
                _logger.Info("从数据库中找到用户 {UserId} 的在线设备列表，数量: {DeviceCount}", userId, onlineUsers.Count);

                // 将数据库中的设备信息添加到内存中
                devices = onlineUsers.Select(u => new HbtSignalRDevice
                {
                    UserId = u.UserId,
                    DeviceId = u.DeviceId,
                    ConnectionId = u.ConnectionId,
                    LastActivity = u.LastActivity,
                    LastHeartbeat = u.LastHeartbeat,
                    OnlineStatus = u.OnlineStatus,

                    GroupId = u.GroupId,
                    IpAddress = u.ClientIp,
                    UserAgent = u.UserAgent
                }).ToList();

                _userDevices[userId] = devices;
                return devices;
            }

            _logger.Warn("未找到用户 {UserId} 的在线设备列表", userId);
            return new List<HbtSignalRDevice>();
        }

        /// <summary>
        /// 移除用户设备
        /// </summary>
        public async Task RemoveUserDevice(string userId, string deviceId)
        {
            if (_userDevices.TryGetValue(userId, out var devices))
            {
                var device = devices.FirstOrDefault(d => d.DeviceId == deviceId);
                if (device != null)
                {
                    devices.Remove(device);
                    if (!devices.Any())
                    {
                        _userDevices.TryRemove(userId, out _);
                    }
                }
            }
            await Task.CompletedTask;
        }

        /// <summary>
        /// 获取用户设备
        /// </summary>
        public HbtSignalRDevice GetUserDevice(string userId, string connectionId)
        {
            try
            {
                _logger.Info("开始获取用户设备 - UserId: {UserId}, ConnectionId: {ConnectionId}", userId, connectionId);

                var devices = GetUserDevices(userId);
                _logger.Info("找到用户设备列表 - UserId: {UserId}, 设备数量: {Count}", userId, devices.Count);

                var device = devices.FirstOrDefault(d => d.ConnectionId == connectionId);
                if (device == null)
                {
                    _logger.Warn("未找到指定连接ID的设备 - UserId: {UserId}, ConnectionId: {ConnectionId}", userId, connectionId);
                    throw new Exception($"未找到连接ID为 {connectionId} 的用户设备");
                }

                _logger.Info("成功获取设备信息 - UserId: {UserId}, DeviceId: {DeviceId}", userId, device.DeviceId);
                return device;
            }
            catch (Exception ex)
            {
                _logger.Error("获取用户设备时发生错误 - UserId: {UserId}, ConnectionId: {ConnectionId}, Error: {Error}",
                    userId, connectionId, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 检查用户是否在线
        /// </summary>
        public bool IsUserOnline(string userId)
        {
            return _userDevices.ContainsKey(userId);
        }

        /// <summary>
        /// 更新设备活动时间
        /// </summary>
        public void UpdateDeviceActivity(string userId, string deviceId)
        {
            if (_userDevices.ContainsKey(userId))
            {
                var device = _userDevices[userId].FirstOrDefault(d => d.DeviceId == deviceId);
                if (device != null)
                {
                    device.LastActivity = DateTime.Now;
                    _logger.Info("更新用户 {UserId} 的设备 {DeviceId} 活动时间", userId, deviceId);
                }
            }
        }

        /// <summary>
        /// 获取所有在线用户
        /// </summary>
        public List<HbtSignalRDevice> GetAllOnlineDevices()
        {
            return _userDevices.Values.SelectMany(d => d).ToList();
        }

        /// <summary>
        /// 保存在线用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task SaveOnlineUserAsync(HbtOnlineUser user)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.UserId == user.UserId && u.DeviceId == user.DeviceId);

                var existingUser = await Repository.GetFirstAsync(exp.ToExpression());
                if (existingUser != null)
                {
                    // 更新现有记录
                    existingUser.ConnectionId = user.ConnectionId;
                    existingUser.LastActivity = user.LastActivity;
                    existingUser.LastHeartbeat = user.LastHeartbeat;
                    existingUser.OnlineStatus = user.OnlineStatus;
                    existingUser.ClientIp = user.ClientIp;
                    existingUser.UserAgent = user.UserAgent;
                    await Repository.UpdateAsync(existingUser);
                }
                else
                {
                    await Repository.CreateAsync(user);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("保存在线用户时发生错误: ConnectionId={ConnectionId}, UserId={UserId}", user.ConnectionId, user.UserId, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 获取在线用户信息
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task<HbtOnlineUser> GetOnlineUserAsync(string connectionId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.ConnectionId == connectionId);
                return await Repository.GetFirstAsync(exp.ToExpression()) ?? throw new Exception($"未找到连接ID为 {connectionId} 的在线用户");
            }
            catch (Exception)
            {
                _logger.Error("获取在线用户信息时发生错误: ConnectionId={ConnectionId}", connectionId);
                throw;
            }
        }

        /// <summary>
        /// 根据设备ID获取在线用户
        /// </summary>
        public async Task<HbtOnlineUser> GetOnlineUserByDeviceAsync(long userId, string deviceId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.UserId == userId && u.DeviceId == deviceId);

                return await Repository.GetFirstAsync(exp.ToExpression()) ?? throw new Exception($"未找到用户ID为 {userId} 设备ID为 {deviceId} 的在线用户");
            }
            catch (Exception)
            {
                _logger.Error("获取在线用户信息时发生错误: UserId={UserId}, DeviceId={DeviceId}", userId, deviceId);
                throw;
            }
        }

        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task<string> GetDeviceInfo(string connectionId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.ConnectionId == connectionId);

                var user = await Repository.GetFirstAsync(exp.ToExpression());
                return user?.DeviceId ?? string.Empty;
            }
            catch (Exception ex)
            {
                _logger.Error("获取设备信息时发生错误: ConnectionId={ConnectionId}", connectionId, ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// 删除在线用户
        /// </summary>
        public async Task<bool> DeleteOnlineUserAsync(string connectionId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.ConnectionId == connectionId);
                var result = await Repository.DeleteAsync(exp.ToExpression());
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.Error("删除在线用户时发生错误: ConnectionId={ConnectionId}", connectionId, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 获取在线用户列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetConnectionIdsAsync(long userId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.UserId == userId && u.OnlineStatus == 0); // 0表示在线状态
                exp.And(u => u.LastActivity > DateTime.Now.AddMinutes(-5)); // 只返回5分钟内有活动的连接

                var users = await Repository.GetListAsync(exp.ToExpression());
                return users.Where(u => !string.IsNullOrEmpty(u.ConnectionId))
                    .Select(u => u.ConnectionId!)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("获取用户连接ID列表时发生错误: UserId={UserId}", userId, ex.Message);
                throw;
            }
        }



        /// <summary>
        /// 更新用户最后活动时间
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task UpdateUserLastActiveTimeAsync(string connectionId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.ConnectionId == connectionId);

                var user = await Repository.GetFirstAsync(exp.ToExpression());
                if (user != null)
                {
                    user.LastActivity = DateTime.Now;
                    await Repository.UpdateAsync(user);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("更新用户最后活动时间时发生错误: ConnectionId={ConnectionId}", connectionId, ex.Message, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 获取邮件接收者列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetMailReceivers(long userId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.UserId == userId && u.OnlineStatus == 0);
                exp.And(u => u.LastActivity > DateTime.Now.AddMinutes(-5));

                var users = await Repository.GetListAsync(exp.ToExpression());
                return users.Where(u => !string.IsNullOrEmpty(u.ConnectionId))
                    .Select(u => u.ConnectionId!)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("获取邮件接收者列表时发生错误: UserId={UserId}", userId, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 获取通知接收者列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetNoticeReceivers(long userId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.UserId == userId && u.OnlineStatus == 0);
                exp.And(u => u.LastActivity > DateTime.Now.AddMinutes(-5));

                var users = await Repository.GetListAsync(exp.ToExpression());
                return users.Where(u => !string.IsNullOrEmpty(u.ConnectionId))
                    .Select(u => u.ConnectionId!)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("获取通知接收者列表时发生错误: UserId={UserId}", userId, ex.Message, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 获取ISO文档接收者列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetIsoDocumentReceivers(long userId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.UserId == userId && u.OnlineStatus == 0);
                exp.And(u => u.LastActivity > DateTime.Now.AddMinutes(-5));

                var users = await Repository.GetListAsync(exp.ToExpression());
                return users.Where(u => !string.IsNullOrEmpty(u.ConnectionId))
                    .Select(u => u.ConnectionId!)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("获取ISO文档接收者列表时发生错误: UserId={UserId}", userId, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 获取会议通知接收者列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetMeetingReceivers(long userId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.UserId == userId && u.OnlineStatus == 0);
                exp.And(u => u.LastActivity > DateTime.Now.AddMinutes(-5));

                var users = await Repository.GetListAsync(exp.ToExpression());
                return users.Where(u => !string.IsNullOrEmpty(u.ConnectionId))
                    .Select(u => u.ConnectionId!)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("获取会议通知接收者列表时发生错误: UserId={UserId}", userId, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 获取用车通知接收者列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetVehicleReceivers(long userId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.UserId == userId && u.OnlineStatus == 0);
                exp.And(u => u.LastActivity > DateTime.Now.AddMinutes(-5));

                var users = await Repository.GetListAsync(exp.ToExpression());
                return users.Where(u => !string.IsNullOrEmpty(u.ConnectionId))
                    .Select(u => u.ConnectionId!)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("获取用车通知接收者列表时发生错误: UserId={UserId}", userId, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 获取Quartz任务通知接收者列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetQuartzReceivers(long userId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.UserId == userId && u.OnlineStatus == 0);
                exp.And(u => u.LastActivity > DateTime.Now.AddMinutes(-5));

                var users = await Repository.GetListAsync(exp.ToExpression());
                return users.Where(u => !string.IsNullOrEmpty(u.ConnectionId))
                    .Select(u => u.ConnectionId!)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("获取Quartz任务通知接收者列表时发生错误: UserId={UserId}", userId, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 获取项目通知接收者列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetProjectReceivers(long userId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.UserId == userId && u.OnlineStatus == 0);
                exp.And(u => u.LastActivity > DateTime.Now.AddMinutes(-5));

                var users = await Repository.GetListAsync(exp.ToExpression());
                return users.Where(u => !string.IsNullOrEmpty(u.ConnectionId))
                    .Select(u => u.ConnectionId!)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("获取项目通知接收者列表时发生错误: UserId={UserId}", userId, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 获取合同通知接收者列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetContractReceivers(long userId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.UserId == userId && u.OnlineStatus == 0);
                exp.And(u => u.LastActivity > DateTime.Now.AddMinutes(-5));

                var users = await Repository.GetListAsync(exp.ToExpression());
                return users.Where(u => !string.IsNullOrEmpty(u.ConnectionId))
                    .Select(u => u.ConnectionId!)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("获取合同通知接收者列表时发生错误: UserId={UserId}", userId, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 获取设变通知接收者列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetChangeRequestReceivers(long userId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.UserId == userId && u.OnlineStatus == 0);
                exp.And(u => u.LastActivity > DateTime.Now.AddMinutes(-5));

                var users = await Repository.GetListAsync(exp.ToExpression());
                return users.Where(u => !string.IsNullOrEmpty(u.ConnectionId))
                    .Select(u => u.ConnectionId!)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("获取设变通知接收者列表时发生错误: UserId={UserId}", userId, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task DisconnectUserAsync(string connectionId)
        {
            try
            {
                _logger.Info("断开用户连接: ConnectionId={ConnectionId}", connectionId);
                await _hubContext.Clients.Client(connectionId).SendAsync("ForceOffline", "您已被管理员强制下线");
            }
            catch (Exception ex)
            {
                _logger.Error("断开用户连接时发生错误: ConnectionId={ConnectionId}", connectionId, ex.Message, ex.Message, ex.Message);
                throw;
            }
        }

        /// <summary>
        ///  发送消息
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task SendMessageAsync(string connectionId, string method, object[] args)
        {
            try
            {
                await _hubContext.Clients.Client(connectionId).SendAsync(method, args);
            }
            catch (Exception ex)
            {
                _logger.Error("发送消息时发生错误: ConnectionId={ConnectionId}, Method={Method}", connectionId, method, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 添加用户设备
        /// </summary>
        public async Task AddUserDevice(string userId, string deviceId, string connectionId)
        {
            try
            {
                _logger.Info("开始添加/更新用户设备: UserId={UserId}, DeviceId={DeviceId}, ConnectionId={ConnectionId}",
                    userId, deviceId, connectionId);

                await Task.Yield(); // 确保方法异步执行

                if (!_userDevices.ContainsKey(userId))
                {
                    _logger.Info("创建新的用户设备列表 - 用户ID: {UserId}", userId);
                    _userDevices[userId] = new List<HbtSignalRDevice>();
                }

                // 检查是否存在相同设备ID的设备
                var existingDevices = _userDevices[userId].Where(d => d.DeviceId == deviceId).ToList();
                if (existingDevices.Any())
                {
                    _logger.Info("发现相同设备ID的设备，更新设备信息 - 设备ID: {DeviceId}", deviceId);
                    // 更新所有相同设备ID的设备状态
                    foreach (var existingDevice in existingDevices)
                    {
                        existingDevice.ConnectionId = connectionId;
                        existingDevice.LastActivity = DateTime.Now;
                        existingDevice.LastHeartbeat = DateTime.Now;
                        existingDevice.OnlineStatus = 0;
                    }
                }
                else
                {
                    var device = new HbtSignalRDevice
                    {
                        UserId = long.Parse(userId),
                        DeviceId = deviceId,
                        ConnectionId = connectionId,
                        LastActivity = DateTime.Now,
                        LastHeartbeat = DateTime.Now,
                        OnlineStatus = 0
                    };
                    _logger.Info("添加新设备 - 设备ID: {DeviceId}", deviceId);
                    _userDevices[userId].Add(device);
                }

                _logger.Info("当前用户 {UserId} 的设备数量: {DeviceCount}", userId, _userDevices[userId].Count);
            }
            catch (Exception)
            {
                _logger.Error("添加用户设备时发生错误: UserId={UserId}, DeviceId={DeviceId}",
                    userId, deviceId);
                throw;
            }
        }
    }
}