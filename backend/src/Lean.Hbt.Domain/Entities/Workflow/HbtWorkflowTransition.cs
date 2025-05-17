#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowTransition.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流转换实体
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Workflow
{
    /// <summary>
    /// 工作流转换实体
    /// </summary>
    [SugarTable("hbt_workflow_transition", "工作流转换表")]
    public class HbtWorkflowTransition : HbtBaseEntity
    {
        /// <summary>
        /// 源节点ID
        /// </summary>
        [SugarColumn(ColumnName = "source_node_id", ColumnDescription = "源节点ID", ColumnDataType = "bigint", IsNullable = false)]
        public long SourceNodeId { get; set; }

        /// <summary>
        /// 目标节点ID
        /// </summary>
        [SugarColumn(ColumnName = "target_node_id", ColumnDescription = "目标节点ID", ColumnDataType = "bigint", IsNullable = false)]
        public long TargetNodeId { get; set; }

        /// <summary>
        /// 转换条件(JSON格式)
        /// </summary>
        [SugarColumn(ColumnName = "condition", ColumnDescription = "转换条件(JSON格式)", ColumnDataType = "text", IsNullable = true)]
        public string? Condition { get; set; }

        /// <summary>
        /// 工作流定义ID
        /// </summary>
        [SugarColumn(ColumnName = "workflow_definition_id", ColumnDescription = "工作流定义ID", ColumnDataType = "bigint", IsNullable = false)]
        public long WorkflowDefinitionId { get; set; }
        /// <summary>
        /// 租户ID
        /// </summary>
        [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long TenantId { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(TenantId))]
        public HbtTenant? Tenant { get; set; }

        /// <summary>
        /// 源节点
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(SourceNodeId))]
        public HbtWorkflowNode? SourceNode { get; set; }

        /// <summary>
        /// 目标节点
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(TargetNodeId))]
        public HbtWorkflowNode? TargetNode { get; set; }

        /// <summary>
        /// 工作流定义
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(WorkflowDefinitionId))]
        public HbtWorkflowDefinition? WorkflowDefinition { get; set; }
    }
}