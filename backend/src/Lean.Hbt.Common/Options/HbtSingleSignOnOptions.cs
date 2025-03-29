using System;
using System.Collections.Generic;

namespace Lean.Hbt.Common.Options
{
    /// <summary>
    /// 单点登录配置选项
    /// </summary>
    public class HbtSingleSignOnOptions
    {
        /// <summary>
        /// 是否启用单点登录
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 是否踢出旧会话
        /// </summary>
        public bool KickoutOldSession { get; set; }

        /// <summary>
        /// 排除的角色（这些角色不受单点登录限制）
        /// </summary>
        public List<string> ExcludeRoles { get; set; } = new();

        /// <summary>
        /// 排除的用户（这些用户不受单点登录限制）
        /// </summary>
        public List<string> ExcludeUsers { get; set; } = new();

        /// <summary>
        /// 是否通知被踢出的会话
        /// </summary>
        public bool NotifyKickout { get; set; }

        /// <summary>
        /// 踢出消息
        /// </summary>
        public string KickoutMessage { get; set; } = "您的账号已在其他设备上登录";
    }
} 