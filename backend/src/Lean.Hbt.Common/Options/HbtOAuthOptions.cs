//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOAuthOptions.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : OAuth第三方登录配置选项
//===================================================================

namespace Lean.Hbt.Common.Options;

/// <summary>
/// OAuth第三方登录配置选项
/// </summary>
public class HbtOAuthOptions
{
    /// <summary>
    /// 是否启用OAuth登录
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// 默认回调地址
    /// </summary>
    public string DefaultRedirectUri { get; set; } = "https://your-app.com/oauth/callback";

    /// <summary>
    /// 会话超时时间（分钟）
    /// </summary>
    public int SessionTimeoutMinutes { get; set; } = 30;

    /// <summary>
    /// GitHub登录配置
    /// </summary>
    public GitHubOptions GitHub { get; set; } = new();

    /// <summary>
    /// Google登录配置
    /// </summary>
    public GoogleOptions Google { get; set; } = new();

    /// <summary>
    /// Facebook登录配置
    /// </summary>
    public FacebookOptions Facebook { get; set; } = new();

    /// <summary>
    /// Twitter登录配置
    /// </summary>
    public TwitterOptions Twitter { get; set; } = new();

    /// <summary>
    /// QQ登录配置
    /// </summary>
    public QQOptions QQ { get; set; } = new();

    /// <summary>
    /// Microsoft登录配置
    /// </summary>
    public MicrosoftOptions Microsoft { get; set; } = new();

    /// <summary>
    /// Apple登录配置
    /// </summary>
    public AppleOptions Apple { get; set; } = new();

    /// <summary>
    /// Amazon登录配置
    /// </summary>
    public AmazonOptions Amazon { get; set; } = new();

    /// <summary>
    /// 钉钉登录配置
    /// </summary>
    public DingTalkOptions DingTalk { get; set; } = new();

    /// <summary>
    /// LinkedIn登录配置
    /// </summary>
    public LinkedInOptions LinkedIn { get; set; } = new();

    /// <summary>
    /// 微博登录配置
    /// </summary>
    public WeiboOptions Weibo { get; set; } = new();
}

/// <summary>
/// GitHub登录配置选项
/// </summary>
public class GitHubOptions
{
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled { get; set; } = false;

    /// <summary>
    /// 客户端ID
    /// </summary>
    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// 客户端密钥
    /// </summary>
    public string ClientSecret { get; set; } = string.Empty;

    /// <summary>
    /// 回调地址
    /// </summary>
    public string RedirectUri { get; set; } = string.Empty;

    /// <summary>
    /// 授权范围
    /// </summary>
    public string Scope { get; set; } = "read:user,user:email";

    /// <summary>
    /// 授权URL
    /// </summary>
    public string AuthorizationUrl { get; set; } = "https://github.com/login/oauth/authorize";

    /// <summary>
    /// 令牌URL
    /// </summary>
    public string TokenUrl { get; set; } = "https://github.com/login/oauth/access_token";

    /// <summary>
    /// 用户信息URL
    /// </summary>
    public string UserInfoUrl { get; set; } = "https://api.github.com/user";
}

/// <summary>
/// Google登录配置选项
/// </summary>
public class GoogleOptions
{
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled { get; set; } = false;

    /// <summary>
    /// 客户端ID
    /// </summary>
    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// 客户端密钥
    /// </summary>
    public string ClientSecret { get; set; } = string.Empty;

    /// <summary>
    /// 回调地址
    /// </summary>
    public string RedirectUri { get; set; } = string.Empty;

    /// <summary>
    /// 授权范围
    /// </summary>
    public string Scope { get; set; } = "openid email profile";

    /// <summary>
    /// 授权URL
    /// </summary>
    public string AuthorizationUrl { get; set; } = "https://accounts.google.com/o/oauth2/v2/auth";

    /// <summary>
    /// 令牌URL
    /// </summary>
    public string TokenUrl { get; set; } = "https://oauth2.googleapis.com/token";

    /// <summary>
    /// 用户信息URL
    /// </summary>
    public string UserInfoUrl { get; set; } = "https://www.googleapis.com/oauth2/v2/userinfo";
}

/// <summary>
/// Facebook登录配置选项
/// </summary>
public class FacebookOptions
{
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled { get; set; } = false;

    /// <summary>
    /// 应用ID
    /// </summary>
    public string AppId { get; set; } = string.Empty;

    /// <summary>
    /// 应用密钥
    /// </summary>
    public string AppSecret { get; set; } = string.Empty;

    /// <summary>
    /// 回调地址
    /// </summary>
    public string RedirectUri { get; set; } = string.Empty;

