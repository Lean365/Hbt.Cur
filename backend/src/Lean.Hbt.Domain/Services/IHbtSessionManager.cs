using System.Threading.Tasks;

namespace Lean.Hbt.Domain.Services
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
        /// <returns>会话信息</returns>
        Task<HbtSessionInfo> CreateSessionAsync(string userId);

        /// <summary>
        /// 单点登出
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="excludeSessionId">排除的会话ID</param>
        /// <returns>异步任务</returns>
        Task SingleSignOutAsync(string userId, string excludeSessionId = null);
    }

    /// <summary>
    /// 会话信息
    /// </summary>
    public class HbtSessionInfo
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
