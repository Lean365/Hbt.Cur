#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowResults.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流执行结果类
//===================================================================

using System.Collections.Generic;

namespace Lean.Hbt.Application.Services.Workflow.Engine
{
    /// <summary>
    /// 工作流节点执行结果
    /// </summary>
    public class HbtWorkflowNodeResult
    {
        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// 输出变量
        /// </summary>
        public Dictionary<string, object>? OutputVariables { get; set; }
    }

    /// <summary>
    /// 工作流转换执行结果
    /// </summary>
    public class HbtWorkflowTransitionResult
    {
        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// 目标节点ID
        /// </summary>
        public long TargetNodeId { get; set; }
    }
}