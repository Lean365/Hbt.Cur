//===================================================================
// 项目名 : Hbt.Cur.Domain.Entities.Routine
// 文件名 : HbtMeetingSignIn.cs
// 创建者 : Lean365
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 会议签到管理实体
//===================================================================

using Hbt.Cur.Domain.Entities.Identity;
using SqlSugar;

namespace Hbt.Cur.Domain.Entities.Routine.Metting
{
    /// <summary>
    /// 会议签到管理实体
    /// </summary>
    [SugarTable("hbt_routine_meeting_sign_in", "会议签到管理")]
    [SugarIndex("index_sign_in_meeting_id", nameof(MeetingId), OrderByType.Asc)]
    [SugarIndex("index_sign_in_booking_id", nameof(BookingId), OrderByType.Asc)]
    [SugarIndex("index_sign_in_user_id", nameof(UserId), OrderByType.Asc)]
    [SugarIndex("index_sign_in_time", nameof(SignInTime), OrderByType.Asc)]
    public class HbtMeetingSignIn : HbtBaseEntity
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
        /// 用户ID
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long UserId { get; set; }

        /// <summary>
        /// 签到时间
        /// </summary>
        [SugarColumn(ColumnName = "sign_in_time", ColumnDescription = "签到时间", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime SignInTime { get; set; }

        /// <summary>
        /// 签到状态（0：正常签到，1：迟到，2：早退，3：缺勤，4：请假）
        /// </summary>
        [SugarColumn(ColumnName = "sign_in_status", ColumnDescription = "签到状态（0：正常签到，1：迟到，2：早退，3：缺勤，4：请假）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int SignInStatus { get; set; }

        /// <summary>
        /// 签到方式（0：手动签到，1：二维码签到，2：人脸识别，3：指纹签到，4：刷卡签到）
        /// </summary>
        [SugarColumn(ColumnName = "sign_in_method", ColumnDescription = "签到方式（0：手动签到，1：二维码签到，2：人脸识别，3：指纹签到，4：刷卡签到）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int SignInMethod { get; set; }

        /// <summary>
        /// 签到位置
        /// </summary>
        [SugarColumn(ColumnName = "sign_in_location", ColumnDescription = "签到位置", Length = 200, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? SignInLocation { get; set; }

        /// <summary>
        /// 签到IP地址
        /// </summary>
        [SugarColumn(ColumnName = "sign_in_ip", ColumnDescription = "签到IP地址", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? SignInIp { get; set; }

        /// <summary>
        /// 签到设备
        /// </summary>
        [SugarColumn(ColumnName = "sign_in_device", ColumnDescription = "签到设备", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? SignInDevice { get; set; }

        /// <summary>
        /// 签到备注
        /// </summary>
        [SugarColumn(ColumnName = "sign_in_remarks", ColumnDescription = "签到备注", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? SignInRemarks { get; set; }

        /// <summary>
        /// 签退时间
        /// </summary>
        [SugarColumn(ColumnName = "sign_out_time", ColumnDescription = "签退时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? SignOutTime { get; set; }

        /// <summary>
        /// 签退方式（0：手动签退，1：自动签退，2：强制签退）
        /// </summary>
        [SugarColumn(ColumnName = "sign_out_method", ColumnDescription = "签退方式（0：手动签退，1：自动签退，2：强制签退）", IsNullable = true, ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int? SignOutMethod { get; set; }

        /// <summary>
        /// 签退位置
        /// </summary>
        [SugarColumn(ColumnName = "sign_out_location", ColumnDescription = "签退位置", Length = 200, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? SignOutLocation { get; set; }

        /// <summary>
        /// 签退IP地址
        /// </summary>
        [SugarColumn(ColumnName = "sign_out_ip", ColumnDescription = "签退IP地址", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? SignOutIp { get; set; }

        /// <summary>
        /// 签退设备
        /// </summary>
        [SugarColumn(ColumnName = "sign_out_device", ColumnDescription = "签退设备", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? SignOutDevice { get; set; }

        /// <summary>
        /// 签退备注
        /// </summary>
        [SugarColumn(ColumnName = "sign_out_remarks", ColumnDescription = "签退备注", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? SignOutRemarks { get; set; }

        /// <summary>
        /// 参会时长（分钟）
        /// </summary>
        [SugarColumn(ColumnName = "attendance_duration", ColumnDescription = "参会时长（分钟）", IsNullable = true, ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int? AttendanceDuration { get; set; }

        /// <summary>
        /// 是否有效签到（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "is_valid", ColumnDescription = "是否有效签到（0：否，1：是）", IsNullable = false, DefaultValue = "1", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int IsValid { get; set; }

        /// <summary>
        /// 无效原因
        /// </summary>
        [SugarColumn(ColumnName = "invalid_reason", ColumnDescription = "无效原因", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? InvalidReason { get; set; }

        /// <summary>
        /// 照片路径
        /// </summary>
        [SugarColumn(ColumnName = "photo_path", ColumnDescription = "照片路径", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? PhotoPath { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "remarks", ColumnDescription = "备注", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Remarks { get; set; }
    }
} 