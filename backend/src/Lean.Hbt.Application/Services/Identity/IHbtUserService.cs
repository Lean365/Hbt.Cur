//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtUserService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 用户服务接口
//===================================================================

using Lean.Hbt.Application.Dtos.Identity;

namespace Lean.Hbt.Application.Services.Identity
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-17
    /// </remarks>
    public interface IHbtUserService
    {
        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>用户分页列表</returns>
        Task<HbtPagedResult<HbtUserDto>> GetListAsync(HbtUserQueryDto query);

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户详情</returns>
        Task<HbtUserDto> GetByIdAsync(long userId);

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>用户ID</returns>
        Task<long> CreateAsync(HbtUserCreateDto input);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtUserUpdateDto input);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long userId);

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="ids">用户ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);

        /// <summary>
        /// 导入用户数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1");

        /// <summary>
        /// 导出用户数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtUserQueryDto query, string sheetName = "Sheet1");

        /// <summary>
        /// 获取用户导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1");

        /// <summary>
        /// 更新用户状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(HbtUserStatusDto input);

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="input">重置密码对象</param>
        /// <returns>是否成功</returns>
        Task<bool> ResetPasswordAsync(HbtUserResetPwdDto input);

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="input">修改密码对象</param>
        /// <returns>是否成功</returns>
        Task<bool> ChangePasswordAsync(HbtUserChangePwdDto input);

        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="input">解锁用户信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UnlockUserAsync(HbtUserUnlockDto input);

        /// <summary>
        /// 获取用户选项列表
        /// </summary>
        /// <returns>用户选项列表</returns>
        Task<List<HbtSelectOption>> GetOptionsAsync();

        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户角色列表</returns>
        Task<List<HbtUserRoleDto>> GetUserRoleIdsAsync(long userId);

        /// <summary>
        /// 获取用户部门列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户部门列表</returns>
        Task<List<HbtUserDeptDto>> GetUserDeptIdsAsync(long userId);

        /// <summary>
        /// 获取用户岗位列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户岗位列表</returns>
        Task<List<HbtUserPostDto>> GetUserPostIdsAsync(long userId);

        /// <summary>
        /// 分配用户角色
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="roleIds">角色ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> AllocateUserRolesAsync(long userId, long[] roleIds);

        /// <summary>
        /// 分配用户部门
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deptIds">部门ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> AllocateUserDeptsAsync(long userId, long[] deptIds);

        /// <summary>
        /// 分配用户岗位
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="postIds">岗位ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> AllocateUserPostsAsync(long userId, long[] postIds);
    }
}