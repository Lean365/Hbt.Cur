#nullable enable

using SqlSugar;

namespace Hbt.Domain.Entities.Logistics.Production
{
    /// <summary>
    /// BOM主表实体
    /// </summary>
    [SugarTable("hbt_logistics_bom_head", "BOM主表")]
    [SugarIndex("ix_material_code", nameof(MaterialCode), OrderByType.Asc, false)]
    [SugarIndex("ix_plant_code", nameof(PlantCode), OrderByType.Asc, false)]
    public class HbtBomHead : HbtBaseEntity
    {
        /// <summary>物料编码</summary>
        [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>工厂代码</summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>用途</summary>
        [SugarColumn(ColumnName = "usage", ColumnDescription = "用途", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Usage { get; set; }

        /// <summary>设变号码</summary>
        [SugarColumn(ColumnName = "change_number", ColumnDescription = "设变号码", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeNumber { get; set; }

        /// <summary>生效日期</summary>
        [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime EffectiveDate { get; set; }

        /// <summary>版本</summary>
        [SugarColumn(ColumnName = "version", ColumnDescription = "版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Version { get; set; } = string.Empty;

        // ========== 导航属性 ==========
        /// <summary>BOM明细列表</summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtBomDetail.BomHeadId))]
        public List<HbtBomDetail> BomDetails { get; set; } = new();
    }
} 