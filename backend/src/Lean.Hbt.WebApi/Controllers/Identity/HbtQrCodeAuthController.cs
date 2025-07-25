//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtQrCodeAuthController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 二维码认证控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Application.Services.Identity;
using Lean.Hbt.Common.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Lean.Hbt.WebApi.Controllers.Identity;

/// <summary>
/// 二维码认证控制器
/// </summary>
/// <remarks>
/// 本控制器负责处理:
/// 1. 生成二维码
/// 2. 检查二维码状态
/// 3. 扫描二维码
/// 4. 确认二维码登录
/// 5. 拒绝二维码登录
/// </remarks>
[ApiController]
[Route("api/[controller]")]
[ApiModule("identity", "身份认证")]
public class HbtQrCodeAuthController : HbtBaseController
{
    private readonly IHbtQrCodeService _qrCodeService;

    /// <summary>
    /// 构造函数
    /// <param name="qrCodeService">二维码服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    /// </summary>
    public HbtQrCodeAuthController(
        IHbtQrCodeService qrCodeService,
        IHbtLogger logger,
        IHbtCurrentUser currentUser,
        IHbtLocalizationService localization) : base(logger, currentUser, localization)
    {
        _qrCodeService = qrCodeService ?? throw new ArgumentNullException(nameof(qrCodeService));
    }

    /// <summary>
    /// 生成二维码
    /// </summary>
    /// <param name="request">生成二维码请求</param>
    /// <returns>二维码信息</returns>
    [HttpPost("generate")]
    [AllowAnonymous]
    public async Task<IActionResult> GenerateQrCode([FromBody] HbtGenerateQrCodeRequest request)
    {
        try
        {
            _logger.Info("[二维码认证] 开始生成二维码: QrCodeType={QrCodeType}", request.QrCodeType);

            var result = await _qrCodeService.GenerateQrCodeAsync(request);

            _logger.Info("[二维码认证] 二维码生成成功: QrCodeId={QrCodeId}", result.QrCodeId);
            return Success(result);
        }
        catch (HbtException ex)
        {
            _logger.Warn("[二维码认证] 生成二维码失败: QrCodeType={QrCodeType}, Error={Error}", request.QrCodeType, ex.Message);
            return Error(ex.Message, ex.Code);
        }
        catch (Exception ex)
        {
            _logger.Error("[二维码认证] 生成二维码异常: QrCodeType={QrCodeType}, Error={Error}", request.QrCodeType, ex.Message);
            return Error("服务器内部错误", int.Parse(HbtConstants.ErrorCodes.ServerError));
        }
    }

    /// <summary>
    /// 检查二维码状态
    /// </summary>
    /// <param name="request">检查状态请求</param>
    /// <returns>二维码状态</returns>
    [HttpPost("check-status")]
    [AllowAnonymous]
    public async Task<IActionResult> CheckQrCodeStatus([FromBody] HbtCheckQrCodeStatusRequest request)
    {
        try
        {
            _logger.Info("[二维码认证] 开始检查二维码状态: QrCodeId={QrCodeId}", request.QrCodeId);

            var result = await _qrCodeService.CheckQrCodeStatusAsync(request);

            _logger.Info("[二维码认证] 二维码状态检查完成: QrCodeId={QrCodeId}, Status={Status}", request.QrCodeId, result.Status);
            return Success(result);
        }
        catch (HbtException ex)
        {
            _logger.Warn("[二维码认证] 检查二维码状态失败: QrCodeId={QrCodeId}, Error={Error}", request.QrCodeId, ex.Message);
            return Error(ex.Message, ex.Code);
        }
        catch (Exception ex)
        {
            _logger.Error("[二维码认证] 检查二维码状态异常: QrCodeId={QrCodeId}, Error={Error}", request.QrCodeId, ex.Message);
            return Error("服务器内部错误", int.Parse(HbtConstants.ErrorCodes.ServerError));
        }
    }

