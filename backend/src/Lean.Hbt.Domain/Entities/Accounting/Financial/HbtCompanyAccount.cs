#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtCompanyAccount.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 公司科目表实体类 (基于SAP FI公司科目表SKB1)
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

using System.Collections.Generic;

namespace Lean.Hbt.Domain.Entities.Accounting.Financial
{
    /// <summary>
    /// 公司科目表实体类 (基于SAP FI公司科目表SKB1)
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 参考: SAP FI-FI 公司科目表 (SKB1)
    /// </remarks>
    [SugarTable("hbt_accounting_financial_company_account", "公司科目表")]
    [SugarIndex("ix_company_gl_account", nameof(CompanyCode), OrderByType.Asc, nameof(GlAccountCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_plant", nameof(CompanyCode), OrderByType.Asc, nameof(PlantCode), OrderByType.Asc, false)]
    public class HbtCompanyAccount : HbtBaseEntity
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
        /// 总账科目编码
        /// </summary>
        [SugarColumn(ColumnName = "gl_account_code", ColumnDescription = "总账科目编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string GlAccountCode { get; set; } = string.Empty;

        /// <summary>
        /// 总账科目主数据
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(GlAccountCode))]
        public HbtGlAccount? GlAccount { get; set; }

        /// <summary>
        /// 预算科目关联
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(CompanyCode), nameof(GlAccountCode))]
        public List<Budget.HbtBudgetAccount>? BudgetAccounts { get; set; }

        /// <summary>
        /// 公司科目编码
        /// </summary>
        [SugarColumn(ColumnName = "company_account_code", ColumnDescription = "公司科目编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyAccountCode { get; set; }

        /// <summary>
        /// 公司科目名称
        /// </summary>
        [SugarColumn(ColumnName = "company_account_name", ColumnDescription = "公司科目名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyAccountName { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>
        /// 余额方向(1=借方 2=贷方)
        /// </summary>
        [SugarColumn(ColumnName = "balance_direction", ColumnDescription = "余额方向", ColumnDataType = "int", IsNullable = false)]
        public int BalanceDirection { get; set; }

        /// <summary>
        /// 是否允许过账
        /// </summary>
        [SugarColumn(ColumnName = "allow_posting", ColumnDescription = "是否允许过账", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int AllowPosting { get; set; } = 1;

        /// <summary>
        /// 是否允许手工过账
        /// </summary>
        [SugarColumn(ColumnName = "allow_manual_posting", ColumnDescription = "是否允许手工过账", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int AllowManualPosting { get; set; } = 1;

        /// <summary>
        /// 是否允许自动过账
        /// </summary>
        [SugarColumn(ColumnName = "allow_auto_posting", ColumnDescription = "是否允许自动过账", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int AllowAutoPosting { get; set; } = 1;

        /// <summary>
        /// 是否允许负数余额
        /// </summary>
        [SugarColumn(ColumnName = "allow_negative_balance", ColumnDescription = "是否允许负数余额", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int AllowNegativeBalance { get; set; } = 0;

        /// <summary>
        /// 是否允许跨年度过账
        /// </summary>
        [SugarColumn(ColumnName = "allow_cross_year_posting", ColumnDescription = "是否允许跨年度过账", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int AllowCrossYearPosting { get; set; } = 1;

        /// <summary>
        /// 是否明细账
        /// </summary>
        [SugarColumn(ColumnName = "is_detail_account", ColumnDescription = "是否明细账", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsDetailAccount { get; set; } = 0;

        /// <summary>
        /// 是否辅助核算
        /// </summary>
        [SugarColumn(ColumnName = "is_auxiliary_account", ColumnDescription = "是否辅助核算", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsAuxiliaryAccount { get; set; } = 0;

        /// <summary>
        /// 是否外币核算
        /// </summary>
        [SugarColumn(ColumnName = "is_foreign_currency", ColumnDescription = "是否外币核算", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsForeignCurrency { get; set; } = 0;

        /// <summary>
        /// 是否数量核算
        /// </summary>
        [SugarColumn(ColumnName = "is_quantity_account", ColumnDescription = "是否数量核算", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsQuantityAccount { get; set; } = 0;

        /// <summary>
        /// 科目余额
        /// </summary>
        [SugarColumn(ColumnName = "account_balance", ColumnDescription = "科目余额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal AccountBalance { get; set; } = 0;

        /// <summary>
        /// 期初余额
        /// </summary>
        [SugarColumn(ColumnName = "opening_balance", ColumnDescription = "期初余额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal OpeningBalance { get; set; } = 0;

        /// <summary>
        /// 期末余额
        /// </summary>
        [SugarColumn(ColumnName = "closing_balance", ColumnDescription = "期末余额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ClosingBalance { get; set; } = 0;

        /// <summary>
        /// 本年累计借方
        /// </summary>
        [SugarColumn(ColumnName = "year_debit_total", ColumnDescription = "本年累计借方", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal YearDebitTotal { get; set; } = 0;

        /// <summary>
        /// 本年累计贷方
        /// </summary>
        [SugarColumn(ColumnName = "year_credit_total", ColumnDescription = "本年累计贷方", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal YearCreditTotal { get; set; } = 0;

        /// <summary>
        /// 科目启用日期
        /// </summary>
        [SugarColumn(ColumnName = "account_start_date", ColumnDescription = "科目启用日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? AccountStartDate { get; set; }

        /// <summary>
        /// 科目停用日期
        /// </summary>
        [SugarColumn(ColumnName = "account_end_date", ColumnDescription = "科目停用日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? AccountEndDate { get; set; }

        /// <summary>
        /// 科目描述
        /// </summary>
        [SugarColumn(ColumnName = "account_description", ColumnDescription = "科目描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AccountDescription { get; set; }

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