#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowScheduledTask.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流定时任务实体
//===================================================================

using System;
using SqlSugar;
using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.Domain.Entities.Workflow
{
    /// <summary>
    /// 工作流定时任务实体
    /// </summary>
    [SugarTable("hbt_workflow_scheduled_task", "工作流定时任务表")]
    public class HbtWorkflowScheduledTask : HbtBaseEntity
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        [SugarColumn(ColumnName = "workflow_instance_id", ColumnDescription = "工作流实例ID", ColumnDataType = "bigint", IsNullable = false)]
        public long WorkflowInstanceId { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        [SugarColumn(ColumnName = "node_id", ColumnDescription = "节点ID", ColumnDataType = "bigint", IsNullable = false)]
        public long NodeId { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        [SugarColumn(ColumnName = "task_type", ColumnDescription = "任务类型", ColumnDataType = "int", IsNullable = false)]
        public HbtWorkflowScheduledTaskType TaskType { get; set; }

        /// <summary>
        /// 计划执行时间
        /// </summary>
        [SugarColumn(ColumnName = "scheduled_time", ColumnDescription = "计划执行时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime ScheduledTime { get; set; }

        /// <summary>
        /// 实际执行时间
        /// </summary>
        [SugarColumn(ColumnName = "executed_time", ColumnDescription = "实际执行时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ExecutedTime { get; set; }

        /// <summary>
        /// 执行状态
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "执行状态", ColumnDataType = "int", IsNullable = false)]
        public HbtWorkflowScheduledTaskStatus Status { get; set; }

        /// <summary>
        /// 重试次数
        /// </summary>
        [SugarColumn(ColumnName = "retry_count", ColumnDescription = "重试次数", ColumnDataType = "int", IsNullable = false)]
        public int RetryCount { get; set; }

        /// <summary>
        /// 最大重试次数
        /// </summary>
        [SugarColumn(ColumnName = "max_retry_count", ColumnDescription = "最大重试次数", ColumnDataType = "int", IsNullable = false)]
        public int MaxRetryCount { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [SugarColumn(ColumnName = "error_message", ColumnDescription = "错误信息", ColumnDataType = "text", IsNullable = true)]
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// 任务参数(JSON格式)
        /// </summary>
        [SugarColumn(ColumnName = "task_parameters", ColumnDescription = "任务参数(JSON格式)", ColumnDataType = "text", IsNullable = true)]
        public string? TaskParameters { get; set; }

        /// <summary>
        /// 工作流实例
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(WorkflowInstanceId))]
        public HbtWorkflowInstance? WorkflowInstance { get; set; }

        /// <summary>
        /// 节点
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(NodeId))]
        public HbtWorkflowNode? Node { get; set; }
    }
} 