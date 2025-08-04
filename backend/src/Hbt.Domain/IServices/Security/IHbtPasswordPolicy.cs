//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : IHbtPasswordPolicy.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-17 16:30
// 版本号 : V.0.0.1
// 描述    : 密码策略接口
//===================================================================

namespace Hbt.Domain.IServices.Security
{
    /// <summary>
    /// 密码策略接口
    /// </summary>
    public interface IHbtPasswordPolicy
    {
        /// <summary>
        /// 验证密码复杂度
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>是否符合要求</returns>
        bool ValidatePasswordComplexity(string password);

        /// <summary>
        /// 验证密码历史
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="newPassword">新密码</param>
        /// <returns>是否符合要求</returns>
        Task<bool> ValidatePasswordHistoryAsync(long userId, string newPassword);

        /// <summary>
        /// 记录密码历史
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="password">密码</param>
        /// <returns>任务</returns>
        Task RecordPasswordHistoryAsync(long userId, string password);

        /// <summary>
        /// 验证密码有效期
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>是否需要修改密码</returns>
        Task<bool> ValidatePasswordExpirationAsync(long userId);

        /// <summary>
        /// 获取密码到期剩余天数
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>剩余天数</returns>
        Task<int> GetPasswordExpirationDaysAsync(long userId);

        /// <summary>
        /// 获取默认密码
        /// </summary>
        string DefaultPassword { get; }
    }
}