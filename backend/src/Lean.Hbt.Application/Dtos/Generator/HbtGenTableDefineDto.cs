#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenTableDefineDto.cs
// 创建者 : Lean365
// 创建时间: 2024-03-24
// 版本号 : V0.0.1
// 描述    : 代码生成表定义DTO
//===================================================================

using System.ComponentModel.DataAnnotations;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Dtos.Generator;

/// <summary>
/// 代码生成表定义DTO
/// </summary>
public class HbtGenTableDefineDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenTableDefineDto()
    {
        DatabaseName = string.Empty;
        TableName = string.Empty;
        TableComment = string.Empty;
        ClassName = string.Empty;
        Namespace = string.Empty;
        BaseNamespace = string.Empty;
        CsharpTypeName = string.Empty;
        ModuleName = string.Empty;
        BusinessName = string.Empty;
        FunctionName = string.Empty;
        Author = string.Empty;
        GenPath = string.Empty;
        CreateBy = string.Empty;
        UpdateBy = string.Empty;
        Columns = new List<HbtGenColumnDefineDto>();
    }

    /// <summary>
    /// 主键ID
    /// </summary>
    public long Id { get; set; }

    #region 基本信息

    /// <summary>
    /// 数据库名称
    /// </summary>
    [Required(ErrorMessage = "数据库名称不能为空")]
    [StringLength(100, ErrorMessage = "数据库名称长度不能超过100个字符")]
    public string DatabaseName { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    [Required(ErrorMessage = "表名不能为空")]
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string TableName { get; set; }

    /// <summary>
    /// 表描述
    /// </summary>
    [Required(ErrorMessage = "表描述不能为空")]
    [StringLength(200, ErrorMessage = "表描述长度不能超过200个字符")]
    public string TableComment { get; set; }

    #endregion

    #region 类型信息

    /// <summary>
    /// 实体类名
    /// </summary>
    [Required(ErrorMessage = "实体类名不能为空")]
    [StringLength(100, ErrorMessage = "实体类名长度不能超过100个字符")]
    public string ClassName { get; set; }

    /// <summary>
    /// 命名空间
    /// </summary>
    [Required(ErrorMessage = "命名空间不能为空")]
    [StringLength(200, ErrorMessage = "命名空间长度不能超过200个字符")]
    public string Namespace { get; set; }

    /// <summary>
    /// 基本命名空间前缀
    /// </summary>
    [Required(ErrorMessage = "命名前缀不能为空")]
    [StringLength(100, ErrorMessage = "命名前缀长度不能超过100个字符")]
    public string BaseNamespace { get; set; }

    /// <summary>
    /// C#类名
    /// </summary>
    [Required(ErrorMessage = "C#类名不能为空")]
    [StringLength(100, ErrorMessage = "C#类名长度不能超过100个字符")]
    public string CsharpTypeName { get; set; }

    #endregion

    #region 关联信息

    /// <summary>
    /// 关联父表名
    /// </summary>
    [StringLength(100, ErrorMessage = "关联父表长度不能超过100个字符")]
    public string? ParentTableName { get; set; }

    /// <summary>
    /// 本表关联父表的外键名
    /// </summary>
    [StringLength(100, ErrorMessage = "关联外键长度不能超过100个字符")]
    public string? ParentTableFkName { get; set; }

    #endregion

    #region 生成配置信息

    /// <summary>
    /// 使用的模板
    /// </summary>
    [Required(ErrorMessage = "使用模板不能为空")]
    public int TemplateType { get; set; } = 1;

    /// <summary>
    /// 生成模块名
    /// </summary>
    [Required(ErrorMessage = "模块名称不能为空")]
    [StringLength(50, ErrorMessage = "模块名称长度不能超过50个字符")]
    public string ModuleName { get; set; }

    /// <summary>
    /// 生成业务名
    /// </summary>
    [Required(ErrorMessage = "业务名称不能为空")]
    [StringLength(50, ErrorMessage = "业务名称长度不能超过50个字符")]
    public string BusinessName { get; set; }

    /// <summary>
    /// 生成功能名
    /// </summary>
    [Required(ErrorMessage = "功能名称不能为空")]
    [StringLength(50, ErrorMessage = "功能名称长度不能超过50个字符")]
    public string FunctionName { get; set; }

    /// <summary>
    /// 生成作者名
    /// </summary>
    [Required(ErrorMessage = "作者名称不能为空")]
    [StringLength(50, ErrorMessage = "作者名称长度不能超过50个字符")]
    public string Author { get; set; }

    #endregion

    #region 生成选项

    /// <summary>
    /// 生成代码方式
    /// </summary>
    [Required(ErrorMessage = "生成方式不能为空")]
    public int GenMode { get; set; } = 0;

    /// <summary>
    /// 代码生成存放位置
    /// </summary>
    [Required(ErrorMessage = "存放位置不能为空")]
    [StringLength(200, ErrorMessage = "存放位置长度不能超过200个字符")]
    public string GenPath { get; set; }

    /// <summary>
    /// 其他生成选项
    /// </summary>
    public string? Options { get; set; }

    #endregion

    #region 系统信息

    /// <summary>
    /// 租户ID
    /// </summary>
    public long TenantId { get; set; }

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

    #endregion

    /// <summary>
    /// 字段定义列表
    /// </summary>
    public List<HbtGenColumnDefineDto> Columns { get; set; }
}

