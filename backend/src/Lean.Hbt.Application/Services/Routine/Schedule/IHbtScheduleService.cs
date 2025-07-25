//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtScheduleService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 日程服务接口
//===================================================================

using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Application.Dtos.Routine;

namespace Lean.Hbt.Application.Services.Routine.Schedule
{
    /// <summary>
    /// 日程服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface IHbtScheduleService
    {
        /// <summary>
        /// 获取日程分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>日程分页列表</returns>
        Task<HbtPagedResult<HbtScheduleDto>> GetListAsync(HbtScheduleQueryDto query);

        /// <summary>
        /// 获取日程详情
        /// </summary>
        /// <param name="scheduleId">日程ID</param>
        /// <returns>日程详情</returns>
        Task<HbtScheduleDto> GetByIdAsync(long scheduleId);

        /// <summary>
        /// 创建日程
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>日程ID</returns>
        Task<long> CreateAsync(HbtScheduleCreateDto input);

        /// <summary>
        /// 更新日程
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtScheduleUpdateDto input);

        /// <summary>
        /// 删除日程
        /// </summary>
        /// <param name="scheduleId">日程ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long scheduleId);

        /// <summary>
        /// 批量删除日程
        /// </summary>
        /// <param name="scheduleIds">日程ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] scheduleIds);

        /// <summary>
        /// 导入日程数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "日程信息");

        /// <summary>
        /// 导出日程数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtScheduleQueryDto query, string sheetName = "日程信息");

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "日程信息");

        /// <summary>
        /// 获取指定用户的日程状态统计
        /// </summary>
        /// <param name="createBy">创建者</param>
        /// <returns>状态-数量字典</returns>
        Task<Dictionary<int, int>> GetStatusStatisticsAsync(string createBy);
    }
} 