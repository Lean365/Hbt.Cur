#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenColumnDefine.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 代码生成列定义实体
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Generator;

/// <summary>
/// 代码生成列定义实体
/// </summary>
[SugarTable("hbt_gen_column_define", "代码生成列定义表")]
[SugarIndex("ix_gen_column_define_table", nameof(TableId), OrderByType.Asc, nameof(ColumnName), OrderByType.Asc, true)]
[SugarIndex("ix_gen_column_define_tenant", nameof(TenantId), OrderByType.Asc, nameof(TableId), OrderByType.Asc, nameof(ColumnName), OrderByType.Asc, true)]
public class HbtGenColumnDefine : HbtBaseEntity
{
    /// <summary>
    /// 表ID
    /// </summary>
    [SugarColumn(ColumnName = "table_id", ColumnDescription = "表ID", ColumnDataType = "bigint", IsNullable = false)]
    public long TableId { get; set; }

    /// <summary>
    /// 列名
    /// </summary>
    [SugarColumn(ColumnName = "column_name", ColumnDescription = "列名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string ColumnName { get; set; } = string.Empty;

    /// <summary>
    /// 列说明
    /// </summary>
    [SugarColumn(ColumnName = "column_comment", ColumnDescription = "列说明", Length = 200, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string ColumnComment { get; set; } = string.Empty;

    /// <summary>
    /// 数据库列类型
    /// </summary>
    [SugarColumn(ColumnName = "db_column_type", ColumnDescription = "库列类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string DbColumnType { get; set; } = string.Empty;

    /// <summary>
    /// C#类型
    /// </summary>
    [SugarColumn(ColumnName = "csharp_type", ColumnDescription = "C#类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string CsharpType { get; set; } = string.Empty;

    /// <summary>
    /// C#列名（首字母大写）
    /// </summary>
    [SugarColumn(ColumnName = "csharp_column", ColumnDescription = "C#列名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string CsharpColumn { get; set; } = string.Empty;

    /// <summary>
    /// C#长度（字符串长度、数值类型的整数位数）
    /// </summary>
    [SugarColumn(ColumnName = "csharp_length", ColumnDescription = "C#长度", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int CsharpLength { get; set; } = 0;

    /// <summary>
    /// C#小数位数（decimal等数值类型的小数位数）
    /// </summary>
    [SugarColumn(ColumnName = "csharp_decimal_digits", ColumnDescription = "C#小数位", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int CsharpDecimalDigits { get; set; } = 0;

    /// <summary>
    /// C#字段名（首字母小写）
    /// </summary>
    [SugarColumn(ColumnName = "csharp_field", ColumnDescription = "C#字段名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string? CsharpField { get; set; }

    /// <summary>
    /// 是否自增（1是）
    /// </summary>
    [SugarColumn(ColumnName = "is_increment", ColumnDescription = "自增", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsIncrement { get; set; } = 0;

    /// <summary>
    /// 是否主键（1是）
    /// </summary>
    [SugarColumn(ColumnName = "is_primary_key", ColumnDescription = "主键", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsPrimaryKey { get; set; } = 0;

    /// <summary>
    /// 是否必填（1是）
    /// </summary>
    [SugarColumn(ColumnName = "is_required", ColumnDescription = "必填", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsRequired { get; set; } = 0;

    /// <summary>
    /// 是否为新增字段（1是）
    /// </summary>
    [SugarColumn(ColumnName = "is_insert", ColumnDescription = "新增", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsInsert { get; set; } = 0;

    /// <summary>
    /// 是否编辑字段（1是）
    /// </summary>
    [SugarColumn(ColumnName = "is_edit", ColumnDescription = "编辑", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsEdit { get; set; } = 0;

    /// <summary>
    /// 是否列表字段（1是）
    /// </summary>
    [SugarColumn(ColumnName = "is_list", ColumnDescription = "列表", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsList { get; set; } = 0;

    /// <summary>
    /// 是否查询字段（1是）
    /// </summary>
    [SugarColumn(ColumnName = "is_query", ColumnDescription = "查询", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsQuery { get; set; } = 0;

    /// <summary>
    /// 查询方式（等于、不等于、大于、小于、范围）
    /// </summary>
    [SugarColumn(ColumnName = "query_type", ColumnDescription = "查询方式", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string QueryType { get; set; } = string.Empty;

    /// <summary>
    /// 是否排序字段（1是）
    /// </summary>
    [SugarColumn(ColumnName = "is_sort", ColumnDescription = "排序字段", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsSort { get; set; } = 0;

    /// <summary>
    /// 是否导出字段（1是）
    /// </summary>
    [SugarColumn(ColumnName = "is_export", ColumnDescription = "导出", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsExport { get; set; } = 0;

    /// <summary>
    /// 显示类型（文本框、文本域、下拉框、复选框、单选框、日期控件）
    /// </summary>
    [SugarColumn(ColumnName = "display_type", ColumnDescription = "显示类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string DisplayType { get; set; } = string.Empty;

    /// <summary>
    /// 字典类型（用于下拉框、单选框、复选框等需要数据字典的字段）
    /// </summary>
    [SugarColumn(ColumnName = "dict_type", ColumnDescription = "字典类型", Length = 200, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string DictType { get; set; } = string.Empty;

    /// <summary>
    /// 排序
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 租户ID
    /// </summary>
    [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    public long TenantId { get; set; } = 0;

    /// <summary>
    /// 租户
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(TenantId))]
    public HbtTenant? Tenant { get; set; }
}