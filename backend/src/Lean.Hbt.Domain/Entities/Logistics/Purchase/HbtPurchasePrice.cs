using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Domain.Entities.Logistics.Purchase
{
    /// <summary>
    /// 采购信息记录
    /// </summary>
    [SugarTable("hbt_logistics_purchase_price", "采购价格表")]
    [SugarIndex("ix_purchase_price_customer_material", nameof(Lifnr), OrderByType.Asc, nameof(Matnr), OrderByType.Asc, true)]
    public class HbtPurchasePrice : HbtBaseEntity
    {
        /// <summary>集团</summary>
        [SugarColumn(ColumnName = "mandt", ColumnDescription = "集团", Length = 6, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Mandt { get; set; }
        /// <summary>信息记录号</summary>
        [SugarColumn(ColumnName = "infnr", ColumnDescription = "信息记录号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Infnr { get; set; }
        /// <summary>供应商</summary>
        [SugarColumn(ColumnName = "lifnr", ColumnDescription = "供应商", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Lifnr { get; set; }
        /// <summary>物料</summary>
        [SugarColumn(ColumnName = "matnr", ColumnDescription = "物料", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Matnr { get; set; }
        /// <summary>工厂</summary>
        [SugarColumn(ColumnName = "werks", ColumnDescription = "工厂", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Werks { get; set; }
        /// <summary>采购组织</summary>
        [SugarColumn(ColumnName = "ekorg", ColumnDescription = "采购组织", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ekorg { get; set; }
        /// <summary>有效从</summary>
        [SugarColumn(ColumnName = "datab", ColumnDescription = "有效从", ColumnDataType = "date", IsNullable = false)]
        public DateTime? Datab { get; set; }
        /// <summary>有效至</summary>
        [SugarColumn(ColumnName = "datbi", ColumnDescription = "有效至", ColumnDataType = "date", IsNullable = false)]
        public DateTime? Datbi { get; set; }
        /// <summary>价格单位</summary>
        [SugarColumn(ColumnName = "peinh", ColumnDescription = "价格单位", ColumnDataType = "decimal", IsNullable = true)]
        public decimal? Peinh { get; set; }
        /// <summary>净价</summary>
        [SugarColumn(ColumnName = "netpr", ColumnDescription = "净价", ColumnDataType = "decimal", IsNullable = true)]
        public decimal? Netpr { get; set; }
        /// <summary>货币</summary>
        [SugarColumn(ColumnName = "waers", ColumnDescription = "货币", Length = 6, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Waers { get; set; }
        /// <summary>采购单位</summary>
        [SugarColumn(ColumnName = "meins", ColumnDescription = "采购单位", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Meins { get; set; }
        /// <summary>交货时间（天）</summary>
        [SugarColumn(ColumnName = "delif", ColumnDescription = "交货时间（天）", ColumnDataType = "decimal", IsNullable = true)]
        public decimal? Delif { get; set; }
        /// <summary>最小订单数量</summary>
        [SugarColumn(ColumnName = "minbm", ColumnDescription = "最小订单数量", ColumnDataType = "decimal", IsNullable = true)]
        public decimal? Minbm { get; set; }
        /// <summary>最小交货数量</summary>
        [SugarColumn(ColumnName = "minlf", ColumnDescription = "最小交货数量", ColumnDataType = "decimal", IsNullable = true)]
        public decimal? Minlf { get; set; }
        /// <summary>最大交货数量</summary>
        [SugarColumn(ColumnName = "maxlf", ColumnDescription = "最大交货数量", ColumnDataType = "decimal", IsNullable = true)]
        public decimal? Maxlf { get; set; }
        /// <summary>标准交货时间（天）</summary>
        [SugarColumn(ColumnName = "stdlf", ColumnDescription = "标准交货时间（天）", ColumnDataType = "decimal", IsNullable = true)]
        public decimal? Stdlf { get; set; }
        /// <summary>供应商物料编号</summary>
        [SugarColumn(ColumnName = "idnlf", ColumnDescription = "供应商物料编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Idnlf { get; set; }
        /// <summary>供应商物料描述</summary>
        [SugarColumn(ColumnName = "matkl", ColumnDescription = "供应商物料描述", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Matkl { get; set; }
        /// <summary>供应商物料类型</summary>
        [SugarColumn(ColumnName = "mtart", ColumnDescription = "供应商物料类型", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Mtart { get; set; }
        /// <summary>供应商物料品牌</summary>
        [SugarColumn(ColumnName = "brand", ColumnDescription = "供应商物料品牌", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Brand { get; set; }
        /// <summary>供应商物料型号</summary>
        [SugarColumn(ColumnName = "model", ColumnDescription = "供应商物料型号", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Model { get; set; }
        /// <summary>供应商物料规格</summary>
        [SugarColumn(ColumnName = "spec", ColumnDescription = "供应商物料规格", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Spec { get; set; }

    }
} 