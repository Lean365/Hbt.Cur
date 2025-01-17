//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtSessionInfo.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-17 14:30
// 版本号 : V1.0.0
// 描述    : 会话信息模型
//===================================================================

using System;

namespace Lean.Hbt.Domain.Models
{
    /// <summary>
    /// 会话信息模型
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-17
    /// </remarks>
    public class HbtSessionInfo
    {
        /// <summary>
        /// 会话ID
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 最后访问时间
        /// </summary>
        public DateTime LastAccessTime { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }
    }
} 