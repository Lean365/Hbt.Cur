//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSmsAuthOptions.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 短信认证配置选项
//===================================================================

namespace Hbt.Cur.Common.Options;

/// <summary>
/// 短信认证配置选项
/// </summary>
public class HbtSmsAuthOptions
{
    /// <summary>
    /// 是否启用短信登录
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// 短信服务提供商
    /// </summary>
    public string Provider { get; set; } = "Aliyun";

    /// <summary>
    /// 访问密钥ID
    /// </summary>
    public string AccessKeyId { get; set; } = string.Empty;

    /// <summary>
    /// 访问密钥Secret
    /// </summary>
    public string AccessKeySecret { get; set; } = string.Empty;

    /// <summary>
    /// 短信签名
    /// </summary>
    public string SignName { get; set; } = string.Empty;

    /// <summary>
    /// 短信模板代码
    /// </summary>
    public string TemplateCode { get; set; } = string.Empty;

    /// <summary>
    /// 验证码过期时间（分钟）
    /// </summary>
    public int ExpirationMinutes { get; set; } = 5;

    /// <summary>
    /// 每小时最大发送次数
    /// </summary>
    public int MaxSendCountPerHour { get; set; } = 10;

    /// <summary>
    /// 是否启用验证码
    /// </summary>
    public bool EnableCaptcha { get; set; } = true;

    /// <summary>
    /// 需要验证码的尝试次数
    /// </summary>
    public int CaptchaRequiredAttempts { get; set; } = 3;

    /// <summary>
    /// 是否启用频率限制
    /// </summary>
    public bool EnableFrequencyLimit { get; set; } = true;

    /// <summary>
    /// 是否启用IP限制
    /// </summary>
    public bool EnableIpLimit { get; set; } = true;

    /// <summary>
    /// 每个IP每小时最大发送次数
    /// </summary>
    public int MaxSendCountPerIpPerHour { get; set; } = 20;
} 