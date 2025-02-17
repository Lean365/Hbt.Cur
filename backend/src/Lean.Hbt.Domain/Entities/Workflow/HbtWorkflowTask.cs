#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowTask.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流任务实体
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Workflow
{
    /// <summary>
    /// 工作流任务实体
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
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
        /// 任务标题
        /// </summary>
        [SugarColumn(ColumnName = "task_title", ColumnDescription = "任务标题", Length = 255, ColumnDataType = "nvarchar", IsNullable = false)]
        public string TaskTitle { get; set; } = string.Empty;

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
        /// 处理人ID
        /// </summary>
        [SugarColumn(ColumnName = "handler_id", ColumnDescription = "处理人ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? AssigneeId { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        [SugarColumn(ColumnName = "task_comment", ColumnDescription = "处理意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Comment { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        [SugarColumn(ColumnName = "task_result", ColumnDescription = "处理结果", ColumnDataType = "tinyint", IsNullable = true)]
        public string? Result { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        [SugarColumn(ColumnName = "complete_time", ColumnDescription = "完成时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? CompleteTime { get; set; }

        /// <summary>
        /// 到期时间
        /// </summary>
        [SugarColumn(ColumnName = "due_time", ColumnDescription = "到期时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? DueTime { get; set; }

        /// <summary>
        /// 提醒时间
        /// </summary>
        [SugarColumn(ColumnName = "reminder_time", ColumnDescription = "提醒时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ReminderTime { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        [SugarColumn(ColumnName = "priority", ColumnDescription = "优先级", ColumnDataType = "int", IsNullable = false)]
        public int Priority { get; set; }

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