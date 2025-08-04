#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtAssyDefectDailyDetail.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 组立不良明细实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Hbt.Cur.Domain.Entities.Logistics.Production
{
    /// <summary>
    /// 组立不良明细实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 参考: 组立不良明细管理
    /// </remarks>
    [SugarTable("hbt_logistics_production_assy_defect_detail", "组立不良明细表")]
    [SugarIndex("ix_assy_defect_id", nameof(AssyDefectId), OrderByType.Asc, false)]
    [SugarIndex("ix_hour_no", nameof(HourNo), OrderByType.Asc, false)]
    public class HbtAssyDefectDetail : HbtBaseEntity
    {
        /// <summary>
        /// 组立不良ID
        /// </summary>
        [SugarColumn(ColumnName = "assy_defect_id", ColumnDescription = "组立不良ID", ColumnDataType = "bigint", IsNullable = false)]
        public long AssyDefectId { get; set; }

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
        /// 不良数量
        /// </summary>
        [SugarColumn(ColumnName = "defect_quantity", ColumnDescription = "不良数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal DefectQuantity { get; set; } = 0;

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
        /// 不良等级(1=轻微 2=一般 3=严重 4=致命)
        /// </summary>
        [SugarColumn(ColumnName = "defect_level", ColumnDescription = "不良等级", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int DefectLevel { get; set; } = 1;

        /// <summary>
        /// 处理方式(1=返工 2=报废 3=让步接收 4=退货)
        /// </summary>
        [SugarColumn(ColumnName = "process_method", ColumnDescription = "处理方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ProcessMethod { get; set; } = 1;

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
        /// 组立不良日报
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(AssyDefectId))]
        public HbtAssyDefect? AssyDefect { get; set; }
    }
} 