#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtParallelBranch.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流并行分支状态实体
//===================================================================

namespace Lean.Hbt.Domain.Entities.Workflow
{
    /// <summary>
    /// 工作流并行分支状态实体
    /// </summary>
    [SugarTable("hbt_workflow_parallel_branch", "工作流并行分支状态表")]
    [SugarIndex("ix_workflow_parallel_branch_instance", nameof(InstanceId), OrderByType.Asc)]
    [SugarIndex("ix_workflow_parallel_branch_node", nameof(ParallelNodeId), OrderByType.Asc)]
    [SugarIndex("ix_workflow_parallel_branch_transition", nameof(BranchTransitionId), OrderByType.Asc)]
    public class HbtParallelBranch : HbtBaseEntity
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        [SugarColumn(ColumnName = "instance_id", ColumnDescription = "工作流实例ID", ColumnDataType = "bigint", IsNullable = false)]
        public long InstanceId { get; set; }

        /// <summary>
        /// 并行节点ID
        /// </summary>
        [SugarColumn(ColumnName = "parallel_node_id", ColumnDescription = "并行节点ID", ColumnDataType = "bigint", IsNullable = false)]
        public long ParallelNodeId { get; set; }

        /// <summary>
        /// 分支转换ID
        /// </summary>
        [SugarColumn(ColumnName = "branch_transition_id", ColumnDescription = "分支转换ID", ColumnDataType = "bigint", IsNullable = false)]
        public long BranchTransitionId { get; set; }

        /// <summary>
        /// 关联的节点模板ID
        /// </summary>
        [SugarColumn(ColumnName = "node_template_id", ColumnDescription = "节点模板ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? NodeTemplateId { get; set; }

        /// <summary>
        /// 分支名称
        /// </summary>
        [SugarColumn(ColumnName = "branch_name", ColumnDescription = "分支名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BranchName { get; set; }

        /// <summary>
        /// 是否完成(0:未完成 1:已完成 )
        /// </summary>
        [SugarColumn(ColumnName = "is_completed", ColumnDescription = "是否完成", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsCompleted { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        [SugarColumn(ColumnName = "complete_time", ColumnDescription = "完成时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? CompleteTime { get; set; }

        /// <summary>
        /// 工作流实例
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(InstanceId))]
        public HbtInstance? WorkflowInstance { get; set; }

        /// <summary>
        /// 并行节点
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ParallelNodeId))]
        public HbtNode? ParallelNode { get; set; }

        /// <summary>
        /// 分支转换
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(BranchTransitionId))]
        public HbtTransition? BranchTransition { get; set; }

        /// <summary>
        /// 关联的节点模板
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(NodeTemplateId))]
        public HbtNodeTemplate? NodeTemplate { get; set; }
    }
}