    /// <summary>
    /// 授权范围
    /// </summary>
    public string Scope { get; set; } = "email,public_profile";

    /// <summary>
    /// 授权URL
    /// </summary>
    public string AuthorizationUrl { get; set; } = "https://www.facebook.com/v12.0/dialog/oauth";

    /// <summary>
    /// 令牌URL
    /// </summary>
    public string TokenUrl { get; set; } = "https://graph.facebook.com/v12.0/oauth/access_token";

    /// <summary>
    /// 用户信息URL
    /// </summary>
    public string UserInfoUrl { get; set; } = "https://graph.facebook.com/me";
}

/// <summary>
/// Twitter登录配置选项
/// </summary>
public class TwitterOptions
{
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled { get; set; } = false;

    /// <summary>
    /// API密钥
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// API密钥秘密
    /// </summary>
    public string ApiKeySecret { get; set; } = string.Empty;

    /// <summary>
    /// 回调地址
    /// </summary>
    public string RedirectUri { get; set; } = string.Empty;

    /// <summary>
    /// 授权范围
    /// </summary>
    public string Scope { get; set; } = "tweet.read users.read offline.access";

    /// <summary>
    /// 授权URL
    /// </summary>
    public string AuthorizationUrl { get; set; } = "https://twitter.com/i/oauth2/authorize";

    /// <summary>
    /// 令牌URL
    /// </summary>
    public string TokenUrl { get; set; } = "https://api.twitter.com/2/oauth2/token";

    /// <summary>
    /// 用户信息URL
    /// </summary>
    public string UserInfoUrl { get; set; } = "https://api.twitter.com/2/users/me";
}

/// <summary>
/// QQ登录配置选项
/// </summary>
public class QQOptions
{
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled { get; set; } = false;

    /// <summary>
    /// 应用ID
    /// </summary>
    public string AppId { get; set; } = string.Empty;

    /// <summary>
    /// 应用密钥
    /// </summary>
    public string AppKey { get; set; } = string.Empty;

    /// <summary>
    /// 回调地址
    /// </summary>
    public string RedirectUri { get; set; } = string.Empty;

    /// <summary>
    /// 授权范围
    /// </summary>
    public string Scope { get; set; } = "get_user_info";

    /// <summary>
    /// 授权URL
    /// </summary>
    public string AuthorizationUrl { get; set; } = "https://graph.qq.com/oauth2.0/authorize";

    /// <summary>
    /// 令牌URL
    /// </summary>
    public string TokenUrl { get; set; } = "https://graph.qq.com/oauth2.0/token";

    /// <summary>
    /// 用户信息URL
    /// </summary>
    public string UserInfoUrl { get; set; } = "https://graph.qq.com/user/get_user_info";
}



/// <summary>
/// Microsoft登录配置选项
/// </summary>
public class MicrosoftOptions
{
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled { get; set; } = false;

    /// <summary>
    /// 客户端ID
    /// </summary>
    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// 客户端密钥
    /// </summary>
    public string ClientSecret { get; set; } = string.Empty;

    /// <summary>
    /// 回调地址
    /// </summary>
    public string RedirectUri { get; set; } = string.Empty;

    /// <summary>
    /// 授权范围
    /// </summary>
    public string Scope { get; set; } = "openid email profile";

    /// <summary>
    /// 授权URL
    /// </summary>
    public string AuthorizationUrl { get; set; } = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize";

    /// <summary>
    /// 令牌URL
    /// </summary>
    public string TokenUrl { get; set; } = "https://login.microsoftonline.com/common/oauth2/v2.0/token";

    /// <summary>
    /// 用户信息URL
    /// </summary>
    public string UserInfoUrl { get; set; } = "https://graph.microsoft.com/v1.0/me";
}

/// <summary>
/// Apple登录配置选项
/// </summary>
public class AppleOptions
{
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled { get; set; } = false;

    /// <summary>
    /// 客户端ID
    /// </summary>
    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// 客户端密钥
    /// </summary>
    public string ClientSecret { get; set; } = string.Empty;

    /// <summary>
    /// 回调地址
    /// </summary>
    public string RedirectUri { get; set; } = string.Empty;

    /// <summary>
    /// 授权范围
    /// </summary>
    public string Scope { get; set; } = "name email";

    /// <summary>
    /// 授权URL
    /// </summary>
    public string AuthorizationUrl { get; set; } = "https://appleid.apple.com/auth/authorize";

    /// <summary>
    /// 令牌URL
    /// </summary>
    public string TokenUrl { get; set; } = "https://appleid.apple.com/auth/token";

    /// <summary>
    /// 用户信息URL
    /// </summary>
    public string UserInfoUrl { get; set; } = "https://appleid.apple.com/auth/userinfo";

