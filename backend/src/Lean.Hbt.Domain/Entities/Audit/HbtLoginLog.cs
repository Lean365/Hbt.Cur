#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginLogEntity.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 17:00
// 版本号 : V.0.0.1
// 描述    : 登录日志实体
//===================================================================

using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Domain.Entities.Audit
{
    /// <summary>
    /// 登录日志实体
    /// </summary>
    [SugarTable("hbt_audit_login_log", "登录日志表")]
    public class HbtLoginLog : HbtBaseEntity
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        [SugarColumn(ColumnName = "log_level", ColumnDescription = "日志级别", ColumnDataType = "int", IsNullable = false)]
        public int LogLevel { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long UserId { get; set; }

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
        /// IP地理位置
        /// </summary>
        [SugarColumn(ColumnName = "location", ColumnDescription = "IP地理位置", Length = 150, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// 用户代理
        /// </summary>
        [SugarColumn(ColumnName = "user_agent", ColumnDescription = "用户代理", Length = -1, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string UserAgent { get; set; } = string.Empty;

        /// <summary>
        /// 登录类型（0:其他，1:用户密码登录，2:短信登录，3:邮件登录，4:微信登录，5:QQ登录，6:钉钉登录，7:企业微信登录，8:飞书登录，9:支付宝登录，等等）
        /// </summary>
        [SugarColumn(ColumnName = "login_type", ColumnDescription = "登录类型", ColumnDataType = "int", IsNullable = false)]
        public HbtLoginType LoginType { get; set; }

        /// <summary>t
        /// 登录状态（0:成功，1:失败）
        /// </summary>
        [SugarColumn(ColumnName = "login_status", ColumnDescription = "登录状态", ColumnDataType = "int", IsNullable = false)]
        public HbtLoginStatus LoginStatus { get; set; }

        /// <summary>
        /// 登录来源
        /// </summary>
        [SugarColumn(ColumnName = "login_source", ColumnDescription = "登录来源", ColumnDataType = "int", IsNullable = false)]
        public int LoginSource { get; set; }

        /// <summary>
        /// 是否成功（0：成功，1：失败）
        /// </summary>
        [SugarColumn(ColumnName = "login_success", ColumnDescription = "是否成功（0失败 1成功）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int LoginSuccess { get; set; } = 0;

        /// <summary>
        /// 消息
        /// </summary>
        [SugarColumn(ColumnName = "login_message", ColumnDescription = "消息", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LoginMessage { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        [SugarColumn(ColumnName = "login_time", ColumnDescription = "登录时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime LoginTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 设备信息
        /// </summary>
        [SugarColumn(ColumnName = "device_info", ColumnDescription = "设备信息", ColumnDataType = "nvarchar(max)", IsJson = true)]
        public HbtSignalRDevice? DeviceInfo { get; set; }

        /// <summary>
        /// 环境信息
        /// </summary>
        [SugarColumn(ColumnName = "environment_info", ColumnDescription = "环境信息", ColumnDataType = "nvarchar(max)", IsJson = true)]
        public HbtSignalREnvironment? EnvironmentInfo { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        [SugarColumn(ColumnName = "device_id", ColumnDescription = "设备ID", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeviceId { get; set; }

        /// <summary>
        /// 登录设备日志
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(DeviceId))]
        public virtual HbtLoginDevLog? DeviceLog { get; set; }

        /// <summary>
        /// 环境ID（根据环境指纹生成的唯一标识）
        /// </summary>
        [SugarColumn(ColumnName = "environment_id", ColumnDescription = "环境ID", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EnvironmentId { get; set; }

        /// <summary>
        /// 登录环境日志信息
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(EnvironmentId))]
        public virtual HbtLoginEnvLog? EnvLog { get; set; }
    }
}