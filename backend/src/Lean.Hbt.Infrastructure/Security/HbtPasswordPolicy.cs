//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPasswordPolicy.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 10:00
// 版本号 : V.0.0.1
// 描述    : 密码策略实现
//===================================================================

using System.Text.RegularExpressions;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Infrastructure.Caching;
using Lean.Hbt.Infrastructure.Persistence;
using Microsoft.Extensions.Options;

namespace Lean.Hbt.Infrastructure.Security
{
    /// <summary>
    /// 密码策略实现
    /// </summary>
    public class HbtPasswordPolicy : IHbtPasswordPolicy
    {
        private readonly HbtPasswordPolicyOptions _options;
        private readonly IHbtRedisCache _cache;
        private readonly HbtDbContext _context;

        public HbtPasswordPolicy(IOptions<HbtPasswordPolicyOptions> options, IHbtRedisCache cache, HbtDbContext context)
        {
            _options = options.Value;
            _cache = cache;
            _context = context;
        }

        public bool ValidatePasswordComplexity(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            if (password.Length < _options.MinLength || password.Length > _options.MaxLength)
                return false;

            if (_options.RequireDigit && !password.Any(char.IsDigit))
                return false;

            if (_options.RequireLowercase && !password.Any(char.IsLower))
                return false;

            if (_options.RequireUppercase && !password.Any(char.IsUpper))
                return false;

            if (_options.RequireSpecialChar && !Regex.IsMatch(password, @"[^a-zA-Z0-9]"))
                return false;

            return true;
        }

        public async Task<bool> ValidatePasswordHistoryAsync(long userId, string newPassword)
        {
            if (!_options.EnablePasswordHistory)
                return true;

            var key = $"password:history:{userId}";
            var history = await _cache.GetAsync<List<string>>(key) ?? new List<string>();

            foreach (var oldPassword in history)
            {
                if (BCrypt.Net.BCrypt.Verify(newPassword, oldPassword))
                    return false;
            }

            return true;
        }

        public async Task RecordPasswordHistoryAsync(long userId, string password)
        {
            if (!_options.EnablePasswordHistory)
                return;

            var key = $"password:history:{userId}";
            var history = await _cache.GetAsync<List<string>>(key) ?? new List<string>();

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            history.Insert(0, hashedPassword);

            if (history.Count > _options.PasswordHistoryCount)
                history.RemoveAt(history.Count - 1);

            await _cache.SetAsync(key, history);

            // 更新最后修改密码时间
            var repo = _context.Client.GetSimpleClient<HbtUser>();
            var user = await repo.GetByIdAsync(userId);
            if (user != null)
            {
                user.LastPasswordChangeTime = DateTime.Now;
                await repo.UpdateAsync(user);
            }
        }

        public async Task<bool> ValidatePasswordExpirationAsync(long userId)
        {
            if (!_options.EnablePasswordExpiration)
                return false;

            var repo = _context.Client.GetSimpleClient<HbtUser>();
            var user = await repo.GetByIdAsync(userId);
            if (user == null || !user.LastPasswordChangeTime.HasValue)
                return true;

            var daysSinceLastChange = (DateTime.Now - user.LastPasswordChangeTime.Value).TotalDays;
            return daysSinceLastChange > _options.PasswordExpirationDays;
        }

        public async Task<int> GetPasswordExpirationDaysAsync(long userId)
        {
            if (!_options.EnablePasswordExpiration)
                return -1;

            var repo = _context.Client.GetSimpleClient<HbtUser>();
            var user = await repo.GetByIdAsync(userId);
            if (user == null || !user.LastPasswordChangeTime.HasValue)
                return 0;

            var daysSinceLastChange = (DateTime.Now - user.LastPasswordChangeTime.Value).TotalDays;
            var remainingDays = _options.PasswordExpirationDays - (int)daysSinceLastChange;

            return remainingDays > 0 ? remainingDays : 0;
        }
    }
}