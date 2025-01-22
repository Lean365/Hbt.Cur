//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOnlineUser.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V1.0.0
// 描述    : 在线用户实体
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.RealTime
{
    /// <summary>
    /// 在线用户实体
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    [SugarTable("hbt_online_user", "在线用户表")]
    [SugarIndex("ix_tenant_user", nameof(TenantId), OrderByType.Asc, nameof(UserId), OrderByType.Asc, true)]
    public class HbtOnlineUser : HbtBaseEntity
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long TenantId { get; set; }

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
        [SugarColumn(ColumnName = "connection_id", ColumnDescription = "SignalR连接ID", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, IsPrimaryKey = true)]
        public string ConnectionId { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        [SugarColumn(ColumnName = "client_ip", ColumnDescription = "客户端IP", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ClientIp { get; set; }

        /// <summary>
        /// 浏览器信息
        /// </summary>
        [SugarColumn(ColumnName = "user_agent", ColumnDescription = "浏览器信息", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string UserAgent { get; set; }

        /// <summary>
        /// 最后活动时间
        /// </summary>
        [SugarColumn(ColumnName = "last_activity", ColumnDescription = "最后活动时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime LastActivity { get; set; }
    }
} 