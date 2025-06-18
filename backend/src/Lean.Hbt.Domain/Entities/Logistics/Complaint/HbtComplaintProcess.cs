using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Domain.Entities.Logistics.Complaint
{
    /// <summary>
    /// 客诉处理表
    /// </summary>
    [SugarTable("hbt_logistics_complaint_process", "客诉处理表")]
    [SugarIndex("ix_complaint_process_code", nameof(Qmnum), OrderByType.Asc, true)]
    public class HbtComplaintProcess : HbtBaseEntity
    {
        /// <summary>集团</summary>
        [SugarColumn(ColumnName = "mandt", ColumnDescription = "集团", Length = 6, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Mandt { get; set; }
        /// <summary>投诉通知编号</summary>
        [SugarColumn(ColumnName = "qmnum", ColumnDescription = "投诉通知编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Qmnum { get; set; }
        /// <summary>措施编号</summary>
        [SugarColumn(ColumnName = "manum", ColumnDescription = "措施编号", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Manum { get; set; }
        /// <summary>措施描述</summary>
        [SugarColumn(ColumnName = "matxt", ColumnDescription = "措施描述", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Matxt { get; set; }
        /// <summary>任务编号</summary>
        [SugarColumn(ColumnName = "tasknum", ColumnDescription = "任务编号", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Tasknum { get; set; }
        /// <summary>任务描述</summary>
        [SugarColumn(ColumnName = "tasktxt", ColumnDescription = "任务描述", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Tasktxt { get; set; }
        /// <summary>文档编号</summary>
        [SugarColumn(ColumnName = "docnum", ColumnDescription = "文档编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Docnum { get; set; }
        /// <summary>文档链接</summary>
        [SugarColumn(ColumnName = "doclink", ColumnDescription = "文档链接", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Doclink { get; set; }
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