    /// <summary>
    /// 扫描二维码
    /// </summary>
    /// <param name="qrCodeId">二维码ID</param>
    /// <returns>扫描结果</returns>
    [HttpPost("scan/{qrCodeId}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> ScanQrCode(string qrCodeId)
    {
        try
        {
            var userId = HttpContext.User.FindFirst("uid")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                _logger.Warn("[二维码认证] 未找到用户ID");
                return Error("未登录", int.Parse(HbtConstants.ErrorCodes.Unauthorized));
            }

            _logger.Info("[二维码认证] 开始扫描二维码: QrCodeId={QrCodeId}, UserId={UserId}", qrCodeId, userId);

            var success = await _qrCodeService.ScanQrCodeAsync(qrCodeId, long.Parse(userId));

            if (success)
            {
                _logger.Info("[二维码认证] 二维码扫描成功: QrCodeId={QrCodeId}, UserId={UserId}", qrCodeId, userId);
                return Success(new { success = true });
            }
            else
            {
                _logger.Warn("[二维码认证] 二维码扫描失败: QrCodeId={QrCodeId}, UserId={UserId}", qrCodeId, userId);
                return Success(new { success = false });
            }
        }
        catch (Exception ex)
        {
            _logger.Error("[二维码认证] 扫描二维码异常: QrCodeId={QrCodeId}, Error={Error}", qrCodeId, ex.Message);
            return Error("服务器内部错误", int.Parse(HbtConstants.ErrorCodes.ServerError));
        }
    }

    /// <summary>
    /// 确认二维码登录
    /// </summary>
    /// <param name="request">确认登录请求</param>
    /// <returns>确认结果</returns>
    [HttpPost("confirm")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> ConfirmQrCodeLogin([FromBody] HbtConfirmQrCodeLoginRequest request)
    {
        try
        {
            var userId = HttpContext.User.FindFirst("uid")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                _logger.Warn("[二维码认证] 未找到用户ID");
                return Error("未登录", int.Parse(HbtConstants.ErrorCodes.Unauthorized));
            }

            // 确保用户只能确认自己的登录
            if (long.Parse(userId) != request.UserId)
            {
                _logger.Warn("[二维码认证] 用户ID不匹配: ExpectedUserId={ExpectedUserId}, ActualUserId={ActualUserId}", 
                    request.UserId, userId);
                return Error("权限不足", int.Parse(HbtConstants.ErrorCodes.Forbidden));
            }

            _logger.Info("[二维码认证] 开始确认二维码登录: QrCodeId={QrCodeId}, UserId={UserId}", request.QrCodeId, request.UserId);

            var result = await _qrCodeService.ConfirmQrCodeLoginAsync(request);

            if (result.Success)
            {
                _logger.Info("[二维码认证] 二维码登录确认成功: QrCodeId={QrCodeId}, UserId={UserId}", request.QrCodeId, request.UserId);
            }
            else
            {
                _logger.Warn("[二维码认证] 二维码登录确认失败: QrCodeId={QrCodeId}, UserId={UserId}, Message={Message}", 
                    request.QrCodeId, request.UserId, result.Message);
            }

            return Success(result);
        }
        catch (Exception ex)
        {
            _logger.Error("[二维码认证] 确认二维码登录异常: QrCodeId={QrCodeId}, Error={Error}", request.QrCodeId, ex.Message);
            return Error("服务器内部错误", int.Parse(HbtConstants.ErrorCodes.ServerError));
        }
    }

    /// <summary>
    /// 拒绝二维码登录
    /// </summary>
    /// <param name="qrCodeId">二维码ID</param>
    /// <returns>拒绝结果</returns>
    [HttpPost("reject/{qrCodeId}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> RejectQrCodeLogin(string qrCodeId)
    {
        try
        {
            var userId = HttpContext.User.FindFirst("uid")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                _logger.Warn("[二维码认证] 未找到用户ID");
                return Error("未登录", int.Parse(HbtConstants.ErrorCodes.Unauthorized));
            }

            _logger.Info("[二维码认证] 开始拒绝二维码登录: QrCodeId={QrCodeId}, UserId={UserId}", qrCodeId, userId);

            var success = await _qrCodeService.RejectQrCodeLoginAsync(qrCodeId, long.Parse(userId));

            if (success)
            {
                _logger.Info("[二维码认证] 二维码登录拒绝成功: QrCodeId={QrCodeId}, UserId={UserId}", qrCodeId, userId);
                return Success(new { success = true });
            }
            else
            {
                _logger.Warn("[二维码认证] 二维码登录拒绝失败: QrCodeId={QrCodeId}, UserId={UserId}", qrCodeId, userId);
                return Success(new { success = false });
            }
        }
        catch (Exception ex)
        {
            _logger.Error("[二维码认证] 拒绝二维码登录异常: QrCodeId={QrCodeId}, Error={Error}", qrCodeId, ex.Message);
            return Error("服务器内部错误", int.Parse(HbtConstants.ErrorCodes.ServerError));
        }
    }

    /// <summary>
    /// 取消二维码
    /// </summary>
    /// <param name="qrCodeId">二维码ID</param>
    /// <returns>取消结果</returns>
    [HttpPost("cancel/{qrCodeId}")]
    [AllowAnonymous]
    public async Task<IActionResult> CancelQrCode(string qrCodeId)
    {
        try
        {
            _logger.Info("[二维码认证] 开始取消二维码: QrCodeId={QrCodeId}", qrCodeId);

            var success = await _qrCodeService.CancelQrCodeAsync(qrCodeId);

            if (success)
            {
                _logger.Info("[二维码认证] 二维码取消成功: QrCodeId={QrCodeId}", qrCodeId);
                return Success(new { success = true });
            }
            else
            {
                _logger.Warn("[二维码认证] 二维码取消失败: QrCodeId={QrCodeId}", qrCodeId);
                return Success(new { success = false });
            }
        }
        catch (Exception ex)
        {
            _logger.Error("[二维码认证] 取消二维码异常: QrCodeId={QrCodeId}, Error={Error}", qrCodeId, ex.Message);
            return Error("服务器内部错误", int.Parse(HbtConstants.ErrorCodes.ServerError));
        }
    }

    /// <summary>
    /// 清理过期二维码
    /// </summary>
    /// <returns>清理结果</returns>
    [HttpPost("cleanup")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> CleanupExpiredQrCodes()
    {
        try
        {
            _logger.Info("[二维码认证] 开始清理过期二维码");

            var count = await _qrCodeService.CleanupExpiredQrCodesAsync();

            _logger.Info("[二维码认证] 过期二维码清理完成: Count={Count}", count);

            return Success(new { cleanedCount = count });
        }
        catch (Exception ex)
        {
            _logger.Error("[二维码认证] 清理过期二维码异常: Error={Error}", ex.Message);
            return Error("服务器内部错误", int.Parse(HbtConstants.ErrorCodes.ServerError));
        }
    }
} 