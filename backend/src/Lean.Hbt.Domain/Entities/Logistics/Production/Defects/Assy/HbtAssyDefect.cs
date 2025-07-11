#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtAssyDefectDaily.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述    : 组立不良日报实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Logistics.Production
{
    /// <summary>
    /// 组立不良日报实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 参考: 组立不良日报管理
    /// </remarks>
    [SugarTable("hbt_logistics_production_assy_defect", "组立不良")]
    [SugarIndex("ix_plant_code", nameof(PlantCode), OrderByType.Asc, false)]
    [SugarIndex("ix_report_date", nameof(ReportDate), OrderByType.Desc, false)]
    [SugarIndex("ix_production_line", nameof(ProductionLine), OrderByType.Asc, false)]
    public class HbtAssyDefect : HbtBaseEntity
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
        /// 质量主管
        /// </summary>
        [SugarColumn(ColumnName = "quality_supervisor", ColumnDescription = "质量主管", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? QualitySupervisor { get; set; }

        /// <summary>
        /// 检验员
        /// </summary>
        [SugarColumn(ColumnName = "inspector", ColumnDescription = "检验员", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Inspector { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; } = 0;

        /// <summary>
        /// 组立不良明细列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtAssyDefectDetail.AssyDefectId))]
        public List<HbtAssyDefectDetail>? AssyDefectDetails { get; set; }
    }
} 