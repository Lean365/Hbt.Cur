#nullable enable

using SqlSugar;
using Lean.Hbt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTeamSchedule.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述    : 团队协作日程实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Routine.Schedule
{
    /// <summary>
    /// 团队协作日程实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 说明: 记录团队协作日程的相关信息，包括团队信息、协作内容、参与人员、进度跟踪等
    /// </remarks>
    [SugarTable("hbt_routine_team_schedule", "团队协作日程表")]
    [SugarIndex("ix_team_schedule_code", nameof(TeamScheduleCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_team_schedule", nameof(CompanyCode), OrderByType.Asc, nameof(TeamScheduleCode), OrderByType.Asc, false)]
    [SugarIndex("ix_team_schedule_status", nameof(ScheduleStatus), OrderByType.Asc, false)]
    [SugarIndex("ix_team_schedule_type", nameof(ScheduleType), OrderByType.Asc, false)]
    public class HbtTeamSchedule : HbtBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>团队协作日程编号</summary>
        [SugarColumn(ColumnName = "team_schedule_code", ColumnDescription = "团队协作日程编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string TeamScheduleCode { get; set; } = string.Empty;

        /// <summary>团队协作日程标题</summary>
        [SugarColumn(ColumnName = "team_schedule_title", ColumnDescription = "团队协作日程标题", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string TeamScheduleTitle { get; set; } = string.Empty;

        /// <summary>团队协作日程类型(1=项目协作 2=任务协作 3=会议协作 4=培训协作 5=活动协作 6=其他协作)</summary>
        [SugarColumn(ColumnName = "schedule_type", ColumnDescription = "团队协作日程类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ScheduleType { get; set; } = 1;

        /// <summary>团队协作日程描述</summary>
        [SugarColumn(ColumnName = "team_schedule_description", ColumnDescription = "团队协作日程描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TeamScheduleDescription { get; set; }

        /// <summary>协作目标</summary>
        [SugarColumn(ColumnName = "collaboration_goal", ColumnDescription = "协作目标", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CollaborationGoal { get; set; }

        /// <summary>协作内容</summary>
        [SugarColumn(ColumnName = "collaboration_content", ColumnDescription = "协作内容", Length = 2000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CollaborationContent { get; set; }

        /// <summary>计划开始时间</summary>
        [SugarColumn(ColumnName = "planned_start_time", ColumnDescription = "计划开始时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime PlannedStartTime { get; set; } = DateTime.Now;

        /// <summary>计划结束时间</summary>
        [SugarColumn(ColumnName = "planned_end_time", ColumnDescription = "计划结束时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime PlannedEndTime { get; set; } = DateTime.Now.AddHours(2);

        /// <summary>实际开始时间</summary>
        [SugarColumn(ColumnName = "actual_start_time", ColumnDescription = "实际开始时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ActualStartTime { get; set; }

        /// <summary>实际结束时间</summary>
        [SugarColumn(ColumnName = "actual_end_time", ColumnDescription = "实际结束时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ActualEndTime { get; set; }

        /// <summary>协作地点</summary>
        [SugarColumn(ColumnName = "collaboration_location", ColumnDescription = "协作地点", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CollaborationLocation { get; set; }

        /// <summary>协作方式(1=线下协作 2=线上协作 3=混合协作)</summary>
        [SugarColumn(ColumnName = "collaboration_method", ColumnDescription = "协作方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int CollaborationMethod { get; set; } = 1;

        /// <summary>团队负责人</summary>
        [SugarColumn(ColumnName = "team_leader", ColumnDescription = "团队负责人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TeamLeader { get; set; }

        /// <summary>团队负责人电话</summary>
        [SugarColumn(ColumnName = "team_leader_phone", ColumnDescription = "团队负责人电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TeamLeaderPhone { get; set; }

        /// <summary>团队负责人邮箱</summary>
        [SugarColumn(ColumnName = "team_leader_email", ColumnDescription = "团队负责人邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TeamLeaderEmail { get; set; }

        /// <summary>参与人员</summary>
        [SugarColumn(ColumnName = "participants", ColumnDescription = "参与人员", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Participants { get; set; }

        /// <summary>参与部门</summary>
        [SugarColumn(ColumnName = "participant_departments", ColumnDescription = "参与部门", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ParticipantDepartments { get; set; }

        /// <summary>预计参与人数</summary>
        [SugarColumn(ColumnName = "expected_participant_count", ColumnDescription = "预计参与人数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ExpectedParticipantCount { get; set; } = 0;

        /// <summary>实际参与人数</summary>
        [SugarColumn(ColumnName = "actual_participant_count", ColumnDescription = "实际参与人数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ActualParticipantCount { get; set; } = 0;

        /// <summary>协作工具</summary>
        [SugarColumn(ColumnName = "collaboration_tools", ColumnDescription = "协作工具", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CollaborationTools { get; set; }

        /// <summary>协作平台</summary>
        [SugarColumn(ColumnName = "collaboration_platform", ColumnDescription = "协作平台", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CollaborationPlatform { get; set; }

        /// <summary>协作链接</summary>
        [SugarColumn(ColumnName = "collaboration_link", ColumnDescription = "协作链接", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CollaborationLink { get; set; }

        /// <summary>协作密码</summary>
        [SugarColumn(ColumnName = "collaboration_password", ColumnDescription = "协作密码", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CollaborationPassword { get; set; }

        /// <summary>进度百分比</summary>
        [SugarColumn(ColumnName = "progress_percentage", ColumnDescription = "进度百分比", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ProgressPercentage { get; set; } = 0;

        /// <summary>当前阶段</summary>
        [SugarColumn(ColumnName = "current_stage", ColumnDescription = "当前阶段", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CurrentStage { get; set; }

        /// <summary>完成情况</summary>
        [SugarColumn(ColumnName = "completion_status", ColumnDescription = "完成情况", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompletionStatus { get; set; }

        /// <summary>遇到的问题</summary>
        [SugarColumn(ColumnName = "issues_encountered", ColumnDescription = "遇到的问题", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IssuesEncountered { get; set; }

        /// <summary>解决方案</summary>
        [SugarColumn(ColumnName = "solutions", ColumnDescription = "解决方案", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Solutions { get; set; }

        /// <summary>下一步计划</summary>
        [SugarColumn(ColumnName = "next_steps", ColumnDescription = "下一步计划", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NextSteps { get; set; }

        /// <summary>团队协作日程状态(0=未开始 1=进行中 2=已完成 3=已暂停 4=已取消)</summary>
        [SugarColumn(ColumnName = "schedule_status", ColumnDescription = "团队协作日程状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ScheduleStatus { get; set; } = 0;

        /// <summary>优先级(1=低 2=中 3=高 4=紧急)</summary>
        [SugarColumn(ColumnName = "priority", ColumnDescription = "优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int Priority { get; set; } = 2;

        /// <summary>是否重要(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_important", ColumnDescription = "是否重要", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsImportant { get; set; } = 0;

        /// <summary>是否紧急(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_urgent", ColumnDescription = "是否紧急", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsUrgent { get; set; } = 0;

        /// <summary>是否需要提醒(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "need_reminder", ColumnDescription = "是否需要提醒", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int NeedReminder { get; set; } = 1;

        /// <summary>提醒时间(分钟)</summary>
        [SugarColumn(ColumnName = "remind_minutes", ColumnDescription = "提醒时间(分钟)", ColumnDataType = "int", IsNullable = true)]
        public int? RemindMinutes { get; set; }

        /// <summary>相关项目</summary>
        [SugarColumn(ColumnName = "related_project", ColumnDescription = "相关项目", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedProject { get; set; }

        /// <summary>相关任务</summary>
        [SugarColumn(ColumnName = "related_task", ColumnDescription = "相关任务", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedTask { get; set; }

        /// <summary>相关会议</summary>
        [SugarColumn(ColumnName = "related_meeting", ColumnDescription = "相关会议", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedMeeting { get; set; }

        /// <summary>相关附件</summary>
        [SugarColumn(ColumnName = "related_attachments", ColumnDescription = "相关附件", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedAttachments { get; set; }

        /// <summary>备注</summary>
        [SugarColumn(ColumnName = "team_schedule_remark", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TeamScheduleRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 