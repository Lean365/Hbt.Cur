#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPcbaSmtOutput.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : PCBA SMT输出实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Hbt.Domain.Entities.Logistics.Production.Outputs.Pcba
{
    /// <summary>
    /// PCBA SMT输出实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 参考: PCBA SMT输出管理
    /// </remarks>
    [SugarTable("hbt_logistics_production_pcba_mp_smt_output", "PCBA SMT产出")]
    [SugarIndex("ix_plant_code", nameof(PlantCode), OrderByType.Asc, false)]
    [SugarIndex("ix_prod_date", nameof(ProdDate), OrderByType.Desc, false)]
    [SugarIndex("ix_prod_line", nameof(ProdLine), OrderByType.Asc, false)]
    public class HbtPcbaMpSmtOutput : HbtBaseEntity
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
        /// SMT数量
        /// </summary>
        [SugarColumn(ColumnName = "smt_qty", ColumnDescription = "SMT数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 1, IsNullable = false, DefaultValue = "0")]
        public decimal SmtQty { get; set; } = 0;

        /// <summary>
        /// 标准点数
        /// </summary>
        [SugarColumn(ColumnName = "std_shorts", ColumnDescription = "标准点数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int StdShorts { get; set; } = 0;

        /// <summary>
        /// 实际点数
        /// </summary>
        [SugarColumn(ColumnName = "actual_shorts", ColumnDescription = "实际点数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ActualShorts { get; set; } = 0;

        /// <summary>
        /// PCBA SMT输出明细列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtPcbaMpSmtOutputDetail.PcbaMpSmtOutputId))]
        public List<HbtPcbaMpSmtOutputDetail>? PcbaMpSmtOutputDetails { get; set; }
    }
} 