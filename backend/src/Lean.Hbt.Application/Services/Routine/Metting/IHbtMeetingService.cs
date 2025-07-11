//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtMeetingService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 会议服务接口
//===================================================================

using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Application.Dtos.Routine;

namespace Lean.Hbt.Application.Services.Routine
{
    /// <summary>
    /// 会议服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface IHbtMeetingService
    {
        /// <summary>
        /// 获取会议分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>会议分页列表</returns>
        Task<HbtPagedResult<HbtMeetingDto>> GetListAsync(HbtMeetingQueryDto query);

        /// <summary>
        /// 获取会议详情
        /// </summary>
        /// <param name="meetingId">会议ID</param>
        /// <returns>会议详情</returns>
        Task<HbtMeetingDto> GetByIdAsync(long meetingId);

        /// <summary>
        /// 创建会议
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>会议ID</returns>
        Task<long> CreateAsync(HbtMeetingCreateDto input);

        /// <summary>
        /// 更新会议
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtMeetingUpdateDto input);

        /// <summary>
        /// 删除会议
        /// </summary>
        /// <param name="meetingId">会议ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long meetingId);

        /// <summary>
        /// 批量删除会议
        /// </summary>
        /// <param name="meetingIds">会议ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] meetingIds);

        /// <summary>
        /// 导入会议数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "会议信息");

        /// <summary>
        /// 导出会议数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtMeetingQueryDto query, string sheetName = "会议信息");

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "会议信息");
    }
} 