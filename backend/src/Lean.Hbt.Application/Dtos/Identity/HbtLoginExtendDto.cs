#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginExtendDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V1.0.0
// 描述    : 登录扩展信息传输对象
//===================================================================

using System;
using System.ComponentModel.DataAnnotations;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Dtos.Identity
{
    /// <summary>
    /// 登录扩展信息传输对象
    /// </summary>
    public class HbtLoginExtendDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public long UserId { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public long? TenantId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public long? RoleId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public long? DeptId { get; set; }

        /// <summary>
        /// 岗位ID
        /// </summary>
        public long? PostId { get; set; }

        /// <summary>
        /// 登录类型
        /// </summary>
        [Required(ErrorMessage = "登录类型不能为空")]
        public HbtLoginType LoginType { get; set; } = HbtLoginType.Normal;

        /// <summary>
        /// 登录来源
        /// </summary>
        [Required(ErrorMessage = "登录来源不能为空")]
        public HbtLoginSource LoginSource { get; set; } = HbtLoginSource.Web;

        /// <summary>
        /// 登录状态
        /// </summary>
        [Required(ErrorMessage = "登录状态不能为空")]
        public HbtLoginStatus LoginStatus { get; set; } = HbtLoginStatus.Offline;

        /// <summary>
        /// 首次登录时间
        /// </summary>
        public DateTime? FirstLoginTime { get; set; }

        /// <summary>
        /// 首次登录IP
        /// </summary>
        public string? FirstLoginIp { get; set; }

        /// <summary>
        /// 首次登录地点
        /// </summary>
        public string? FirstLoginLocation { get; set; }

        /// <summary>
        /// 首次登录设备ID
        /// </summary>
        public string? FirstLoginDeviceId { get; set; }

        /// <summary>
        /// 首次登录设备类型
        /// </summary>
        public HbtDeviceType? FirstLoginDeviceType { get; set; }

        /// <summary>
        /// 首次登录浏览器类型
        /// </summary>
        public HbtBrowserType? FirstLoginBrowser { get; set; }

        /// <summary>
        /// 首次登录操作系统类型
        /// </summary>
        public HbtOsType? FirstLoginOs { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        public string? LastLoginIp { get; set; }

        /// <summary>
        /// 最后登录地点
        /// </summary>
        public string? LastLoginLocation { get; set; }

        /// <summary>
        /// 最后登录设备ID
        /// </summary>
        public string? LastLoginDeviceId { get; set; }

        /// <summary>
        /// 最后登录设备类型
        /// </summary>
        public HbtDeviceType? LastLoginDeviceType { get; set; }

        /// <summary>
        /// 最后登录浏览器类型
        /// </summary>
        public HbtBrowserType? LastLoginBrowser { get; set; }

        /// <summary>
        /// 最后登录操作系统类型
        /// </summary>
        public HbtOsType? LastLoginOs { get; set; }

        /// <summary>
        /// 最后离线时间
        /// </summary>
        public DateTime? LastOfflineTime { get; set; }

        /// <summary>
        /// 今日在线时段(JSON格式,例如:["09:00-12:00","14:00-18:00"])
        /// </summary>
        public string? TodayOnlinePeriods { get; set; }

        /// <summary>
        /// 总登录次数
        /// </summary>
        public int LoginCount { get; set; }

        /// <summary>
        /// 连续登录天数
        /// </summary>
        public int ContinuousLoginDays { get; set; }
    }

    /// <summary>
    /// 登录扩展信息更新请求
    /// </summary>
    public class HbtLoginExtendUpdateRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public long UserId { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public long? TenantId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public long? RoleId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public long? DeptId { get; set; }

        /// <summary>
        /// 岗位ID
        /// </summary>
        public long? PostId { get; set; }

        /// <summary>
        /// 登录类型
        /// </summary>
        [Required(ErrorMessage = "登录类型不能为空")]
        public HbtLoginType LoginType { get; set; }

        /// <summary>
        /// 登录来源
        /// </summary>
        [Required(ErrorMessage = "登录来源不能为空")]
        public HbtLoginSource LoginSource { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [Required(ErrorMessage = "IP地址不能为空")]
        [MaxLength(50, ErrorMessage = "IP地址长度不能超过50个字符")]
        public string IpAddress { get; set; } = string.Empty;

        /// <summary>
        /// 地理位置
        /// </summary>
        [MaxLength(100, ErrorMessage = "地理位置长度不能超过100个字符")]
        public string? Location { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        [Required(ErrorMessage = "设备ID不能为空")]
        [MaxLength(100, ErrorMessage = "设备ID长度不能超过100个字符")]
        public string DeviceId { get; set; } = string.Empty;

        /// <summary>
        /// 设备类型
        /// </summary>
        [Required(ErrorMessage = "设备类型不能为空")]
        public HbtDeviceType DeviceType { get; set; }

        /// <summary>
        /// 浏览器类型
        /// </summary>
        [Required(ErrorMessage = "浏览器类型不能为空")]
        public HbtBrowserType BrowserType { get; set; }

        /// <summary>
        /// 操作系统类型
        /// </summary>
        [Required(ErrorMessage = "操作系统类型不能为空")]
        public HbtOsType OsType { get; set; }
    }

    /// <summary>
    /// 在线时段更新请求
    /// </summary>
    public class HbtOnlinePeriodUpdateRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public long UserId { get; set; }

        /// <summary>
        /// 在线时段(格式:"HH:mm-HH:mm")
        /// </summary>
        [Required(ErrorMessage = "在线时段不能为空")]
        [RegularExpression(@"^([0-1][0-9]|2[0-3]):[0-5][0-9]-([0-1][0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "在线时段格式不正确")]
        public string OnlinePeriod { get; set; } = string.Empty;
    }

    /// <summary>
    /// 登录扩展信息分页查询请求
    /// </summary>
    public class HbtLoginExtendPageRequest : HbtPagedQuery
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public long? TenantId { get; set; }

        /// <summary>
        /// 登录类型
        /// </summary>
        public HbtLoginType? LoginType { get; set; }

        /// <summary>
        /// 登录来源
        /// </summary>
        public HbtLoginSource? LoginSource { get; set; }

        /// <summary>
        /// 登录状态
        /// </summary>
        public HbtLoginStatus? LoginStatus { get; set; }

        /// <summary>
        /// 最后登录时间范围开始
        /// </summary>
        public DateTime? LastLoginTimeStart { get; set; }

        /// <summary>
        /// 最后登录时间范围结束
        /// </summary>
        public DateTime? LastLoginTimeEnd { get; set; }
    }

    /// <summary>
    /// 登录扩展信息导出请求
    /// </summary>
    public class HbtLoginExtendExportRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public long? TenantId { get; set; }

        /// <summary>
        /// 登录类型
        /// </summary>
        public HbtLoginType? LoginType { get; set; }

        /// <summary>
        /// 登录来源
        /// </summary>
        public HbtLoginSource? LoginSource { get; set; }

        /// <summary>
        /// 登录状态
        /// </summary>
        public HbtLoginStatus? LoginStatus { get; set; }

        /// <summary>
        /// 最后登录时间范围开始
        /// </summary>
        public DateTime? LastLoginTimeStart { get; set; }

        /// <summary>
        /// 最后登录时间范围结束
        /// </summary>
        public DateTime? LastLoginTimeEnd { get; set; }

        /// <summary>
        /// 导出字段列表
        /// </summary>
        public string[]? ExportFields { get; set; }

        /// <summary>
        /// 导出文件类型
        /// </summary>
        [Required(ErrorMessage = "导出文件类型不能为空")]
        public string FileType { get; set; } = "xlsx";
    }
} 