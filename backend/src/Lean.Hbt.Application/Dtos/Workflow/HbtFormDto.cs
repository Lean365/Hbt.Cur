//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtFormDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流表单数据传输对象
//===================================================================

namespace Lean.Hbt.Application.Dtos.Workflow
{
    /// <summary>
    /// 工作流表单基础DTO
    /// </summary>
    public class HbtFormDto
    {
        /// <summary>
        /// 构造函数，初始化string类型字段
        /// </summary>
        public HbtFormDto()
        {
            FormName = string.Empty;
            FormDesc = string.Empty;
            FormConfig = string.Empty;
            FormVersion = string.Empty;
            CreateBy = string.Empty;
            UpdateBy = string.Empty;
            DeleteBy = string.Empty;
        }
        /// <summary>
        /// 表单ID
        /// </summary>
        [AdaptMember("Id")]
        public long FormId { get; set; }
        /// <summary>
        /// 表单名称
        /// </summary>
        public string FormName { get; set; }
        /// <summary>
        /// 表单描述
        /// </summary>
        public string? FormDesc { get; set; }
        /// <summary>
        /// 表单分类
        /// </summary>
        public int FormCategory { get; set; }
        /// <summary>
        /// 表单配置
        /// </summary>
        public string FormConfig { get; set; }
        /// <summary>
        /// 关联的工作流定义ID
        /// </summary>
        public long? DefinitionId { get; set; }
        /// <summary>
        /// 关联的工作流定义名称
        /// </summary>
        public string? DefinitionName { get; set; }
        /// <summary>
        /// 表单版本
        /// </summary>
        public string FormVersion { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
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
        /// 是否删除
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
    /// 工作流表单查询DTO
    /// </summary>
    public class HbtFormQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数，初始化string类型字段
        /// </summary>
        public HbtFormQueryDto()
        {
            FormName = string.Empty;
        }
        /// <summary>
        /// 关联的工作流定义ID
        /// </summary>
        public long? DefinitionId { get; set; }
        /// <summary>
        /// 表单名称
        /// </summary>
        public string? FormName { get; set; }
        /// <summary>
        /// 表单分类
        /// </summary>
        public int? FormCategory { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }
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
    /// 工作流表单创建DTO
    /// </summary>
    public class HbtFormCreateDto
    {
        /// <summary>
        /// 构造函数，初始化string类型字段
        /// </summary>
        public HbtFormCreateDto()
        {
            FormCategory = 0;
            FormVersion = string.Empty;
            FormName = string.Empty;
            FormDesc = string.Empty;
            FormConfig = string.Empty;
            Remark = string.Empty;
        }
        /// <summary>
        /// 表单名称
        /// </summary>
        public string FormName { get; set; }
        /// <summary>
        /// 表单描述
        /// </summary>
        public string? FormDesc { get; set; }
        /// <summary>
        /// 表单分类
        /// </summary>
        public int FormCategory { get; set; }
        /// <summary>
        /// 表单配置
        /// </summary>
        public string FormConfig { get; set; }
        /// <summary>
        /// 关联的工作流定义ID
        /// </summary>
        public long? DefinitionId { get; set; }
        /// <summary>
        /// 表单版本
        /// </summary>
        public string FormVersion { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工作流表单更新DTO
    /// </summary>
    public class HbtFormUpdateDto : HbtFormCreateDto
    {
        /// <summary>
        /// 表单ID
        /// </summary>
        [AdaptMember("Id")]
        public long FormId { get; set; }
    }

    /// <summary>
    /// 工作流表单导入DTO
    /// </summary>
    public class HbtFormImportDto
    {
        /// <summary>
        /// 构造函数，初始化string类型字段
        /// </summary>
        public HbtFormImportDto()
        {
            FormName = string.Empty;
            FormDesc = string.Empty;
            FormConfig = string.Empty;
            FormVersion = string.Empty;
        }
        /// <summary>
        /// 表单名称
        /// </summary>
        public string FormName { get; set; }
        /// <summary>
        /// 表单描述
        /// </summary>
        public string? FormDesc { get; set; }
        /// <summary>
        /// 表单分类
        /// </summary>
        public int FormCategory { get; set; }
        /// <summary>
        /// 表单配置
        /// </summary>
        public string FormConfig { get; set; }
        /// <summary>
        /// 表单版本
        /// </summary>
        public string FormVersion { get; set; }
        /// <summary>
        /// 关联的工作流定义ID
        /// </summary>
        public long? DefinitionId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 工作流表单导出DTO
    /// </summary>
    public class HbtFormExportDto
    {
        /// <summary>
        /// 构造函数，初始化string类型字段
        /// </summary>
        public HbtFormExportDto()
        {
            FormName = string.Empty;
            FormDesc = string.Empty;
            FormConfig = string.Empty;

        }
        /// <summary>
        /// 表单名称
        /// </summary>
        public string FormName { get; set; }
        /// <summary>
        /// 表单描述
        /// </summary>
        public string? FormDesc { get; set; }
        /// <summary>
        /// 表单分类
        /// </summary>
        public int FormCategory { get; set; }
        /// <summary>
        /// 表单配置
        /// </summary>
        public string FormConfig { get; set; }
        /// <summary>
        /// 表单版本
        /// </summary>
        public string FormVersion { get; set; }
        /// <summary>
        /// 关联的工作流定义ID
        /// </summary>
        public long? DefinitionId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

    }

    /// <summary>
    /// 工作流表单模板DTO
    /// </summary>
    public class HbtFormTemplateDto
    {
        /// <summary>
        /// 构造函数，初始化string类型字段
        /// </summary>
        public HbtFormTemplateDto()
        {
            FormName = "示例表单";
            FormDesc = "这是一个表单模板";
            FormConfig = "{}";
        }
        /// <summary>
        /// 表单名称
        /// </summary>
        public string FormName { get; set; }
        /// <summary>
        /// 表单描述
        /// </summary>
        public string? FormDesc { get; set; }
        /// <summary>
        /// 表单分类
        /// </summary>
        public int FormCategory { get; set; }
        /// <summary>
        /// 表单配置
        /// </summary>
        public string FormConfig { get; set; }
        /// <summary>
        /// 表单版本
        /// </summary>
        public string FormVersion { get; set; }
        /// <summary>
        /// 关联的工作流定义ID
        /// </summary>
        public long? DefinitionId { get; set; } 
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; } = 0;
    }
} 