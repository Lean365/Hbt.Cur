//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSmsAuthController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 短信认证控制器
//===================================================================

using Hbt.Application.Dtos.Identity;
using Hbt.Application.Services.Identity;
using Hbt.Common.Constants;

namespace Hbt.WebApi.Controllers.Identity;

/// <summary>
/// 短信认证控制器
/// </summary>
/// <remarks>
/// 本控制器负责处理:
/// 1. 发送短信验证码
/// 2. 短信验证码登录
/// 3. 验证码相关操作
/// </remarks>
[ApiController]
[Route("api/[controller]")]
[ApiModule("identity", "身份认证")]
public class HbtSmsAuthController : HbtBaseController
{
    private readonly IHbtSmsService _smsService;

    /// <summary>
    /// 构造函数
    /// <param name="smsService">短信服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    /// </summary>
    public HbtSmsAuthController(
        IHbtSmsService smsService,
        IHbtLogger logger,
        IHbtCurrentUser currentUser,
        IHbtLocalizationService localization) : base(logger, currentUser, localization)
    {
        _smsService = smsService ?? throw new ArgumentNullException(nameof(smsService));
    }

    /// <summary>
    /// 发送短信验证码
    /// </summary>
    /// <param name="request">发送验证码请求</param>
    /// <returns>发送结果</returns>
    [HttpPost("send-code")]
    [AllowAnonymous]
    public async Task<IActionResult> SendVerificationCode([FromBody] HbtSendSmsCodeRequest request)
    {
        try
        {
            _logger.Info("[短信认证] 开始发送验证码: Phone={Phone}, CodeType={CodeType}", request.Phone, request.CodeType);

            var result = await _smsService.SendVerificationCodeAsync(request);

            if (result.Success)
            {
                _logger.Info("[短信认证] 验证码发送成功: Phone={Phone}", request.Phone);
                return Success(result);
            }
            else
            {
                _logger.Warn("[短信认证] 验证码发送失败: Phone={Phone}, Message={Message}", request.Phone, result.Message);
                return Error(result.Message, int.Parse(HbtConstants.ErrorCodes.ValidationFailed));
            }
        }
        catch (Exception ex)
        {
            _logger.Error("[短信认证] 发送验证码异常: Phone={Phone}, Error={Error}", request.Phone, ex.Message);
            return Error("服务器内部错误", int.Parse(HbtConstants.ErrorCodes.ServerError));
        }
    }

    /// <summary>
    /// 短信验证码登录
    /// </summary>
    /// <param name="request">登录请求</param>
    /// <returns>登录结果</returns>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] HbtSmsLoginRequest request)
    {
        try
        {
            _logger.Info("[短信认证] 开始短信登录: Phone={Phone}", request.Phone);

            var result = await _smsService.LoginAsync(request);

            _logger.Info("[短信认证] 短信登录成功: Phone={Phone}", request.Phone);
            return Success(result);
        }
        catch (HbtException ex)
        {
            _logger.Warn("[短信认证] 短信登录失败: Phone={Phone}, Error={Error}", request.Phone, ex.Message);
            return Error(ex.Message, ex.Code);
        }
        catch (Exception ex)
        {
            _logger.Error("[短信认证] 短信登录异常: Phone={Phone}, Error={Error}", request.Phone, ex.Message);
            return Error("服务器内部错误", int.Parse(HbtConstants.ErrorCodes.ServerError));
        }
    }

    /// <summary>
    /// 验证短信验证码
    /// </summary>
    /// <param name="phone">手机号</param>
    /// <param name="code">验证码</param>
    /// <param name="codeType">验证码类型</param>
    /// <returns>验证结果</returns>
    [HttpPost("verify-code")]
    [AllowAnonymous]
    public async Task<IActionResult> VerifyCode([FromQuery] string phone, [FromQuery] string code, [FromQuery] HbtSmsCodeType codeType)
    {
        try
        {
            _logger.Info("[短信认证] 开始验证验证码: Phone={Phone}, Code={Code}, CodeType={CodeType}", phone, code, codeType);

            var isValid = await _smsService.VerifyCodeAsync(phone, code, codeType);

            if (isValid)
            {
                _logger.Info("[短信认证] 验证码验证成功: Phone={Phone}", phone);
                return Success(new { isValid = true });
            }
            else
            {
                _logger.Warn("[短信认证] 验证码验证失败: Phone={Phone}, Code={Code}", phone, code);
                return Success(new { isValid = false });
            }
        }
        catch (Exception ex)
        {
            _logger.Error("[短信认证] 验证验证码异常: Phone={Phone}, Error={Error}", phone, ex.Message);
            return Error("服务器内部错误", int.Parse(HbtConstants.ErrorCodes.ServerError));
        }
    }

    /// <summary>
    /// 检查发送频率限制
    /// </summary>
    /// <param name="phone">手机号</param>
    /// <param name="codeType">验证码类型</param>
    /// <returns>检查结果</returns>
    [HttpGet("check-limit")]
    [AllowAnonymous]
    public async Task<IActionResult> CheckSendLimit([FromQuery] string phone, [FromQuery] HbtSmsCodeType codeType)
    {
        try
        {
            _logger.Info("[短信认证] 开始检查发送频率限制: Phone={Phone}, CodeType={CodeType}", phone, codeType);

            var (canSend, remainingAttempts, nextSendTime) = await _smsService.CheckSendLimitAsync(phone, codeType);

            _logger.Info("[短信认证] 发送频率限制检查完成: Phone={Phone}, CanSend={CanSend}, RemainingAttempts={RemainingAttempts}",
                phone, canSend, remainingAttempts);

            return Success(new
            {
                canSend,
                remainingAttempts,
                nextSendTime
            });
        }
        catch (Exception ex)
        {
            _logger.Error("[短信认证] 检查发送频率限制异常: Phone={Phone}, Error={Error}", phone, ex.Message);
            return Error("服务器内部错误", int.Parse(HbtConstants.ErrorCodes.ServerError));
        }
    }

    /// <summary>
    /// 清理过期验证码
    /// </summary>
    /// <returns>清理结果</returns>
    [HttpPost("cleanup")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> CleanupExpiredCodes()
    {
        try
        {
            _logger.Info("[短信认证] 开始清理过期验证码");

            var count = await _smsService.CleanupExpiredCodesAsync();

            _logger.Info("[短信认证] 过期验证码清理完成: Count={Count}", count);

            return Success(new { cleanedCount = count });
        }
        catch (Exception ex)
        {
            _logger.Error("[短信认证] 清理过期验证码异常: Error={Error}", ex.Message);
            return Error("服务器内部错误", int.Parse(HbtConstants.ErrorCodes.ServerError));
        }
    }
}