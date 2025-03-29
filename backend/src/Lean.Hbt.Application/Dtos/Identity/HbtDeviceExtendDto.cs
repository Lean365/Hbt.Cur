#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDeviceExtendDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V1.0.0
// 描述    : 设备扩展信息传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.Identity
{
    /// <summary>
    /// 设备扩展信息传输对象
    /// </summary>
    public class HbtDeviceExtendDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
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
        /// 设备类型
        /// </summary>
        [Required(ErrorMessage = "设备类型不能为空")]
        public int DeviceType { get; set; }

        /// <summary>
        /// 设备标识
        /// </summary>
        [Required(ErrorMessage = "设备标识不能为空")]
        [MaxLength(100, ErrorMessage = "设备标识长度不能超过100个字符")]
        public string DeviceId { get; set; } = string.Empty;

        /// <summary>
        /// 设备名称
        /// </summary>
        [MaxLength(100, ErrorMessage = "设备名称长度不能超过100个字符")]
        public string? DeviceName { get; set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        [MaxLength(100, ErrorMessage = "设备型号长度不能超过100个字符")]
        public string? DeviceModel { get; set; }

        /// <summary>
        /// 操作系统类型
        /// </summary>
        public int? OsType { get; set; }

        /// <summary>
        /// 系统版本
        /// </summary>
        [MaxLength(50, ErrorMessage = "系统版本长度不能超过50个字符")]
        public string? OsVersion { get; set; }

        /// <summary>
        /// 浏览器类型
        /// </summary>
        public int? BrowserType { get; set; }

        /// <summary>
        /// 浏览器版本
        /// </summary>
        [MaxLength(50, ErrorMessage = "浏览器版本长度不能超过50个字符")]
        public string? BrowserVersion { get; set; }

        /// <summary>
        /// 分辨率
        /// </summary>
        [MaxLength(50, ErrorMessage = "分辨率长度不能超过50个字符")]
        public string? Resolution { get; set; }

        /// <summary>
        /// 设备状态
        /// </summary>
        [Required(ErrorMessage = "设备状态不能为空")]
        public int LoginStatus { get; set; }

        /// <summary>
        /// 最后在线时间
        /// </summary>
        public DateTime? LastOnlineTime { get; set; }

        /// <summary>
        /// 最后离线时间
        /// </summary>
        public DateTime? LastOfflineTime { get; set; }

        /// <summary>
        /// 今日在线时段(JSON格式,例如:["09:00-12:00","14:00-18:00"])
        /// </summary>
        public string? TodayOnlinePeriods { get; set; }
    }

    /// <summary>
    /// 设备扩展信息更新传输对象
    /// </summary>
    public class HbtDeviceExtendUpdateDto
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
        /// 设备类型
        /// </summary>
        [Required(ErrorMessage = "设备类型不能为空")]
        public int DeviceType { get; set; }

        /// <summary>
        /// 设备标识
        /// </summary>
        [Required(ErrorMessage = "设备标识不能为空")]
        [MaxLength(100, ErrorMessage = "设备标识长度不能超过100个字符")]
        public string DeviceId { get; set; } = string.Empty;

        /// <summary>
        /// 设备名称
        /// </summary>
        [MaxLength(100, ErrorMessage = "设备名称长度不能超过100个字符")]
        public string? DeviceName { get; set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        [MaxLength(100, ErrorMessage = "设备型号长度不能超过100个字符")]
        public string? DeviceModel { get; set; }

        /// <summary>
        /// 操作系统类型
        /// </summary>
        public int? OsType { get; set; }

        /// <summary>
        /// 系统版本
        /// </summary>
        [MaxLength(50, ErrorMessage = "系统版本长度不能超过50个字符")]
        public string? OsVersion { get; set; }

        /// <summary>
        /// 浏览器类型
        /// </summary>
        public int? BrowserType { get; set; }

        /// <summary>
        /// 浏览器版本
        /// </summary>
        [MaxLength(50, ErrorMessage = "浏览器版本长度不能超过50个字符")]
        public string? BrowserVersion { get; set; }

        /// <summary>
        /// 分辨率
        /// </summary>
        [MaxLength(50, ErrorMessage = "分辨率长度不能超过50个字符")]
        public string? Resolution { get; set; }
    }

    /// <summary>
    /// 设备在线时段更新传输对象
    /// </summary>
    public class HbtDeviceOnlinePeriodUpdateDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public long UserId { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        [Required(ErrorMessage = "设备ID不能为空")]
        [MaxLength(100, ErrorMessage = "设备ID长度不能超过100个字符")]
        public string DeviceId { get; set; } = string.Empty;

        /// <summary>
        /// 在线时段(格式:"HH:mm-HH:mm")
        /// </summary>
        [Required(ErrorMessage = "在线时段不能为空")]
        [RegularExpression(@"^([0-1][0-9]|2[0-3]):[0-5][0-9]-([0-1][0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "在线时段格式不正确")]
        public string OnlinePeriod { get; set; } = string.Empty;
    }

    /// <summary>
    /// 设备扩展信息查询传输对象
    /// </summary>
    public class HbtDeviceExtendQueryDto : HbtPagedQuery
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
        /// 设备类型
        /// </summary>
        public int? DeviceType { get; set; }

        /// <summary>
        /// 设备标识
        /// </summary>
        public string? DeviceId { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string? DeviceName { get; set; }

        /// <summary>
        /// 设备状态
        /// </summary>
        public int? LoginStatus { get; set; }

        /// <summary>
        /// 最后在线时间范围开始
        /// </summary>
        public DateTime? LastOnlineTimeStart { get; set; }

        /// <summary>
        /// 最后在线时间范围结束
        /// </summary>
        public DateTime? LastOnlineTimeEnd { get; set; }
    }

    /// <summary>
    /// 设备扩展信息导出传输对象
    /// </summary>
    public class HbtDeviceExtendExportDto
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
        /// 设备类型
        /// </summary>
        public int? DeviceType { get; set; }

        /// <summary>
        /// 设备标识
        /// </summary>
        public string? DeviceId { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string? DeviceName { get; set; }

        /// <summary>
        /// 设备状态
        /// </summary>
        public int? LoginStatus { get; set; }

        /// <summary>
        /// 最后在线时间范围开始
        /// </summary>
        public DateTime? LastOnlineTimeStart { get; set; }

        /// <summary>
        /// 最后在线时间范围结束
        /// </summary>
        public DateTime? LastOnlineTimeEnd { get; set; }

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