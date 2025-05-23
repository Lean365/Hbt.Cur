#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginEnvLogDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V1.0.0
// 描述    : 登录环境日志信息传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.Audit
{
    /// <summary>
    /// 登录环境日志信息传输对象
    /// </summary>
    public class HbtLoginEnvLogDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long LoginExtendId { get; set; }

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
        /// 登录类型（0普通 1短信 2邮箱 3微信 4QQ 5GitHub）
        /// </summary>
        [Required(ErrorMessage = "登录类型不能为空")]
        public int LoginType { get; set; } = 0;

        /// <summary>
        /// 登录来源（0Web 1App 2小程序 3其他）
        /// </summary>
        [Required(ErrorMessage = "登录来源不能为空")]
        public int LoginSource { get; set; } = 0;

        /// <summary>
        /// 登录状态（0离线 1在线）
        /// </summary>
        [Required(ErrorMessage = "登录状态不能为空")]
        public int LoginStatus { get; set; } = 0;

        /// <summary>
        /// 登录提供者（0系统 1微信 2钉钉 3企业微信）
        /// </summary>
        public int LoginProvider { get; set; } = 0;

        /// <summary>
        /// 提供者Key
        /// </summary>
        public string ProviderKey { get; set; } = string.Empty;

        /// <summary>
        /// 提供者显示名称
        /// </summary>
        public string ProviderDisplayName { get; set; } = string.Empty;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; } = 0;

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
        /// 首次登录设备类型（0PC 1Android 2iOS 3MacOS 4Linux 5其他）
        /// </summary>
        public int? FirstLoginDeviceType { get; set; }

        /// <summary>
        /// 首次登录浏览器类型（0Chrome 1Firefox 2Edge 3Safari 4IE 5其他）
        /// </summary>
        public int? FirstLoginBrowser { get; set; }

        /// <summary>
        /// 首次登录操作系统类型（0Windows 1Android 2iOS 3MacOS 4Linux 5其他）
        /// </summary>
        public int? FirstLoginOs { get; set; }

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
        /// 最后登录设备类型（0PC 1Android 2iOS 3MacOS 4Linux 5其他）
        /// </summary>
        public int? LastLoginDeviceType { get; set; }

        /// <summary>
        /// 最后登录浏览器类型（0Chrome 1Firefox 2Edge 3Safari 4IE 5其他）
        /// </summary>
        public int? LastLoginBrowser { get; set; }

        /// <summary>
        /// 最后登录操作系统类型（0Windows 1Android 2iOS 3MacOS 4Linux 5其他）
        /// </summary>
        public int? LastLoginOs { get; set; }

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

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime DeleteTime { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string DeleteBy { get; set; } = string.Empty;
    }

    /// <summary>
    /// 登录环境日志信息查询传输对象
    /// </summary>
    public class HbtLoginEnvLogQueryDto : HbtPagedQuery
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
        /// 登录类型（0普通 1短信 2邮箱 3微信 4QQ 5GitHub）
        /// </summary>
        public int? LoginType { get; set; }

        /// <summary>
        /// 登录来源（0Web 1App 2小程序 3其他）
        /// </summary>
        public int? LoginSource { get; set; }

        /// <summary>
        /// 登录状态（0离线 1在线）
        /// </summary>
        public int? LoginStatus { get; set; }

        /// <summary>
        /// 登录提供者（0系统 1微信 2钉钉 3企业微信）
        /// </summary>
        public int? LoginProvider { get; set; }

        /// <summary>
        /// 提供者Key
        /// </summary>
        [MaxLength(100, ErrorMessage = "提供者Key长度不能超过100个字符")]
        public string? ProviderKey { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int? Status { get; set; }

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
    /// 登录环境日志信息创建传输对象
    /// </summary>
    public class HbtLoginEnvLogCreateDto
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
        /// 登录类型（0普通 1短信 2邮箱 3微信 4QQ 5GitHub）
        /// </summary>
        [Required(ErrorMessage = "登录类型不能为空")]
        public int LoginType { get; set; } = 0;

        /// <summary>
        /// 登录来源（0Web 1App 2小程序 3其他）
        /// </summary>
        [Required(ErrorMessage = "登录来源不能为空")]
        public int LoginSource { get; set; } = 0;

        /// <summary>
        /// 登录提供者（0系统 1微信 2钉钉 3企业微信）
        /// </summary>
        public int LoginProvider { get; set; } = 0;

        /// <summary>
        /// 提供者Key
        /// </summary>
        [MaxLength(100, ErrorMessage = "提供者Key长度不能超过100个字符")]
        public string ProviderKey { get; set; } = string.Empty;

        /// <summary>
        /// 提供者显示名称
        /// </summary>
        [MaxLength(50, ErrorMessage = "提供者显示名称长度不能超过50个字符")]
        public string ProviderDisplayName { get; set; } = string.Empty;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; } = 0;

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
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// 设备ID
        /// </summary>
        [Required(ErrorMessage = "设备ID不能为空")]
        [MaxLength(100, ErrorMessage = "设备ID长度不能超过100个字符")]
        public string DeviceId { get; set; } = string.Empty;

        /// <summary>
        /// 设备类型（0PC 1Android 2iOS 3MacOS 4Linux 5其他）
        /// </summary>
        [Required(ErrorMessage = "设备类型不能为空")]
        public int DeviceType { get; set; } = 0;

        /// <summary>
        /// 浏览器类型（0Chrome 1Firefox 2Edge 3Safari 4IE 5其他）
        /// </summary>
        [Required(ErrorMessage = "浏览器类型不能为空")]
        public int BrowserType { get; set; } = 0;

        /// <summary>
        /// 操作系统类型（0Windows 1Android 2iOS 3MacOS 4Linux 5其他）
        /// </summary>
        [Required(ErrorMessage = "操作系统类型不能为空")]
        public int OsType { get; set; } = 0;
    }

    /// <summary>
    /// 登录环境日志信息更新传输对象
    /// </summary>
    public class HbtLoginEnvLogUpdateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "ID不能为空")]
        public long Id { get; set; }

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
        /// 登录类型（0普通 1短信 2邮箱 3微信 4QQ 5GitHub）
        /// </summary>
        [Required(ErrorMessage = "登录类型不能为空")]
        public int LoginType { get; set; }

        /// <summary>
        /// 登录来源（0Web 1App 2小程序 3其他）
        /// </summary>
        [Required(ErrorMessage = "登录来源不能为空")]
        public int LoginSource { get; set; }

        /// <summary>
        /// 登录提供者（0系统 1微信 2钉钉 3企业微信）
        /// </summary>
        public int LoginProvider { get; set; }

        /// <summary>
        /// 提供者Key
        /// </summary>
        [MaxLength(100, ErrorMessage = "提供者Key长度不能超过100个字符")]
        public string ProviderKey { get; set; } = string.Empty;

        /// <summary>
        /// 提供者显示名称
        /// </summary>
        [MaxLength(50, ErrorMessage = "提供者显示名称长度不能超过50个字符")]
        public string ProviderDisplayName { get; set; } = string.Empty;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }

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
        /// 设备类型（0PC 1Android 2iOS 3MacOS 4Linux 5其他）
        /// </summary>
        [Required(ErrorMessage = "设备类型不能为空")]
        public int DeviceType { get; set; }

        /// <summary>
        /// 浏览器类型（0Chrome 1Firefox 2Edge 3Safari 4IE 5其他）
        /// </summary>
        [Required(ErrorMessage = "浏览器类型不能为空")]
        public int BrowserType { get; set; }

        /// <summary>
        /// 操作系统类型（0Windows 1Android 2iOS 3MacOS 4Linux 5其他）
        /// </summary>
        [Required(ErrorMessage = "操作系统类型不能为空")]
        public int OsType { get; set; }
    }

    /// <summary>
    /// 登录环境日志信息在线时段更新传输对象
    /// </summary>
    public class HbtLoginEnvLogOnlinePeriodUpdateDto
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
    /// 登录环境日志信息导出传输对象
    /// </summary>
    public class HbtLoginEnvLogExportDto
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
        /// 登录类型（0普通 1短信 2邮箱 3微信 4QQ 5GitHub）
        /// </summary>
        public int? LoginType { get; set; }

        /// <summary>
        /// 登录来源（0Web 1App 2小程序 3其他）
        /// </summary>
        public int? LoginSource { get; set; }

        /// <summary>
        /// 登录状态（0离线 1在线）
        /// </summary>
        public int? LoginStatus { get; set; }

        /// <summary>
        /// 登录提供者（0系统 1微信 2钉钉 3企业微信）
        /// </summary>
        public int? LoginProvider { get; set; }

        /// <summary>
        /// 提供者Key
        /// </summary>
        public string? ProviderKey { get; set; }

        /// <summary>
        /// 提供者显示名称
        /// </summary>
        public string? ProviderDisplayName { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int? Status { get; set; }

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

    /// <summary>
    /// 登录环境日志状态更新传输对象
    /// </summary>
    public class HbtLoginEnvLogStatusDto
    {
        /// <summary>
        /// 登录ID
        /// </summary>
        [Required(ErrorMessage = "登录ID不能为空")]
        public long LoginId { get; set; }

        /// <summary>
        /// 登录状态
        /// </summary>
        [Required(ErrorMessage = "登录状态不能为空")]
        public int LoginStatus { get; set; }
    }
}