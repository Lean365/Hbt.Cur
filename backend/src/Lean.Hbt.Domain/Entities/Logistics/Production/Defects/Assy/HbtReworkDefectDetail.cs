#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtReworkDefectDailyDetail.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述    : 返工不良明细实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Logistics.Production
{
    /// <summary>
    /// 返工不良明细实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 参考: 返工不良明细管理
    /// </remarks>
    [SugarTable("hbt_logistics_production_rework_defect_detail", "返工不良明细表")]
    [SugarIndex("ix_rework_defect_id", nameof(ReworkDefectId), OrderByType.Asc, false)]
    [SugarIndex("ix_hour_no", nameof(HourNo), OrderByType.Asc, false)]
    public class HbtReworkDefectDetail : HbtBaseEntity
    {
        /// <summary>
        /// 返工不良日报ID
        /// </summary>
        [SugarColumn(ColumnName = "rework_defect_id", ColumnDescription = "返工不良ID", ColumnDataType = "bigint", IsNullable = false)]
        public long ReworkDefectId { get; set; }

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
        /// 返工不良数量
        /// </summary>
        [SugarColumn(ColumnName = "rework_defect_quantity", ColumnDescription = "返工不良数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ReworkDefectQuantity { get; set; } = 0;

        /// <summary>
        /// 不良类型
        /// </summary>
        [SugarColumn(ColumnName = "defect_type", ColumnDescription = "不良类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DefectType { get; set; }

        /// <summary>
        /// 不良原因
        /// </summary>
        [SugarColumn(ColumnName = "defect_reason", ColumnDescription = "不良原因", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DefectReason { get; set; }

        /// <summary>
        /// 不良等级(A=严重 B=一般 C=轻微)
        /// </summary>
        [SugarColumn(ColumnName = "defect_level", ColumnDescription = "不良等级", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DefectLevel { get; set; }

        /// <summary>
        /// 处理方式(1=返工 2=报废 3=让步接收)
        /// </summary>
        [SugarColumn(ColumnName = "disposal_method", ColumnDescription = "处理方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int DisposalMethod { get; set; } = 1;

        /// <summary>
        /// 返工不良率(%)
        /// </summary>
        [SugarColumn(ColumnName = "rework_defect_rate", ColumnDescription = "返工不良率", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ReworkDefectRate { get; set; } = 0;

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
        /// 返工不良日报
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ReworkDefectId))]
        public HbtReworkDefect? ReworkDefect { get; set; }
    }
} 