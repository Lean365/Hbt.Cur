using System;

namespace Lean.Hbt.Domain.Models.SignalR
{
    /// <summary>
    /// 在线用户模型
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtOnlineUser
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 连接ID
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        public string ClientIp { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 最后活动时间
        /// </summary>
        public DateTime LastActiveTime { get; set; }
    }
} 