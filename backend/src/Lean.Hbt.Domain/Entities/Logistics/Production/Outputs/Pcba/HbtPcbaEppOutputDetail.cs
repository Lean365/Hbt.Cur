#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPcbaEppDailyDetail.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述    : PCBA EPP明细实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Logistics.Production.Outputs.Pcba
{
    /// <summary>
    /// PCBA EPP明细实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 参考: PCBA EPP明细管理
    /// </remarks>
    [SugarTable("hbt_logistics_production_pcba_epp_output_detail", "PCBA EPP明细表")]
    [SugarIndex("ix_pcba_epp_output_id", nameof(PcbaEppOutputId), OrderByType.Asc, false)]
    [SugarIndex("ix_hour_no", nameof(HourNo), OrderByType.Asc, false)]
    public class HbtPcbaEppOutputDetail : HbtBaseEntity
    {
        /// <summary>
        /// PCBA EPP日报ID
        /// </summary>
        [SugarColumn(ColumnName = "pcba_epp_output_id", ColumnDescription = "PCBA EPP日报ID", ColumnDataType = "bigint", IsNullable = false)]
        public long PcbaEppOutputId { get; set; }

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
        /// 计划EPP数量
        /// </summary>
        [SugarColumn(ColumnName = "plan_epp_quantity", ColumnDescription = "计划EPP数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal PlanEppQuantity { get; set; } = 0;

        /// <summary>
        /// 实际EPP数量
        /// </summary>
        [SugarColumn(ColumnName = "actual_epp_quantity", ColumnDescription = "实际EPP数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ActualEppQuantity { get; set; } = 0;

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
        /// EPP效率(%)
        /// </summary>
        [SugarColumn(ColumnName = "efficiency_rate", ColumnDescription = "EPP效率", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
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
        /// PCBA EPP日报
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(PcbaEppOutputId))]
        public HbtPcbaEppOutput? PcbaEppOutput { get; set; }
    }
} 