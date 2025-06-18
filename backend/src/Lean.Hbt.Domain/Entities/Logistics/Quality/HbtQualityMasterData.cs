using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Domain.Entities.Logistics.Quality
{
    /// <summary>
    /// 质量主数据表
    /// </summary>
    [SugarTable("hbt_logistics_quality_master_data", "质量主数据表")]
    [SugarIndex("ix_quality_master_code", nameof(Prueflos), OrderByType.Asc, true)]
    public class HbtQualityMasterData : HbtBaseEntity
    {
        /// <summary>集团</summary>
        [SugarColumn(ColumnName = "mandt", ColumnDescription = "集团", Length = 6, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Mandt { get; set; }
        /// <summary>检验批号</summary>
        [SugarColumn(ColumnName = "prueflos", ColumnDescription = "检验批号", Length = 12, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Prueflos { get; set; }
        /// <summary>物料编号</summary>
        [SugarColumn(ColumnName = "matnr", ColumnDescription = "物料编号", Length = 18, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Matnr { get; set; }
        /// <summary>工厂</summary>
        [SugarColumn(ColumnName = "werks", ColumnDescription = "工厂", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Werks { get; set; }
        /// <summary>BOM编号</summary>
        [SugarColumn(ColumnName = "stlnr", ColumnDescription = "BOM编号", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Stlnr { get; set; }
        /// <summary>BOM版本</summary>
        [SugarColumn(ColumnName = "stlal", ColumnDescription = "BOM版本", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Stlal { get; set; }
        /// <summary>检验类型</summary>
        [SugarColumn(ColumnName = "art", ColumnDescription = "检验类型", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Art { get; set; }
        /// <summary>检验批状态</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "检验批状态", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Status { get; set; }
        /// <summary>检验批数量</summary>
        [SugarColumn(ColumnName = "menge", ColumnDescription = "检验批数量", ColumnDataType = "decimal", IsNullable = true)]
        public decimal? Menge { get; set; }
        /// <summary>单位</summary>
        [SugarColumn(ColumnName = "meins", ColumnDescription = "单位", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Meins { get; set; }
        /// <summary>检验批创建日期</summary>
        [SugarColumn(ColumnName = "erdat", ColumnDescription = "检验批创建日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Erdat { get; set; }
        /// <summary>检验批创建人</summary>
        [SugarColumn(ColumnName = "ernam", ColumnDescription = "检验批创建人", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ernam { get; set; }
        /// <summary>检验批修改日期</summary>
        [SugarColumn(ColumnName = "aedat", ColumnDescription = "检验批修改日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Aedat { get; set; }
        /// <summary>检验批修改人</summary>
        [SugarColumn(ColumnName = "aenam", ColumnDescription = "检验批修改人", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Aenam { get; set; }
        /// <summary>检验批特性编号</summary>
        [SugarColumn(ColumnName = "merkmalnr", ColumnDescription = "检验批特性编号", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Merkmalnr { get; set; }
        /// <summary>特性结果</summary>
        [SugarColumn(ColumnName = "merkmalwert", ColumnDescription = "特性结果", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Merkmalwert { get; set; }
        /// <summary>特性评估</summary>
        [SugarColumn(ColumnName = "bewertung", ColumnDescription = "特性评估", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Bewertung { get; set; }
        /// <summary>检验批结果记录编号</summary>
        [SugarColumn(ColumnName = "amrnr", ColumnDescription = "检验批结果记录编号", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Amrnr { get; set; }
        /// <summary>结果记录</summary>
        [SugarColumn(ColumnName = "amrwert", ColumnDescription = "结果记录", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Amrwert { get; set; }
        /// <summary>结果评估</summary>
        [SugarColumn(ColumnName = "amrbewertung", ColumnDescription = "结果评估", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Amrbewertung { get; set; }
        /// <summary>删除标志</summary>
        [SugarColumn(ColumnName = "loekz", ColumnDescription = "删除标志", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Loekz { get; set; }
        /// <summary>删除日期</summary>
        [SugarColumn(ColumnName = "lodat", ColumnDescription = "删除日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Lodat { get; set; }
        /// <summary>删除人</summary>
        [SugarColumn(ColumnName = "lousr", ColumnDescription = "删除人", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Lousr { get; set; }
    }
} 