using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Domain.Entities.Logistics.Service
{
    /// <summary>
    /// 服务通知表
    /// </summary>
    [SugarTable("hbt_logistics_service_notification", "服务通知表")]
    [SugarIndex("ix_service_notification_code", nameof(Qmnum), OrderByType.Asc, true)]
    public class HbtServiceNotification : HbtBaseEntity
    {
        /// <summary>集团</summary>
        [SugarColumn(ColumnName = "mandt", ColumnDescription = "集团", Length = 6, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Mandt { get; set; }
        /// <summary>服务通知编号</summary>
        [SugarColumn(ColumnName = "qmnum", ColumnDescription = "服务通知编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Qmnum { get; set; }
        /// <summary>服务通知类型</summary>
        [SugarColumn(ColumnName = "qmart", ColumnDescription = "服务通知类型", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Qmart { get; set; }
        /// <summary>物料</summary>
        [SugarColumn(ColumnName = "matnr", ColumnDescription = "物料", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Matnr { get; set; }
        /// <summary>工厂</summary>
        [SugarColumn(ColumnName = "werks", ColumnDescription = "工厂", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Werks { get; set; }
        /// <summary>通知数量</summary>
        [SugarColumn(ColumnName = "menge", ColumnDescription = "通知数量", ColumnDataType = "decimal", IsNullable = true)]
        public decimal? Menge { get; set; }
        /// <summary>单位</summary>
        [SugarColumn(ColumnName = "meins", ColumnDescription = "单位", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Meins { get; set; }
        /// <summary>通知日期</summary>
        [SugarColumn(ColumnName = "erdat", ColumnDescription = "通知日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Erdat { get; set; }
        /// <summary>通知人</summary>
        [SugarColumn(ColumnName = "ernam", ColumnDescription = "通知人", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ernam { get; set; }
        /// <summary>通知状态</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "通知状态", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Status { get; set; }
        /// <summary>通知原因</summary>
        [SugarColumn(ColumnName = "grund", ColumnDescription = "通知原因", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Grund { get; set; }
        /// <summary>通知原因描述</summary>
        [SugarColumn(ColumnName = "grundtxt", ColumnDescription = "通知原因描述", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Grundtxt { get; set; }
        /// <summary>通知项目编号</summary>
        [SugarColumn(ColumnName = "fepos", ColumnDescription = "通知项目编号", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Fepos { get; set; }
        /// <summary>通知项目描述</summary>
        [SugarColumn(ColumnName = "fetxt", ColumnDescription = "通知项目描述", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Fetxt { get; set; }
        /// <summary>通知修改日期</summary>
        [SugarColumn(ColumnName = "aedat", ColumnDescription = "通知修改日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Aedat { get; set; }
        /// <summary>通知修改人</summary>
        [SugarColumn(ColumnName = "aenam", ColumnDescription = "通知修改人", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Aenam { get; set; }
        /// <summary>通知删除标志</summary>
        [SugarColumn(ColumnName = "loekz", ColumnDescription = "通知删除标志", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Loekz { get; set; }
        /// <summary>通知删除日期</summary>
        [SugarColumn(ColumnName = "lodat", ColumnDescription = "通知删除日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Lodat { get; set; }
        /// <summary>通知删除人</summary>
        [SugarColumn(ColumnName = "lousr", ColumnDescription = "通知删除人", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Lousr { get; set; }
    }
} 