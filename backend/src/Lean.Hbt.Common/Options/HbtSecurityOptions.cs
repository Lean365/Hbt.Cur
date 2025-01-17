//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtSecurityOptions.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-17 10:00
// 版本号 : V1.0.0
// 描述    : 安全配置选项
//===================================================================

namespace Lean.Hbt.Common.Options
{
    /// <summary>
    /// 安全配置选项
    /// </summary>
    public class HbtSecurityOptions
    {
        /// <summary>
        /// 登录策略配置
        /// </summary>
        public LoginPolicyOptions LoginPolicy { get; set; } = new();

        /// <summary>
        /// 密码策略配置
        /// </summary>
        public PasswordPolicyOptions PasswordPolicy { get; set; } = new();

        /// <summary>
        /// 会话管理配置
        /// </summary>
        public SessionOptions Session { get; set; } = new();
    }

    /// <summary>
    /// 登录策略配置
    /// </summary>
    public class LoginPolicyOptions
    {
        /// <summary>
        /// 最大失败次数
        /// </summary>
        public int MaxFailedAttempts { get; set; } = 5;

        /// <summary>
        /// 锁定时间（分钟）
        /// </summary>
        public int LockoutMinutes { get; set; } = 30;

        /// <summary>
        /// 是否启用登录限制
        /// </summary>
        public bool EnableLoginRestriction { get; set; } = true;
    }

    /// <summary>
    /// 密码策略配置
    /// </summary>
    public class PasswordPolicyOptions
    {
        /// <summary>
        /// 密码最小长度
        /// </summary>
        public int MinLength { get; set; } = 8;

        /// <summary>
        /// 密码最大长度
        /// </summary>
        public int MaxLength { get; set; } = 20;

        /// <summary>
        /// 是否要求包含数字
        /// </summary>
        public bool RequireDigit { get; set; } = true;

        /// <summary>
        /// 是否要求包含小写字母
        /// </summary>
        public bool RequireLowercase { get; set; } = true;

        /// <summary>
        /// 是否要求包含大写字母
        /// </summary>
        public bool RequireUppercase { get; set; } = true;

        /// <summary>
        /// 是否要求包含特殊字符
        /// </summary>
        public bool RequireSpecialChar { get; set; } = true;

        /// <summary>
        /// 密码有效期（天）
        /// </summary>
        public int PasswordExpirationDays { get; set; } = 90;
    }

    /// <summary>
    /// 会话管理配置
    /// </summary>
    public class SessionOptions
    {
        /// <summary>
        /// 会话过期时间（分钟）
        /// </summary>
        public int SessionExpiryMinutes { get; set; } = 30;

        /// <summary>
        /// 是否允许多设备同时登录
        /// </summary>
        public bool AllowMultipleDevices { get; set; } = true;

        /// <summary>
        /// 最大同时在线会话数
        /// </summary>
        public int MaxConcurrentSessions { get; set; } = 3;

        /// <summary>
        /// 是否启用会话滑动过期
        /// </summary>
        public bool EnableSlidingExpiration { get; set; } = true;
    }
} 