/// <summary>
/// 代码生成表定义查询DTO
/// </summary>
public class HbtGenTableDefineQueryDto : HbtPagedQuery
{
    /// <summary>
    /// 表名
    /// </summary>
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string? TableName { get; set; }

    /// <summary>
    /// 表描述
    /// </summary>
    [StringLength(200, ErrorMessage = "表描述长度不能超过200个字符")]
    public string? TableComment { get; set; }

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
/// 代码生成表定义创建DTO
/// </summary>
public class HbtGenTableDefineCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenTableDefineCreateDto()
    {
        DatabaseName = string.Empty;
        TableName = string.Empty;
        TableComment = string.Empty;
        ClassName = string.Empty;
        Namespace = string.Empty;
        BaseNamespace = string.Empty;
        CsharpTypeName = string.Empty;
        ModuleName = string.Empty;
        BusinessName = string.Empty;
        FunctionName = string.Empty;
        Author = string.Empty;
        GenPath = string.Empty;
        Columns = new List<HbtGenColumnDefineDto>();
    }

    #region 基本信息

    /// <summary>
    /// 数据库名称
    /// </summary>
    [Required(ErrorMessage = "数据库名称不能为空")]
    [StringLength(100, ErrorMessage = "数据库名称长度不能超过100个字符")]
    public string DatabaseName { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    [Required(ErrorMessage = "表名不能为空")]
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string TableName { get; set; }

    /// <summary>
    /// 表描述
    /// </summary>
    [Required(ErrorMessage = "表描述不能为空")]
    [StringLength(200, ErrorMessage = "表描述长度不能超过200个字符")]
    public string TableComment { get; set; }

    #endregion

    #region 类型信息

    /// <summary>
    /// 实体类名
    /// </summary>
    [Required(ErrorMessage = "实体类名不能为空")]
    [StringLength(100, ErrorMessage = "实体类名长度不能超过100个字符")]
    public string ClassName { get; set; }

    /// <summary>
    /// 命名空间
    /// </summary>
    [Required(ErrorMessage = "命名空间不能为空")]
    [StringLength(200, ErrorMessage = "命名空间长度不能超过200个字符")]
    public string Namespace { get; set; }

    /// <summary>
    /// 基本命名空间前缀
    /// </summary>
    [Required(ErrorMessage = "命名前缀不能为空")]
    [StringLength(100, ErrorMessage = "命名前缀长度不能超过100个字符")]
    public string BaseNamespace { get; set; }

    /// <summary>
    /// C#类名
    /// </summary>
    [Required(ErrorMessage = "C#类名不能为空")]
    [StringLength(100, ErrorMessage = "C#类名长度不能超过100个字符")]
    public string CsharpTypeName { get; set; }

    #endregion

    #region 关联信息

    /// <summary>
    /// 关联父表名
    /// </summary>
    [StringLength(100, ErrorMessage = "关联父表长度不能超过100个字符")]
    public string? ParentTableName { get; set; }

    /// <summary>
    /// 本表关联父表的外键名
    /// </summary>
    [StringLength(100, ErrorMessage = "关联外键长度不能超过100个字符")]
    public string? ParentTableFkName { get; set; }

    #endregion

    #region 生成配置信息

    /// <summary>
    /// 使用的模板
    /// </summary>
    [Required(ErrorMessage = "使用模板不能为空")]
    public int TemplateType { get; set; } = 1;

    /// <summary>
    /// 生成模块名
    /// </summary>
    [Required(ErrorMessage = "模块名称不能为空")]
    [StringLength(50, ErrorMessage = "模块名称长度不能超过50个字符")]
    public string ModuleName { get; set; }

    /// <summary>
    /// 生成业务名
    /// </summary>
    [Required(ErrorMessage = "业务名称不能为空")]
    [StringLength(50, ErrorMessage = "业务名称长度不能超过50个字符")]
    public string BusinessName { get; set; }

    /// <summary>
    /// 生成功能名
    /// </summary>
    [Required(ErrorMessage = "功能名称不能为空")]
    [StringLength(50, ErrorMessage = "功能名称长度不能超过50个字符")]
    public string FunctionName { get; set; }

    /// <summary>
    /// 生成作者名
    /// </summary>
    [Required(ErrorMessage = "作者名称不能为空")]
    [StringLength(50, ErrorMessage = "作者名称长度不能超过50个字符")]
    public string Author { get; set; }

    #endregion

    #region 生成选项

    /// <summary>
    /// 生成代码方式
    /// </summary>
    [Required(ErrorMessage = "生成方式不能为空")]
    public int GenMode { get; set; } = 0;

    /// <summary>
    /// 代码生成存放位置
    /// </summary>
    [Required(ErrorMessage = "存放位置不能为空")]
    [StringLength(200, ErrorMessage = "存放位置长度不能超过200个字符")]
    public string GenPath { get; set; }

    /// <summary>
    /// 其他生成选项
    /// </summary>
    public string? Options { get; set; }

    #endregion

    /// <summary>
    /// 字段定义列表
    /// </summary>
    public List<HbtGenColumnDefineDto> Columns { get; set; }
}

/// <summary>
/// 代码生成表定义更新DTO
/// </summary>
public class HbtGenTableDefineUpdateDto : HbtGenTableDefineCreateDto
{
    /// <summary>
    /// 主键ID
    /// </summary>
    [Required(ErrorMessage = "主键不能为空")]
    public long Id { get; set; }
}

/// <summary>
/// 代码生成表定义导入DTO
/// </summary>
public class HbtGenTableDefineImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenTableDefineImportDto()
    {
        DatabaseName = string.Empty;
        TableName = string.Empty;
        TableComment = string.Empty;
        ClassName = string.Empty;
        Namespace = string.Empty;
        BaseNamespace = string.Empty;
        CsharpTypeName = string.Empty;
        ModuleName = string.Empty;
        BusinessName = string.Empty;
        FunctionName = string.Empty;
        Author = string.Empty;
        GenPath = string.Empty;
        Columns = new List<HbtGenColumnDefineDto>();
    }

