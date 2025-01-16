//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : IUserDomainService.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-16 13:00
// 版本号 : V0.0.1
// 描述    : 用户领域服务接口
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;

namespace Lean.Hbt.Domain.Services.Identity
{
    /// <summary>
    /// 用户领域服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    public interface IUserDomainService
    {
        /// <summary>
        /// 验证用户密码
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="password">密码</param>
        /// <returns>是否验证通过</returns>
        Task<bool> ValidatePasswordAsync(HbtUser user, string password);

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="password">密码</param>
        /// <returns>创建结果</returns>
        Task<HbtUser> CreateUserAsync(HbtUser user, string password);

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="newPassword">新密码</param>
        /// <returns>更新结果</returns>
        Task<bool> UpdatePasswordAsync(HbtUser user, string newPassword);

        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>角色列表</returns>
        Task<List<HbtRole>> GetUserRolesAsync(long userId);

        /// <summary>
        /// 获取用户部门列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>部门列表</returns>
        Task<List<HbtDept>> GetUserDeptsAsync(long userId);

        /// <summary>
        /// 获取用户岗位列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>岗位列表</returns>
        Task<List<HbtPost>> GetUserPostsAsync(long userId);
    }
} 