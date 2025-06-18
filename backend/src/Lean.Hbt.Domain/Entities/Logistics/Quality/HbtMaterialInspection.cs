using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Domain.Entities.Logistics.Quality
{
    /// <summary>
    /// 物料检验表
    /// </summary>
    [SugarTable("hbt_logistics_quality_material_inspection", "物料检验表")]
    [SugarIndex("ix_material_inspection_code", nameof(Matnr), OrderByType.Asc, true)]
    public class HbtMaterialInspection : HbtBaseEntity
    {
        /// <summary>集团</summary>
        [SugarColumn(ColumnName = "mandt", ColumnDescription = "集团", Length = 6, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Mandt { get; set; }
        /// <summary>物料编号</summary>
        [SugarColumn(ColumnName = "matnr", ColumnDescription = "物料编号", Length = 18, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Matnr { get; set; }
        /// <summary>工厂</summary>
        [SugarColumn(ColumnName = "werks", ColumnDescription = "工厂", Length = 4, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Werks { get; set; }
        /// <summary>检验计划编号</summary>
        [SugarColumn(ColumnName = "plnnr", ColumnDescription = "检验计划编号", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Plnnr { get; set; }
        /// <summary>检验计划组计数器</summary>
        [SugarColumn(ColumnName = "plnal", ColumnDescription = "检验计划组计数器", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Plnal { get; set; }
        /// <summary>物料类型</summary>
        [SugarColumn(ColumnName = "mtart", ColumnDescription = "物料类型", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Mtart { get; set; }
        /// <summary>基本计量单位</summary>
        [SugarColumn(ColumnName = "meins", ColumnDescription = "基本计量单位", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Meins { get; set; }
        /// <summary>质量管理视图激活</summary>
        [SugarColumn(ColumnName = "xchpf", ColumnDescription = "质量管理视图激活", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Xchpf { get; set; }
        /// <summary>检验类型</summary>
        [SugarColumn(ColumnName = "art", ColumnDescription = "检验类型", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Art { get; set; }
        /// <summary>检验周期</summary>
        [SugarColumn(ColumnName = "pruefzykl", ColumnDescription = "检验周期", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Pruefzykl { get; set; }
        /// <summary>检验周期单位</summary>
        [SugarColumn(ColumnName = "zyklme", ColumnDescription = "检验周期单位", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Zyklme { get; set; }
        /// <summary>检验间隔</summary>
        [SugarColumn(ColumnName = "pruefint", ColumnDescription = "检验间隔", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Pruefint { get; set; }
        /// <summary>检验间隔单位</summary>
        [SugarColumn(ColumnName = "intme", ColumnDescription = "检验间隔单位", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Intme { get; set; }
        /// <summary>批次管理</summary>
        [SugarColumn(ColumnName = "xchpf_batch", ColumnDescription = "批次管理", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? XchpfBatch { get; set; }
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