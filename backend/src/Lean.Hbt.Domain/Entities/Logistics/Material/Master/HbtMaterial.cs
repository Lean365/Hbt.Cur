#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtMaterial.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述    : 物料主数据实体类 (基于SAP MM物料管理)
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Logistics.Material
{
    /// <summary>
    /// 物料主数据实体类 (基于SAP MM物料管理)
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 参考: SAP MM 物料管理模块
    /// </remarks>
    [SugarTable("hbt_logistics_material", "物料主数据表")]
    [SugarIndex("ix_material_code", nameof(MaterialCode), OrderByType.Asc, true)]
    [SugarIndex("ix_material_type", nameof(MaterialType), OrderByType.Asc, false)]
    [SugarIndex("ix_material_group", nameof(MaterialGroup), OrderByType.Asc, false)]
    public class HbtMaterial : HbtBaseEntity
    {
        /// <summary>
        /// 物料编码
        /// </summary>
        [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料类型(1=原材料 2=半成品 3=成品 4=包装材料 5=辅助材料 6=其他)
        /// </summary>
        [SugarColumn(ColumnName = "material_type", ColumnDescription = "物料类型", ColumnDataType = "int", IsNullable = false)]
        public int MaterialType { get; set; }

        /// <summary>
        /// 物料组
        /// </summary>
        [SugarColumn(ColumnName = "material_group", ColumnDescription = "物料组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialGroup { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        [SugarColumn(ColumnName = "material_name", ColumnDescription = "物料名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialName { get; set; } = string.Empty;

        /// <summary>
        /// 物料规格
        /// </summary>
        [SugarColumn(ColumnName = "material_spec", ColumnDescription = "物料规格", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialSpec { get; set; }

        /// <summary>
        /// 物料型号
        /// </summary>
        [SugarColumn(ColumnName = "material_model", ColumnDescription = "物料型号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialModel { get; set; }

        /// <summary>
        /// 基本计量单位
        /// </summary>
        [SugarColumn(ColumnName = "base_unit", ColumnDescription = "基本计量单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string BaseUnit { get; set; } = string.Empty;

        /// <summary>
        /// 采购组
        /// </summary>
        [SugarColumn(ColumnName = "purchase_group", ColumnDescription = "采购组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PurchaseGroup { get; set; }

        /// <summary>
        /// 旧物料编码
        /// </summary>
        [SugarColumn(ColumnName = "old_material_code", ColumnDescription = "旧物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldMaterialCode { get; set; }

        /// <summary>
        /// 物料阶层(1=原材料 2=半成品 3=成品 4=包装材料 5=辅助材料 6=其他)
        /// </summary>
        [SugarColumn(ColumnName = "material_level", ColumnDescription = "物料阶层", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int MaterialLevel { get; set; } = 1;

        /// <summary>
        /// 产品层次(1=产品族 2=产品线 3=产品系列 4=产品型号 5=产品规格 6=其他)
        /// </summary>
        [SugarColumn(ColumnName = "product_hierarchy", ColumnDescription = "产品层次", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ProductHierarchy { get; set; } = 1;

        /// <summary>
        /// 毛重
        /// </summary>
        [SugarColumn(ColumnName = "gross_weight", ColumnDescription = "毛重", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal GrossWeight { get; set; } = 0;

        /// <summary>
        /// 净重
        /// </summary>
        [SugarColumn(ColumnName = "net_weight", ColumnDescription = "净重", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal NetWeight { get; set; } = 0;

        /// <summary>
        /// 重量单位
        /// </summary>
        [SugarColumn(ColumnName = "weight_unit", ColumnDescription = "重量单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? WeightUnit { get; set; }

        /// <summary>
        /// 体积
        /// </summary>
        [SugarColumn(ColumnName = "volume", ColumnDescription = "体积", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal Volume { get; set; } = 0;

        /// <summary>
        /// 体积单位
        /// </summary>
        [SugarColumn(ColumnName = "volume_unit", ColumnDescription = "体积单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? VolumeUnit { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        [SugarColumn(ColumnName = "length", ColumnDescription = "长度", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal Length { get; set; } = 0;

        /// <summary>
        /// 宽度
        /// </summary>
        [SugarColumn(ColumnName = "width", ColumnDescription = "宽度", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal Width { get; set; } = 0;

        /// <summary>
        /// 高度
        /// </summary>
        [SugarColumn(ColumnName = "height", ColumnDescription = "高度", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal Height { get; set; } = 0;

        /// <summary>
        /// 尺寸单位
        /// </summary>
        [SugarColumn(ColumnName = "dimension_unit", ColumnDescription = "尺寸单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DimensionUnit { get; set; }

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
        /// 是否危险品
        /// </summary>
        [SugarColumn(ColumnName = "is_dangerous", ColumnDescription = "是否危险品", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsDangerous { get; set; } = 0;

        /// <summary>
        /// 是否易碎品
        /// </summary>
        [SugarColumn(ColumnName = "is_fragile", ColumnDescription = "是否易碎品", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsFragile { get; set; } = 0;

        /// <summary>
        /// 是否易腐品
        /// </summary>
        [SugarColumn(ColumnName = "is_perishable", ColumnDescription = "是否易腐品", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsPerishable { get; set; } = 0;

        /// <summary>
        /// 存储条件(1=常温 2=冷藏 3=冷冻 4=恒温 5=其他)
        /// </summary>
        [SugarColumn(ColumnName = "storage_condition", ColumnDescription = "存储条件", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int StorageCondition { get; set; } = 1;

        /// <summary>
        /// 最小存储温度
        /// </summary>
        [SugarColumn(ColumnName = "min_temperature", ColumnDescription = "最小存储温度", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? MinTemperature { get; set; }

        /// <summary>
        /// 最大存储温度
        /// </summary>
        [SugarColumn(ColumnName = "max_temperature", ColumnDescription = "最大存储温度", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? MaxTemperature { get; set; }

        /// <summary>
        /// 温度单位
        /// </summary>
        [SugarColumn(ColumnName = "temperature_unit", ColumnDescription = "温度单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TemperatureUnit { get; set; }

        /// <summary>
        /// 是否循环盘点
        /// </summary>
        [SugarColumn(ColumnName = "is_cycle_count", ColumnDescription = "是否循环盘点", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsCycleCount { get; set; } = 0;

        /// <summary>
        /// 循环盘点周期(天)
        /// </summary>
        [SugarColumn(ColumnName = "cycle_count_period", ColumnDescription = "循环盘点周期", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int CycleCountPeriod { get; set; } = 0;

        /// <summary>
        /// 物料描述
        /// </summary>
        [SugarColumn(ColumnName = "material_description", ColumnDescription = "物料描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialDescription { get; set; }

        /// <summary>
        /// 技术参数
        /// </summary>
        [SugarColumn(ColumnName = "technical_parameters", ColumnDescription = "技术参数", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TechnicalParameters { get; set; }

        /// <summary>
        /// 质量标准
        /// </summary>
        [SugarColumn(ColumnName = "quality_standard", ColumnDescription = "质量标准", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? QualityStandard { get; set; }

        /// <summary>
        /// 包装要求
        /// </summary>
        [SugarColumn(ColumnName = "packaging_requirements", ColumnDescription = "包装要求", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PackagingRequirements { get; set; }

        /// <summary>
        /// 运输要求
        /// </summary>
        [SugarColumn(ColumnName = "transport_requirements", ColumnDescription = "运输要求", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TransportRequirements { get; set; }

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