#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenColumnDefine.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 代码生成列定义实体
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Generator;

/// <summary>
/// 代码生成列定义实体
/// </summary>
[SugarTable("hbt_generator_column_define", "代码生成列定义表")]
[SugarIndex("ix_gen_column_define_table", nameof(TableId), OrderByType.Asc, nameof(ColumnName), OrderByType.Asc, true)]
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
    [SugarColumn(ColumnName = "column_name", ColumnDescription = "列名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ColumnName { get; set; } = string.Empty;

    /// <summary>
    /// 列说明
    /// </summary>
    [SugarColumn(ColumnName = "column_comment", ColumnDescription = "列说明", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ColumnComment { get; set; } = string.Empty;

    /// <summary>
    /// 数据库列类型
    /// </summary>
    [SugarColumn(ColumnName = "db_column_type", ColumnDescription = "库列类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string DbColumnType { get; set; } = string.Empty;

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
    /// 是否自增（1是）
    /// </summary>
    [SugarColumn(ColumnName = "is_increment", ColumnDescription = "自增", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsIncrement { get; set; } = 0;

    /// <summary>
    /// 列长度
    /// </summary>
    [SugarColumn(ColumnName = "column_length", ColumnDescription = "列长度", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ColumnLength { get; set; } = 0;

    /// <summary>
    /// 小数位数
    /// </summary>
    [SugarColumn(ColumnName = "decimal_digits", ColumnDescription = "小数位数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DecimalDigits { get; set; } = 0;

    /// <summary>
    /// 默认值
    /// </summary>
    [SugarColumn(ColumnName = "default_value", ColumnDescription = "默认值", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? DefaultValue { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;
}