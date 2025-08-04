//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPasswordRecoveryController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 找回密码控制器
//===================================================================

using Hbt.Cur.Application.Dtos.Identity;
using Hbt.Cur.Application.Services.Identity;

namespace Hbt.Cur.WebApi.Controllers.Identity
{
    /// <summary>
    /// 找回密码控制器
    /// </summary>
    [Route("api/[controller]", Name = "找回密码")]
    [ApiController]
    [ApiModule("identity", "身份认证")]
    public class HbtRecoveryController : HbtBaseController
    {
        private readonly IHbtRecoveryService _passwordRecoveryService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="passwordRecoveryService">找回密码服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtRecoveryController(
            IHbtRecoveryService passwordRecoveryService,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, currentUser, localization)
        {
            _passwordRecoveryService = passwordRecoveryService;
        }

        /// <summary>
        /// 验证用户身份
        /// </summary>
        /// <param name="request">身份验证请求</param>
        /// <returns>身份验证响应</returns>
        [HttpPost("verify-identity")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyIdentityAsync([FromBody] HbtIdentityVerificationRequest request)
        {
            try
            {
                _logger.Info("[找回密码] 开始验证用户身份: {UserName}", request.UserName);
                
                var result = await _passwordRecoveryService.VerifyIdentityAsync(request);
                
                _logger.Info("[找回密码] 用户身份验证完成: {UserName}, 结果: {Success}", request.UserName, result.Success);
                
                return Success(result);
            }
            catch (Exception ex)
            {
                _logger.Error("[找回密码] 用户身份验证失败: {UserName}", request.UserName, ex);
                return Error(ex.Message);
            }
        }

        /// <summary>
        /// 发送邮箱验证码
        /// </summary>
        /// <param name="request">发送验证码请求</param>
        /// <returns>发送验证码响应</returns>
        [HttpPost("send-verification-code")]
        [AllowAnonymous]
        public async Task<IActionResult> SendVerificationCodeAsync([FromBody] HbtSendVerificationCodeRequest request)
        {
            try
            {
                _logger.Info("[找回密码] 开始发送验证码: {UserName}, {Email}", request.UserName, request.Email);
                
                var result = await _passwordRecoveryService.SendVerificationCodeAsync(request);
                
                _logger.Info("[找回密码] 验证码发送完成: {UserName}, 结果: {Success}", request.UserName, result.Success);
                
                return Success(result);
            }
            catch (Exception ex)
            {
                _logger.Error("[找回密码] 发送验证码失败: {UserName}, {Email}", request.UserName, request.Email, ex);
                return Error(ex.Message);
            }
        }

        /// <summary>
        /// 验证邮箱验证码
        /// </summary>
        /// <param name="request">邮箱验证请求</param>
        /// <returns>邮箱验证响应</returns>
        [HttpPost("verify-email-code")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmailCodeAsync([FromBody] HbtEmailVerificationRequest request)
        {
            try
            {
                _logger.Info("[找回密码] 开始验证邮箱验证码: {UserName}", request.UserName);
                
                var result = await _passwordRecoveryService.VerifyEmailCodeAsync(request);
                
                _logger.Info("[找回密码] 邮箱验证码验证完成: {UserName}, 结果: {Success}", request.UserName, result.Success);
                
                return Success(result);
            }
            catch (Exception ex)
            {
                _logger.Error("[找回密码] 邮箱验证码验证失败: {UserName}", request.UserName, ex);
                return Error(ex.Message);
            }
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="request">密码重置请求</param>
        /// <returns>密码重置响应</returns>
        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] HbtPasswordResetRequest request)
        {
            try
            {
                _logger.Info("[找回密码] 开始重置密码: {UserName}", request.UserName);
                
                var result = await _passwordRecoveryService.ResetPasswordAsync(request);
                
                _logger.Info("[找回密码] 密码重置完成: {UserName}, 结果: {Success}", request.UserName, result.Success);
                
                return Success(result);
            }
            catch (Exception ex)
            {
                _logger.Error("[找回密码] 密码重置失败: {UserName}", request.UserName, ex);
                return Error(ex.Message);
            }
        }
    }
} 