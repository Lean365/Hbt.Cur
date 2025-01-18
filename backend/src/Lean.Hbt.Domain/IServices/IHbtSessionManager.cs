//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : IHbtSessionManager.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-17 16:30
// 版本号 : V.0.0.1
// 描述    : 会话管理接口
//===================================================================

using System.Threading.Tasks;
using System.Collections.Generic;
using Lean.Hbt.Domain.Models.Identity;

namespace Lean.Hbt.Domain.IServices
{
    /// <summary>
    /// 会话管理接口
    /// </summary>
    public interface IHbtSessionManager
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
        Task<HbtSessionInfo> GetSessionInfoAsync(string sessionId);

        /// <summary>
        /// 获取用户所有会话
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>会话列表</returns>
        Task<List<HbtSessionInfo>> GetUserSessionsAsync(string userId);

        /// <summary>
        /// 移除会话
        /// </summary>
        /// <param name="sessionId">会话ID</param>
        /// <returns>异步任务</returns>
        Task RemoveSessionAsync(string sessionId);

        /// <summary>
        /// 移除用户所有会话
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>异步任务</returns>
        Task RemoveUserSessionsAsync(string userId);

        /// <summary>
        /// 更新会话访问时间
        /// </summary>
        /// <param name="sessionId">会话ID</param>
        /// <returns>异步任务</returns>
        Task UpdateSessionAccessTimeAsync(string sessionId);
    }
} 