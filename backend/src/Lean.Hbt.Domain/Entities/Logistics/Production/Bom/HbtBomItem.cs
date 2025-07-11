#nullable enable

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Logistics.Production
{
    /// <summary>
    /// BOM物料项实体
    /// </summary>
    [SugarTable("hbt_logistics_bom_item", "BOM物料项表")]
    [SugarIndex("ix_bom_detail_id", nameof(BomDetailId), OrderByType.Asc, false)]
    [SugarIndex("ix_material_code", nameof(MaterialCode), OrderByType.Asc, false)]
    [SugarIndex("ix_item_number", nameof(ItemNumber), OrderByType.Asc, false)]
    public class HbtBomItem : HbtBaseEntity
    {
        /// <summary>BOM明细Id</summary>
        [SugarColumn(ColumnName = "bom_detail_id", ColumnDescription = "BOM明细Id", IsNullable = false)]
        public long BomDetailId { get; set; }

        /// <summary>物料编码</summary>
        [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>物料名称</summary>
        [SugarColumn(ColumnName = "material_name", ColumnDescription = "物料名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialName { get; set; } = string.Empty;

        /// <summary>物料规格</summary>
        [SugarColumn(ColumnName = "material_specification", ColumnDescription = "物料规格", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialSpecification { get; set; }

        /// <summary>数量</summary>
        [SugarColumn(ColumnName = "quantity", ColumnDescription = "数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal Quantity { get; set; } = 0;

        /// <summary>单位</summary>
        [SugarColumn(ColumnName = "unit", ColumnDescription = "单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Unit { get; set; } = string.Empty;

        /// <summary>位置</summary>
        [SugarColumn(ColumnName = "position", ColumnDescription = "位置", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Position { get; set; }

        /// <summary>物料番号</summary>
        [SugarColumn(ColumnName = "item_number", ColumnDescription = "物料番号", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ItemNumber { get; set; }

        /// <summary>图纸编号</summary>
        [SugarColumn(ColumnName = "drawing_number", ColumnDescription = "图纸编号", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DrawingNumber { get; set; }

        /// <summary>图纸版本</summary>
        [SugarColumn(ColumnName = "drawing_version", ColumnDescription = "图纸版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DrawingVersion { get; set; }

        /// <summary>备注</summary>
        [SugarColumn(ColumnName = "remarks", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Remarks { get; set; }

        // ========== 导航属性 ==========
        /// <summary>BOM明细</summary>
        [Navigate(NavigateType.OneToOne, nameof(BomDetailId))]
        public HbtBomDetail? BomDetail { get; set; }
    }
} 