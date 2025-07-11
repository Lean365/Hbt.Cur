//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtInstance.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 11:50
// 版本号 : V.0.0.1
// 描述    : 工作流实例实体类
//===================================================================

namespace Lean.Hbt.Domain.Entities.Workflow
{
    /// <summary>
    /// 工作流实例实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-22
    /// </remarks>
    [SugarTable("hbt_workflow_instance", "工作流实例表")]
    [SugarIndex("ix_workflow_instance_definition", nameof(DefinitionId), OrderByType.Asc)]
    [SugarIndex("ix_workflow_instance_business_key", nameof(BusinessKey), OrderByType.Asc, true)]
    [SugarIndex("ix_workflow_instance_status", nameof(Status), OrderByType.Asc)]
    [SugarIndex("ix_workflow_instance_initiator", nameof(InitiatorId), OrderByType.Asc)]
    [SugarIndex("ix_workflow_instance_start_time", nameof(StartTime), OrderByType.Desc)]
    public class HbtInstance : HbtBaseEntity
    {
        /// <summary>
        /// 实例名称
        /// </summary>
        [SugarColumn(ColumnName = "instance_name", ColumnDescription = "实例名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
        public string? InstanceName { get; set; }

        /// <summary>
        /// 业务键(唯一标识)
        /// </summary>
        [SugarColumn(ColumnName = "business_key", ColumnDescription = "业务键", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
        public string? BusinessKey { get; set; }

        /// <summary>
        /// 工作流定义ID
        /// </summary>
        [SugarColumn(ColumnName = "workflow_definition_id", ColumnDescription = "工作流定义ID", ColumnDataType = "bigint", IsNullable = false)]
        public long DefinitionId { get; set; }

        /// <summary>
        /// 当前节点ID
        /// </summary>
        [SugarColumn(ColumnName = "current_node_id", ColumnDescription = "当前节点ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? CurrentNodeId { get; set; }

        /// <summary>
        /// 发起人ID
        /// </summary>
        [SugarColumn(ColumnName = "initiator_id", ColumnDescription = "发起人ID", ColumnDataType = "bigint", IsNullable = false)]
        public long InitiatorId { get; set; }

        /// <summary>
        /// 表单数据(JSON格式)
        /// </summary>
        [SugarColumn(ColumnName = "form_data", ColumnDescription = "表单数据(JSON格式)", ColumnDataType = "text", IsNullable = true)]
        public string? FormData { get; set; }

        /// <summary>
        /// 实例状态(0:未开始 1:进行中 2:已完成 3:已终止 4:已挂起)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "实例状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [SugarColumn(ColumnName = "start_time", ColumnDescription = "开始时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [SugarColumn(ColumnName = "end_time", ColumnDescription = "结束时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 优先级(1=低 2=中 3=高 4=紧急)
        /// </summary>
        [SugarColumn(ColumnName = "priority", ColumnDescription = "优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int Priority { get; set; } = 2;


        /// <summary>
        /// 工作流定义
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(DefinitionId))]
        public HbtDefinition? WorkflowDefinition { get; set; }

        /// <summary>
        /// 当前节点
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(CurrentNodeId))]
        public HbtNode? CurrentNode { get; set; }

        /// <summary>
        /// 工作流节点列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtNode.InstanceId))]
        public List<HbtNode>? WorkflowNodes { get; set; }

        /// <summary>
        /// 工作流历史记录列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtHistory.InstanceId))]
        public List<HbtHistory>? WorkflowHistories { get; set; }

        /// <summary>
        /// 工作流任务列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtProcessTask.InstanceId))]
        public List<HbtProcessTask>? ProcessTasks { get; set; }
    }
}