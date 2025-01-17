//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : IHbtUserService.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-17 16:30
// 版本号 : V1.0.0
// 描述    : 用户服务接口
//===================================================================

using System.Threading.Tasks;
using System.Collections.Generic;
using Lean.Hbt.Application.Identity.Dtos;
using Lean.Hbt.Common.Models;

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
        /// 获取用户列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>用户列表</returns>
        Task<HbtPagedResult<HbtUserDto>> GetUserListAsync(HbtUserQueryDto query);

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns>用户详情</returns>
        Task<HbtUserDto> GetUserAsync(long id);

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input">创建信息</param>
        /// <returns>用户ID</returns>
        Task<long> CreateUserAsync(HbtUserCreateDto input);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="input">更新信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateUserAsync(HbtUserUpdateDto input);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteUserAsync(long id);

        /// <summary>
        /// 导入用户
        /// </summary>
        /// <param name="users">用户列表</param>
        /// <returns>导入结果</returns>
        Task<string> ImportUsersAsync(List<HbtUserImportDto> users);

        /// <summary>
        /// 导出用户
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>导出数据</returns>
        Task<List<HbtUserExportDto>> ExportUsersAsync(HbtUserQueryDto query);

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <returns>模板数据</returns>
        Task<List<HbtUserTemplateDto>> GetTemplateAsync();

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="input">状态信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(HbtUserStatusDto input);

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="input">密码信息</param>
        /// <returns>是否成功</returns>
        Task<bool> ResetPasswordAsync(HbtUserResetPwdDto input);

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="input">密码信息</param>
        /// <returns>是否成功</returns>
        Task<bool> ChangePasswordAsync(HbtUserChangePwdDto input);
    }
} 