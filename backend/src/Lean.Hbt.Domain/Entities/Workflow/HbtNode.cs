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
    public class HbtNode : HbtBaseEntity
    {
        /// <summary>
        /// 实例ID
        /// </summary>
        [SugarColumn(ColumnName = "instance_id", ColumnDescription = "实例ID", ColumnDataType = "bigint", IsNullable = false)]
        public long InstanceId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        [SugarColumn(ColumnName = "node_name", ColumnDescription = "节点名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
        public string? NodeName { get; set; }

        /// <summary>
        /// 节点类型
        /// </summary>
        [SugarColumn(ColumnName = "node_type", ColumnDescription = "节点类型", ColumnDataType = "int", IsNullable = false)]
        public int NodeType { get; set; }

        /// <summary>
        /// 工作流定义ID
        /// </summary>
        [SugarColumn(ColumnName = "workflow_definition_id", ColumnDescription = "工作流定义ID", ColumnDataType = "bigint", IsNullable = false)]
        public long DefinitionId { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        [SugarColumn(ColumnName = "parent_node_id", ColumnDescription = "父节点ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ParentNodeId { get; set; }

        /// <summary>
        /// 节点配置
        /// </summary>
        [SugarColumn(ColumnName = "node_config", ColumnDescription = "节点配置", ColumnDataType = "text", IsNullable = false)]
        public string ?NodeConfig { get; set; }

        /// <summary>
        /// 状态(0:未开始 1:进行中 2:已完成 3:已取消)
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
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false)]
        public int OrderNum { get; set; }

        /// <summary>
        /// 工作流定义
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(DefinitionId))]
        public HbtDefinition? WorkflowDefinition { get; set; }

        /// <summary>
        /// 子节点列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(ParentNodeId))]
        public List<HbtNode>? ChildNodes { get; set; }
    }
}