//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : IHbtWorkflowVariableService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流变量服务接口
//===================================================================

using System.IO;
using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流变量服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public interface IHbtWorkflowVariableService
    {
        /// <summary>
        /// 获取工作流变量分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页结果</returns>
        Task<HbtPagedResult<HbtWorkflowVariableDto>> GetPagedListAsync(HbtWorkflowVariableQueryDto query);

        /// <summary>
        /// 获取工作流变量详情
        /// </summary>
        /// <param name="id">工作流变量ID</param>
        /// <returns>工作流变量详情</returns>
        Task<HbtWorkflowVariableDto> GetAsync(long id);

        /// <summary>
        /// 创建工作流变量
        /// </summary>
        /// <param name="input">创建信息</param>
        /// <returns>新创建的工作流变量ID</returns>
        Task<long> InsertAsync(HbtWorkflowVariableCreateDto input);

        /// <summary>
        /// 更新工作流变量
        /// </summary>
        /// <param name="input">更新信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtWorkflowVariableUpdateDto input);

        /// <summary>
        /// 删除工作流变量
        /// </summary>
        /// <param name="id">工作流变量ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除工作流变量
        /// </summary>
        /// <param name="ids">工作流变量ID数组</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);

        /// <summary>
        /// 导出工作流变量(单个Sheet)
        /// </summary>
        /// <param name="data">数据集合</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<byte[]> ExportAsync(IEnumerable<HbtWorkflowVariableDto> data, string sheetName = "Sheet1");

        /// <summary>
        /// 导入工作流变量(单个Sheet)
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>数据集合</returns>
        Task<List<HbtWorkflowVariableDto>> ImportAsync(Stream fileStream, string sheetName = "Sheet1");

        /// <summary>
        /// 导出工作流变量(多个Sheet)
        /// </summary>
        /// <param name="sheets">Sheet数据字典，key为sheet名称，value为数据集合</param>
        /// <returns>Excel文件字节数组</returns>
        Task<byte[]> ExportMultiSheetAsync(Dictionary<string, IEnumerable<HbtWorkflowVariableDto>> sheets);

        /// <summary>
        /// 导入工作流变量(多个Sheet)
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <returns>数据字典，key为sheet名称，value为数据集合</returns>
        Task<Dictionary<string, List<HbtWorkflowVariableDto>>> ImportMultiSheetAsync(Stream fileStream);

        /// <summary>
        /// 生成工作流变量导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件字节数组</returns>
        Task<byte[]> GetTemplateAsync(string sheetName = "Sheet1");

        /// <summary>
        /// 获取工作流实例的所有变量
        /// </summary>
        /// <param name="workflowInstanceId">工作流实例ID</param>
        /// <returns>变量列表</returns>
        Task<List<HbtWorkflowVariableDto>> GetVariablesByWorkflowInstanceAsync(long workflowInstanceId);

        /// <summary>
        /// 获取工作流节点的所有变量
        /// </summary>
        /// <param name="workflowNodeId">工作流节点ID</param>
        /// <returns>变量列表</returns>
        Task<List<HbtWorkflowVariableDto>> GetVariablesByWorkflowNodeAsync(long workflowNodeId);

        /// <summary>
        /// 获取工作流变量值
        /// </summary>
        /// <param name="workflowInstanceId">工作流实例ID</param>
        /// <param name="variableName">变量名称</param>
        /// <returns>变量值</returns>
        Task<string> GetVariableValueAsync(long workflowInstanceId, string variableName);

        /// <summary>
        /// 设置工作流变量值
        /// </summary>
        /// <param name="workflowInstanceId">工作流实例ID</param>
        /// <param name="variableName">变量名称</param>
        /// <param name="variableValue">变量值</param>
        /// <returns>是否成功</returns>
        Task<bool> SetVariableValueAsync(long workflowInstanceId, string variableName, string variableValue);
    }
} 