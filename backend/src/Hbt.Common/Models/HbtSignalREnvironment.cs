//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSignalREnvironment.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : SignalR环境信息实体
//===================================================================

using System;
using System.Text.Json.Serialization;

namespace Hbt.Cur.Common.Models;

/// <summary>
/// SignalR环境信息
/// </summary>
public class HbtSignalREnvironment
{
    /// <summary>
    /// 环境ID
    /// </summary>
    [JsonPropertyName("environmentId")]
    public string? EnvironmentId { get; set; }

    /// <summary>
    /// 环境指纹
    /// </summary>
    [JsonPropertyName("fingerprint")]
    public string? Fingerprint { get; set; }

    /// <summary>
    /// 浏览器信息
    /// </summary>
    [JsonPropertyName("browserInfo")]
    public string? BrowserInfo { get; set; }

    /// <summary>
    /// 操作系统信息
    /// </summary>
    [JsonPropertyName("osInfo")]
    public string? OsInfo { get; set; }

    /// <summary>
    /// 屏幕信息
    /// </summary>
    [JsonPropertyName("screenInfo")]
    public string? ScreenInfo { get; set; }

    /// <summary>
    /// 时区信息
    /// </summary>
    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    /// <summary>
    /// 语言信息
    /// </summary>
    [JsonPropertyName("language")]
    public string? Language { get; set; }

    /// <summary>
    /// 插件信息
    /// </summary>
    [JsonPropertyName("plugins")]
    public string? Plugins { get; set; }

    /// <summary>
    /// Canvas指纹
    /// </summary>
    [JsonPropertyName("canvasFingerprint")]
    public string? CanvasFingerprint { get; set; }

    /// <summary>
    /// WebGL指纹
    /// </summary>
    [JsonPropertyName("webglFingerprint")]
    public string? WebglFingerprint { get; set; }

    /// <summary>
    /// 字体信息
    /// </summary>
    [JsonPropertyName("fonts")]
    public string? Fonts { get; set; }

    /// <summary>
    /// 音频指纹
    /// </summary>
    [JsonPropertyName("audioFingerprint")]
    public string? AudioFingerprint { get; set; }
} 