    #region 基本信息

    /// <summary>
    /// 数据库名称
    /// </summary>
    public string DatabaseName { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; }

    /// <summary>
    /// 表描述
    /// </summary>
    public string TableComment { get; set; }

    #endregion

    #region 类型信息

    /// <summary>
    /// 实体类名
    /// </summary>
    public string ClassName { get; set; }

    /// <summary>
    /// 命名空间
    /// </summary>
    public string Namespace { get; set; }

    /// <summary>
    /// 基本命名空间前缀
    /// </summary>
    public string BaseNamespace { get; set; }

    /// <summary>
    /// C#类名
    /// </summary>
    public string CsharpTypeName { get; set; }

    #endregion

    #region 关联信息

    /// <summary>
    /// 关联父表名
    /// </summary>
    public string? ParentTableName { get; set; }

    /// <summary>
    /// 本表关联父表的外键名
    /// </summary>
    public string? ParentTableFkName { get; set; }

    #endregion

    #region 生成配置信息

    /// <summary>
    /// 使用的模板
    /// </summary>
    public int TemplateType { get; set; } = 1;

    /// <summary>
    /// 生成模块名
    /// </summary>
    public string ModuleName { get; set; }

    /// <summary>
    /// 生成业务名
    /// </summary>
    public string BusinessName { get; set; }

    /// <summary>
    /// 生成功能名
    /// </summary>
    public string FunctionName { get; set; }

    /// <summary>
    /// 生成作者名
    /// </summary>
    public string Author { get; set; }

    #endregion

    #region 生成选项

    /// <summary>
    /// 生成代码方式
    /// </summary>
    public int GenMode { get; set; } = 0;

    /// <summary>
    /// 代码生成存放位置
    /// </summary>
    public string GenPath { get; set; }

    /// <summary>
    /// 其他生成选项
    /// </summary>
    public string? Options { get; set; }

    #endregion

    /// <summary>
    /// 字段定义列表
    /// </summary>
    public List<HbtGenColumnDefineDto> Columns { get; set; }
}

