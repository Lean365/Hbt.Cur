using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Domain.Entities.Logistics.Quality
{
    /// <summary>
    /// 检验计划表
    /// </summary>
    [SugarTable("hbt_logistics_quality_inspection_plan", "检验计划表")]
    [SugarIndex("ix_inspection_plan_code", nameof(Plnnr), OrderByType.Asc, true)]
    public class HbtInspectionPlan : HbtBaseEntity
    {
        /// <summary>集团</summary>
        [SugarColumn(ColumnName = "mandt", ColumnDescription = "集团", Length = 6, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Mandt { get; set; }
        /// <summary>检验计划编号</summary>
        [SugarColumn(ColumnName = "plnnr", ColumnDescription = "检验计划编号", Length = 12, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Plnnr { get; set; }
        /// <summary>检验计划组计数器</summary>
        [SugarColumn(ColumnName = "plnal", ColumnDescription = "检验计划组计数器", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Plnal { get; set; }
        /// <summary>物料编号</summary>
        [SugarColumn(ColumnName = "matnr", ColumnDescription = "物料编号", Length = 18, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Matnr { get; set; }
        /// <summary>工厂</summary>
        [SugarColumn(ColumnName = "werks", ColumnDescription = "工厂", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Werks { get; set; }
        /// <summary>检验计划描述</summary>
        [SugarColumn(ColumnName = "ktext", ColumnDescription = "检验计划描述", Length = 40, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ktext { get; set; }
        /// <summary>工序编号</summary>
        [SugarColumn(ColumnName = "vornr", ColumnDescription = "工序编号", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Vornr { get; set; }
        /// <summary>工序描述</summary>
        [SugarColumn(ColumnName = "ltxa1", ColumnDescription = "工序描述", Length = 40, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ltxa1 { get; set; }
        /// <summary>工作中心</summary>
        [SugarColumn(ColumnName = "arbpl", ColumnDescription = "工作中心", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Arbpl { get; set; }
        /// <summary>工作中心描述</summary>
        [SugarColumn(ColumnName = "arbtxt", ColumnDescription = "工作中心描述", Length = 40, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Arbtxt { get; set; }
        /// <summary>检验特性编号</summary>
        [SugarColumn(ColumnName = "merkmalnr", ColumnDescription = "检验特性编号", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Merkmalnr { get; set; }
        /// <summary>检验特性描述</summary>
        [SugarColumn(ColumnName = "mktxt", ColumnDescription = "检验特性描述", Length = 40, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Mktxt { get; set; }
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