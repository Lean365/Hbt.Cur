//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtSmsService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 短信服务接口
//===================================================================

using Lean.Hbt.Application.Dtos.Identity;

namespace Lean.Hbt.Application.Services.Identity;

/// <summary>
/// 短信服务接口
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public interface IHbtSmsService
{
    /// <summary>
    /// 发送短信验证码
    /// </summary>
    /// <param name="request">发送验证码请求</param>
    /// <returns>发送结果</returns>
    Task<HbtSendSmsCodeResponse> SendVerificationCodeAsync(HbtSendSmsCodeRequest request);

    /// <summary>
    /// 验证短信验证码
    /// </summary>
    /// <param name="phone">手机号</param>
    /// <param name="code">验证码</param>
    /// <param name="codeType">验证码类型</param>
    /// <returns>验证结果</returns>
    Task<bool> VerifyCodeAsync(string phone, string code, HbtSmsCodeType codeType);

    /// <summary>
    /// 短信验证码登录
    /// </summary>
    /// <param name="request">登录请求</param>
    /// <returns>登录结果</returns>
    Task<HbtLoginResultDto> LoginAsync(HbtSmsLoginRequest request);

    /// <summary>
    /// 检查手机号发送频率限制
    /// </summary>
    /// <param name="phone">手机号</param>
    /// <param name="codeType">验证码类型</param>
    /// <returns>检查结果</returns>
    Task<(bool CanSend, int RemainingAttempts, int NextSendTime)> CheckSendLimitAsync(string phone, HbtSmsCodeType codeType);

    /// <summary>
    /// 清理过期的验证码
    /// </summary>
    /// <returns>清理数量</returns>
    Task<int> CleanupExpiredCodesAsync();
} 