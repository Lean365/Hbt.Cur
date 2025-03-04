#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowTransitionDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流转换DTO
//===================================================================

namespace Lean.Hbt.Application.Dtos.Workflow
{
    /// <summary>
    /// 工作流转换DTO
    /// </summary>
    public class HbtWorkflowTransitionDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        [AdaptMember("Id")]
        public long WorkflowTransitionId { get; set; }

        /// <summary>
        /// 源节点ID
        /// </summary>
        public long SourceNodeId { get; set; }

        /// <summary>
        /// 目标节点ID
        /// </summary>
        public long TargetNodeId { get; set; }

        /// <summary>
        /// 转换条件(JSON)
        /// </summary>
        public string? Condition { get; set; }

        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long WorkflowDefinitionId { get; set; }
    }
}