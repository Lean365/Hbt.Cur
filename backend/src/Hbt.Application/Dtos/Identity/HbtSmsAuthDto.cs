//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSmsAuthDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 短信登录数据传输对象
//===================================================================

#nullable enable

using System.ComponentModel.DataAnnotations;
using Hbt.Common.Enums;

namespace Hbt.Application.Dtos.Identity;

/// <summary>
/// 发送短信验证码请求DTO
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public class HbtSendSmsCodeRequest
{
    /// <summary>
    /// 手机号
    /// </summary>
    [Required(ErrorMessage = "手机号不能为空")]
    [RegularExpression(@"^1[3-9]\d{9}$", ErrorMessage = "手机号格式不正确")]
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// 验证码类型
    /// </summary>
    public HbtSmsCodeType CodeType { get; set; } = HbtSmsCodeType.Login;

    /// <summary>
    /// 图形验证码Token（如果需要）
    /// </summary>
    public string? CaptchaToken { get; set; }

    /// <summary>
    /// 图形验证码偏移量（如果需要）
    /// </summary>
    public int CaptchaOffset { get; set; }
}

/// <summary>
/// 发送短信验证码响应DTO
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public class HbtSendSmsCodeResponse
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 消息
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// 剩余发送次数
    /// </summary>
    public int RemainingAttempts { get; set; }

    /// <summary>
    /// 下次可发送时间（秒）
    /// </summary>
    public int NextSendTime { get; set; }
}

/// <summary>
/// 短信登录请求DTO
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public class HbtSmsLoginRequest
{
    /// <summary>
    /// 手机号
    /// </summary>
    [Required(ErrorMessage = "手机号不能为空")]
    [RegularExpression(@"^1[3-9]\d{9}$", ErrorMessage = "手机号格式不正确")]
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// 验证码
    /// </summary>
    [Required(ErrorMessage = "验证码不能为空")]
    [RegularExpression(@"^\d{6}$", ErrorMessage = "验证码应为6位数字")]
    public string VerificationCode { get; set; } = string.Empty;

    /// <summary>
    /// IP地址
    /// </summary>
    public string IpAddress { get; set; } = string.Empty;

    /// <summary>
    /// 用户代理
    /// </summary>
    public string UserAgent { get; set; } = string.Empty;

    /// <summary>
    /// 登录来源
    /// </summary>
    public int LoginSource { get; set; }

    /// <summary>
    /// 设备信息
    /// </summary>
    public HbtSignalRDevice? DeviceInfo { get; set; }

    /// <summary>
    /// 环境信息
    /// </summary>
    public HbtSignalREnvironment? EnvironmentInfo { get; set; }
}

/// <summary>
/// 短信验证码类型枚举
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public enum HbtSmsCodeType
{
    /// <summary>
    /// 登录验证码
    /// </summary>
    Login = 1,

    /// <summary>
    /// 注册验证码
    /// </summary>
    Register = 2,

    /// <summary>
    /// 找回密码验证码
    /// </summary>
    PasswordRecovery = 3,

    /// <summary>
    /// 绑定手机验证码
    /// </summary>
    BindPhone = 4,

    /// <summary>
    /// 解绑手机验证码
    /// </summary>
    UnbindPhone = 5,

    /// <summary>
    /// 修改手机验证码
    /// </summary>
    ChangePhone = 6
} 