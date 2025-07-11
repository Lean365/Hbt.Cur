#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtActivity.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流活动实体
//===================================================================

namespace Lean.Hbt.Domain.Entities.Workflow
{
    /// <summary>
    /// 工作流活动实体
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    [SugarTable("hbt_workflow_activity", "工作流活动表")]
    [SugarIndex("ix_workflow_activity_definition", nameof(DefinitionId), OrderByType.Asc)]
    [SugarIndex("ix_workflow_activity_node_template", nameof(NodeTemplateId), OrderByType.Asc)]
    public class HbtActivity : HbtBaseEntity
    {
        /// <summary>
        /// 活动名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        [SugarColumn(ColumnName = "name", ColumnDescription = "活动名称", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 活动类型
        /// </summary>
        [SugarColumn(ColumnName = "activity_type", ColumnDescription = "活动类型", ColumnDataType = "int", IsNullable = false)]
        public int ActivityType { get; set; }

        /// <summary>
        /// 活动配置(JSON)
        /// </summary>
        [SugarColumn(ColumnName = "configuration", ColumnDescription = "活动配置", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
        public string? Configuration { get; set; }

        /// <summary>
        /// 工作流定义ID
        /// </summary>
        [SugarColumn(ColumnName = "definition_id", ColumnDescription = "工作流定义ID", ColumnDataType = "bigint", IsNullable = false)]
        public long DefinitionId { get; set; }

        /// <summary>
        /// 关联的节点模板ID
        /// </summary>
        [SugarColumn(ColumnName = "node_template_id", ColumnDescription = "节点模板ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? NodeTemplateId { get; set; }

        /// <summary>
        /// 工作流定义
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(DefinitionId))]
        public virtual HbtDefinition? WorkflowDefinition { get; set; }

        /// <summary>
        /// 关联的节点模板
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(NodeTemplateId))]
        public virtual HbtNodeTemplate? NodeTemplate { get; set; }

        /// <summary>
        /// 出站转换列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtTransition.SourceActivityId))]
        public List<HbtTransition>? OutgoingTransitions { get; set; }

        /// <summary>
        /// 入站转换列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtTransition.TargetActivityId))]
        public List<HbtTransition>? IncomingTransitions { get; set; }
    }
}
