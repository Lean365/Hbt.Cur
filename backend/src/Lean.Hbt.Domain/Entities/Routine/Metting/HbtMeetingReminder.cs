//===================================================================
// 项目名 : Lean.Hbt.Domain.Entities.Routine
// 文件名 : HbtMeetingReminder.cs
// 创建者 : Lean365
// 创建时间: 2024-01-26 14:30
// 版本号 : V1.0.0
// 描述   : 会议提醒管理实体
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Routine.Metting
{
    /// <summary>
    /// 会议提醒管理实体
    /// </summary>
    [SugarTable("hbt_routine_meeting_reminder", "会议提醒管理")]
    [SugarIndex("index_reminder_meeting_id", nameof(MeetingId), OrderByType.Asc)]
    [SugarIndex("index_reminder_booking_id", nameof(BookingId), OrderByType.Asc)]
    [SugarIndex("index_reminder_type", nameof(ReminderType), OrderByType.Asc)]
    [SugarIndex("index_reminder_status", nameof(Status), OrderByType.Asc)]
    [SugarIndex("index_reminder_remind_time", nameof(RemindTime), OrderByType.Asc)]
    public class HbtMeetingReminder : HbtBaseEntity
    {
        /// <summary>
        /// 会议ID
        /// </summary>
        [SugarColumn(ColumnName = "meeting_id", ColumnDescription = "会议ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? MeetingId { get; set; }

        /// <summary>
        /// 预约ID
        /// </summary>
        [SugarColumn(ColumnName = "booking_id", ColumnDescription = "预约ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? BookingId { get; set; }

        /// <summary>
        /// 提醒类型（0：会议开始前提醒，1：会议签到提醒，2：会议结束提醒，3：会议纪要提醒，4：其他）
        /// </summary>
        [SugarColumn(ColumnName = "reminder_type", ColumnDescription = "提醒类型（0：会议开始前提醒，1：会议签到提醒，2：会议结束提醒，3：会议纪要提醒，4：其他）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int ReminderType { get; set; }

        /// <summary>
        /// 提醒标题
        /// </summary>
        [SugarColumn(ColumnName = "reminder_title", ColumnDescription = "提醒标题", Length = 200, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string ReminderTitle { get; set; } = string.Empty;

        /// <summary>
        /// 提醒内容
        /// </summary>
        [SugarColumn(ColumnName = "reminder_content", ColumnDescription = "提醒内容", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ReminderContent { get; set; }

        /// <summary>
        /// 提醒状态（0：未提醒，1：已提醒，2：已确认，3：已忽略）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "提醒状态（0：未提醒，1：已提醒，2：已确认，3：已忽略）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 提醒时间
        /// </summary>
        [SugarColumn(ColumnName = "remind_time", ColumnDescription = "提醒时间", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime RemindTime { get; set; }

        /// <summary>
        /// 提前提醒分钟数
        /// </summary>
        [SugarColumn(ColumnName = "advance_minutes", ColumnDescription = "提前提醒分钟数", IsNullable = false, DefaultValue = "15", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int AdvanceMinutes { get; set; }

        /// <summary>
        /// 提醒级别（0：低，1：中，2：高，3：紧急）
        /// </summary>
        [SugarColumn(ColumnName = "priority_level", ColumnDescription = "提醒级别（0：低，1：中，2：高，3：紧急）", IsNullable = false, DefaultValue = "1", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int PriorityLevel { get; set; }

        /// <summary>
        /// 提醒方式（0：系统消息，1：邮件，2：短信，3：微信，4：全部）
        /// </summary>
        [SugarColumn(ColumnName = "reminder_method", ColumnDescription = "提醒方式（0：系统消息，1：邮件，2：短信，3：微信，4：全部）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int ReminderMethod { get; set; }

        /// <summary>
        /// 提醒人ID列表
        /// </summary>
        [SugarColumn(ColumnName = "reminder_user_ids", ColumnDescription = "提醒人ID列表", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ReminderUserIds { get; set; }

        /// <summary>
        /// 提醒人姓名列表
        /// </summary>
        [SugarColumn(ColumnName = "reminder_user_names", ColumnDescription = "提醒人姓名列表", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ReminderUserNames { get; set; }

        /// <summary>
        /// 首次提醒时间
        /// </summary>
        [SugarColumn(ColumnName = "first_remind_time", ColumnDescription = "首次提醒时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? FirstRemindTime { get; set; }

        /// <summary>
        /// 最后提醒时间
        /// </summary>
        [SugarColumn(ColumnName = "last_remind_time", ColumnDescription = "最后提醒时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? LastRemindTime { get; set; }

        /// <summary>
        /// 提醒次数
        /// </summary>
        [SugarColumn(ColumnName = "remind_count", ColumnDescription = "提醒次数", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int RemindCount { get; set; }

        /// <summary>
        /// 最大提醒次数
        /// </summary>
        [SugarColumn(ColumnName = "max_remind_count", ColumnDescription = "最大提醒次数", IsNullable = false, DefaultValue = "3", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int MaxRemindCount { get; set; }

        /// <summary>
        /// 提醒间隔（分钟）
        /// </summary>
        [SugarColumn(ColumnName = "remind_interval", ColumnDescription = "提醒间隔（分钟）", IsNullable = false, DefaultValue = "30", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int RemindInterval { get; set; }

        /// <summary>
        /// 确认时间
        /// </summary>
        [SugarColumn(ColumnName = "confirm_time", ColumnDescription = "确认时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ConfirmTime { get; set; }

        /// <summary>
        /// 确认人ID
        /// </summary>
        [SugarColumn(ColumnName = "confirm_user_id", ColumnDescription = "确认人ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? ConfirmUserId { get; set; }

        /// <summary>
        /// 确认人姓名
        /// </summary>
        [SugarColumn(ColumnName = "confirm_user_name", ColumnDescription = "确认人姓名", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ConfirmUserName { get; set; }

        /// <summary>
        /// 确认备注
        /// </summary>
        [SugarColumn(ColumnName = "confirm_remarks", ColumnDescription = "确认备注", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ConfirmRemarks { get; set; }

        /// <summary>
        /// 忽略时间
        /// </summary>
        [SugarColumn(ColumnName = "ignore_time", ColumnDescription = "忽略时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? IgnoreTime { get; set; }

        /// <summary>
        /// 忽略人ID
        /// </summary>
        [SugarColumn(ColumnName = "ignore_user_id", ColumnDescription = "忽略人ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? IgnoreUserId { get; set; }

        /// <summary>
        /// 忽略人姓名
        /// </summary>
        [SugarColumn(ColumnName = "ignore_user_name", ColumnDescription = "忽略人姓名", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? IgnoreUserName { get; set; }

        /// <summary>
        /// 忽略原因
        /// </summary>
        [SugarColumn(ColumnName = "ignore_reason", ColumnDescription = "忽略原因", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? IgnoreReason { get; set; }

        /// <summary>
        /// 会议标题
        /// </summary>
        [SugarColumn(ColumnName = "meeting_title", ColumnDescription = "会议标题", Length = 200, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? MeetingTitle { get; set; }

        /// <summary>
        /// 会议开始时间
        /// </summary>
        [SugarColumn(ColumnName = "meeting_start_time", ColumnDescription = "会议开始时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? MeetingStartTime { get; set; }

        /// <summary>
        /// 会议结束时间
        /// </summary>
        [SugarColumn(ColumnName = "meeting_end_time", ColumnDescription = "会议结束时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? MeetingEndTime { get; set; }

        /// <summary>
        /// 会议地点
        /// </summary>
        [SugarColumn(ColumnName = "meeting_location", ColumnDescription = "会议地点", Length = 200, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? MeetingLocation { get; set; }

        /// <summary>
        /// 会议主持人
        /// </summary>
        [SugarColumn(ColumnName = "meeting_host", ColumnDescription = "会议主持人", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? MeetingHost { get; set; }

        /// <summary>
        /// 会议参与人
        /// </summary>
        [SugarColumn(ColumnName = "meeting_participants", ColumnDescription = "会议参与人", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? MeetingParticipants { get; set; }

        /// <summary>
        /// 会议室名称
        /// </summary>
        [SugarColumn(ColumnName = "room_name", ColumnDescription = "会议室名称", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? RoomName { get; set; }

        /// <summary>
        /// 是否重复提醒（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "is_repeat", ColumnDescription = "是否重复提醒（0：否，1：是）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int IsRepeat { get; set; }

        /// <summary>
        /// 重复周期（分钟）
        /// </summary>
        [SugarColumn(ColumnName = "repeat_cycle", ColumnDescription = "重复周期（分钟）", IsNullable = true, ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int? RepeatCycle { get; set; }

        /// <summary>
        /// 下次提醒时间
        /// </summary>
        [SugarColumn(ColumnName = "next_remind_time", ColumnDescription = "下次提醒时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? NextRemindTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "remarks", ColumnDescription = "备注", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Remarks { get; set; }
    }
} 