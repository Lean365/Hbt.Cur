//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : IHbtJwtHandler.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-16 10:00
// 版本号 : V0.0.1
// 描述    : JWT令牌处理器接口
//===================================================================

using System.Security.Claims;

namespace Lean.Hbt.Infrastructure.Authentication
{
    /// <summary>
    /// JWT令牌处理器接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    public interface IHbtJwtHandler
    {
        /// <summary>
        /// 生成JWT令牌
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="roles">用户角色列表</param>
        /// <returns>JWT令牌字符串</returns>
        string GenerateToken(string userId, string userName, IEnumerable<string> roles);

        /// <summary>
        /// 验证JWT令牌
        /// </summary>
        /// <param name="token">JWT令牌字符串</param>
        /// <returns>验证通过返回声明主体，否则返回null</returns>
        ClaimsPrincipal? ValidateToken(string token);
    }
} 