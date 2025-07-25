//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSignalRDevice.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : SignalR设备信息实体
//===================================================================

using System;
using Lean.Hbt.Common.Enums;
using System.Text.Json.Serialization;

namespace Lean.Hbt.Common.Models;

/// <summary>
/// SignalR设备信息
/// </summary>
public class HbtSignalRDevice
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [JsonPropertyName("userId")]
    public long UserId { get; set; }

    /// <summary>
    /// 用户组ID
    /// </summary>
    [JsonPropertyName("groupId")]
    public long GroupId { get; set; }

    /// <summary>
    /// 连接ID
    /// </summary>
    [JsonPropertyName("connectionId")]
    public string? ConnectionId { get; set; }

    /// <summary>
    /// 设备ID
    /// </summary>
    [JsonPropertyName("deviceId")]
    public string? DeviceId { get; set; }

    /// <summary>
    /// 客户端IP
    /// </summary>
    [JsonPropertyName("ipAddress")]
    public string? IpAddress { get; set; }

    /// <summary>
    /// 用户代理
    /// </summary>
    [JsonPropertyName("userAgent")]
    public string? UserAgent { get; set; }

    /// <summary>
    /// 最后活动时间
    /// </summary>
    [JsonPropertyName("lastActivity")]
    public DateTime LastActivity { get; set; }

    /// <summary>
    /// 最后心跳时间
    /// </summary>
    [JsonPropertyName("lastHeartbeat")]
    public DateTime LastHeartbeat { get; set; }

    /// <summary>
    /// 在线状态（0-在线，1-离线）
    /// </summary>
    [JsonPropertyName("onlineStatus")]
    public int OnlineStatus { get; set; }

    /// <summary>
    /// 设备类型
    /// </summary>
    [JsonPropertyName("deviceType")]
    public HbtDeviceType DeviceType { get; set; }

    /// <summary>
    /// 设备令牌
    /// </summary>
    [JsonPropertyName("deviceToken")]
    public string? DeviceToken { get; set; }

    /// <summary>
    /// 设备名称
    /// </summary>
    [JsonPropertyName("deviceName")]
    public string? DeviceName { get; set; }

    /// <summary>
    /// 设备型号
    /// </summary>
    [JsonPropertyName("deviceModel")]
    public string? DeviceModel { get; set; }

    /// <summary>
    /// 操作系统类型
    /// </summary>
    [JsonPropertyName("osType")]
    public int? OsType { get; set; }

    /// <summary>
    /// 系统版本
    /// </summary>
    [JsonPropertyName("osVersion")]
    public string? OsVersion { get; set; }

    /// <summary>
    /// 浏览器类型
    /// </summary>
    [JsonPropertyName("browserType")]
    public int? BrowserType { get; set; }

    /// <summary>
    /// 浏览器版本
    /// </summary>
    [JsonPropertyName("browserVersion")]
    public string? BrowserVersion { get; set; }

    /// <summary>
    /// 分辨率
    /// </summary>
    [JsonPropertyName("resolution")]
    public string? Resolution { get; set; }

    /// <summary>
    /// 位置信息
    /// </summary>
    [JsonPropertyName("location")]
    public string? Location { get; set; }

    /// <summary>
    /// 设备指纹
    /// </summary>
    [JsonPropertyName("deviceFingerprint")]
    public string? DeviceFingerprint { get; set; }
} 