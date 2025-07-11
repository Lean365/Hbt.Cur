using Lean.Hbt.Application.Dtos.Security;
using Lean.Hbt.Domain.IServices.Security;

namespace Lean.Hbt.WebApi.Controllers.Security;

/// <summary>
/// 验证码控制器
/// </summary>
[Route("api/[controller]", Name = "验证码")]
[ApiController]
[ApiModule("identity", "身份认证")]
public class HbtCaptchaController : HbtBaseController
{
    private readonly IHbtCaptchaService _captchaService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="captchaService">验证码服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public HbtCaptchaController(
        IHbtCaptchaService captchaService,
        IHbtLogger logger,
        IHbtCurrentUser currentUser,
        IHbtLocalizationService localization) : base(logger, currentUser, localization)
    {
        _captchaService = captchaService;
        _logger.Info("验证码控制器已创建");
    }

    /// <summary>
    /// 生成滑块验证码
    /// </summary>
    [HttpGet("slider")]
    [AllowAnonymous]
    public async Task<ActionResult<SliderCaptchaDto>> GenerateSliderAsync()
    {
        _logger.Info("开始生成滑块验证码");
        try
        {
            var (bgImage, sliderImage, token) = await _captchaService.GenerateSliderAsync();
            _logger.Info("滑块验证码生成成功 - Token: {Token}, 时间: {Time}", token, DateTime.Now);
            return Ok(new SliderCaptchaDto
            {
                BackgroundImage = bgImage,
                SliderImage = sliderImage,
                Token = token
            });
        }
        catch (Exception ex)
        {
            _logger.Error("生成滑块验证码时发生错误", ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 验证滑块
    /// </summary>
    [HttpPost("slider/validate")]
    [AllowAnonymous]
    public async Task<ActionResult<CaptchaResultDto>> ValidateSliderAsync([FromBody] SliderValidateDto request)
    {
        _logger.Info("开始验证滑块: Token={Token}, XOffset={XOffset}", request.Token, request.XOffset);
        try
        {
            var isValid = await _captchaService.ValidateSliderAsync(request.Token, request.XOffset);
            _logger.Info("滑块验证结果: {Result}", isValid ? "验证通过" : "验证失败");
            return Ok(new CaptchaResultDto
            {
                Success = isValid,
                Message = isValid ? "验证通过" : "验证失败"
            });
        }
        catch (Exception ex)
        {
            _logger.Error("验证滑块时发生错误", ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 收集行为数据
    /// </summary>
    [HttpPost("behavior/collect")]
    [AllowAnonymous]
    public async Task<ActionResult<string>> CollectBehaviorDataAsync([FromBody] BehaviorDataDto request)
    {
        var token = await _captchaService.CollectBehaviorDataAsync(request.UserId, new Domain.IServices.Security.BehaviorData
        {
            MouseTrack = request.MouseTrack.Select(p => new Domain.IServices.Security.Point
            {
                X = p.X,
                Y = p.Y,
                Timestamp = p.Timestamp
            }).ToList(),
            KeyIntervals = request.KeyIntervals,
            Duration = request.Duration
        });
        return Ok(token);
    }

    /// <summary>
    /// 验证行为特征
    /// </summary>
    [HttpPost("behavior/validate")]
    [AllowAnonymous]
    public async Task<ActionResult<CaptchaResultDto>> ValidateBehaviorAsync([FromBody] string token)
    {
        var isValid = await _captchaService.ValidateBehaviorAsync(token);
        return Ok(new CaptchaResultDto
        {
            Success = isValid,
            Message = isValid ? "验证通过" : "验证失败"
        });
    }
}