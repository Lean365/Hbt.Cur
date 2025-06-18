using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Domain.Entities.Logistics.Quality
{
    /// <summary>
    /// 客户评估表
    /// </summary>
    [SugarTable("hbt_logistics_quality_customer_evaluation", "客户评估表")]
    [SugarIndex("ix_customer_evaluation_code", nameof(EvalBatch), OrderByType.Asc, true)]
    public class HbtCustomerEvaluation : HbtBaseEntity
    {
        /// <summary>集团</summary>
        [SugarColumn(ColumnName = "mandt", ColumnDescription = "集团", Length = 6, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Mandt { get; set; }
        /// <summary>客户编号</summary>
        [SugarColumn(ColumnName = "kunnr", ColumnDescription = "客户编号", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Kunnr { get; set; }
        /// <summary>客户名称</summary>
        [SugarColumn(ColumnName = "name1", ColumnDescription = "客户名称", Length = 35, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Name1 { get; set; }
        /// <summary>国家</summary>
        [SugarColumn(ColumnName = "land1", ColumnDescription = "国家", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Land1 { get; set; }
        /// <summary>城市</summary>
        [SugarColumn(ColumnName = "ort01", ColumnDescription = "城市", Length = 35, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ort01 { get; set; }
        /// <summary>评估批次</summary>
        [SugarColumn(ColumnName = "evalbatch", ColumnDescription = "评估批次", Length = 12, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? EvalBatch { get; set; }
        /// <summary>评估ID</summary>
        [SugarColumn(ColumnName = "evalid", ColumnDescription = "评估ID", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EvalId { get; set; }
        /// <summary>评估日期</summary>
        [SugarColumn(ColumnName = "evaldate", ColumnDescription = "评估日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? EvalDate { get; set; }
        /// <summary>评估结果</summary>
        [SugarColumn(ColumnName = "evalresult", ColumnDescription = "评估结果", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EvalResult { get; set; }
        /// <summary>评估人</summary>
        [SugarColumn(ColumnName = "evaluser", ColumnDescription = "评估人", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EvalUser { get; set; }
        /// <summary>评估说明</summary>
        [SugarColumn(ColumnName = "evaltxt", ColumnDescription = "评估说明", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EvalTxt { get; set; }
        /// <summary>创建日期</summary>
        [SugarColumn(ColumnName = "erdat", ColumnDescription = "创建日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Erdat { get; set; }
        /// <summary>创建人</summary>
        [SugarColumn(ColumnName = "ernam", ColumnDescription = "创建人", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ernam { get; set; }
        /// <summary>修改日期</summary>
        [SugarColumn(ColumnName = "aedat", ColumnDescription = "修改日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Aedat { get; set; }
        /// <summary>修改人</summary>
        [SugarColumn(ColumnName = "aenam", ColumnDescription = "修改人", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Aenam { get; set; }
        /// <summary>删除标志</summary>
        [SugarColumn(ColumnName = "loekz", ColumnDescription = "删除标志", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Loekz { get; set; }
        /// <summary>删除日期</summary>
        [SugarColumn(ColumnName = "lodat", ColumnDescription = "删除日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Lodat { get; set; }
        /// <summary>删除人</summary>
        [SugarColumn(ColumnName = "lousr", ColumnDescription = "删除人", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Lousr { get; set; }
    }
} 