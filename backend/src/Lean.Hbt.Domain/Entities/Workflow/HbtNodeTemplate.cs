//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtNodeTemplate.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 11:50
// 版本号 : V.0.0.1
// 描述    : 工作流节点模板实体类
//===================================================================

namespace Lean.Hbt.Domain.Entities.Workflow
{
    /// <summary>
    /// 工作流节点模板实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-22
    /// </remarks>
    [SugarTable("hbt_workflow_node_template", "工作流节点模板表")]
    [SugarIndex("ix_node_template_definition", nameof(DefinitionId), OrderByType.Asc)]
    [SugarIndex("ix_node_template_parent", nameof(ParentNodeId), OrderByType.Asc)]
    public class HbtNodeTemplate : HbtBaseEntity
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        [SugarColumn(ColumnName = "node_name", ColumnDescription = "节点名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
        public string? NodeName { get; set; }

        /// <summary>
        /// 节点类型(1=开始节点 2=审批节点 3=条件节点 4=并行节点 5=结束节点)
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
        /// 节点配置(JSON格式)
        /// 包含：审批人配置、条件配置、表单配置等
        /// </summary>
        [SugarColumn(ColumnName = "node_config", ColumnDescription = "节点配置", ColumnDataType = "text", IsNullable = false)]
        public string? NodeConfig { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false)]
        public int OrderNum { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [SugarColumn(ColumnName = "is_enabled", ColumnDescription = "是否启用", ColumnDataType = "bit", IsNullable = false, DefaultValue = "1")]
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 工作流定义
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(DefinitionId))]
        public HbtDefinition? WorkflowDefinition { get; set; }

        /// <summary>
        /// 子节点模板列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(ParentNodeId))]
        public List<HbtNodeTemplate>? ChildNodeTemplates { get; set; }

        /// <summary>
        /// 关联的工作流活动列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtActivity.NodeTemplateId))]
        public List<HbtActivity>? WorkflowActivities { get; set; }
    }
} 