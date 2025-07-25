#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPcbaInspectionDaily.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : PCBA检查日报实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Logistics.Production
{
    /// <summary>
    /// PCBA检查日报实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 参考: PCBA检查日报管理
    /// </remarks>
    [SugarTable("hbt_logistics_production_pcba_inspection", "PCBA检查")]
    [SugarIndex("ix_plant_code", nameof(PlantCode), OrderByType.Asc, false)]
    [SugarIndex("ix_report_date", nameof(ReportDate), OrderByType.Desc, false)]
    [SugarIndex("ix_production_line", nameof(ProductionLine), OrderByType.Asc, false)]
    public class HbtPcbaInspection : HbtBaseEntity
    {
        /// <summary>
        /// 工厂代码
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产日期
        /// </summary>
        [SugarColumn(ColumnName = "report_date", ColumnDescription = "生产日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime ReportDate { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        [SugarColumn(ColumnName = "production_line", ColumnDescription = "生产线", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProductionLine { get; set; } = string.Empty;

        /// <summary>
        /// 班次(1=早班 2=中班 3=晚班)
        /// </summary>
        [SugarColumn(ColumnName = "shift_no", ColumnDescription = "班次", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ShiftNo { get; set; } = 1;

        /// <summary>
        /// 生产工单号
        /// </summary>
        [SugarColumn(ColumnName = "prod_order_code", ColumnDescription = "生产工单号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProdOrderCode { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        [SugarColumn(ColumnName = "model_code", ColumnDescription = "机种", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ModelCode { get; set; } = string.Empty;

        /// <summary>
        /// 批次
        /// </summary>
        [SugarColumn(ColumnName = "batch_no", ColumnDescription = "批次", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BatchNo { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 检查数量
        /// </summary>
        [SugarColumn(ColumnName = "inspection_quantity", ColumnDescription = "检查数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal InspectionQuantity { get; set; } = 0;

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
        /// 标准工时(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "standard_minutes", ColumnDescription = "标准工时(分钟)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal StandardMinutes { get; set; } = 0;

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
        /// 检查主管
        /// </summary>
        [SugarColumn(ColumnName = "inspection_supervisor", ColumnDescription = "检查主管", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InspectionSupervisor { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; } = 0;

        /// <summary>
        /// PCBA检查明细列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtPcbaInspectionDetail.PcbaInspectionId))]
        public List<HbtPcbaInspectionDetail>? PcbaInspectionDetails { get; set; }
    }
} 