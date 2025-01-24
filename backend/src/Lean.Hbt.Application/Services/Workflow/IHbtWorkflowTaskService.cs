//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : IHbtWorkflowTaskService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流任务服务接口
//===================================================================

using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流任务服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public interface IHbtWorkflowTaskService
    {
        /// <summary>
        /// 获取工作流任务分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页结果</returns>
        Task<HbtPagedResult<HbtWorkflowTaskDto>> GetPagedListAsync(HbtWorkflowTaskQueryDto query);

        /// <summary>
        /// 获取工作流任务详情
        /// </summary>
        /// <param name="id">工作流任务ID</param>
        /// <returns>工作流任务详情</returns>
        Task<HbtWorkflowTaskDto> GetAsync(long id);

        /// <summary>
        /// 创建工作流任务
        /// </summary>
        /// <param name="input">创建信息</param>
        /// <returns>新创建的工作流任务ID</returns>
        Task<long> InsertAsync(HbtWorkflowTaskCreateDto input);

        /// <summary>
        /// 更新工作流任务
        /// </summary>
        /// <param name="input">更新信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtWorkflowTaskUpdateDto input);

        /// <summary>
        /// 删除工作流任务
        /// </summary>
        /// <param name="id">工作流任务ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除工作流任务
        /// </summary>
        /// <param name="ids">工作流任务ID数组</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);

        /// <summary>
        /// 导入工作流任务
        /// </summary>
        /// <param name="tasks">工作流任务导入列表</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(List<HbtWorkflowTaskImportDto> tasks);

        /// <summary>
        /// 导出工作流任务
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>导出数据列表</returns>
        Task<List<HbtWorkflowTaskExportDto>> ExportAsync(HbtWorkflowTaskQueryDto query);

        /// <summary>
        /// 获取工作流任务导入模板
        /// </summary>
        /// <returns>模板数据</returns>
        Task<HbtWorkflowTaskTemplateDto> GetTemplateAsync();

        /// <summary>
        /// 更新工作流任务状态
        /// </summary>
        /// <param name="input">状态更新信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(HbtWorkflowTaskStatusDto input);

        /// <summary>
        /// 完成工作流任务
        /// </summary>
        /// <param name="id">工作流任务ID</param>
        /// <param name="result">处理结果</param>
        /// <param name="comment">处理意见</param>
        /// <returns>是否成功</returns>
        Task<bool> CompleteAsync(long id, string result, string comment);

        /// <summary>
        /// 转办工作流任务
        /// </summary>
        /// <param name="id">工作流任务ID</param>
        /// <param name="assigneeId">新处理人ID</param>
        /// <param name="comment">转办说明</param>
        /// <returns>是否成功</returns>
        Task<bool> TransferAsync(long id, long assigneeId, string comment);

        /// <summary>
        /// 退回工作流任务
        /// </summary>
        /// <param name="id">工作流任务ID</param>
        /// <param name="comment">退回说明</param>
        /// <returns>是否成功</returns>
        Task<bool> RejectAsync(long id, string comment);

        /// <summary>
        /// 撤销工作流任务
        /// </summary>
        /// <param name="id">工作流任务ID</param>
        /// <param name="comment">撤销说明</param>
        /// <returns>是否成功</returns>
        Task<bool> CancelAsync(long id, string comment);
    }
} 