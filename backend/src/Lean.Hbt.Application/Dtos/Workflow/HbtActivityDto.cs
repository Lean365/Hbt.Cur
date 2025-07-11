#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtActivityDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流活动DTO
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.Workflow
{
    /// <summary>
    /// 工作流活动DTO
    /// </summary>
    public class HbtActivityDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtActivityDto()
        {
            Name = string.Empty;
            Configuration = string.Empty;
            ActivityId = 0;
            ActivityType = 0;
            DefinitionId = 0;
        }

        /// <summary>
        /// 主键
        /// </summary>
        [AdaptMember("Id")]
        public long ActivityId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 活动类型
        /// </summary>
        public int ActivityType { get; set; }

        /// <summary>
        /// 活动配置(JSON)
        /// </summary>
        public string? Configuration { get; set; }

        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }

        /// <summary>
        /// 工作流定义名称
        /// </summary>
        public string? DefinitionName { get; set; }

        /// <summary>
        /// 关联的节点模板ID
        /// </summary>
        public long? NodeTemplateId { get; set; }

        /// <summary>
        /// 关联的节点模板名称
        /// </summary>
        public string? NodeTemplateName { get; set; }

        /// <summary>
        /// 工作流定义
        /// </summary>
        public HbtDefinitionDto? WorkflowDefinition { get; set; }

        /// <summary>
        /// 关联的节点模板
        /// </summary>
        public HbtNodeTemplateDto? NodeTemplate { get; set; }

        /// <summary>
        /// 出站转换列表
        /// </summary>
        public List<HbtTransitionDto>? OutgoingTransitions { get; set; }

        /// <summary>
        /// 入站转换列表
        /// </summary>
        public List<HbtTransitionDto>? IncomingTransitions { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否删除（0未删除 1已删除）
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string? DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
    }

    /// <summary>
    /// 工作流活动查询DTO
    /// </summary>
    public class HbtActivityQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long? DefinitionId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string? ActivityName { get; set; }

        /// <summary>
        /// 活动类型
        /// </summary>
        public int? ActivityType { get; set; }

        /// <summary>
        /// 节点模板ID
        /// </summary>
        public long? NodeTemplateId { get; set; }
    }

    /// <summary>
    /// 工作流活动创建DTO
    /// </summary>
    public class HbtActivityCreateDto
    {
        /// <summary>
        /// 活动名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 活动类型
        /// </summary>
        public int ActivityType { get; set; }

        /// <summary>
        /// 活动配置(JSON)
        /// </summary>
        public string? Configuration { get; set; }

        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }

        /// <summary>
        /// 关联的节点模板ID
        /// </summary>
        public long? NodeTemplateId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工作流活动更新DTO
    /// </summary>
    public class HbtActivityUpdateDto : HbtActivityCreateDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        [AdaptMember("Id")]
        public long ActivityId { get; set; }
    }

    /// <summary>
    /// 工作流活动导入DTO
    /// </summary>
    public class HbtActivityImportDto
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 活动类型
        /// </summary>
        public int ActivityType { get; set; }

        /// <summary>
        /// 活动配置(JSON)
        /// </summary>
        public string? Configuration { get; set; }

        /// <summary>
        /// 关联的节点模板ID
        /// </summary>
        public long? NodeTemplateId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工作流活动导出DTO
    /// </summary>
    public class HbtActivityExportDto
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 活动类型
        /// </summary>
        public string ActivityTypeName { get; set; } = string.Empty;

        /// <summary>
        /// 关联的节点模板名称
        /// </summary>
        public string? NodeTemplateName { get; set; }

        /// <summary>
        /// 活动配置(JSON)
        /// </summary>
        public string? Configuration { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}