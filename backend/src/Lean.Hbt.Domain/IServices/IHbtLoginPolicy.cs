//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : IHbtLoginPolicy.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-17 16:30
// 版本号 : V.0.0.1
// 描述    : 登录策略接口
//===================================================================

using System.Threading.Tasks;

namespace Lean.Hbt.Domain.IServices
{
    /// <summary>
    /// 登录策略接口
    /// </summary>
    public interface IHbtLoginPolicy
    {
        /// <summary>
        /// 验证登录尝试
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>是否允许登录</returns>
        Task<bool> ValidateLoginAttemptAsync(string userName);

        /// <summary>
        /// 记录登录失败
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>任务</returns>
        Task RecordFailedLoginAsync(string userName);

        /// <summary>
        /// 记录登录成功
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>任务</returns>
        Task RecordSuccessfulLoginAsync(string userName);

        /// <summary>
        /// 获取剩余登录尝试次数
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>剩余次数</returns>
        Task<int> GetRemainingAttemptsAsync(string userName);

        /// <summary>
        /// 获取账户锁定剩余时间(分钟)
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>剩余分钟</returns>
        Task<int> GetLockoutRemainingMinutesAsync(string userName);
    }
} 