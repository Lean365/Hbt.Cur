#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenTableDto.cs
// 创建者 : Lean365
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 代码生成表DTO
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.Generator;

/// <summary>
/// 代码生成表DTO
/// </summary>
public class HbtGenTableDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenTableDto()
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
        Columns = new List<HbtGenColumnDto>();
    }

    /// <summary>
    /// 主键ID
    /// </summary>
    public long TableId { get; set; }

    /// <summary>
    /// 数据库名称
    /// </summary>
    [Required(ErrorMessage = "数据库名称不能为空")]
    [StringLength(100, ErrorMessage = "数据库名称长度不能超过100个字符")]
    public string DatabaseName { get; set; } = string.Empty;

    /// <summary>
    /// 表名
    /// </summary>
    [Required(ErrorMessage = "表名不能为空")]
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// 表注释
    /// </summary>
    [Required(ErrorMessage = "表描述不能为空")]
    [StringLength(200, ErrorMessage = "表描述长度不能超过200个字符")]
    public string TableComment { get; set; } = string.Empty;

    /// <summary>
    /// 类名
    /// </summary>
    [Required(ErrorMessage = "实体类名不能为空")]
    [StringLength(100, ErrorMessage = "实体类名长度不能超过100个字符")]
    public string ClassName { get; set; } = string.Empty;

    /// <summary>
    /// 命名空间
    /// </summary>
    [Required(ErrorMessage = "命名空间不能为空")]
    [StringLength(200, ErrorMessage = "命名空间长度不能超过200个字符")]
    public string Namespace { get; set; } = string.Empty;

    /// <summary>
    /// 基础命名空间
    /// </summary>
    [Required(ErrorMessage = "命名前缀不能为空")]
    [StringLength(100, ErrorMessage = "命名前缀长度不能超过100个字符")]
    public string BaseNamespace { get; set; } = string.Empty;

    /// <summary>
    /// C#类型名称
    /// </summary>
    [Required(ErrorMessage = "C#类名不能为空")]
    [StringLength(100, ErrorMessage = "C#类名长度不能超过100个字符")]
    public string CsharpTypeName { get; set; } = string.Empty;

    /// <summary>
    /// 父表名称
    /// </summary>
    [StringLength(100, ErrorMessage = "关联父表长度不能超过100个字符")]
    public string? ParentTableName { get; set; }

    /// <summary>
    /// 父表外键名称
    /// </summary>
    [StringLength(100, ErrorMessage = "关联外键长度不能超过100个字符")]
    public string? ParentTableFkName { get; set; }

    /// <summary>
    /// 模板类型
    /// </summary>
    [Required(ErrorMessage = "使用模板不能为空")]
    public int TemplateType { get; set; } = 1;

    /// <summary>
    /// 模块名称
    /// </summary>
    [Required(ErrorMessage = "模块名称不能为空")]
    [StringLength(50, ErrorMessage = "模块名称长度不能超过50个字符")]
    public string ModuleName { get; set; } = string.Empty;

    /// <summary>
    /// 业务名称
    /// </summary>
    [Required(ErrorMessage = "业务名称不能为空")]
    [StringLength(50, ErrorMessage = "业务名称长度不能超过50个字符")]
    public string BusinessName { get; set; } = string.Empty;

    /// <summary>
    /// 功能名称
    /// </summary>
    [Required(ErrorMessage = "功能名称不能为空")]
    [StringLength(50, ErrorMessage = "功能名称长度不能超过50个字符")]
    public string FunctionName { get; set; } = string.Empty;

    /// <summary>
    /// 作者
    /// </summary>
    [Required(ErrorMessage = "作者名称不能为空")]
    [StringLength(50, ErrorMessage = "作者名称长度不能超过50个字符")]
    public string Author { get; set; } = string.Empty;

    /// <summary>
    /// 生成模式
    /// </summary>
    [Required(ErrorMessage = "生成方式不能为空")]
    public int GenMode { get; set; } = 0;

    /// <summary>
    /// 生成路径
    /// </summary>
    [Required(ErrorMessage = "存放位置不能为空")]
    [StringLength(200, ErrorMessage = "存放位置长度不能超过200个字符")]
    public string GenPath { get; set; } = string.Empty;

    /// <summary>
    /// 选项
    /// </summary>
    public string? Options { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    [Required(ErrorMessage = "租户ID不能为空")]
    public long TenantId { get; set; } = 0;

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

    /// <summary>
    /// 页码
    /// </summary>
    public int PageIndex { get; set; } = 1;

    /// <summary>
    /// 每页记录数
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// 列信息
    /// </summary>
    public List<HbtGenColumnDto> Columns { get; set; } = new();
}