    /// <summary>
    /// 团队ID
    /// </summary>
    public string TeamId { get; set; } = string.Empty;

    /// <summary>
    /// 密钥ID
    /// </summary>
    public string KeyId { get; set; } = string.Empty;

    /// <summary>
    /// 私钥
    /// </summary>
    public string PrivateKey { get; set; } = string.Empty;
}

/// <summary>
/// Amazon登录配置选项
/// </summary>
public class AmazonOptions
{
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled { get; set; } = false;

    /// <summary>
    /// 客户端ID
    /// </summary>
    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// 客户端密钥
    /// </summary>
    public string ClientSecret { get; set; } = string.Empty;

    /// <summary>
    /// 回调地址
    /// </summary>
    public string RedirectUri { get; set; } = string.Empty;

    /// <summary>
    /// 授权范围
    /// </summary>
    public string Scope { get; set; } = "profile";

    /// <summary>
    /// 授权URL
    /// </summary>
    public string AuthorizationUrl { get; set; } = "https://www.amazon.com/ap/oa";

    /// <summary>
    /// 令牌URL
    /// </summary>
    public string TokenUrl { get; set; } = "https://api.amazon.com/auth/o2/token";

    /// <summary>
    /// 用户信息URL
    /// </summary>
    public string UserInfoUrl { get; set; } = "https://api.amazon.com/user/profile";
}

/// <summary>
/// 钉钉登录配置选项
/// </summary>
public class DingTalkOptions
{
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled { get; set; } = false;

    /// <summary>
    /// 应用ID
    /// </summary>
    public string AppId { get; set; } = string.Empty;

    /// <summary>
    /// 应用密钥
    /// </summary>
    public string AppSecret { get; set; } = string.Empty;

    /// <summary>
    /// 回调地址
    /// </summary>
    public string RedirectUri { get; set; } = string.Empty;

    /// <summary>
    /// 授权范围
    /// </summary>
    public string Scope { get; set; } = "openid";

    /// <summary>
    /// 授权URL
    /// </summary>
    public string AuthorizationUrl { get; set; } = "https://oapi.dingtalk.com/connect/oauth2/sns_authorize";

    /// <summary>
    /// 令牌URL
    /// </summary>
    public string TokenUrl { get; set; } = "https://oapi.dingtalk.com/gettoken";

    /// <summary>
    /// 用户信息URL
    /// </summary>
    public string UserInfoUrl { get; set; } = "https://oapi.dingtalk.com/user/getuserinfo";
}

/// <summary>
/// LinkedIn登录配置选项
/// </summary>
public class LinkedInOptions
{
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled { get; set; } = false;

    /// <summary>
    /// 客户端ID
    /// </summary>
    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// 客户端密钥
    /// </summary>
    public string ClientSecret { get; set; } = string.Empty;

    /// <summary>
    /// 回调地址
    /// </summary>
    public string RedirectUri { get; set; } = string.Empty;

    /// <summary>
    /// 授权范围
    /// </summary>
    public string Scope { get; set; } = "r_liteprofile r_emailaddress";

    /// <summary>
    /// 授权URL
    /// </summary>
    public string AuthorizationUrl { get; set; } = "https://www.linkedin.com/oauth/v2/authorization";

    /// <summary>
    /// 令牌URL
    /// </summary>
    public string TokenUrl { get; set; } = "https://www.linkedin.com/oauth/v2/accessToken";

    /// <summary>
    /// 用户信息URL
    /// </summary>
    public string UserInfoUrl { get; set; } = "https://api.linkedin.com/v2/me";
}

/// <summary>
/// 微博登录配置选项
/// </summary>
public class WeiboOptions
{
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled { get; set; } = false;

    /// <summary>
    /// 应用Key
    /// </summary>
    public string AppKey { get; set; } = string.Empty;

    /// <summary>
    /// 应用Secret
    /// </summary>
    public string AppSecret { get; set; } = string.Empty;

    /// <summary>
    /// 回调地址
    /// </summary>
    public string RedirectUri { get; set; } = string.Empty;

    /// <summary>
    /// 授权范围
    /// </summary>
    public string Scope { get; set; } = "all";

    /// <summary>
    /// 授权URL
    /// </summary>
    public string AuthorizationUrl { get; set; } = "https://api.weibo.com/oauth2/authorize";

    /// <summary>
    /// 令牌URL
    /// </summary>
    public string TokenUrl { get; set; } = "https://api.weibo.com/oauth2/access_token";

    /// <summary>
    /// 用户信息URL
    /// </summary>
    public string UserInfoUrl { get; set; } = "https://api.weibo.com/2/users/show.json";
}