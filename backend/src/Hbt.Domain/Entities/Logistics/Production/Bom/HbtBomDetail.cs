#nullable enable

using SqlSugar;

namespace Hbt.Cur.Domain.Entities.Logistics.Production
{
    /// <summary>
    /// BOM组件明细实体（基于SAP BOM创建CS01）
    /// </summary>
    [SugarTable("hbt_logistics_bom_detail", "BOM组件明细表")]
    [SugarIndex("ix_bom_head_id", nameof(BomHeadId), OrderByType.Asc, false)]
    [SugarIndex("ix_component_code", nameof(ComponentCode), OrderByType.Asc, false)]
    [SugarIndex("ix_item_id", nameof(ItemId), OrderByType.Asc, false)]
    public class HbtBomDetail : HbtBaseEntity
    {
        /// <summary>BOM主表Id</summary>
        [SugarColumn(ColumnName = "bom_head_id", ColumnDescription = "BOM主表Id", IsNullable = false)]
        public long BomHeadId { get; set; }

        /// <summary>组件编码</summary>
        [SugarColumn(ColumnName = "component_code", ColumnDescription = "组件编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ComponentCode { get; set; } = string.Empty;

        /// <summary>项目</summary>
        [SugarColumn(ColumnName = "item", ColumnDescription = "项目", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Item { get; set; } = string.Empty;

        /// <summary>项目类型</summary>
        [SugarColumn(ColumnName = "item_type", ColumnDescription = "项目类型", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ItemType { get; set; } = string.Empty;

        /// <summary>组件名称</summary>
        [SugarColumn(ColumnName = "component_name", ColumnDescription = "组件名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ComponentName { get; set; } = string.Empty;

        /// <summary>数量</summary>
        [SugarColumn(ColumnName = "quantity", ColumnDescription = "数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal Quantity { get; set; } = 0;

        /// <summary>单位</summary>
        [SugarColumn(ColumnName = "unit", ColumnDescription = "单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Unit { get; set; } = string.Empty;

        /// <summary>装配</summary>
        [SugarColumn(ColumnName = "assembly", ColumnDescription = "装配", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Assembly { get; set; }

        /// <summary>有效起始日期</summary>
        [SugarColumn(ColumnName = "valid_from", ColumnDescription = "有效起始日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime ValidFrom { get; set; }

        /// <summary>有效截止日期</summary>
        [SugarColumn(ColumnName = "valid_to", ColumnDescription = "有效截止日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ValidTo { get; set; }

        /// <summary>设变号码</summary>
        [SugarColumn(ColumnName = "change_number", ColumnDescription = "设变号码", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeNumber { get; set; }

        /// <summary>虚拟件标识</summary>
        [SugarColumn(ColumnName = "phantom_flag", ColumnDescription = "虚拟件标识", ColumnDataType = "bit", IsNullable = false, DefaultValue = "0")]
        public bool PhantomFlag { get; set; } = false;

        /// <summary>项目ID</summary>
        [SugarColumn(ColumnName = "item_id", ColumnDescription = "项目ID", IsNullable = false)]
        public long ItemId { get; set; }

        // ========== 导航属性 ==========
        /// <summary>BOM主表</summary>
        [Navigate(NavigateType.OneToOne, nameof(BomHeadId))]
        public HbtBomHead? BomHead { get; set; }

        /// <summary>BOM物料项列表</summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtBomItem.BomDetailId))]
        public List<HbtBomItem> BomItems { get; set; } = new();
    }
} 