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
using Lean.Hbt.Common.Models;

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
        QueryType = string.Empty;
        DisplayType = string.Empty;
        DictType = string.Empty;
    }

    /// <summary>
    /// 主键ID
    /// </summary>
    public long Id { get; set; }

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
    /// C#类型
    /// </summary>
    [Required(ErrorMessage = "C#类型不能为空")]
    [StringLength(50, ErrorMessage = "C#类型长度不能超过50个字符")]
    public string CsharpType { get; set; }

    /// <summary>
    /// C#列名（首字母大写）
    /// </summary>
    [Required(ErrorMessage = "C#列名不能为空")]
    [StringLength(50, ErrorMessage = "C#列名长度不能超过50个字符")]
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
    /// 是否为新增字段（1是）
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
    [StringLength(50, ErrorMessage = "查询方式长度不能超过50个字符")]
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
    [StringLength(50, ErrorMessage = "显示类型长度不能超过50个字符")]
    public string DisplayType { get; set; }

    /// <summary>
    /// 字典类型（用于下拉框、单选框、复选框等需要数据字典的字段）
    /// </summary>
    [StringLength(200, ErrorMessage = "字典类型长度不能超过200个字符")]
    public string DictType { get; set; }

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
    /// C#类型
    /// </summary>
    [Required(ErrorMessage = "C#类型不能为空")]
    [StringLength(50, ErrorMessage = "C#类型长度不能超过50个字符")]
    public string CsharpType { get; set; }

    /// <summary>
    /// C#列名（首字母大写）
    /// </summary>
    [Required(ErrorMessage = "C#列名不能为空")]
    [StringLength(50, ErrorMessage = "C#列名长度不能超过50个字符")]
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
    /// 是否为新增字段（1是）
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
    [StringLength(50, ErrorMessage = "查询方式长度不能超过50个字符")]
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
    [StringLength(50, ErrorMessage = "显示类型长度不能超过50个字符")]
    public string DisplayType { get; set; }

    /// <summary>
    /// 字典类型（用于下拉框、单选框、复选框等需要数据字典的字段）
    /// </summary>
    [StringLength(200, ErrorMessage = "字典类型长度不能超过200个字符")]
    public string DictType { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int OrderNum { get; set; }
}

