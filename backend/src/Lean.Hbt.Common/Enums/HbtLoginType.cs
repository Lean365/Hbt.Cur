//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtLoginType.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V1.0.0
// 描述    : 登录类型枚举
//===================================================================

namespace Lean.Hbt.Common.Enums
{
    /// <summary>
    /// 登录类型枚举
    /// </summary>
    public enum HbtLoginType
    {
        /// <summary>
        /// 其他
        /// </summary>
        Other = 0,

        /// <summary>
        /// 用户名密码
        /// </summary>
        Password = 1,

        /// <summary>
        /// 短信验证码
        /// </summary>
        Sms = 2,

        /// <summary>
        /// 邮箱验证码
        /// </summary>
        Email = 3,

        /// <summary>
        /// 微信登录
        /// </summary>
        WeChat = 4,

        /// <summary>
        /// QQ登录
        /// </summary>
        QQ = 5,

        /// <summary>
        /// 钉钉登录
        /// </summary>
        DingTalk = 6
    }
} 