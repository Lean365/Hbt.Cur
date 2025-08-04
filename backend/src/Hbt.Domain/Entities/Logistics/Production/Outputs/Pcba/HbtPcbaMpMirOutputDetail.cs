#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPcbaMirOutputDetail.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : PCBA手插输出明细实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Hbt.Cur.Domain.Entities.Logistics.Production.Outputs.Pcba
{
    /// <summary>
    /// PCBA手插输出明细实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 参考: PCBA手插输出明细管理
    /// </remarks>
    [SugarTable("hbt_logistics_production_pcba_mp_mir_output_detail", "PCBA 本产手插产出明细表")]
    [SugarIndex("ix_pcba_mp_mir_output_id", nameof(PcbaMpMirOutputId), OrderByType.Asc, false)]
    public class HbtPcbaMpMirOutputDetail : HbtBaseEntity
    {
        /// <summary>
        /// PCBA手插输出ID
        /// </summary>
        [SugarColumn(ColumnName = "pcba_mir_output_id", ColumnDescription = "PCBA手插输出ID", ColumnDataType = "bigint", IsNullable = false)]
        public long PcbaMpMirOutputId { get; set; }

        /// <summary>
        /// 班次(1=早班 2=中班 3=晚班)
        /// </summary>
        [SugarColumn(ColumnName = "shift_no", ColumnDescription = "班次", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ShiftNo { get; set; } = 1;

        /// <summary>
        /// PCB板别
        /// </summary>
        [SugarColumn(ColumnName = "pcb_board_type", ColumnDescription = "PCB板别", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PcbBoardType { get; set; } = string.Empty;

        /// <summary>
        /// 面板别
        /// </summary>
        [SugarColumn(ColumnName = "panel_side", ColumnDescription = "面板别", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PanelSide { get; set; } = string.Empty;

        /// <summary>
        /// 生产时间
        /// </summary>
        [SugarColumn(ColumnName = "prod_time", ColumnDescription = "生产时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime ProdTime { get; set; }

        /// <summary>
        /// 批次数
        /// </summary>
        [SugarColumn(ColumnName = "batch_qty", ColumnDescription = "批次数", ColumnDataType = "decimal", Length = 18, DecimalDigits = 1, IsNullable = false, DefaultValue = "0")]
        public decimal BatchQty { get; set; } = 0;

        /// <summary>
        /// 当日完成数
        /// </summary>
        [SugarColumn(ColumnName = "daily_completed_qty", ColumnDescription = "当日完成数", ColumnDataType = "decimal", Length = 18, DecimalDigits = 1, IsNullable = false, DefaultValue = "0")]
        public decimal DailyCompletedQty { get; set; } = 0;

        /// <summary>
        /// 累计完成数
        /// </summary>
        [SugarColumn(ColumnName = "total_completed_qty", ColumnDescription = "累计完成数", ColumnDataType = "decimal", Length = 18, DecimalDigits = 1, IsNullable = false, DefaultValue = "0")]
        public decimal TotalCompletedQty { get; set; } = 0;

        /// <summary>
        /// 完成状态
        /// </summary>
        [SugarColumn(ColumnName = "completion_status", ColumnDescription = "完成状态", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompletionStatus { get; set; } = string.Empty;

        /// <summary>
        /// 停线时间
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
        /// 序列号
        /// </summary>
        [SugarColumn(ColumnName = "serial_no", ColumnDescription = "序列号", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string SerialNo { get; set; } = string.Empty;

        /// <summary>
        /// 投入工时
        /// </summary>
        [SugarColumn(ColumnName = "input_minutes", ColumnDescription = "投入工时", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal InputMinutes { get; set; } = 0;

        /// <summary>
        /// 生产工时
        /// </summary>
        [SugarColumn(ColumnName = "prod_minutes", ColumnDescription = "生产工时", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal ProdMinutes { get; set; } = 0;

        /// <summary>
        /// 实际工时
        /// </summary>
        [SugarColumn(ColumnName = "actual_minutes", ColumnDescription = "实际工时", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal ActualMinutes { get; set; } = 0;

        /// <summary>
        /// 达成率
        /// </summary>
        [SugarColumn(ColumnName = "efficiency_rate", ColumnDescription = "达成率", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal EfficiencyRate { get; set; } = 0;

        /// <summary>
        /// PCBA手插输出
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(PcbaMpMirOutputId))]
        public HbtPcbaMpMirOutput? PcbaMpMirOutput { get; set; }
    }
} 