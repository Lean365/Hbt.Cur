#nullable enable

using SqlSugar;

namespace Hbt.Domain.Entities.Logistics.Production.Change
{
    /// <summary>
    /// 设变执行跟踪实体（只保留部门、批次、实施、说明）
    /// </summary>
    [SugarTable("hbt_logistics_change_execution_track", "设变执行跟踪")]
    public class HbtChangeExecutionTrack : HbtBaseEntity
    {
        /// <summary>
        /// 设变号码
        /// </summary>
        [SugarColumn(ColumnName = "change_number", ColumnDescription = "设变号码", Length = 30, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 部门
        /// </summary>
        [SugarColumn(ColumnName = "department", ColumnDescription = "部门", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Department { get; set; } = string.Empty;

        /// <summary>
        /// 批次
        /// </summary>
        [SugarColumn(ColumnName = "lot", ColumnDescription = "批次", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Lot { get; set; }

        /// <summary>
        /// 采购订单
        /// </summary>
        [SugarColumn(ColumnName = "purchase_order", ColumnDescription = "采购订单", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PurchaseOrder { get; set; }

        /// <summary>
        /// 生产工单
        /// </summary>
        [SugarColumn(ColumnName = "prod_order", ColumnDescription = "生产工单", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProdOrder { get; set; }

        /// <summary>
        /// 生产班组
        /// </summary>
        [SugarColumn(ColumnName = "prod_team", ColumnDescription = "生产班组", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProdTeam { get; set; }

        /// <summary>
        /// 出库单号
        /// </summary>
        [SugarColumn(ColumnName = "delivery_order", ColumnDescription = "出库单号", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeliveryOrder { get; set; }

        /// <summary>
        /// 实施
        /// </summary>
        [SugarColumn(ColumnName = "action", ColumnDescription = "实施", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Action { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "memo", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Memo { get; set; }
    }
} 