//===================================================================
// 项目名 : Hbt.Cur.Domain.Entities.Routine
// 文件名 : HbtMeetingRoom.cs
// 创建者 : Lean365
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 会议室管理实体
//===================================================================

using Hbt.Cur.Domain.Entities.Identity;
using SqlSugar;

namespace Hbt.Cur.Domain.Entities.Routine.Metting
{
    /// <summary>
    /// 会议室管理实体
    /// </summary>
    [SugarTable("hbt_routine_meeting_room", "会议室管理")]
    [SugarIndex("index_room_name", nameof(RoomName), OrderByType.Asc)]
    [SugarIndex("index_room_type", nameof(RoomType), OrderByType.Asc)]
    [SugarIndex("index_room_status", nameof(Status), OrderByType.Asc)]
    public class HbtMeetingRoom : HbtBaseEntity
    {
        /// <summary>
        /// 会议室名称
        /// </summary>
        [SugarColumn(ColumnName = "room_name", ColumnDescription = "会议室名称", Length = 100, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string RoomName { get; set; } = string.Empty;

        /// <summary>
        /// 会议室编号
        /// </summary>
        [SugarColumn(ColumnName = "room_code", ColumnDescription = "会议室编号", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string RoomCode { get; set; } = string.Empty;

        /// <summary>
        /// 会议室类型（0：小型会议室，1：中型会议室，2：大型会议室，3：培训室，4：视频会议室）
        /// </summary>
        [SugarColumn(ColumnName = "room_type", ColumnDescription = "会议室类型（0：小型会议室，1：中型会议室，2：大型会议室，3：培训室，4：视频会议室）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int RoomType { get; set; }

        /// <summary>
        /// 会议室状态（0：空闲，1：使用中，2：维护中，3：停用）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "会议室状态（0：空闲，1：使用中，2：维护中，3：停用）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 容纳人数
        /// </summary>
        [SugarColumn(ColumnName = "capacity", ColumnDescription = "容纳人数", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Capacity { get; set; }

        /// <summary>
        /// 位置/楼层
        /// </summary>
        [SugarColumn(ColumnName = "location", ColumnDescription = "位置/楼层", Length = 200, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Location { get; set; }

        /// <summary>
        /// 设备配置
        /// </summary>
        [SugarColumn(ColumnName = "equipment", ColumnDescription = "设备配置", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Equipment { get; set; }

        /// <summary>
        /// 会议室描述
        /// </summary>
        [SugarColumn(ColumnName = "description", ColumnDescription = "会议室描述", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Description { get; set; }

        /// <summary>
        /// 是否支持视频会议（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "support_video", ColumnDescription = "是否支持视频会议（0：否，1：是）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int SupportVideo { get; set; }

        /// <summary>
        /// 是否支持投影（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "support_projector", ColumnDescription = "是否支持投影（0：否，1：是）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int SupportProjector { get; set; }

        /// <summary>
        /// 是否支持白板（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "support_whiteboard", ColumnDescription = "是否支持白板（0：否，1：是）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int SupportWhiteboard { get; set; }

        /// <summary>
        /// 是否支持网络（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "support_network", ColumnDescription = "是否支持网络（0：否，1：是）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int SupportNetwork { get; set; }

        /// <summary>
        /// 开放时间（开始）
        /// </summary>
        [SugarColumn(ColumnName = "open_time_start", ColumnDescription = "开放时间（开始）", IsNullable = true, ColumnDataType = "time", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public TimeSpan? OpenTimeStart { get; set; }

        /// <summary>
        /// 开放时间（结束）
        /// </summary>
        [SugarColumn(ColumnName = "open_time_end", ColumnDescription = "开放时间（结束）", IsNullable = true, ColumnDataType = "time", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public TimeSpan? OpenTimeEnd { get; set; }

        /// <summary>
        /// 预约提前时间（小时）
        /// </summary>
        [SugarColumn(ColumnName = "advance_booking_hours", ColumnDescription = "预约提前时间（小时）", IsNullable = false, DefaultValue = "24", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int AdvanceBookingHours { get; set; }

        /// <summary>
        /// 最大预约时长（小时）
        /// </summary>
        [SugarColumn(ColumnName = "max_booking_hours", ColumnDescription = "最大预约时长（小时）", IsNullable = false, DefaultValue = "4", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int MaxBookingHours { get; set; }

        /// <summary>
        /// 是否需要审批（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "need_approval", ColumnDescription = "是否需要审批（0：否，1：是）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int NeedApproval { get; set; }

        /// <summary>
        /// 审批人ID
        /// </summary>
        [SugarColumn(ColumnName = "approver_id", ColumnDescription = "审批人ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? ApproverId { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int SortOrder { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "remarks", ColumnDescription = "备注", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Remarks { get; set; }
    }
} 