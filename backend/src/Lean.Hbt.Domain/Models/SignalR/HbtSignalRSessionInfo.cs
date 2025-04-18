namespace Lean.Hbt.Domain.Models.SignalR
{
    /// <summary>
    /// SignalR会话信息
    /// </summary>
    public class HbtSignalRSessionInfo
    {
        /// <summary>
        /// 会话ID
        /// </summary>
        public string? SessionId { get; set; }

        /// <summary>
        /// 刷新令牌
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// 过期时间（分钟）
        /// </summary>
        public int ExpiresIn { get; set; }
    }
}