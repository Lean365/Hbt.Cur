//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtOperLogService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 操作日志服务接口
//===================================================================

using Hbt.Application.Dtos.Audit;

namespace Hbt.Application.Services.Audit
{
    /// <summary>
    /// 操作日志服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public interface IHbtOperLogService
    {
        /// <summary>
        /// 获取操作日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页结果</returns>
        Task<HbtPagedResult<HbtOperLogDto>> GetListAsync(HbtOperLogQueryDto query);

        /// <summary>
        /// 获取操作日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>操作日志详情</returns>
        Task<HbtOperLogDto> GetByIdAsync(long logId);

        /// <summary>
        /// 导出操作日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtOperLogQueryDto query, string sheetName);

        /// <summary>
        /// 清空操作日志
        /// </summary>
        /// <returns>是否成功</returns>
        Task<bool> ClearAsync();
    }
}