#nullable enable

using SqlSugar;
using Lean.Hbt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPlant.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述    : 工厂实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Logistics.Material
{
    /// <summary>
    /// 工厂实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("hbt_plant", "工厂表")]
    [SugarIndex("ix_tenant_plant", nameof(TenantId), OrderByType.Asc, nameof(PlantCode), OrderByType.Asc, true)]
    public class HbtPlant : HbtBaseEntity
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
        /// 工厂编码
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? PlantCode { get; set; }

        /// <summary>
        /// 工厂名称
        /// </summary>
        [SugarColumn(ColumnName = "plant_name", ColumnDescription = "工厂名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? PlantName { get; set; }

        /// <summary>
        /// 工厂简称
        /// </summary>
        [SugarColumn(ColumnName = "plant_short_name", ColumnDescription = "工厂简称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PlantShortName { get; set; }

        /// <summary>
        /// 工厂类型
        /// </summary>
        [SugarColumn(ColumnName = "plant_type", ColumnDescription = "工厂类型", ColumnDataType = "int", IsNullable = false)]
        public int PlantType { get; set; }

        /// <summary>
        /// 工厂地址
        /// </summary>
        [SugarColumn(ColumnName = "plant_address", ColumnDescription = "工厂地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PlantAddress { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [SugarColumn(ColumnName = "contact_person", ColumnDescription = "联系人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactPerson { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [SugarColumn(ColumnName = "contact_phone", ColumnDescription = "联系电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactPhone { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [SugarColumn(ColumnName = "email", ColumnDescription = "电子邮箱", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Email { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false)]
        public int OrderNum { get; set; }

        /// <summary>
        /// 工厂状态(0=停用 1=正常)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "工厂状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }

    }
} 