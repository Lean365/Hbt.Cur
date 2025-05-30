#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenColumnDefineDto.cs
// 创建者 : Claude
// 创建时间: 2024-03-21
// 版本号 : V0.0.1
// 描述   : 代码生成列定义DTO
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.Generator;

/// <summary>
/// 代码生成列定义DTO
/// </summary>
public class HbtGenColumnDefineDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenColumnDefineDto()
    {
        ColumnName = string.Empty;
        ColumnComment = string.Empty;
        DbColumnType = string.Empty;
        DefaultValue = string.Empty;
    }

    /// <summary>
    /// 主键ID
    /// </summary>
    [AdaptMember("Id")]
    public long GenColumnDefineId { get; set; }

    /// <summary>
    /// 表ID
    /// </summary>
    [Required(ErrorMessage = "表ID不能为空")]
    public long TableId { get; set; }

    /// <summary>
    /// 列名
    /// </summary>
    [Required(ErrorMessage = "列名不能为空")]
    [StringLength(50, ErrorMessage = "列名长度不能超过50个字符")]
    public string ColumnName { get; set; }

    /// <summary>
    /// 列说明
    /// </summary>
    [Required(ErrorMessage = "列说明不能为空")]
    [StringLength(200, ErrorMessage = "列说明长度不能超过200个字符")]
    public string ColumnComment { get; set; }

    /// <summary>
    /// 数据库列类型
    /// </summary>
    [Required(ErrorMessage = "数据库列类型不能为空")]
    [StringLength(50, ErrorMessage = "数据库列类型长度不能超过50个字符")]
    public string DbColumnType { get; set; }

    /// <summary>
    /// 是否主键（1是）
    /// </summary>
    public int IsPrimaryKey { get; set; }

    /// <summary>
    /// 是否必填（1是）
    /// </summary>
    public int IsRequired { get; set; }

    /// <summary>
    /// 是否自增（1是）
    /// </summary>
    public int IsIncrement { get; set; }

    /// <summary>
    /// 列长度
    /// </summary>
    public int ColumnLength { get; set; }

    /// <summary>
    /// 小数位数
    /// </summary>
    public int DecimalDigits { get; set; }

    /// <summary>
    /// 默认值
    /// </summary>
    [StringLength(200, ErrorMessage = "默认值长度不能超过200个字符")]
    public string? DefaultValue { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int OrderNum { get; set; }
}

/// <summary>
/// 代码生成列定义查询DTO
/// </summary>
public class HbtGenColumnDefineQueryDto : HbtPagedQuery
{
    /// <summary>
    /// 表ID
    /// </summary>
    public long? TableId { get; set; }

    /// <summary>
    /// 列名
    /// </summary>
    [StringLength(50, ErrorMessage = "列名长度不能超过50个字符")]
    public string? ColumnName { get; set; }

    /// <summary>
    /// 列说明
    /// </summary>
    [StringLength(200, ErrorMessage = "列说明长度不能超过200个字符")]
    public string? ColumnComment { get; set; }
}

/// <summary>
/// 代码生成列定义创建DTO
/// </summary>
public class HbtGenColumnDefineCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenColumnDefineCreateDto()
    {
        ColumnName = string.Empty;
        ColumnComment = string.Empty;
        DbColumnType = string.Empty;
        DefaultValue = string.Empty;
    }

    /// <summary>
    /// 表ID
    /// </summary>
    [Required(ErrorMessage = "表ID不能为空")]
    public long TableId { get; set; }

    /// <summary>
    /// 列名
    /// </summary>
    [Required(ErrorMessage = "列名不能为空")]
    [StringLength(50, ErrorMessage = "列名长度不能超过50个字符")]
    public string ColumnName { get; set; }

    /// <summary>
    /// 列说明
    /// </summary>
    [Required(ErrorMessage = "列说明不能为空")]
    [StringLength(200, ErrorMessage = "列说明长度不能超过200个字符")]
    public string ColumnComment { get; set; }

    /// <summary>
    /// 数据库列类型
    /// </summary>
    [Required(ErrorMessage = "数据库列类型不能为空")]
    [StringLength(50, ErrorMessage = "数据库列类型长度不能超过50个字符")]
    public string DbColumnType { get; set; }

    /// <summary>
    /// 是否主键（1是）
    /// </summary>
    public int IsPrimaryKey { get; set; }

    /// <summary>
    /// 是否必填（1是）
    /// </summary>
    public int IsRequired { get; set; }

    /// <summary>
    /// 是否自增（1是）
    /// </summary>
    public int IsIncrement { get; set; }

    /// <summary>
    /// 列长度
    /// </summary>
    public int ColumnLength { get; set; }

    /// <summary>
    /// 小数位数
    /// </summary>
    public int DecimalDigits { get; set; }

    /// <summary>
    /// 默认值
    /// </summary>
    [StringLength(200, ErrorMessage = "默认值长度不能超过200个字符")]
    public string? DefaultValue { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int OrderNum { get; set; }
}

/// <summary>
/// 代码生成列定义更新DTO
/// </summary>
public class HbtGenColumnDefineUpdateDto : HbtGenColumnDefineCreateDto
{
    /// <summary>
    /// 主键ID
    /// </summary>
    [AdaptMember("Id")]
    public long GenColumnDefineId { get; set; }
}

