//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtLoginPolicy.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 16:30
// 版本号 : V.0.0.1
// 描述    : 登录策略接口
//===================================================================

namespace Hbt.Cur.Domain.IServices.Security
{
    /// <summary>
    /// 登录策略接口
    /// </summary>
    public interface IHbtLoginPolicy
    {
        /// <summary>
        /// 验证登录尝试
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>(是否允许登录, 剩余等待时间(秒))</returns>
        Task<(bool allowed, int? remainingSeconds)> ValidateLoginAttemptAsync(string username);

        /// <summary>
        /// 验证管理员登录尝试(使用更严格的策略)
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>(是否允许登录, 剩余等待时间(秒))</returns>
        Task<(bool allowed, int? remainingSeconds)> ValidateAdminLoginAttemptAsync(string userName);

        /// <summary>
        /// 记录登录失败
        /// </summary>
        /// <param name="username">用户名</param>
        Task RecordFailedLoginAsync(string username);

        /// <summary>
        /// 记录管理员登录失败(使用更严格的策略)
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>任务</returns>
        Task RecordAdminFailedLoginAsync(string userName);

        /// <summary>
        /// 记录登录成功
        /// </summary>
        /// <param name="username">用户名</param>
        Task RecordSuccessfulLoginAsync(string username);

        /// <summary>
        /// 获取剩余登录尝试次数
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>剩余次数</returns>
        Task<int> GetRemainingAttemptsAsync(string userName);

        /// <summary>
        /// 获取账户锁定剩余时间(秒)
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>剩余秒数</returns>
        Task<int> GetLockoutRemainingSecondsAsync(string userName);
    }
}