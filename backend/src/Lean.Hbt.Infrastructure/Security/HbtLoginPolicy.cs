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
using Lean.Hbt.Infrastructure.Caching;
using Microsoft.Extensions.Options;

namespace Lean.Hbt.Infrastructure.Security
{
    /// <summary>
    /// 登录限制策略实现
    /// </summary>
    public class HbtLoginPolicy : IHbtLoginPolicy
    {
        private readonly HbtLoginPolicyOptions _options;
        private readonly IHbtRedisCache _cache;

        public HbtLoginPolicy(IOptions<HbtLoginPolicyOptions> options, IHbtRedisCache cache)
        {
            _options = options.Value;
            _cache = cache;
        }

        public async Task<bool> ValidateLoginAttemptAsync(string userName)
        {
            if (!_options.EnableLoginRestriction)
                return true;

            var attempts = await GetFailedAttemptsAsync(userName);
            if (attempts >= _options.MaxFailedAttempts)
            {
                var remainingMinutes = await GetLockoutRemainingMinutesAsync(userName);
                return remainingMinutes <= 0;
            }

            return true;
        }

        public async Task RecordFailedLoginAsync(string userName)
        {
            if (!_options.EnableLoginRestriction)
                return;

            var key = $"login:failed:{userName}";
            var attempts = await GetFailedAttemptsAsync(userName);
            attempts++;

            if (attempts >= _options.MaxFailedAttempts)
            {
                await LockAccountAsync(userName);
            }
            else
            {
                await _cache.SetAsync(key, attempts, TimeSpan.FromMinutes(_options.LockoutMinutes));
            }
        }

        public async Task RecordSuccessfulLoginAsync(string userName)
        {
            var key = $"login:failed:{userName}";
            await _cache.RemoveAsync(key);
        }

        public async Task<int> GetRemainingAttemptsAsync(string userName)
        {
            if (!_options.EnableLoginRestriction)
                return -1;

            var attempts = await GetFailedAttemptsAsync(userName);
            return Math.Max(0, _options.MaxFailedAttempts - attempts);
        }

        public async Task<int> GetLockoutRemainingMinutesAsync(string userName)
        {
            var key = $"login:locked:{userName}";
            var lockTime = await _cache.GetAsync<DateTime?>(key);
            if (!lockTime.HasValue)
            {
                return 0;
            }

            var remainingMinutes = (int)(lockTime.Value.AddMinutes(_options.LockoutMinutes) - DateTime.Now).TotalMinutes;
            return remainingMinutes > 0 ? remainingMinutes : 0;
        }

        private async Task<int> GetFailedAttemptsAsync(string userName)
        {
            var key = $"login:failed:{userName}";
            return await _cache.GetAsync<int>(key);
        }

        private async Task LockAccountAsync(string userName)
        {
            var key = $"login:locked:{userName}";
            await _cache.SetAsync(key, DateTime.Now, TimeSpan.FromMinutes(_options.LockoutMinutes));
        }
    }
}