#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenTemplateDto.cs
// 创建者 : Lean365
// 创建时间: 2024-03-24
// 版本号 : V0.0.1
// 描述    : 代码生成模板DTO
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.Generator;

/// <summary>
/// 代码生成模板DTO
/// </summary>
public class HbtGenTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenTemplateDto()
    {
        TemplateName = string.Empty;
        TemplateContent = string.Empty;
        GenPath = string.Empty;
        FileName = string.Empty;
        CreateBy = string.Empty;
        UpdateBy = string.Empty;
    }

    /// <summary>
    /// 主键ID
    /// </summary>
    public long GenTemplateId { get; set; }

    /// <summary>
    /// 模板名称
    /// </summary>
    [Required(ErrorMessage = "模板名称不能为空")]
    [StringLength(100, ErrorMessage = "模板名称长度不能超过100个字符")]
    public string TemplateName { get; set; }

    /// <summary>
    /// 模板类型
    /// </summary>
    [Required(ErrorMessage = "模板类型不能为空")]
    public int TemplateType { get; set; } = 1;

    /// <summary>
    /// 模板分类
    /// </summary>
    [Required(ErrorMessage = "模板分类不能为空")]
    public int TemplateCategory { get; set; } = 1;

    /// <summary>
    /// 编程语言
    /// </summary>
    [Required(ErrorMessage = "编程语言不能为空")]
    public int TemplateLanguage { get; set; } = 1;

    /// <summary>
    /// 版本号
    /// </summary>
    [Required(ErrorMessage = "版本号不能为空")]
    public int TemplateVersion { get; set; } = 1;

    /// <summary>
    /// 模板内容
    /// </summary>
    [Required(ErrorMessage = "模板内容不能为空")]
    public string TemplateContent { get; set; }

    /// <summary>
    /// 生成路径
    /// </summary>
    [Required(ErrorMessage = "生成路径不能为空")]
    [StringLength(200, ErrorMessage = "生成路径长度不能超过200个字符")]
    public string GenPath { get; set; }

    /// <summary>
    /// 文件名
    /// </summary>
    [Required(ErrorMessage = "文件名不能为空")]
    [StringLength(100, ErrorMessage = "文件名长度不能超过100个字符")]
    public string FileName { get; set; }

    /// <summary>
    /// 状态（0：停用，1：正常）
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    public int Status { get; set; } = 1;

    /// <summary>
    /// 备注
    /// </summary>
    [StringLength(500, ErrorMessage = "备注长度不能超过500个字符")]
    public string? Remark { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public string CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 更新人
    /// </summary>
    public string UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }
}

/// <summary>
/// 代码生成模板查询DTO
/// </summary>
public class HbtGenTemplateQueryDto : HbtPagedQuery
{
    /// <summary>
    /// 模板名称
    /// </summary>
    [StringLength(100, ErrorMessage = "模板名称长度不能超过100个字符")]
    public string? TemplateName { get; set; }

    /// <summary>
    /// 模板类型
    /// </summary>
    public int? TemplateType { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? BeginTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }
}

