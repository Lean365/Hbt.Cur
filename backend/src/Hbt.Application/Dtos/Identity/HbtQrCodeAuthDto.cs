//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtQrCodeAuthDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 二维码登录数据传输对象
//===================================================================

#nullable enable

using System.ComponentModel.DataAnnotations;
using Hbt.Cur.Common.Enums;

namespace Hbt.Cur.Application.Dtos.Identity;

/// <summary>
/// 生成二维码登录请求DTO
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public class HbtGenerateQrCodeRequest
{
    /// <summary>
    /// 二维码类型
    /// </summary>
    public HbtQrCodeType QrCodeType { get; set; } = HbtQrCodeType.Login;

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
/// 生成二维码登录响应DTO
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public class HbtGenerateQrCodeResponse
{
    /// <summary>
    /// 二维码ID
    /// </summary>
    public string QrCodeId { get; set; } = string.Empty;

    /// <summary>
    /// 二维码内容
    /// </summary>
    public string QrCodeContent { get; set; } = string.Empty;

    /// <summary>
    /// 二维码图片（Base64）
    /// </summary>
    public string QrCodeImage { get; set; } = string.Empty;

    /// <summary>
    /// 过期时间（秒）
    /// </summary>
    public int ExpiresIn { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// 检查二维码状态请求DTO
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public class HbtCheckQrCodeStatusRequest
{
    /// <summary>
    /// 二维码ID
    /// </summary>
    [Required(ErrorMessage = "二维码ID不能为空")]
    public string QrCodeId { get; set; } = string.Empty;
}

/// <summary>
/// 检查二维码状态响应DTO
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public class HbtCheckQrCodeStatusResponse
{
    /// <summary>
    /// 二维码状态
    /// </summary>
    public HbtQrCodeStatus Status { get; set; }

    /// <summary>
    /// 用户信息（如果已确认）
    /// </summary>
    public HbtUserInfoDto? UserInfo { get; set; }

    /// <summary>
    /// 访问令牌（如果已确认）
    /// </summary>
    public string? AccessToken { get; set; }

    /// <summary>
    /// 刷新令牌（如果已确认）
    /// </summary>
    public string? RefreshToken { get; set; }

    /// <summary>
    /// 过期时间（秒）
    /// </summary>
    public int? ExpiresIn { get; set; }

    /// <summary>
    /// 消息
    /// </summary>
    public string Message { get; set; } = string.Empty;
}

/// <summary>
/// 确认二维码登录请求DTO
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public class HbtConfirmQrCodeLoginRequest
{
    /// <summary>
    /// 二维码ID
    /// </summary>
    [Required(ErrorMessage = "二维码ID不能为空")]
    public string QrCodeId { get; set; } = string.Empty;

    /// <summary>
    /// 用户ID
    /// </summary>
    [Required(ErrorMessage = "用户ID不能为空")]
    public long UserId { get; set; }

    /// <summary>
    /// 确认操作
    /// </summary>
    public bool Confirm { get; set; } = true;
}

/// <summary>
/// 确认二维码登录响应DTO
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public class HbtConfirmQrCodeLoginResponse
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 消息
    /// </summary>
    public string Message { get; set; } = string.Empty;
}

/// <summary>
/// 二维码类型枚举
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public enum HbtQrCodeType
{
    /// <summary>
    /// 登录二维码
    /// </summary>
    Login = 1,

    /// <summary>
    /// 绑定设备二维码
    /// </summary>
    BindDevice = 2,

    /// <summary>
    /// 授权登录二维码
    /// </summary>
    Authorize = 3,

    /// <summary>
    /// 微信扫码登录二维码
    /// </summary>
    WeChatLogin = 4,

    /// <summary>
    /// 支付宝扫码登录二维码
    /// </summary>
    AlipayLogin = 5
}

/// <summary>
/// 二维码状态枚举
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public enum HbtQrCodeStatus
{
    /// <summary>
    /// 等待扫描
    /// </summary>
    Waiting = 0,

    /// <summary>
    /// 已扫描
    /// </summary>
    Scanned = 1,

    /// <summary>
    /// 已确认
    /// </summary>
    Confirmed = 2,

    /// <summary>
    /// 已拒绝
    /// </summary>
    Rejected = 3,

    /// <summary>
    /// 已过期
    /// </summary>
    Expired = 4,

    /// <summary>
    /// 已取消
    /// </summary>
    Cancelled = 5
} 