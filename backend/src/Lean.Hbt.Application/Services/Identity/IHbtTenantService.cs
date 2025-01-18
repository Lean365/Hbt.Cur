//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtTenantService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 19:15
// 版本号 : V0.0.1
// 描述   : 租户服务接口
//===================================================================

using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Services.Identity;

/// <summary>
/// 租户服务接口
/// </summary>
public interface IHbtTenantService
{
    /// <summary>
    /// 获取租户列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>租户列表</returns>
    Task<HbtPagedResult<HbtTenantDto>> GetPagedListAsync(HbtTenantQueryDto query);

    /// <summary>
    /// 获取租户详情
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <returns>租户详情</returns>
    Task<HbtTenantDto> GetAsync(long id);

    /// <summary>
    /// 创建租户
    /// </summary>
    /// <param name="input">创建对象</param>
    /// <returns>租户ID</returns>
    Task<long> CreateAsync(HbtTenantCreateDto input);

    /// <summary>
    /// 更新租户
    /// </summary>
    /// <param name="input">更新对象</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateAsync(HbtTenantUpdateDto input);

    /// <summary>
    /// 删除租户
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 批量删除租户
    /// </summary>
    /// <param name="ids">租户ID集合</param>
    /// <returns>是否成功</returns>
    Task<bool> BatchDeleteAsync(long[] ids);

    /// <summary>
    /// 导出租户数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>导出数据列表</returns>
    Task<List<HbtTenantExportDto>> ExportAsync(HbtTenantQueryDto query);

    /// <summary>
    /// 更新租户状态
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <param name="status">状态</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateStatusAsync(long id, HbtStatus status);
}