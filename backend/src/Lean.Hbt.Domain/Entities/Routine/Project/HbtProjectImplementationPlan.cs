#nullable enable

using SqlSugar;
using Lean.Hbt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtProjectImplementationPlan.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 项目实施计划实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Routine.Project
{
    /// <summary>
    /// 项目实施计划实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 说明: 记录项目实施计划的相关信息，包括计划信息、阶段、里程碑等
    /// </remarks>
    [SugarTable("hbt_routine_project_implementation_plan", "项目实施计划表")]
    [SugarIndex("ix_project_implementation_plan_code", nameof(ProjectImplementationPlanCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_project_implementation_plan", nameof(CompanyCode), OrderByType.Asc, nameof(ProjectImplementationPlanCode), OrderByType.Asc, false)]
    [SugarIndex("ix_project_implementation_plan_status", nameof(ProjectImplementationPlanStatus), OrderByType.Asc, false)]
    [SugarIndex("ix_project_implementation_plan_type", nameof(ProjectImplementationPlanType), OrderByType.Asc, false)]
    public class HbtProjectImplementationPlan : HbtBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>项目实施计划编号</summary>
        [SugarColumn(ColumnName = "project_implementation_plan_code", ColumnDescription = "项目实施计划编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProjectImplementationPlanCode { get; set; } = string.Empty;

        /// <summary>实施计划名称</summary>
        [SugarColumn(ColumnName = "implementation_plan_name", ColumnDescription = "实施计划名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ImplementationPlanName { get; set; } = string.Empty;

        /// <summary>实施计划类型(1=总体实施计划 2=阶段实施计划 3=详细实施计划 4=里程碑计划 5=其他计划)</summary>
        [SugarColumn(ColumnName = "project_implementation_plan_type", ColumnDescription = "实施计划类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ProjectImplementationPlanType { get; set; } = 1;

        /// <summary>关联项目编号</summary>
        [SugarColumn(ColumnName = "related_project_code", ColumnDescription = "关联项目编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RelatedProjectCode { get; set; } = string.Empty;

        /// <summary>关联项目名称</summary>
        [SugarColumn(ColumnName = "related_project_name", ColumnDescription = "关联项目名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RelatedProjectName { get; set; } = string.Empty;

        /// <summary>父计划编号</summary>
        [SugarColumn(ColumnName = "parent_plan_code", ColumnDescription = "父计划编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ParentPlanCode { get; set; }

        /// <summary>父计划名称</summary>
        [SugarColumn(ColumnName = "parent_plan_name", ColumnDescription = "父计划名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ParentPlanName { get; set; }

        /// <summary>计划层级</summary>
        [SugarColumn(ColumnName = "plan_level", ColumnDescription = "计划层级", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int PlanLevel { get; set; } = 1;

        /// <summary>计划顺序</summary>
        [SugarColumn(ColumnName = "plan_sequence", ColumnDescription = "计划顺序", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int PlanSequence { get; set; } = 1;

        /// <summary>实施计划描述</summary>
        [SugarColumn(ColumnName = "implementation_plan_description", ColumnDescription = "实施计划描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ImplementationPlanDescription { get; set; }

        /// <summary>实施目标</summary>
        [SugarColumn(ColumnName = "implementation_objectives", ColumnDescription = "实施目标", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ImplementationObjectives { get; set; }

        /// <summary>实施范围</summary>
        [SugarColumn(ColumnName = "implementation_scope", ColumnDescription = "实施范围", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ImplementationScope { get; set; }

        /// <summary>实施策略</summary>
        [SugarColumn(ColumnName = "implementation_strategy", ColumnDescription = "实施策略", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ImplementationStrategy { get; set; }

        /// <summary>实施方法</summary>
        [SugarColumn(ColumnName = "implementation_method", ColumnDescription = "实施方法", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ImplementationMethod { get; set; }

        /// <summary>实施工具</summary>
        [SugarColumn(ColumnName = "implementation_tools", ColumnDescription = "实施工具", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ImplementationTools { get; set; }

        /// <summary>计划开始日期</summary>
        [SugarColumn(ColumnName = "planned_start_date", ColumnDescription = "计划开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PlannedStartDate { get; set; }

        /// <summary>计划结束日期</summary>
        [SugarColumn(ColumnName = "planned_end_date", ColumnDescription = "计划结束日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PlannedEndDate { get; set; }

        /// <summary>计划工期(天)</summary>
        [SugarColumn(ColumnName = "planned_duration", ColumnDescription = "计划工期(天)", ColumnDataType = "int", IsNullable = true)]
        public int? PlannedDuration { get; set; }

        /// <summary>实际开始日期</summary>
        [SugarColumn(ColumnName = "actual_start_date", ColumnDescription = "实际开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualStartDate { get; set; }

        /// <summary>实际结束日期</summary>
        [SugarColumn(ColumnName = "actual_end_date", ColumnDescription = "实际结束日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualEndDate { get; set; }

        /// <summary>实际工期(天)</summary>
        [SugarColumn(ColumnName = "actual_duration", ColumnDescription = "实际工期(天)", ColumnDataType = "int", IsNullable = true)]
        public int? ActualDuration { get; set; }

        /// <summary>工期偏差(天)</summary>
        [SugarColumn(ColumnName = "duration_variance", ColumnDescription = "工期偏差(天)", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DurationVariance { get; set; } = 0;

        /// <summary>计划进度(%)</summary>
        [SugarColumn(ColumnName = "planned_progress", ColumnDescription = "计划进度(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal PlannedProgress { get; set; } = 0;

        /// <summary>实际进度(%)</summary>
        [SugarColumn(ColumnName = "actual_progress", ColumnDescription = "实际进度(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ActualProgress { get; set; } = 0;

        /// <summary>进度偏差(%)</summary>
        [SugarColumn(ColumnName = "progress_variance", ColumnDescription = "进度偏差(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ProgressVariance { get; set; } = 0;

        /// <summary>计划工作量(小时)</summary>
        [SugarColumn(ColumnName = "planned_work_hours", ColumnDescription = "计划工作量(小时)", ColumnDataType = "decimal(10,2)", IsNullable = true)]
        public decimal? PlannedWorkHours { get; set; }

        /// <summary>实际工作量(小时)</summary>
        [SugarColumn(ColumnName = "actual_work_hours", ColumnDescription = "实际工作量(小时)", ColumnDataType = "decimal(10,2)", IsNullable = true)]
        public decimal? ActualWorkHours { get; set; }

        /// <summary>工作量偏差(小时)</summary>
        [SugarColumn(ColumnName = "work_hours_variance", ColumnDescription = "工作量偏差(小时)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal WorkHoursVariance { get; set; } = 0;

        /// <summary>计划成本</summary>
        [SugarColumn(ColumnName = "planned_cost", ColumnDescription = "计划成本", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? PlannedCost { get; set; }

        /// <summary>实际成本</summary>
        [SugarColumn(ColumnName = "actual_cost", ColumnDescription = "实际成本", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? ActualCost { get; set; }

        /// <summary>成本偏差</summary>
        [SugarColumn(ColumnName = "cost_variance", ColumnDescription = "成本偏差", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal CostVariance { get; set; } = 0;

        /// <summary>币种(CNY=人民币 USD=美元 EUR=欧元)</summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>实施阶段</summary>
        [SugarColumn(ColumnName = "implementation_phase", ColumnDescription = "实施阶段", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ImplementationPhase { get; set; }

        /// <summary>阶段顺序</summary>
        [SugarColumn(ColumnName = "phase_sequence", ColumnDescription = "阶段顺序", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int PhaseSequence { get; set; } = 1;

        /// <summary>是否里程碑(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_milestone", ColumnDescription = "是否里程碑", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsMilestone { get; set; } = 0;

        /// <summary>里程碑名称</summary>
        [SugarColumn(ColumnName = "milestone_name", ColumnDescription = "里程碑名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MilestoneName { get; set; }

        /// <summary>里程碑描述</summary>
        [SugarColumn(ColumnName = "milestone_description", ColumnDescription = "里程碑描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MilestoneDescription { get; set; }

        /// <summary>里程碑标准</summary>
        [SugarColumn(ColumnName = "milestone_criteria", ColumnDescription = "里程碑标准", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MilestoneCriteria { get; set; }

        /// <summary>是否关键路径(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_critical_path", ColumnDescription = "是否关键路径", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsCriticalPath { get; set; } = 0;

        /// <summary>前置计划</summary>
        [SugarColumn(ColumnName = "predecessor_plans", ColumnDescription = "前置计划", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PredecessorPlans { get; set; }

        /// <summary>后置计划</summary>
        [SugarColumn(ColumnName = "successor_plans", ColumnDescription = "后置计划", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SuccessorPlans { get; set; }

        /// <summary>依赖关系</summary>
        [SugarColumn(ColumnName = "dependency_relationship", ColumnDescription = "依赖关系", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DependencyRelationship { get; set; }

        /// <summary>实施团队</summary>
        [SugarColumn(ColumnName = "implementation_team", ColumnDescription = "实施团队", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ImplementationTeam { get; set; }

        /// <summary>计划负责人</summary>
        [SugarColumn(ColumnName = "plan_manager", ColumnDescription = "计划负责人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PlanManager { get; set; }

        /// <summary>计划负责人电话</summary>
        [SugarColumn(ColumnName = "plan_manager_phone", ColumnDescription = "计划负责人电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PlanManagerPhone { get; set; }

        /// <summary>计划负责人邮箱</summary>
        [SugarColumn(ColumnName = "plan_manager_email", ColumnDescription = "计划负责人邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PlanManagerEmail { get; set; }

        /// <summary>实施地点</summary>
        [SugarColumn(ColumnName = "implementation_location", ColumnDescription = "实施地点", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ImplementationLocation { get; set; }

        /// <summary>实施环境</summary>
        [SugarColumn(ColumnName = "implementation_environment", ColumnDescription = "实施环境", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ImplementationEnvironment { get; set; }

        /// <summary>技术要求</summary>
        [SugarColumn(ColumnName = "technical_requirements", ColumnDescription = "技术要求", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TechnicalRequirements { get; set; }

        /// <summary>质量要求</summary>
        [SugarColumn(ColumnName = "quality_requirements", ColumnDescription = "质量要求", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? QualityRequirements { get; set; }

        /// <summary>安全要求</summary>
        [SugarColumn(ColumnName = "safety_requirements", ColumnDescription = "安全要求", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SafetyRequirements { get; set; }

        /// <summary>风险识别</summary>
        [SugarColumn(ColumnName = "risk_identification", ColumnDescription = "风险识别", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RiskIdentification { get; set; }

        /// <summary>风险应对措施</summary>
        [SugarColumn(ColumnName = "risk_response_measures", ColumnDescription = "风险应对措施", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RiskResponseMeasures { get; set; }

        /// <summary>验收标准</summary>
        [SugarColumn(ColumnName = "acceptance_criteria", ColumnDescription = "验收标准", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AcceptanceCriteria { get; set; }

        /// <summary>验收方法</summary>
        [SugarColumn(ColumnName = "acceptance_method", ColumnDescription = "验收方法", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AcceptanceMethod { get; set; }

        /// <summary>验收人员</summary>
        [SugarColumn(ColumnName = "acceptance_personnel", ColumnDescription = "验收人员", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AcceptancePersonnel { get; set; }

        /// <summary>相关文档</summary>
        [SugarColumn(ColumnName = "related_documents", ColumnDescription = "相关文档", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedDocuments { get; set; }

        /// <summary>相关资源</summary>
        [SugarColumn(ColumnName = "related_resources", ColumnDescription = "相关资源", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedResources { get; set; }

        /// <summary>实施计划状态(0=草稿 1=待审核 2=已审核 3=执行中 4=已完成 5=已暂停 6=已取消)</summary>
        [SugarColumn(ColumnName = "project_implementation_plan_status", ColumnDescription = "实施计划状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ProjectImplementationPlanStatus { get; set; } = 0;

        /// <summary>实施计划备注</summary>
        [SugarColumn(ColumnName = "implementation_plan_remark", ColumnDescription = "实施计划备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ImplementationPlanRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 