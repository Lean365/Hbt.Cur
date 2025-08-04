#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPcbaEppMirDaily.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : PCBA手插EPP日报实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Hbt.Domain.Entities.Logistics.Production.Outputs.Pcba
{
    /// <summary>
    /// PCBA手插EPP日报实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 参考: PCBA手插EPP日报管理
    /// </remarks>
    [SugarTable("hbt_logistics_production_pcba_epp_mir_output", "PCBA手插EPP产出")]
    [SugarIndex("ix_plant_code", nameof(PlantCode), OrderByType.Asc, false)]
    [SugarIndex("ix_prod_date", nameof(ProdDate), OrderByType.Desc, false)]
    [SugarIndex("ix_prod_line", nameof(ProdLine), OrderByType.Asc, false)]
    public class HbtPcbaEppMirOutput : HbtBaseEntity
    {
        /// <summary>
        /// 工厂代码
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产日期
        /// </summary>
        [SugarColumn(ColumnName = "prod_date", ColumnDescription = "生产日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime ProdDate { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        [SugarColumn(ColumnName = "prod_line", ColumnDescription = "生产线", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProdLine { get; set; } = string.Empty;

        /// <summary>
        /// 直接人员
        /// </summary>
        [SugarColumn(ColumnName = "direct_labor", ColumnDescription = "直接人员", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DirectLabor { get; set; } = 0;

        /// <summary>
        /// 间接人员
        /// </summary>
        [SugarColumn(ColumnName = "indirect_labor", ColumnDescription = "间接人员", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IndirectLabor { get; set; } = 0;

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
        /// 物料编码
        /// </summary>
        [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 批次
        /// </summary>
        [SugarColumn(ColumnName = "batch_no", ColumnDescription = "批次", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BatchNo { get; set; }

        /// <summary>
        /// EPP数量
        /// </summary>
        [SugarColumn(ColumnName = "epp_qty", ColumnDescription = "EPP数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 1, IsNullable = false, DefaultValue = "0")]
        public decimal EppQty { get; set; } = 0;

        /// <summary>
        /// 标准工时(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "std_minutes", ColumnDescription = "标准工时", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal StdMinutes { get; set; } = 0;

        /// <summary>
        /// 标准产能
        /// </summary>
        [SugarColumn(ColumnName = "std_capacity", ColumnDescription = "标准产能", ColumnDataType = "decimal", Length = 18, DecimalDigits = 1, IsNullable = false, DefaultValue = "0")]
        public decimal StdCapacity { get; set; } = 0;

        /// <summary>
        /// PCBA手插EPP明细列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtPcbaEppMirOutputDetail.PcbaEppMirOutputId))]
        public List<HbtPcbaEppMirOutputDetail>? PcbaEppMirOutputDetails { get; set; }
    }
} 