/// <summary>
/// 代码生成表定义导出DTO
/// </summary>
public class HbtGenTableDefineExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenTableDefineExportDto()
    {
        DatabaseName = string.Empty;
        TableName = string.Empty;
        TableComment = string.Empty;
        ClassName = string.Empty;
        Namespace = string.Empty;
        BaseNamespace = string.Empty;
        CsharpTypeName = string.Empty;
        ModuleName = string.Empty;
        BusinessName = string.Empty;
        FunctionName = string.Empty;
        Author = string.Empty;
        GenPath = string.Empty;
        CreateBy = string.Empty;
        UpdateBy = string.Empty;
        Columns = new List<HbtGenColumnDefineDto>();
    }

    #region 基本信息

    /// <summary>
    /// 数据库名称
    /// </summary>
    public string DatabaseName { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; }

    /// <summary>
    /// 表描述
    /// </summary>
    public string TableComment { get; set; }

    #endregion

    #region 类型信息

    /// <summary>
    /// 实体类名
    /// </summary>
    public string ClassName { get; set; }

    /// <summary>
    /// 命名空间
    /// </summary>
    public string Namespace { get; set; }

    /// <summary>
    /// 基本命名空间前缀
    /// </summary>
    public string BaseNamespace { get; set; }

    /// <summary>
    /// C#类名
    /// </summary>
    public string CsharpTypeName { get; set; }

    #endregion

    #region 关联信息

    /// <summary>
    /// 关联父表名
    /// </summary>
    public string? ParentTableName { get; set; }

    /// <summary>
    /// 本表关联父表的外键名
    /// </summary>
    public string? ParentTableFkName { get; set; }

    #endregion

    #region 生成配置信息

    /// <summary>
    /// 使用的模板
    /// </summary>
    public int TemplateType { get; set; }

    /// <summary>
    /// 生成模块名
    /// </summary>
    public string ModuleName { get; set; }

    /// <summary>
    /// 生成业务名
    /// </summary>
    public string BusinessName { get; set; }

    /// <summary>
    /// 生成功能名
    /// </summary>
    public string FunctionName { get; set; }

    /// <summary>
    /// 生成作者名
    /// </summary>
    public string Author { get; set; }

    #endregion

    #region 生成选项

    /// <summary>
    /// 生成代码方式
    /// </summary>
    public int GenMode { get; set; }

    /// <summary>
    /// 代码生成存放位置
    /// </summary>
    public string GenPath { get; set; }

    /// <summary>
    /// 其他生成选项
    /// </summary>
    public string? Options { get; set; }

    #endregion

    #region 系统信息

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

    #endregion

    /// <summary>
    /// 字段定义列表
    /// </summary>
    public List<HbtGenColumnDefineDto> Columns { get; set; }
}

/// <summary>
/// 代码生成表定义模板DTO
/// </summary>
public class HbtGenTableDefineTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenTableDefineTemplateDto()
    {
        DatabaseName = string.Empty;
        TableName = string.Empty;
        TableComment = string.Empty;
        ClassName = string.Empty;
        Namespace = string.Empty;
        BaseNamespace = string.Empty;
        CsharpTypeName = string.Empty;
        ModuleName = string.Empty;
        BusinessName = string.Empty;
        FunctionName = string.Empty;
        Author = string.Empty;
        GenPath = string.Empty;
        GenMode = "0";
        TemplateType = "1";
        Columns = new List<HbtGenColumnDefineTemplateDto>();
    }

    #region 基本信息

    /// <summary>
    /// 数据库名称
    /// </summary>
    public string DatabaseName { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; }

    /// <summary>
    /// 表描述
    /// </summary>
    public string TableComment { get; set; }

    #endregion

    #region 类型信息

    /// <summary>
    /// 实体类名
    /// </summary>
    public string ClassName { get; set; }

    /// <summary>
    /// 命名空间
    /// </summary>
    public string Namespace { get; set; }

    /// <summary>
    /// 基本命名空间前缀
    /// </summary>
    public string BaseNamespace { get; set; }

    /// <summary>
    /// C#类名
    /// </summary>
    public string CsharpTypeName { get; set; }

    #endregion

    #region 关联信息

    /// <summary>
    /// 关联父表名
    /// </summary>
    public string? ParentTableName { get; set; }

    /// <summary>
    /// 本表关联父表的外键名
    /// </summary>
    public string? ParentTableFkName { get; set; }

    #endregion

    #region 生成配置信息

    /// <summary>
    /// 使用的模板
    /// </summary>
    public string TemplateType { get; set; }

    /// <summary>
    /// 生成模块名
    /// </summary>
    public string ModuleName { get; set; }

    /// <summary>
    /// 生成业务名
    /// </summary>
    public string BusinessName { get; set; }

    /// <summary>
    /// 生成功能名
    /// </summary>
    public string FunctionName { get; set; }

    /// <summary>
    /// 生成作者名
    /// </summary>
    public string Author { get; set; }

    #endregion

    #region 生成选项

    /// <summary>
    /// 生成代码方式
    /// </summary>
    public string GenMode { get; set; }

    /// <summary>
    /// 代码生成存放位置
    /// </summary>
    public string GenPath { get; set; }

    /// <summary>
    /// 其他生成选项
    /// </summary>
    public string? Options { get; set; }

    #endregion

    /// <summary>
    /// 字段定义列表
    /// </summary>
    public List<HbtGenColumnDefineTemplateDto> Columns { get; set; }
}
