//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSessionManager.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 10:00
// 版本号 : V.0.0.1
// 描述    : 会话管理实现
//===================================================================

using Lean.Hbt.Common.Options;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Domain.Models.Identity;
using Lean.Hbt.Domain.Repositories;
using Microsoft.Extensions.Options;

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
        private readonly HbtSessionOptions _defaultOptions;
        private readonly IHbtRedisCache _cache;
        private readonly IHbtRepository<HbtSysConfig> _configRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        /// <param name="cache"></param>
        /// <param name="configRepository"></param>
        public HbtSessionManager(
            IOptions<HbtSessionOptions> options, 
            IHbtRedisCache cache,
            IHbtRepository<HbtSysConfig> configRepository)
        {
            _defaultOptions = options.Value;
            _cache = cache;
            _configRepository = configRepository;
        }

        private async Task<HbtSessionOptions> GetOptionsAsync()
        {
            var options = new HbtSessionOptions
            {
                AllowMultipleDevices = _defaultOptions.AllowMultipleDevices,
                MaxConcurrentSessions = _defaultOptions.MaxConcurrentSessions,
                TimeoutMinutes = _defaultOptions.TimeoutMinutes,
                EnableSlidingExpiration = _defaultOptions.EnableSlidingExpiration,
                EnableAbsoluteExpiration = _defaultOptions.EnableAbsoluteExpiration,
                AbsoluteExpirationHours = _defaultOptions.AbsoluteExpirationHours,
                SessionExpiryMinutes = _defaultOptions.SessionExpiryMinutes
            };

            var configs = await _configRepository.GetListAsync();
            foreach (var config in configs)
            {
                switch (config.ConfigKey)
                {
                    case "sso.session.allowMultipleDevices":
                        if (bool.TryParse(config.ConfigValue, out bool allowMultipleDevices))
                            options.AllowMultipleDevices = allowMultipleDevices;
                        break;
                    case "sso.session.maxConcurrentSessions":
                        if (int.TryParse(config.ConfigValue, out int maxConcurrentSessions))
                            options.MaxConcurrentSessions = maxConcurrentSessions;
                        break;
                }
            }

            return options;
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
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            var options = await GetOptionsAsync();

            if (!options.AllowMultipleDevices)
            {
                await RemoveUserSessionsAsync(userId);
            }
            else
            {
                var sessions = await GetUserSessionsAsync(userId);
                if (sessions.Count >= options.MaxConcurrentSessions)
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
                IpAddress = ipAddress ?? "Unknown",
                UserAgent = userAgent ?? "Unknown",
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
            if (string.IsNullOrEmpty(sessionId))
                return null;

            var key = $"session:{sessionId}";
            var sessionInfo = await _cache.GetAsync<HbtSessionInfo>(key);
            return sessionInfo;
        }

        /// <summary>
        /// 更新会话访问时间
        /// </summary>
        /// <param name="sessionId">会话ID</param>
        /// <returns>异步任务</returns>
        public async Task UpdateSessionAccessTimeAsync(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
                return;

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
            if (string.IsNullOrEmpty(sessionId))
                return;

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
            var expiry = _defaultOptions.EnableSlidingExpiration
                ? TimeSpan.FromMinutes(_defaultOptions.SessionExpiryMinutes)
                : TimeSpan.FromMinutes(_defaultOptions.SessionExpiryMinutes * 2);
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

        /// <summary>
        /// 单点登出
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="excludeSessionId">排除的会话ID</param>
        /// <returns>异步任务</returns>
        public async Task SingleSignOutAsync(string userId, string excludeSessionId = null)
        {
            var sessions = await GetUserSessionsAsync(userId);
            foreach (var session in sessions)
            {
                if (session.SessionId != excludeSessionId)
                {
                    await RemoveSessionAsync(session.SessionId);
                }
            }
        }

        /// <summary>
        /// 清理过期会话
        /// </summary>
        private async Task CleanupExpiredSessionsAsync()
        {
            var options = await GetOptionsAsync();
            var allSessions = await GetAllSessionsAsync();
            var now = DateTime.Now;

            foreach (var session in allSessions)
            {
                var isExpired = false;
                
                if (options.EnableSlidingExpiration)
                {
                    var idleTime = now - session.LastAccessTime;
                    if (idleTime.TotalMinutes > options.SessionExpiryMinutes)
                    {
                        isExpired = true;
                    }
                }

                if (options.EnableAbsoluteExpiration)
                {
                    var sessionAge = now - session.LoginTime;
                    if (sessionAge.TotalHours > options.AbsoluteExpirationHours)
                    {
                        isExpired = true;
                    }
                }

                if (isExpired)
                {
                    await RemoveSessionAsync(session.SessionId);
                }
            }
        }

        /// <summary>
        /// 获取所有会话
        /// </summary>
        private async Task<List<HbtSessionInfo>> GetAllSessionsAsync()
        {
            var pattern = "session:*";
            var keys = await _cache.SearchKeysAsync(pattern);
            var sessions = new List<HbtSessionInfo>();

            foreach (var key in keys)
            {
                var session = await _cache.GetAsync<HbtSessionInfo>(key);
                if (session != null)
                {
                    sessions.Add(session);
                }
            }

            return sessions;
        }
    }
}