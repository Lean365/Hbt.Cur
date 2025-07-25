#nullable enable

using SqlSugar;
using Lean.Hbt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtProjectAssessmentLog.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 项目考核与日志实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Routine.Project
{
    /// <summary>
    /// 项目考核与日志实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 说明: 记录项目考核与日志的相关信息，包括考核信息、日志记录、评价等
    /// </remarks>
    [SugarTable("hbt_routine_project_assessment_log", "项目考核与日志表")]
    [SugarIndex("ix_project_assessment_log_code", nameof(ProjectAssessmentLogCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_project_assessment_log", nameof(CompanyCode), OrderByType.Asc, nameof(ProjectAssessmentLogCode), OrderByType.Asc, false)]
    [SugarIndex("ix_project_assessment_log_status", nameof(ProjectAssessmentLogStatus), OrderByType.Asc, false)]
    [SugarIndex("ix_project_assessment_log_type", nameof(ProjectAssessmentLogType), OrderByType.Asc, false)]
    public class HbtProjectAssessmentLog : HbtBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>项目考核与日志编号</summary>
        [SugarColumn(ColumnName = "project_assessment_log_code", ColumnDescription = "项目考核与日志编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProjectAssessmentLogCode { get; set; } = string.Empty;

        /// <summary>考核或日志标题</summary>
        [SugarColumn(ColumnName = "assessment_log_title", ColumnDescription = "考核或日志标题", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string AssessmentLogTitle { get; set; } = string.Empty;

        /// <summary>考核与日志类型(1=项目考核 2=人员考核 3=质量考核 4=进度考核 5=成本考核 6=风险考核 7=工作日志 8=问题日志 9=其他)</summary>
        [SugarColumn(ColumnName = "project_assessment_log_type", ColumnDescription = "考核与日志类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ProjectAssessmentLogType { get; set; } = 1;

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

        /// <summary>考核或日志内容</summary>
        [SugarColumn(ColumnName = "assessment_log_content", ColumnDescription = "考核或日志内容", Length = 2000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssessmentLogContent { get; set; }

        /// <summary>考核或日志摘要</summary>
        [SugarColumn(ColumnName = "assessment_log_summary", ColumnDescription = "考核或日志摘要", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssessmentLogSummary { get; set; }

        /// <summary>考核日期</summary>
        [SugarColumn(ColumnName = "assessment_date", ColumnDescription = "考核日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? AssessmentDate { get; set; }

        /// <summary>考核周期(1=日 2=周 3=月 4=季度 5=年 6=项目周期)</summary>
        [SugarColumn(ColumnName = "assessment_period", ColumnDescription = "考核周期", ColumnDataType = "int", IsNullable = false, DefaultValue = "3")]
        public int AssessmentPeriod { get; set; } = 3;

        /// <summary>考核开始日期</summary>
        [SugarColumn(ColumnName = "assessment_start_date", ColumnDescription = "考核开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? AssessmentStartDate { get; set; }

        /// <summary>考核结束日期</summary>
        [SugarColumn(ColumnName = "assessment_end_date", ColumnDescription = "考核结束日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? AssessmentEndDate { get; set; }

        /// <summary>日志日期</summary>
        [SugarColumn(ColumnName = "log_date", ColumnDescription = "日志日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? LogDate { get; set; }

        /// <summary>日志时间</summary>
        [SugarColumn(ColumnName = "log_time", ColumnDescription = "日志时间", ColumnDataType = "time", IsNullable = true)]
        public TimeSpan? LogTime { get; set; }

        /// <summary>被考核人</summary>
        [SugarColumn(ColumnName = "assessed_person", ColumnDescription = "被考核人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssessedPerson { get; set; }

        /// <summary>被考核人电话</summary>
        [SugarColumn(ColumnName = "assessed_person_phone", ColumnDescription = "被考核人电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssessedPersonPhone { get; set; }

        /// <summary>被考核人邮箱</summary>
        [SugarColumn(ColumnName = "assessed_person_email", ColumnDescription = "被考核人邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssessedPersonEmail { get; set; }

        /// <summary>被考核人部门</summary>
        [SugarColumn(ColumnName = "assessed_person_department", ColumnDescription = "被考核人部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssessedPersonDepartment { get; set; }

        /// <summary>被考核人岗位</summary>
        [SugarColumn(ColumnName = "assessed_person_position", ColumnDescription = "被考核人岗位", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssessedPersonPosition { get; set; }

        /// <summary>考核人</summary>
        [SugarColumn(ColumnName = "assessor", ColumnDescription = "考核人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Assessor { get; set; }

        /// <summary>考核人电话</summary>
        [SugarColumn(ColumnName = "assessor_phone", ColumnDescription = "考核人电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssessorPhone { get; set; }

        /// <summary>考核人邮箱</summary>
        [SugarColumn(ColumnName = "assessor_email", ColumnDescription = "考核人邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssessorEmail { get; set; }

        /// <summary>考核人部门</summary>
        [SugarColumn(ColumnName = "assessor_department", ColumnDescription = "考核人部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssessorDepartment { get; set; }

        /// <summary>考核人岗位</summary>
        [SugarColumn(ColumnName = "assessor_position", ColumnDescription = "考核人岗位", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssessorPosition { get; set; }

        /// <summary>考核指标</summary>
        [SugarColumn(ColumnName = "assessment_indicators", ColumnDescription = "考核指标", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssessmentIndicators { get; set; }

        /// <summary>考核标准</summary>
        [SugarColumn(ColumnName = "assessment_criteria", ColumnDescription = "考核标准", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssessmentCriteria { get; set; }

        /// <summary>考核方法</summary>
        [SugarColumn(ColumnName = "assessment_method", ColumnDescription = "考核方法", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssessmentMethod { get; set; }

        /// <summary>考核权重(%)</summary>
        [SugarColumn(ColumnName = "assessment_weight", ColumnDescription = "考核权重(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "100")]
        public decimal AssessmentWeight { get; set; } = 100;

        /// <summary>考核得分</summary>
        [SugarColumn(ColumnName = "assessment_score", ColumnDescription = "考核得分", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? AssessmentScore { get; set; }

        /// <summary>满分</summary>
        [SugarColumn(ColumnName = "full_score", ColumnDescription = "满分", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "100")]
        public decimal FullScore { get; set; } = 100;

        /// <summary>得分率(%)</summary>
        [SugarColumn(ColumnName = "score_rate", ColumnDescription = "得分率(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ScoreRate { get; set; } = 0;

        /// <summary>考核等级(1=优秀 2=良好 3=合格 4=不合格)</summary>
        [SugarColumn(ColumnName = "assessment_grade", ColumnDescription = "考核等级", ColumnDataType = "int", IsNullable = false, DefaultValue = "3")]
        public int AssessmentGrade { get; set; } = 3;

        /// <summary>考核结果</summary>
        [SugarColumn(ColumnName = "assessment_result", ColumnDescription = "考核结果", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssessmentResult { get; set; }

        /// <summary>考核评价</summary>
        [SugarColumn(ColumnName = "assessment_evaluation", ColumnDescription = "考核评价", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssessmentEvaluation { get; set; }

        /// <summary>优点</summary>
        [SugarColumn(ColumnName = "strengths", ColumnDescription = "优点", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Strengths { get; set; }

        /// <summary>不足</summary>
        [SugarColumn(ColumnName = "weaknesses", ColumnDescription = "不足", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Weaknesses { get; set; }

        /// <summary>改进建议</summary>
        [SugarColumn(ColumnName = "improvement_suggestions", ColumnDescription = "改进建议", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ImprovementSuggestions { get; set; }

        /// <summary>工作内容</summary>
        [SugarColumn(ColumnName = "work_content", ColumnDescription = "工作内容", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? WorkContent { get; set; }

        /// <summary>工作时间(小时)</summary>
        [SugarColumn(ColumnName = "work_hours", ColumnDescription = "工作时间(小时)", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? WorkHours { get; set; }

        /// <summary>工作成果</summary>
        [SugarColumn(ColumnName = "work_achievements", ColumnDescription = "工作成果", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? WorkAchievements { get; set; }

        /// <summary>遇到的问题</summary>
        [SugarColumn(ColumnName = "problems_encountered", ColumnDescription = "遇到的问题", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProblemsEncountered { get; set; }

        /// <summary>解决方案</summary>
        [SugarColumn(ColumnName = "solutions", ColumnDescription = "解决方案", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Solutions { get; set; }

        /// <summary>明日计划</summary>
        [SugarColumn(ColumnName = "tomorrow_plan", ColumnDescription = "明日计划", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TomorrowPlan { get; set; }

        /// <summary>需要支持</summary>
        [SugarColumn(ColumnName = "support_needed", ColumnDescription = "需要支持", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupportNeeded { get; set; }

        /// <summary>考核状态(0=草稿 1=待考核 2=考核中 3=已考核 4=已确认 5=已归档)</summary>
        [SugarColumn(ColumnName = "assessment_status", ColumnDescription = "考核状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int AssessmentStatus { get; set; } = 0;

        /// <summary>是否被考核人确认(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_confirmed_by_assessed", ColumnDescription = "是否被考核人确认", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsConfirmedByAssessed { get; set; } = 0;

        /// <summary>被考核人确认日期</summary>
        [SugarColumn(ColumnName = "assessed_confirmation_date", ColumnDescription = "被考核人确认日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? AssessedConfirmationDate { get; set; }

        /// <summary>被考核人意见</summary>
        [SugarColumn(ColumnName = "assessed_opinion", ColumnDescription = "被考核人意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssessedOpinion { get; set; }

        /// <summary>相关附件</summary>
        [SugarColumn(ColumnName = "related_attachments", ColumnDescription = "相关附件", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedAttachments { get; set; }

        /// <summary>项目考核与日志状态(0=草稿 1=待审核 2=已审核 3=已发布 4=已归档)</summary>
        [SugarColumn(ColumnName = "project_assessment_log_status", ColumnDescription = "项目考核与日志状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ProjectAssessmentLogStatus { get; set; } = 0;

        /// <summary>考核与日志备注</summary>
        [SugarColumn(ColumnName = "assessment_log_remark", ColumnDescription = "考核与日志备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssessmentLogRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 