namespace Lean.Hbt.Domain.Entities.Finance.Budget
{
    /// <summary>
    /// 成本滚动预算表
    /// </summary>
    [SugarTable("hbt_budget_cost_rolling", "成本滚动预算表")]
    [SugarIndex("ix_cost_rolling_budget_year_month", nameof(BudgetYear), OrderByType.Asc, nameof(BudgetMonth), OrderByType.Asc, true)]
    public class HbtCostRollingBudget : HbtBaseEntity
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
        /// <summary>成本中心ID</summary>
        [SugarColumn(ColumnName = "cost_center_id", ColumnDescription = "成本中心ID", ColumnDataType = "bigint", IsNullable = false)]
        public long CostCenterId { get; set; }
        /// <summary>成本要素ID</summary>
        [SugarColumn(ColumnName = "cost_element_id", ColumnDescription = "成本要素ID", ColumnDataType = "bigint", IsNullable = false)]
        public long CostElementId { get; set; }
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