/// <summary>
/// 代码生成模板创建DTO
/// </summary>
public class HbtGenTemplateCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenTemplateCreateDto()
    {
        TemplateName = string.Empty;
        TemplateContent = string.Empty;
        GenPath = string.Empty;
        FileName = string.Empty;
    }

    /// <summary>
    /// 模板名称
    /// </summary>
    [Required(ErrorMessage = "模板名称不能为空")]
    [StringLength(100, ErrorMessage = "模板名称长度不能超过100个字符")]
    public string TemplateName { get; set; }

    /// <summary>
    /// 模板类型
    /// </summary>
    [Required(ErrorMessage = "模板类型不能为空")]
    public int TemplateType { get; set; } = 1;

    /// <summary>
    /// 模板分类
    /// </summary>
    [Required(ErrorMessage = "模板分类不能为空")]
    public int TemplateCategory { get; set; } = 1;

    /// <summary>
    /// 编程语言
    /// </summary>
    [Required(ErrorMessage = "编程语言不能为空")]
    public int TemplateLanguage { get; set; } = 1;

    /// <summary>
    /// 版本号
    /// </summary>
    [Required(ErrorMessage = "版本号不能为空")]
    public int TemplateVersion { get; set; } = 1;

    /// <summary>
    /// 模板内容
    /// </summary>
    [Required(ErrorMessage = "模板内容不能为空")]
    public string TemplateContent { get; set; }

    /// <summary>
    /// 生成路径
    /// </summary>
    [Required(ErrorMessage = "生成路径不能为空")]
    [StringLength(200, ErrorMessage = "生成路径长度不能超过200个字符")]
    public string GenPath { get; set; }

    /// <summary>
    /// 文件名
    /// </summary>
    [Required(ErrorMessage = "文件名不能为空")]
    [StringLength(100, ErrorMessage = "文件名长度不能超过100个字符")]
    public string FileName { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [StringLength(500, ErrorMessage = "备注长度不能超过500个字符")]
    public string? Remark { get; set; }

    /// <summary>
    /// 状态（0：停用，1：正常）
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    public int Status { get; set; } = 1;
}

/// <summary>
/// 代码生成模板更新DTO
/// </summary>
public class HbtGenTemplateUpdateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenTemplateUpdateDto()
    {
        TemplateName = string.Empty;
        TemplateContent = string.Empty;
        GenPath = string.Empty;
        FileName = string.Empty;
    }

    /// <summary>
    /// 主键ID
    /// </summary>
    [Required(ErrorMessage = "主键ID不能为空")]
    public long Id { get; set; }

    /// <summary>
    /// 模板名称
    /// </summary>
    [Required(ErrorMessage = "模板名称不能为空")]
    [StringLength(100, ErrorMessage = "模板名称长度不能超过100个字符")]
    public string TemplateName { get; set; }

    /// <summary>
    /// 模板类型
    /// </summary>
    [Required(ErrorMessage = "模板类型不能为空")]
    public int TemplateType { get; set; } = 1;

    /// <summary>
    /// 模板分类
    /// </summary>
    [Required(ErrorMessage = "模板分类不能为空")]
    public int TemplateCategory { get; set; } = 1;

    /// <summary>
    /// 编程语言
    /// </summary>
    [Required(ErrorMessage = "编程语言不能为空")]
    public int TemplateLanguage { get; set; } = 1;

    /// <summary>
    /// 版本号
    /// </summary>
    [Required(ErrorMessage = "版本号不能为空")]
    public int TemplateVersion { get; set; } = 1;

    /// <summary>
    /// 模板内容
    /// </summary>
    [Required(ErrorMessage = "模板内容不能为空")]
    public string TemplateContent { get; set; }

    /// <summary>
    /// 生成路径
    /// </summary>
    [Required(ErrorMessage = "生成路径不能为空")]
    [StringLength(200, ErrorMessage = "生成路径长度不能超过200个字符")]
    public string GenPath { get; set; }

    /// <summary>
    /// 文件名
    /// </summary>
    [Required(ErrorMessage = "文件名不能为空")]
    [StringLength(100, ErrorMessage = "文件名长度不能超过100个字符")]
    public string FileName { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [StringLength(500, ErrorMessage = "备注长度不能超过500个字符")]
    public string? Remark { get; set; }

    /// <summary>
    /// 状态（0：停用，1：正常）
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    public int Status { get; set; } = 1;
}