/// <summary>
/// 代码生成表查询DTO
/// </summary>
public class HbtGenTableQueryDto : HbtPagedQuery
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    [StringLength(100, ErrorMessage = "数据库名称长度不能超过100个字符")]
    public string? DatabaseName { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string? TableName { get; set; }

    /// <summary>
    /// 表注释
    /// </summary>
    [StringLength(200, ErrorMessage = "表描述长度不能超过200个字符")]
    public string? TableComment { get; set; }

    /// <summary>
    /// 创建时间范围开始
    /// </summary>
    public DateTime? BeginTime { get; set; }

    /// <summary>
    /// 创建时间范围结束
    /// </summary>
    public DateTime? EndTime { get; set; }
}

/// <summary>
/// 代码生成表更新DTO
/// </summary>
public class HbtGenTableUpdateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenTableUpdateDto()
    {
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
        Columns = new List<HbtGenColumnDto>();
    }

    /// <summary>
    /// 主键ID
    /// </summary>
    [Required(ErrorMessage = "主键ID不能为空")]
    public long TableId { get; set; }

    /// <summary>
    /// 数据库名称
    /// </summary>
    [Required(ErrorMessage = "数据库名称不能为空")]
    [StringLength(100, ErrorMessage = "数据库名称长度不能超过100个字符")]
    public string DatabaseName { get; set; } = string.Empty;

    /// <summary>
    /// 表名
    /// </summary>
    [Required(ErrorMessage = "表名不能为空")]
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// 表注释
    /// </summary>
    [Required(ErrorMessage = "表描述不能为空")]
    [StringLength(200, ErrorMessage = "表描述长度不能超过200个字符")]
    public string TableComment { get; set; } = string.Empty;

    /// <summary>
    /// 类名
    /// </summary>
    [Required(ErrorMessage = "实体类名不能为空")]
    [StringLength(100, ErrorMessage = "实体类名长度不能超过100个字符")]
    public string ClassName { get; set; } = string.Empty;

    /// <summary>
    /// 命名空间
    /// </summary>
    [Required(ErrorMessage = "命名空间不能为空")]
    [StringLength(200, ErrorMessage = "命名空间长度不能超过200个字符")]
    public string Namespace { get; set; } = string.Empty;

    /// <summary>
    /// 基础命名空间
    /// </summary>
    [Required(ErrorMessage = "命名前缀不能为空")]
    [StringLength(100, ErrorMessage = "命名前缀长度不能超过100个字符")]
    public string BaseNamespace { get; set; } = string.Empty;

    /// <summary>
    /// C#类型名称
    /// </summary>
    [Required(ErrorMessage = "C#类名不能为空")]
    [StringLength(100, ErrorMessage = "C#类名长度不能超过100个字符")]
    public string CsharpTypeName { get; set; } = string.Empty;

    /// <summary>
    /// 父表名称
    /// </summary>
    [StringLength(100, ErrorMessage = "关联父表长度不能超过100个字符")]
    public string? ParentTableName { get; set; }

    /// <summary>
    /// 父表外键名称
    /// </summary>
    [StringLength(100, ErrorMessage = "关联外键长度不能超过100个字符")]
    public string? ParentTableFkName { get; set; }

    /// <summary>
    /// 模板类型
    /// </summary>
    [Required(ErrorMessage = "使用模板不能为空")]
    public int TemplateType { get; set; } = 1;

    /// <summary>
    /// 模块名称
    /// </summary>
    [Required(ErrorMessage = "模块名称不能为空")]
    [StringLength(50, ErrorMessage = "模块名称长度不能超过50个字符")]
    public string ModuleName { get; set; } = string.Empty;

    /// <summary>
    /// 业务名称
    /// </summary>
    [Required(ErrorMessage = "业务名称不能为空")]
    [StringLength(50, ErrorMessage = "业务名称长度不能超过50个字符")]
    public string BusinessName { get; set; } = string.Empty;

    /// <summary>
    /// 功能名称
    /// </summary>
    [Required(ErrorMessage = "功能名称不能为空")]
    [StringLength(50, ErrorMessage = "功能名称长度不能超过50个字符")]
    public string FunctionName { get; set; } = string.Empty;

    /// <summary>
    /// 作者
    /// </summary>
    [Required(ErrorMessage = "作者名称不能为空")]
    [StringLength(50, ErrorMessage = "作者名称长度不能超过50个字符")]
    public string Author { get; set; } = string.Empty;

    /// <summary>
    /// 生成模式
    /// </summary>
    [Required(ErrorMessage = "生成方式不能为空")]
    public int GenMode { get; set; } = 0;

    /// <summary>
    /// 生成路径
    /// </summary>
    [Required(ErrorMessage = "存放位置不能为空")]
    [StringLength(200, ErrorMessage = "存放位置长度不能超过200个字符")]
    public string GenPath { get; set; } = string.Empty;

    /// <summary>
    /// 选项
    /// </summary>
    public string? Options { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    [Required(ErrorMessage = "租户ID不能为空")]
    public long TenantId { get; set; } = 0;

    /// <summary>
    /// 列信息
    /// </summary>
    public List<HbtGenColumnDto> Columns { get; set; } = new();
}

