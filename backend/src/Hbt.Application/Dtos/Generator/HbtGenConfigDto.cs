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

namespace Hbt.Application.Dtos.Generator;

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
        GenConfigName = string.Empty;
        FunctionName = string.Empty;
        ModuleName = string.Empty;
        BusinessName = string.Empty;
        ProjectName = string.Empty;
        Author = string.Empty;
        GenPath = string.Empty;
        Options = string.Empty;
        CreateBy = string.Empty;
        UpdateBy = string.Empty;
    }

    /// <summary>
    /// 主键ID
    /// </summary>
    [AdaptMember("Id")]
    public long GenConfigId { get; set; }

    /// <summary>
    /// 配置名称
    /// </summary>
    public string GenConfigName { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    /// 项目名称
    /// </summary>
    public string ProjectName { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    public string ModuleName { get; set; }

    /// <summary>
    /// 业务名称
    /// </summary>
    public string BusinessName { get; set; }

    /// <summary>
    /// 功能名称
    /// </summary>
    public string FunctionName { get; set; }

    /// <summary>
    /// 生成代码方式（0zip压缩包 1自定义路径）)
    /// </summary>
    public int GenMethod { get; set; }

    /// <summary>
    /// 模板类型（0：使用wwwroot/Generator/*.scriban模板，1：使用HbtGenTemplate表中的模板）
    /// </summary>
    public int GenTplType { get; set; }

    /// <summary>
    /// 生成路径
    /// </summary>
    public string GenPath { get; set; }

    /// <summary>
    /// 生成选项
    /// </summary>
    public string Options { get; set; }

    /// <summary>
    /// 状态（0正常 1停用）
    /// </summary>
    public int Status { get; set; }

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
/// 代码生成配置查询DTO
/// </summary>
public class HbtGenConfigQueryDto : HbtPagedQuery
{
    /// <summary>
    /// 配置名称
    /// </summary>
    public string? GenConfigName { get; set; }

    /// <summary>
    /// 项目名称
    /// </summary>
    public string? ProjectName { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    public string? ModuleName { get; set; }

    /// <summary>
    /// 业务名称
    /// </summary>
    public string? BusinessName { get; set; }

    /// <summary>
    /// 功能名称
    /// </summary>
    public string? FunctionName { get; set; }

    /// <summary>
    /// 生成方式
    /// </summary>
    public int? GenMethod { get; set; }

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
        GenConfigName = string.Empty;
        Author = string.Empty;
        FunctionName = string.Empty;
        ModuleName = string.Empty;
        BusinessName = string.Empty;
        ProjectName = string.Empty;
        GenPath = string.Empty;
        Options = string.Empty;
    }

    /// <summary>
    /// 配置名称
    /// </summary>
    [Required(ErrorMessage = "配置名称不能为空")]
    [StringLength(100, ErrorMessage = "配置名称长度不能超过100个字符")]
    public string GenConfigName { get; set; }

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
    /// 项目名称
    /// </summary>
    [Required(ErrorMessage = "项目名称不能为空")]
    [StringLength(50, ErrorMessage = "项目名称长度不能超过50个字符")]
    public string ProjectName { get; set; }

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
    /// 生成代码方式（0zip压缩包 1自定义路径）)
    /// </summary>
    [Required(ErrorMessage = "生成方式不能为空")]
    public int GenMethod { get; set; }

    /// <summary>
    /// 模板类型（0：使用wwwroot/Generator/*.scriban模板，1：使用HbtGenTemplate表中的模板）
    /// </summary>
    [Required(ErrorMessage = "模板类型不能为空")]
    [Range(0, 1, ErrorMessage = "模板类型必须是0或1")]
    public int GenTplType { get; set; }

    /// <summary>
    /// 生成路径
    /// </summary>
    [Required(ErrorMessage = "生成路径不能为空")]
    [StringLength(200, ErrorMessage = "生成路径长度不能超过200个字符")]
    public string GenPath { get; set; }

    /// <summary>
    /// 生成选项
    /// </summary>
    public string Options { get; set; }

    /// <summary>
    /// 状态（0正常 1停用）
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    [Range(0, 1, ErrorMessage = "状态必须是0或1")]
    public int Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [StringLength(200, ErrorMessage = "备注长度不能超过200个字符")]
    public string? Remark { get; set; }
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
    [AdaptMember("Id")]
    public long GenConfigId { get; set; }
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
        GenConfigName = string.Empty;
        Author = string.Empty;
        FunctionName = string.Empty;
        ModuleName = string.Empty;
        BusinessName = string.Empty;
        ProjectName = string.Empty;
        GenPath = string.Empty;
        Options = string.Empty;
    }

    /// <summary>
    /// 配置名称
    /// </summary>
    public string GenConfigName { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    public string ModuleName { get; set; }

    /// <summary>
    /// 项目名称
    /// </summary>
    public string ProjectName { get; set; }

    /// <summary>
    /// 业务名称
    /// </summary>
    public string BusinessName { get; set; }

    /// <summary>
    /// 功能名称
    /// </summary>
    public string FunctionName { get; set; }

    /// <summary>
    /// 生成代码方式（0zip压缩包 1自定义路径）)
    /// </summary>
    public int GenMethod { get; set; }

    /// <summary>
    /// 模板类型（0：使用wwwroot/Generator/*.scriban模板，1：使用HbtGenTemplate表中的模板）
    /// </summary>
    public int GenTplType { get; set; }

    /// <summary>
    /// 生成路径
    /// </summary>
    public string GenPath { get; set; }

    /// <summary>
    /// 生成选项
    /// </summary>
    public string Options { get; set; }

    /// <summary>
    /// 状态（0正常 1停用）
    /// </summary>
    public int Status { get; set; }
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
        GenConfigName = string.Empty;
        Author = string.Empty;
        FunctionName = string.Empty;
        ModuleName = string.Empty;
        BusinessName = string.Empty;
        ProjectName = string.Empty;
        GenPath = string.Empty;
        Options = string.Empty;
    }

    /// <summary>
    /// 配置名称
    /// </summary>
    public string GenConfigName { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    public string ModuleName { get; set; }

    /// <summary>
    /// 项目名称
    /// </summary>
    public string ProjectName { get; set; }

    /// <summary>
    /// 业务名称
    /// </summary>
    public string BusinessName { get; set; }

    /// <summary>
    /// 功能名称
    /// </summary>
    public string FunctionName { get; set; }

    /// <summary>
    /// 生成代码方式（0zip压缩包 1自定义路径）)
    /// </summary>
    public int GenMethod { get; set; }

    /// <summary>
    /// 模板类型（0：使用wwwroot/Generator/*.scriban模板，1：使用HbtGenTemplate表中的模板）
    /// </summary>
    public int GenTplType { get; set; }

    /// <summary>
    /// 生成路径
    /// </summary>
    public string GenPath { get; set; }

    /// <summary>
    /// 生成选项
    /// </summary>
    public string Options { get; set; }

    /// <summary>
    /// 状态（0正常 1停用）
    /// </summary>
    public int Status { get; set; }

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
        GenConfigName = string.Empty;
        Author = string.Empty;
        FunctionName = string.Empty;
        ModuleName = string.Empty;
        BusinessName = string.Empty;
        ProjectName = string.Empty;
        GenPath = string.Empty;
        Options = string.Empty;
    }

    /// <summary>
    /// 配置名称
    /// </summary>
    public string GenConfigName { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    public string ModuleName { get; set; }

    /// <summary>
    /// 项目名称
    /// </summary>
    public string ProjectName { get; set; }

    /// <summary>
    /// 业务名称
    /// </summary>
    public string BusinessName { get; set; }

    /// <summary>
    /// 功能名称
    /// </summary>
    public string FunctionName { get; set; }

    /// <summary>
    /// 生成代码方式（0zip压缩包 1自定义路径）)
    /// </summary>
    public int GenMethod { get; set; }

    /// <summary>
    /// 模板类型（0：使用wwwroot/Generator/*.scriban模板，1：使用HbtGenTemplate表中的模板）
    /// </summary>
    public int GenTplType { get; set; }

    /// <summary>
    /// 生成路径
    /// </summary>
    public string GenPath { get; set; }

    /// <summary>
    /// 生成选项
    /// </summary>
    public string Options { get; set; }

    /// <summary>
    /// 状态（0正常 1停用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 代码生成配置状态DTO
/// </summary>
public class HbtGenConfigStatusDto
{
    /// <summary>
    /// 配置ID
    /// </summary>
    [Required(ErrorMessage = "配置ID不能为空")]
    [AdaptMember("Id")]
    public long GenConfigId { get; set; }

    /// <summary>
    /// 状态（0正常 1停用）
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    [Range(0, 1, ErrorMessage = "状态必须是0或1")]
    public int Status { get; set; }
}