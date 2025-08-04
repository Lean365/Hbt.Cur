#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtCostCenter.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 成本中心实体类 (基于SAP CO成本中心管理)
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Hbt.Domain.Entities.Accounting.Controlling
{
    /// <summary>
    /// 成本中心实体类 (基于SAP CO成本中心管理)
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 参考: SAP CO-CCA 成本中心会计
    /// </remarks>
    [SugarTable("hbt_accounting_controlling_cost_center", "成本中心表")]
    [SugarIndex("ix_cost_center_code", nameof(CostCenterCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_plant", nameof(CompanyCode), OrderByType.Asc, nameof(PlantCode), OrderByType.Asc, false)]
    public class HbtCostCenter : HbtBaseEntity
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
        /// 成本中心编码
        /// </summary>
        [SugarColumn(ColumnName = "cost_center_code", ColumnDescription = "成本中心编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CostCenterCode { get; set; } = string.Empty;

        /// <summary>
        /// 成本中心名称
        /// </summary>
        [SugarColumn(ColumnName = "cost_center_name", ColumnDescription = "成本中心名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CostCenterName { get; set; } = string.Empty;

        /// <summary>
        /// 成本中心简称
        /// </summary>
        [SugarColumn(ColumnName = "cost_center_short_name", ColumnDescription = "成本中心简称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostCenterShortName { get; set; }

        /// <summary>
        /// 成本中心类型
        /// </summary>
        [SugarColumn(ColumnName = "cost_center_type", ColumnDescription = "成本中心类型", ColumnDataType = "int", IsNullable = false)]
        public int CostCenterType { get; set; }

        /// <summary>
        /// 成本中心类别
        /// </summary>
        [SugarColumn(ColumnName = "cost_center_category", ColumnDescription = "成本中心类别", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostCenterCategory { get; set; }

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
        /// 负责人编码
        /// </summary>
        [SugarColumn(ColumnName = "manager_code", ColumnDescription = "负责人编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ManagerCode { get; set; }

        /// <summary>
        /// 负责人姓名
        /// </summary>
        [SugarColumn(ColumnName = "manager_name", ColumnDescription = "负责人姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ManagerName { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [SugarColumn(ColumnName = "dept_id", ColumnDescription = "部门ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? DeptId { get; set; }

        /// <summary>
        /// 部门编码
        /// </summary>
        [SugarColumn(ColumnName = "dept_code", ColumnDescription = "部门编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeptCode { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [SugarColumn(ColumnName = "dept_name", ColumnDescription = "部门名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeptName { get; set; }

        /// <summary>
        /// 成本中心描述
        /// </summary>
        [SugarColumn(ColumnName = "cost_center_description", ColumnDescription = "成本中心描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostCenterDescription { get; set; }

        /// <summary>
        /// 成本中心地址
        /// </summary>
        [SugarColumn(ColumnName = "cost_center_address", ColumnDescription = "成本中心地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostCenterAddress { get; set; }

        /// <summary>
        /// 成本中心电话
        /// </summary>
        [SugarColumn(ColumnName = "cost_center_phone", ColumnDescription = "成本中心电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostCenterPhone { get; set; }

        /// <summary>
        /// 成本中心邮箱
        /// </summary>
        [SugarColumn(ColumnName = "cost_center_email", ColumnDescription = "成本中心邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostCenterEmail { get; set; }

        /// <summary>
        /// 成本中心启用日期
        /// </summary>
        [SugarColumn(ColumnName = "cost_center_start_date", ColumnDescription = "成本中心启用日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? CostCenterStartDate { get; set; }

        /// <summary>
        /// 成本中心停用日期
        /// </summary>
        [SugarColumn(ColumnName = "cost_center_end_date", ColumnDescription = "成本中心停用日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? CostCenterEndDate { get; set; }

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