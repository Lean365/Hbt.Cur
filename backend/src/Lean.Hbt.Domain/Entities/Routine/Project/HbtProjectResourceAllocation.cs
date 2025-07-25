#nullable enable

using SqlSugar;
using Lean.Hbt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtProjectResourceAllocation.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 项目资源分配实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Routine.Project
{
    /// <summary>
    /// 项目资源分配实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 说明: 记录项目资源分配的相关信息，包括资源分配信息、工作量、成本等
    /// </remarks>
    [SugarTable("hbt_routine_project_resource_allocation", "项目资源分配表")]
    [SugarIndex("ix_project_resource_allocation_code", nameof(ProjectResourceAllocationCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_project_resource_allocation", nameof(CompanyCode), OrderByType.Asc, nameof(ProjectResourceAllocationCode), OrderByType.Asc, false)]
    [SugarIndex("ix_project_resource_allocation_status", nameof(ProjectResourceAllocationStatus), OrderByType.Asc, false)]
    [SugarIndex("ix_project_resource_allocation_type", nameof(ProjectResourceAllocationType), OrderByType.Asc, false)]
    public class HbtProjectResourceAllocation : HbtBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>项目资源分配编号</summary>
        [SugarColumn(ColumnName = "project_resource_allocation_code", ColumnDescription = "项目资源分配编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProjectResourceAllocationCode { get; set; } = string.Empty;

        /// <summary>关联项目编号</summary>
        [SugarColumn(ColumnName = "related_project_code", ColumnDescription = "关联项目编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RelatedProjectCode { get; set; } = string.Empty;

        /// <summary>关联项目名称</summary>
        [SugarColumn(ColumnName = "related_project_name", ColumnDescription = "关联项目名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RelatedProjectName { get; set; } = string.Empty;

        /// <summary>关联任务编号</summary>
        [SugarColumn(ColumnName = "related_task_code", ColumnDescription = "关联任务编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedTaskCode { get; set; }

        /// <summary>关联任务名称</summary>
        [SugarColumn(ColumnName = "related_task_name", ColumnDescription = "关联任务名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedTaskName { get; set; }

        /// <summary>资源类型(1=人力资源 2=设备资源 3=材料资源 4=资金资源 5=时间资源 6=其他资源)</summary>
        [SugarColumn(ColumnName = "project_resource_allocation_type", ColumnDescription = "资源类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ProjectResourceAllocationType { get; set; } = 1;

        /// <summary>资源编号</summary>
        [SugarColumn(ColumnName = "resource_code", ColumnDescription = "资源编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ResourceCode { get; set; } = string.Empty;

        /// <summary>资源名称</summary>
        [SugarColumn(ColumnName = "resource_name", ColumnDescription = "资源名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ResourceName { get; set; } = string.Empty;

        /// <summary>资源描述</summary>
        [SugarColumn(ColumnName = "resource_description", ColumnDescription = "资源描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ResourceDescription { get; set; }

        /// <summary>资源规格</summary>
        [SugarColumn(ColumnName = "resource_specification", ColumnDescription = "资源规格", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ResourceSpecification { get; set; }

        /// <summary>资源单位</summary>
        [SugarColumn(ColumnName = "resource_unit", ColumnDescription = "资源单位", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ResourceUnit { get; set; }

        /// <summary>计划数量</summary>
        [SugarColumn(ColumnName = "planned_quantity", ColumnDescription = "计划数量", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? PlannedQuantity { get; set; }

        /// <summary>实际数量</summary>
        [SugarColumn(ColumnName = "actual_quantity", ColumnDescription = "实际数量", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? ActualQuantity { get; set; }

        /// <summary>计划工时(小时)</summary>
        [SugarColumn(ColumnName = "planned_hours", ColumnDescription = "计划工时(小时)", ColumnDataType = "decimal(10,2)", IsNullable = true)]
        public decimal? PlannedHours { get; set; }

        /// <summary>实际工时(小时)</summary>
        [SugarColumn(ColumnName = "actual_hours", ColumnDescription = "实际工时(小时)", ColumnDataType = "decimal(10,2)", IsNullable = true)]
        public decimal? ActualHours { get; set; }

        /// <summary>计划开始日期</summary>
        [SugarColumn(ColumnName = "planned_start_date", ColumnDescription = "计划开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PlannedStartDate { get; set; }

        /// <summary>计划结束日期</summary>
        [SugarColumn(ColumnName = "planned_end_date", ColumnDescription = "计划结束日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PlannedEndDate { get; set; }

        /// <summary>实际开始日期</summary>
        [SugarColumn(ColumnName = "actual_start_date", ColumnDescription = "实际开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualStartDate { get; set; }

        /// <summary>实际结束日期</summary>
        [SugarColumn(ColumnName = "actual_end_date", ColumnDescription = "实际结束日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualEndDate { get; set; }

        /// <summary>计划成本</summary>
        [SugarColumn(ColumnName = "planned_cost", ColumnDescription = "计划成本", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? PlannedCost { get; set; }

        /// <summary>实际成本</summary>
        [SugarColumn(ColumnName = "actual_cost", ColumnDescription = "实际成本", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? ActualCost { get; set; }

        /// <summary>成本差异</summary>
        [SugarColumn(ColumnName = "cost_variance", ColumnDescription = "成本差异", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? CostVariance { get; set; }

        /// <summary>币种(CNY=人民币 USD=美元 EUR=欧元)</summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>资源单价</summary>
        [SugarColumn(ColumnName = "resource_unit_price", ColumnDescription = "资源单价", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? ResourceUnitPrice { get; set; }

        /// <summary>资源费率(每小时)</summary>
        [SugarColumn(ColumnName = "resource_hourly_rate", ColumnDescription = "资源费率(每小时)", ColumnDataType = "decimal(10,2)", IsNullable = true)]
        public decimal? ResourceHourlyRate { get; set; }

        /// <summary>资源可用性(%)</summary>
        [SugarColumn(ColumnName = "resource_availability", ColumnDescription = "资源可用性(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "100")]
        public decimal ResourceAvailability { get; set; } = 100;

        /// <summary>资源利用率(%)</summary>
        [SugarColumn(ColumnName = "resource_utilization", ColumnDescription = "资源利用率(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ResourceUtilization { get; set; } = 0;

        /// <summary>资源分配比例(%)</summary>
        [SugarColumn(ColumnName = "resource_allocation_ratio", ColumnDescription = "资源分配比例(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "100")]
        public decimal ResourceAllocationRatio { get; set; } = 100;

        /// <summary>资源分配人</summary>
        [SugarColumn(ColumnName = "resource_allocator", ColumnDescription = "资源分配人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ResourceAllocator { get; set; }

        /// <summary>资源分配日期</summary>
        [SugarColumn(ColumnName = "resource_allocation_date", ColumnDescription = "资源分配日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ResourceAllocationDate { get; set; }

        /// <summary>资源使用人</summary>
        [SugarColumn(ColumnName = "resource_user", ColumnDescription = "资源使用人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ResourceUser { get; set; }

        /// <summary>资源使用人电话</summary>
        [SugarColumn(ColumnName = "resource_user_phone", ColumnDescription = "资源使用人电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ResourceUserPhone { get; set; }

        /// <summary>资源使用人邮箱</summary>
        [SugarColumn(ColumnName = "resource_user_email", ColumnDescription = "资源使用人邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ResourceUserEmail { get; set; }

        /// <summary>资源部门</summary>
        [SugarColumn(ColumnName = "resource_department", ColumnDescription = "资源部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ResourceDepartment { get; set; }

        /// <summary>资源技能</summary>
        [SugarColumn(ColumnName = "resource_skills", ColumnDescription = "资源技能", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ResourceSkills { get; set; }

        /// <summary>资源经验(年)</summary>
        [SugarColumn(ColumnName = "resource_experience", ColumnDescription = "资源经验(年)", ColumnDataType = "int", IsNullable = true)]
        public int? ResourceExperience { get; set; }

        /// <summary>资源等级(1=初级 2=中级 3=高级 4=专家)</summary>
        [SugarColumn(ColumnName = "resource_level", ColumnDescription = "资源等级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int ResourceLevel { get; set; } = 2;

        /// <summary>资源状态(0=可用 1=使用中 2=维护中 3=故障 4=报废)</summary>
        [SugarColumn(ColumnName = "resource_status", ColumnDescription = "资源状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ResourceStatus { get; set; } = 0;

        /// <summary>项目资源分配状态(0=计划中 1=已分配 2=使用中 3=已完成 4=已取消)</summary>
        [SugarColumn(ColumnName = "project_resource_allocation_status", ColumnDescription = "项目资源分配状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ProjectResourceAllocationStatus { get; set; } = 0;

        /// <summary>资源备注</summary>
        [SugarColumn(ColumnName = "resource_remark", ColumnDescription = "资源备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ResourceRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 