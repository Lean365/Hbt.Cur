//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtIdentitySessionManager.cs
// 创建者 : Lean365
// 创建时间: 2024-01-24 10:00
// 版本号 : V0.0.1
// 描述    : 身份认证会话管理接口
//===================================================================

using System.Threading.Tasks;
using System.Collections.Generic;
using Hbt.Domain.Models.Identity;

namespace Hbt.Domain.IServices.Extensions
{
    /// <summary>
    /// 身份认证会话管理接口
    /// </summary>
    public interface IHbtIdentitySessionManager
    {
        /// <summary>
        /// 创建会话
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="ipAddress">IP地址</param>
        /// <param name="userAgent">用户代理</param>
        /// <returns>会话ID</returns>
        Task<string> CreateSessionAsync(string userId, string userName, string ipAddress, string userAgent);

        /// <summary>
        /// 获取会话信息
        /// </summary>
        /// <param name="sessionId">会话ID</param>
        /// <returns>会话信息</returns>
        Task<HbtIdentitySessionInfo> GetSessionInfoAsync(string sessionId);

        /// <summary>
        /// 获取用户所有会话
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>会话信息列表</returns>
        Task<List<HbtIdentitySessionInfo>> GetUserSessionsAsync(string userId);

        /// <summary>
        /// 更新会话访问时间
        /// </summary>
        /// <param name="sessionId">会话ID</param>
        Task UpdateSessionAccessTimeAsync(string sessionId);

        /// <summary>
        /// 移除会话
        /// </summary>
        /// <param name="sessionId">会话ID</param>
        Task RemoveSessionAsync(string sessionId);

        /// <summary>
        /// 移除用户所有会话
        /// </summary>
        /// <param name="userId">用户ID</param>
        Task RemoveUserSessionsAsync(string userId);
    }
} 