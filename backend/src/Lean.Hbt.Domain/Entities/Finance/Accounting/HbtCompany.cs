#nullable enable
using SqlSugar;
using Lean.Hbt.Domain.Entities.Identity;

namespace Lean.Hbt.Domain.Entities.Finance.Controlling
{
    /// <summary>
    /// 公司代码实体类（字段参照SAP标准简写）
    /// </summary>
    [SugarTable("hbt_accounting_company", "公司代码表")]
    [SugarIndex("ix_bukrs", nameof(Bukrs), OrderByType.Asc, true)]
    public class HbtCompany : HbtBaseEntity
    {
        /// <summary>集团</summary>
        [SugarColumn(ColumnName = "mandt", ColumnDescription = "集团", Length = 6, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Mandt { get; set; }

        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "bukrs", ColumnDescription = "公司代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Bukrs { get; set; }

        /// <summary>公司代码或公司的名称</summary>
        [SugarColumn(ColumnName = "butxt", ColumnDescription = "公司代码或公司的名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Butxt { get; set; }

        /// <summary>城市</summary>
        [SugarColumn(ColumnName = "ort01", ColumnDescription = "城市", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ort01 { get; set; }

        /// <summary>国家键值</summary>
        [SugarColumn(ColumnName = "land1", ColumnDescription = "国家键值", Length = 6, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Land1 { get; set; }

        /// <summary>货币码</summary>
        [SugarColumn(ColumnName = "waers", ColumnDescription = "货币码", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Waers { get; set; }

        /// <summary>语言代码</summary>
        [SugarColumn(ColumnName = "spras", ColumnDescription = "语言代码", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Spras { get; set; }

        /// <summary>帐目表</summary>
        [SugarColumn(ColumnName = "ktopl", ColumnDescription = "帐目表", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ktopl { get; set; }

        /// <summary>最大汇率偏差幅度百分比</summary>
        [SugarColumn(ColumnName = "waabw", ColumnDescription = "最大汇率偏差幅度百分比", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Waabw { get; set; }

        /// <summary>会计年度变式</summary>
        [SugarColumn(ColumnName = "periv", ColumnDescription = "会计年度变式", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Periv { get; set; }

        /// <summary>分配标识符</summary>
        [SugarColumn(ColumnName = "kokfi", ColumnDescription = "分配标识符", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Kokfi { get; set; }

        /// <summary>公司</summary>
        [SugarColumn(ColumnName = "rcomp", ColumnDescription = "公司", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Rcomp { get; set; }

        /// <summary>地址</summary>
        [SugarColumn(ColumnName = "adrnr", ColumnDescription = "地址", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Adrnr { get; set; }

        /// <summary>增值税登记号</summary>
        [SugarColumn(ColumnName = "stceg", ColumnDescription = "增值税登记号", Length = 40, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Stceg { get; set; }

        /// <summary>财务管理范围</summary>
        [SugarColumn(ColumnName = "fikrs", ColumnDescription = "财务管理范围", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Fikrs { get; set; }
    }
}
