#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDefectsPcbaOrder.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : PCBA工单组立不良统计实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Hbt.Cur.Domain.Entities.Logistics.Production
{
    /// <summary>
    /// PCBA工单组立不良统计实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 参考: PCBA工单组立不良统计管理
    /// </remarks>
    [SugarTable("hbt_logistics_pcba_defects_order", "PCBA工单组立不良统计表")]
    [SugarIndex("ix_plant_code", nameof(PlantCode), OrderByType.Asc, false)]
    [SugarIndex("ix_prod_order_code", nameof(ProdOrderCode), OrderByType.Asc, false)]
    [SugarIndex("ix_production_date", nameof(ProductionDate), OrderByType.Desc, false)]
    public class HbtDefectsPcbaOrder : HbtBaseEntity
    {
        /// <summary>
        /// 工厂代码
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产工单号
        /// </summary>
        [SugarColumn(ColumnName = "prod_order_code", ColumnDescription = "生产工单号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProdOrderCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产日期
        /// </summary>
        [SugarColumn(ColumnName = "production_date", ColumnDescription = "生产日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime ProductionDate { get; set; }

        /// <summary>
        /// 累计订单数量
        /// </summary>
        [SugarColumn(ColumnName = "total_order_quantity", ColumnDescription = "累计订单数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal TotalOrderQuantity { get; set; } = 0;

        /// <summary>
        /// 累计生产数量
        /// </summary>
        [SugarColumn(ColumnName = "total_production_quantity", ColumnDescription = "累计生产数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal TotalProductionQuantity { get; set; } = 0;

        /// <summary>
        /// 累计无不良数量
        /// </summary>
        [SugarColumn(ColumnName = "total_good_quantity", ColumnDescription = "累计无不良数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal TotalGoodQuantity { get; set; } = 0;

        /// <summary>
        /// 累计不良数量
        /// </summary>
        [SugarColumn(ColumnName = "total_defect_quantity", ColumnDescription = "累计不良数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal TotalDefectQuantity { get; set; } = 0;

        /// <summary>
        /// 执行率(%)
        /// </summary>
        [SugarColumn(ColumnName = "execution_rate", ColumnDescription = "执行率", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ExecutionRate { get; set; } = 0;

        /// <summary>
        /// 不良率(%)
        /// </summary>
        [SugarColumn(ColumnName = "defect_rate", ColumnDescription = "不良率", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal DefectRate { get; set; } = 0;

        /// <summary>
        /// 生产线
        /// </summary>
        [SugarColumn(ColumnName = "production_line", ColumnDescription = "生产线", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductionLine { get; set; }
    }
} 