/// <summary>
/// 代码生成表导入DTO
/// </summary>
public class HbtGenTableImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenTableImportDto()
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
    }

    /// <summary>
    /// 数据库名称
    /// </summary>
    [Required(ErrorMessage = "数据库名称不能为空")]
    [StringLength(100, ErrorMessage = "数据库名称长度不能超过100个字符")]
    public string DatabaseName { get; set; } = string.Empty;

    /// <summary>
    /// 表名
    /// </summary>
    [Required(ErrorMessage = "表名不能为空")]
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// 表注释
    /// </summary>
    [Required(ErrorMessage = "表描述不能为空")]
    [StringLength(200, ErrorMessage = "表描述长度不能超过200个字符")]
    public string TableComment { get; set; } = string.Empty;

    /// <summary>
    /// 类名
    /// </summary>
    [Required(ErrorMessage = "实体类名不能为空")]
    [StringLength(100, ErrorMessage = "实体类名长度不能超过100个字符")]
    public string ClassName { get; set; } = string.Empty;

    /// <summary>
    /// 命名空间
    /// </summary>
    [Required(ErrorMessage = "命名空间不能为空")]
    [StringLength(200, ErrorMessage = "命名空间长度不能超过200个字符")]
    public string Namespace { get; set; } = string.Empty;

    /// <summary>
    /// 基础命名空间
    /// </summary>
    [Required(ErrorMessage = "命名前缀不能为空")]
    [StringLength(100, ErrorMessage = "命名前缀长度不能超过100个字符")]
    public string BaseNamespace { get; set; } = string.Empty;

    /// <summary>
    /// C#类型名称
    /// </summary>
    [Required(ErrorMessage = "C#类名不能为空")]
    [StringLength(100, ErrorMessage = "C#类名长度不能超过100个字符")]
    public string CsharpTypeName { get; set; } = string.Empty;

    /// <summary>
    /// 父表名称
    /// </summary>
    [StringLength(100, ErrorMessage = "关联父表长度不能超过100个字符")]
    public string? ParentTableName { get; set; }

    /// <summary>
    /// 父表外键名称
    /// </summary>
    [StringLength(100, ErrorMessage = "关联外键长度不能超过100个字符")]
    public string? ParentTableFkName { get; set; }

    /// <summary>
    /// 模板类型
    /// </summary>
    [Required(ErrorMessage = "使用模板不能为空")]
    public int TemplateType { get; set; } = 1;

    /// <summary>
    /// 模块名称
    /// </summary>
    [Required(ErrorMessage = "模块名称不能为空")]
    [StringLength(50, ErrorMessage = "模块名称长度不能超过50个字符")]
    public string ModuleName { get; set; } = string.Empty;

    /// <summary>
    /// 业务名称
    /// </summary>
    [Required(ErrorMessage = "业务名称不能为空")]
    [StringLength(50, ErrorMessage = "业务名称长度不能超过50个字符")]
    public string BusinessName { get; set; } = string.Empty;

    /// <summary>
    /// 功能名称
    /// </summary>
    [Required(ErrorMessage = "功能名称不能为空")]
    [StringLength(50, ErrorMessage = "功能名称长度不能超过50个字符")]
    public string FunctionName { get; set; } = string.Empty;

    /// <summary>
    /// 作者
    /// </summary>
    [Required(ErrorMessage = "作者名称不能为空")]
    [StringLength(50, ErrorMessage = "作者名称长度不能超过50个字符")]
    public string Author { get; set; } = string.Empty;

    /// <summary>
    /// 生成模式
    /// </summary>
    [Required(ErrorMessage = "生成方式不能为空")]
    public int GenMode { get; set; } = 0;

    /// <summary>
    /// 生成路径
    /// </summary>
    [Required(ErrorMessage = "存放位置不能为空")]
    [StringLength(200, ErrorMessage = "存放位置长度不能超过200个字符")]
    public string GenPath { get; set; } = string.Empty;

    /// <summary>
    /// 选项
    /// </summary>
    public string? Options { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    [Required(ErrorMessage = "租户ID不能为空")]
    public long TenantId { get; set; } = 0;

    /// <summary>
    /// 字段列表
    /// </summary>
    public List<HbtGenColumnImportDto> Columns { get; set; } = new();
}

