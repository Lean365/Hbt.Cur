#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtBudgetAccountMapping.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述    : 预算科目映射实体类 (基于SAP FI预算科目映射管理)
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Accounting.Budget
{
    /// <summary>
    /// 预算科目映射实体类 (基于SAP FI预算科目映射管理)
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 参考: SAP FI-FI 预算科目映射管理
    /// </remarks>
    [SugarTable("hbt_accounting_budget_budget_account_mapping", "预算科目映射表")]
    [SugarIndex("ix_budget_mapping_code", nameof(CompanyCode), OrderByType.Asc, nameof(PlantCode), OrderByType.Asc, nameof(BudgetAccountCode), OrderByType.Asc, nameof(MappingType), OrderByType.Asc, true)]
    [SugarIndex("ix_company_plant", nameof(CompanyCode), OrderByType.Asc, nameof(PlantCode), OrderByType.Asc, false)]
    public class HbtBudgetAccountMapping : HbtBaseEntity
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
        /// 预算科目编码
        /// </summary>
        [SugarColumn(ColumnName = "budget_account_code", ColumnDescription = "预算科目编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string BudgetAccountCode { get; set; } = string.Empty;

        /// <summary>
        /// 预算科目关联
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(BudgetAccountCode))]
        public HbtBudgetAccount? BudgetAccount { get; set; }

        /// <summary>
        /// 映射类型(1=成本中心映射 2=成本要素映射 3=费用草稿映射 4=资金映射 5=模具映射 6=销售映射 7=人员映射)
        /// </summary>
        [SugarColumn(ColumnName = "mapping_type", ColumnDescription = "映射类型", ColumnDataType = "int", IsNullable = false)]
        public int MappingType { get; set; }

        /// <summary>
        /// 映射对象编码
        /// </summary>
        [SugarColumn(ColumnName = "mapping_object_code", ColumnDescription = "映射对象编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MappingObjectCode { get; set; } = string.Empty;

        /// <summary>
        /// 映射对象名称
        /// </summary>
        [SugarColumn(ColumnName = "mapping_object_name", ColumnDescription = "映射对象名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MappingObjectName { get; set; } = string.Empty;

        /// <summary>
        /// 映射对象类型(1=成本中心 2=成本要素 3=费用项目 4=银行账户 5=模具 6=产品 7=部门 8=岗位)
        /// </summary>
        [SugarColumn(ColumnName = "mapping_object_type", ColumnDescription = "映射对象类型", ColumnDataType = "int", IsNullable = false)]
        public int MappingObjectType { get; set; }

        /// <summary>
        /// 映射比例(%)
        /// </summary>
        [SugarColumn(ColumnName = "mapping_ratio", ColumnDescription = "映射比例", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "100")]
        public decimal MappingRatio { get; set; } = 100;

        /// <summary>
        /// 映射优先级(1=最高 2=高 3=中 4=低 5=最低)
        /// </summary>
        [SugarColumn(ColumnName = "mapping_priority", ColumnDescription = "映射优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "3")]
        public int MappingPriority { get; set; } = 3;

        /// <summary>
        /// 是否默认映射
        /// </summary>
        [SugarColumn(ColumnName = "is_default_mapping", ColumnDescription = "是否默认映射", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsDefaultMapping { get; set; } = 0;

        /// <summary>
        /// 是否启用映射
        /// </summary>
        [SugarColumn(ColumnName = "enable_mapping", ColumnDescription = "是否启用映射", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int EnableMapping { get; set; } = 1;

        /// <summary>
        /// 映射生效日期
        /// </summary>
        [SugarColumn(ColumnName = "mapping_start_date", ColumnDescription = "映射生效日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime MappingStartDate { get; set; }

        /// <summary>
        /// 映射失效日期
        /// </summary>
        [SugarColumn(ColumnName = "mapping_end_date", ColumnDescription = "映射失效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? MappingEndDate { get; set; }

        /// <summary>
        /// 映射描述
        /// </summary>
        [SugarColumn(ColumnName = "mapping_description", ColumnDescription = "映射描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MappingDescription { get; set; }

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