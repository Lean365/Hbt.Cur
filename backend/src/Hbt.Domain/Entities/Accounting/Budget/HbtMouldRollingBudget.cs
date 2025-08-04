#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtMouldRollingBudget.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 模具滚动预算实体类 (基于SAP FI模具滚动预算管理)
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Hbt.Domain.Entities.Accounting.Budget
{
    /// <summary>
    /// 模具滚动预算实体类 (基于SAP FI模具滚动预算管理)
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 参考: SAP FI-FI 模具预算管理
    /// </remarks>
    [SugarTable("hbt_accounting_budget_mould_rolling_budget", "模具滚动预算表")]
    [SugarIndex("ix_mould_rolling_budget_code", nameof(CompanyCode), OrderByType.Asc, nameof(PlantCode), OrderByType.Asc, nameof(MouldCode), OrderByType.Asc, nameof(FiscalYear), OrderByType.Asc, nameof(FiscalPeriod), OrderByType.Asc, true)]
    [SugarIndex("ix_company_plant", nameof(CompanyCode), OrderByType.Asc, nameof(PlantCode), OrderByType.Asc, false)]
    public class HbtMouldRollingBudget : HbtBaseEntity
    {
        /// <summary>
        /// 公司代码
        /// </summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>
        /// 工厂代码
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 模具编码
        /// </summary>
        [SugarColumn(ColumnName = "mould_code", ColumnDescription = "模具编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MouldCode { get; set; } = string.Empty;

        /// <summary>
        /// 模具名称
        /// </summary>
        [SugarColumn(ColumnName = "mould_name", ColumnDescription = "模具名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MouldName { get; set; } = string.Empty;

        /// <summary>
        /// 模具类型(1=注塑模具 2=冲压模具 3=压铸模具 4=其他)
        /// </summary>
        [SugarColumn(ColumnName = "mould_type", ColumnDescription = "模具类型", ColumnDataType = "int", IsNullable = false)]
        public int MouldType { get; set; }

        /// <summary>
        /// 产品编码
        /// </summary>
        [SugarColumn(ColumnName = "product_code", ColumnDescription = "产品编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductCode { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [SugarColumn(ColumnName = "product_name", ColumnDescription = "产品名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductName { get; set; }

        /// <summary>
        /// 总账科目编码
        /// </summary>
        [SugarColumn(ColumnName = "gl_account_code", ColumnDescription = "总账科目编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string GlAccountCode { get; set; } = string.Empty;

        /// <summary>
        /// 预算科目编码
        /// </summary>
        [SugarColumn(ColumnName = "budget_account_code", ColumnDescription = "预算科目编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BudgetAccountCode { get; set; }

        /// <summary>
        /// 预算科目关联
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(CompanyCode), nameof(BudgetAccountCode))]
        public HbtBudgetAccount? BudgetAccount { get; set; }

        /// <summary>
        /// 财政年度
        /// </summary>
        [SugarColumn(ColumnName = "fiscal_year", ColumnDescription = "财政年度", ColumnDataType = "int", IsNullable = false)]
        public int FiscalYear { get; set; }

        /// <summary>
        /// 财政期间
        /// </summary>
        [SugarColumn(ColumnName = "fiscal_period", ColumnDescription = "财政期间", ColumnDataType = "int", IsNullable = false)]
        public int FiscalPeriod { get; set; }

        /// <summary>
        /// 滚动预算类型(1=月度滚动 2=季度滚动 3=年度滚动)
        /// </summary>
        [SugarColumn(ColumnName = "rolling_budget_type", ColumnDescription = "滚动预算类型", ColumnDataType = "int", IsNullable = false)]
        public int RollingBudgetType { get; set; }

        /// <summary>
        /// 滚动周期数
        /// </summary>
        [SugarColumn(ColumnName = "rolling_period_count", ColumnDescription = "滚动周期数", ColumnDataType = "int", IsNullable = false)]
        public int RollingPeriodCount { get; set; }

        /// <summary>
        /// 预算版本
        /// </summary>
        [SugarColumn(ColumnName = "budget_version", ColumnDescription = "预算版本", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string BudgetVersion { get; set; } = string.Empty;

        /// <summary>
        /// 原始预算金额
        /// </summary>
        [SugarColumn(ColumnName = "original_budget_amount", ColumnDescription = "原始预算金额", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal OriginalBudgetAmount { get; set; }

        /// <summary>
        /// 调整后预算金额
        /// </summary>
        [SugarColumn(ColumnName = "adjusted_budget_amount", ColumnDescription = "调整后预算金额", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal AdjustedBudgetAmount { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>
        /// 已用预算金额
        /// </summary>
        [SugarColumn(ColumnName = "used_budget_amount", ColumnDescription = "已用预算金额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal UsedBudgetAmount { get; set; } = 0;

        /// <summary>
        /// 剩余预算金额
        /// </summary>
        [SugarColumn(ColumnName = "remaining_budget_amount", ColumnDescription = "剩余预算金额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal RemainingBudgetAmount { get; set; } = 0;

        /// <summary>
        /// 预算使用率
        /// </summary>
        [SugarColumn(ColumnName = "budget_usage_rate", ColumnDescription = "预算使用率", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal BudgetUsageRate { get; set; } = 0;

        /// <summary>
        /// 滚动调整金额
        /// </summary>
        [SugarColumn(ColumnName = "rolling_adjustment_amount", ColumnDescription = "滚动调整金额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal RollingAdjustmentAmount { get; set; } = 0;

        /// <summary>
        /// 滚动结转金额
        /// </summary>
        [SugarColumn(ColumnName = "rolling_carry_forward_amount", ColumnDescription = "滚动结转金额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal RollingCarryForwardAmount { get; set; } = 0;

        /// <summary>
        /// 模具寿命(小时)
        /// </summary>
        [SugarColumn(ColumnName = "mould_life_hours", ColumnDescription = "模具寿命", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int MouldLifeHours { get; set; } = 0;

        /// <summary>
        /// 已使用寿命(小时)
        /// </summary>
        [SugarColumn(ColumnName = "used_life_hours", ColumnDescription = "已使用寿命", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int UsedLifeHours { get; set; } = 0;

        /// <summary>
        /// 剩余寿命(小时)
        /// </summary>
        [SugarColumn(ColumnName = "remaining_life_hours", ColumnDescription = "剩余寿命", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int RemainingLifeHours { get; set; } = 0;

        /// <summary>
        /// 模具状态(0=停用 1=正常 2=维护中 3=报废)
        /// </summary>
        [SugarColumn(ColumnName = "mould_status", ColumnDescription = "模具状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int MouldStatus { get; set; } = 1;

        /// <summary>
        /// 滚动预算状态(0=草稿 1=已提交 2=已审批 3=已发布 4=已关闭)
        /// </summary>
        [SugarColumn(ColumnName = "rolling_budget_status", ColumnDescription = "滚动预算状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int RollingBudgetStatus { get; set; } = 0;

        /// <summary>
        /// 滚动预算描述
        /// </summary>
        [SugarColumn(ColumnName = "rolling_budget_description", ColumnDescription = "滚动预算描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RollingBudgetDescription { get; set; }

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