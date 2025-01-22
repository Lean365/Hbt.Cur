//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtRoleService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 角色服务接口
//===================================================================

using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Application.Dtos.Identity;
using System.IO;

namespace Lean.Hbt.Application.Services.Identity
{
    /// <summary>
    /// 角色服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public interface IHbtRoleService
    {
        /// <summary>
        /// 获取角色分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页结果</returns>
        Task<HbtPagedResult<HbtRoleDto>> GetPagedListAsync(HbtRoleQueryDto query);

        /// <summary>
        /// 获取角色详情
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>角色详情</returns>
        Task<HbtRoleDto> GetAsync(long roleId);

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>角色ID</returns>
        Task<long> InsertAsync(HbtRoleCreateDto input);

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtRoleUpdateDto input);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long roleId);

        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="roleIds">角色ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] roleIds);

        /// <summary>
        /// 导出角色数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>导出的Excel文件字节数组</returns>
        Task<byte[]> ExportAsync(HbtRoleQueryDto query);

        /// <summary>
        /// 更新角色状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(HbtRoleStatusDto input);

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <returns>导入模板Excel文件字节数组</returns>
        Task<byte[]> GetImportTemplateAsync();

        /// <summary>
        /// 导入角色数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <returns>导入结果</returns>
        Task<string> ImportAsync(Stream fileStream);
    }
} 