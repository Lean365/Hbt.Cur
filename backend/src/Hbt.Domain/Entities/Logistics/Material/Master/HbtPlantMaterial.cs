#nullable enable

using SqlSugar;
using Hbt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPlantMaterial.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 工厂物料实体类 (基于SAP MM物料管理)
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Hbt.Domain.Entities.Logistics.Material
{
    /// <summary>
    /// 工厂物料实体类 (基于SAP MM物料管理)
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 参考: SAP MM 物料管理模块
    /// </remarks>
    [SugarTable("hbt_logistics_plant_material", "工厂物料")]
    [SugarIndex("ix_plant_material", nameof(PlantCode), OrderByType.Asc, nameof(MaterialCode), OrderByType.Asc, true)]
    [SugarIndex("ix_material_code", nameof(MaterialCode), OrderByType.Asc, false)]
    [SugarIndex("ix_plant_code", nameof(PlantCode), OrderByType.Asc, false)]
    public class HbtPlantMaterial : HbtBaseEntity
    {
        /// <summary>
        /// 工厂编码
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂编码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料主数据关联
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(MaterialCode))]
        public HbtMaterial? Material { get; set; }

        /// <summary>
        /// 工厂关联
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(PlantCode))]
        public HbtPlant? Plant { get; set; }

        /// <summary>
        /// 工厂特定物料状态(0=停用 1=正常 2=冻结 3=待审核)
        /// </summary>
        [SugarColumn(ColumnName = "plant_material_status", ColumnDescription = "工厂特定物料状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int PlantMaterialStatus { get; set; } = 1;


        /// <summary>
        /// 工厂特定物料状态生效日期
        /// </summary>
        [SugarColumn(ColumnName = "plant_material_status_date", ColumnDescription = "工厂特定物料状态生效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PlantMaterialStatusDate { get; set; }

        /// <summary>
        /// 是否批次管理
        /// </summary>
        [SugarColumn(ColumnName = "is_batch_managed", ColumnDescription = "是否批次管理", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsBatchManaged { get; set; } = 0;

        /// <summary>
        /// 是否序列号管理
        /// </summary>
        [SugarColumn(ColumnName = "is_serial_managed", ColumnDescription = "是否序列号管理", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsSerialManaged { get; set; } = 0;

        /// <summary>
        /// 是否有效期管理
        /// </summary>
        [SugarColumn(ColumnName = "is_shelf_life_managed", ColumnDescription = "是否有效期管理", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsShelfLifeManaged { get; set; } = 0;

        /// <summary>
        /// ABC分类(A=高价值 B=中价值 C=低价值)
        /// </summary>
        [SugarColumn(ColumnName = "abc_classification", ColumnDescription = "ABC分类", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AbcClassification { get; set; }

        /// <summary>
        /// 是否关键物料
        /// </summary>
        [SugarColumn(ColumnName = "is_critical_material", ColumnDescription = "是否关键物料", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsCriticalMaterial { get; set; } = 0;

        /// <summary>
        /// 采购组
        /// </summary>
        [SugarColumn(ColumnName = "purchase_group", ColumnDescription = "采购组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PurchaseGroup { get; set; }

        /// <summary>
        /// 发货单位
        /// </summary>
        [SugarColumn(ColumnName = "issue_unit", ColumnDescription = "发货单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IssueUnit { get; set; }

        /// <summary>
        /// MRP参数文件
        /// </summary>
        [SugarColumn(ColumnName = "mrp_profile", ColumnDescription = "MRP参数文件", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MrpProfile { get; set; }

        /// <summary>
        /// MRP类型(1=消耗型 2=生产型 3=采购型 4=其他)
        /// </summary>
        [SugarColumn(ColumnName = "mrp_type", ColumnDescription = "MRP类型", ColumnDataType = "int", IsNullable = true)]
        public int? MrpType { get; set; }

        /// <summary>
        /// MRP控制者
        /// </summary>
        [SugarColumn(ColumnName = "mrp_controller", ColumnDescription = "MRP控制者", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MrpController { get; set; }


        /// <summary>
        /// 计划交货时间(天)
        /// </summary>
        [SugarColumn(ColumnName = "planned_delivery_time", ColumnDescription = "计划交货时间", ColumnDataType = "decimal(5,1)", IsNullable = false, DefaultValue = "0")]
        public decimal PlannedDeliveryTime { get; set; } = 0;

        /// <summary>
        /// 收货处理时间(天)
        /// </summary>
        [SugarColumn(ColumnName = "goods_receipt_time", ColumnDescription = "收货处理时间", ColumnDataType = "decimal(5,1)", IsNullable = false, DefaultValue = "0")]
        public decimal GoodsReceiptTime { get; set; } = 0;

        /// <summary>
        /// 期间标识
        /// </summary>
        [SugarColumn(ColumnName = "period_indicator", ColumnDescription = "期间标识", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PeriodIndicator { get; set; }

        /// <summary>
        /// 装配报废百分比
        /// </summary>
        [SugarColumn(ColumnName = "assembly_scrap_percent", ColumnDescription = "装配报废百分比", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal AssemblyScrapPercent { get; set; } = 0;

        /// <summary>
        /// 批量大小
        /// </summary>
        [SugarColumn(ColumnName = "lot_size", ColumnDescription = "批量大小", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LotSize { get; set; }

        /// <summary>
        /// 采购类型(1=外部采购 2=内部生产 3=库存转储 4=其他)
        /// </summary>
        [SugarColumn(ColumnName = "procurement_type", ColumnDescription = "采购类型", ColumnDataType = "int", IsNullable = true)]
        public int? ProcurementType { get; set; }

        /// <summary>
        /// 特殊采购类型
        /// </summary>
        [SugarColumn(ColumnName = "special_procurement_type", ColumnDescription = "特殊采购类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SpecialProcurementType { get; set; }

        /// <summary>
        /// 最小安全库存
        /// </summary>
        [SugarColumn(ColumnName = "min_safety_stock", ColumnDescription = "最小安全库存", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal MinSafetyStock { get; set; } = 0;

        /// <summary>
        /// 最大安全库存
        /// </summary>
        [SugarColumn(ColumnName = "max_safety_stock", ColumnDescription = "最大安全库存", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal MaxSafetyStock { get; set; } = 0;

        /// <summary>
        /// 重订货点
        /// </summary>
        [SugarColumn(ColumnName = "reorder_point", ColumnDescription = "重订货点", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ReorderPoint { get; set; } = 0;

        /// <summary>
        /// 经济订货量
        /// </summary>
        [SugarColumn(ColumnName = "economic_order_quantity", ColumnDescription = "经济订货量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal EconomicOrderQuantity { get; set; } = 0;

        /// <summary>
        /// 是否启用质量管理
        /// </summary>
        [SugarColumn(ColumnName = "is_quality_managed", ColumnDescription = "是否启用质量管理", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsQualityManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用成本管理
        /// </summary>
        [SugarColumn(ColumnName = "is_cost_managed", ColumnDescription = "是否启用成本管理", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsCostManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用库存管理
        /// </summary>
        [SugarColumn(ColumnName = "is_inventory_managed", ColumnDescription = "是否启用库存管理", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsInventoryManaged { get; set; } = 1;

        /// <summary>
        /// 是否启用生产管理
        /// </summary>
        [SugarColumn(ColumnName = "is_production_managed", ColumnDescription = "是否启用生产管理", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsProductionManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用采购管理
        /// </summary>
        [SugarColumn(ColumnName = "is_purchase_managed", ColumnDescription = "是否启用采购管理", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsPurchaseManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用销售管理
        /// </summary>
        [SugarColumn(ColumnName = "is_sales_managed", ColumnDescription = "是否启用销售管理", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsSalesManaged { get; set; } = 0;


        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false)]
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态(0=停用 1=正常)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }
    }
} 