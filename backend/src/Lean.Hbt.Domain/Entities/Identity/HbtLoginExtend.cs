#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginExtend.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 登录扩展实体类
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Identity
{
    /// <summary>
    /// 登录扩展实体
    /// </summary>
    [SugarTable("hbt_id_login_extend", "登录扩展表")]
    [SugarIndex("ix_login_tenant_user", nameof(TenantId), OrderByType.Asc, nameof(UserId), OrderByType.Asc, nameof(FirstLoginDeviceId), OrderByType.Asc,true)]
    public class HbtLoginExtend : HbtBaseEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long UserId { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long TenantId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [SugarColumn(ColumnName = "role_id", ColumnDescription = "角色ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? RoleId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [SugarColumn(ColumnName = "dept_id", ColumnDescription = "部门ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? DeptId { get; set; }

        /// <summary>
        /// 岗位ID
        /// </summary>
        [SugarColumn(ColumnName = "post_id", ColumnDescription = "岗位ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? PostId { get; set; }

        /// <summary>
        /// 登录类型（0普通 1短信 2邮箱 3微信 4QQ 5GitHub）
        /// </summary>
        [SugarColumn(ColumnName = "login_type", ColumnDescription = "登录类型（0普通 1短信 2邮箱 3微信 4QQ 5GitHub）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int LoginType { get; set; } = 0;

        /// <summary>
        /// 登录来源（0Web 1App 2小程序 3其他）
        /// </summary>
        [SugarColumn(ColumnName = "login_source", ColumnDescription = "登录来源（0Web 1App 2小程序 3其他）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int LoginSource { get; set; } = 0;

        /// <summary>
        /// 登录状态（0离线 1在线）
        /// </summary>
        [SugarColumn(ColumnName = "login_status", ColumnDescription = "登录状态（0离线 1在线）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int LoginStatus { get; set; } = 0;

        /// <summary>
        /// 登录提供者（0系统 1微信 2钉钉 3企业微信）
        /// </summary>
        [SugarColumn(ColumnName = "login_provider", ColumnDescription = "登录提供者（0系统 1微信 2钉钉 3企业微信）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int LoginProvider { get; set; } = 0;

        /// <summary>
        /// 提供者Key
        /// </summary>
        [SugarColumn(ColumnName = "provider_key", ColumnDescription = "提供者Key", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string ProviderKey { get; set; } = string.Empty;

        /// <summary>
        /// 提供者显示名称
        /// </summary>
        [SugarColumn(ColumnName = "provider_display_name", ColumnDescription = "提供者显示名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string ProviderDisplayName { get; set; } = string.Empty;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态（0正常 1停用）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;

        /// <summary>
        /// 首次登录时间
        /// </summary>
        [SugarColumn(ColumnName = "first_login_time", ColumnDescription = "首次登录时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? FirstLoginTime { get; set; }

        /// <summary>
        /// 首次登录IP
        /// </summary>
        [SugarColumn(ColumnName = "first_login_ip", ColumnDescription = "首次登录IP", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FirstLoginIp { get; set; }

        /// <summary>
        /// 首次登录地点
        /// </summary>
        [SugarColumn(ColumnName = "first_login_location", ColumnDescription = "首次登录地点", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FirstLoginLocation { get; set; }

        /// <summary>
        /// 首次登录设备ID
        /// </summary>
        [SugarColumn(ColumnName = "first_login_device_id", ColumnDescription = "首次登录设备ID", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FirstLoginDeviceId { get; set; }

        /// <summary>
        /// 首次登录设备类型（0PC 1Android 2iOS 3MacOS 4Linux 5其他）
        /// </summary>
        [SugarColumn(ColumnName = "first_login_device_type", ColumnDescription = "首次登录设备类型（0PC 1Android 2iOS 3MacOS 4Linux 5其他）", ColumnDataType = "int", IsNullable = true)]
        public int? FirstLoginDeviceType { get; set; }

        /// <summary>
        /// 首次登录浏览器类型（0Chrome 1Firefox 2Edge 3Safari 4IE 5其他）
        /// </summary>
        [SugarColumn(ColumnName = "first_login_browser", ColumnDescription = "首次登录浏览器类型（0Chrome 1Firefox 2Edge 3Safari 4IE 5其他）", ColumnDataType = "int", IsNullable = true)]
        public int? FirstLoginBrowser { get; set; }

        /// <summary>
        /// 首次登录操作系统类型（0Windows 1Android 2iOS 3MacOS 4Linux 5其他）
        /// </summary>
        [SugarColumn(ColumnName = "first_login_os", ColumnDescription = "首次登录操作系统类型（0Windows 1Android 2iOS 3MacOS 4Linux 5其他）", ColumnDataType = "int", IsNullable = true)]
        public int? FirstLoginOs { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        [SugarColumn(ColumnName = "last_login_time", ColumnDescription = "最后登录时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        [SugarColumn(ColumnName = "last_login_ip", ColumnDescription = "最后登录IP", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LastLoginIp { get; set; }

        /// <summary>
        /// 最后登录地点
        /// </summary>
        [SugarColumn(ColumnName = "last_login_location", ColumnDescription = "最后登录地点", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LastLoginLocation { get; set; }

        /// <summary>
        /// 最后登录设备ID
        /// </summary>
        [SugarColumn(ColumnName = "last_login_device_id", ColumnDescription = "最后登录设备ID", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LastLoginDeviceId { get; set; }

        /// <summary>
        /// 最后登录设备类型（0PC 1Android 2iOS 3MacOS 4Linux 5其他）
        /// </summary>
        [SugarColumn(ColumnName = "last_login_device_type", ColumnDescription = "最后登录设备类型（0PC 1Android 2iOS 3MacOS 4Linux 5其他）", ColumnDataType = "int", IsNullable = true)]
        public int? LastLoginDeviceType { get; set; }

        /// <summary>
        /// 最后登录浏览器类型（0Chrome 1Firefox 2Edge 3Safari 4IE 5其他）
        /// </summary>
        [SugarColumn(ColumnName = "last_login_browser", ColumnDescription = "最后登录浏览器类型（0Chrome 1Firefox 2Edge 3Safari 4IE 5其他）", ColumnDataType = "int", IsNullable = true)]
        public int? LastLoginBrowser { get; set; }

        /// <summary>
        /// 最后登录操作系统类型（0Windows 1Android 2iOS 3MacOS 4Linux 5其他）
        /// </summary>
        [SugarColumn(ColumnName = "last_login_os", ColumnDescription = "最后登录操作系统类型（0Windows 1Android 2iOS 3MacOS 4Linux 5其他）", ColumnDataType = "int", IsNullable = true)]
        public int? LastLoginOs { get; set; }

        /// <summary>
        /// 最后离线时间
        /// </summary>
        [SugarColumn(ColumnName = "last_offline_time", ColumnDescription = "最后离线时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? LastOfflineTime { get; set; }

        /// <summary>
        /// 今日在线时段(JSON格式,例如:["09:00-12:00","14:00-18:00"])
        /// </summary>
        [SugarColumn(ColumnName = "today_online_periods", ColumnDescription = "今日在线时段", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TodayOnlinePeriods { get; set; }

        /// <summary>
        /// 总登录次数
        /// </summary>
        [SugarColumn(ColumnName = "login_count", ColumnDescription = "总登录次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int LoginCount { get; set; } = 0;

        /// <summary>
        /// 连续登录天数
        /// </summary>
        [SugarColumn(ColumnName = "continuous_login_days", ColumnDescription = "连续登录天数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ContinuousLoginDays { get; set; } = 0;

        /// <summary>
        /// 用户导航属性
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(UserId))]
        public HbtUser? User { get; set; }

        /// <summary>
        /// 租户导航属性
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(TenantId))]
        public HbtTenant? Tenant { get; set; }
    }
} 