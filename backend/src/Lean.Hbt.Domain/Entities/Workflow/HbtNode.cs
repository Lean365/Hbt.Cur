//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtNode.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 11:50
// 版本号 : V.0.0.1
// 描述    : 工作流节点实体类
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Workflow
{
    /// <summary>
    /// 工作流节点实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-22
    /// </remarks>
    [SugarTable("hbt_workflow_node", "工作流节点表")]
    [SugarIndex("ix_workflow_node_instance", nameof(InstanceId), OrderByType.Asc)]
    [SugarIndex("ix_workflow_node_parent", nameof(ParentNodeId), OrderByType.Asc)]
    [SugarIndex("ix_workflow_node_status", nameof(Status), OrderByType.Asc)]
    [SugarIndex("ix_workflow_node_template", nameof(NodeTemplateId), OrderByType.Asc)]
    public class HbtNode : HbtBaseEntity
    {
        /// <summary>
        /// 实例ID
        /// </summary>
        [SugarColumn(ColumnName = "instance_id", ColumnDescription = "实例ID", ColumnDataType = "bigint", IsNullable = false)]
        public long InstanceId { get; set; }

        /// <summary>
        /// 对应的节点模板ID
        /// </summary>
        [SugarColumn(ColumnName = "node_template_id", ColumnDescription = "节点模板ID", ColumnDataType = "bigint", IsNullable = false)]
        public long NodeTemplateId { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        [SugarColumn(ColumnName = "parent_node_id", ColumnDescription = "父节点ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ParentNodeId { get; set; }

        /// <summary>
        /// 状态(0:未开始 1:进行中 2:已完成 3:已取消 4:已跳过)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; }

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
        /// 节点结果(JSON格式)
        /// 包含：审批结果、条件判断结果等
        /// </summary>
        [SugarColumn(ColumnName = "node_result", ColumnDescription = "节点结果", ColumnDataType = "text", IsNullable = true)]
        public string? NodeResult { get; set; }

        /// <summary>
        /// 运行时配置(JSON格式)
        /// 可以覆盖模板配置，用于运行时调整
        /// </summary>
        [SugarColumn(ColumnName = "runtime_config", ColumnDescription = "运行时配置", ColumnDataType = "text", IsNullable = true)]
        public string? RuntimeConfig { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; }

        /// <summary>
        /// 工作流实例
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(InstanceId))]
        public HbtInstance? WorkflowInstance { get; set; }

        /// <summary>
        /// 节点模板
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(NodeTemplateId))]
        public HbtNodeTemplate? NodeTemplate { get; set; }

        /// <summary>
        /// 子节点列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(ParentNodeId))]
        public List<HbtNode>? ChildNodes { get; set; }

        /// <summary>
        /// 工作流任务列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtProcessTask.NodeId))]
        public List<HbtProcessTask>? ProcessTasks { get; set; }
    }
}