//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDeviceInfo.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V1.0.0
// 描述    : 设备信息模型
//===================================================================

using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.Common.Models
{
    /// <summary>
    /// 设备信息模型
    /// </summary>
    public class HbtDeviceInfo
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public long? TenantId { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public string? DeviceId { get; set; }

        /// <summary>
        /// 设备令牌（后端生成的唯一标识）
        /// </summary>
        public string? DeviceToken { get; set; }

        /// <summary>
        /// 设备指纹（前端生成的特征值）
        /// </summary>
        public string? DeviceFingerprint { get; set; }

        /// <summary>
        /// 登录令牌
        /// </summary>
        public string? AccessToken { get; set; }

        /// <summary>
        /// 首次登录时间
        /// </summary>
        public DateTime? FirstLoginTime { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public HbtDeviceType DeviceType { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string? DeviceName { get; set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        public string? DeviceModel { get; set; }

        /// <summary>
        /// 操作系统类型
        /// </summary>
        public HbtOsType OsType { get; set; }

        /// <summary>
        /// 操作系统版本
        /// </summary>
        public string? OsVersion { get; set; }

        /// <summary>
        /// 浏览器类型
        /// </summary>
        public HbtBrowserType BrowserType { get; set; }

        /// <summary>
        /// 浏览器版本
        /// </summary>
        public string? BrowserVersion { get; set; }

        /// <summary>
        /// 分辨率
        /// </summary>
        public string? Resolution { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// 地理位置
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// 处理器核心数
        /// </summary>
        public string? ProcessorCores { get; set; }

        /// <summary>
        /// 平台供应商
        /// </summary>
        public string? PlatformVendor { get; set; }

        /// <summary>
        /// 硬件并发数
        /// </summary>
        public string? HardwareConcurrency { get; set; }

        /// <summary>
        /// 系统语言
        /// </summary>
        public string? SystemLanguage { get; set; }

        /// <summary>
        /// 时区
        /// </summary>
        public string? TimeZone { get; set; }

        /// <summary>
        /// 屏幕色深
        /// </summary>
        public string? ScreenColorDepth { get; set; }

        /// <summary>
        /// 设备内存
        /// </summary>
        public string? DeviceMemory { get; set; }

        /// <summary>
        /// WebGL渲染器信息
        /// </summary>
        public string? WebGLRenderer { get; set; }
    }
} 