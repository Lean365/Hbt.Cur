using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Domain.Entities.Logistics.Complaint
{
    /// <summary>
    /// 核心客诉主表
    /// </summary>
    [SugarTable("hbt_logistics_complaint", "核心客诉主表")]
    [SugarIndex("ix_complaint_code", nameof(Qmnum), OrderByType.Asc, true)]
    public class HbtComplaint : HbtBaseEntity
    {
        /// <summary>集团</summary>
        [SugarColumn(ColumnName = "mandt", ColumnDescription = "集团", Length = 6, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Mandt { get; set; }
        /// <summary>投诉通知编号</summary>
        [SugarColumn(ColumnName = "qmnum", ColumnDescription = "投诉通知编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Qmnum { get; set; }
        /// <summary>投诉通知类型</summary>
        [SugarColumn(ColumnName = "qmart", ColumnDescription = "投诉通知类型", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Qmart { get; set; }
        /// <summary>状态</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Status { get; set; }
        /// <summary>创建日期</summary>
        [SugarColumn(ColumnName = "erdat", ColumnDescription = "创建日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Erdat { get; set; }
        /// <summary>创建人</summary>
        [SugarColumn(ColumnName = "ernam", ColumnDescription = "创建人", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ernam { get; set; }
        /// <summary>投诉项目编号</summary>
        [SugarColumn(ColumnName = "fepos", ColumnDescription = "投诉项目编号", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Fepos { get; set; }
        /// <summary>投诉项目描述</summary>
        [SugarColumn(ColumnName = "fetxt", ColumnDescription = "投诉项目描述", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Fetxt { get; set; }
        /// <summary>投诉原因编号</summary>
        [SugarColumn(ColumnName = "grund", ColumnDescription = "投诉原因编号", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Grund { get; set; }
        /// <summary>投诉原因描述</summary>
        [SugarColumn(ColumnName = "grundtxt", ColumnDescription = "投诉原因描述", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Grundtxt { get; set; }
        /// <summary>投诉状态历史</summary>
        [SugarColumn(ColumnName = "status_history", ColumnDescription = "投诉状态历史", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? StatusHistory { get; set; }
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