/// <summary>
/// 代码生成列定义更新DTO
/// </summary>
public class HbtGenColumnDefineUpdateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenColumnDefineUpdateDto()
    {
        ColumnComment = string.Empty;
        DbColumnType = string.Empty;
        CsharpType = string.Empty;
        CsharpColumn = string.Empty;
        QueryType = string.Empty;
        DisplayType = string.Empty;
        DictType = string.Empty;
    }

    /// <summary>
    /// 主键ID
    /// </summary>
    [Required(ErrorMessage = "主键ID不能为空")]
    public long Id { get; set; }

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
    /// C#类型
    /// </summary>
    [Required(ErrorMessage = "C#类型不能为空")]
    [StringLength(50, ErrorMessage = "C#类型长度不能超过50个字符")]
    public string CsharpType { get; set; }

    /// <summary>
    /// C#列名（首字母大写）
    /// </summary>
    [Required(ErrorMessage = "C#列名不能为空")]
    [StringLength(50, ErrorMessage = "C#列名长度不能超过50个字符")]
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
    /// 是否为新增字段（1是）
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
    [StringLength(50, ErrorMessage = "查询方式长度不能超过50个字符")]
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
    [StringLength(50, ErrorMessage = "显示类型长度不能超过50个字符")]
    public string DisplayType { get; set; }

    /// <summary>
    /// 字典类型（用于下拉框、单选框、复选框等需要数据字典的字段）
    /// </summary>
    [StringLength(200, ErrorMessage = "字典类型长度不能超过200个字符")]
    public string DictType { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int OrderNum { get; set; }
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
        QueryType = string.Empty;
        DisplayType = string.Empty;
        DictType = string.Empty;
    }

    /// <summary>
    /// 列名
    /// </summary>
    public string ColumnName { get; set; }

    /// <summary>
    /// 列说明
    /// </summary>
    public string ColumnComment { get; set; }

    /// <summary>
    /// 数据库列类型
    /// </summary>
    public string DbColumnType { get; set; }

    /// <summary>
    /// C#类型
    /// </summary>
    public string CsharpType { get; set; }

    /// <summary>
    /// C#列名（首字母大写）
    /// </summary>
    public string CsharpColumn { get; set; }

    /// <summary>
    /// C#长度（字符串长度、数值类型的整数位数）
    /// </summary>
    public string CsharpLength { get; set; }

    /// <summary>
    /// C#小数位数（decimal等数值类型的小数位数）
    /// </summary>
    public string CsharpDecimalDigits { get; set; }

    /// <summary>
    /// 是否自增（1是）
    /// </summary>
    public string IsIncrement { get; set; }

    /// <summary>
    /// 是否主键（1是）
    /// </summary>
    public string IsPrimaryKey { get; set; }

    /// <summary>
    /// 是否必填（1是）
    /// </summary>
    public string IsRequired { get; set; }

    /// <summary>
    /// 是否为新增字段（1是）
    /// </summary>
    public string IsInsert { get; set; }

    /// <summary>
    /// 是否编辑字段（1是）
    /// </summary>
    public string IsEdit { get; set; }

    /// <summary>
    /// 是否列表字段（1是）
    /// </summary>
    public string IsList { get; set; }

    /// <summary>
    /// 是否查询字段（1是）
    /// </summary>
    public string IsQuery { get; set; }

    /// <summary>
    /// 查询方式（等于、不等于、大于、小于、范围）
    /// </summary>
    public string QueryType { get; set; }

    /// <summary>
    /// 是否排序字段（1是）
    /// </summary>
    public string IsSort { get; set; }

    /// <summary>
    /// 是否导出字段（1是）
    /// </summary>
    public string IsExport { get; set; }

    /// <summary>
    /// 显示类型（文本框、文本域、下拉框、复选框、单选框、日期控件）
    /// </summary>
    public string DisplayType { get; set; }

    /// <summary>
    /// 字典类型（用于下拉框、单选框、复选框等需要数据字典的字段）
    /// </summary>
    public string DictType { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public string OrderNum { get; set; }

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
        QueryType = string.Empty;
        DisplayType = string.Empty;
        DictType = string.Empty;
        CreateTime = DateTime.Now;
    }

    /// <summary>
    /// 列名
    /// </summary>
    public string ColumnName { get; set; }

    /// <summary>
    /// 列说明
    /// </summary>
    public string ColumnComment { get; set; }

    /// <summary>
    /// 数据库列类型
    /// </summary>
    public string DbColumnType { get; set; }

    /// <summary>
    /// C#类型
    /// </summary>
    public string CsharpType { get; set; }

    /// <summary>
    /// C#列名（首字母大写）
    /// </summary>
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
    /// 是否为新增字段（1是）
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
    /// 字典类型（用于下拉框、单选框、复选框等需要数据字典的字段）
    /// </summary>
    public string DictType { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
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
        QueryType = string.Empty;
        DisplayType = string.Empty;
        DictType = string.Empty;
    }

    /// <summary>
    /// 列名
    /// </summary>
    public string ColumnName { get; set; }

    /// <summary>
    /// 列说明
    /// </summary>
    public string ColumnComment { get; set; }

    /// <summary>
    /// 数据库列类型
    /// </summary>
    public string DbColumnType { get; set; }

    /// <summary>
    /// C#类型
    /// </summary>
    public string CsharpType { get; set; }

    /// <summary>
    /// C#列名（首字母大写）
    /// </summary>
    public string CsharpColumn { get; set; }

    /// <summary>
    /// C#长度（字符串长度、数值类型的整数位数）
    /// </summary>
    public string CsharpLength { get; set; }

    /// <summary>
    /// C#小数位数（decimal等数值类型的小数位数）
    /// </summary>
    public string CsharpDecimalDigits { get; set; }

    /// <summary>
    /// 是否自增（1是）
    /// </summary>
    public string IsIncrement { get; set; }

    /// <summary>
    /// 是否主键（1是）
    /// </summary>
    public string IsPrimaryKey { get; set; }

    /// <summary>
    /// 是否必填（1是）
    /// </summary>
    public string IsRequired { get; set; }

    /// <summary>
    /// 是否为新增字段（1是）
    /// </summary>
    public string IsInsert { get; set; }

    /// <summary>
    /// 是否编辑字段（1是）
    /// </summary>
    public string IsEdit { get; set; }

    /// <summary>
    /// 是否列表字段（1是）
    /// </summary>
    public string IsList { get; set; }

    /// <summary>
    /// 是否查询字段（1是）
    /// </summary>
    public string IsQuery { get; set; }

    /// <summary>
    /// 查询方式（等于、不等于、大于、小于、范围）
    /// </summary>
    public string QueryType { get; set; }

    /// <summary>
    /// 是否排序字段（1是）
    /// </summary>
    public string IsSort { get; set; }

    /// <summary>
    /// 是否导出字段（1是）
    /// </summary>
    public string IsExport { get; set; }

    /// <summary>
    /// 显示类型（文本框、文本域、下拉框、复选框、单选框、日期控件）
    /// </summary>
    public string DisplayType { get; set; }

    /// <summary>
    /// 字典类型（用于下拉框、单选框、复选框等需要数据字典的字段）
    /// </summary>
    public string DictType { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public string OrderNum { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
} 