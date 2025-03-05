#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbDiffLog.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 15:45
// 版本号 : V.0.0.1
// 描述    : 数据库差异日志实体
//===================================================================

using Lean.Hbt.Common.Enums;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Audit
{
    /// <summary>
    /// 数据库差异日志实体
    /// </summary>
    [SugarTable("hbt_mon_diff_log", "数据库差异日志表")]
    [SugarIndex("ix_tenant_dbdiff", nameof(TenantId), OrderByType.Asc)]
    public class HbtDbDiffLog : HbtBaseEntity
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        [SugarColumn(ColumnName = "log_level", ColumnDescription = "日志级别", ColumnDataType = "int", IsNullable = false)]
        public int LogLevel { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        [SugarColumn(ColumnName = "table_name", ColumnDescription = "表名", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string TableName { get; set; } = string.Empty;

        /// <summary>
        /// 变更类型（新建表、新增列、修改列、删除列）
        /// </summary>
        [SugarColumn(ColumnName = "change_type", ColumnDescription = "变更类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string ChangeType { get; set; } = string.Empty;

        /// <summary>
        /// 列名
        /// </summary>
        [SugarColumn(ColumnName = "column_name", ColumnDescription = "列名", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ColumnName { get; set; }

        /// <summary>
        /// 原数据类型
        /// </summary>
        [SugarColumn(ColumnName = "old_data_type", ColumnDescription = "原数据类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldDataType { get; set; }

        /// <summary>
        /// 新数据类型
        /// </summary>
        [SugarColumn(ColumnName = "new_data_type", ColumnDescription = "新数据类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewDataType { get; set; }

        /// <summary>
        /// 原长度
        /// </summary>
        [SugarColumn(ColumnName = "old_length", ColumnDescription = "原长度", ColumnDataType = "int", IsNullable = true)]
        public int? OldLength { get; set; }

        /// <summary>
        /// 新长度
        /// </summary>
        [SugarColumn(ColumnName = "new_length", ColumnDescription = "新长度", ColumnDataType = "int", IsNullable = true)]
        public int? NewLength { get; set; }

        /// <summary>
        /// 原是否允许为空（0不允许 1允许）
        /// </summary>
        [SugarColumn(ColumnName = "old_is_nullable", ColumnDescription = "原是否允许为空（0不允许 1允许）", ColumnDataType = "int", IsNullable = true)]
        public int? OldIsNullable { get; set; }

        /// <summary>
        /// 新是否允许为空（0不允许 1允许）
        /// </summary>
        [SugarColumn(ColumnName = "new_is_nullable", ColumnDescription = "新是否允许为空（0不允许 1允许）", ColumnDataType = "int", IsNullable = true)]
        public int? NewIsNullable { get; set; }

        /// <summary>
        /// 变更描述
        /// </summary>
        [SugarColumn(ColumnName = "change_description", ColumnDescription = "变更描述", Length = -1, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string ChangeDescription { get; set; } = string.Empty;

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

        /// <summary>
        /// 租户ID
        /// </summary>
        [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? TenantId { get; set; }

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
    }
} 