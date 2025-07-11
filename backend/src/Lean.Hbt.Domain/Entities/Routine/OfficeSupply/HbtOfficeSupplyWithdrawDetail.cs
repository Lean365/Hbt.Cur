#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOfficeSupplyWithdrawDetail.cs
// 创建者 : Lean365
// 创建时间: 2024-12-19
// 版本号 : V1.0.0
// 描述    : 办公用品领用申请明细表实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Routine.OfficeSupply
{
    /// <summary>
    /// 办公用品领用申请明细表实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-12-19
    /// 说明: 记录办公用品领用申请的详细物品信息，包括物品、数量、单价等
    /// </remarks>
    [SugarTable("hbt_routine_office_supply_withdraw_detail", "办公用品领用申请明细")]
    [SugarIndex("ix_office_supply_withdraw_detail_withdraw", nameof(WithdrawId), OrderByType.Asc, false)]
    [SugarIndex("ix_office_supply_withdraw_detail_supply", nameof(SupplyId), OrderByType.Asc, false)]
    public class HbtOfficeSupplyWithdrawDetail : HbtBaseEntity
    {
        /// <summary>领用申请ID</summary>
        [SugarColumn(ColumnName = "withdraw_id", ColumnDescription = "领用申请ID", ColumnDataType = "bigint", IsNullable = false)]
        public long WithdrawId { get; set; }

        /// <summary>用品ID</summary>
        [SugarColumn(ColumnName = "supply_id", ColumnDescription = "用品ID", ColumnDataType = "bigint", IsNullable = false)]
        public long SupplyId { get; set; }

        /// <summary>用品编号</summary>
        [SugarColumn(ColumnName = "supply_code", ColumnDescription = "用品编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string SupplyCode { get; set; } = string.Empty;

        /// <summary>用品名称</summary>
        [SugarColumn(ColumnName = "supply_name", ColumnDescription = "用品名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string SupplyName { get; set; } = string.Empty;

        /// <summary>用品规格</summary>
        [SugarColumn(ColumnName = "specification", ColumnDescription = "用品规格", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Specification { get; set; }

        /// <summary>品牌</summary>
        [SugarColumn(ColumnName = "brand", ColumnDescription = "品牌", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Brand { get; set; }

        /// <summary>型号</summary>
        [SugarColumn(ColumnName = "model", ColumnDescription = "型号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Model { get; set; }

        /// <summary>单位</summary>
        [SugarColumn(ColumnName = "unit", ColumnDescription = "单位", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Unit { get; set; } = string.Empty;

        /// <summary>申请数量</summary>
        [SugarColumn(ColumnName = "request_quantity", ColumnDescription = "申请数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int RequestQuantity { get; set; } = 0;

        /// <summary>审批数量</summary>
        [SugarColumn(ColumnName = "approve_quantity", ColumnDescription = "审批数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ApproveQuantity { get; set; } = 0;

        /// <summary>发放数量</summary>
        [SugarColumn(ColumnName = "issue_quantity", ColumnDescription = "发放数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IssueQuantity { get; set; } = 0;

        /// <summary>单价</summary>
        [SugarColumn(ColumnName = "unit_price", ColumnDescription = "单价", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal UnitPrice { get; set; } = 0;

        /// <summary>总金额</summary>
        [SugarColumn(ColumnName = "total_amount", ColumnDescription = "总金额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal TotalAmount { get; set; } = 0;

        /// <summary>当前库存</summary>
        [SugarColumn(ColumnName = "current_stock", ColumnDescription = "当前库存", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int CurrentStock { get; set; } = 0;


    }
}