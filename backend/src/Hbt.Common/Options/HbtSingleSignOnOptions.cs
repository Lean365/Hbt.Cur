using System.ComponentModel.DataAnnotations;

namespace Hbt.Common.Options
{
    /// <summary>
    /// 单点登录配置选项
    /// </summary>
    public class HbtSingleSignOnOptions
    {
        /// <summary>
        /// 最大设备数
        /// </summary>
        public int MaxDevices { get; set; }

        /// <summary>
        /// 是否启用单点登录
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 是否踢出旧会话
        /// </summary>
        public bool KickoutOldSession { get; set; }

        /// <summary>
        /// 是否通知被踢出的用户
        /// </summary>
        public bool NotifyKickout { get; set; }

        /// <summary>
        /// 踢出提示消息
        /// </summary>
        public string? KickoutMessage { get; set; }

        /// <summary>
        /// 最大并发会话数
        /// </summary>
        public int MaxConcurrentSessions { get; set; }

        /// <summary>
        /// 每个用户最大连接数
        /// </summary>
        public int MaxConnectionsPerUser { get; set; }
    }
} 