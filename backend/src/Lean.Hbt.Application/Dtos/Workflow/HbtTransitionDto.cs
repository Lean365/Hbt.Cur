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
            SourceActivityId = 0;
            TargetActivityId = 0;
            Condition = string.Empty;
            DefinitionId = 0;
        }

        /// <summary>
        /// 主键
        /// </summary>
        [AdaptMember("Id")]
        public long TransitionId { get; set; }

        /// <summary>
        /// 源活动ID
        /// </summary>
        public long SourceActivityId { get; set; }

        /// <summary>
        /// 源活动名称
        /// </summary>
        public string SourceActivityName { get; set; } = string.Empty;

        /// <summary>
        /// 目标活动ID
        /// </summary>
        public long TargetActivityId { get; set; }

        /// <summary>
        /// 目标活动名称
        /// </summary>
        public string TargetActivityName { get; set; } = string.Empty;

        /// <summary>
        /// 转换条件(JSON)
        /// </summary>
        public string? Condition { get; set; }

        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }

        /// <summary>
        /// 工作流定义名称
        /// </summary>
        public string DefinitionName { get; set; } = string.Empty;

        /// <summary>
        /// 转换名称
        /// </summary>
        public string? TransitionName { get; set; }

        /// <summary>
        /// 转换类型(1=自动 2=手动 3=条件)
        /// </summary>
        public int TransitionType { get; set; } = 1;

        /// <summary>
        /// 源活动
        /// </summary>
        public HbtActivityDto? SourceActivity { get; set; }

        /// <summary>
        /// 目标活动
        /// </summary>
        public HbtActivityDto? TargetActivity { get; set; }

        /// <summary>
        /// 工作流定义
        /// </summary>
        public HbtDefinitionDto? WorkflowDefinition { get; set; }

        /// <summary>
        /// 并行分支列表
        /// </summary>
        public List<HbtParallelBranchDto>? ParallelBranches { get; set; }

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
        /// 源活动ID
        /// </summary>
        public long SourceActivityId { get; set; }
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
        /// 源活动ID
        /// </summary>
        public long SourceActivityId { get; set; }

        /// <summary>
        /// 目标活动ID  
        /// </summary>
        public long TargetActivityId { get; set; }

        /// <summary>
        /// 转换条件(JSON)
        /// </summary>
        public string? Condition { get; set; }

        /// <summary>
        /// 转换名称
        /// </summary>
        public string? TransitionName { get; set; }

        /// <summary>
        /// 转换类型(1=自动 2=手动 3=条件)
        /// </summary>
        public int TransitionType { get; set; } = 1;
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
        /// 主键
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
    /// 工作流转换模板DTO
    /// </summary>
    public class HbtTransitionTemplateDto
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }

        /// <summary>
        /// 源活动ID
        /// </summary>
        public long SourceActivityId { get; set; }

        /// <summary>
        /// 目标活动ID
        /// </summary>
        public long TargetActivityId { get; set; }
    }

    /// <summary>
    /// 工作流转换执行DTO
    /// </summary>
    public class HbtTransitionExecuteDto
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long InstanceId { get; set; }

        /// <summary>
        /// 转换ID
        /// </summary>
        public long TransitionId { get; set; }

        /// <summary>
        /// 工作流变量
        /// </summary>
        public Dictionary<string, object>? Variables { get; set; }
    }
}