/// <summary>
/// 代码生成表导出DTO
/// </summary>
public class HbtGenTableExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenTableExportDto()
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
    }

    /// <summary>
    /// 数据库名称
    /// </summary>
    [Required(ErrorMessage = "数据库名称不能为空")]
    [StringLength(100, ErrorMessage = "数据库名称长度不能超过100个字符")]
    public string DatabaseName { get; set; } = string.Empty;

    /// <summary>
    /// 表名
    /// </summary>
    [Required(ErrorMessage = "表名不能为空")]
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// 表注释
    /// </summary>
    [Required(ErrorMessage = "表描述不能为空")]
    [StringLength(200, ErrorMessage = "表描述长度不能超过200个字符")]
    public string TableComment { get; set; } = string.Empty;

    /// <summary>
    /// 类名
    /// </summary>
    [Required(ErrorMessage = "实体类名不能为空")]
    [StringLength(100, ErrorMessage = "实体类名长度不能超过100个字符")]
    public string ClassName { get; set; } = string.Empty;

    /// <summary>
    /// 命名空间
    /// </summary>
    [Required(ErrorMessage = "命名空间不能为空")]
    [StringLength(200, ErrorMessage = "命名空间长度不能超过200个字符")]
    public string Namespace { get; set; } = string.Empty;

    /// <summary>
    /// 基础命名空间
    /// </summary>
    [Required(ErrorMessage = "命名前缀不能为空")]
    [StringLength(100, ErrorMessage = "命名前缀长度不能超过100个字符")]
    public string BaseNamespace { get; set; } = string.Empty;

    /// <summary>
    /// C#类型名称
    /// </summary>
    [Required(ErrorMessage = "C#类名不能为空")]
    [StringLength(100, ErrorMessage = "C#类名长度不能超过100个字符")]
    public string CsharpTypeName { get; set; } = string.Empty;

    /// <summary>
    /// 父表名称
    /// </summary>
    [StringLength(100, ErrorMessage = "关联父表长度不能超过100个字符")]
    public string? ParentTableName { get; set; }

    /// <summary>
    /// 父表外键名称
    /// </summary>
    [StringLength(100, ErrorMessage = "关联外键长度不能超过100个字符")]
    public string? ParentTableFkName { get; set; }

    /// <summary>
    /// 模板类型
    /// </summary>
    [Required(ErrorMessage = "使用模板不能为空")]
    public int TemplateType { get; set; } = 1;

    /// <summary>
    /// 模块名称
    /// </summary>
    [Required(ErrorMessage = "模块名称不能为空")]
    [StringLength(50, ErrorMessage = "模块名称长度不能超过50个字符")]
    public string ModuleName { get; set; } = string.Empty;

    /// <summary>
    /// 业务名称
    /// </summary>
    [Required(ErrorMessage = "业务名称不能为空")]
    [StringLength(50, ErrorMessage = "业务名称长度不能超过50个字符")]
    public string BusinessName { get; set; } = string.Empty;

    /// <summary>
    /// 功能名称
    /// </summary>
    [Required(ErrorMessage = "功能名称不能为空")]
    [StringLength(50, ErrorMessage = "功能名称长度不能超过50个字符")]
    public string FunctionName { get; set; } = string.Empty;

    /// <summary>
    /// 作者
    /// </summary>
    [Required(ErrorMessage = "作者名称不能为空")]
    [StringLength(50, ErrorMessage = "作者名称长度不能超过50个字符")]
    public string Author { get; set; } = string.Empty;

    /// <summary>
    /// 生成模式
    /// </summary>
    [Required(ErrorMessage = "生成方式不能为空")]
    public int GenMode { get; set; } = 0;

    /// <summary>
    /// 生成路径
    /// </summary>
    [Required(ErrorMessage = "存放位置不能为空")]
    [StringLength(200, ErrorMessage = "存放位置长度不能超过200个字符")]
    public string GenPath { get; set; } = string.Empty;

    /// <summary>
    /// 选项
    /// </summary>
    public string? Options { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    [Required(ErrorMessage = "租户ID不能为空")]
    public long TenantId { get; set; } = 0;

    /// <summary>
    /// 表信息
    /// </summary>
    public HbtGenTableDto Table { get; set; } = new();

    /// <summary>
    /// 列信息
    /// </summary>
    public List<HbtGenColumnDto> Columns { get; set; } = new();
}