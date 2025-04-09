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
        private readonly ConcurrentDictionary<string, List<HbtSignalRDevice>> _userDevices = new();

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
            var userIdStr = device.UserId.ToString();
            if (!_userDevices.ContainsKey(userIdStr))
            {
                _userDevices[userIdStr] = new List<HbtSignalRDevice>();
            }
            _userDevices[userIdStr].Add(device);
            await Task.CompletedTask;
        }

        /// <summary>
        /// 获取用户设备列表
        /// </summary>
        public List<HbtSignalRDevice> GetUserDevices(string userId)
        {
            if (_userDevices.TryGetValue(userId, out var devices))
            {
                return devices;
            }
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
                exp.And(u => u.ConnectionId == user.ConnectionId);

                var existingUser = await _repository.GetInfoAsync(exp.ToExpression());
                if (existingUser != null)
                {
                    await _repository.UpdateAsync(user);
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
        /// 删除在线用户
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="deleteBy"></param>
        /// <returns></returns>
        public async Task<bool> DeleteOnlineUserAsync(string connectionId, string deleteBy)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.ConnectionId == connectionId);

                var user = await _repository.GetInfoAsync(exp.ToExpression());
                if (user != null)
                {
                    user.DeleteBy = deleteBy;
                    user.DeleteTime = DateTime.Now;
                    user.OnlineStatus = 1; // 设置为离线状态
                    await _repository.UpdateAsync(user);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除在线用户时发生错误: ConnectionId={ConnectionId}", connectionId);
                throw;
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
                exp.And(u => u.UserId == userId && u.OnlineStatus == 0);
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
        /// <param name="mailId"></param>
        /// <returns></returns>
        public async Task<List<long>> GetMailReceivers(long mailId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.OnlineStatus == 0);

                var users = await _repository.GetListAsync(exp.ToExpression());
                return users.Select(u => u.UserId).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取邮件接收者列表时发生错误: MailId={MailId}", mailId);
                throw;
            }
        }

        /// <summary>
        /// 获取通知接收者列表
        /// </summary>
        /// <param name="noticeId"></param>
        /// <returns></returns>
        public async Task<List<long>> GetNoticeReceivers(long noticeId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.OnlineStatus == 0);

                var users = await _repository.GetListAsync(exp.ToExpression());
                return users.Select(u => u.UserId).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取通知接收者列表时发生错误: NoticeId={NoticeId}", noticeId);
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
        /// 获取在线用户信息
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task<HbtOnlineUser?> GetOnlineUserAsync(string connectionId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.ConnectionId == connectionId && u.OnlineStatus == 0);
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
                exp.And(u => u.UserId == userId && u.DeviceId == deviceId && u.OnlineStatus == 0);
                exp.And(u => u.LastActivity > DateTime.Now.AddMinutes(-5)); // 只返回5分钟内有活动的连接

                return await _repository.GetInfoAsync(exp.ToExpression());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "根据设备ID获取在线用户时发生错误: UserId={UserId}, DeviceId={DeviceId}", userId, deviceId);
                throw;
            }
        }
    }
}