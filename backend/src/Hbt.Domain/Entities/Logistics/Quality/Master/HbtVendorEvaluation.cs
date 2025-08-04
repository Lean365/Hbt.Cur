#nullable enable

using SqlSugar;

namespace Hbt.Domain.Entities.Logistics.Quality.Master
{
    /// <summary>
    /// 供应商评价（Vendor）实体
    /// </summary>
    [SugarTable("hbt_logistics_quality_vendor_evaluation", "卖方评价")]
    public class HbtVendorEvaluation : HbtBaseEntity
    {
        /// <summary>
        /// 供应商ID
        /// </summary>
        [SugarColumn(ColumnName = "vendor_id", ColumnDescription = "供应商ID", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string VendorId { get; set; } = string.Empty;

        /// <summary>
        /// 供应商名称
        /// </summary>
        [SugarColumn(ColumnName = "vendor_name", ColumnDescription = "供应商名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string VendorName { get; set; } = string.Empty;

        /// <summary>
        /// 评价日期
        /// </summary>
        [SugarColumn(ColumnName = "evaluation_date", ColumnDescription = "评价日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? EvaluationDate { get; set; }

        /// <summary>
        /// 评价人
        /// </summary>
        [SugarColumn(ColumnName = "evaluator", ColumnDescription = "评价人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Evaluator { get; set; }

        /// <summary>
        /// 评分
        /// </summary>
        [SugarColumn(ColumnName = "score", ColumnDescription = "评分", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? Score { get; set; }

        /// <summary>
        /// 评价内容
        /// </summary>
        [SugarColumn(ColumnName = "content", ColumnDescription = "评价内容", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Content { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "memo", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Memo { get; set; }
    }
} 