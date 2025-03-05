//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtLoginStatus.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V1.0.0
// 描述    : 登录状态枚举
//===================================================================

namespace Lean.Hbt.Common.Enums
{
    /// <summary>
    /// 登录状态枚举
    /// </summary>
    public enum HbtLoginStatus
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 失败
        /// </summary>
        Failed = 1,

        /// <summary>
        /// 账号被锁定
        /// </summary>
        Locked = 2,

        /// <summary>
        /// 账号被禁用
        /// </summary>
        Disabled = 3,

        /// <summary>
        /// 验证码错误
        /// </summary>
        InvalidCode = 4,

        /// <summary>
        /// 密码错误
        /// </summary>
        InvalidPassword = 5,

        /// <summary>
        /// 用户不存在
        /// </summary>
        UserNotFound = 6
    }
} 