#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtFiscalYearVariant.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述    : 财政年度变式实体类 (基于SAP FI财政年度变式)
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Accounting.Budget
{
    /// <summary>
    /// 财政年度变式实体类 (基于SAP FI财政年度变式)
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 参考: SAP FI-FI 财政年度变式
    /// </remarks>
    [SugarTable("hbt_accounting_budget_fiscal_year_variant", "年度变式")]
    [SugarIndex("ix_fiscal_year_variant_code", nameof(FiscalYearVariantCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_plant", nameof(CompanyCode), OrderByType.Asc, nameof(PlantCode), OrderByType.Asc, false)]
    public class HbtFiscalYearVariant : HbtBaseEntity
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
        /// 财政年度变式编码
        /// </summary>
        [SugarColumn(ColumnName = "fiscal_year_variant_code", ColumnDescription = "财政年度变式编码", Length = 4, ColumnDataType = "nvarchar", IsNullable = false)]
        public string FiscalYearVariantCode { get; set; } = string.Empty;

        /// <summary>
        /// 财政年度变式名称
        /// </summary>
        [SugarColumn(ColumnName = "fiscal_year_variant_name", ColumnDescription = "财政年度变式名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string FiscalYearVariantName { get; set; } = string.Empty;

        /// <summary>
        /// 财政年度变式简称
        /// </summary>
        [SugarColumn(ColumnName = "fiscal_year_variant_short_name", ColumnDescription = "财政年度变式简称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FiscalYearVariantShortName { get; set; }

        /// <summary>
        /// 财政年度变式类型(1=自然年度 2=财政年度 3=自定义年度)
        /// </summary>
        [SugarColumn(ColumnName = "fiscal_year_variant_type", ColumnDescription = "财政年度变式类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int FiscalYearVariantType { get; set; } = 1;

        /// <summary>
        /// 年度开始月份
        /// </summary>
        [SugarColumn(ColumnName = "year_start_month", ColumnDescription = "年度开始月份", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int YearStartMonth { get; set; } = 1;

        /// <summary>
        /// 年度开始日期
        /// </summary>
        [SugarColumn(ColumnName = "year_start_day", ColumnDescription = "年度开始日期", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int YearStartDay { get; set; } = 1;

        /// <summary>
        /// 期间数量
        /// </summary>
        [SugarColumn(ColumnName = "period_count", ColumnDescription = "期间数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "12")]
        public int PeriodCount { get; set; } = 12;

        /// <summary>
        /// 是否包含调整期间
        /// </summary>
        [SugarColumn(ColumnName = "include_adjustment_period", ColumnDescription = "是否包含调整期间", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IncludeAdjustmentPeriod { get; set; } = 0;

        /// <summary>
        /// 调整期间数量
        /// </summary>
        [SugarColumn(ColumnName = "adjustment_period_count", ColumnDescription = "调整期间数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int AdjustmentPeriodCount { get; set; } = 0;

        /// <summary>
        /// 是否允许跨年度过账
        /// </summary>
        [SugarColumn(ColumnName = "allow_cross_year_posting", ColumnDescription = "是否允许跨年度过账", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int AllowCrossYearPosting { get; set; } = 1;

        /// <summary>
        /// 是否允许年度结转
        /// </summary>
        [SugarColumn(ColumnName = "allow_year_carry_forward", ColumnDescription = "是否允许年度结转", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int AllowYearCarryForward { get; set; } = 1;

        /// <summary>
        /// 财政年度变式描述
        /// </summary>
        [SugarColumn(ColumnName = "fiscal_year_variant_description", ColumnDescription = "财政年度变式描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FiscalYearVariantDescription { get; set; }

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