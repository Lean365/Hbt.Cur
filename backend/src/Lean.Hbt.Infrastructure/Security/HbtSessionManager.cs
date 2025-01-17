//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtSessionManager.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-16 10:00
// 版本号 : V0.0.1
// 描述    : 会话管理实现
//===================================================================

using Lean.Hbt.Infrastructure.Caching;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using Lean.Hbt.Common.Options;

namespace Lean.Hbt.Infrastructure.Security
{
    /// <summary>
    /// 会话管理实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    public class HbtSessionManager : IHbtSessionManager
    {
        private readonly SessionOptions _options;
        private readonly IHbtRedisCache _cache;

        public HbtSessionManager(IOptions<HbtSecurityOptions> options, IHbtRedisCache cache)
        {
            _options = options.Value.Session;
            _cache = cache;
        }

        /// <summary>
        /// 创建会话
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="ipAddress">IP地址</param>
        /// <param name="userAgent">用户代理</param>
        /// <returns>会话ID</returns>
        public async Task<string> CreateSessionAsync(string userId, string userName, string ipAddress, string userAgent)
        {
            if (!_options.AllowMultipleDevices)
            {
                await RemoveUserSessionsAsync(userId);
            }
            else
            {
                var sessions = await GetUserSessionsAsync(userId);
                if (sessions.Count >= _options.MaxConcurrentSessions)
                {
                    // 移除最早的会话
                    var oldestSession = sessions.OrderBy(s => s.LastAccessTime).First();
                    await RemoveSessionAsync(oldestSession.SessionId);
                }
            }

            var sessionId = Guid.NewGuid().ToString("N");
            var sessionInfo = new HbtSessionInfo
            {
                SessionId = sessionId,
                UserId = userId,
                UserName = userName,
                IpAddress = ipAddress,
                UserAgent = userAgent,
                LastAccessTime = DateTime.Now,
                LoginTime = DateTime.Now
            };

            await SaveSessionInfoAsync(userId, sessionId, sessionInfo);
            await AddToUserSessionsAsync(userId, sessionId);

            return sessionId;
        }

        /// <summary>
        /// 获取会话信息
        /// </summary>
        /// <param name="sessionId">会话ID</param>
        /// <returns>会话信息</returns>
        public async Task<HbtSessionInfo> GetSessionInfoAsync(string sessionId)
        {
            var key = $"session:{sessionId}";
            return await _cache.GetAsync<HbtSessionInfo>(key);
        }

        /// <summary>
        /// 更新会话访问时间
        /// </summary>
        /// <param name="sessionId">会话ID</param>
        /// <returns>异步任务</returns>
        public async Task UpdateSessionAccessTimeAsync(string sessionId)
        {
            var sessionInfo = await GetSessionInfoAsync(sessionId);
            if (sessionInfo != null)
            {
                sessionInfo.LastAccessTime = DateTime.Now;
                await SaveSessionInfoAsync(sessionInfo.UserId, sessionId, sessionInfo);
            }
        }

        /// <summary>
        /// 移除会话
        /// </summary>
        /// <param name="sessionId">会话ID</param>
        /// <returns>异步任务</returns>
        public async Task RemoveSessionAsync(string sessionId)
        {
            var sessionInfo = await GetSessionInfoAsync(sessionId);
            if (sessionInfo != null)
            {
                await _cache.RemoveAsync($"session:{sessionId}");
                await RemoveFromUserSessionsAsync(sessionInfo.UserId, sessionId);
            }
        }

        /// <summary>
        /// 获取用户所有会话
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>会话列表</returns>
        public async Task<List<HbtSessionInfo>> GetUserSessionsAsync(string userId)
        {
            var sessionIds = await GetUserSessionIdsAsync(userId);
            var sessions = new List<HbtSessionInfo>();

            foreach (var sessionId in sessionIds)
            {
                var sessionInfo = await GetSessionInfoAsync(sessionId);
                if (sessionInfo != null)
                {
                    sessions.Add(sessionInfo);
                }
            }

            return sessions;
        }

        /// <summary>
        /// 移除用户所有会话
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>异步任务</returns>
        public async Task RemoveUserSessionsAsync(string userId)
        {
            var sessionIds = await GetUserSessionIdsAsync(userId);
            foreach (var sessionId in sessionIds)
            {
                await RemoveSessionAsync(sessionId);
            }
        }

        /// <summary>
        /// 保存会话信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="sessionId">会话ID</param>
        /// <param name="sessionInfo">会话信息</param>
        /// <returns>异步任务</returns>
        private async Task SaveSessionInfoAsync(string userId, string sessionId, HbtSessionInfo sessionInfo)
        {
            var key = $"session:{sessionId}";
            var expiry = _options.EnableSlidingExpiration 
                ? TimeSpan.FromMinutes(_options.SessionExpiryMinutes)
                : TimeSpan.FromMinutes(_options.SessionExpiryMinutes * 2);
            await _cache.SetAsync(key, sessionInfo, expiry);
        }

        /// <summary>
        /// 添加到用户会话列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="sessionId">会话ID</param>
        /// <returns>异步任务</returns>
        private async Task AddToUserSessionsAsync(string userId, string sessionId)
        {
            var key = $"user:sessions:{userId}";
            var sessions = await GetUserSessionIdsAsync(userId);
            sessions.Add(sessionId);
            await _cache.SetAsync(key, sessions);
        }

        /// <summary>
        /// 从用户会话列表移除
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="sessionId">会话ID</param>
        /// <returns>异步任务</returns>
        private async Task RemoveFromUserSessionsAsync(string userId, string sessionId)
        {
            var key = $"user:sessions:{userId}";
            var sessions = await GetUserSessionIdsAsync(userId);
            sessions.Remove(sessionId);
            await _cache.SetAsync(key, sessions);
        }

        /// <summary>
        /// 获取用户会话ID列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>会话ID列表</returns>
        private async Task<List<string>> GetUserSessionIdsAsync(string userId)
        {
            var key = $"user:sessions:{userId}";
            return await _cache.GetAsync<List<string>>(key) ?? new List<string>();
        }
    }

    /// <summary>
    /// 会话信息
    /// </summary>
    public class HbtSessionInfo
    {
        /// <summary>
        /// 会话ID
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 最后访问时间
        /// </summary>
        public DateTime LastAccessTime { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }
    }
} 