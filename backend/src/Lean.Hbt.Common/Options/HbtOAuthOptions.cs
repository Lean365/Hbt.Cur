using System.Collections.Generic;

namespace Lean.Hbt.Common.Options
{
    /// <summary>
    /// OAuth配置选项
    /// </summary>
    public class HbtOAuthOptions
    {
        /// <summary>
        /// 是否启用OAuth认证
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// OAuth提供商配置
        /// </summary>
        public Dictionary<string, HbtOAuthProviderOptions> Providers { get; set; } = new();
    }

    /// <summary>
    /// OAuth提供商配置
    /// </summary>
    public class HbtOAuthProviderOptions
    {
        /// <summary>
        /// 客户端ID
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 客户端密钥
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// 授权端点
        /// </summary>
        public string AuthorizationEndpoint { get; set; }

        /// <summary>
        /// 令牌端点
        /// </summary>
        public string TokenEndpoint { get; set; }

        /// <summary>
        /// 用户信息端点
        /// </summary>
        public string UserInfoEndpoint { get; set; }

        /// <summary>
        /// 回调地址
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// 作用域
        /// </summary>
        public string Scope { get; set; } = "openid profile email";
    }
} 