//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSignalRUserService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-24 10:00
// 版本号 : V1.0.0
// 描述    : SignalR用户服务实现
//===================================================================

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.RealTime;
using Lean.Hbt.Domain.IServices.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Lean.Hbt.Infrastructure.SignalR
{
    /// <summary>
    /// SignalR用户服务
    /// </summary>
    public class HbtSignalRUserService : IHbtSignalRUserService
    {
        private readonly IHubContext<HbtSignalRHub> _hubContext;
        private readonly ILogger<HbtSignalRUserService> _logger;
        private readonly IHbtRepository<HbtOnlineUser> _repository;
        private static readonly ConcurrentDictionary<string, List<HbtSignalRDevice>> _userDevices = new();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hubContext"></param>
        /// <param name="logger"></param>
        /// <param name="repository"></param>
        public HbtSignalRUserService(
            IHubContext<HbtSignalRHub> hubContext,
            ILogger<HbtSignalRUserService> logger,
            IHbtRepository<HbtOnlineUser> repository)
        {
            _hubContext = hubContext;
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// 添加用户设备
        /// </summary>
        public async Task AddUserDevice(HbtSignalRDevice device)
        {
            _logger.LogInformation("开始添加用户设备 - 用户ID: {UserId}, 设备ID: {DeviceId}, 连接ID: {ConnectionId}", 
                device.UserId, device.DeviceId, device.ConnectionId);

            var userId = device.UserId.ToString();
            if (!_userDevices.ContainsKey(userId))
            {
                _logger.LogInformation("创建新的用户设备列表 - 用户ID: {UserId}", userId);
                _userDevices[userId] = new List<HbtSignalRDevice>();
            }

            var existingDevice = _userDevices[userId]
                .FirstOrDefault(d => d.DeviceId == device.DeviceId);

            if (existingDevice != null)
            {
                _logger.LogInformation("更新现有设备信息 - 设备ID: {DeviceId}", device.DeviceId);
                existingDevice.ConnectionId = device.ConnectionId;
                existingDevice.LastActivity = device.LastActivity;
                existingDevice.LastHeartbeat = device.LastHeartbeat;
                existingDevice.OnlineStatus = device.OnlineStatus;
            }
            else
            {
                _logger.LogInformation("添加新设备 - 设备ID: {DeviceId}", device.DeviceId);
                _userDevices[userId].Add(device);
            }

            _logger.LogInformation("当前用户 {UserId} 的设备数量: {DeviceCount}", 
                userId, _userDevices[userId].Count);

            // 验证设备是否已添加
            var devices = GetUserDevices(userId);
            _logger.LogInformation("验证设备信息 - 用户ID: {UserId}, 设备数量: {DeviceCount}", 
                userId, devices.Count);

            await Task.CompletedTask;
        }

        /// <summary>
        /// 获取用户设备列表
        /// </summary>
        public List<HbtSignalRDevice> GetUserDevices(string userId)
        {
            _logger.LogInformation("开始获取用户 {UserId} 的设备列表", userId);
            
            // 先从内存中获取
            if (_userDevices.TryGetValue(userId, out var devices))
            {
                // 过滤掉已断开的设备
                devices = devices.Where(d => d.OnlineStatus == 0).ToList();
                if (devices.Any())
                {
                    _logger.LogInformation("从内存中找到用户 {UserId} 的在线设备列表，数量: {DeviceCount}", userId, devices.Count);
                    return devices;
                }
                // 如果内存中没有在线设备，从字典中移除
                _userDevices.TryRemove(userId, out _);
            }
            
            // 如果内存中没有，从数据库查询
            var exp = Expressionable.Create<HbtOnlineUser>();
            exp.And(u => u.UserId == long.Parse(userId) && u.OnlineStatus == 0);
            var onlineUsers = _repository.GetListAsync(exp.ToExpression()).Result;
            
            if (onlineUsers != null && onlineUsers.Any())
            {
                _logger.LogInformation("从数据库中找到用户 {UserId} 的在线设备列表，数量: {DeviceCount}", userId, onlineUsers.Count);
                
                // 将数据库中的设备信息添加到内存中
                devices = onlineUsers.Select(u => new HbtSignalRDevice
                {
                    UserId = u.UserId,
                    DeviceId = u.DeviceId,
                    ConnectionId = u.ConnectionId,
                    LastActivity = u.LastActivity,
                    LastHeartbeat = u.LastHeartbeat,
                    OnlineStatus = u.OnlineStatus,
                    TenantId = u.TenantId,
                    GroupId = u.GroupId,
                    IpAddress = u.ClientIp,
                    UserAgent = u.UserAgent
                }).ToList();
                
                _userDevices[userId] = devices;
                return devices;
            }
            
            _logger.LogWarning("未找到用户 {UserId} 的在线设备列表", userId);
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
            return GetUserDevices(userId).FirstOrDefault(d => d.ConnectionId == connectionId);
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
                    device.LastActivity = DateTime.UtcNow;
                    _logger.LogInformation("更新用户 {UserId} 的设备 {DeviceId} 活动时间", userId, deviceId);
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

                var existingUser = await _repository.GetInfoAsync(exp.ToExpression());
                if (existingUser != null)
                {
                    // 更新现有记录
                    existingUser.ConnectionId = user.ConnectionId;
                    existingUser.LastActivity = user.LastActivity;
                    existingUser.LastHeartbeat = user.LastHeartbeat;
                    existingUser.OnlineStatus = user.OnlineStatus;
                    existingUser.ClientIp = user.ClientIp;
                    existingUser.UserAgent = user.UserAgent;
                    await _repository.UpdateAsync(existingUser);
                }
                else
                {
                    await _repository.CreateAsync(user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存在线用户时发生错误: ConnectionId={ConnectionId}, UserId={UserId}", user.ConnectionId, user.UserId);
                throw;
            }
        }

        /// <summary>
        /// 获取在线用户信息
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task<HbtOnlineUser?> GetOnlineUserAsync(string connectionId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.ConnectionId == connectionId);
                return await _repository.GetInfoAsync(exp.ToExpression());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取在线用户信息时发生错误: ConnectionId={ConnectionId}", connectionId);
                throw;
            }
        }

        /// <summary>
        /// 根据设备ID获取在线用户
        /// </summary>
        public async Task<HbtOnlineUser?> GetOnlineUserByDeviceAsync(long userId, string deviceId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.UserId == userId && u.DeviceId == deviceId);

                return await _repository.GetInfoAsync(exp.ToExpression());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取在线用户信息时发生错误: UserId={UserId}, DeviceId={DeviceId}", userId, deviceId);
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

                var user = await _repository.GetInfoAsync(exp.ToExpression());
                return user?.DeviceId ?? string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取设备信息时发生错误: ConnectionId={ConnectionId}", connectionId);
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
                var result = await _repository.DeleteAsync(exp.ToExpression());
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除在线用户时发生错误: ConnectionId={ConnectionId}", connectionId);
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

                var users = await _repository.GetListAsync(exp.ToExpression());
                return users.Select(u => u.ConnectionId).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取用户连接ID列表时发生错误: UserId={UserId}", userId);
                throw;
            }
        }

        /// <summary>
        /// 获取租户组在线用户列表
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetGroupConnectionIdsAsync(long tenantId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.TenantId == tenantId && u.OnlineStatus == 0);

                var users = await _repository.GetListAsync(exp.ToExpression());
                return users.Select(u => u.ConnectionId).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取租户组连接ID列表时发生错误: TenantId={TenantId}", tenantId);
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

                var user = await _repository.GetInfoAsync(exp.ToExpression());
                if (user != null)
                {
                    user.LastActivity = DateTime.Now;
                    await _repository.UpdateAsync(user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新用户最后活动时间时发生错误: ConnectionId={ConnectionId}", connectionId);
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

                var users = await _repository.GetListAsync(exp.ToExpression());
                return users.Select(u => u.ConnectionId).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取邮件接收者列表时发生错误: UserId={UserId}", userId);
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

                var users = await _repository.GetListAsync(exp.ToExpression());
                return users.Select(u => u.ConnectionId).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取通知接收者列表时发生错误: UserId={UserId}", userId);
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
                _logger.LogInformation("断开用户连接: ConnectionId={ConnectionId}", connectionId);
                await _hubContext.Clients.Client(connectionId).SendAsync("ForceOffline", "您已被管理员强制下线");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "断开用户连接时发生错误: ConnectionId={ConnectionId}", connectionId);
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
                _logger.LogError(ex, "发送消息时发生错误: ConnectionId={ConnectionId}, Method={Method}", connectionId, method);
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
                var device = new HbtSignalRDevice
                {
                    UserId = long.Parse(userId),
                    DeviceId = deviceId,
                    ConnectionId = connectionId,
                    LastActivity = DateTime.UtcNow,
                    LastHeartbeat = DateTime.UtcNow,
                    OnlineStatus = 0
                };

                if (!_userDevices.ContainsKey(userId))
                {
                    _userDevices[userId] = new List<HbtSignalRDevice>();
                }

                _userDevices[userId].Add(device);
                _logger.LogInformation("添加用户设备: UserId={UserId}, DeviceId={DeviceId}, ConnectionId={ConnectionId}", userId, deviceId, connectionId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "添加用户设备时发生错误: UserId={UserId}, DeviceId={DeviceId}", userId, deviceId);
                throw;
            }
        }
    }
}