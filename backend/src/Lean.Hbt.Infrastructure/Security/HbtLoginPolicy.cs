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
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.Entities.Identity;

namespace Lean.Hbt.Infrastructure.Security
{
    /// <summary>
    /// 登录限制策略实现
    /// </summary>
    /// <remarks>
    /// 登录策略包含三种主要情况：
    /// 1. 正常登录：用户名密码正确，无需验证码
    /// 2. 重复登录：5分钟内重复登录需要验证码
    /// 3. 错误限制：连续登录失败3次后需要验证码，4次失败将触发锁定
    ///    - admin用户：锁定30分钟
    ///    - 普通用户：永久锁定，需要管理员手动解锁
    /// </remarks>
    public class HbtLoginPolicy : IHbtLoginPolicy
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<HbtLoginPolicy> _logger;
        private readonly IHbtRepository<HbtUser> _userRepository;
        
        // 缓存键前缀
        private const string LOGIN_ATTEMPT_PREFIX = "login_attempt:";      // 登录尝试次数缓存键前缀
        private const string LAST_LOGIN_PREFIX = "last_login:";           // 最后登录时间缓存键前缀
        private const string CAPTCHA_REQUIRED_PREFIX = "captcha_required:"; // 验证码要求标志缓存键前缀
        
        // 登录策略常量
        private const int MAX_FAILED_ATTEMPTS = 5;           // 普通用户最大失败次数（5次后永久锁定）
        private const int ADMIN_MAX_FAILED_ATTEMPTS = 3;     // 管理员最大失败次数（3次后临时锁定）
        private const int ADMIN_LOCKOUT_MINUTES = 30;        // 管理员锁定时间（30分钟）
        private const int CAPTCHA_REQUIRED_ATTEMPTS = 3;     // 需要验证码的失败次数阈值（3次）
        private const int CAPTCHA_REQUIRED_MINUTES = 5;      // 验证码有效期（5分钟）
        private const int REPEAT_LOGIN_MINUTES = 5;          // 重复登录检测时间窗口（5分钟）

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtLoginPolicy(
            IMemoryCache cache, 
            ILogger<HbtLoginPolicy> logger,
            IHbtRepository<HbtUser> userRepository)
        {
            _cache = cache;
            _logger = logger;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 验证登录尝试
        /// </summary>
        /// <remarks>
        /// 登录验证流程：
        /// 1. 首先检查用户是否被永久锁定
        /// 2. 检查是否在5分钟内重复登录（需要验证码）
        /// 3. 检查失败次数，判断是否需要验证码或锁定
        /// 4. 根据用户类型（admin/普通用户）应用不同的锁定策略
        /// </remarks>
        public async Task<(bool allowed, int? remainingSeconds)> ValidateLoginAttemptAsync(string username)
        {
            var attemptKey = $"{LOGIN_ATTEMPT_PREFIX}{username}";
            var lastLoginKey = $"{LAST_LOGIN_PREFIX}{username}";
            bool needCaptcha = false;
            
            // 1. 检查用户是否存在且是否被永久锁定
            var user = await _userRepository.FirstOrDefaultAsync(u => u.UserName == username);
            if (user != null && user.IsLock == 2) // IsLock=2表示永久锁定
            {
                _logger.LogWarning("[登录策略] 用户 {Username} 已被永久锁定，需要管理员解锁", username);
                return (false, null);
            }

            // 2. 检查是否在5分钟内重复登录（第二种情况）
            if (_cache.TryGetValue(lastLoginKey, out DateTime lastLoginTime))
            {
                var timeSinceLastLogin = DateTime.UtcNow - lastLoginTime;
                if (timeSinceLastLogin.TotalMinutes <= REPEAT_LOGIN_MINUTES)
                {
                    _logger.LogInformation("[登录策略] 用户 {Username} 在5分钟内重复登录，需要验证码", username);
                    needCaptcha = true;
                }
            }

            // 3. 获取失败次数，处理第三种情况（错误限制）
            int failedAttempts = 0;
            _cache.TryGetValue(attemptKey, out failedAttempts);

            bool isAdmin = username.ToLower() == "admin";
            int maxAttempts = isAdmin ? ADMIN_MAX_FAILED_ATTEMPTS : MAX_FAILED_ATTEMPTS;

            // 4. 检查是否达到最大失败次数
            if (failedAttempts >= maxAttempts)
            {
                if (isAdmin)
                {
                    // 管理员特殊处理：临时锁定30分钟
                    var lockoutEndTime = _cache.Get<DateTime?>(attemptKey + "_lockout");
                    if (lockoutEndTime.HasValue && DateTime.UtcNow < lockoutEndTime.Value)
                    {
                        var remainingSeconds = (int)(lockoutEndTime.Value - DateTime.UtcNow).TotalSeconds;
                        return (false, remainingSeconds);
                    }
                }
                else
                {
                    // 普通用户处理：达到5次失败后永久锁定
                    if (user != null)
                    {
                        user.IsLock = 2; // 设置永久锁定
                        user.LoginCount = failedAttempts;
                        await _userRepository.UpdateAsync(user);
                        _logger.LogWarning("[登录策略] 用户 {Username} 已被永久锁定", username);
                        return (false, null);
                    }
                }
            }

            // 5. 检查是否需要验证码（3次以上失败）
            if (failedAttempts >= CAPTCHA_REQUIRED_ATTEMPTS)
            {
                needCaptcha = true;
            }
            
            // 如果需要验证码，返回特殊状态
            if (needCaptcha)
            {
                return (false, 0); // 返回0表示需要验证码但不需要等待
            }

            // 第一种情况：正常登录，无需验证码
            return (true, null);
        }

        /// <summary>
        /// 记录登录失败
        /// </summary>
        /// <remarks>
        /// 失败处理逻辑：
        /// 1. admin用户：
        ///    - 3次失败后锁定30分钟
        ///    - 锁定期间禁止登录
        /// 2. 普通用户：
        ///    - 5次失败后永久锁定
        ///    - 需要管理员手动解锁
        /// </remarks>
        public async Task RecordFailedLoginAsync(string username)
        {
            var attemptKey = $"{LOGIN_ATTEMPT_PREFIX}{username}";
            
            // 获取当前失败次数
            int currentAttempts = 0;
            _cache.TryGetValue(attemptKey, out currentAttempts);
            currentAttempts++;

            bool isAdmin = username.ToLower() == "admin";
            int maxAttempts = isAdmin ? ADMIN_MAX_FAILED_ATTEMPTS : MAX_FAILED_ATTEMPTS;

            // 更新失败次数
            if (isAdmin)
            {
                if (currentAttempts >= maxAttempts)
                {
                    // admin用户：锁定30分钟
                    var lockoutEndTime = DateTime.UtcNow.AddMinutes(ADMIN_LOCKOUT_MINUTES);
                    _cache.Set(attemptKey + "_lockout", lockoutEndTime, TimeSpan.FromMinutes(ADMIN_LOCKOUT_MINUTES));
                    _logger.LogWarning("[登录策略] 管理员账号已被临时锁定 {Minutes} 分钟", ADMIN_LOCKOUT_MINUTES);
                }
            }
            else
            {
                // 普通用户：更新数据库中的失败记录
                var user = await _userRepository.FirstOrDefaultAsync(u => u.UserName == username);
                if (user != null)
                {
                    if (currentAttempts >= maxAttempts)
                    {
                        // 达到5次失败，设置永久锁定
                        user.IsLock = 2;
                        user.LoginCount = currentAttempts;
                        await _userRepository.UpdateAsync(user);
                        _logger.LogWarning("[登录策略] 用户 {Username} 已被永久锁定", username);
                    }
                    else
                    {
                        // 更新失败次数
                        user.LoginCount = currentAttempts;
                        await _userRepository.UpdateAsync(user);
                    }
                }
            }

            // 更新缓存中的失败次数（24小时有效期）
            _cache.Set(attemptKey, currentAttempts, TimeSpan.FromHours(24));
            
            _logger.LogWarning("[登录策略] 用户 {Username} 登录失败，当前失败次数: {Count}", username, currentAttempts);
        }

        /// <summary>
        /// 记录登录成功
        /// </summary>
        /// <remarks>
        /// 成功登录后的处理：
        /// 1. 清除失败记录
        /// 2. 记录本次登录时间（用于检测重复登录）
        /// 3. 重置用户的锁定状态和失败计数
        /// </remarks>
        public async Task RecordSuccessfulLoginAsync(string username)
        {
            var attemptKey = $"{LOGIN_ATTEMPT_PREFIX}{username}";
            var lastLoginKey = $"{LAST_LOGIN_PREFIX}{username}";

            // 清除失败记录
            _cache.Remove(attemptKey);
            _cache.Remove(attemptKey + "_lockout");

            // 记录最后登录时间（用于检测5分钟内的重复登录）
            _cache.Set(lastLoginKey, DateTime.UtcNow, TimeSpan.FromMinutes(REPEAT_LOGIN_MINUTES));

            // 更新用户状态
            var user = await _userRepository.FirstOrDefaultAsync(u => u.UserName == username);
            if (user != null)
            {
                user.LoginCount = 0; // 重置登录失败次数
                if (user.IsLock == 1) // 如果是临时锁定，则解除
                {
                    user.IsLock = 0;
                }
                await _userRepository.UpdateAsync(user);
            }

            _logger.LogInformation("[登录策略] 用户 {Username} 登录成功，清除所有限制", username);
        }

        /// <summary>
        /// 验证管理员登录尝试
        /// </summary>
        public Task<(bool allowed, int? remainingSeconds)> ValidateAdminLoginAttemptAsync(string userName)
        {
            return Task.FromResult<(bool allowed, int? remainingSeconds)>((true, null));
        }

        /// <summary>
        /// 记录管理员登录失败
        /// </summary>
        public Task RecordAdminFailedLoginAsync(string userName)
        {
            return RecordFailedLoginAsync(userName);
        }

        /// <summary>
        /// 获取剩余登录尝试次数
        /// </summary>
        public Task<int> GetRemainingAttemptsAsync(string userName)
        {
            var attemptKey = $"{LOGIN_ATTEMPT_PREFIX}{userName}";
            int currentAttempts = 0;
            _cache.TryGetValue(attemptKey, out currentAttempts);

            var maxAttempts = userName.ToLower() == "admin" ? ADMIN_MAX_FAILED_ATTEMPTS : MAX_FAILED_ATTEMPTS;
            return Task.FromResult(Math.Max(0, maxAttempts - currentAttempts));
        }

        /// <summary>
        /// 获取账户锁定剩余时间(秒)
        /// </summary>
        public async Task<int> GetLockoutRemainingSecondsAsync(string userName)
        {
            var attemptKey = $"{LOGIN_ATTEMPT_PREFIX}{userName}";
            
            if (userName.ToLower() == "admin")
            {
                var lockoutEndTime = _cache.Get<DateTime?>(attemptKey + "_lockout");
                if (lockoutEndTime.HasValue)
                {
                    return (int)Math.Max(0, (lockoutEndTime.Value - DateTime.UtcNow).TotalSeconds);
                }
            }
            else
            {
                var user = await _userRepository.FirstOrDefaultAsync(u => u.UserName == userName);
                if (user?.IsLock == 2) // 永久锁定
                {
                    return -1; // 返回-1表示永久锁定
                }
            }
            
            return 0;
        }
    }
}