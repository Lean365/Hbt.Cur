//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtTenantLogService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 租户审计日志服务接口
//===================================================================

using System.Threading.Tasks;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Application.Dtos.Audit;

namespace Lean.Hbt.Application.Services.Audit
{
    /// <summary>
    /// 租户审计日志服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public interface IHbtTenantLogService
    {
        /// <summary>
        /// 获取审计日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页结果</returns>
        Task<HbtPagedResult<HbtTenantLogDto>> GetListAsync(HbtTenantLogQueryDto query);

        /// <summary>
        /// 获取审计日志详情
        /// </summary>
        /// <param name="id">日志ID</param>
        /// <returns>审计日志详情</returns>
        Task<HbtTenantLogDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建审计日志
        /// </summary>
        /// <param name="dto">创建DTO</param>
        /// <returns>审计日志ID</returns>
        Task<long> CreateAsync(HbtTenantLogCreateDto dto);

        /// <summary>
        /// 更新审计日志
        /// </summary>
        /// <param name="dto">更新DTO</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtTenantLogUpdateDto dto);

        /// <summary>
        /// 删除审计日志
        /// </summary>
        /// <param name="id">日志ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除审计日志
        /// </summary>
        /// <param name="ids">日志ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);

        /// <summary>
        /// 导出审计日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtTenantLogQueryDto query, string sheetName);

        /// <summary>
        /// 清理审计日志
        /// </summary>
        /// <returns>是否成功</returns>
        Task<bool> ClearAsync();
    }
} 