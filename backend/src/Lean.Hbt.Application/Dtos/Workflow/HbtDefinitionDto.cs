//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDefinitionDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流定义数据传输对象
//===================================================================

namespace Lean.Hbt.Application.Dtos.Workflow
{


    /// <summary>
    /// 工作流定义基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtDefinitionDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDefinitionDto()
        {
            WorkflowName = string.Empty;
            WorkflowCategory = string.Empty;
            WorkflowVersion = "A";
            WorkflowConfig = string.Empty;
            WorkflowNodes = new List<HbtNodeDto>();
            Remark = string.Empty;
            CreateBy = string.Empty;
            CreateTime = DateTime.Now;
            UpdateBy = string.Empty;
            UpdateTime = null;
            IsDeleted = 0;
            DeleteBy = string.Empty;
            DeleteTime = null;
        }

        /// <summary>
        /// 工作流ID
        /// </summary>
        [AdaptMember("Id")]
        public long DefinitionId { get; set; }

        /// <summary>
        /// 工作流名称
        /// </summary>
        public string WorkflowName { get; set; }

        /// <summary>
        /// 工作流分类
        /// </summary>
        public string WorkflowCategory { get; set; }

        /// <summary>
        /// 工作流版本
        /// </summary>
        public string WorkflowVersion { get; set; }

        /// <summary>
        /// 表单配置
        /// </summary>
        public int FormId { get; set; }

        /// <summary>
        /// 工作流配置
        /// </summary>
        public string WorkflowConfig { get; set; }

        /// <summary>
        /// 工作流状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 工作流节点列表
        /// </summary>
        public List<HbtNodeDto> WorkflowNodes { get; set; }

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
    /// 工作流定义查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtDefinitionQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 工作流名称
        /// </summary>
        public string? WorkflowName { get; set; }

        /// <summary>
        /// 工作流分类
        /// </summary>
        public string? WorkflowCategory { get; set; }

        /// <summary>
        /// 工作流状态
        /// </summary>
        public int? WorkflowStatus { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// 工作流定义创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtDefinitionCreateDto
    {
        /// <summary>
        /// 工作流名称
        /// </summary>
        public string? WorkflowName { get; set; }

        /// <summary>
        /// 工作流分类
        /// </summary>
        public string? WorkflowCategory { get; set; }

        /// <summary>
        /// 表单配置
        /// </summary>
        public int FormId { get; set; }

        /// <summary>
        /// 工作流配置
        /// </summary>
        public string? WorkflowConfig { get; set; }

        /// <summary>
        /// 工作流节点列表
        /// </summary>
        public List<HbtNodeCreateDto>? WorkflowNodes { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工作流定义更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtDefinitionUpdateDto : HbtDefinitionCreateDto
    {
        /// <summary>
        /// 工作流ID
        /// </summary>
        [AdaptMember("Id")]
        public long DefinitionId { get; set; }

    }
    /// <summary>
    /// 工作流定义状态DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtDefinitionStatusDto
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>
        [AdaptMember("Id")]
        public long DefinitionId { get; set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public int Status { get; set; }

    }
    /// <summary>
    /// 工作流定义导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtDefinitionImportDto
    {
        /// <summary>
        /// 工作流名称
        /// </summary>
        public string? WorkflowName { get; set; }

        /// <summary>
        /// 工作流分类
        /// </summary>
        public string? WorkflowCategory { get; set; }

        /// <summary>
        /// 工作流版本
        /// </summary>
        public string? WorkflowVersion { get; set; }

        /// <summary>
        /// 表单配置
        /// </summary>
        public int FormId { get; set; }

        /// <summary>
        /// 工作流配置
        /// </summary>
        public string? WorkflowConfig { get; set; }

        /// <summary>
        /// 工作流状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工作流定义导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtDefinitionExportDto
    {
        /// <summary>
        /// 工作流名称
        /// </summary>
        public string? WorkflowName { get; set; }

        /// <summary>
        /// 工作流分类
        /// </summary>
        public string? WorkflowCategory { get; set; }

        /// <summary>
        /// 工作流版本
        /// </summary>
        public string? WorkflowVersion { get; set; }

        /// <summary>
        /// 表单配置
        /// </summary>
        public int FormId { get; set; }

        /// <summary>
        /// 工作流配置
        /// </summary>
        public string? WorkflowConfig { get; set; }

        /// <summary>
        /// 工作流状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

    }

    /// <summary>
    /// 工作流定义模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtDefinitionTemplateDto
    {
        /// <summary>
        /// 工作流名称
        /// </summary>
        public string? WorkflowName { get; set; }

        /// <summary>
        /// 工作流分类
        /// </summary>
        public string? WorkflowCategory { get; set; }

        /// <summary>
        /// 工作流版本
        /// </summary>
        public string? WorkflowVersion { get; set; }

        /// <summary>
        /// 表单配置
        /// </summary>
        public int FormId { get; set; }

        /// <summary>
        /// 工作流配置
        /// </summary>
        public string? WorkflowConfig { get; set; }

        /// <summary>
        /// 工作流状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
}