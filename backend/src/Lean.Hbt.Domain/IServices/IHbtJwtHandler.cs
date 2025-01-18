//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtJwtHandler.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 17:30
// 版本号 : V1.0.0
// 描述    : JWT处理接口
//===================================================================

namespace Lean.Hbt.Domain.IServices
{
    /// <summary>
    /// JWT处理接口
    /// </summary>
    public interface IHbtJwtHandler
    {
        /// <summary>
        /// 创建访问令牌
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="roles">角色列表</param>
        /// <returns>访问令牌</returns>
        string CreateAccessToken(long userId, string userName, IEnumerable<string> roles);

        /// <summary>
        /// 创建刷新令牌
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>刷新令牌</returns>
        string CreateRefreshToken(long userId);

        /// <summary>
        /// 验证访问令牌
        /// </summary>
        /// <param name="token">访问令牌</param>
        /// <returns>是否有效</returns>
        bool ValidateAccessToken(string token);

        /// <summary>
        /// 验证刷新令牌
        /// </summary>
        /// <param name="token">刷新令牌</param>
        /// <returns>是否有效</returns>
        bool ValidateRefreshToken(string token);

        /// <summary>
        /// 从令牌中获取用户ID
        /// </summary>
        /// <param name="token">令牌</param>
        /// <returns>用户ID</returns>
        long GetUserIdFromToken(string token);
    }
} 