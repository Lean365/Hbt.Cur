using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Domain.Entities.Logistics.Production
{
    /// <summary>
    /// 生产工单
    /// </summary>
    [SugarTable("hbt_logistics_prod_order", "生产工单表")]
    [SugarIndex("ix_prod_order_code", nameof(Aufnr), OrderByType.Asc, true)]
    public class HbtProdOrder : HbtBaseEntity
    {
        /// <summary>集团</summary>
        [SugarColumn(ColumnName = "mandt", ColumnDescription = "集团", Length = 6, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Mandt { get; set; }
        /// <summary>生产订单号</summary>
        [SugarColumn(ColumnName = "aufnr", ColumnDescription = "生产订单号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Aufnr { get; set; }
        /// <summary>生产订单类型</summary>
        [SugarColumn(ColumnName = "auart", ColumnDescription = "生产订单类型", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Auart { get; set; }
        /// <summary>物料</summary>
        [SugarColumn(ColumnName = "matnr", ColumnDescription = "物料", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Matnr { get; set; }
        /// <summary>工厂</summary>
        [SugarColumn(ColumnName = "werks", ColumnDescription = "工厂", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Werks { get; set; }
        /// <summary>计划工厂</summary>
        [SugarColumn(ColumnName = "plnbez", ColumnDescription = "计划工厂", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Plnbez { get; set; }
        /// <summary>生产订单数量</summary>
        [SugarColumn(ColumnName = "gamng", ColumnDescription = "生产订单数量", ColumnDataType = "decimal", IsNullable = true)]
        public decimal? Gamng { get; set; }
        /// <summary>单位</summary>
        [SugarColumn(ColumnName = "gmein", ColumnDescription = "单位", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Gmein { get; set; }
        /// <summary>计划开始日期</summary>
        [SugarColumn(ColumnName = "gstrp", ColumnDescription = "计划开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Gstrp { get; set; }
        /// <summary>计划完成日期</summary>
        [SugarColumn(ColumnName = "gltrp", ColumnDescription = "计划完成日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Gltrp { get; set; }
        /// <summary>实际开始日期</summary>
        [SugarColumn(ColumnName = "gstrs", ColumnDescription = "实际开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Gstrs { get; set; }
        /// <summary>实际完成日期</summary>
        [SugarColumn(ColumnName = "gltrs", ColumnDescription = "实际完成日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Gltrs { get; set; }
        /// <summary>生产订单状态</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "生产订单状态", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Status { get; set; }
        /// <summary>生产订单优先级</summary>
        [SugarColumn(ColumnName = "priok", ColumnDescription = "生产订单优先级", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Priok { get; set; }
        /// <summary>生产订单创建日期</summary>
        [SugarColumn(ColumnName = "erdat", ColumnDescription = "生产订单创建日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Erdat { get; set; }
        /// <summary>生产订单创建人</summary>
        [SugarColumn(ColumnName = "ernam", ColumnDescription = "生产订单创建人", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ernam { get; set; }
        /// <summary>生产订单修改日期</summary>
        [SugarColumn(ColumnName = "aedat", ColumnDescription = "生产订单修改日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Aedat { get; set; }
        /// <summary>生产订单修改人</summary>
        [SugarColumn(ColumnName = "aenam", ColumnDescription = "生产订单修改人", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Aenam { get; set; }
        /// <summary>生产订单删除标志</summary>
        [SugarColumn(ColumnName = "loekz", ColumnDescription = "生产订单删除标志", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Loekz { get; set; }
        /// <summary>生产订单删除日期</summary>
        [SugarColumn(ColumnName = "lodat", ColumnDescription = "生产订单删除日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Lodat { get; set; }
        /// <summary>生产订单删除人</summary>
        [SugarColumn(ColumnName = "lousr", ColumnDescription = "生产订单删除人", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Lousr { get; set; }
    }
} 