/// <summary>
/// 代码生成模板导入DTO
/// </summary>
public class HbtGenTemplateImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenTemplateImportDto()
    {
        TemplateName = string.Empty;
        TemplateContent = string.Empty;
        GenPath = string.Empty;
        FileName = string.Empty;
        Templates = new List<HbtGenTemplateImportDto>();
    }

    /// <summary>
    /// 模板名称
    /// </summary>
    public string TemplateName { get; set; }

    /// <summary>
    /// 模板类型
    /// </summary>
    public int TemplateType { get; set; } = 1;

    /// <summary>
    /// 模板分类
    /// </summary>
    public int TemplateCategory { get; set; } = 1;

    /// <summary>
    /// 编程语言
    /// </summary>
    public int TemplateLanguage { get; set; } = 1;

    /// <summary>
    /// 版本号
    /// </summary>
    public int TemplateVersion { get; set; } = 1;

    /// <summary>
    /// 模板内容
    /// </summary>
    public string TemplateContent { get; set; }

    /// <summary>
    /// 生成路径
    /// </summary>
    public string GenPath { get; set; }

    /// <summary>
    /// 文件名
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 状态（0：停用，1：正常）
    /// </summary>
    public int Status { get; set; } = 1;

    /// <summary>
    /// 导入的模板列表
    /// </summary>
    public List<HbtGenTemplateImportDto> Templates { get; set; }
}

/// <summary>
/// 代码生成模板导出DTO
/// </summary>
public class HbtGenTemplateExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenTemplateExportDto()
    {
        TemplateName = string.Empty;
        TemplateContent = string.Empty;
        GenPath = string.Empty;
        FileName = string.Empty;
        CreateBy = string.Empty;
        UpdateBy = string.Empty;
    }

    /// <summary>
    /// 主键ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 模板名称
    /// </summary>
    public string TemplateName { get; set; }

    /// <summary>
    /// 模板类型
    /// </summary>
    public int TemplateType { get; set; }

    /// <summary>
    /// 模板分类
    /// </summary>
    public int TemplateCategory { get; set; }

    /// <summary>
    /// 编程语言
    /// </summary>
    public int TemplateLanguage { get; set; }

    /// <summary>
    /// 版本号
    /// </summary>
    public int TemplateVersion { get; set; }

    /// <summary>
    /// 模板内容
    /// </summary>
    public string TemplateContent { get; set; }

    /// <summary>
    /// 生成路径
    /// </summary>
    public string GenPath { get; set; }

    /// <summary>
    /// 文件名
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 状态（0：停用，1：正常）
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public string CreateBy { get; set; }

    /// <summary>
    /// 更新人
    /// </summary>
    public string UpdateBy { get; set; }
}

/// <summary>
/// 代码生成模板模板DTO
/// </summary>
public class HbtGenTemplateTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenTemplateTemplateDto()
    {
        TemplateName = string.Empty;
        TemplateContent = string.Empty;
        GenPath = string.Empty;
        FileName = string.Empty;
        Status = "1";
    }

    /// <summary>
    /// 模板名称
    /// </summary>
    public string TemplateName { get; set; }

    /// <summary>
    /// 模板类型
    /// </summary>
    public int TemplateType { get; set; } = 1;

    /// <summary>
    /// 模板分类
    /// </summary>
    public int TemplateCategory { get; set; } = 1;

    /// <summary>
    /// 编程语言
    /// </summary>
    public int TemplateLanguage { get; set; } = 1;

    /// <summary>
    /// 版本号
    /// </summary>
    public int TemplateVersion { get; set; } = 1;

    /// <summary>
    /// 模板内容
    /// </summary>
    public string TemplateContent { get; set; }

    /// <summary>
    /// 生成路径
    /// </summary>
    public string GenPath { get; set; }

    /// <summary>
    /// 文件名
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 状态（0：停用，1：正常）
    /// </summary>
    public string Status { get; set; }
}

/// <summary>
/// 代码生成模板状态DTO
/// </summary>
public class HbtGenTemplateStatusDto
{
    /// <summary>
    /// 模板ID
    /// </summary>
    [Required(ErrorMessage = "模板ID不能为空")]
    public long TemplateId { get; set; }

    /// <summary>
    /// 状态（0：停用，1：正常）
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    public int Status { get; set; }
}