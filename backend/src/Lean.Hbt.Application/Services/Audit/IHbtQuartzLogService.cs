//===================================================================
// 项目名 : Lean.Hbt.Application 
// 文件名 : IHbtQuartzLogService.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-26 14:30
// 版本号 : V1.0.0
// 描述   : 任务日志服务接口
//===================================================================

using System.Threading.Tasks;
using Lean.Hbt.Application.Dtos.Audit;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Services.Audit
{
    /// <summary>
    /// 任务日志服务接口
    /// </summary>
    public interface IHbtQuartzLogService
    {
        /// <summary>
        /// 获取任务日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页结果</returns>
        Task<HbtPagedResult<HbtQuartzLogDto>> GetPagedAsync(HbtQuartzLogQueryDto query);

        /// <summary>
        /// 获取任务日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>任务日志详情</returns>
        Task<HbtQuartzLogDto> GetByIdAsync(long logId);

        /// <summary>
        /// 导出任务日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtQuartzLogQueryDto query, string sheetName = "任务日志数据");

        /// <summary>
        /// 清空任务日志
        /// </summary>
        /// <returns>是否成功</returns>
        Task<bool> ClearAsync();
    }
} 