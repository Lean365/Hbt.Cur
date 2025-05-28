#nullable enable

using SqlSugar;
using Lean.Hbt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPlantMaterial.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述    : 工厂物料实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Logistics.Material
{
    /// <summary>
    /// 工厂物料实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("hbt_plant_material", "工厂物料表")]
    [SugarIndex("ix_tenant_plant_material", nameof(TenantId), OrderByType.Asc, nameof(PlantId), OrderByType.Asc, nameof(MaterialId), OrderByType.Asc, true)]
    public class HbtPlantMaterial : HbtBaseEntity
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long TenantId { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(TenantId))]
        public HbtTenant? Tenant { get; set; }

        /// <summary>
        /// 工厂ID
        /// </summary>
        [SugarColumn(ColumnName = "plant_id", ColumnDescription = "工厂ID", ColumnDataType = "bigint", IsNullable = false)]
        public long PlantId { get; set; }

        /// <summary>
        /// 物料ID
        /// </summary>
        [SugarColumn(ColumnName = "material_id", ColumnDescription = "物料ID", ColumnDataType = "bigint", IsNullable = false)]
        public long MaterialId { get; set; }

        /// <summary>
        /// 物料
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(MaterialId))]
        public HbtMaterial? Material { get; set; }

        /// <summary>
        /// 工厂物料编码
        /// </summary>
        [SugarColumn(ColumnName = "plant_material_code", ColumnDescription = "工厂物料编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? PlantMaterialCode { get; set; }

        /// <summary>
        /// 工厂物料名称
        /// </summary>
        [SugarColumn(ColumnName = "plant_material_name", ColumnDescription = "工厂物料名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? PlantMaterialName { get; set; }

        /// <summary>
        /// 最小库存量
        /// </summary>
        [SugarColumn(ColumnName = "min_stock", ColumnDescription = "最小库存量", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal MinStock { get; set; }

        /// <summary>
        /// 最大库存量
        /// </summary>
        [SugarColumn(ColumnName = "max_stock", ColumnDescription = "最大库存量", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal MaxStock { get; set; }

        /// <summary>
        /// 安全库存量
        /// </summary>
        [SugarColumn(ColumnName = "safe_stock", ColumnDescription = "安全库存量", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal SafeStock { get; set; }

        /// <summary>
        /// 采购提前期(天)
        /// </summary>
        [SugarColumn(ColumnName = "purchase_lead_time", ColumnDescription = "采购提前期", ColumnDataType = "int", IsNullable = false)]
        public int PurchaseLeadTime { get; set; }

        /// <summary>
        /// 生产提前期(天)
        /// </summary>
        [SugarColumn(ColumnName = "production_lead_time", ColumnDescription = "生产提前期", ColumnDataType = "int", IsNullable = false)]
        public int ProductionLeadTime { get; set; }

        /// <summary>
        /// 物料状态(0=停用 1=正常)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "物料状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "remark", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Remark { get; set; }
    }
} 