using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Finance.Budget
{
    /// <summary>
    /// 模具预算表
    /// </summary>
    [SugarTable("hbt_budget_mould", "模具预算表")]
    [SugarIndex("ix_mould_budget_year_month", nameof(BudgetYear), OrderByType.Asc, nameof(BudgetMonth), OrderByType.Asc, true)]
    public class HbtMouldBudget : HbtBaseEntity
    {
        /// <summary>预算年度</summary>
        [SugarColumn(ColumnName = "budget_year", ColumnDescription = "预算年度", ColumnDataType = "int", IsNullable = false)]
        public int BudgetYear { get; set; }
        /// <summary>预算月份</summary>
        [SugarColumn(ColumnName = "budget_month", ColumnDescription = "预算月份", ColumnDataType = "int", IsNullable = false)]
        public int BudgetMonth { get; set; }
        /// <summary>模具项目ID</summary>
        [SugarColumn(ColumnName = "mould_item_id", ColumnDescription = "模具项目ID", ColumnDataType = "bigint", IsNullable = false)]
        public long MouldItemId { get; set; }
        /// <summary>预算金额</summary>
        [SugarColumn(ColumnName = "budget_amount", ColumnDescription = "预算金额", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal BudgetAmount { get; set; }
        /// <summary>预算状态(0=草稿 1=已提交 2=已审核 3=已批准)</summary>
        [SugarColumn(ColumnName = "budget_status", ColumnDescription = "预算状态", ColumnDataType = "int", IsNullable = false)]
        public int BudgetStatus { get; set; }
    }
} 