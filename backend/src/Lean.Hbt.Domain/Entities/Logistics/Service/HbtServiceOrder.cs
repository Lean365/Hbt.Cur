using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Domain.Entities.Logistics.Service
{
    /// <summary>
    /// 服务主数据表
    /// </summary>
    [SugarTable("hbt_logistics_service_order", "服务主数据表")]
    [SugarIndex("ix_service_order_code", nameof(Guid), OrderByType.Asc, true)]
    public class HbtServiceOrder : HbtBaseEntity
    {
        /// <summary>集团</summary>
        [SugarColumn(ColumnName = "mandt", ColumnDescription = "集团", Length = 6, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Mandt { get; set; }
        /// <summary>服务订单GUID</summary>
        [SugarColumn(ColumnName = "guid", ColumnDescription = "服务订单GUID", Length = 32, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Guid { get; set; }
        /// <summary>服务订单编号</summary>
        [SugarColumn(ColumnName = "object_id", ColumnDescription = "服务订单编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? ObjectId { get; set; }
        /// <summary>服务订单类型</summary>
        [SugarColumn(ColumnName = "process_type", ColumnDescription = "服务订单类型", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProcessType { get; set; }
        /// <summary>服务订单描述</summary>
        [SugarColumn(ColumnName = "description", ColumnDescription = "服务订单描述", Length = 120, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Description { get; set; }
        /// <summary>服务订单项目GUID</summary>
        [SugarColumn(ColumnName = "item_guid", ColumnDescription = "服务订单项目GUID", Length = 32, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ItemGuid { get; set; }
        /// <summary>服务项目编号</summary>
        [SugarColumn(ColumnName = "item_object_id", ColumnDescription = "服务项目编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ItemObjectId { get; set; }
        /// <summary>服务项目类型</summary>
        [SugarColumn(ColumnName = "item_type", ColumnDescription = "服务项目类型", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ItemType { get; set; }
        /// <summary>服务数据GUID</summary>
        [SugarColumn(ColumnName = "service_guid", ColumnDescription = "服务数据GUID", Length = 32, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ServiceGuid { get; set; }
        /// <summary>服务代码</summary>
        [SugarColumn(ColumnName = "service_code", ColumnDescription = "服务代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ServiceCode { get; set; }
        /// <summary>服务描述</summary>
        [SugarColumn(ColumnName = "service_desc", ColumnDescription = "服务描述", Length = 120, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ServiceDesc { get; set; }
        /// <summary>合作伙伴GUID</summary>
        [SugarColumn(ColumnName = "partner_guid", ColumnDescription = "合作伙伴GUID", Length = 32, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PartnerGuid { get; set; }
        /// <summary>合作伙伴编号</summary>
        [SugarColumn(ColumnName = "partner_no", ColumnDescription = "合作伙伴编号", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PartnerNo { get; set; }
        /// <summary>合作伙伴类型</summary>
        [SugarColumn(ColumnName = "partner_fct", ColumnDescription = "合作伙伴类型", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PartnerFct { get; set; }
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