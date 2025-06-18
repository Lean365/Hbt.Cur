using SqlSugar;
using System;

namespace Lean.Hbt.Domain.Entities.Finance.Controlling
{
    /// <summary>
    /// 费用签拟单表
    /// </summary>
    [SugarTable("hbt_controlling_expense_draft", "费用签拟单表")]
    [SugarIndex("ix_expense_draft_code", nameof(DocNo), OrderByType.Asc, true)]
    public class HbtExpenseDraft : HbtBaseEntity
    {
        /// <summary>集团</summary>
        [SugarColumn(ColumnName = "mandt", ColumnDescription = "集团", Length = 6, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Mandt { get; set; }
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "bukrs", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Bukrs { get; set; }
        /// <summary>工厂代码</summary>
        [SugarColumn(ColumnName = "werks", ColumnDescription = "工厂代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Werks { get; set; }

        /// <summary>单据编号</summary>
        [SugarColumn(ColumnName = "doc_no", ColumnDescription = "单据编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? DocNo { get; set; }

        /// <summary>申请人</summary>
        [SugarColumn(ColumnName = "applicant", ColumnDescription = "申请人", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Applicant { get; set; }
        /// <summary>申请部门</summary>
        [SugarColumn(ColumnName = "department", ColumnDescription = "申请部门", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Department { get; set; }
        /// <summary>申请日期</summary>
        [SugarColumn(ColumnName = "apply_date", ColumnDescription = "申请日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime ApplyDate { get; set; }
        /// <summary>预算类型</summary>
        [SugarColumn(ColumnName = "budget_type", ColumnDescription = "预算类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BudgetType { get; set; }
        /// <summary>预算编号</summary>
        [SugarColumn(ColumnName = "budget_no", ColumnDescription = "预算编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BudgetNo { get; set; }
        /// <summary>预算ID</summary>
        [SugarColumn(ColumnName = "budget_id", ColumnDescription = "预算ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? BudgetId { get; set; }
        /// <summary>预算金额</summary>
        [SugarColumn(ColumnName = "budget_amount", ColumnDescription = "预算金额", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? BudgetAmount { get; set; }        
        /// <summary>费用类型</summary>
        [SugarColumn(ColumnName = "expense_type", ColumnDescription = "费用类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? ExpenseType { get; set; }
        /// <summary>费用金额</summary>
        [SugarColumn(ColumnName = "expense_amount", ColumnDescription = "费用金额", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal ExpenseAmount { get; set; }
        /// <summary>币种</summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 6, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Currency { get; set; }
        /// <summary>用途说明</summary>
        [SugarColumn(ColumnName = "purpose", ColumnDescription = "用途说明", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Purpose { get; set; }
        /// <summary>状态</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }

    }
} 