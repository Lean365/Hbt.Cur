#nullable enable

using SqlSugar;

namespace Hbt.Domain.Entities.Logistics.Quality.Improvement
{
    /// <summary>
    /// 批次追溯明细表
    /// </summary>
    [SugarTable("hbt_logistics_quality_batch_traceability_detail", "批次追溯明细表")]
    public class HbtBatchTraceabilityDetail : HbtBaseEntity
    {
        /// <summary>
        /// 追溯单号（外键）
        /// </summary>
        [SugarColumn(ColumnName = "trace_no", ColumnDescription = "追溯单号", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string TraceNo { get; set; } = string.Empty;

        /// <summary>
        /// 追溯环节/工序
        /// </summary>
        [SugarColumn(ColumnName = "step", ColumnDescription = "追溯环节", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Step { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        [SugarColumn(ColumnName = "operator", ColumnDescription = "操作人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Operator { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [SugarColumn(ColumnName = "time", ColumnDescription = "时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? Time { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        [SugarColumn(ColumnName = "result", ColumnDescription = "结果", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Result { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "remark", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Remark { get; set; }
    }
} 