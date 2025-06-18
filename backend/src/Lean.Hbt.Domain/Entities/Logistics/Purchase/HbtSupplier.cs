using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Domain.Entities.Logistics.Purchase
{
    /// <summary>
    /// 供应商主数据
    /// </summary>
    [SugarTable("hbt_logistics_purchase_supplier", "供应商主数据")]
    [SugarIndex("ix_supplier_code", nameof(Lifnr), OrderByType.Asc, true)]
    public class HbtSupplier : HbtBaseEntity
    {
        /// <summary>集团</summary>
        [SugarColumn(ColumnName = "mandt", ColumnDescription = "集团", Length = 6, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Mandt { get; set; }
        /// <summary>供应商编号</summary>
        [SugarColumn(ColumnName = "lifnr", ColumnDescription = "供应商编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Lifnr { get; set; }
        /// <summary>名称1</summary>
        [SugarColumn(ColumnName = "name1", ColumnDescription = "名称1", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Name1 { get; set; }
        /// <summary>名称2</summary>
        [SugarColumn(ColumnName = "name2", ColumnDescription = "名称2", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Name2 { get; set; }
        /// <summary>搜索项</summary>
        [SugarColumn(ColumnName = "sortl", ColumnDescription = "搜索项", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Sortl { get; set; }
        /// <summary>国家</summary>
        [SugarColumn(ColumnName = "land1", ColumnDescription = "国家", Length = 6, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Land1 { get; set; }
        /// <summary>城市</summary>
        [SugarColumn(ColumnName = "ort01", ColumnDescription = "城市", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ort01 { get; set; }
        /// <summary>邮政编码</summary>
        [SugarColumn(ColumnName = "pstlz", ColumnDescription = "邮政编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Pstlz { get; set; }
        /// <summary>街道</summary>
        [SugarColumn(ColumnName = "stras", ColumnDescription = "街道", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Stras { get; set; }
        /// <summary>电话</summary>
        [SugarColumn(ColumnName = "telf1", ColumnDescription = "电话", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Telf1 { get; set; }
        /// <summary>传真</summary>
        [SugarColumn(ColumnName = "telfx", ColumnDescription = "传真", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Telfx { get; set; }
        /// <summary>银行国家</summary>
        [SugarColumn(ColumnName = "banks", ColumnDescription = "银行国家", Length = 6, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Banks { get; set; }
        /// <summary>银行帐号</summary>
        [SugarColumn(ColumnName = "bankl", ColumnDescription = "银行帐号", Length = 15, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Bankl { get; set; }
        /// <summary>银行账户</summary>
        [SugarColumn(ColumnName = "bankn", ColumnDescription = "银行账户", Length = 18, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Bankn { get; set; }
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "bukrs", ColumnDescription = "公司代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Bukrs { get; set; }
        /// <summary>采购组织</summary>
        [SugarColumn(ColumnName = "ekorg", ColumnDescription = "采购组织", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ekorg { get; set; }
        /// <summary>删除标志</summary>
        [SugarColumn(ColumnName = "loevm", ColumnDescription = "删除标志", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Loevm { get; set; }
        /// <summary>创建日期</summary>
        [SugarColumn(ColumnName = "erdat", ColumnDescription = "创建日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Erdat { get; set; }
        /// <summary>创建人</summary>
        [SugarColumn(ColumnName = "ernam", ColumnDescription = "创建人", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ernam { get; set; }
        /// <summary>供应商分组</summary>
        [SugarColumn(ColumnName = "ktokk", ColumnDescription = "供应商分组", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ktokk { get; set; }
        /// <summary>采购组织中的删除标志</summary>
        [SugarColumn(ColumnName = "loevm_ekorg", ColumnDescription = "采购组织中的删除标志", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LoevmEkorg { get; set; }
        /// <summary>工厂</summary>
        [SugarColumn(ColumnName = "werks", ColumnDescription = "工厂", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Werks { get; set; }
        /// <summary>采购组织中的工厂删除标志</summary>
        [SugarColumn(ColumnName = "loevm_lfm1", ColumnDescription = "采购组织中的工厂删除标志", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LoevmLfm1 { get; set; }
    }
} 