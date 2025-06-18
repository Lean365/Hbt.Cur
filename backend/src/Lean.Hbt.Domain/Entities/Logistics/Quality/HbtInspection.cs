using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Domain.Entities.Logistics.Quality
{
    /// <summary>
    /// 质量检验
    /// </summary>
    [SugarTable("hbt_logistics_quality_inspection", "质量检验表")]
    [SugarIndex("ix_quality_inspection_code", nameof(Qmnum), OrderByType.Asc, true)]
    public class HbtInspection : HbtBaseEntity
    {
        /// <summary>集团</summary>
        [SugarColumn(ColumnName = "mandt", ColumnDescription = "集团", Length = 6, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Mandt { get; set; }
        /// <summary>检验批号</summary>
        [SugarColumn(ColumnName = "qmnum", ColumnDescription = "检验批号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Qmnum { get; set; }
        /// <summary>检验类型</summary>
        [SugarColumn(ColumnName = "art", ColumnDescription = "检验类型", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Art { get; set; }
        /// <summary>物料</summary>
        [SugarColumn(ColumnName = "matnr", ColumnDescription = "物料", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Matnr { get; set; }
        /// <summary>工厂</summary>
        [SugarColumn(ColumnName = "werks", ColumnDescription = "工厂", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Werks { get; set; }
        /// <summary>检验数量</summary>
        [SugarColumn(ColumnName = "menge", ColumnDescription = "检验数量", ColumnDataType = "decimal", IsNullable = true)]
        public decimal? Menge { get; set; }
        /// <summary>单位</summary>
        [SugarColumn(ColumnName = "meins", ColumnDescription = "单位", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Meins { get; set; }
        /// <summary>检验日期</summary>
        [SugarColumn(ColumnName = "pruefdat", ColumnDescription = "检验日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Pruefdat { get; set; }
        /// <summary>检验结果</summary>
        [SugarColumn(ColumnName = "ergebnis", ColumnDescription = "检验结果", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ergebnis { get; set; }
        /// <summary>检验状态</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "检验状态", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Status { get; set; }
        /// <summary>检验人</summary>
        [SugarColumn(ColumnName = "pruefer", ColumnDescription = "检验人", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Pruefer { get; set; }
        /// <summary>检验创建日期</summary>
        [SugarColumn(ColumnName = "erdat", ColumnDescription = "检验创建日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Erdat { get; set; }
        /// <summary>检验创建人</summary>
        [SugarColumn(ColumnName = "ernam", ColumnDescription = "检验创建人", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ernam { get; set; }
        /// <summary>检验修改日期</summary>
        [SugarColumn(ColumnName = "aedat", ColumnDescription = "检验修改日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Aedat { get; set; }
        /// <summary>检验修改人</summary>
        [SugarColumn(ColumnName = "aenam", ColumnDescription = "检验修改人", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Aenam { get; set; }
        /// <summary>检验删除标志</summary>
        [SugarColumn(ColumnName = "loekz", ColumnDescription = "检验删除标志", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Loekz { get; set; }
        /// <summary>检验删除日期</summary>
        [SugarColumn(ColumnName = "lodat", ColumnDescription = "检验删除日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Lodat { get; set; }
        /// <summary>检验删除人</summary>
        [SugarColumn(ColumnName = "lousr", ColumnDescription = "检验删除人", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Lousr { get; set; }
    }
} 