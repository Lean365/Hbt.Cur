//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowNode.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 11:50
// 版本号 : V.0.0.1
// 描述    : 工作流节点实体类
//===================================================================

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
    [SugarTable("hbt_wf_node", "工作流节点表")]
    public class HbtWorkflowNode : HbtBaseEntity
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        [SugarColumn(ColumnName = "node_name", ColumnDescription = "节点名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
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
        public long WorkflowDefinitionId { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        [SugarColumn(ColumnName = "parent_node_id", ColumnDescription = "父节点ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ParentNodeId { get; set; }

        /// <summary>
        /// 节点配置(JSON格式)
        /// 包含：审批人配置、条件配置、预警配置等
        /// </summary>
        [SugarColumn(ColumnName = "node_config", ColumnDescription = "节点配置(JSON格式)", ColumnDataType = "text", IsNullable = false)]
        public string? NodeConfig { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "node_sort", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false)]
        public int OrderNum { get; set; }

        /// <summary>
        /// 工作流定义
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(WorkflowDefinitionId))]
        public HbtWorkflowDefinition? WorkflowDefinition { get; set; }

        /// <summary>
        /// 子节点列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(ParentNodeId))]
        public List<HbtWorkflowNode>? ChildNodes { get; set; }
    }
}