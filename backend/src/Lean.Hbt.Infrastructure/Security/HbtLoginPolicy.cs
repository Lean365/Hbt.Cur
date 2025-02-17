//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginPolicy.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 10:00
// 版本号 : V.0.0.1
// 描述    : 登录限制策略实现
//===================================================================

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Lean.Hbt.Domain.IServices.Security;

namespace Lean.Hbt.Infrastructure.Security
{
    /// <summary>
    /// 登录限制策略实现
    /// </summary>
    public class HbtLoginPolicy : IHbtLoginPolicy
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<HbtLoginPolicy> _logger;
        private const string LOGIN_ATTEMPT_PREFIX = "login_attempt:";
        private const string LAST_LOGIN_PREFIX = "last_login:";
        private const string CAPTCHA_REQUIRED_PREFIX = "captcha_required:";
        private const int MAX_FAILED_ATTEMPTS = 5;  // 普通用户5次
        private const int ADMIN_MAX_FAILED_ATTEMPTS = 3; // 管理员3次
        private const int ADMIN_LOCKOUT_MINUTES = 30;  // 管理员锁定30分钟
        private const int USER_LOCKOUT_MINUTES = 10;   // 普通用户锁定10分钟
        private const int CAPTCHA_REQUIRED_ATTEMPTS = 1; // 连续1次失败后需要验证码
        private const int CAPTCHA_REQUIRED_MINUTES = 5;  // 5分钟内需要验证码

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cache">缓存服务</param>
        /// <param name="logger">日志服务</param>
        public HbtLoginPolicy(IMemoryCache cache, ILogger<HbtLoginPolicy> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        /// <summary>
        /// 清除指定用户的登录限制缓存
        /// </summary>
        /// <param name="username">用户名</param>
        private void ClearUserLoginRestrictions(string username)
        {
            _logger.LogInformation("[登录策略] 正在清除用户 {Username} 的登录限制", username);
            var attemptKey = $"{LOGIN_ATTEMPT_PREFIX}{username}";
            var captchaKey = $"{CAPTCHA_REQUIRED_PREFIX}{username}";
            
            _cache.Remove(attemptKey);
            _cache.Remove(captchaKey);
            
            _logger.LogInformation("[登录策略] 用户 {Username} 的登录限制已清除", username);
        }

        /// <summary>
        /// 验证登录尝试
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Task<(bool allowed, int? remainingSeconds)> ValidateLoginAttemptAsync(string username)
        {
            var attemptKey = $"{LOGIN_ATTEMPT_PREFIX}{username}";
            var captchaKey = $"{CAPTCHA_REQUIRED_PREFIX}{username}";

            _logger.LogInformation("[登录策略] 开始验证用户 {Username} 的登录尝试", username);

            // 获取失败次数
            int failedAttempts = 0;
            bool hasFailedAttempts = _cache.TryGetValue(attemptKey, out failedAttempts);
            
            if (hasFailedAttempts)
            {
                _logger.LogInformation("[登录策略] 用户 {Username} 在缓存中存在失败记录，当前失败次数: {FailedAttempts}", username, failedAttempts);
                
                var maxAttempts = username.ToLower() == "admin" ? ADMIN_MAX_FAILED_ATTEMPTS : MAX_FAILED_ATTEMPTS;

                // 只有在有失败记录时才判断是否锁定
                if (failedAttempts >= maxAttempts)
                {
                    var cacheEntry = _cache.Get<DateTimeOffset?>(attemptKey + "_expiry");
                    if (cacheEntry.HasValue)
                    {
                        var remainingTime = cacheEntry.Value - DateTimeOffset.UtcNow;
                        var remainingSeconds = (int)Math.Ceiling(remainingTime.TotalSeconds);
                        
                        if (remainingSeconds > 0)
                        {
                            _logger.LogWarning("[登录策略] 用户 {Username} 已被锁定，剩余时间 {Seconds} 秒", username, remainingSeconds);
                            return Task.FromResult<(bool allowed, int? remainingSeconds)>((false, remainingSeconds));
                        }
                        else
                        {
                            // 如果过期时间已过，清除缓存
                            _cache.Remove(attemptKey);
                            _cache.Remove(attemptKey + "_expiry");
                            _logger.LogInformation("[登录策略] 用户 {Username} 锁定时间已过，清除缓存", username);
                            return Task.FromResult<(bool allowed, int? remainingSeconds)>((true, null));
                        }
                    }
                }
            }
            else
            {
                _logger.LogInformation("[登录策略] 用户 {Username} 在缓存中无失败记录", username);
            }

            // 检查是否需要验证码
            bool requireCaptcha = false;
            if (_cache.TryGetValue(captchaKey, out requireCaptcha))
            {
                _logger.LogInformation("[登录策略] 用户 {Username} 验证码状态: {RequireCaptcha}", username, requireCaptcha);
            }

            if (hasFailedAttempts && failedAttempts >= CAPTCHA_REQUIRED_ATTEMPTS)
            {
                requireCaptcha = true;
                _logger.LogInformation("[登录策略] 用户 {Username} 需要验证码验证，失败次数: {FailedAttempts}", username, failedAttempts);
            }

            // 如果需要验证码，返回特殊状态
            if (requireCaptcha)
            {
                _logger.LogInformation("[登录策略] 用户 {Username} 需要验证码验证", username);
                return Task.FromResult<(bool allowed, int? remainingSeconds)>((false, 0)); // 返回0表示需要验证码但不需要等待
            }

            _logger.LogInformation("[登录策略] 用户 {Username} 验证通过", username);
            return Task.FromResult<(bool allowed, int? remainingSeconds)>((true, null));
        }

        /// <summary>
        /// 验证管理员登录尝试
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>是否允许登录</returns>
        public Task<(bool allowed, int? remainingSeconds)> ValidateAdminLoginAttemptAsync(string userName)
        {
            var attemptKey = $"{LOGIN_ATTEMPT_PREFIX}{userName}";
            int failedAttempts = 0;
            
            // 使用TryGetValue替代GetOrCreate
            if (_cache.TryGetValue(attemptKey, out int attempts))
            {
                failedAttempts = attempts;
                _logger.LogInformation("[登录策略] 管理员用户 {Username} 当前失败次数: {FailedAttempts}", userName, failedAttempts);
            }
            else
            {
                _logger.LogInformation("[登录策略] 管理员用户 {Username} 无失败记录", userName);
                return Task.FromResult<(bool allowed, int? remainingSeconds)>((true, null));
            }

            // 如果已经达到最大失败次数
            if (failedAttempts >= ADMIN_MAX_FAILED_ATTEMPTS)
            {
                // 获取缓存项的过期时间
                var cacheEntry = _cache.Get<ICacheEntry>(attemptKey);
                var remainingTime = cacheEntry?.AbsoluteExpiration - DateTimeOffset.UtcNow;
                var remainingSeconds = remainingTime.HasValue ? (int)Math.Ceiling(remainingTime.Value.TotalSeconds) : 0;
                
                _logger.LogWarning("[登录策略] 管理员用户 {Username} 已被锁定，剩余时间 {Seconds} 秒", userName, remainingSeconds);
                return Task.FromResult<(bool allowed, int? remainingSeconds)>((false, remainingSeconds));
            }

            // 如果失败次数大于等于需要验证码的次数，但未达到最大失败次数
            if (failedAttempts >= CAPTCHA_REQUIRED_ATTEMPTS && failedAttempts < ADMIN_MAX_FAILED_ATTEMPTS)
            {
                _logger.LogInformation("[登录策略] 管理员用户 {Username} 需要验证码验证", userName);
                return Task.FromResult<(bool allowed, int? remainingSeconds)>((false, 0)); // 返回0表示需要验证码但不需要等待
            }

            _logger.LogInformation("[登录策略] 管理员用户 {Username} 验证通过", userName);
            return Task.FromResult<(bool allowed, int? remainingSeconds)>((true, null));
        }

        /// <summary>
        /// 记录登录失败
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Task RecordFailedLoginAsync(string username)
        {
            var attemptKey = $"{LOGIN_ATTEMPT_PREFIX}{username}";
            var captchaKey = $"{CAPTCHA_REQUIRED_PREFIX}{username}";
            
            // 获取当前失败次数
            int currentAttempts = 0;
            if (_cache.TryGetValue(attemptKey, out currentAttempts))
            {
                _logger.LogInformation("[登录策略] 用户 {Username} 当前已有失败次数: {Count}", username, currentAttempts);
            }

            // 更新失败次数
            currentAttempts++;
            
            var isAdmin = username.ToLower() == "admin";
            var maxAttempts = isAdmin ? ADMIN_MAX_FAILED_ATTEMPTS : MAX_FAILED_ATTEMPTS;
            
            // 设置缓存过期时间
            TimeSpan expirationTime;
            if (currentAttempts >= maxAttempts)
            {
                // 达到最大失败次数，设置锁定时间
                expirationTime = TimeSpan.FromMinutes(isAdmin ? ADMIN_LOCKOUT_MINUTES : USER_LOCKOUT_MINUTES);
                _logger.LogWarning("[登录策略] 用户 {Username} 达到最大失败次数，设置锁定时间 {Minutes} 分钟", 
                    username, isAdmin ? ADMIN_LOCKOUT_MINUTES : USER_LOCKOUT_MINUTES);
            }
            else
            {
                // 未达到最大失败次数，设置较短的过期时间
                expirationTime = TimeSpan.FromMinutes(1);
                _logger.LogInformation("[登录策略] 用户 {Username} 未达到最大失败次数，设置临时记录时间 1 分钟", username);
            }
            
            // 设置失败次数和过期时间
            _cache.Set(attemptKey, currentAttempts, expirationTime);
            _cache.Set(attemptKey + "_expiry", DateTimeOffset.UtcNow.Add(expirationTime), expirationTime);
            
            // 设置需要验证码标记
            if (currentAttempts >= CAPTCHA_REQUIRED_ATTEMPTS)
            {
                _cache.Set(captchaKey, true, TimeSpan.FromMinutes(CAPTCHA_REQUIRED_MINUTES));
                _logger.LogInformation("[登录策略] 用户 {Username} 需要验证码，设置验证码标记 {Minutes} 分钟", 
                    username, CAPTCHA_REQUIRED_MINUTES);
            }

            _logger.LogWarning("[登录策略] 用户 {Username} 登录失败，当前失败次数: {Count}, 过期时间: {ExpirationTime}", 
                username, currentAttempts, expirationTime);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 记录管理员登录失败
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>任务</returns>
        public Task RecordAdminFailedLoginAsync(string userName)
        {
            return RecordFailedLoginAsync(userName);
        }

        /// <summary>
        /// 记录登录成功
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>任务</returns>
        public Task RecordSuccessfulLoginAsync(string username)
        {
            // 登录成功时，清除该用户的所有限制
            ClearUserLoginRestrictions(username);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取剩余登录尝试次数
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>剩余次数</returns>
        public Task<int> GetRemainingAttemptsAsync(string userName)
        {
            var attemptKey = $"{LOGIN_ATTEMPT_PREFIX}{userName}";
            int currentAttempts = 0;
            
            // 使用TryGetValue替代GetOrCreate
            if (_cache.TryGetValue(attemptKey, out int attempts))
            {
                currentAttempts = attempts;
                _logger.LogInformation("[登录策略] 用户 {Username} 当前失败次数: {FailedAttempts}", userName, currentAttempts);
            }
            else
            {
                _logger.LogInformation("[登录策略] 用户 {Username} 无失败记录", userName);
            }

            var maxAttempts = userName.ToLower() == "admin" ? ADMIN_MAX_FAILED_ATTEMPTS : MAX_FAILED_ATTEMPTS;
            return Task.FromResult(Math.Max(0, maxAttempts - currentAttempts));
        }

        /// <summary>
        /// 获取账户锁定剩余时间(秒)
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>剩余秒数</returns>
        public Task<int> GetLockoutRemainingSecondsAsync(string userName)
        {
            var attemptKey = $"{LOGIN_ATTEMPT_PREFIX}{userName}";
            if (_cache.TryGetValue(attemptKey, out int failedAttempts))
            {
                var maxAttempts = userName.ToLower() == "admin" ? ADMIN_MAX_FAILED_ATTEMPTS : MAX_FAILED_ATTEMPTS;
                if (failedAttempts >= maxAttempts)
                {
                    // 获取缓存项的过期时间
                    var cacheEntry = _cache.Get<ICacheEntry>(attemptKey);
                    var remainingTime = cacheEntry?.AbsoluteExpiration - DateTimeOffset.UtcNow;
                    return Task.FromResult(remainingTime.HasValue ? (int)Math.Ceiling(remainingTime.Value.TotalSeconds) : 0);
                }
            }
            return Task.FromResult(0);
        }
    }
}