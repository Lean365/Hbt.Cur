#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTransition.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流转换实体
//===================================================================

namespace Lean.Hbt.Domain.Entities.Workflow
{
    /// <summary>
    /// 工作流转换实体
    /// </summary>
    [SugarTable("hbt_workflow_transition", "工作流转换表")]
    [SugarIndex("ix_workflow_transition_definition", nameof(DefinitionId), OrderByType.Asc)]
    [SugarIndex("ix_workflow_transition_source", nameof(SourceActivityId), OrderByType.Asc)]
    [SugarIndex("ix_workflow_transition_target", nameof(TargetActivityId), OrderByType.Asc)]
    public class HbtTransition : HbtBaseEntity
    {
        /// <summary>
        /// 源活动ID
        /// </summary>
        [SugarColumn(ColumnName = "source_activity_id", ColumnDescription = "源活动ID", ColumnDataType = "bigint", IsNullable = false)]
        public long SourceActivityId { get; set; }

        /// <summary>
        /// 目标活动ID
        /// </summary>
        [SugarColumn(ColumnName = "target_activity_id", ColumnDescription = "目标活动ID", ColumnDataType = "bigint", IsNullable = false)]
        public long TargetActivityId { get; set; }

        /// <summary>
        /// 转换条件(JSON格式)
        /// </summary>
        [SugarColumn(ColumnName = "condition", ColumnDescription = "转换条件", ColumnDataType = "text", IsNullable = true)]
        public string? Condition { get; set; }

        /// <summary>
        /// 工作流定义ID
        /// </summary>
        [SugarColumn(ColumnName = "workflow_definition_id", ColumnDescription = "工作流定义ID", ColumnDataType = "bigint", IsNullable = false)]
        public long DefinitionId { get; set; }

        /// <summary>
        /// 转换名称
        /// </summary>
        [SugarColumn(ColumnName = "transition_name", ColumnDescription = "转换名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TransitionName { get; set; }

        /// <summary>
        /// 转换类型(1=自动 2=手动 3=条件)
        /// </summary>
        [SugarColumn(ColumnName = "transition_type", ColumnDescription = "转换类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int TransitionType { get; set; } = 1;

        /// <summary>
        /// 源活动
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(SourceActivityId))]
        public HbtActivity? SourceActivity { get; set; }

        /// <summary>
        /// 目标活动
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(TargetActivityId))]
        public HbtActivity? TargetActivity { get; set; }

        /// <summary>
        /// 工作流定义
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(DefinitionId))]
        public HbtDefinition? WorkflowDefinition { get; set; }

        /// <summary>
        /// 并行分支列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtParallelBranch.BranchTransitionId))]
        public List<HbtParallelBranch>? ParallelBranches { get; set; }
    }
}