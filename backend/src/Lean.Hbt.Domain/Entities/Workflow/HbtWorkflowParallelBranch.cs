#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowParallelBranch.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流并行分支状态实体
//===================================================================

using System;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Workflow
{
    /// <summary>
    /// 工作流并行分支状态实体
    /// </summary>
    [SugarTable("hbt_wf_parallel_branch", "工作流并行分支状态表")]
    public class HbtWorkflowParallelBranch : HbtBaseEntity
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        [SugarColumn(ColumnName = "workflow_instance_id", ColumnDescription = "工作流实例ID", ColumnDataType = "bigint", IsNullable = false)]
        public long WorkflowInstanceId { get; set; }

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
        /// 是否完成
        /// </summary>
        [SugarColumn(ColumnName = "is_completed", ColumnDescription = "是否完成", ColumnDataType = "bit", IsNullable = false)]
        public bool IsCompleted { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        [SugarColumn(ColumnName = "complete_time", ColumnDescription = "完成时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? CompleteTime { get; set; }

        /// <summary>
        /// 工作流实例
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(WorkflowInstanceId))]
        public HbtWorkflowInstance WorkflowInstance { get; set; }

        /// <summary>
        /// 并行节点
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ParallelNodeId))]
        public HbtWorkflowNode ParallelNode { get; set; }

        /// <summary>
        /// 分支转换
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(BranchTransitionId))]
        public HbtWorkflowTransition BranchTransition { get; set; }
    }
} 