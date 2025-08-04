#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPcbaRepairDailyDetail.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : PCBA改修明细实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Hbt.Domain.Entities.Logistics.Production
{
    /// <summary>
    /// PCBA改修明细实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 参考: PCBA改修明细管理
    /// </remarks>
    [SugarTable("hbt_logistics_production_pcba_repair_detail", "PCBA改修明细表")]
    [SugarIndex("ix_pcba_repair_id", nameof(PcbaRepairId), OrderByType.Asc, false)]
    [SugarIndex("ix_hour_no", nameof(HourNo), OrderByType.Asc, false)]
    public class HbtPcbaRepairDetail : HbtBaseEntity
    {
        /// <summary>
        /// PCBA改修日报ID
        /// </summary>
        [SugarColumn(ColumnName = "pcba_repair_id", ColumnDescription = "PCBA改修ID", ColumnDataType = "bigint", IsNullable = false)]
        public long PcbaRepairId { get; set; }

        /// <summary>
        /// 小时编号(0-23)
        /// </summary>
        [SugarColumn(ColumnName = "hour_no", ColumnDescription = "小时编号", ColumnDataType = "int", IsNullable = false)]
        public int HourNo { get; set; }

        /// <summary>
        /// 小时开始时间
        /// </summary>
        [SugarColumn(ColumnName = "hour_start_time", ColumnDescription = "小时开始时间", ColumnDataType = "time", IsNullable = false)]
        public TimeSpan HourStartTime { get; set; }

        /// <summary>
        /// 小时结束时间
        /// </summary>
        [SugarColumn(ColumnName = "hour_end_time", ColumnDescription = "小时结束时间", ColumnDataType = "time", IsNullable = false)]
        public TimeSpan HourEndTime { get; set; }

        /// <summary>
        /// 计划改修数量
        /// </summary>
        [SugarColumn(ColumnName = "plan_repair_quantity", ColumnDescription = "计划改修数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal PlanRepairQuantity { get; set; } = 0;

        /// <summary>
        /// 实际改修数量
        /// </summary>
        [SugarColumn(ColumnName = "actual_repair_quantity", ColumnDescription = "实际改修数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ActualRepairQuantity { get; set; } = 0;

        /// <summary>
        /// 完成数量
        /// </summary>
        [SugarColumn(ColumnName = "completed_quantity", ColumnDescription = "完成数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal CompletedQuantity { get; set; } = 0;

        /// <summary>
        /// 报废数量
        /// </summary>
        [SugarColumn(ColumnName = "scrap_quantity", ColumnDescription = "报废数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ScrapQuantity { get; set; } = 0;

        /// <summary>
        /// 实际工时(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "actual_minutes", ColumnDescription = "实际工时(分钟)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ActualMinutes { get; set; } = 0;

        /// <summary>
        /// 实际点数
        /// </summary>
        [SugarColumn(ColumnName = "actual_shorts", ColumnDescription = "实际点数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ActualShorts { get; set; } = 0;

        /// <summary>
        /// 点数单位
        /// </summary>
        [SugarColumn(ColumnName = "points_unit", ColumnDescription = "点数单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "SHORT")]
        public string PointsUnit { get; set; } = "SHORT";

        /// <summary>
        /// 改修效率(%)
        /// </summary>
        [SugarColumn(ColumnName = "efficiency_rate", ColumnDescription = "改修效率", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal EfficiencyRate { get; set; } = 0;

        /// <summary>
        /// 完成率(%)
        /// </summary>
        [SugarColumn(ColumnName = "completion_rate", ColumnDescription = "完成率", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal CompletionRate { get; set; } = 0;

        /// <summary>
        /// 操作员
        /// </summary>
        [SugarColumn(ColumnName = "operator_name", ColumnDescription = "操作员", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OperatorName { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; } = 0;

        /// <summary>
        /// PCBA改修日报
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(PcbaRepairId))]
        public HbtPcbaRepair? PcbaRepair { get; set; }
    }
} 