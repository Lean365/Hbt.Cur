//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtLoginService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 登录服务接口
//===================================================================

using System.Threading.Tasks;
using Hbt.Application.Dtos.Identity;
using Hbt.Domain.Entities.Identity;

namespace Hbt.Application.Services.Identity;

/// <summary>
/// 登录服务接口
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public interface IHbtAuthService
{
    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="loginDto">登录信息</param>
    /// <returns>登录结果</returns>
    Task<HbtLoginResultDto> LoginAsync(HbtAuthDto loginDto);

    /// <summary>
    /// 用户登出
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>是否成功</returns>
    Task<bool> LogoutAsync(long userId);

    /// <summary>
    /// 刷新访问令牌
    /// </summary>
    /// <param name="refreshToken">刷新令牌</param>
    /// <returns>新的访问令牌</returns>
    Task<HbtLoginResultDto> RefreshTokenAsync(string refreshToken);

    /// <summary>
    /// 获取用户盐值
    /// </summary>
    /// <param name="username">用户名</param>
    /// <returns>用户盐值信息</returns>
    Task<(string Salt, int Iterations)?> GetUserSaltAsync(string username);

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户信息</returns>
    Task<HbtUserInfoDto> GetUserInfoAsync(long userId);

} 