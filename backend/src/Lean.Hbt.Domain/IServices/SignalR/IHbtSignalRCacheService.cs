using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Hbt.Domain.Entities.SignalR;

namespace Lean.Hbt.Domain.IServices.SignalR
{
    /// <summary>
    /// SignalR缓存服务接口
    /// </summary>
    public interface IHbtSignalRCacheService
    {
        /// <summary>
        /// 保存用户信息到缓存
        /// </summary>
        Task SetAsync(string key, HbtOnlineUser user, TimeSpan? expiry = null);

        /// <summary>
        /// 从缓存获取用户信息
        /// </summary>
        Task<HbtOnlineUser?> GetAsync(string key);

        /// <summary>
        /// 从缓存删除用户信息
        /// </summary>
        Task RemoveAsync(string key);

        /// <summary>
        /// 获取所有匹配模式的键
        /// </summary>
        Task<List<string>> GetKeysByPatternAsync(string pattern);

        /// <summary>
        /// 设置用户连接列表
        /// </summary>
        Task SetConnectionsAsync(long userId, List<string> connections);

        /// <summary>
        /// 获取用户连接列表
        /// </summary>
        Task<List<string>> GetConnectionsAsync(long userId);

        /// <summary>
        /// 删除用户连接列表
        /// </summary>
        Task RemoveConnectionsAsync(long userId);

        /// <summary>
        /// 获取在线用户列表
        /// </summary>
        Task<List<HbtOnlineUser>> GetOnlineUsersAsync();

        /// <summary>
        /// 更新用户最后活动时间
        /// </summary>
        Task UpdateLastActivityAsync(string connectionId, DateTime lastActivity);

        /// <summary>
        /// 更新用户心跳时间
        /// </summary>
        Task UpdateHeartbeatAsync(string connectionId, DateTime heartbeat);

        /// <summary>
        /// 设置用户在线状态
        /// </summary>
        Task SetOnlineStatusAsync(string connectionId, int status);
    }
} 