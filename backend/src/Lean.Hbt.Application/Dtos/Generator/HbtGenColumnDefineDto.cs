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
        CsharpType = string.Empty;
        CsharpColumn = string.Empty;
        CsharpField = string.Empty;
        QueryType = string.Empty;
        DisplayType = string.Empty;
        DictType = string.Empty;
        CreateBy = string.Empty;
        UpdateBy = string.Empty;
    }

    /// <summary>
    /// 主键ID
    /// </summary>
    public long GenColumnId { get; set; }

    /// <summary>
    /// 表ID
    /// </summary>
    [Required(ErrorMessage = "表ID不能为空")]
    public long TableId { get; set; }

    /// <summary>
    /// 列名
    /// </summary>
    [Required(ErrorMessage = "列名不能为空")]
    [StringLength(100, ErrorMessage = "列名长度不能超过100个字符")]
    public string ColumnName { get; set; }

    /// <summary>
    /// 列注释
    /// </summary>
    [Required(ErrorMessage = "列描述不能为空")]
    [StringLength(200, ErrorMessage = "列描述长度不能超过200个字符")]
    public string ColumnComment { get; set; }

    /// <summary>
    /// 数据库列类型
    /// </summary>
    [Required(ErrorMessage = "数据库列类型不能为空")]
    public string DbColumnType { get; set; }

    /// <summary>
    /// 列类型
    /// </summary>
    [Required(ErrorMessage = "列类型不能为空")]
    public string CsharpType { get; set; }

    /// <summary>
    /// C#列名（首字母大写）
    /// </summary>
    [Required(ErrorMessage = "C#列名不能为空")]
    public string CsharpColumn { get; set; }

    /// <summary>
    /// C#长度（字符串长度、数值类型的整数位数）
    /// </summary>
    public int CsharpLength { get; set; }

    /// <summary>
    /// C#小数位数（decimal等数值类型的小数位数）
    /// </summary>
    public int CsharpDecimalDigits { get; set; }

    /// <summary>
    /// 实体字段名
    /// </summary>
    [Required(ErrorMessage = "实体字段名不能为空")]
    public string CsharpField { get; set; }

    /// <summary>
    /// 是否自增（1是）
    /// </summary>
    public int IsIncrement { get; set; }

    /// <summary>
    /// 是否主键（1是）
    /// </summary>
    public int IsPrimaryKey { get; set; }

    /// <summary>
    /// 是否必填（1是）
    /// </summary>
    public int IsRequired { get; set; }

    /// <summary>
    /// 是否列表字段（1是）
    /// </summary>

    public int IsInsert { get; set; }
    /// <summary>
    /// 是否编辑字段（1是）
    /// </summary>

    public int IsEdit { get; set; }

    /// <summary>
    /// 是否列表字段（1是）
    /// </summary>
    public int IsList { get; set; }

    /// <summary>
    /// 是否查询字段（1是）
    /// </summary>
    public int IsQuery { get; set; }

    /// <summary>
    /// 查询方式（等于、不等于、大于、小于、范围）
    /// </summary>
    public string QueryType { get; set; }

    /// <summary>
    /// 是否排序字段（1是）
    /// </summary>
    public int IsSort { get; set; }

    /// <summary>
    /// 是否导出字段（1是）
    /// </summary>
    public int IsExport { get; set; }

    /// <summary>
    /// 显示类型（文本框、文本域、下拉框、复选框、单选框、日期控件）
    /// </summary>
    public string DisplayType { get; set; }

    /// <summary>
    /// 字典类型
    /// </summary>
    public string DictType { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    public long TenantId { get; set; }

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
        CsharpType = string.Empty;
        CsharpColumn = string.Empty;
        QueryType = string.Empty;
        DisplayType = string.Empty;
        DictType = string.Empty;
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
    [StringLength(100, ErrorMessage = "列名长度不能超过100个字符")]
    public string ColumnName { get; set; }

    /// <summary>
    /// 列注释
    /// </summary>
    [Required(ErrorMessage = "列描述不能为空")]
    [StringLength(200, ErrorMessage = "列描述长度不能超过200个字符")]
    public string ColumnComment { get; set; }

    /// <summary>
    /// 数据库列类型
    /// </summary>
    [Required(ErrorMessage = "数据库列类型不能为空")]
    public string DbColumnType { get; set; }

    /// <summary>
    /// 列类型
    /// </summary>
    [Required(ErrorMessage = "列类型不能为空")]
    public string CsharpType { get; set; }

    /// <summary>
    /// C#列名（首字母大写）
    /// </summary>
    [Required(ErrorMessage = "C#列名不能为空")]
    public string CsharpColumn { get; set; }

    /// <summary>
    /// C#长度（字符串长度、数值类型的整数位数）
    /// </summary>
    public int CsharpLength { get; set; }

    /// <summary>
    /// C#小数位数（decimal等数值类型的小数位数）
    /// </summary>
    public int CsharpDecimalDigits { get; set; }

    /// <summary>
    /// 实体字段名
    /// </summary>
    [Required(ErrorMessage = "实体字段名不能为空")]
    public string CsharpField { get; set; }

    /// <summary>
    /// 是否自增（1是）
    /// </summary>
    public int IsIncrement { get; set; }

    /// <summary>
    /// 是否主键（1是）
    /// </summary>
    public int IsPrimaryKey { get; set; }

    /// <summary>
    /// 是否必填（1是）
    /// </summary>
    public int IsRequired { get; set; }

    /// <summary>
    /// 是否列表字段（1是）
    /// </summary>

    public int IsInsert { get; set; }
    /// <summary>
    /// 是否编辑字段（1是）
    /// </summary>

    public int IsEdit { get; set; }

    /// <summary>
    /// 是否列表字段（1是）
    /// </summary>
    public int IsList { get; set; }

    /// <summary>
    /// 是否查询字段（1是）
    /// </summary>
    public int IsQuery { get; set; }

    /// <summary>
    /// 查询方式（等于、不等于、大于、小于、范围）
    /// </summary>
    public string QueryType { get; set; }

    /// <summary>
    /// 是否排序字段（1是）
    /// </summary>
    public int IsSort { get; set; }

    /// <summary>
    /// 是否导出字段（1是）
    /// </summary>
    public int IsExport { get; set; }

    /// <summary>
    /// 显示类型（文本框、文本域、下拉框、复选框、单选框、日期控件）
    /// </summary>
    public string DisplayType { get; set; }

    /// <summary>
    /// 字典类型
    /// </summary>
    public string DictType { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    public long TenantId { get; set; }
}

/// <summary>
/// 代码生成列定义更新DTO
/// </summary>
public class HbtGenColumnDefineUpdateDto : HbtGenColumnCreateDto
{
    /// <summary>
    /// 主键ID
    /// </summary>
    public long GenColumnId { get; set; }
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
        CsharpType = string.Empty;
        CsharpColumn = string.Empty;
        CsharpField = string.Empty;
        QueryType = string.Empty;
        DisplayType = string.Empty;
        DictType = string.Empty;
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
    [StringLength(100, ErrorMessage = "列名长度不能超过100个字符")]
    public string ColumnName { get; set; }

    /// <summary>
    /// 列注释
    /// </summary>
    [Required(ErrorMessage = "列描述不能为空")]
    [StringLength(200, ErrorMessage = "列描述长度不能超过200个字符")]
    public string ColumnComment { get; set; }

    /// <summary>
    /// 数据库列类型
    /// </summary>
    [Required(ErrorMessage = "数据库列类型不能为空")]
    public string DbColumnType { get; set; }

    /// <summary>
    /// 列类型
    /// </summary>
    [Required(ErrorMessage = "列类型不能为空")]
    public string CsharpType { get; set; }

    /// <summary>
    /// C#列名（首字母大写）
    /// </summary>
    [Required(ErrorMessage = "C#列名不能为空")]
    public string CsharpColumn { get; set; }

    /// <summary>
    /// C#长度（字符串长度、数值类型的整数位数）
    /// </summary>
    public int CsharpLength { get; set; }

    /// <summary>
    /// C#小数位数（decimal等数值类型的小数位数）
    /// </summary>
    public int CsharpDecimalDigits { get; set; }

    /// <summary>
    /// 实体字段名
    /// </summary>
    [Required(ErrorMessage = "实体字段名不能为空")]
    public string CsharpField { get; set; }

    /// <summary>
    /// 是否自增（1是）
    /// </summary>
    public int IsIncrement { get; set; }

    /// <summary>
    /// 是否主键（1是）
    /// </summary>
    public int IsPrimaryKey { get; set; }

    /// <summary>
    /// 是否必填（1是）
    /// </summary>
    public int IsRequired { get; set; }

    /// <summary>
    /// 是否列表字段（1是）
    /// </summary>

    public int IsInsert { get; set; }
    /// <summary>
    /// 是否编辑字段（1是）
    /// </summary>

    public int IsEdit { get; set; }

    /// <summary>
    /// 是否列表字段（1是）
    /// </summary>
    public int IsList { get; set; }

    /// <summary>
    /// 是否查询字段（1是）
    /// </summary>
    public int IsQuery { get; set; }

    /// <summary>
    /// 查询方式（等于、不等于、大于、小于、范围）
    /// </summary>
    public string QueryType { get; set; }

    /// <summary>
    /// 是否排序字段（1是）
    /// </summary>
    public int IsSort { get; set; }

    /// <summary>
    /// 是否导出字段（1是）
    /// </summary>
    public int IsExport { get; set; }

    /// <summary>
    /// 显示类型（文本框、文本域、下拉框、复选框、单选框、日期控件）
    /// </summary>
    public string DisplayType { get; set; }

    /// <summary>
    /// 字典类型
    /// </summary>
    public string DictType { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    public long TenantId { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
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
        CsharpType = string.Empty;
        CsharpColumn = string.Empty;
        CsharpField = string.Empty;
        QueryType = string.Empty;
        DisplayType = string.Empty;
        DictType = string.Empty;
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
    [StringLength(100, ErrorMessage = "列名长度不能超过100个字符")]
    public string ColumnName { get; set; }

    /// <summary>
    /// 列注释
    /// </summary>
    [Required(ErrorMessage = "列描述不能为空")]
    [StringLength(200, ErrorMessage = "列描述长度不能超过200个字符")]
    public string ColumnComment { get; set; }

    /// <summary>
    /// 数据库列类型
    /// </summary>
    [Required(ErrorMessage = "数据库列类型不能为空")]
    public string DbColumnType { get; set; }

    /// <summary>
    /// 列类型
    /// </summary>
    [Required(ErrorMessage = "列类型不能为空")]
    public string CsharpType { get; set; }

    /// <summary>
    /// C#列名（首字母大写）
    /// </summary>
    [Required(ErrorMessage = "C#列名不能为空")]
    public string CsharpColumn { get; set; }

    /// <summary>
    /// C#长度（字符串长度、数值类型的整数位数）
    /// </summary>
    public int CsharpLength { get; set; }

    /// <summary>
    /// C#小数位数（decimal等数值类型的小数位数）
    /// </summary>
    public int CsharpDecimalDigits { get; set; }

    /// <summary>
    /// 实体字段名
    /// </summary>
    [Required(ErrorMessage = "实体字段名不能为空")]
    public string CsharpField { get; set; }

    /// <summary>
    /// 是否自增（1是）
    /// </summary>
    public int IsIncrement { get; set; }

    /// <summary>
    /// 是否主键（1是）
    /// </summary>
    public int IsPrimaryKey { get; set; }

    /// <summary>
    /// 是否必填（1是）
    /// </summary>
    public int IsRequired { get; set; }

    /// <summary>
    /// 是否列表字段（1是）
    /// </summary>

    public int IsInsert { get; set; }
    /// <summary>
    /// 是否编辑字段（1是）
    /// </summary>

    public int IsEdit { get; set; }

    /// <summary>
    /// 是否列表字段（1是）
    /// </summary>
    public int IsList { get; set; }

    /// <summary>
    /// 是否查询字段（1是）
    /// </summary>
    public int IsQuery { get; set; }

    /// <summary>
    /// 查询方式（等于、不等于、大于、小于、范围）
    /// </summary>
    public string QueryType { get; set; }

    /// <summary>
    /// 是否排序字段（1是）
    /// </summary>
    public int IsSort { get; set; }

    /// <summary>
    /// 是否导出字段（1是）
    /// </summary>
    public int IsExport { get; set; }

    /// <summary>
    /// 显示类型（文本框、文本域、下拉框、复选框、单选框、日期控件）
    /// </summary>
    public string DisplayType { get; set; }

    /// <summary>
    /// 字典类型
    /// </summary>
    public string DictType { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    public long TenantId { get; set; }
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
        CsharpType = string.Empty;
        CsharpColumn = string.Empty;
        CsharpField = string.Empty;
        QueryType = string.Empty;
        DisplayType = string.Empty;
        DictType = string.Empty;
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
    [StringLength(100, ErrorMessage = "列名长度不能超过100个字符")]
    public string ColumnName { get; set; }

    /// <summary>
    /// 列注释
    /// </summary>
    [Required(ErrorMessage = "列描述不能为空")]
    [StringLength(200, ErrorMessage = "列描述长度不能超过200个字符")]
    public string ColumnComment { get; set; }

    /// <summary>
    /// 数据库列类型
    /// </summary>
    [Required(ErrorMessage = "数据库列类型不能为空")]
    public string DbColumnType { get; set; }

    /// <summary>
    /// 列类型
    /// </summary>
    [Required(ErrorMessage = "列类型不能为空")]
    public string CsharpType { get; set; }

    /// <summary>
    /// C#列名（首字母大写）
    /// </summary>
    [Required(ErrorMessage = "C#列名不能为空")]
    public string CsharpColumn { get; set; }

    /// <summary>
    /// C#长度（字符串长度、数值类型的整数位数）
    /// </summary>
    public int CsharpLength { get; set; }

    /// <summary>
    /// C#小数位数（decimal等数值类型的小数位数）
    /// </summary>
    public int CsharpDecimalDigits { get; set; }

    /// <summary>
    /// 实体字段名
    /// </summary>
    [Required(ErrorMessage = "实体字段名不能为空")]
    public string CsharpField { get; set; }

    /// <summary>
    /// 是否自增（1是）
    /// </summary>
    public int IsIncrement { get; set; }

    /// <summary>
    /// 是否主键（1是）
    /// </summary>
    public int IsPrimaryKey { get; set; }

    /// <summary>
    /// 是否必填（1是）
    /// </summary>
    public int IsRequired { get; set; }

    /// <summary>
    /// 是否列表字段（1是）
    /// </summary>

    public int IsInsert { get; set; }
    /// <summary>
    /// 是否编辑字段（1是）
    /// </summary>

    public int IsEdit { get; set; }

    /// <summary>
    /// 是否列表字段（1是）
    /// </summary>
    public int IsList { get; set; }

    /// <summary>
    /// 是否查询字段（1是）
    /// </summary>
    public int IsQuery { get; set; }

    /// <summary>
    /// 查询方式（等于、不等于、大于、小于、范围）
    /// </summary>
    public string QueryType { get; set; }

    /// <summary>
    /// 是否排序字段（1是）
    /// </summary>
    public int IsSort { get; set; }

    /// <summary>
    /// 是否导出字段（1是）
    /// </summary>
    public int IsExport { get; set; }

    /// <summary>
    /// 显示类型（文本框、文本域、下拉框、复选框、单选框、日期控件）
    /// </summary>
    public string DisplayType { get; set; }

    /// <summary>
    /// 字典类型
    /// </summary>
    public string DictType { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    public long TenantId { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}