#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtCostCenter.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述    : 成本中心实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Finance.Controlling
{
    /// <summary>
    /// 成本中心实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("hbt_controlling_cost_center", "成本中心表")]
    [SugarIndex("ix_cost_center_code", nameof(CostCenterCode), OrderByType.Asc, true)]
    public class HbtCostCenter : HbtBaseEntity
    {


        /// <summary>
        /// 成本中心编码
        /// </summary>
        [SugarColumn(ColumnName = "cost_center_code", ColumnDescription = "成本中心编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? CostCenterCode { get; set; }

        /// <summary>
        /// 成本中心名称
        /// </summary>
        [SugarColumn(ColumnName = "cost_center_name", ColumnDescription = "成本中心名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? CostCenterName { get; set; }

        /// <summary>
        /// 成本中心类型
        /// </summary>
        [SugarColumn(ColumnName = "cost_center_type", ColumnDescription = "成本中心类型", ColumnDataType = "int", IsNullable = false)]
        public int CostCenterType { get; set; }

        /// <summary>
        /// 上级成本中心ID
        /// </summary>
        [SugarColumn(ColumnName = "parent_id", ColumnDescription = "上级成本中心ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ParentId { get; set; }

        /// <summary>
        /// 上级成本中心
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ParentId))]
        public HbtCostCenter? Parent { get; set; }

        /// <summary>
        /// 负责人ID
        /// </summary>
        [SugarColumn(ColumnName = "manager_id", ColumnDescription = "负责人ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ManagerId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [SugarColumn(ColumnName = "dept_id", ColumnDescription = "部门ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? DeptId { get; set; }

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