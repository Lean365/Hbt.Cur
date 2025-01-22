#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginExtend.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V1.0.0
// 描述    : 登录扩展信息实体
//===================================================================

using SqlSugar;
using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.Domain.Entities.Identity
{
    /// <summary>
    /// 登录扩展信息实体
    /// </summary>
    [SugarTable("hbt_id_login_extend", "登录扩展信息表")]
    [SugarIndex("ix_login_extend_user", nameof(UserId), OrderByType.Asc)]
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
        [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? TenantId { get; set; }

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
        /// 登录类型
        /// </summary>
        [SugarColumn(ColumnName = "login_type", ColumnDescription = "登录类型", ColumnDataType = "int", IsNullable = false)]
        public HbtLoginType LoginType { get; set; } = HbtLoginType.Normal;

        /// <summary>
        /// 登录来源
        /// </summary>
        [SugarColumn(ColumnName = "login_source", ColumnDescription = "登录来源", ColumnDataType = "int", IsNullable = false)]
        public HbtLoginSource LoginSource { get; set; } = HbtLoginSource.Web;

        /// <summary>
        /// 登录状态
        /// </summary>
        [SugarColumn(ColumnName = "login_status", ColumnDescription = "登录状态", ColumnDataType = "int", IsNullable = false)]
        public HbtLoginStatus LoginStatus { get; set; } = HbtLoginStatus.Offline;

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
        /// 首次登录设备类型
        /// </summary>
        [SugarColumn(ColumnName = "first_login_device_type", ColumnDescription = "首次登录设备类型", ColumnDataType = "int", IsNullable = true)]
        public HbtDeviceType? FirstLoginDeviceType { get; set; }

        /// <summary>
        /// 首次登录浏览器类型
        /// </summary>
        [SugarColumn(ColumnName = "first_login_browser", ColumnDescription = "首次登录浏览器类型", ColumnDataType = "int", IsNullable = true)]
        public HbtBrowserType? FirstLoginBrowser { get; set; }

        /// <summary>
        /// 首次登录操作系统类型
        /// </summary>
        [SugarColumn(ColumnName = "first_login_os", ColumnDescription = "首次登录操作系统类型", ColumnDataType = "int", IsNullable = true)]
        public HbtOsType? FirstLoginOs { get; set; }

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
        /// 最后登录设备类型
        /// </summary>
        [SugarColumn(ColumnName = "last_login_device_type", ColumnDescription = "最后登录设备类型", ColumnDataType = "int", IsNullable = true)]
        public HbtDeviceType? LastLoginDeviceType { get; set; }

        /// <summary>
        /// 最后登录浏览器类型
        /// </summary>
        [SugarColumn(ColumnName = "last_login_browser", ColumnDescription = "最后登录浏览器类型", ColumnDataType = "int", IsNullable = true)]
        public HbtBrowserType? LastLoginBrowser { get; set; }

        /// <summary>
        /// 最后登录操作系统类型
        /// </summary>
        [SugarColumn(ColumnName = "last_login_os", ColumnDescription = "最后登录操作系统类型", ColumnDataType = "int", IsNullable = true)]
        public HbtOsType? LastLoginOs { get; set; }

        /// <summary>
        /// 最后离线时间
        /// </summary>
        [SugarColumn(ColumnName = "last_offline_time", ColumnDescription = "最后离线时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? LastOfflineTime { get; set; }

        /// <summary>
        /// 今日在线时段(JSON格式,例如:["09:00-12:00","14:00-18:00"])
        /// </summary>
        [SugarColumn(ColumnName = "today_online_periods", ColumnDescription = "今日在线时段", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
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
    }
} 