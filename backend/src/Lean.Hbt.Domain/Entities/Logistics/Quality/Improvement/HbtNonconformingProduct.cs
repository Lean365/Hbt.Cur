#nullable enable

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Logistics.Quality.Improvement
{
    /// <summary>
    /// 不合格品主表
    /// </summary>
    [SugarTable("hbt_logistics_quality_nonconforming_product", "不合格品主表")]
    public class HbtNonconformingProduct : HbtBaseEntity
    {
        /// <summary>
        /// 不合格品单号
        /// </summary>
        [SugarColumn(ColumnName = "nc_no", ColumnDescription = "不合格品单号", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string NcNo { get; set; } = string.Empty;

        /// <summary>
        /// 不合格品日期
        /// </summary>
        [SugarColumn(ColumnName = "nc_date", ColumnDescription = "不合格品日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? NcDate { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [SugarColumn(ColumnName = "product_name", ColumnDescription = "产品名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductName { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        [SugarColumn(ColumnName = "lot_no", ColumnDescription = "批次号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LotNo { get; set; }

        /// <summary>
        /// 责任人
        /// </summary>
        [SugarColumn(ColumnName = "responsible", ColumnDescription = "责任人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Responsible { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "memo", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Memo { get; set; }

        /// <summary>
        /// 不合格品明细集合
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtNonconformingProductDetail.NcNo))]
        public List<HbtNonconformingProductDetail>? Details { get; set; }
    }
} 