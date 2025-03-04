#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowActivityDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流活动DTO
//===================================================================

using System.ComponentModel.DataAnnotations;
using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.Application.Dtos.Workflow
{
    /// <summary>
    /// 工作流活动DTO
    /// </summary>
    public class HbtWorkflowActivityDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        [AdaptMember("Id")]
        public long WorkflowActivityId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 活动类型
        /// </summary>
        public HbtWorkflowActivityType Type { get; set; }

        /// <summary>
        /// 活动配置(JSON)
        /// </summary>
        public string? Configuration { get; set; }

        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long WorkflowDefinitionId { get; set; }
    }
}