//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtLoginService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V1.0.0
// 描述    : 登录服务接口
//===================================================================

using System.Threading.Tasks;
using Lean.Hbt.Application.Dtos.Identity;

namespace Lean.Hbt.Application.Services.Identity;

/// <summary>
/// 登录服务接口
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public interface IHbtLoginService
{
    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="loginDto">登录信息</param>
    /// <returns>登录结果</returns>
    Task<HbtLoginResultDto> LoginAsync(HbtLoginDto loginDto);

    /// <summary>
    /// 刷新令牌
    /// </summary>
    /// <param name="refreshToken">刷新令牌</param>
    /// <returns>登录结果</returns>
    Task<HbtLoginResultDto> RefreshTokenAsync(string refreshToken);

    /// <summary>
    /// 用户登出
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>是否成功</returns>
    Task<bool> LogoutAsync(long userId);
} 