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
    Task<string> GenerateAccessTokenAsync(HbtUser user);

    /// <summary>
    /// 生成刷新令牌
    /// </summary>
    Task<string> GenerateRefreshTokenAsync();
} 