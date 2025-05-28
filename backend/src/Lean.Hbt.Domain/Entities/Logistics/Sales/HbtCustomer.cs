#nullable enable

using SqlSugar;
using Lean.Hbt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtCustomer.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述    : 客户实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Logistics.Sales
{
    /// <summary>
    /// 客户实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("hbt_customer", "客户表")]
    [SugarIndex("ix_tenant_customer", nameof(TenantId), OrderByType.Asc, nameof(CustomerCode), OrderByType.Asc, true)]
    public class HbtCustomer : HbtBaseEntity
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
        /// 客户编码
        /// </summary>
        [SugarColumn(ColumnName = "customer_code", ColumnDescription = "客户编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? CustomerCode { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        [SugarColumn(ColumnName = "customer_name", ColumnDescription = "客户名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? CustomerName { get; set; }

        /// <summary>
        /// 客户简称
        /// </summary>
        [SugarColumn(ColumnName = "customer_short_name", ColumnDescription = "客户简称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CustomerShortName { get; set; }

        /// <summary>
        /// 客户类型
        /// </summary>
        [SugarColumn(ColumnName = "customer_type", ColumnDescription = "客户类型", ColumnDataType = "int", IsNullable = false)]
        public int CustomerType { get; set; }

        /// <summary>
        /// 客户级别
        /// </summary>
        [SugarColumn(ColumnName = "customer_level", ColumnDescription = "客户级别", ColumnDataType = "int", IsNullable = false)]
        public int CustomerLevel { get; set; }

        /// <summary>
        /// 客户地址
        /// </summary>
        [SugarColumn(ColumnName = "customer_address", ColumnDescription = "客户地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CustomerAddress { get; set; }

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
        /// 客户状态(0=停用 1=正常)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "客户状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }


    }
} 