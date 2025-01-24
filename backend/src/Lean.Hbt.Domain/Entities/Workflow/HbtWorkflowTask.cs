//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtWorkflowTask.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-22 11:50
// 版本号 : V.0.0.1
// 描述    : 工作流任务实体类
//===================================================================

using System;
using SqlSugar;
using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.Domain.Entities.Workflow
{
    /// <summary>
    /// 工作流任务实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-22
    /// </remarks>
    [SugarTable("hbt_workflow_task", "工作流任务表")]
    public class HbtWorkflowTask : HbtBaseEntity
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
        /// 处理人ID
        /// </summary>
        [SugarColumn(ColumnName = "handler_id", ColumnDescription = "处理人ID", ColumnDataType = "bigint", IsNullable = false)]
        public long HandlerId { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        [SugarColumn(ColumnName = "task_type", ColumnDescription = "任务类型", ColumnDataType = "tinyint", IsNullable = false)]
        public HbtWorkflowTaskType TaskType { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        [SugarColumn(ColumnName = "task_status", ColumnDescription = "任务状态", ColumnDataType = "tinyint", IsNullable = false)]
        public HbtWorkflowTaskStatus Status { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        [SugarColumn(ColumnName = "task_result", ColumnDescription = "处理结果", ColumnDataType = "tinyint", IsNullable = true)]
        public HbtWorkflowTaskResult? TaskResult { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        [SugarColumn(ColumnName = "task_comment", ColumnDescription = "处理意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string TaskComment { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [SugarColumn(ColumnName = "start_time", ColumnDescription = "开始时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        [SugarColumn(ColumnName = "complete_time", ColumnDescription = "完成时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? CompleteTime { get; set; }

        /// <summary>
        /// 工作流实例
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(WorkflowInstanceId))]
        public HbtWorkflowInstance WorkflowInstance { get; set; }

        /// <summary>
        /// 节点
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(NodeId))]
        public HbtWorkflowNode Node { get; set; }
    }
} 