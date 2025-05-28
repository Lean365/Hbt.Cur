#nullable enable

using SqlSugar;
using Lean.Hbt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSalesBudget.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述    : 销售预算实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Finance.Budget
{
    /// <summary>
    /// 销售预算实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("hbt_sales_budget", "销售预算表")]
    [SugarIndex("ix_tenant_sales_budget", nameof(TenantId), OrderByType.Asc, nameof(BudgetYear), OrderByType.Asc, nameof(BudgetMonth), OrderByType.Asc, true)]
    public class HbtSalesBudget : HbtBaseEntity
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
        /// 预算年度
        /// </summary>
        [SugarColumn(ColumnName = "budget_year", ColumnDescription = "预算年度", ColumnDataType = "int", IsNullable = false)]
        public int BudgetYear { get; set; }

        /// <summary>
        /// 预算月份
        /// </summary>
        [SugarColumn(ColumnName = "budget_month", ColumnDescription = "预算月份", ColumnDataType = "int", IsNullable = false)]
        public int BudgetMonth { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [SugarColumn(ColumnName = "product_id", ColumnDescription = "产品ID", ColumnDataType = "bigint", IsNullable = false)]
        public long ProductId { get; set; }

        /// <summary>
        /// 销售区域ID
        /// </summary>
        [SugarColumn(ColumnName = "sales_area_id", ColumnDescription = "销售区域ID", ColumnDataType = "bigint", IsNullable = false)]
        public long SalesAreaId { get; set; }

        /// <summary>
        /// 销售渠道ID
        /// </summary>
        [SugarColumn(ColumnName = "sales_channel_id", ColumnDescription = "销售渠道ID", ColumnDataType = "bigint", IsNullable = false)]
        public long SalesChannelId { get; set; }

        /// <summary>
        /// 销售数量
        /// </summary>
        [SugarColumn(ColumnName = "sales_quantity", ColumnDescription = "销售数量", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal SalesQuantity { get; set; }

        /// <summary>
        /// 销售单价
        /// </summary>
        [SugarColumn(ColumnName = "unit_price", ColumnDescription = "销售单价", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 销售金额
        /// </summary>
        [SugarColumn(ColumnName = "sales_amount", ColumnDescription = "销售金额", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal SalesAmount { get; set; }

        /// <summary>
        /// 销售成本
        /// </summary>
        [SugarColumn(ColumnName = "sales_cost", ColumnDescription = "销售成本", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal SalesCost { get; set; }

        /// <summary>
        /// 销售毛利
        /// </summary>
        [SugarColumn(ColumnName = "sales_profit", ColumnDescription = "销售毛利", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal SalesProfit { get; set; }

        /// <summary>
        /// 销售费用
        /// </summary>
        [SugarColumn(ColumnName = "sales_expense", ColumnDescription = "销售费用", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal SalesExpense { get; set; }

        /// <summary>
        /// 销售净利
        /// </summary>
        [SugarColumn(ColumnName = "sales_net_profit", ColumnDescription = "销售净利", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal SalesNetProfit { get; set; }

        /// <summary>
        /// 预算状态(0=草稿 1=已提交 2=已审核 3=已批准)
        /// </summary>
        [SugarColumn(ColumnName = "budget_status", ColumnDescription = "预算状态", ColumnDataType = "int", IsNullable = false)]
        public int BudgetStatus { get; set; }

    }
} 