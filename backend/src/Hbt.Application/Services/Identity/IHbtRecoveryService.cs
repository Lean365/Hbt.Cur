//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtPasswordRecoveryService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 找回密码服务接口
//===================================================================

using Hbt.Cur.Application.Dtos.Identity;

namespace Hbt.Cur.Application.Services.Identity
{
    /// <summary>
    /// 找回密码服务接口
    /// </summary>
    public interface IHbtRecoveryService
    {
        /// <summary>
        /// 验证用户身份
        /// </summary>
        /// <param name="request">身份验证请求</param>
        /// <returns>身份验证响应</returns>
        Task<HbtIdentityVerificationResponse> VerifyIdentityAsync(HbtIdentityVerificationRequest request);

        /// <summary>
        /// 发送邮箱验证码
        /// </summary>
        /// <param name="request">发送验证码请求</param>
        /// <returns>发送验证码响应</returns>
        Task<HbtSendVerificationCodeResponse> SendVerificationCodeAsync(HbtSendVerificationCodeRequest request);

        /// <summary>
        /// 验证邮箱验证码
        /// </summary>
        /// <param name="request">邮箱验证请求</param>
        /// <returns>邮箱验证响应</returns>
        Task<HbtEmailVerificationResponse> VerifyEmailCodeAsync(HbtEmailVerificationRequest request);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="request">密码重置请求</param>
        /// <returns>密码重置响应</returns>
        Task<HbtPasswordResetResponse> ResetPasswordAsync(HbtPasswordResetRequest request);
    }
} 