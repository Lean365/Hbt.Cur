//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtInstance.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流实例实体类
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Workflow
{
    /// <summary>
    /// 工作流实例实体类
    /// </summary>
    [SugarTable("hbt_workflow_instance", "工作流实例表")]
    [SugarIndex("ix_workflow_instance_scheme", nameof(SchemeId), OrderByType.Asc)]
    [SugarIndex("ix_workflow_instance_status", nameof(Status), OrderByType.Asc)]
    [SugarIndex("ix_workflow_instance_priority", nameof(Priority), OrderByType.Asc)]
    [SugarIndex("ix_workflow_instance_urgency", nameof(Urgency), OrderByType.Asc)]
    [SugarIndex("ix_workflow_instance_initiator", nameof(InitiatorId), OrderByType.Asc)]
    public class HbtInstance : HbtBaseEntity
    {
        /// <summary>
        /// 流程定义ID
        /// </summary>
        [SugarColumn(ColumnName = "scheme_id", ColumnDescription = "流程定义ID", ColumnDataType = "bigint", IsNullable = false)]
        public long SchemeId { get; set; }

        /// <summary>
        /// 实例标题
        /// </summary>
        [SugarColumn(ColumnName = "instance_title", ColumnDescription = "实例标题", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
        public string InstanceTitle { get; set; } = string.Empty;

        /// <summary>
        /// 业务键
        /// </summary>
        [SugarColumn(ColumnName = "business_key", ColumnDescription = "业务键", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
        public string? BusinessKey { get; set; }

        /// <summary>
        /// 发起人ID
        /// </summary>
        [SugarColumn(ColumnName = "initiator_id", ColumnDescription = "发起人ID", ColumnDataType = "bigint", IsNullable = false)]
        public long InitiatorId { get; set; }

        /// <summary>
        /// 当前节点ID
        /// </summary>
        [SugarColumn(ColumnName = "current_node_id", ColumnDescription = "当前节点ID", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
        public string? CurrentNodeId { get; set; }

        /// <summary>
        /// 当前节点名称
        /// </summary>
        [SugarColumn(ColumnName = "current_node_name", ColumnDescription = "当前节点名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
        public string? CurrentNodeName { get; set; }

        /// <summary>
        /// 状态(0:草稿 1:运行中 2:已完成 3:已暂停 4:已终止)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; }

        /// <summary>
        /// 优先级(1:低 2:普通 3:高 4:紧急 5:特急)
        /// </summary>
        [SugarColumn(ColumnName = "priority", ColumnDescription = "优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int Priority { get; set; }

        /// <summary>
        /// 紧急程度(1:普通 2:加急 3:特急)
        /// </summary>
        [SugarColumn(ColumnName = "urgency", ColumnDescription = "紧急程度", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int Urgency { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [SugarColumn(ColumnName = "start_time", ColumnDescription = "开始时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [SugarColumn(ColumnName = "end_time", ColumnDescription = "结束时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 流程变量(JSON格式)
        /// </summary>
        [SugarColumn(ColumnName = "variables", ColumnDescription = "流程变量", ColumnDataType = "text", IsNullable = true)]
        public string? Variables { get; set; }

        /// <summary>
        /// 流程定义
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(SchemeId))]
        public HbtScheme? WorkflowScheme { get; set; }

        /// <summary>
        /// 表单列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtForm.InstanceId))]
        public List<HbtForm>? Forms { get; set; }

        /// <summary>
        /// 流程历史列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtInstanceTrans.InstanceId))]
        public List<HbtInstanceTrans>? WorkflowHistories { get; set; }

        /// <summary>
        /// 操作记录列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtInstanceOper.InstanceId))]
        public List<HbtInstanceOper>? WorkflowOperations { get; set; }
    }
}