#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSqlDiffLog.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 15:45
// 版本号 : V.0.0.1
// 描述    : SqlSugar差异日志实体
//===================================================================

namespace Hbt.Domain.Entities.Audit
{
    /// <summary>
    /// SqlSugar差异日志实体
    /// </summary>
    [SugarTable("hbt_audit_diff_log", "差异日志")]
    public class HbtSqlDiffLog : HbtBaseEntity
    {
        /// <summary>
        /// 差异类型（Insert、Update、Delete）
        /// </summary>
        [SugarColumn(ColumnName = "diff_type", ColumnDescription = "差异类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string DiffType { get; set; } = string.Empty;

        /// <summary>
        /// 表名
        /// </summary>
        [SugarColumn(ColumnName = "table_name", ColumnDescription = "表名", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string TableName { get; set; } = string.Empty;

        /// <summary>
        /// 业务名称
        /// </summary>
        [SugarColumn(ColumnName = "business_name", ColumnDescription = "业务名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BusinessName { get; set; }

        /// <summary>
        /// 主键值
        /// </summary>
        [SugarColumn(ColumnName = "primary_key", ColumnDescription = "主键值", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PrimaryKey { get; set; }

        /// <summary>
        /// 变更前的数据（JSON格式）
        /// </summary>
        [SugarColumn(ColumnName = "before_data", ColumnDescription = "变更前的数据", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BeforeData { get; set; }

        /// <summary>
        /// 变更后的数据（JSON格式）
        /// </summary>
        [SugarColumn(ColumnName = "after_data", ColumnDescription = "变更后的数据", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AfterData { get; set; }

        /// <summary>
        /// 差异字段（JSON格式）
        /// </summary>
        [SugarColumn(ColumnName = "diff_fields", ColumnDescription = "差异字段", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DiffFields { get; set; }

        /// <summary>
        /// 执行的SQL语句
        /// </summary>
        [SugarColumn(ColumnName = "execute_sql", ColumnDescription = "执行的SQL语句", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExecuteSql { get; set; }

        /// <summary>
        /// SQL参数（JSON格式）
        /// </summary>
        [SugarColumn(ColumnName = "sql_parameters", ColumnDescription = "SQL参数", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SqlParameters { get; set; }
    }
}