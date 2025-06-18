using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Finance.Budget
{
    /// <summary>
    /// 人员预算表
    /// </summary>
    [SugarTable("hbt_budget_staff", "人员预算表")]
    [SugarIndex("ix_staff_budget_year_month", nameof(BudgetYear), OrderByType.Asc, nameof(BudgetMonth), OrderByType.Asc, true)]
    public class HbtStaffBudget : HbtBaseEntity
    {
        /// <summary>预算年度</summary>
        [SugarColumn(ColumnName = "budget_year", ColumnDescription = "预算年度", ColumnDataType = "int", IsNullable = false)]
        public int BudgetYear { get; set; }
        /// <summary>预算月份</summary>
        [SugarColumn(ColumnName = "budget_month", ColumnDescription = "预算月份", ColumnDataType = "int", IsNullable = false)]
        public int BudgetMonth { get; set; }
        /// <summary>部门ID</summary>
        [SugarColumn(ColumnName = "department_id", ColumnDescription = "部门ID", ColumnDataType = "bigint", IsNullable = false)]
        public long DepartmentId { get; set; }
        /// <summary>岗位ID</summary>
        [SugarColumn(ColumnName = "position_id", ColumnDescription = "岗位ID", ColumnDataType = "bigint", IsNullable = false)]
        public long PositionId { get; set; }
        /// <summary>预算人数</summary>
        [SugarColumn(ColumnName = "staff_count", ColumnDescription = "预算人数", ColumnDataType = "int", IsNullable = false)]
        public int StaffCount { get; set; }
        /// <summary>预算工资总额</summary>
        [SugarColumn(ColumnName = "salary_total", ColumnDescription = "预算工资总额", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal SalaryTotal { get; set; }
        /// <summary>预算奖金</summary>
        [SugarColumn(ColumnName = "bonus", ColumnDescription = "预算奖金", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal Bonus { get; set; }
        /// <summary>预算社保</summary>
        [SugarColumn(ColumnName = "social_security", ColumnDescription = "预算社保", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal SocialSecurity { get; set; }
        /// <summary>预算福利</summary>
        [SugarColumn(ColumnName = "welfare", ColumnDescription = "预算福利", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal Welfare { get; set; }
        /// <summary>预算状态(0=草稿 1=已提交 2=已审核 3=已批准)</summary>
        [SugarColumn(ColumnName = "budget_status", ColumnDescription = "预算状态", ColumnDataType = "int", IsNullable = false)]
        public int BudgetStatus { get; set; }
    }
} 