/// <summary>
/// 代码生成列定义导入DTO
/// </summary>
public class HbtGenColumnDefineImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenColumnDefineImportDto()
    {
        ColumnName = string.Empty;
        ColumnComment = string.Empty;
        DbColumnType = string.Empty;
        DefaultValue = string.Empty;
    }

    /// <summary>
    /// 表ID
    /// </summary>
    [Required(ErrorMessage = "表ID不能为空")]
    public long TableId { get; set; }

    /// <summary>
    /// 列名
    /// </summary>
    [Required(ErrorMessage = "列名不能为空")]
    [StringLength(50, ErrorMessage = "列名长度不能超过50个字符")]
    public string ColumnName { get; set; }

    /// <summary>
    /// 列说明
    /// </summary>
    [Required(ErrorMessage = "列说明不能为空")]
    [StringLength(200, ErrorMessage = "列说明长度不能超过200个字符")]
    public string ColumnComment { get; set; }

    /// <summary>
    /// 数据库列类型
    /// </summary>
    [Required(ErrorMessage = "数据库列类型不能为空")]
    [StringLength(50, ErrorMessage = "数据库列类型长度不能超过50个字符")]
    public string DbColumnType { get; set; }

    /// <summary>
    /// 是否主键（1是）
    /// </summary>
    public int IsPrimaryKey { get; set; }

    /// <summary>
    /// 是否必填（1是）
    /// </summary>
    public int IsRequired { get; set; }

    /// <summary>
    /// 是否自增（1是）
    /// </summary>
    public int IsIncrement { get; set; }

    /// <summary>
    /// 列长度
    /// </summary>
    public int ColumnLength { get; set; }

    /// <summary>
    /// 小数位数
    /// </summary>
    public int DecimalDigits { get; set; }

    /// <summary>
    /// 默认值
    /// </summary>
    [StringLength(200, ErrorMessage = "默认值长度不能超过200个字符")]
    public string? DefaultValue { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int OrderNum { get; set; }
}

/// <summary>
/// 代码生成列定义导出DTO
/// </summary>
public class HbtGenColumnDefineExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenColumnDefineExportDto()
    {
        ColumnName = string.Empty;
        ColumnComment = string.Empty;
        DbColumnType = string.Empty;
        DefaultValue = string.Empty;
    }

    /// <summary>
    /// 表ID
    /// </summary>
    [Required(ErrorMessage = "表ID不能为空")]
    public long TableId { get; set; }

    /// <summary>
    /// 列名
    /// </summary>
    [Required(ErrorMessage = "列名不能为空")]
    [StringLength(50, ErrorMessage = "列名长度不能超过50个字符")]
    public string ColumnName { get; set; }

    /// <summary>
    /// 列说明
    /// </summary>
    [Required(ErrorMessage = "列说明不能为空")]
    [StringLength(200, ErrorMessage = "列说明长度不能超过200个字符")]
    public string ColumnComment { get; set; }

    /// <summary>
    /// 数据库列类型
    /// </summary>
    [Required(ErrorMessage = "数据库列类型不能为空")]
    [StringLength(50, ErrorMessage = "数据库列类型长度不能超过50个字符")]
    public string DbColumnType { get; set; }

    /// <summary>
    /// 是否主键（1是）
    /// </summary>
    public int IsPrimaryKey { get; set; }

    /// <summary>
    /// 是否必填（1是）
    /// </summary>
    public int IsRequired { get; set; }

    /// <summary>
    /// 是否自增（1是）
    /// </summary>
    public int IsIncrement { get; set; }

    /// <summary>
    /// 列长度
    /// </summary>
    public int ColumnLength { get; set; }

    /// <summary>
    /// 小数位数
    /// </summary>
    public int DecimalDigits { get; set; }

    /// <summary>
    /// 默认值
    /// </summary>
    [StringLength(200, ErrorMessage = "默认值长度不能超过200个字符")]
    public string? DefaultValue { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int OrderNum { get; set; }
}

/// <summary>
/// 代码生成列定义模板DTO
/// </summary>
public class HbtGenColumnDefineTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenColumnDefineTemplateDto()
    {
        ColumnName = string.Empty;
        ColumnComment = string.Empty;
        DbColumnType = string.Empty;
        DefaultValue = string.Empty;
    }

    /// <summary>
    /// 表ID
    /// </summary>
    [Required(ErrorMessage = "表ID不能为空")]
    public long TableId { get; set; }

    /// <summary>
    /// 列名
    /// </summary>
    [Required(ErrorMessage = "列名不能为空")]
    [StringLength(50, ErrorMessage = "列名长度不能超过50个字符")]
    public string ColumnName { get; set; }

    /// <summary>
    /// 列说明
    /// </summary>
    [Required(ErrorMessage = "列说明不能为空")]
    [StringLength(200, ErrorMessage = "列说明长度不能超过200个字符")]
    public string ColumnComment { get; set; }

    /// <summary>
    /// 数据库列类型
    /// </summary>
    [Required(ErrorMessage = "数据库列类型不能为空")]
    [StringLength(50, ErrorMessage = "数据库列类型长度不能超过50个字符")]
    public string DbColumnType { get; set; }

    /// <summary>
    /// 是否主键（1是）
    /// </summary>
    public int IsPrimaryKey { get; set; }

    /// <summary>
    /// 是否必填（1是）
    /// </summary>
    public int IsRequired { get; set; }

    /// <summary>
    /// 是否自增（1是）
    /// </summary>
    public int IsIncrement { get; set; }

    /// <summary>
    /// 列长度
    /// </summary>
    public int ColumnLength { get; set; }

    /// <summary>
    /// 小数位数
    /// </summary>
    public int DecimalDigits { get; set; }

    /// <summary>
    /// 默认值
    /// </summary>
    [StringLength(200, ErrorMessage = "默认值长度不能超过200个字符")]
    public string? DefaultValue { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int OrderNum { get; set; }
}