//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtSignalRSessionManager.cs
// 创建者 : Lean365
// 创建时间: 2024-01-24 10:00
// 版本号 : V1.0.0
// 描述    : SignalR实时通信会话管理接口
//===================================================================

using System.Threading.Tasks;

namespace Lean.Hbt.Domain.IServices.SignalR
{
    /// <summary>
    /// SignalR实时通信会话管理接口
    /// </summary>
    public interface IHbtSignalRSessionManager
    {
        /// <summary>
        /// 创建会话
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>会话信息</returns>
        Task<HbtSignalRSessionInfo> CreateSessionAsync(string userId);

        /// <summary>
        /// 单点登出
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="excludeSessionId">排除的会话ID</param>
        /// <returns>异步任务</returns>
        Task SingleSignOutAsync(string userId, string excludeSessionId = null);
    }

    /// <summary>
    /// SignalR实时通信会话信息
    /// </summary>
    public class HbtSignalRSessionInfo
    {
        /// <summary>
        /// 会话ID
        /// </summary>
        public string SessionId { get; set; } = string.Empty;

        /// <summary>
        /// 刷新令牌
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;

        /// <summary>
        /// 过期时间(分钟)
        /// </summary>
        public int ExpiresIn { get; set; }
    }
} 