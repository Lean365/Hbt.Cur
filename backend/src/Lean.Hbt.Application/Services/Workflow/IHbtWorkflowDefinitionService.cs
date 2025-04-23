//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : IHbtWorkflowDefinitionService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流定义服务接口
//===================================================================

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流定义服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public interface IHbtWorkflowDefinitionService
    {
        /// <summary>
        /// 获取工作流定义分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页结果</returns>
        Task<HbtPagedResult<HbtWorkflowDefinitionDto>> GetListAsync(HbtWorkflowDefinitionQueryDto query);

        /// <summary>
        /// 获取工作流定义详情
        /// </summary>
        /// <param name="id">工作流定义ID</param>
        /// <returns>工作流定义详情</returns>
        Task<HbtWorkflowDefinitionDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建工作流定义
        /// </summary>
        /// <param name="input">创建信息</param>
        /// <returns>新创建的工作流定义ID</returns>
        Task<long> CreateAsync(HbtWorkflowDefinitionCreateDto input);

        /// <summary>
        /// 更新工作流定义
        /// </summary>
        /// <param name="input">更新信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtWorkflowDefinitionUpdateDto input);

        /// <summary>
        /// 删除工作流定义
        /// </summary>
        /// <param name="id">工作流定义ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除工作流定义
        /// </summary>
        /// <param name="ids">工作流定义ID数组</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);

        /// <summary>
        /// 导入工作流定义
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入的工作流定义列表</returns>
        Task<List<HbtWorkflowDefinitionDto>> ImportAsync(Stream fileStream, string sheetName = "Sheet1");

        /// <summary>
        /// 导出工作流定义
        /// </summary>
        /// <param name="data">要导出的数据集合</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(IEnumerable<HbtWorkflowDefinitionDto> data, string sheetName = "Sheet1");

        /// <summary>
        /// 获取工作流定义导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1");

        /// <summary>
        /// 更新工作流定义状态
        /// </summary>
        /// <param name="input">状态更新信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(HbtWorkflowDefinitionStatusDto input);
    }
} 