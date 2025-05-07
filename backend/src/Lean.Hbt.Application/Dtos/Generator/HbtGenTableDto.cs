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
    /// 状态
    /// </summary>
    public int? Status { get; set; }

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
        DatabaseName = string.Empty;
        TableName = string.Empty;
        TableComment = string.Empty;
        ClassName = string.Empty;
        Namespace = string.Empty;
        PackageName = string.Empty;
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
    public string DatabaseName { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    [Required(ErrorMessage = "表名不能为空")]
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string TableName { get; set; }

    /// <summary>
    /// 表注释
    /// </summary>
    [Required(ErrorMessage = "表描述不能为空")]
    [StringLength(200, ErrorMessage = "表描述长度不能超过200个字符")]
    public string TableComment { get; set; }

    /// <summary>
    /// 类名
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
    /// 包名
    /// </summary>
    [Required(ErrorMessage = "包名不能为空")]
    [StringLength(200, ErrorMessage = "包名长度不能超过200个字符")]
    public string PackageName { get; set; }

    /// <summary>
    /// 基本命名空间前缀
    /// </summary>
    [StringLength(200, ErrorMessage = "基本命名空间前缀长度不能超过200个字符")]
    public string BaseNamespace { get; set; }

    /// <summary>
    /// C#类名
    /// </summary>
    [StringLength(100, ErrorMessage = "C#类名长度不能超过100个字符")]
    public string CsharpTypeName { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    [Required(ErrorMessage = "模块名称不能为空")]
    [StringLength(50, ErrorMessage = "模块名称长度不能超过50个字符")]
    public string ModuleName { get; set; }

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
    /// 作者
    /// </summary>
    [Required(ErrorMessage = "作者不能为空")]
    [StringLength(50, ErrorMessage = "作者长度不能超过50个字符")]
    public string Author { get; set; }

    /// <summary>
    /// 生成代码方式
    /// </summary>
    [Required(ErrorMessage = "生成方式不能为空")]
    public int GenMode { get; set; }

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

    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [StringLength(500, ErrorMessage = "备注长度不能超过500个字符")]
    public string? Remark { get; set; }

    /// <summary>
    /// 列信息
    /// </summary>
    public List<HbtGenColumnDto> Columns { get; set; }
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


/// <summary>
/// 数据库表信息DTO
/// </summary>
public class HbtGenTableInfoDto
{
    /// <summary>
    /// 表名
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 表描述
    /// </summary>
    public string Description { get; set; } = string.Empty;
}

/// <summary>
/// 数据库表列信息DTO（与SqlSugar DbColumnInfo字段一致）
/// </summary>
public class HbtGenTableColumnInfoDto
{
    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// 表ID
    /// </summary>
    public long TableId { get; set; }

    /// <summary>
    /// 列名
    /// </summary>
    public string DbColumnName { get; set; } = string.Empty;

    /// <summary>
    /// 属性名
    /// </summary>
    public string PropertyName { get; set; } = string.Empty;

    /// <summary>
    /// 数据类型
    /// </summary>
    public string DataType { get; set; } = string.Empty;

    /// <summary>
    /// Oracle类型
    /// </summary>
    public string OracleDataType { get; set; } = string.Empty;

    /// <summary>
    /// 属性类型
    /// </summary>
    public string PropertyType { get; set; } = string.Empty;

    /// <summary>
    /// 长度
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// 列描述
    /// </summary>
    public string ColumnDescription { get; set; } = string.Empty;

    /// <summary>
    /// 默认值
    /// </summary>
    public string DefaultValue { get; set; } = string.Empty;

    /// <summary>
    /// 是否可空
    /// </summary>
    public bool IsNullable { get; set; }

    /// <summary>
    /// 是否自增
    /// </summary>
    public bool IsIdentity { get; set; }

    /// <summary>
    /// 是否主键
    /// </summary>
    public bool IsPrimarykey { get; set; }

    /// <summary>
    /// 值
    /// </summary>
    public object? Value { get; set; }

    /// <summary>
    /// 小数位数
    /// </summary>
    public int DecimalDigits { get; set; }

    /// <summary>
    /// 精度
    /// </summary>
    public int Scale { get; set; }

    /// <summary>
    /// 是否数组
    /// </summary>
    public bool IsArray { get; set; }

    /// <summary>
    /// 是否Json
    /// </summary>
    public bool IsJson { get; set; }

    /// <summary>
    /// 是否无符号
    /// </summary>
   // public bool IsUnsigned { get; set; }

    /// <summary>
    /// 建表字段排序
    /// </summary>
    public int CreateTableFieldSort { get; set; }

    /// <summary>
    /// 插入服务器时间
    /// </summary>
    //public DateTime? InsertServerTime { get; set; }

    /// <summary>
    /// 插入SQL
    /// </summary>
    public string InsertSql { get; set; } = string.Empty;

    /// <summary>
    /// 更新服务器时间
    /// </summary>
   // public DateTime? UpdateServerTime { get; set; }

    /// <summary>
    /// 更新SQL
    /// </summary>
    public string UpdateSql { get; set; } = string.Empty;

    /// <summary>
    /// 参数类型
    /// </summary>
    //public string SqlParameterDbType { get; set; } = string.Empty;
}