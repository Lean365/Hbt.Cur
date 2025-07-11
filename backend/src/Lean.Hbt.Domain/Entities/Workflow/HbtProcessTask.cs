#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTask.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流任务实体
//===================================================================

namespace Lean.Hbt.Domain.Entities.Workflow
{
    /// <summary>
    /// 工作流任务实体
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    [SugarTable("hbt_workflow_process_task", "工作流任务表")]
    [SugarIndex("ix_workflow_process_task_instance", nameof(InstanceId), OrderByType.Asc)]
    public class HbtProcessTask : HbtBaseEntity
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        [SugarColumn(ColumnName = "instance_id", ColumnDescription = "工作流实例ID", ColumnDataType = "bigint", IsNullable = false)]
        public long InstanceId { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        [SugarColumn(ColumnName = "node_id", ColumnDescription = "节点ID", ColumnDataType = "bigint", IsNullable = false)]
        public long NodeId { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        [SugarColumn(ColumnName = "task_name", ColumnDescription = "任务名称", Length = 255, ColumnDataType = "nvarchar", IsNullable = false)]
        public string TaskName { get; set; } = string.Empty;

        /// <summary>
        /// 任务类型（0：未处理 1:审批 2:转交 3:退回，4：挂起）
        /// </summary>
        [SugarColumn(ColumnName = "task_type", ColumnDescription = "任务类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int TaskType { get; set; }

        /// <summary>
        /// 任务状态（0:待处理 1:处理中 2:已同意 3:已拒绝 4:已退回 5:已转办 6:已取消）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "任务状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; }

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
        /// 优先级(0:低 1:中 2:高)
        /// </summary>
        [SugarColumn(ColumnName = "priority", ColumnDescription = "优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Priority { get; set; }


        /// <summary>
        /// 工作流实例
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(InstanceId))]
        public HbtInstance? WorkflowInstance { get; set; }

        /// <summary>
        /// 节点
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(NodeId))]
        public HbtNode? Node { get; set; }
    }
}