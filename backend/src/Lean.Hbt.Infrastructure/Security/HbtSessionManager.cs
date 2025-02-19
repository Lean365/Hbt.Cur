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
using Lean.Hbt.Domain.Models.Identity;
using Lean.Hbt.Domain.Repositories;
using Microsoft.Extensions.Options;
using Lean.Hbt.Domain.IServices.Identity;
using Lean.Hbt.Domain.IServices.SignalR;
using Lean.Hbt.Domain.IServices.Caching;

namespace Lean.Hbt.Infrastructure.Security
{
    /// <summary>
    /// 会话管理实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    public class HbtSessionManager : 
        IHbtIdentitySessionManager,
        IHbtSignalRSessionManager
    {
        private readonly HbtSessionOptions _defaultOptions;
        private readonly IHbtRedisCache _cache;
        private readonly IHbtRepository<HbtConfig> _configRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        /// <param name="cache"></param>
        /// <param name="configRepository"></param>
        public HbtSessionManager(
            IOptions<HbtSessionOptions> options, 
            IHbtRedisCache cache,
            IHbtRepository<HbtConfig> configRepository)
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
        /// 创建会话 (私有实现)
        /// </summary>
        private async Task<string> CreateIdentitySessionAsync(string userId, string userName, string ipAddress, string userAgent)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            var options = await GetOptionsAsync();

            if (!options.AllowMultipleDevices)
            {
                await ((IHbtIdentitySessionManager)this).RemoveUserSessionsAsync(userId);
            }
            else
            {
                var sessions = await GetUserIdentitySessionsAsync(userId);
                if (sessions.Count >= options.MaxConcurrentSessions)
                {
                    // 移除最早的会话
                    var oldestSession = sessions.OrderBy(s => s.LastAccessTime).First();
                    await ((IHbtIdentitySessionManager)this).RemoveSessionAsync(oldestSession.SessionId);
                }
            }

            var sessionId = Guid.NewGuid().ToString("N");
            var sessionInfo = new HbtIdentitySessionInfo
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
        /// 创建会话 (私有实现)
        /// </summary>
        private async Task<HbtSignalRSessionInfo> CreateServiceSessionAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));

            var sessionId = Guid.NewGuid().ToString("N");
            var refreshToken = Guid.NewGuid().ToString("N");
            var sessionInfo = new HbtSignalRSessionInfo
            {
                SessionId = sessionId,
                RefreshToken = refreshToken,
                ExpiresIn = _defaultOptions.SessionExpiryMinutes
            };

            await SaveServiceSessionInfoAsync(userId, sessionId, sessionInfo);
            await AddToUserSessionsAsync(userId, sessionId);

            return sessionInfo;
        }

        /// <summary>
        /// 获取会话信息 (私有实现)
        /// </summary>
        private async Task<HbtIdentitySessionInfo> GetIdentitySessionInfoAsync(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
                return null;

            var key = $"session:{sessionId}";
            var sessionInfo = await _cache.GetAsync<HbtIdentitySessionInfo>(key);
            return sessionInfo;
        }

        /// <summary>
        /// 获取用户所有会话 (私有实现)
        /// </summary>
        private async Task<List<HbtIdentitySessionInfo>> GetUserIdentitySessionsAsync(string userId)
        {
            var sessionIds = await GetUserSessionIdsAsync(userId);
            var sessions = new List<HbtIdentitySessionInfo>();

            foreach (var sessionId in sessionIds)
            {
                var sessionInfo = await GetIdentitySessionInfoAsync(sessionId);
                if (sessionInfo != null)
                {
                    sessions.Add(sessionInfo);
                }
            }

            return sessions;
        }

        /// <summary>
        /// 创建会话 (Domain.IServices.Identity.IHbtIdentitySessionManager)
        /// </summary>
        async Task<string> IHbtIdentitySessionManager.CreateSessionAsync(string userId, string userName, string ipAddress, string userAgent)
        {
            return await CreateIdentitySessionAsync(userId, userName, ipAddress, userAgent);
        }

        /// <summary>
        /// 获取会话信息 (Domain.IServices.Identity.IHbtIdentitySessionManager)
        /// </summary>
        async Task<HbtIdentitySessionInfo> IHbtIdentitySessionManager.GetSessionInfoAsync(string sessionId)
        {
            return await GetIdentitySessionInfoAsync(sessionId);
        }

        /// <summary>
        /// 获取用户所有会话 (Domain.IServices.Identity.IHbtIdentitySessionManager)
        /// </summary>
        async Task<List<HbtIdentitySessionInfo>> IHbtIdentitySessionManager.GetUserSessionsAsync(string userId)
        {
            return await GetUserIdentitySessionsAsync(userId);
        }

        /// <summary>
        /// 创建会话 (Domain.IServices.SignalR.IHbtSignalRSessionManager)
        /// </summary>
        async Task<HbtSignalRSessionInfo> IHbtSignalRSessionManager.CreateSessionAsync(string userId)
        {
            return await CreateServiceSessionAsync(userId);
        }

        /// <summary>
        /// 单点登出 (Domain.IServices.SignalR.IHbtSignalRSessionManager)
        /// </summary>
        async Task IHbtSignalRSessionManager.SingleSignOutAsync(string userId, string excludeSessionId)
        {
            var sessions = await GetUserIdentitySessionsAsync(userId);
            foreach (var session in sessions)
            {
                if (session.SessionId != excludeSessionId)
                {
                    await ((IHbtIdentitySessionManager)this).RemoveSessionAsync(session.SessionId);
                }
            }
        }

        /// <summary>
        /// 更新会话访问时间
        /// </summary>
        async Task IHbtIdentitySessionManager.UpdateSessionAccessTimeAsync(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
                return;

            var sessionInfo = await GetIdentitySessionInfoAsync(sessionId);
            if (sessionInfo != null)
            {
                sessionInfo.LastAccessTime = DateTime.Now;
                await SaveSessionInfoAsync(sessionInfo.UserId, sessionId, sessionInfo);
            }
        }

        /// <summary>
        /// 移除会话
        /// </summary>
        async Task IHbtIdentitySessionManager.RemoveSessionAsync(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
                return;

            var sessionInfo = await GetIdentitySessionInfoAsync(sessionId);
            if (sessionInfo != null)
            {
                await _cache.RemoveAsync($"session:{sessionId}");
                await RemoveFromUserSessionsAsync(sessionInfo.UserId, sessionId);
            }
        }

        /// <summary>
        /// 移除用户所有会话
        /// </summary>
        async Task IHbtIdentitySessionManager.RemoveUserSessionsAsync(string userId)
        {
            var sessionIds = await GetUserSessionIdsAsync(userId);
            foreach (var sessionId in sessionIds)
            {
                await ((IHbtIdentitySessionManager)this).RemoveSessionAsync(sessionId);
            }
        }

        /// <summary>
        /// 保存会话信息
        /// </summary>
        private async Task SaveSessionInfoAsync(string userId, string sessionId, HbtIdentitySessionInfo sessionInfo)
        {
            var key = $"session:{sessionId}";
            var expiry = _defaultOptions.EnableSlidingExpiration
                ? TimeSpan.FromMinutes(_defaultOptions.SessionExpiryMinutes)
                : TimeSpan.FromMinutes(_defaultOptions.SessionExpiryMinutes * 2);
            await _cache.SetAsync(key, sessionInfo, expiry);
        }

        /// <summary>
        /// 保存服务会话信息
        /// </summary>
        private async Task SaveServiceSessionInfoAsync(string userId, string sessionId, HbtSignalRSessionInfo sessionInfo)
        {
            var key = $"service:session:{sessionId}";
            var expiry = TimeSpan.FromMinutes(_defaultOptions.SessionExpiryMinutes);
            await _cache.SetAsync(key, sessionInfo, expiry);
        }

        /// <summary>
        /// 添加到用户会话列表
        /// </summary>
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
        private async Task<List<string>> GetUserSessionIdsAsync(string userId)
        {
            var key = $"user:sessions:{userId}";
            return await _cache.GetAsync<List<string>>(key) ?? new List<string>();
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
                    await ((IHbtIdentitySessionManager)this).RemoveSessionAsync(session.SessionId);
                }
            }
        }

        /// <summary>
        /// 获取所有会话
        /// </summary>
        private async Task<List<HbtIdentitySessionInfo>> GetAllSessionsAsync()
        {
            var pattern = "session:*";
            var keys = await _cache.SearchKeysAsync(pattern);
            var sessions = new List<HbtIdentitySessionInfo>();

            foreach (var key in keys)
            {
                var session = await _cache.GetAsync<HbtIdentitySessionInfo>(key);
                if (session != null)
                {
                    sessions.Add(session);
                }
            }

            return sessions;
        }
    }
}