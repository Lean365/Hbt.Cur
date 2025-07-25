//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtQrCodeAuthOptions.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 二维码认证配置选项
//===================================================================

namespace Lean.Hbt.Common.Options;

/// <summary>
/// 二维码认证配置选项
/// </summary>
public class HbtQrCodeAuthOptions
{
    /// <summary>
    /// 是否启用二维码认证
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// 二维码过期时间（分钟）
    /// </summary>
    public int ExpirationMinutes { get; set; } = 5;

    /// <summary>
    /// 二维码图片像素大小
    /// </summary>
    public int PixelsPerModule { get; set; } = 20;

    /// <summary>
    /// 二维码纠错级别（L=7%, M=15%, Q=25%, H=30%）
    /// </summary>
    public string EccLevel { get; set; } = "M";

    /// <summary>
    /// 二维码基础URL
    /// </summary>
    public string BaseUrl { get; set; } = "https://your-app.com/qr";

    /// <summary>
    /// 是否启用微信扫码登录
    /// </summary>
    public bool EnableWeChatLogin { get; set; } = false;

    /// <summary>
    /// 是否启用支付宝扫码登录
    /// </summary>
    public bool EnableAlipayLogin { get; set; } = false;
}

/// <summary>
/// 微信扫码登录配置选项
/// </summary>
public class WeChatQrCodeOptions
{
    /// <summary>
    /// 微信应用ID
    /// </summary>
    public string AppId { get; set; } = string.Empty;

    /// <summary>
    /// 回调地址
    /// </summary>
    public string RedirectUri { get; set; } = string.Empty;

    /// <summary>
    /// 授权范围
    /// </summary>
    public string Scope { get; set; } = "snsapi_login";
}

/// <summary>
/// 支付宝扫码登录配置选项
/// </summary>
public class AlipayQrCodeOptions
{
    /// <summary>
    /// 支付宝应用ID
    /// </summary>
    public string AppId { get; set; } = string.Empty;

    /// <summary>
    /// 回调地址
    /// </summary>
    public string RedirectUri { get; set; } = string.Empty;

    /// <summary>
    /// 授权范围
    /// </summary>
    public string Scope { get; set; } = "auth_user";
} 