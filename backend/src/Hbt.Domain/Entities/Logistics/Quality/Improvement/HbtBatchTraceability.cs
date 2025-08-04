#nullable enable

using SqlSugar;

namespace Hbt.Cur.Domain.Entities.Logistics.Quality.Improvement
{
    /// <summary>
    /// 批次追溯主表
    /// </summary>
    [SugarTable("hbt_logistics_quality_batch_traceability", "批次追溯主表")]
    public class HbtBatchTraceability : HbtBaseEntity
    {
        /// <summary>
        /// 追溯单号
        /// </summary>
        [SugarColumn(ColumnName = "trace_no", ColumnDescription = "追溯单号", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string TraceNo { get; set; } = string.Empty;

        /// <summary>
        /// 追溯日期
        /// </summary>
        [SugarColumn(ColumnName = "trace_date", ColumnDescription = "追溯日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? TraceDate { get; set; }

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
        /// 追溯明细集合
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtBatchTraceabilityDetail.TraceNo))]
        public List<HbtBatchTraceabilityDetail>? Details { get; set; }
    }
} 