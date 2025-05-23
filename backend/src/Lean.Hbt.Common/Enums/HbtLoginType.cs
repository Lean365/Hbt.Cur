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
        DingTalk = 6,

        /// <summary>
        /// 企业微信登录
        /// </summary>
        WeCom = 7,

        /// <summary>
        /// 飞书登录
        /// </summary>
        Feishu = 8,

        /// <summary>
        /// 支付宝登录
        /// </summary>
        Alipay = 9,

        /// <summary>
        /// 微博登录
        /// </summary>
        Weibo = 10,

        /// <summary>
        /// GitHub登录
        /// </summary>
        GitHub = 11,

        /// <summary>
        /// Google登录
        /// </summary>
        Google = 12,

        /// <summary>
        /// 微软账号登录
        /// </summary>
        Microsoft = 13,

        /// <summary>
        /// Apple登录
        /// </summary>
        Apple = 14,

        /// <summary>
        /// 指纹登录
        /// </summary>
        Fingerprint = 15,

        /// <summary>
        /// 人脸识别登录
        /// </summary>
        FaceRecognition = 16,

        /// <summary>
        /// 声纹登录
        /// </summary>
        VoicePrint = 17,

        /// <summary>
        /// 扫码登录
        /// </summary>
        QRCode = 18,

        /// <summary>
        /// 单点登录(SSO)
        /// </summary>
        SSO = 19,

        /// <summary>
        /// OAuth2.0登录
        /// </summary>
        OAuth2 = 20,

        /// <summary>
        /// OpenID Connect登录
        /// </summary>
        OpenIDConnect = 21,

        /// <summary>
        /// SAML登录
        /// </summary>
        SAML = 22,

        /// <summary>
        /// LDAP登录
        /// </summary>
        LDAP = 23,

        /// <summary>
        /// 证书登录
        /// </summary>
        Certificate = 24,

        /// <summary>
        /// 硬件令牌登录
        /// </summary>
        HardwareToken = 25,

        /// <summary>
        /// 动态口令登录
        /// </summary>
        TOTP = 26,

        /// <summary>
        /// 手机号一键登录
        /// </summary>
        OneClick = 27,

        /// <summary>
        /// 免密登录
        /// </summary>
        Passwordless = 28,

        /// <summary>
        /// 游客登录
        /// </summary>
        Guest = 29,

        /// <summary>
        /// 匿名登录
        /// </summary>
        Anonymous = 30
    }
} 