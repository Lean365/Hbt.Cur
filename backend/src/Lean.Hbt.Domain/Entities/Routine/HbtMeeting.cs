//===================================================================
// 项目名 : Lean.Hbt.Domain.Entities.Routine
// 文件名 : HbtMeeting.cs
// 创建者 : Lean365
// 创建时间: 2024-01-26 14:30
// 版本号 : V1.0.0
// 描述   : 会议管理实体
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Routine
{
    /// <summary>
    /// 会议管理实体
    /// </summary>
    [SugarTable("hbt_routine_meeting", "会议管理")]
    [SugarIndex("index_meeting_title", nameof(Title), OrderByType.Asc)]
    [SugarIndex("index_meeting_type", nameof(MeetingType), OrderByType.Asc)]
    [SugarIndex("index_meeting_status", nameof(Status), OrderByType.Asc)]
    public class HbtMeeting : HbtBaseEntity
    {
        /// <summary>
        /// 会议标题
        /// </summary>
        [SugarColumn(ColumnName = "title", ColumnDescription = "会议标题", Length = 200, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 会议内容
        /// </summary>
        [SugarColumn(ColumnName = "content", ColumnDescription = "会议内容", IsNullable = false, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string Content { get; set; } = string.Empty;

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
        /// 会议类型（0：普通会议，1：重要会议，2：紧急会议）
        /// </summary>
        [SugarColumn(ColumnName = "meeting_type", ColumnDescription = "会议类型（0：普通会议，1：重要会议，2：紧急会议）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int MeetingType { get; set; }

        /// <summary>
        /// 会议状态（0：未开始，1：进行中，2：已完成，3：已取消）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "会议状态（0：未开始，1：进行中，2：已完成，3：已取消）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 是否全天（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "is_all_day", ColumnDescription = "是否全天（0：否，1：是）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int IsAllDay { get; set; }

        /// <summary>
        /// 提醒时间（分钟）
        /// </summary>
        [SugarColumn(ColumnName = "remind_minutes", ColumnDescription = "提醒时间（分钟）", IsNullable = true, ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int? RemindMinutes { get; set; }

        /// <summary>
        /// 地点
        /// </summary>
        [SugarColumn(ColumnName = "location", ColumnDescription = "地点", Length = 200, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Location { get; set; }

        /// <summary>
        /// 参与人
        /// </summary>
        [SugarColumn(ColumnName = "participants", ColumnDescription = "参与人", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Participants { get; set; }

        /// <summary>
        /// 主持人
        /// </summary>
        [SugarColumn(ColumnName = "host", ColumnDescription = "主持人", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Host { get; set; }

        /// <summary>
        /// 会议纪要
        /// </summary>
        [SugarColumn(ColumnName = "minutes", ColumnDescription = "会议纪要", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Minutes { get; set; }

        /// <summary>
        /// 是否需要签到（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "need_sign_in", ColumnDescription = "是否需要签到（0：否，1：是）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int NeedSignIn { get; set; }

    }
}