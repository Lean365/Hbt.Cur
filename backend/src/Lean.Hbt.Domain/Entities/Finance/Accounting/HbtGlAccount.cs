using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Finance.Accounting
{
    /// <summary>
    /// 主科目表
    /// </summary>
    [SugarTable("hbt_accounting_gl_account", "主科目表")]
    [SugarIndex("ix_gl_account_code", nameof(Saknr), OrderByType.Asc, true)]
    public class HbtGlAccount : HbtBaseEntity
    {
        /// <summary>集团</summary>
        [SugarColumn(ColumnName = "mandt", ColumnDescription = "集团", Length = 6, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Mandt { get; set; }
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "bukrs", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Bukrs { get; set; }
        /// <summary>科目编码</summary>
        [SugarColumn(ColumnName = "saknr", ColumnDescription = "科目编码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Saknr { get; set; }
        /// <summary>科目名称</summary>
        [SugarColumn(ColumnName = "txt20", ColumnDescription = "科目名称", Length = 40, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Txt20 { get; set; }
        /// <summary>科目类型</summary>
        [SugarColumn(ColumnName = "ktopl", ColumnDescription = "科目类型", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ktopl { get; set; }
        /// <summary>币种</summary>
        [SugarColumn(ColumnName = "waers", ColumnDescription = "币种", Length = 6, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Waers { get; set; }
        /// <summary>状态</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }

    }
} 