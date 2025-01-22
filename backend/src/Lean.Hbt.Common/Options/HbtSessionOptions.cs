//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSessionOptions.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 18:00
// 版本号 : V1.0.0
// 描述    : 会话配置选项
//===================================================================

namespace Lean.Hbt.Common.Options
{
    /// <summary>
    /// 会话配置选项
    /// </summary>
    public class HbtSessionOptions
    {
        /// <summary>
        /// 会话超时时间(分钟)
        /// </summary>
        public int TimeoutMinutes { get; set; } = 30;

        /// <summary>
        /// 是否启用滑动过期
        /// </summary>
        public bool EnableSlidingExpiration { get; set; } = true;

        /// <summary>
        /// 是否启用绝对过期
        /// </summary>
        public bool EnableAbsoluteExpiration { get; set; } = true;

        /// <summary>
        /// 绝对过期时间(小时)
        /// </summary>
        public int AbsoluteExpirationHours { get; set; } = 24;

        /// <summary>
        /// 是否允许多设备登录
        /// </summary>
        public bool AllowMultipleDevices { get; set; } = true;

        /// <summary>
        /// 最大并发会话数
        /// </summary>
        public int MaxConcurrentSessions { get; set; } = 3;

        /// <summary>
        /// 会话过期时间(分钟)
        /// </summary>
        public int SessionExpiryMinutes { get; set; } = 30;
    }
} 