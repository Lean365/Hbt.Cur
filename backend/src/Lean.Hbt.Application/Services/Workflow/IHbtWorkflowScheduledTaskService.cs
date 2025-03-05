#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtWorkflowScheduledTaskService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流定时任务服务接口
//===================================================================

using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流定时任务服务接口
    /// </summary>
    public interface IHbtWorkflowScheduledTaskService
    {
        /// <summary>
        /// 创建定时任务
        /// </summary>
        /// <param name="workflowInstanceId">工作流实例ID</param>
        /// <param name="nodeId">节点ID</param>
        /// <param name="taskType">任务类型</param>
        /// <param name="scheduledTime">计划执行时间</param>
        /// <param name="parameters">任务参数(JSON格式)</param>
        /// <returns>任务ID</returns>
        Task<long> CreateAsync(long workflowInstanceId, long nodeId, int taskType, DateTime scheduledTime, string? parameters = null);

        /// <summary>
        /// 取消定时任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <returns>是否成功</returns>
        Task<bool> CancelAsync(long taskId);

        /// <summary>
        /// 执行定时任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <returns>是否成功</returns>
        Task<bool> ExecuteAsync(long taskId);

        /// <summary>
        /// 获取待执行的定时任务列表
        /// </summary>
        /// <param name="batchSize">批次大小</param>
        /// <returns>待执行的定时任务列表</returns>
        Task<List<HbtWorkflowScheduledTaskDto>> GetPendingTasksAsync(int batchSize = 100);

        /// <summary>
        /// 更新任务状态
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="status">新状态</param>
        /// <param name="errorMessage">错误信息(可选)</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(long taskId, int status, string? errorMessage = null);

        /// <summary>
        /// 清理过期任务
        /// </summary>
        /// <param name="days">保留天数</param>
        /// <returns>清理的任务数量</returns>
        Task<int> CleanupExpiredTasksAsync(int days = 30);
    }
} 