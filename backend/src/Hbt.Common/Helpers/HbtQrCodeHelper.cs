//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtQrCodeHelper.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 二维码生成帮助类
//===================================================================

using System.Drawing;
using QRCoder;

namespace Hbt.Common.Helpers;

/// <summary>
/// 二维码生成帮助类
/// </summary>
/// <remarks>
/// 基于QRCoder库实现，支持多种格式的二维码生成功能
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public static class HbtQrCodeHelper
{
    /// <summary>
    /// 默认颜色配置
    /// </summary>
    private static readonly Color DefaultDarkColor = Color.Black;
    private static readonly Color DefaultLightColor = Color.White;

    /// <summary>
    /// 生成二维码图片（PNG格式）
    /// </summary>
    /// <param name="content">二维码内容</param>
    /// <param name="pixelsPerModule">每个模块的像素数（默认20）</param>
    /// <param name="eccLevel">纠错级别（默认M）</param>
    /// <param name="darkColor">深色（默认黑色）</param>
    /// <param name="lightColor">浅色（默认白色）</param>
    /// <param name="drawQuietZones">是否绘制静默区域（默认true）</param>
    /// <returns>Base64编码的PNG图片</returns>
    public static string GenerateQrCodePng(string content, int pixelsPerModule = 20,
        QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.M, Color? darkColor = null, Color? lightColor = null, bool drawQuietZones = true)
    {
        if (string.IsNullOrEmpty(content))
            throw new ArgumentException("二维码内容不能为空", nameof(content));

        using var qrGenerator = new QRCodeGenerator();
        using var qrCodeData = qrGenerator.CreateQrCode(content, eccLevel);
        var qrCode = new PngByteQRCode(qrCodeData);
        var imageBytes = qrCode.GetGraphic(pixelsPerModule,
            darkColor ?? DefaultDarkColor,
            lightColor ?? DefaultLightColor);

        return Convert.ToBase64String(imageBytes);
    }

    /// <summary>
    /// 生成二维码图片（Bitmap格式）
    /// </summary>
    /// <param name="content">二维码内容</param>
    /// <param name="pixelsPerModule">每个模块的像素数（默认20）</param>
    /// <param name="eccLevel">纠错级别（默认M）</param>
    /// <param name="darkColor">深色（默认黑色）</param>
    /// <param name="lightColor">浅色（默认白色）</param>
    /// <param name="drawQuietZones">是否绘制静默区域（默认true）</param>
    /// <returns>Bitmap对象</returns>
    /// <remarks>
    /// 注意：QRCoder 1.6.0版本中QRCode类可能不可用，此方法暂不可用
    /// </remarks>
    public static Bitmap GenerateQrCodeBitmap(string content, int pixelsPerModule = 20,
        QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.M,
        Color? darkColor = null, Color? lightColor = null, bool drawQuietZones = true)
    {
        // QRCoder 1.6.0版本中QRCode类可能不可用，暂时抛出异常
        throw new NotImplementedException("QRCoder 1.6.0版本中QRCode类暂不可用，请使用其他方法");
    }

    /// <summary>
    /// 生成二维码字节数组
    /// </summary>
    /// <param name="content">二维码内容</param>
    /// <param name="pixelsPerModule">每个模块的像素数（默认20）</param>
    /// <param name="eccLevel">纠错级别（默认M）</param>
    /// <param name="darkColor">深色（默认黑色）</param>
    /// <param name="lightColor">浅色（默认白色）</param>
    /// <param name="drawQuietZones">是否绘制静默区域（默认true）</param>
    /// <returns>图片字节数组</returns>
    public static byte[] GenerateQrCodeBytes(string content, int pixelsPerModule = 20,
        QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.M,
        Color? darkColor = null, Color? lightColor = null, bool drawQuietZones = true)
    {
        if (string.IsNullOrEmpty(content))
            throw new ArgumentException("二维码内容不能为空", nameof(content));

        using var qrGenerator = new QRCodeGenerator();
        using var qrCodeData = qrGenerator.CreateQrCode(content, eccLevel);
        var qrCode = new PngByteQRCode(qrCodeData);
        return qrCode.GetGraphic(pixelsPerModule,
            darkColor ?? DefaultDarkColor,
            lightColor ?? DefaultLightColor);
    }

    /// <summary>
    /// 生成彩色二维码图片（PNG格式）
    /// </summary>
    /// <param name="content">二维码内容</param>
    /// <param name="pixelsPerModule">每个模块的像素数（默认20）</param>
    /// <param name="eccLevel">纠错级别（默认M）</param>
    /// <param name="darkColor">深色（默认黑色）</param>
    /// <param name="lightColor">浅色（默认白色）</param>
    /// <param name="drawQuietZones">是否绘制静默区域（默认true）</param>
    /// <returns>Base64编码的PNG图片</returns>
    public static string GenerateColorQrCode(string content, int pixelsPerModule = 20,
        QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.M,
        Color? darkColor = null, Color? lightColor = null, bool drawQuietZones = true)
    {
        if (string.IsNullOrEmpty(content))
            throw new ArgumentException("二维码内容不能为空", nameof(content));

        using var qrGenerator = new QRCodeGenerator();
        using var qrCodeData = qrGenerator.CreateQrCode(content, eccLevel);
        var qrCode = new PngByteQRCode(qrCodeData);
        var imageBytes = qrCode.GetGraphic(pixelsPerModule,
            darkColor ?? DefaultDarkColor,
            lightColor ?? DefaultLightColor);

        return Convert.ToBase64String(imageBytes);
    }

    /// <summary>
    /// 生成彩色二维码字节数组
    /// </summary>
    /// <param name="content">二维码内容</param>
    /// <param name="pixelsPerModule">每个模块的像素数（默认20）</param>
    /// <param name="eccLevel">纠错级别（默认M）</param>
    /// <param name="darkColor">深色（默认黑色）</param>
    /// <param name="lightColor">浅色（默认白色）</param>
    /// <param name="drawQuietZones">是否绘制静默区域（默认true）</param>
    /// <returns>图片字节数组</returns>
    public static byte[] GenerateColorQrCodeBytes(string content, int pixelsPerModule = 20,
        QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.M,
        Color? darkColor = null, Color? lightColor = null, bool drawQuietZones = true)
    {
        if (string.IsNullOrEmpty(content))
            throw new ArgumentException("二维码内容不能为空", nameof(content));

        using var qrGenerator = new QRCodeGenerator();
        using var qrCodeData = qrGenerator.CreateQrCode(content, eccLevel);
        var qrCode = new PngByteQRCode(qrCodeData);
        return qrCode.GetGraphic(pixelsPerModule,
            darkColor ?? DefaultDarkColor,
            lightColor ?? DefaultLightColor);
    }

    /// <summary>
    /// 生成二维码SVG字符串
    /// </summary>
    /// <param name="content">二维码内容</param>
    /// <param name="pixelsPerModule">每个模块的像素数（默认20）</param>
    /// <param name="eccLevel">纠错级别（默认M）</param>
    /// <param name="darkColor">深色（默认黑色）</param>
    /// <param name="lightColor">浅色（默认白色）</param>
    /// <param name="drawQuietZones">是否绘制静默区域（默认true）</param>
    /// <returns>SVG字符串</returns>
    public static string GenerateQrCodeSvg(string content, int pixelsPerModule = 20,
        QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.M,
        Color? darkColor = null, Color? lightColor = null, bool drawQuietZones = true)
    {
        if (string.IsNullOrEmpty(content))
            throw new ArgumentException("二维码内容不能为空", nameof(content));

        using var qrGenerator = new QRCodeGenerator();
        using var qrCodeData = qrGenerator.CreateQrCode(content, eccLevel);
        var qrCode = new SvgQRCode(qrCodeData);

        var darkColorHex = (darkColor ?? DefaultDarkColor).ToHexString();
        var lightColorHex = (lightColor ?? DefaultLightColor).ToHexString();

        return qrCode.GetGraphic(pixelsPerModule, darkColorHex, lightColorHex, drawQuietZones);
    }

    /// <summary>
    /// 生成带Logo的二维码图片（PNG格式）
    /// </summary>
    /// <param name="content">二维码内容</param>
    /// <param name="logoBytes">Logo图片字节数组</param>
    /// <param name="pixelsPerModule">每个模块的像素数（默认20）</param>
    /// <param name="eccLevel">纠错级别（默认M）</param>
    /// <param name="darkColor">深色（默认黑色）</param>
    /// <param name="lightColor">浅色（默认白色）</param>
    /// <param name="logoSizePercent">Logo大小百分比（默认15）</param>
    /// <param name="logoBorder">Logo边框宽度（默认6）</param>
    /// <param name="logoBackgroundColor">Logo背景色（默认白色）</param>
    /// <returns>Base64编码的PNG图片</returns>
    /// <remarks>
    /// 注意：QRCoder 1.6.0版本暂不支持Logo功能，此方法返回普通二维码
    /// </remarks>
    public static string GenerateQrCodeWithLogo(string content, byte[] logoBytes, int pixelsPerModule = 20,
        QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.M,
        Color? darkColor = null, Color? lightColor = null, int logoSizePercent = 15,
        int logoBorder = 6, Color? logoBackgroundColor = null)
    {
        // QRCoder 1.6.0版本暂不支持Logo功能，返回普通二维码
        return GenerateColorQrCode(content, pixelsPerModule, eccLevel, darkColor, lightColor);
    }

    /// <summary>
    /// 生成带Logo的二维码字节数组
    /// </summary>
    /// <param name="content">二维码内容</param>
    /// <param name="logoBytes">Logo图片字节数组</param>
    /// <param name="pixelsPerModule">每个模块的像素数（默认20）</param>
    /// <param name="eccLevel">纠错级别（默认M）</param>
    /// <param name="darkColor">深色（默认黑色）</param>
    /// <param name="lightColor">浅色（默认白色）</param>
    /// <param name="logoSizePercent">Logo大小百分比（默认15）</param>
    /// <param name="logoBorder">Logo边框宽度（默认6）</param>
    /// <param name="logoBackgroundColor">Logo背景色（默认白色）</param>
    /// <returns>图片字节数组</returns>
    /// <remarks>
    /// 注意：QRCoder 1.6.0版本暂不支持Logo功能，此方法返回普通二维码
    /// </remarks>
    public static byte[] GenerateQrCodeWithLogoBytes(string content, byte[] logoBytes, int pixelsPerModule = 20,
        QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.M,
        Color? darkColor = null, Color? lightColor = null, int logoSizePercent = 15,
        int logoBorder = 6, Color? logoBackgroundColor = null)
    {
        // QRCoder 1.6.0版本暂不支持Logo功能，返回普通二维码
        return GenerateColorQrCodeBytes(content, pixelsPerModule, eccLevel, darkColor, lightColor);
    }

    /// <summary>
    /// 生成二维码HTML格式
    /// </summary>
    /// <param name="content">二维码内容</param>
    /// <param name="pixelsPerModule">每个模块的像素数（默认20）</param>
    /// <param name="eccLevel">纠错级别（默认M）</param>
    /// <param name="darkColor">深色（默认黑色）</param>
    /// <param name="lightColor">浅色（默认白色）</param>
    /// <param name="drawQuietZones">是否绘制静默区域（默认true）</param>
    /// <returns>HTML字符串</returns>
    /// <remarks>
    /// 注意：QRCoder 1.6.0版本中HtmlQRCode类暂不可用，此方法暂不可用
    /// </remarks>
    public static string GenerateQrCodeHtml(string content, int pixelsPerModule = 20,
        QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.M,
        Color? darkColor = null, Color? lightColor = null, bool drawQuietZones = true)
    {
        // QRCoder 1.6.0版本中HtmlQRCode类暂不可用，暂时抛出异常
        throw new NotImplementedException("QRCoder 1.6.0版本中HtmlQRCode类暂不可用，请使用其他方法");
    }

    /// <summary>
    /// 生成二维码PostScript格式
    /// </summary>
    /// <param name="content">二维码内容</param>
    /// <param name="pixelsPerModule">每个模块的像素数（默认20）</param>
    /// <param name="eccLevel">纠错级别（默认M）</param>
    /// <param name="darkColor">深色（默认黑色）</param>
    /// <param name="lightColor">浅色（默认白色）</param>
    /// <param name="drawQuietZones">是否绘制静默区域（默认true）</param>
    /// <returns>PostScript字符串</returns>
    /// <remarks>
    /// 注意：QRCoder 1.6.0版本中PostscriptQRCode类暂不可用，此方法暂不可用
    /// </remarks>
    public static string GenerateQrCodePostScript(string content, int pixelsPerModule = 20,
        QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.M,
        Color? darkColor = null, Color? lightColor = null, bool drawQuietZones = true)
    {
        // QRCoder 1.6.0版本中PostscriptQRCode类暂不可用，暂时抛出异常
        throw new NotImplementedException("QRCoder 1.6.0版本中PostscriptQRCode类暂不可用，请使用其他方法");
    }

    /// <summary>
    /// 生成二维码EPS格式
    /// </summary>
    /// <param name="content">二维码内容</param>
    /// <param name="pixelsPerModule">每个模块的像素数（默认20）</param>
    /// <param name="eccLevel">纠错级别（默认M）</param>
    /// <param name="darkColor">深色（默认黑色）</param>
    /// <param name="lightColor">浅色（默认白色）</param>
    /// <param name="drawQuietZones">是否绘制静默区域（默认true）</param>
    /// <returns>EPS字符串</returns>
    /// <remarks>
    /// 注意：QRCoder 1.6.0版本中EpsQRCode类暂不可用，此方法暂不可用
    /// </remarks>
    public static string GenerateQrCodeEps(string content, int pixelsPerModule = 20,
        QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.M,
        Color? darkColor = null, Color? lightColor = null, bool drawQuietZones = true)
    {
        // QRCoder 1.6.0版本中EpsQRCode类暂不可用，暂时抛出异常
        throw new NotImplementedException("QRCoder 1.6.0版本中EpsQRCode类暂不可用，请使用其他方法");
    }

    /// <summary>
    /// 生成二维码Art格式（ASCII艺术）
    /// </summary>
    /// <param name="content">二维码内容</param>
    /// <param name="pixelsPerModule">每个模块的像素数（默认20）</param>
    /// <param name="eccLevel">纠错级别（默认M）</param>
    /// <param name="darkColorString">深色字符（默认"██"）</param>
    /// <param name="lightColorString">浅色字符（默认"  "）</param>
    /// <param name="endOfLine">行结束符（默认"\n"）</param>
    /// <param name="drawQuietZones">是否绘制静默区域（默认true）</param>
    /// <returns>ASCII艺术字符串</returns>
    /// <remarks>
    /// 注意：QRCoder 1.6.0版本中ArtQRCode类暂不可用，此方法暂不可用
    /// </remarks>
    public static string GenerateQrCodeArt(string content, int pixelsPerModule = 20,
        QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.M,
        string darkColorString = "██", string lightColorString = "  ", string endOfLine = "\n", bool drawQuietZones = true)
    {
        // QRCoder 1.6.0版本中ArtQRCode类暂不可用，暂时抛出异常
        throw new NotImplementedException("QRCoder 1.6.0版本中ArtQRCode类暂不可用，请使用其他方法");
    }

    /// <summary>
    /// 验证二维码内容是否有效
    /// </summary>
    /// <param name="content">二维码内容</param>
    /// <param name="maxLength">最大长度（默认2953字节）</param>
    /// <returns>是否有效</returns>
    public static bool IsValidQrCodeContent(string content, int maxLength = 2953)
    {
        if (string.IsNullOrEmpty(content))
            return false;

        if (content.Length > maxLength)
            return false;

        // 检查是否包含无效字符
        var invalidChars = content.Where(c => c < 32 && c != 9 && c != 10 && c != 13).Any();
        return !invalidChars;
    }

    /// <summary>
    /// 将Color转换为十六进制字符串
    /// </summary>
    /// <param name="color">颜色</param>
    /// <returns>十六进制字符串（如：#FF0000）</returns>
    private static string ToHexString(this Color color)
    {
        return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
    }

    #region 第三方扫码登录

    /// <summary>
    /// 生成微信扫码登录二维码
    /// </summary>
    /// <param name="appId">微信应用ID</param>
    /// <param name="redirectUri">回调地址</param>
    /// <param name="state">状态参数</param>
    /// <param name="scope">授权范围（默认snsapi_login）</param>
    /// <returns>微信二维码URL</returns>
    public static string GenerateWeChatQrCodeUrl(string appId, string redirectUri, string state = "", string scope = "snsapi_login")
    {
        if (string.IsNullOrEmpty(appId))
            throw new ArgumentException("微信应用ID不能为空", nameof(appId));

        if (string.IsNullOrEmpty(redirectUri))
            throw new ArgumentException("回调地址不能为空", nameof(redirectUri));

        var encodedRedirectUri = Uri.EscapeDataString(redirectUri);
        var encodedState = Uri.EscapeDataString(state);

        return $"https://open.weixin.qq.com/connect/qrconnect?appid={appId}&redirect_uri={encodedRedirectUri}&response_type=code&scope={scope}&state={encodedState}#wechat_redirect";
    }

    /// <summary>
    /// 生成支付宝扫码登录二维码
    /// </summary>
    /// <param name="appId">支付宝应用ID</param>
    /// <param name="redirectUri">回调地址</param>
    /// <param name="state">状态参数</param>
    /// <param name="scope">授权范围（默认auth_user）</param>
    /// <returns>支付宝二维码URL</returns>
    public static string GenerateAlipayQrCodeUrl(string appId, string redirectUri, string state = "", string scope = "auth_user")
    {
        if (string.IsNullOrEmpty(appId))
            throw new ArgumentException("支付宝应用ID不能为空", nameof(appId));

        if (string.IsNullOrEmpty(redirectUri))
            throw new ArgumentException("回调地址不能为空", nameof(redirectUri));

        var encodedRedirectUri = Uri.EscapeDataString(redirectUri);
        var encodedState = Uri.EscapeDataString(state);

        return $"https://openauth.alipay.com/oauth2/publicAppAuthorize.htm?app_id={appId}&scope={scope}&redirect_uri={encodedRedirectUri}&state={encodedState}";
    }

    /// <summary>
    /// 生成微信扫码登录二维码图片
    /// </summary>
    /// <param name="appId">微信应用ID</param>
    /// <param name="redirectUri">回调地址</param>
    /// <param name="state">状态参数</param>
    /// <param name="scope">授权范围（默认snsapi_login）</param>
    /// <param name="pixelsPerModule">每个模块的像素数（默认20）</param>
    /// <param name="eccLevel">纠错级别（默认M）</param>
    /// <returns>Base64编码的微信二维码图片</returns>
    public static string GenerateWeChatQrCodeImage(string appId, string redirectUri, string state = "", string scope = "snsapi_login",
        int pixelsPerModule = 20, QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.M)
    {
        var qrCodeUrl = GenerateWeChatQrCodeUrl(appId, redirectUri, state, scope);
        return GenerateQrCodePng(qrCodeUrl, pixelsPerModule, eccLevel);
    }

    /// <summary>
    /// 生成支付宝扫码登录二维码图片
    /// </summary>
    /// <param name="appId">支付宝应用ID</param>
    /// <param name="redirectUri">回调地址</param>
    /// <param name="state">状态参数</param>
    /// <param name="scope">授权范围（默认auth_user）</param>
    /// <param name="pixelsPerModule">每个模块的像素数（默认20）</param>
    /// <param name="eccLevel">纠错级别（默认M）</param>
    /// <returns>Base64编码的支付宝二维码图片</returns>
    public static string GenerateAlipayQrCodeImage(string appId, string redirectUri, string state = "", string scope = "auth_user",
        int pixelsPerModule = 20, QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.M)
    {
        var qrCodeUrl = GenerateAlipayQrCodeUrl(appId, redirectUri, state, scope);
        return GenerateQrCodePng(qrCodeUrl, pixelsPerModule, eccLevel);
    }

    /// <summary>
    /// 生成微信扫码登录二维码SVG
    /// </summary>
    /// <param name="appId">微信应用ID</param>
    /// <param name="redirectUri">回调地址</param>
    /// <param name="state">状态参数</param>
    /// <param name="scope">授权范围（默认snsapi_login）</param>
    /// <param name="pixelsPerModule">每个模块的像素数（默认20）</param>
    /// <param name="eccLevel">纠错级别（默认M）</param>
    /// <returns>微信二维码SVG字符串</returns>
    public static string GenerateWeChatQrCodeSvg(string appId, string redirectUri, string state = "", string scope = "snsapi_login",
        int pixelsPerModule = 20, QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.M)
    {
        var qrCodeUrl = GenerateWeChatQrCodeUrl(appId, redirectUri, state, scope);
        return GenerateQrCodeSvg(qrCodeUrl, pixelsPerModule, eccLevel);
    }

    /// <summary>
    /// 生成支付宝扫码登录二维码SVG
    /// </summary>
    /// <param name="appId">支付宝应用ID</param>
    /// <param name="redirectUri">回调地址</param>
    /// <param name="state">状态参数</param>
    /// <param name="scope">授权范围（默认auth_user）</param>
    /// <param name="pixelsPerModule">每个模块的像素数（默认20）</param>
    /// <param name="eccLevel">纠错级别（默认M）</param>
    /// <returns>支付宝二维码SVG字符串</returns>
    public static string GenerateAlipayQrCodeSvg(string appId, string redirectUri, string state = "", string scope = "auth_user",
        int pixelsPerModule = 20, QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.M)
    {
        var qrCodeUrl = GenerateAlipayQrCodeUrl(appId, redirectUri, state, scope);
        return GenerateQrCodeSvg(qrCodeUrl, pixelsPerModule, eccLevel);
    }

    /// <summary>
    /// 验证微信回调参数
    /// </summary>
    /// <param name="code">授权码</param>
    /// <param name="state">状态参数</param>
    /// <returns>是否有效</returns>
    public static bool ValidateWeChatCallback(string code, string state)
    {
        return !string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(state);
    }

    /// <summary>
    /// 验证支付宝回调参数
    /// </summary>
    /// <param name="authCode">授权码</param>
    /// <param name="state">状态参数</param>
    /// <returns>是否有效</returns>
    public static bool ValidateAlipayCallback(string authCode, string state)
    {
        return !string.IsNullOrEmpty(authCode) && !string.IsNullOrEmpty(state);
    }

    /// <summary>
    /// 生成微信扫码登录状态
    /// </summary>
    /// <param name="qrCodeId">二维码ID</param>
    /// <param name="loginType">登录类型</param>
    /// <returns>状态字符串</returns>
    public static string GenerateWeChatState(string qrCodeId, string loginType = "WeChat")
    {
        var stateData = new
        {
            QrCodeId = qrCodeId,
            LoginType = loginType,
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };

        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(stateData)));
    }

    /// <summary>
    /// 生成支付宝扫码登录状态
    /// </summary>
    /// <param name="qrCodeId">二维码ID</param>
    /// <param name="loginType">登录类型</param>
    /// <returns>状态字符串</returns>
    public static string GenerateAlipayState(string qrCodeId, string loginType = "Alipay")
    {
        var stateData = new
        {
            QrCodeId = qrCodeId,
            LoginType = loginType,
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };

        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(stateData)));
    }

    /// <summary>
    /// 解析扫码登录状态
    /// </summary>
    /// <param name="state">状态字符串</param>
    /// <returns>状态数据</returns>
    public static ThirdPartyLoginState? ParseLoginState(string state)
    {
        try
        {
            var jsonBytes = Convert.FromBase64String(state);
            var json = System.Text.Encoding.UTF8.GetString(jsonBytes);
            return System.Text.Json.JsonSerializer.Deserialize<ThirdPartyLoginState>(json);
        }
        catch
        {
            return null;
        }
    }

    #endregion
}

/// <summary>
/// 第三方登录状态数据
/// </summary>
public class ThirdPartyLoginState
{
    /// <summary>
    /// 二维码ID
    /// </summary>
    public string QrCodeId { get; set; } = string.Empty;

    /// <summary>
    /// 登录类型
    /// </summary>
    public string LoginType { get; set; } = string.Empty;

    /// <summary>
    /// 时间戳
    /// </summary>
    public long Timestamp { get; set; }
}