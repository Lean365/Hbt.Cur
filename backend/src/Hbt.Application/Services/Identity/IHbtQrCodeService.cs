//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtQrCodeService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 二维码服务接口
//===================================================================

using Hbt.Cur.Application.Dtos.Identity;

namespace Hbt.Cur.Application.Services.Identity;

/// <summary>
/// 二维码服务接口
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public interface IHbtQrCodeService
{
    /// <summary>
    /// 生成二维码
    /// </summary>
    /// <param name="request">生成二维码请求</param>
    /// <returns>二维码信息</returns>
    Task<HbtGenerateQrCodeResponse> GenerateQrCodeAsync(HbtGenerateQrCodeRequest request);

    /// <summary>
    /// 检查二维码状态
    /// </summary>
    /// <param name="request">检查状态请求</param>
    /// <returns>二维码状态</returns>
    Task<HbtCheckQrCodeStatusResponse> CheckQrCodeStatusAsync(HbtCheckQrCodeStatusRequest request);

    /// <summary>
    /// 扫描二维码
    /// </summary>
    /// <param name="qrCodeId">二维码ID</param>
    /// <param name="userId">扫描用户ID</param>
    /// <returns>是否成功</returns>
    Task<bool> ScanQrCodeAsync(string qrCodeId, long userId);

    /// <summary>
    /// 确认二维码登录
    /// </summary>
    /// <param name="request">确认登录请求</param>
    /// <returns>确认结果</returns>
    Task<HbtConfirmQrCodeLoginResponse> ConfirmQrCodeLoginAsync(HbtConfirmQrCodeLoginRequest request);

    /// <summary>
    /// 拒绝二维码登录
    /// </summary>
    /// <param name="qrCodeId">二维码ID</param>
    /// <param name="userId">用户ID</param>
    /// <returns>是否成功</returns>
    Task<bool> RejectQrCodeLoginAsync(string qrCodeId, long userId);

    /// <summary>
    /// 取消二维码
    /// </summary>
    /// <param name="qrCodeId">二维码ID</param>
    /// <returns>是否成功</returns>
    Task<bool> CancelQrCodeAsync(string qrCodeId);

    /// <summary>
    /// 清理过期的二维码
    /// </summary>
    /// <returns>清理数量</returns>
    Task<int> CleanupExpiredQrCodesAsync();
} 