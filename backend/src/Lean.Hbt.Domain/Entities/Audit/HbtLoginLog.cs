#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginLogEntity.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 17:00
// 版本号 : V.0.0.1
// 描述    : 登录日志实体
//===================================================================

using Lean.Hbt.Common.Enums;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Audit
{
    /// <summary>
    /// 登录日志实体
    /// </summary>
    [SugarTable("hbt_mon_login_log", "登录日志表")]
    [SugarIndex("ix_tenant_login", nameof(TenantId), OrderByType.Asc)]
    public class HbtLoginLog : HbtBaseEntity
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        [SugarColumn(ColumnName = "log_level", ColumnDescription = "日志级别", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public HbtLogLevel LogLevel { get; set; } = HbtLogLevel.Info;

        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long UserId { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? TenantId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// IP地址
        /// </summary>
        [SugarColumn(ColumnName = "ip_address", ColumnDescription = "IP地址", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string IpAddress { get; set; } = string.Empty;

        /// <summary>
        /// 用户代理
        /// </summary>
        [SugarColumn(ColumnName = "user_agent", ColumnDescription = "用户代理", Length = 500, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string UserAgent { get; set; } = string.Empty;

        /// <summary>
        /// 是否成功（0失败 1成功）
        /// </summary>
        [SugarColumn(ColumnName = "success", ColumnDescription = "是否成功（0失败 1成功）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Success { get; set; } = 0;

        /// <summary>
        /// 消息
        /// </summary>
        [SugarColumn(ColumnName = "message", ColumnDescription = "消息", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Message { get; set; }
    }
}