#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPcbaEppMirDailyDetail.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : PCBA手插EPP明细实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Hbt.Cur.Domain.Entities.Logistics.Production.Outputs.Pcba
{
    /// <summary>
    /// PCBA手插EPP明细实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 参考: PCBA手插EPP明细管理
    /// </remarks>
    [SugarTable("hbt_logistics_production_pcba_epp_mir_output_detail", "PCBA手插EPP产出明细表")]
    [SugarIndex("ix_pcba_epp_mir_output_id", nameof(PcbaEppMirOutputId), OrderByType.Asc, false)]
    public class HbtPcbaEppMirOutputDetail : HbtBaseEntity
    {
        /// <summary>
        /// PCBA手插EPP日报ID
        /// </summary>
        [SugarColumn(ColumnName = "pcba_epp_mir_output_id", ColumnDescription = "PCBA手插EPP日报ID", ColumnDataType = "bigint", IsNullable = false)]
        public long PcbaEppMirOutputId { get; set; }

        /// <summary>
        /// 实际生产数量
        /// </summary>
        [SugarColumn(ColumnName = "actual_epp_qty", ColumnDescription = "实际数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 1, IsNullable = false, DefaultValue = "0")]
        public decimal ActualEppQty { get; set; } = 0;

        /// <summary>
        /// 停线时间(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "downtime_minutes", ColumnDescription = "停线时间", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DowntimeMinutes { get; set; } = 0;

        /// <summary>
        /// 停线原因
        /// </summary>
        [SugarColumn(ColumnName = "downtime_reason", ColumnDescription = "停线原因", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DowntimeReason { get; set; }

        /// <summary>
        /// 停线说明
        /// </summary>
        [SugarColumn(ColumnName = "downtime_description", ColumnDescription = "停线说明", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DowntimeDescription { get; set; }

        /// <summary>
        /// 未达成原因
        /// </summary>
        [SugarColumn(ColumnName = "failure_reason", ColumnDescription = "未达成原因", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FailureReason { get; set; }

        /// <summary>
        /// 未达成说明
        /// </summary>
        [SugarColumn(ColumnName = "failure_description", ColumnDescription = "未达成说明", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FailureDescription { get; set; }

        /// <summary>
        /// 投入工时(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "input_minutes", ColumnDescription = "投入工时", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal InputMinutes { get; set; } = 0;

        /// <summary>
        /// 生产工时(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "prod_minutes", ColumnDescription = "生产工时", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal ProdMinutes { get; set; } = 0;

        /// <summary>
        /// 实际工时(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "actual_minutes", ColumnDescription = "实际工时", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal ActualMinutes { get; set; } = 0;

        /// <summary>
        /// 达成率(%)
        /// </summary>
        [SugarColumn(ColumnName = "efficiency_rate", ColumnDescription = "达成率", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal EfficiencyRate { get; set; } = 0;

        /// <summary>
        /// PCBA手插EPP日报
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(PcbaEppMirOutputId))]
        public HbtPcbaEppMirOutput? PcbaEppMirOutput { get; set; }
    }
} 