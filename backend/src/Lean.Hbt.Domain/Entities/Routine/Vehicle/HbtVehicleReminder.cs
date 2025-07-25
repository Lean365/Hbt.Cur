//===================================================================
// 项目名 : Lean.Hbt.Domain.Entities.Routine
// 文件名 : HbtVehicleReminder.cs
// 创建者 : Lean365
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 车辆提醒管理实体
//===================================================================

namespace Lean.Hbt.Domain.Entities.Routine.Vehicle
{
    /// <summary>
    /// 车辆提醒管理实体
    /// </summary>
    [SugarTable("hbt_routine_vehicle_reminder", "车辆提醒管理")]
    [SugarIndex("index_reminder_vehicle_id", nameof(VehicleId), OrderByType.Asc)]
    [SugarIndex("index_reminder_type", nameof(ReminderType), OrderByType.Asc)]
    [SugarIndex("index_reminder_status", nameof(Status), OrderByType.Asc)]
    [SugarIndex("index_reminder_due_date", nameof(DueDate), OrderByType.Asc)]
    public class HbtVehicleReminder : HbtBaseEntity
    {
        /// <summary>
        /// 车辆ID
        /// </summary>
        [SugarColumn(ColumnName = "vehicle_id", ColumnDescription = "车辆ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long VehicleId { get; set; }

        /// <summary>
        /// 提醒类型（0：保险到期，1：年检到期，2：维保到期，3：油费不足，4：其他）
        /// </summary>
        [SugarColumn(ColumnName = "reminder_type", ColumnDescription = "提醒类型", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
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
        /// 提醒状态（0：未提醒，1：已提醒，2：已处理，3：已忽略）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "提醒状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 到期日期
        /// </summary>
        [SugarColumn(ColumnName = "due_date", ColumnDescription = "到期日期", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime DueDate { get; set; }

        /// <summary>
        /// 提醒日期
        /// </summary>
        [SugarColumn(ColumnName = "reminder_date", ColumnDescription = "提醒日期", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime ReminderDate { get; set; }

        /// <summary>
        /// 提前提醒天数
        /// </summary>
        [SugarColumn(ColumnName = "advance_days", ColumnDescription = "提前提醒天数", IsNullable = false, DefaultValue = "7", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int AdvanceDays { get; set; }

        /// <summary>
        /// 提醒级别（0：低，1：中，2：高，3：紧急）
        /// </summary>
        [SugarColumn(ColumnName = "priority_level", ColumnDescription = "提醒级别", IsNullable = false, DefaultValue = "1", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int PriorityLevel { get; set; }

        /// <summary>
        /// 提醒方式（0：系统消息，1：邮件，2：短信，3：微信，4：全部）
        /// </summary>
        [SugarColumn(ColumnName = "reminder_method", ColumnDescription = "提醒方式", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
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
        /// 提醒间隔（小时）
        /// </summary>
        [SugarColumn(ColumnName = "remind_interval", ColumnDescription = "提醒间隔", IsNullable = false, DefaultValue = "24", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int RemindInterval { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        [SugarColumn(ColumnName = "handle_time", ColumnDescription = "处理时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? HandleTime { get; set; }

        /// <summary>
        /// 处理人ID
        /// </summary>
        [SugarColumn(ColumnName = "handle_user_id", ColumnDescription = "处理人ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? HandleUserId { get; set; }

        /// <summary>
        /// 处理人姓名
        /// </summary>
        [SugarColumn(ColumnName = "handle_user_name", ColumnDescription = "处理人姓名", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? HandleUserName { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        [SugarColumn(ColumnName = "handle_result", ColumnDescription = "处理结果", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? HandleResult { get; set; }

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
        /// 相关业务ID
        /// </summary>
        [SugarColumn(ColumnName = "related_business_id", ColumnDescription = "相关业务ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? RelatedBusinessId { get; set; }

        /// <summary>
        /// 相关业务类型
        /// </summary>
        [SugarColumn(ColumnName = "related_business_type", ColumnDescription = "相关业务类型", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? RelatedBusinessType { get; set; }

        /// <summary>
        /// 相关业务标题
        /// </summary>
        [SugarColumn(ColumnName = "related_business_title", ColumnDescription = "相关业务标题", Length = 200, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? RelatedBusinessTitle { get; set; }

        /// <summary>
        /// 是否重复提醒（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "is_repeat", ColumnDescription = "是否重复提醒", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int IsRepeat { get; set; }

        /// <summary>
        /// 重复周期（天）
        /// </summary>
        [SugarColumn(ColumnName = "repeat_cycle", ColumnDescription = "重复周期", IsNullable = true, ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int? RepeatCycle { get; set; }

        /// <summary>
        /// 下次提醒时间
        /// </summary>
        [SugarColumn(ColumnName = "next_remind_time", ColumnDescription = "下次提醒时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? NextRemindTime { get; set; }

    }
}