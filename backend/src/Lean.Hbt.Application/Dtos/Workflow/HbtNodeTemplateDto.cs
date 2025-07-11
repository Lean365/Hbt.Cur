#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtNodeTemplateDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流节点模板DTO
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.Workflow
{
    /// <summary>
    /// 工作流节点模板DTO
    /// </summary>
    public class HbtNodeTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtNodeTemplateDto()
        {
            NodeTemplateId = 0;
            NodeName = string.Empty;
            NodeConfig = string.Empty;
            DefinitionId = 0;
            OrderNum = 0;
            IsEnabled = true;
        }

        /// <summary>
        /// 主键
        /// </summary>
        [AdaptMember("Id")]
        public long NodeTemplateId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string? NodeName { get; set; }

        /// <summary>
        /// 节点类型(1=开始节点 2=审批节点 3=条件节点 4=并行节点 5=结束节点)
        /// </summary>
        public int NodeType { get; set; }

        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }

        /// <summary>
        /// 工作流定义名称
        /// </summary>
        public string? DefinitionName { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public long? ParentNodeId { get; set; }

        /// <summary>
        /// 父节点名称
        /// </summary>
        public string? ParentNodeName { get; set; }

        /// <summary>
        /// 节点配置(JSON格式)
        /// </summary>
        public string? NodeConfig { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 工作流定义
        /// </summary>
        public HbtDefinitionDto? WorkflowDefinition { get; set; }

        /// <summary>
        /// 子节点模板列表
        /// </summary>
        public List<HbtNodeTemplateDto>? ChildNodeTemplates { get; set; }

        /// <summary>
        /// 关联的工作流活动列表
        /// </summary>
        public List<HbtActivityDto>? WorkflowActivities { get; set; }

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
    /// 工作流节点模板查询DTO
    /// </summary>
    public class HbtNodeTemplateQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long? DefinitionId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string? NodeName { get; set; }

        /// <summary>
        /// 节点类型
        /// </summary>
        public int? NodeType { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public long? ParentNodeId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnabled { get; set; }
    }

    /// <summary>
    /// 工作流节点模板创建DTO
    /// </summary>
    public class HbtNodeTemplateCreateDto
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string? NodeName { get; set; }

        /// <summary>
        /// 节点类型(1=开始节点 2=审批节点 3=条件节点 4=并行节点 5=结束节点)
        /// </summary>
        public int NodeType { get; set; }

        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public long? ParentNodeId { get; set; }

        /// <summary>
        /// 节点配置(JSON格式)
        /// </summary>
        [Required]
        public string? NodeConfig { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工作流节点模板更新DTO
    /// </summary>
    public class HbtNodeTemplateUpdateDto : HbtNodeTemplateCreateDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        [AdaptMember("Id")]
        public long NodeTemplateId { get; set; }
    }

    /// <summary>
    /// 工作流节点模板删除DTO
    /// </summary>
    public class HbtNodeTemplateDeleteDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        [AdaptMember("Id")]
        public long NodeTemplateId { get; set; }
    }

    /// <summary>
    /// 工作流节点模板导入DTO
    /// </summary>
    public class HbtNodeTemplateImportDto
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string? NodeName { get; set; }

        /// <summary>
        /// 节点类型
        /// </summary>
        public int NodeType { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public long? ParentNodeId { get; set; }

        /// <summary>
        /// 节点配置(JSON格式)
        /// </summary>
        public string? NodeConfig { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工作流节点模板导出DTO
    /// </summary>
    public class HbtNodeTemplateExportDto
    {
        /// <summary>
        /// 工作流定义名称
        /// </summary>
        public string? DefinitionName { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string? NodeName { get; set; }

        /// <summary>
        /// 节点类型名称
        /// </summary>
        public string NodeTypeName { get; set; } = string.Empty;

        /// <summary>
        /// 父节点名称
        /// </summary>
        public string? ParentNodeName { get; set; }

        /// <summary>
        /// 节点配置(JSON格式)
        /// </summary>
        public string? NodeConfig { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public string IsEnabledName { get; set; } = string.Empty;

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