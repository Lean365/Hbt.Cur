//===================================================================
// 项目名 : Hbt.Cur.Domain.Entities.Routine
// 文件名 : HbtMeetingBooking.cs
// 创建者 : Lean365
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 会议预约管理实体
//===================================================================

using Hbt.Cur.Domain.Entities.Identity;
using SqlSugar;

namespace Hbt.Cur.Domain.Entities.Routine.Metting
{
    /// <summary>
    /// 会议预约管理实体
    /// </summary>
    [SugarTable("hbt_routine_meeting_booking", "会议预约管理")]
    [SugarIndex("index_booking_room_id", nameof(RoomId), OrderByType.Asc)]
    [SugarIndex("index_booking_user_id", nameof(UserId), OrderByType.Asc)]
    [SugarIndex("index_booking_start_time", nameof(StartTime), OrderByType.Asc)]
    [SugarIndex("index_booking_status", nameof(Status), OrderByType.Asc)]
    public class HbtMeetingBooking : HbtBaseEntity
    {
        /// <summary>
        /// 会议室ID
        /// </summary>
        [SugarColumn(ColumnName = "room_id", ColumnDescription = "会议室ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long RoomId { get; set; }

        /// <summary>
        /// 预约人ID
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "预约人ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long UserId { get; set; }

        /// <summary>
        /// 预约标题
        /// </summary>
        [SugarColumn(ColumnName = "booking_title", ColumnDescription = "预约标题", Length = 200, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string BookingTitle { get; set; } = string.Empty;

        /// <summary>
        /// 预约内容
        /// </summary>
        [SugarColumn(ColumnName = "booking_content", ColumnDescription = "预约内容", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? BookingContent { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [SugarColumn(ColumnName = "start_time", ColumnDescription = "开始时间", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [SugarColumn(ColumnName = "end_time", ColumnDescription = "结束时间", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 预约状态（0：待审批，1：已批准，2：已拒绝，3：已取消，4：已完成）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "预约状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 预约类型（0：个人预约，1：部门预约，2：公司预约，3：外部预约）
        /// </summary>
        [SugarColumn(ColumnName = "booking_type", ColumnDescription = "预约类型", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int BookingType { get; set; }

        /// <summary>
        /// 参与人数
        /// </summary>
        [SugarColumn(ColumnName = "participant_count", ColumnDescription = "参与人数", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int ParticipantCount { get; set; }

        /// <summary>
        /// 参与人列表
        /// </summary>
        [SugarColumn(ColumnName = "participants", ColumnDescription = "参与人列表", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Participants { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [SugarColumn(ColumnName = "contact_person", ColumnDescription = "联系人", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ContactPerson { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [SugarColumn(ColumnName = "contact_phone", ColumnDescription = "联系电话", Length = 20, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ContactPhone { get; set; }

        /// <summary>
        /// 是否需要设备（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "need_equipment", ColumnDescription = "是否需要设备", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int NeedEquipment { get; set; }

        /// <summary>
        /// 设备需求
        /// </summary>
        [SugarColumn(ColumnName = "equipment_requirements", ColumnDescription = "设备需求", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? EquipmentRequirements { get; set; }

        /// <summary>
        /// 是否需要茶水服务（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "need_tea_service", ColumnDescription = "是否需要茶水服务", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int NeedTeaService { get; set; }

        /// <summary>
        /// 茶水服务需求
        /// </summary>
        [SugarColumn(ColumnName = "tea_service_requirements", ColumnDescription = "茶水服务需求", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? TeaServiceRequirements { get; set; }

        /// <summary>
        /// 审批人ID
        /// </summary>
        [SugarColumn(ColumnName = "approver_id", ColumnDescription = "审批人ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? ApproverId { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        [SugarColumn(ColumnName = "approval_time", ColumnDescription = "审批时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ApprovalTime { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        [SugarColumn(ColumnName = "approval_remarks", ColumnDescription = "审批意见", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ApprovalRemarks { get; set; }

        /// <summary>
        /// 取消原因
        /// </summary>
        [SugarColumn(ColumnName = "cancel_reason", ColumnDescription = "取消原因", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? CancelReason { get; set; }

        /// <summary>
        /// 取消时间
        /// </summary>
        [SugarColumn(ColumnName = "cancel_time", ColumnDescription = "取消时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? CancelTime { get; set; }

        /// <summary>
        /// 取消人ID
        /// </summary>
        [SugarColumn(ColumnName = "cancel_user_id", ColumnDescription = "取消人ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? CancelUserId { get; set; }

        /// <summary>
        /// 实际开始时间
        /// </summary>
        [SugarColumn(ColumnName = "actual_start_time", ColumnDescription = "实际开始时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ActualStartTime { get; set; }

        /// <summary>
        /// 实际结束时间
        /// </summary>
        [SugarColumn(ColumnName = "actual_end_time", ColumnDescription = "实际结束时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ActualEndTime { get; set; }

        /// <summary>
        /// 签到人数
        /// </summary>
        [SugarColumn(ColumnName = "sign_in_count", ColumnDescription = "签到人数", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int SignInCount { get; set; }

        /// <summary>
        /// 会议纪要
        /// </summary>
        [SugarColumn(ColumnName = "meeting_minutes", ColumnDescription = "会议纪要", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? MeetingMinutes { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "remarks", ColumnDescription = "备注", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Remarks { get; set; }

        /// <summary>
        /// 关联会议ID
        /// </summary>
        [SugarColumn(ColumnName = "meeting_id", ColumnDescription = "关联会议ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? MeetingId { get; set; }

        /// <summary>
        /// 重复预约类型（0：单次，1：每天，2：每周，3：每月）
        /// </summary>
        [SugarColumn(ColumnName = "repeat_type", ColumnDescription = "重复预约类型", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int RepeatType { get; set; }

        /// <summary>
        /// 重复结束时间
        /// </summary>
        [SugarColumn(ColumnName = "repeat_end_time", ColumnDescription = "重复结束时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? RepeatEndTime { get; set; }

        /// <summary>
        /// 重复间隔（天）
        /// </summary>
        [SugarColumn(ColumnName = "repeat_interval", ColumnDescription = "重复间隔（天）", IsNullable = false, DefaultValue = "1", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int RepeatInterval { get; set; }

        /// <summary>
        /// 重复星期（1-7，逗号分隔）
        /// </summary>
        [SugarColumn(ColumnName = "repeat_weekdays", ColumnDescription = "重复星期", IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? RepeatWeekdays { get; set; }

        /// <summary>
        /// 重复日期（1-31，逗号分隔）
        /// </summary>
        [SugarColumn(ColumnName = "repeat_days", ColumnDescription = "重复日期", IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? RepeatDays { get; set; }
    }
} 