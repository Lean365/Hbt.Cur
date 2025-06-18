using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Finance.Budget
{
    /// <summary>
    /// 固定资产预算表
    /// </summary>
    [SugarTable("hbt_budget_fixed_asset", "固定资产预算表")]
    [SugarIndex("ix_fixed_asset_budget_year_month", nameof(BudgetYear), OrderByType.Asc, nameof(BudgetMonth), OrderByType.Asc, true)]
    public class HbtFixedAssetBudget : HbtBaseEntity
    {
        /// <summary>预算年度</summary>
        [SugarColumn(ColumnName = "budget_year", ColumnDescription = "预算年度", ColumnDataType = "int", IsNullable = false)]
        public int BudgetYear { get; set; }
        /// <summary>预算月份</summary>
        [SugarColumn(ColumnName = "budget_month", ColumnDescription = "预算月份", ColumnDataType = "int", IsNullable = false)]
        public int BudgetMonth { get; set; }
        /// <summary>资产类别ID</summary>
        [SugarColumn(ColumnName = "asset_category_id", ColumnDescription = "资产类别ID", ColumnDataType = "bigint", IsNullable = false)]
        public long AssetCategoryId { get; set; }
        /// <summary>资产项目ID</summary>
        [SugarColumn(ColumnName = "asset_item_id", ColumnDescription = "资产项目ID", ColumnDataType = "bigint", IsNullable = false)]
        public long AssetItemId { get; set; }
        /// <summary>预算金额</summary>
        [SugarColumn(ColumnName = "budget_amount", ColumnDescription = "预算金额", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal BudgetAmount { get; set; }
        /// <summary>预算状态(0=草稿 1=已提交 2=已审核 3=已批准)</summary>
        [SugarColumn(ColumnName = "budget_status", ColumnDescription = "预算状态", ColumnDataType = "int", IsNullable = false)]
        public int BudgetStatus { get; set; }
    }
} 