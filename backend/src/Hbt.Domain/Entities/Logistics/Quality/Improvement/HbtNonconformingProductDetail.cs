#nullable enable

namespace Hbt.Domain.Entities.Logistics.Quality.Improvement
{
    /// <summary>
    /// 不合格品明细表
    /// </summary>
    [SugarTable("hbt_logistics_quality_nonconforming_product_detail", "不合格品明细表")]
    public class HbtNonconformingProductDetail : HbtBaseEntity
    {
        /// <summary>
        /// 不合格品单号（外键）
        /// </summary>
        [SugarColumn(ColumnName = "nc_no", ColumnDescription = "不合格品单号", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string NcNo { get; set; } = string.Empty;

        /// <summary>
        /// 不良类型
        /// </summary>
        [SugarColumn(ColumnName = "defect_type", ColumnDescription = "不良类型", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DefectType { get; set; }

        /// <summary>
        /// 不良描述
        /// </summary>
        [SugarColumn(ColumnName = "defect_desc", ColumnDescription = "不良描述", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DefectDesc { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [SugarColumn(ColumnName = "quantity", ColumnDescription = "数量", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? Quantity { get; set; }

        /// <summary>
        /// 处置方式
        /// </summary>
        [SugarColumn(ColumnName = "disposition", ColumnDescription = "处置方式", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Disposition { get; set; }

    }
}