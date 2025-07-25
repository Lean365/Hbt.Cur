#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPcbaInspectionDailyDetail.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : PCBA检查明细实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Logistics.Production
{
    /// <summary>
    /// PCBA检查明细实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 参考: PCBA检查明细管理
    /// </remarks>
    [SugarTable("hbt_logistics_production_pcba_inspection_detail", "PCBA检查明细表")]
    [SugarIndex("ix_pcba_inspection_id", nameof(PcbaInspectionId), OrderByType.Asc, false)]
    [SugarIndex("ix_hour_no", nameof(HourNo), OrderByType.Asc, false)]
    public class HbtPcbaInspectionDetail : HbtBaseEntity
    {
        /// <summary>
        /// PCBA检查日报ID
        /// </summary>
        [SugarColumn(ColumnName = "pcba_inspection_id", ColumnDescription = "PCBA检查ID", ColumnDataType = "bigint", IsNullable = false)]
        public long PcbaInspectionId { get; set; }

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
        /// 计划检查数量
        /// </summary>
        [SugarColumn(ColumnName = "plan_inspection_quantity", ColumnDescription = "计划检查数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal PlanInspectionQuantity { get; set; } = 0;

        /// <summary>
        /// 实际检查数量
        /// </summary>
        [SugarColumn(ColumnName = "actual_inspection_quantity", ColumnDescription = "实际检查数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ActualInspectionQuantity { get; set; } = 0;

        /// <summary>
        /// 合格数量
        /// </summary>
        [SugarColumn(ColumnName = "passed_quantity", ColumnDescription = "合格数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal PassedQuantity { get; set; } = 0;

        /// <summary>
        /// 不合格数量
        /// </summary>
        [SugarColumn(ColumnName = "failed_quantity", ColumnDescription = "不合格数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal FailedQuantity { get; set; } = 0;

        /// <summary>
        /// 实际工时(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "actual_minutes", ColumnDescription = "实际工时(分钟)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ActualMinutes { get; set; } = 0;

        /// <summary>
        /// 检查效率(%)
        /// </summary>
        [SugarColumn(ColumnName = "efficiency_rate", ColumnDescription = "检查效率", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal EfficiencyRate { get; set; } = 0;

        /// <summary>
        /// 合格率(%)
        /// </summary>
        [SugarColumn(ColumnName = "pass_rate", ColumnDescription = "合格率", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal PassRate { get; set; } = 0;

        /// <summary>
        /// 检验员
        /// </summary>
        [SugarColumn(ColumnName = "inspector_name", ColumnDescription = "检验员", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InspectorName { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; } = 0;

        /// <summary>
        /// PCBA检查日报
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(PcbaInspectionId))]
        public HbtPcbaInspection? PcbaInspection { get; set; }
    }
} 