//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtLoginPolicy.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-16 10:00
// 版本号 : V0.0.1
// 描述    : 登录限制策略实现
//===================================================================

using Lean.Hbt.Infrastructure.Caching;

namespace Lean.Hbt.Infrastructure.Security
{
    /// <summary>
    /// 登录限制策略实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    public class HbtLoginPolicy : IHbtLoginPolicy
    {
        /// <summary>
        /// 最大失败次数
        /// </summary>
        private const int MaxFailedAttempts = 5;

        /// <summary>
        /// 锁定时间（分钟）
        /// </summary>
        private const int LockoutMinutes = 30;

        /// <summary>
        /// 缓存服务
        /// </summary>
        private readonly IHbtRedisCache _cache;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cache">缓存服务</param>
        public HbtLoginPolicy(IHbtRedisCache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// 记录登录失败
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>剩余尝试次数</returns>
        public async Task<int> RecordFailedAttemptAsync(string userId)
        {
            var key = $"login:failed:{userId}";
            var attempts = await GetFailedAttemptsAsync(userId);
            attempts++;

            if (attempts >= MaxFailedAttempts)
            {
                await LockAccountAsync(userId);
                return 0;
            }

            await _cache.SetAsync(key, attempts, TimeSpan.FromMinutes(LockoutMinutes));
            return MaxFailedAttempts - attempts;
        }

        /// <summary>
        /// 重置登录失败次数
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>异步任务</returns>
        public async Task ResetFailedAttemptsAsync(string userId)
        {
            var key = $"login:failed:{userId}";
            await _cache.RemoveAsync(key);
        }

        /// <summary>
        /// 检查账户是否被锁定
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>是否锁定</returns>
        public async Task<bool> IsAccountLockedAsync(string userId)
        {
            var key = $"login:locked:{userId}";
            return await _cache.ExistsAsync(key);
        }

        /// <summary>
        /// 获取账户锁定剩余时间
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>剩余分钟数</returns>
        public async Task<int> GetLockoutRemainingMinutesAsync(string userId)
        {
            var key = $"login:locked:{userId}";
            var lockTime = await _cache.GetAsync<DateTime?>(key);
            if (!lockTime.HasValue)
            {
                return 0;
            }

            var remainingMinutes = (int)(lockTime.Value.AddMinutes(LockoutMinutes) - DateTime.Now).TotalMinutes;
            return remainingMinutes > 0 ? remainingMinutes : 0;
        }

        /// <summary>
        /// 获取登录失败次数
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>失败次数</returns>
        private async Task<int> GetFailedAttemptsAsync(string userId)
        {
            var key = $"login:failed:{userId}";
            return await _cache.GetAsync<int>(key);
        }

        /// <summary>
        /// 锁定账户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>异步任务</returns>
        private async Task LockAccountAsync(string userId)
        {
            var key = $"login:locked:{userId}";
            await _cache.SetAsync(key, DateTime.Now, TimeSpan.FromMinutes(LockoutMinutes));
        }
    }
} 