#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenConfigDto.cs
// 创建者 : Lean365
// 创建时间: 2024-03-24
// 版本号 : V0.0.1
// 描述    : 代码生成配置DTO
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.Generator;

/// <summary>
/// 代码生成配置DTO
/// </summary>
public class HbtGenConfigDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenConfigDto()
    {
        TableName = string.Empty;
        FunctionName = string.Empty;
        ModuleName = string.Empty;
        BusinessName = string.Empty;
        PackageName = string.Empty;
        Author = string.Empty;
        GenTemplate = string.Empty;
        GenPath = string.Empty;
        Options = string.Empty;
        CreateBy = string.Empty;
        UpdateBy = string.Empty;
    }

    /// <summary>
    /// 主键ID
    /// </summary>
    public long ConfigId { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    [Required(ErrorMessage = "表名不能为空")]
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string TableName { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    [Required(ErrorMessage = "作者不能为空")]
    [StringLength(50, ErrorMessage = "作者长度不能超过50个字符")]
    public string Author { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    [Required(ErrorMessage = "模块名称不能为空")]
    [StringLength(50, ErrorMessage = "模块名称长度不能超过50个字符")]
    public string ModuleName { get; set; }

    /// <summary>
    /// 包名
    /// </summary>
    [Required(ErrorMessage = "包名不能为空")]
    [StringLength(100, ErrorMessage = "包名长度不能超过100个字符")]
    public string PackageName { get; set; }

    /// <summary>
    /// 业务名称
    /// </summary>
    [Required(ErrorMessage = "业务名称不能为空")]
    [StringLength(50, ErrorMessage = "业务名称长度不能超过50个字符")]
    public string BusinessName { get; set; }

    /// <summary>
    /// 功能名称
    /// </summary>
    [Required(ErrorMessage = "功能名称不能为空")]
    [StringLength(50, ErrorMessage = "功能名称长度不能超过50个字符")]
    public string FunctionName { get; set; }

    /// <summary>
    /// 生成类型（1：单表，2：主从表）
    /// </summary>
    [Required(ErrorMessage = "生成类型不能为空")]
    public int GenType { get; set; } = 1;

    /// <summary>
    /// 生成模板
    /// </summary>
    public string GenTemplate { get; set; }

    /// <summary>
    /// 生成路径
    /// </summary>
    public string GenPath { get; set; }

    /// <summary>
    /// 生成选项
    /// </summary>

    public string Options { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    public long? TenantId { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public string CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新人
    /// </summary>
    public string UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime UpdateTime { get; set; }
}

/// <summary>
/// 代码生成配置查询DTO
/// </summary>
public class HbtGenConfigQueryDto : HbtPagedQuery
{
    /// <summary>
    /// 表名
    /// </summary>
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string? TableName { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    [StringLength(50, ErrorMessage = "模块名称长度不能超过50个字符")]
    public string? ModuleName { get; set; }

    /// <summary>
    /// 业务名称
    /// </summary>
    [StringLength(50, ErrorMessage = "业务名称长度不能超过50个字符")]
    public string? BusinessName { get; set; }

    /// <summary>
    /// 生成类型
    /// </summary>
    public int? GenType { get; set; }

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
/// 代码生成配置创建DTO
/// </summary>
public class HbtGenConfigCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenConfigCreateDto()
    {
        TableName = string.Empty;
        Author = string.Empty;
        FunctionName = string.Empty;
        ModuleName = string.Empty;
        BusinessName = string.Empty;
        PackageName = string.Empty;
        GenTemplate = string.Empty;
        GenPath = string.Empty;
        Options = string.Empty;
    }

    /// <summary>
    /// 表名
    /// </summary>
    [Required(ErrorMessage = "表名不能为空")]
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string TableName { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    [Required(ErrorMessage = "作者不能为空")]
    [StringLength(50, ErrorMessage = "作者长度不能超过50个字符")]
    public string Author { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    [Required(ErrorMessage = "模块名称不能为空")]
    [StringLength(50, ErrorMessage = "模块名称长度不能超过50个字符")]
    public string ModuleName { get; set; }

    /// <summary>
    /// 包名
    /// </summary>
    [Required(ErrorMessage = "包名不能为空")]
    [StringLength(100, ErrorMessage = "包名长度不能超过100个字符")]
    public string PackageName { get; set; }

    /// <summary>
    /// 业务名称
    /// </summary>
    [Required(ErrorMessage = "业务名称不能为空")]
    [StringLength(50, ErrorMessage = "业务名称长度不能超过50个字符")]
    public string BusinessName { get; set; }

    /// <summary>
    /// 功能名称
    /// </summary>
    [Required(ErrorMessage = "功能名称不能为空")]
    [StringLength(50, ErrorMessage = "功能名称长度不能超过50个字符")]
    public string FunctionName { get; set; }

    /// <summary>
    /// 生成类型（1：单表，2：主从表）
    /// </summary>
    [Required(ErrorMessage = "生成类型不能为空")]
    public int GenType { get; set; } = 1;

    /// <summary>
    /// 生成模板
    /// </summary>
    public string GenTemplate { get; set; }

    /// <summary>
    /// 生成路径
    /// </summary>
    public string GenPath { get; set; }

    /// <summary>
    /// 生成选项
    /// </summary>

    public string Options { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    public long? TenantId { get; set; }
}

/// <summary>
/// 代码生成配置更新DTO
/// </summary>
public class HbtGenConfigUpdateDto : HbtGenConfigCreateDto
{
    /// <summary>
    /// 主键ID
    /// </summary>
    [Required(ErrorMessage = "主键ID不能为空")]
    public long ConfigId { get; set; }
}

/// <summary>
/// 代码生成配置导入DTO
/// </summary>
public class HbtGenConfigImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenConfigImportDto()
    {
        TableName = string.Empty;
        Author = string.Empty;
        FunctionName = string.Empty;
        ModuleName = string.Empty;
        BusinessName = string.Empty;
        PackageName = string.Empty;
        GenTemplate = string.Empty;
        GenPath = string.Empty;
        Options = string.Empty;
    }

    /// <summary>
    /// 表名
    /// </summary>
    [Required(ErrorMessage = "表名不能为空")]
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string TableName { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    [Required(ErrorMessage = "作者不能为空")]
    [StringLength(50, ErrorMessage = "作者长度不能超过50个字符")]
    public string Author { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    [Required(ErrorMessage = "模块名称不能为空")]
    [StringLength(50, ErrorMessage = "模块名称长度不能超过50个字符")]
    public string ModuleName { get; set; }

    /// <summary>
    /// 包名
    /// </summary>
    [Required(ErrorMessage = "包名不能为空")]
    [StringLength(100, ErrorMessage = "包名长度不能超过100个字符")]
    public string PackageName { get; set; }

    /// <summary>
    /// 业务名称
    /// </summary>
    [Required(ErrorMessage = "业务名称不能为空")]
    [StringLength(50, ErrorMessage = "业务名称长度不能超过50个字符")]
    public string BusinessName { get; set; }

    /// <summary>
    /// 功能名称
    /// </summary>
    [Required(ErrorMessage = "功能名称不能为空")]
    [StringLength(50, ErrorMessage = "功能名称长度不能超过50个字符")]
    public string FunctionName { get; set; }

    /// <summary>
    /// 生成类型（1：单表，2：主从表）
    /// </summary>
    [Required(ErrorMessage = "生成类型不能为空")]
    public int GenType { get; set; } = 1;

    /// <summary>
    /// 生成模板
    /// </summary>
    public string GenTemplate { get; set; }

    /// <summary>
    /// 生成路径
    /// </summary>
    public string GenPath { get; set; }

    /// <summary>
    /// 生成选项
    /// </summary>

    public string Options { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    public long? TenantId { get; set; }
}

/// <summary>
/// 代码生成配置导出DTO
/// </summary>
public class HbtGenConfigExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenConfigExportDto()
    {
        TableName = string.Empty;
        Author = string.Empty;
        FunctionName = string.Empty;
        ModuleName = string.Empty;
        BusinessName = string.Empty;
        PackageName = string.Empty;
        GenTemplate = string.Empty;
        GenPath = string.Empty;
        Options = string.Empty;
    }

    /// <summary>
    /// 表名
    /// </summary>
    [Required(ErrorMessage = "表名不能为空")]
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string TableName { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    [Required(ErrorMessage = "作者不能为空")]
    [StringLength(50, ErrorMessage = "作者长度不能超过50个字符")]
    public string Author { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    [Required(ErrorMessage = "模块名称不能为空")]
    [StringLength(50, ErrorMessage = "模块名称长度不能超过50个字符")]
    public string ModuleName { get; set; }

    /// <summary>
    /// 包名
    /// </summary>
    [Required(ErrorMessage = "包名不能为空")]
    [StringLength(100, ErrorMessage = "包名长度不能超过100个字符")]
    public string PackageName { get; set; }

    /// <summary>
    /// 业务名称
    /// </summary>
    [Required(ErrorMessage = "业务名称不能为空")]
    [StringLength(50, ErrorMessage = "业务名称长度不能超过50个字符")]
    public string BusinessName { get; set; }

    /// <summary>
    /// 功能名称
    /// </summary>
    [Required(ErrorMessage = "功能名称不能为空")]
    [StringLength(50, ErrorMessage = "功能名称长度不能超过50个字符")]
    public string FunctionName { get; set; }

    /// <summary>
    /// 生成类型（1：单表，2：主从表）
    /// </summary>
    [Required(ErrorMessage = "生成类型不能为空")]
    public int GenType { get; set; } = 1;

    /// <summary>
    /// 生成模板
    /// </summary>
    public string GenTemplate { get; set; }

    /// <summary>
    /// 生成路径
    /// </summary>
    public string GenPath { get; set; }

    /// <summary>
    /// 生成选项
    /// </summary>

    public string Options { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    public long? TenantId { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 代码生成配置模板DTO
/// </summary>
public class HbtGenConfigTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenConfigTemplateDto()
    {
        TableName = string.Empty;
        Author = string.Empty;
        FunctionName = string.Empty;
        ModuleName = string.Empty;
        BusinessName = string.Empty;
        PackageName = string.Empty;
        GenTemplate = string.Empty;
        GenPath = string.Empty;
        Options = string.Empty;
    }

    /// <summary>
    /// 表名
    /// </summary>
    [Required(ErrorMessage = "表名不能为空")]
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string TableName { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    [Required(ErrorMessage = "作者不能为空")]
    [StringLength(50, ErrorMessage = "作者长度不能超过50个字符")]
    public string Author { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    [Required(ErrorMessage = "模块名称不能为空")]
    [StringLength(50, ErrorMessage = "模块名称长度不能超过50个字符")]
    public string ModuleName { get; set; }

    /// <summary>
    /// 包名
    /// </summary>
    [Required(ErrorMessage = "包名不能为空")]
    [StringLength(100, ErrorMessage = "包名长度不能超过100个字符")]
    public string PackageName { get; set; }

    /// <summary>
    /// 业务名称
    /// </summary>
    [Required(ErrorMessage = "业务名称不能为空")]
    [StringLength(50, ErrorMessage = "业务名称长度不能超过50个字符")]
    public string BusinessName { get; set; }

    /// <summary>
    /// 功能名称
    /// </summary>
    [Required(ErrorMessage = "功能名称不能为空")]
    [StringLength(50, ErrorMessage = "功能名称长度不能超过50个字符")]
    public string FunctionName { get; set; }

    /// <summary>
    /// 生成类型（1：单表，2：主从表）
    /// </summary>
    [Required(ErrorMessage = "生成类型不能为空")]
    public int GenType { get; set; } = 1;

    /// <summary>
    /// 生成模板
    /// </summary>
    public string GenTemplate { get; set; }

    /// <summary>
    /// 生成路径
    /// </summary>
    public string GenPath { get; set; }

    /// <summary>
    /// 生成选项
    /// </summary>

    public string Options { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    public long? TenantId { get; set; }
}