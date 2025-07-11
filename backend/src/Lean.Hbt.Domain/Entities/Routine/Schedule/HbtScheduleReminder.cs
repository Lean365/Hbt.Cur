#nullable enable

using SqlSugar;
using Lean.Hbt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtScheduleReminder.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述    : 日程提醒实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Routine.Schedule
{
    /// <summary>
    /// 日程提醒实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 说明: 记录日程提醒的相关信息，包括提醒内容、提醒时间、提醒方式、提醒状态等
    /// </remarks>
    [SugarTable("hbt_routine_schedule_reminder", "日程提醒表")]
    [SugarIndex("ix_schedule_reminder_code", nameof(ReminderCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_schedule_reminder", nameof(CompanyCode), OrderByType.Asc, nameof(ReminderCode), OrderByType.Asc, false)]
    [SugarIndex("ix_schedule_reminder_status", nameof(ReminderStatus), OrderByType.Asc, false)]
    [SugarIndex("ix_schedule_reminder_type", nameof(ReminderType), OrderByType.Asc, false)]
    [SugarIndex("ix_schedule_reminder_time", nameof(ReminderTime), OrderByType.Asc, false)]
    public class HbtScheduleReminder : HbtBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>提醒编号</summary>
        [SugarColumn(ColumnName = "reminder_code", ColumnDescription = "提醒编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ReminderCode { get; set; } = string.Empty;

        /// <summary>提醒标题</summary>
        [SugarColumn(ColumnName = "reminder_title", ColumnDescription = "提醒标题", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ReminderTitle { get; set; } = string.Empty;

        /// <summary>提醒内容</summary>
        [SugarColumn(ColumnName = "reminder_content", ColumnDescription = "提醒内容", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReminderContent { get; set; }

        /// <summary>提醒类型(1=会议提醒 2=任务提醒 3=日程提醒 4=生日提醒 5=纪念日提醒 6=其他提醒)</summary>
        [SugarColumn(ColumnName = "reminder_type", ColumnDescription = "提醒类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ReminderType { get; set; } = 1;

        /// <summary>提醒时间</summary>
        [SugarColumn(ColumnName = "reminder_time", ColumnDescription = "提醒时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime ReminderTime { get; set; } = DateTime.Now;

        /// <summary>提醒日期</summary>
        [SugarColumn(ColumnName = "reminder_date", ColumnDescription = "提醒日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime ReminderDate { get; set; } = DateTime.Today;

        /// <summary>提醒方式(1=系统提醒 2=邮件提醒 3=短信提醒 4=微信提醒 5=钉钉提醒 6=多方式提醒)</summary>
        [SugarColumn(ColumnName = "reminder_method", ColumnDescription = "提醒方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ReminderMethod { get; set; } = 1;

        /// <summary>提前提醒时间(分钟)</summary>
        [SugarColumn(ColumnName = "advance_remind_minutes", ColumnDescription = "提前提醒时间(分钟)", ColumnDataType = "int", IsNullable = false, DefaultValue = "15")]
        public int AdvanceRemindMinutes { get; set; } = 15;

        /// <summary>重复类型(0=不重复 1=每天 2=每周 3=每月 4=每年 5=工作日 6=自定义)</summary>
        [SugarColumn(ColumnName = "repeat_type", ColumnDescription = "重复类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int RepeatType { get; set; } = 0;

        /// <summary>重复间隔</summary>
        [SugarColumn(ColumnName = "repeat_interval", ColumnDescription = "重复间隔", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int RepeatInterval { get; set; } = 1;

        /// <summary>重复结束日期</summary>
        [SugarColumn(ColumnName = "repeat_end_date", ColumnDescription = "重复结束日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? RepeatEndDate { get; set; }

        /// <summary>重复次数</summary>
        [SugarColumn(ColumnName = "repeat_count", ColumnDescription = "重复次数", ColumnDataType = "int", IsNullable = true)]
        public int? RepeatCount { get; set; }

        /// <summary>已重复次数</summary>
        [SugarColumn(ColumnName = "repeated_count", ColumnDescription = "已重复次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int RepeatedCount { get; set; } = 0;

        /// <summary>关联对象类型</summary>
        [SugarColumn(ColumnName = "related_object_type", ColumnDescription = "关联对象类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedObjectType { get; set; }

        /// <summary>关联对象ID</summary>
        [SugarColumn(ColumnName = "related_object_id", ColumnDescription = "关联对象ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? RelatedObjectId { get; set; }

        /// <summary>关联对象编号</summary>
        [SugarColumn(ColumnName = "related_object_code", ColumnDescription = "关联对象编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedObjectCode { get; set; }

        /// <summary>关联对象名称</summary>
        [SugarColumn(ColumnName = "related_object_name", ColumnDescription = "关联对象名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedObjectName { get; set; }

        /// <summary>提醒人</summary>
        [SugarColumn(ColumnName = "reminder_person", ColumnDescription = "提醒人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReminderPerson { get; set; }

        /// <summary>提醒人电话</summary>
        [SugarColumn(ColumnName = "reminder_person_phone", ColumnDescription = "提醒人电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReminderPersonPhone { get; set; }

        /// <summary>提醒人邮箱</summary>
        [SugarColumn(ColumnName = "reminder_person_email", ColumnDescription = "提醒人邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReminderPersonEmail { get; set; }

        /// <summary>提醒人部门</summary>
        [SugarColumn(ColumnName = "reminder_person_department", ColumnDescription = "提醒人部门", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReminderPersonDepartment { get; set; }

        /// <summary>提醒状态(0=未提醒 1=已提醒 2=已确认 3=已忽略 4=已取消)</summary>
        [SugarColumn(ColumnName = "reminder_status", ColumnDescription = "提醒状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ReminderStatus { get; set; } = 0;

        /// <summary>实际提醒时间</summary>
        [SugarColumn(ColumnName = "actual_remind_time", ColumnDescription = "实际提醒时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ActualRemindTime { get; set; }

        /// <summary>确认时间</summary>
        [SugarColumn(ColumnName = "confirm_time", ColumnDescription = "确认时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ConfirmTime { get; set; }

        /// <summary>确认人</summary>
        [SugarColumn(ColumnName = "confirm_person", ColumnDescription = "确认人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ConfirmPerson { get; set; }

        /// <summary>优先级(1=低 2=中 3=高 4=紧急)</summary>
        [SugarColumn(ColumnName = "priority", ColumnDescription = "优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int Priority { get; set; } = 2;

        /// <summary>是否重要(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_important", ColumnDescription = "是否重要", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsImportant { get; set; } = 0;

        /// <summary>是否紧急(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_urgent", ColumnDescription = "是否紧急", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsUrgent { get; set; } = 0;

        /// <summary>提醒图标</summary>
        [SugarColumn(ColumnName = "reminder_icon", ColumnDescription = "提醒图标", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReminderIcon { get; set; }

        /// <summary>提醒颜色</summary>
        [SugarColumn(ColumnName = "reminder_color", ColumnDescription = "提醒颜色", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReminderColor { get; set; }

        /// <summary>提醒声音</summary>
        [SugarColumn(ColumnName = "reminder_sound", ColumnDescription = "提醒声音", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReminderSound { get; set; }

        /// <summary>相关链接</summary>
        [SugarColumn(ColumnName = "related_link", ColumnDescription = "相关链接", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedLink { get; set; }

        /// <summary>相关附件</summary>
        [SugarColumn(ColumnName = "related_attachments", ColumnDescription = "相关附件", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedAttachments { get; set; }

        /// <summary>备注</summary>
        [SugarColumn(ColumnName = "schedule_reminder_remark", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ScheduleReminderRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 