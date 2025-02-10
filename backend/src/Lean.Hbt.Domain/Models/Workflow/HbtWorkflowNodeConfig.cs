#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowNodeConfig.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流节点配置
//===================================================================

using System.Collections.Generic;
using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.Domain.Models.Workflow
{
    /// <summary>
    /// 工作流节点配置
    /// </summary>
    public class HbtWorkflowNodeConfig
    {
        /// <summary>
        /// 审批人类型
        /// </summary>
        public HbtWorkflowApproverType ApproverType { get; set; }

        /// <summary>
        /// 指定审批人ID列表
        /// </summary>
        public List<long>? ApproverIds { get; set; }

        /// <summary>
        /// 指定角色ID列表
        /// </summary>
        public List<long>? RoleIds { get; set; }

        /// <summary>
        /// 指定部门ID列表
        /// </summary>
        public List<long>? DepartmentIds { get; set; }

        /// <summary>
        /// 审批规则
        /// </summary>
        public HbtWorkflowApprovalRule ApprovalRule { get; set; }

        /// <summary>
        /// 审批数量(当ApprovalRule为Count时有效)
        /// </summary>
        public int? ApprovalCount { get; set; }

        /// <summary>
        /// 自定义审批条件表达式
        /// </summary>
        public string? CustomCondition { get; set; }

        /// <summary>
        /// 允许多分支(仅对分支节点有效)
        /// </summary>
        public bool AllowMultipleBranches { get; set; }

        /// <summary>
        /// 并行分支数量(仅对并行节点有效)
        /// </summary>
        public int? ParallelBranchCount { get; set; }

        /// <summary>
        /// 汇聚类型(仅对汇聚节点有效)
        /// </summary>
        public HbtWorkflowJoinType? JoinType { get; set; }

        /// <summary>
        /// 汇聚条件(当JoinType为Custom时有效)
        /// </summary>
        public string? JoinCondition { get; set; }

        /// <summary>
        /// 超时时间(小时)
        /// </summary>
        public int? TimeoutHours { get; set; }

        /// <summary>
        /// 超时通知
        /// </summary>
        public bool TimeoutNotify { get; set; }

        /// <summary>
        /// 超时动作
        /// </summary>
        public HbtWorkflowTimeoutAction? TimeoutAction { get; set; }

        /// <summary>
        /// 自定义属性
        /// </summary>
        public Dictionary<string, object>? CustomProperties { get; set; }
    }
} 