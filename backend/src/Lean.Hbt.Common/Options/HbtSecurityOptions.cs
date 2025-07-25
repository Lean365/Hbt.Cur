//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSecurityOptions.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 18:00
// 版本号 : V0.0.1
// 描述    : 安全配置选项
//===================================================================

namespace Lean.Hbt.Common.Options;

/// <summary>
/// 安全配置选项
/// </summary>
public class HbtSecurityOptions
{
    /// <summary>
    /// 密码策略选项
    /// </summary>
    public HbtPasswordPolicyOptions? PasswordPolicy { get; set; }

    /// <summary>
    /// 登录策略选项
    /// </summary>
    public HbtLoginPolicyOptions? LoginPolicy { get; set; }

    /// <summary>
    /// 会话选项
    /// </summary>
    public HbtSessionOptions? Session { get; set; }

    /// <summary>
    /// CSRF Token过期时间(分钟)
    /// </summary>
    public int CsrfTokenExpirationMinutes { get; set; } = 30;

    /// <summary>
    /// 会话过期时间(分钟)
    /// </summary>
    public int SessionExpirationMinutes { get; set; } = 30;
}