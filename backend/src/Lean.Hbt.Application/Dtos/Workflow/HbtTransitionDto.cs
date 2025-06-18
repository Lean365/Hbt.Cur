#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTransitionDto.cs
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
    public class HbtTransitionDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtTransitionDto()
        {
            TransitionId = 0;
            SourceNodeId = 0;
            TargetNodeId = 0;
            Condition = string.Empty;
            DefinitionId = 0;
        }

        /// <summary>
        /// 主键
        /// </summary>
        [AdaptMember("Id")]
        public long TransitionId { get; set; }

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
        public long DefinitionId { get; set; }
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
    /// 工作流转换查询DTO
    /// </summary>
    public class HbtTransitionQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }

        /// <summary>
        /// 源节点ID
        /// </summary>
        public long SourceNodeId { get; set; }
    }
    /// <summary>
    /// 工作流转换创建DTO
    /// </summary>
    public class HbtTransitionCreateDto
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }
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
    }
    /// <summary>
    /// 工作流转换更新DTO
    /// </summary>
    public class HbtTransitionUpdateDto : HbtTransitionCreateDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        [AdaptMember("Id")]
        public long TransitionId { get; set; }
    }
    /// <summary>
    /// 工作流转换删除DTO
    /// </summary>
    public class HbtTransitionDeleteDto
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>
        [AdaptMember("Id")]
        public long TransitionId { get; set; }
    }
    /// <summary>
    /// 工作流转换导入DTO
    /// </summary>
    public class HbtTransitionImportDto
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }
    }
    /// <summary>
    /// 工作流转换导出DTO
    /// </summary>
    public class HbtTransitionExportDto
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }
    }
    /// <summary>
    /// 工作流转换查询DTO
    /// </summary>
    public class HbtTransitionTemplateDto
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }
        /// <summary>
        /// 源节点ID
        /// </summary>
        public long SourceNodeId { get; set; }
        /// <summary>
        /// 目标节点ID
        /// </summary>
        public long TargetNodeId { get; set; }
    }
}