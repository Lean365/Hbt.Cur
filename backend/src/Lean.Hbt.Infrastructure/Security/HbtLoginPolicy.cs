//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginPolicy.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 10:00
// 版本号 : V.0.0.1
// 描述    : 登录限制策略实现
//===================================================================

using Lean.Hbt.Common.Options;
using Lean.Hbt.Domain.IServices;
using Microsoft.Extensions.Options;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.Entities.Admin;

namespace Lean.Hbt.Infrastructure.Security
{
    /// <summary>
    /// 登录限制策略实现
    /// </summary>
    public class HbtLoginPolicy : IHbtLoginPolicy
    {
        private readonly HbtLoginPolicyOptions _defaultOptions;
        private readonly IHbtRedisCache _cache;
        private readonly IHbtRepository<HbtSysConfig> _configRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        /// <param name="cache"></param>
        /// <param name="configRepository"></param>
        public HbtLoginPolicy(
            IOptions<HbtLoginPolicyOptions> options, 
            IHbtRedisCache cache,
            IHbtRepository<HbtSysConfig> configRepository)
        {
            _defaultOptions = options.Value;
            _cache = cache;
            _configRepository = configRepository;
        }

        private async Task<HbtLoginPolicyOptions> GetOptionsAsync()
        {
            var options = new HbtLoginPolicyOptions
            {
                MaxFailedAttempts = _defaultOptions.MaxFailedAttempts,
                LockoutMinutes = _defaultOptions.LockoutMinutes,
                AllowMultipleLogin = _defaultOptions.AllowMultipleLogin,
                EnableLoginRestriction = _defaultOptions.EnableLoginRestriction
            };

            var configs = await _configRepository.GetListAsync();
            foreach (var config in configs)
            {
                switch (config.ConfigKey)
                {
                    case "sso.login.allowMultipleLogin":
                        if (bool.TryParse(config.ConfigValue, out bool allowMultipleLogin))
                            options.AllowMultipleLogin = allowMultipleLogin;
                        break;
                }
            }

            return options;
        }

        /// <summary>
        /// 验证登录尝试
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<bool> ValidateLoginAttemptAsync(string userName)
        {
            var options = await GetOptionsAsync();
            if (!options.EnableLoginRestriction)
                return true;

            var attempts = await GetFailedAttemptsAsync(userName);
            if (attempts >= options.MaxFailedAttempts)
            {
                var remainingMinutes = await GetLockoutRemainingMinutesAsync(userName);
                return remainingMinutes <= 0;
            }

            return true;
        }

        /// <summary>
        /// 记录登录失败
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task RecordFailedLoginAsync(string userName)
        {
            var options = await GetOptionsAsync();
            if (!options.EnableLoginRestriction)
                return;

            var key = $"login:failed:{userName}";
            var attempts = await GetFailedAttemptsAsync(userName);
            attempts++;

            if (attempts >= options.MaxFailedAttempts)
            {
                await LockAccountAsync(userName);
            }
            else
            {
                await _cache.SetAsync(key, attempts, TimeSpan.FromMinutes(options.LockoutMinutes));
            }
        }

        /// <summary>
        /// 记录登录成功
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task RecordSuccessfulLoginAsync(string userName)
        {
            var key = $"login:failed:{userName}";
            await _cache.RemoveAsync(key);
        }

        /// <summary>
        /// 获取剩余尝试次数
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<int> GetRemainingAttemptsAsync(string userName)
        {
            var options = await GetOptionsAsync();
            if (!options.EnableLoginRestriction)
                return -1;

            var attempts = await GetFailedAttemptsAsync(userName);
            return Math.Max(0, options.MaxFailedAttempts - attempts);
        }

        /// <summary>
        /// 获取锁定时间
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<int> GetLockoutRemainingMinutesAsync(string userName)
        {
            var key = $"login:locked:{userName}";
            var lockTime = await _cache.GetAsync<DateTime?>(key);
            if (!lockTime.HasValue)
            {
                return 0;
            }

            var options = await GetOptionsAsync();
            var remainingMinutes = (int)(lockTime.Value.AddMinutes(options.LockoutMinutes) - DateTime.Now).TotalMinutes;
            return remainingMinutes > 0 ? remainingMinutes : 0;
        }

        /// <summary>
        /// 获取失败尝试次数
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private async Task<int> GetFailedAttemptsAsync(string userName)
        {
            var key = $"login:failed:{userName}";
            return await _cache.GetAsync<int>(key);
        }

        /// <summary>
        /// 锁定账户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private async Task LockAccountAsync(string userName)
        {
            var key = $"login:locked:{userName}";
            await _cache.SetAsync(key, DateTime.Now, TimeSpan.FromMinutes(await GetLockoutRemainingMinutesAsync(userName)));
        }
    }
}