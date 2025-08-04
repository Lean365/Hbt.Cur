#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenTableDefine.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 代码生成表定义实体
//===================================================================

using SqlSugar;

namespace Hbt.Cur.Domain.Entities.Generator;

/// <summary>
/// 代码生成表定义实体
/// </summary>
[SugarTable("hbt_generator_table_define", "代码生成表定义表")]
[SugarIndex("ix_gen_table_define_name", nameof(TableName), OrderByType.Asc, true)]
public class HbtGenTableDefine : HbtBaseEntity
{
    /// <summary>
    /// 数据库类型（0：SqlServer，1：MySQL，2：PostgreSQL，3：Oracle，4：SQLite）
    /// </summary>
    [SugarColumn(ColumnName = "db_type", ColumnDescription = "数据库类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DbType { get; set; } = 0;

    /// <summary>
    /// 连接字符串
    /// </summary>
    [SugarColumn(ColumnName = "connection_string", ColumnDescription = "连接字符串", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// 数据库名称
    /// </summary>
    [SugarColumn(ColumnName = "database_name", ColumnDescription = "数据库名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string DatabaseName { get; set; } = string.Empty;

    /// <summary>
    /// 表名
    /// </summary>
    [SugarColumn(ColumnName = "table_name", ColumnDescription = "表名", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// 表描述
    /// </summary>
    [SugarColumn(ColumnName = "table_comment", ColumnDescription = "表描述", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
    public string TableComment { get; set; } = string.Empty;

    /// <summary>
    /// 作者
    /// </summary>
    [SugarColumn(ColumnName = "author", ColumnDescription = "作者", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Author { get; set; } = string.Empty;

    /// <summary>
    /// 是否生成数据表（0：未生成，1：已生成）
    /// </summary>
    [SugarColumn(ColumnName = "is_gen_table", ColumnDescription = "生成数据表", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsGenTable { get; set; } = 0;

    /// <summary>
    /// 字段定义列表
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(HbtGenColumnDefine.TableId))]
    public List<HbtGenColumnDefine>? Columns { get; set; }
} 