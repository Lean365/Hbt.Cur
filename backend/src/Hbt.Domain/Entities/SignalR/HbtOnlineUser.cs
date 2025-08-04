#nullable enable

using SqlSugar;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOnlineUser.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述    : 在线用户实体
//===================================================================

namespace Hbt.Domain.Entities.SignalR
{
    /// <summary>
    /// 在线用户实体
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    [SugarTable("hbt_signalr_user", "在线用户表")]
    [SugarIndex("ix_user_connection", nameof(UserId), OrderByType.Asc, nameof(ConnectionId), OrderByType.Asc, nameof(DeviceId), OrderByType.Asc, true)]
    public class HbtOnlineUser : HbtBaseEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long UserId { get; set; }

        /// <summary>
        /// 用户组ID
        /// </summary>
        [SugarColumn(ColumnName = "group_id", ColumnDescription = "用户组ID", ColumnDataType = "bigint", IsNullable = false)]
        public long GroupId { get; set; }

        /// <summary>
        /// 连接ID
        /// </summary>
        [SugarColumn(ColumnName = "connection_id", ColumnDescription = "SignalR连接ID", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? ConnectionId { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        [SugarColumn(ColumnName = "device_id", ColumnDescription = "设备ID", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? DeviceId { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        [SugarColumn(ColumnName = "client_ip", ColumnDescription = "客户端IP", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? ClientIp { get; set; }

        /// <summary>
        /// 浏览器信息
        /// </summary>
        [SugarColumn(ColumnName = "user_agent", ColumnDescription = "浏览器信息", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? UserAgent { get; set; }

        /// <summary>
        /// 最后活动时间
        /// </summary>
        [SugarColumn(ColumnName = "last_activity", ColumnDescription = "最后活动时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime LastActivity { get; set; }

        /// <summary>
        /// 用户在线状态 0=在线 1=离线
        /// </summary>
        [SugarColumn(ColumnName = "online_status", ColumnDescription = "用户在线状态", ColumnDataType = "int", IsNullable = false)]
        public int OnlineStatus { get; set; }

        /// <summary>
        /// 最后心跳时间
        /// </summary>
        [SugarColumn(ColumnName = "last_heartbeat", ColumnDescription = "最后心跳时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime LastHeartbeat { get; set; }
    }
}