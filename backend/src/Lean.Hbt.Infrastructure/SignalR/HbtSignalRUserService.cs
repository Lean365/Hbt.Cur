//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtSignalRUserService.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-24 10:00
// 版本号 : V1.0.0
// 描述    : SignalR用户服务实现
//===================================================================

using Lean.Hbt.Domain.IServices.SignalR;
using Lean.Hbt.Domain.Models.SignalR;
using Lean.Hbt.Domain.IServices.Caching;

namespace Lean.Hbt.Infrastructure.SignalR
{
    /// <summary>
    /// SignalR用户服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-24
    /// </remarks>
    public class HbtSignalRUserService : IHbtSignalRUserService
    {
        private readonly IHbtRedisCache _cache;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSignalRUserService(IHbtRedisCache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// 保存在线用户
        /// </summary>
        public async Task SaveOnlineUserAsync(HbtOnlineUser user)
        {
            var key = $"signalr:user:{user.ConnectionId}";
            await _cache.SetAsync(key, user, TimeSpan.FromMinutes(30));

            var userConnectionsKey = $"signalr:user:connections:{user.UserId}";
            var connections = await _cache.GetAsync<List<string>>(userConnectionsKey) ?? new List<string>();
            if (!connections.Contains(user.ConnectionId))
            {
                connections.Add(user.ConnectionId);
                await _cache.SetAsync(userConnectionsKey, connections);
            }
        }

        /// <summary>
        /// 删除在线用户
        /// </summary>
        public async Task DeleteOnlineUserAsync(string connectionId)
        {
            var key = $"signalr:user:{connectionId}";
            var user = await _cache.GetAsync<HbtOnlineUser>(key);
            if (user != null)
            {
                await _cache.RemoveAsync(key);

                var userConnectionsKey = $"signalr:user:connections:{user.UserId}";
                var connections = await _cache.GetAsync<List<string>>(userConnectionsKey) ?? new List<string>();
                connections.Remove(connectionId);
                if (connections.Any())
                    await _cache.SetAsync(userConnectionsKey, connections);
                else
                    await _cache.RemoveAsync(userConnectionsKey);
            }
        }

        /// <summary>
        /// 获取用户的连接ID列表
        /// </summary>
        public async Task<List<string>> GetConnectionIdsAsync(long userId)
        {
            var key = $"signalr:user:connections:{userId}";
            return await _cache.GetAsync<List<string>>(key) ?? new List<string>();
        }

        /// <summary>
        /// 获取租户组的连接ID列表
        /// </summary>
        public async Task<List<string>> GetGroupConnectionIdsAsync(long tenantId)
        {
            var pattern = $"signalr:user:*";
            var keys = await _cache.SearchKeysAsync(pattern);
            var connections = new List<string>();

            foreach (var key in keys)
            {
                var user = await _cache.GetAsync<HbtOnlineUser>(key);
                if (user != null)
                {
                    connections.Add(user.ConnectionId);
                }
            }

            return connections;
        }
    }
} 