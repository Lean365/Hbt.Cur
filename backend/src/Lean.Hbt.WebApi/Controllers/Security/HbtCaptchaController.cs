using Lean.Hbt.Application.Dtos.Security;
using Lean.Hbt.Domain.IServices.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lean.Hbt.Domain.IServices.Admin;

namespace Lean.Hbt.WebApi.Controllers.Security;

/// <summary>
/// 验证码控制器
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class HbtCaptchaController : HbtBaseController
{
    private readonly IHbtCaptchaService _captchaService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="captchaService">验证码服务</param>
    /// <param name="localization">本地化服务</param>
    public HbtCaptchaController(IHbtCaptchaService captchaService, IHbtLocalizationService localization) : base(localization)
    {
        _captchaService = captchaService;
    }

    /// <summary>
    /// 生成滑块验证码
    /// </summary>
    [HttpGet("slider")]
    [AllowAnonymous]
    public async Task<ActionResult<SliderCaptchaDto>> GenerateSliderAsync()
    {
        var (bgImage, sliderImage, token) = await _captchaService.GenerateSliderAsync();
        return Ok(new SliderCaptchaDto
        {
            BackgroundImage = bgImage,
            SliderImage = sliderImage,
            Token = token
        });
    }

    /// <summary>
    /// 验证滑块
    /// </summary>
    [HttpPost("slider/validate")]
    [AllowAnonymous]
    public async Task<ActionResult<CaptchaResultDto>> ValidateSliderAsync([FromBody] SliderValidateDto request)
    {
        var isValid = await _captchaService.ValidateSliderAsync(request.Token, request.XOffset);
        return Ok(new CaptchaResultDto
        {
            Success = isValid,
            Message = isValid ? "验证通过" : "验证失败"
        });
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