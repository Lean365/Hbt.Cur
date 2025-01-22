//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginPolicyOptions.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 18:00
// 版本号 : V1.0.0
// 描述    : 登录策略配置选项
//===================================================================

namespace Lean.Hbt.Common.Options
{
    /// <summary>
    /// 登录策略配置选项
    /// </summary>
    public class HbtLoginPolicyOptions
    {
        /// <summary>
        /// 最大失败次数
        /// </summary>
        public int MaxFailedAttempts { get; set; } = 5;

        /// <summary>
        /// 锁定时间(分钟)
        /// </summary>
        public int LockoutMinutes { get; set; } = 30;

        /// <summary>
        /// 是否允许多端登录
        /// </summary>
        public bool AllowMultipleLogin { get; set; } = true;

        /// <summary>
        /// 是否启用登录限制
        /// </summary>
        public bool EnableLoginRestriction { get; set; } = true;
    }
} 