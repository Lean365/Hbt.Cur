using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Domain.Entities.Logistics.Quality
{
    /// <summary>
    /// 供应商评估表
    /// </summary>
    [SugarTable("hbt_logistics_quality_supplier_evaluation", "供应商评估表")]
    [SugarIndex("ix_supplier_evaluation_code", nameof(Qalsnr), OrderByType.Asc, true)]
    public class HbtSupplierEvaluation : HbtBaseEntity
    {
        /// <summary>集团</summary>
        [SugarColumn(ColumnName = "mandt", ColumnDescription = "集团", Length = 6, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Mandt { get; set; }
        /// <summary>供应商编号</summary>
        [SugarColumn(ColumnName = "lifnr", ColumnDescription = "供应商编号", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Lifnr { get; set; }
        /// <summary>供应商名称</summary>
        [SugarColumn(ColumnName = "name1", ColumnDescription = "供应商名称", Length = 35, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Name1 { get; set; }
        /// <summary>国家</summary>
        [SugarColumn(ColumnName = "land1", ColumnDescription = "国家", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Land1 { get; set; }
        /// <summary>城市</summary>
        [SugarColumn(ColumnName = "ort01", ColumnDescription = "城市", Length = 35, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ort01 { get; set; }
        /// <summary>质量体系</summary>
        [SugarColumn(ColumnName = "qssys", ColumnDescription = "质量体系", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Qssys { get; set; }
        /// <summary>质量标识</summary>
        [SugarColumn(ColumnName = "qsskz", ColumnDescription = "质量标识", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Qsskz { get; set; }
        /// <summary>检验批号</summary>
        [SugarColumn(ColumnName = "qalsnr", ColumnDescription = "检验批号", Length = 12, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Qalsnr { get; set; }
        /// <summary>检验批</summary>
        [SugarColumn(ColumnName = "prueflos", ColumnDescription = "检验批", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Prueflos { get; set; }
        /// <summary>评估ID</summary>
        [SugarColumn(ColumnName = "qaveid", ColumnDescription = "评估ID", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Qaveid { get; set; }
        /// <summary>评估日期</summary>
        [SugarColumn(ColumnName = "qavedat", ColumnDescription = "评估日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Qavedat { get; set; }
        /// <summary>评估结果</summary>
        [SugarColumn(ColumnName = "bewert", ColumnDescription = "评估结果", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Bewert { get; set; }
        /// <summary>评估人</summary>
        [SugarColumn(ColumnName = "qaveusr", ColumnDescription = "评估人", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Qaveusr { get; set; }
        /// <summary>评估说明</summary>
        [SugarColumn(ColumnName = "qavetxt", ColumnDescription = "评估说明", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Qavetxt { get; set; }
        /// <summary>创建日期</summary>
        [SugarColumn(ColumnName = "erdat", ColumnDescription = "创建日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Erdat { get; set; }
        /// <summary>创建人</summary>
        [SugarColumn(ColumnName = "ernam", ColumnDescription = "创建人", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ernam { get; set; }
        /// <summary>修改日期</summary>
        [SugarColumn(ColumnName = "aedat", ColumnDescription = "修改日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Aedat { get; set; }
        /// <summary>修改人</summary>
        [SugarColumn(ColumnName = "aenam", ColumnDescription = "修改人", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Aenam { get; set; }
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