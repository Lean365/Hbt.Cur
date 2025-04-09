using System.Threading.Tasks;
using Lean.Hbt.Domain.Entities.Identity;

namespace Lean.Hbt.Domain.IServices.Security;

/// <summary>
/// JWT处理器接口
/// </summary>
public interface IHbtJwtHandler
{
    /// <summary>
    /// 生成访问令牌
    /// </summary>
    /// <param name="user">用户信息</param>
    /// <param name="tenant">租户信息</param>
    /// <param name="roles">角色列表</param>
    /// <param name="permissions">权限列表</param>
    /// <returns>访问令牌</returns>
    Task<string> GenerateAccessTokenAsync(HbtUser user, HbtTenant tenant, string[] roles, string[] permissions);

    /// <summary>
    /// 生成刷新令牌
    /// </summary>
    /// <returns>刷新令牌</returns>
    Task<string> GenerateRefreshTokenAsync();

    /// <summary>
    /// 验证访问令牌
    /// </summary>
    /// <param name="token">访问令牌</param>
    /// <returns>验证结果</returns>
    Task<bool> ValidateAccessTokenAsync(string token);

    /// <summary>
    /// 验证刷新令牌
    /// </summary>
    /// <param name="token">刷新令牌</param>
    /// <returns>验证结果</returns>
    Task<bool> ValidateRefreshTokenAsync(string token);

    /// <summary>
    /// 从令牌中获取用户ID
    /// </summary>
    /// <param name="token">访问令牌</param>
    /// <returns>用户ID</returns>
    long GetUserIdFromToken(string token);
} 