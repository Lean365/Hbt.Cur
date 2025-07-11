#nullable enable

using SqlSugar;
using Lean.Hbt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtProjectRiskIssue.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述    : 项目风险与问题实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Routine.Project
{
    /// <summary>
    /// 项目风险与问题实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 说明: 记录项目风险与问题的相关信息，包括风险识别、评估、应对等
    /// </remarks>
    [SugarTable("hbt_routine_project_risk_issue", "项目风险与问题表")]
    [SugarIndex("ix_project_risk_issue_code", nameof(ProjectRiskIssueCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_project_risk_issue", nameof(CompanyCode), OrderByType.Asc, nameof(ProjectRiskIssueCode), OrderByType.Asc, false)]
    [SugarIndex("ix_project_risk_issue_status", nameof(ProjectRiskIssueStatus), OrderByType.Asc, false)]
    [SugarIndex("ix_project_risk_issue_type", nameof(ProjectRiskIssueType), OrderByType.Asc, false)]
    public class HbtProjectRiskIssue : HbtBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>项目风险与问题编号</summary>
        [SugarColumn(ColumnName = "project_risk_issue_code", ColumnDescription = "项目风险与问题编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProjectRiskIssueCode { get; set; } = string.Empty;

        /// <summary>风险与问题名称</summary>
        [SugarColumn(ColumnName = "risk_issue_name", ColumnDescription = "风险与问题名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RiskIssueName { get; set; } = string.Empty;

        /// <summary>风险与问题类型(1=技术风险 2=进度风险 3=成本风险 4=质量风险 5=人员风险 6=外部风险 7=问题 8=其他)</summary>
        [SugarColumn(ColumnName = "project_risk_issue_type", ColumnDescription = "风险与问题类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ProjectRiskIssueType { get; set; } = 1;

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

        /// <summary>风险与问题描述</summary>
        [SugarColumn(ColumnName = "risk_issue_description", ColumnDescription = "风险与问题描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RiskIssueDescription { get; set; }

        /// <summary>风险与问题原因</summary>
        [SugarColumn(ColumnName = "risk_issue_cause", ColumnDescription = "风险与问题原因", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RiskIssueCause { get; set; }

        /// <summary>风险与问题影响</summary>
        [SugarColumn(ColumnName = "risk_issue_impact", ColumnDescription = "风险与问题影响", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RiskIssueImpact { get; set; }

        /// <summary>风险概率(1=极低 2=低 3=中等 4=高 5=极高)</summary>
        [SugarColumn(ColumnName = "risk_probability", ColumnDescription = "风险概率", ColumnDataType = "int", IsNullable = false, DefaultValue = "3")]
        public int RiskProbability { get; set; } = 3;

        /// <summary>风险影响程度(1=极低 2=低 3=中等 4=高 5=极高)</summary>
        [SugarColumn(ColumnName = "risk_impact_level", ColumnDescription = "风险影响程度", ColumnDataType = "int", IsNullable = false, DefaultValue = "3")]
        public int RiskImpactLevel { get; set; } = 3;

        /// <summary>风险等级(1=低 2=中 3=高 4=极高)</summary>
        [SugarColumn(ColumnName = "risk_level", ColumnDescription = "风险等级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int RiskLevel { get; set; } = 2;

        /// <summary>问题严重程度(1=轻微 2=一般 3=严重 4=紧急)</summary>
        [SugarColumn(ColumnName = "issue_severity", ColumnDescription = "问题严重程度", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int IssueSeverity { get; set; } = 2;

        /// <summary>问题优先级(1=低 2=中 3=高 4=紧急)</summary>
        [SugarColumn(ColumnName = "issue_priority", ColumnDescription = "问题优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int IssuePriority { get; set; } = 2;

        /// <summary>发现日期</summary>
        [SugarColumn(ColumnName = "discovery_date", ColumnDescription = "发现日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? DiscoveryDate { get; set; }

        /// <summary>预计发生日期</summary>
        [SugarColumn(ColumnName = "expected_occurrence_date", ColumnDescription = "预计发生日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ExpectedOccurrenceDate { get; set; }

        /// <summary>实际发生日期</summary>
        [SugarColumn(ColumnName = "actual_occurrence_date", ColumnDescription = "实际发生日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualOccurrenceDate { get; set; }

        /// <summary>预计影响成本</summary>
        [SugarColumn(ColumnName = "expected_impact_cost", ColumnDescription = "预计影响成本", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? ExpectedImpactCost { get; set; }

        /// <summary>实际影响成本</summary>
        [SugarColumn(ColumnName = "actual_impact_cost", ColumnDescription = "实际影响成本", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? ActualImpactCost { get; set; }

        /// <summary>币种(CNY=人民币 USD=美元 EUR=欧元)</summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>预计影响工期(天)</summary>
        [SugarColumn(ColumnName = "expected_impact_duration", ColumnDescription = "预计影响工期(天)", ColumnDataType = "int", IsNullable = true)]
        public int? ExpectedImpactDuration { get; set; }

        /// <summary>实际影响工期(天)</summary>
        [SugarColumn(ColumnName = "actual_impact_duration", ColumnDescription = "实际影响工期(天)", ColumnDataType = "int", IsNullable = true)]
        public int? ActualImpactDuration { get; set; }

        /// <summary>风险应对策略(1=规避 2=转移 3=减轻 4=接受)</summary>
        [SugarColumn(ColumnName = "risk_response_strategy", ColumnDescription = "风险应对策略", ColumnDataType = "int", IsNullable = false, DefaultValue = "3")]
        public int RiskResponseStrategy { get; set; } = 3;

        /// <summary>问题解决方案</summary>
        [SugarColumn(ColumnName = "issue_solution", ColumnDescription = "问题解决方案", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IssueSolution { get; set; }

        /// <summary>应对措施</summary>
        [SugarColumn(ColumnName = "response_measures", ColumnDescription = "应对措施", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ResponseMeasures { get; set; }

        /// <summary>预防措施</summary>
        [SugarColumn(ColumnName = "preventive_measures", ColumnDescription = "预防措施", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PreventiveMeasures { get; set; }

        /// <summary>应急计划</summary>
        [SugarColumn(ColumnName = "contingency_plan", ColumnDescription = "应急计划", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContingencyPlan { get; set; }

        /// <summary>负责人</summary>
        [SugarColumn(ColumnName = "responsible_person", ColumnDescription = "负责人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ResponsiblePerson { get; set; }

        /// <summary>负责人电话</summary>
        [SugarColumn(ColumnName = "responsible_person_phone", ColumnDescription = "负责人电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ResponsiblePersonPhone { get; set; }

        /// <summary>负责人邮箱</summary>
        [SugarColumn(ColumnName = "responsible_person_email", ColumnDescription = "负责人邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ResponsiblePersonEmail { get; set; }

        /// <summary>计划完成日期</summary>
        [SugarColumn(ColumnName = "planned_completion_date", ColumnDescription = "计划完成日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PlannedCompletionDate { get; set; }

        /// <summary>实际完成日期</summary>
        [SugarColumn(ColumnName = "actual_completion_date", ColumnDescription = "实际完成日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualCompletionDate { get; set; }

        /// <summary>解决状态(0=未解决 1=解决中 2=已解决 3=已关闭)</summary>
        [SugarColumn(ColumnName = "resolution_status", ColumnDescription = "解决状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ResolutionStatus { get; set; } = 0;

        /// <summary>解决结果</summary>
        [SugarColumn(ColumnName = "resolution_result", ColumnDescription = "解决结果", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ResolutionResult { get; set; }

        /// <summary>相关方</summary>
        [SugarColumn(ColumnName = "stakeholders", ColumnDescription = "相关方", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Stakeholders { get; set; }

        /// <summary>相关文档</summary>
        [SugarColumn(ColumnName = "related_documents", ColumnDescription = "相关文档", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedDocuments { get; set; }

        /// <summary>项目风险与问题状态(0=活跃 1=监控中 2=已缓解 3=已关闭 4=已升级)</summary>
        [SugarColumn(ColumnName = "project_risk_issue_status", ColumnDescription = "项目风险与问题状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ProjectRiskIssueStatus { get; set; } = 0;

        /// <summary>风险与问题备注</summary>
        [SugarColumn(ColumnName = "risk_issue_remark", ColumnDescription = "风险与问题备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RiskIssueRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 