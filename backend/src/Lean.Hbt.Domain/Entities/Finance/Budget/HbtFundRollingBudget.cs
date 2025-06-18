namespace Lean.Hbt.Domain.Entities.Finance.Budget
{
    /// <summary>
    /// 经费滚动预算表
    /// </summary>
    [SugarTable("hbt_budget_fund_rolling", "经费滚动预算表")]
    [SugarIndex("ix_fund_rolling_budget_year_month", nameof(BudgetYear), OrderByType.Asc, nameof(BudgetMonth), OrderByType.Asc, true)]
    public class HbtFundRollingBudget : HbtBaseEntity
    {
        /// <summary>预算年度</summary>
        [SugarColumn(ColumnName = "budget_year", ColumnDescription = "预算年度", ColumnDataType = "int", IsNullable = false)]
        public int BudgetYear { get; set; }
        /// <summary>预算月份</summary>
        [SugarColumn(ColumnName = "budget_month", ColumnDescription = "预算月份", ColumnDataType = "int", IsNullable = false)]
        public int BudgetMonth { get; set; }
        /// <summary>滚动周期</summary>
        [SugarColumn(ColumnName = "rolling_period", ColumnDescription = "滚动周期", ColumnDataType = "int", IsNullable = false)]
        public int RollingPeriod { get; set; }
        /// <summary>部门ID</summary>
        [SugarColumn(ColumnName = "department_id", ColumnDescription = "部门ID", ColumnDataType = "bigint", IsNullable = false)]
        public long DepartmentId { get; set; }
        /// <summary>经费项目ID</summary>
        [SugarColumn(ColumnName = "fund_item_id", ColumnDescription = "经费项目ID", ColumnDataType = "bigint", IsNullable = false)]
        public long FundItemId { get; set; }
        /// <summary>预算金额</summary>
        [SugarColumn(ColumnName = "budget_amount", ColumnDescription = "预算金额", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal BudgetAmount { get; set; }
        /// <summary>实际金额</summary>
        [SugarColumn(ColumnName = "actual_amount", ColumnDescription = "实际金额", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal ActualAmount { get; set; }
        /// <summary>差异金额</summary>
        [SugarColumn(ColumnName = "diff_amount", ColumnDescription = "差异金额", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal DiffAmount { get; set; }
        /// <summary>差异率</summary>
        [SugarColumn(ColumnName = "diff_rate", ColumnDescription = "差异率", ColumnDataType = "decimal(18,4)", IsNullable = false)]
        public decimal DiffRate { get; set; }
        /// <summary>预算状态(0=草稿 1=已提交 2=已审核 3=已批准)</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "预算状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }

    }
}