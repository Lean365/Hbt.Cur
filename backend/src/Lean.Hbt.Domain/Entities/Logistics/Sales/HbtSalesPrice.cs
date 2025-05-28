#nullable enable

using SqlSugar;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.Entities.Logistics.Material;
using Lean.Hbt.Domain.Entities.Logistics.Sales;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSalesPrice.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述    : 销售价格实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Logistics.Sales
{
    /// <summary>
    /// 销售价格实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("hbt_sales_price", "销售价格表")]
    [SugarIndex("ix_tenant_sales_price", nameof(TenantId), OrderByType.Asc, nameof(CustomerId), OrderByType.Asc, nameof(MaterialId), OrderByType.Asc, true)]
    public class HbtSalesPrice : HbtBaseEntity
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
        /// 客户ID
        /// </summary>
        [SugarColumn(ColumnName = "customer_id", ColumnDescription = "客户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long CustomerId { get; set; }

        /// <summary>
        /// 客户
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(CustomerId))]
        public HbtCustomer? Customer { get; set; }

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
        /// 价格类型
        /// </summary>
        [SugarColumn(ColumnName = "price_type", ColumnDescription = "价格类型", ColumnDataType = "int", IsNullable = false)]
        public int PriceType { get; set; }

        /// <summary>
        /// 销售价格
        /// </summary>
        [SugarColumn(ColumnName = "sales_price", ColumnDescription = "销售价格", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal SalesPrice { get; set; }

        /// <summary>
        /// 最小数量
        /// </summary>
        [SugarColumn(ColumnName = "min_quantity", ColumnDescription = "最小数量", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal MinQuantity { get; set; }

        /// <summary>
        /// 最大数量
        /// </summary>
        [SugarColumn(ColumnName = "max_quantity", ColumnDescription = "最大数量", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal MaxQuantity { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "失效日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// 价格状态(0=停用 1=正常)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "价格状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }

    }
} 