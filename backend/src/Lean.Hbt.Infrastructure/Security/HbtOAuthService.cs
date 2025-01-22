using System.Web;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Lean.Hbt.Infrastructure.Security
{
    /// <summary>
    /// OAuth服务实现
    /// </summary>
    public class HbtOAuthService : IHbtOAuthService
    {
        private readonly HbtOAuthOptions _defaultOptions;
        private readonly IHbtRepository<HbtSysConfig> _configRepository;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configRepository"></param>
        /// <param name="httpClient"></param>
        public HbtOAuthService(
            IOptions<HbtOAuthOptions> options,
            IHbtRepository<HbtSysConfig> configRepository,
            HttpClient httpClient)
        {
            _defaultOptions = options.Value;
            _configRepository = configRepository;
            _httpClient = httpClient;
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns></returns>
        private async Task<HbtOAuthOptions> GetOptionsAsync()
        {
            var options = new HbtOAuthOptions
            {
                Enabled = _defaultOptions.Enabled,
                Providers = new Dictionary<string, HbtOAuthProviderOptions>()
            };

            var configs = await _configRepository.GetListAsync();
            foreach (var config in configs)
            {
                if (config.ConfigKey.StartsWith("oauth."))
                {
                    var parts = config.ConfigKey.Split('.');
                    if (parts.Length == 3)
                    {
                        var provider = parts[1];
                        var key = parts[2];

                        if (!options.Providers.ContainsKey(provider))
                        {
                            options.Providers[provider] = new HbtOAuthProviderOptions();
                        }

                        var providerOptions = options.Providers[provider];
                        switch (key)
                        {
                            case "clientId":
                                providerOptions.ClientId = config.ConfigValue;
                                break;

                            case "clientSecret":
                                providerOptions.ClientSecret = config.ConfigValue;
                                break;

                            case "authorizationEndpoint":
                                providerOptions.AuthorizationEndpoint = config.ConfigValue;
                                break;

                            case "tokenEndpoint":
                                providerOptions.TokenEndpoint = config.ConfigValue;
                                break;

                            case "userInfoEndpoint":
                                providerOptions.UserInfoEndpoint = config.ConfigValue;
                                break;

                            case "redirectUri":
                                providerOptions.RedirectUri = config.ConfigValue;
                                break;

                            case "scope":
                                providerOptions.Scope = config.ConfigValue;
                                break;
                        }
                    }
                }
            }

            // 合并默认配置
            foreach (var provider in _defaultOptions.Providers)
            {
                if (!options.Providers.ContainsKey(provider.Key))
                {
                    options.Providers[provider.Key] = provider.Value;
                }
            }

            return options;
        }

        /// <summary>
        /// 获取授权地址
        /// </summary>
        public async Task<string> GetAuthorizationUrlAsync(string provider, string state)
        {
            var options = await GetOptionsAsync();
            if (!options.Enabled)
                throw new InvalidOperationException("OAuth is disabled");

            if (!options.Providers.TryGetValue(provider, out var providerOptions))
                throw new InvalidOperationException($"OAuth provider '{provider}' not found");

            var queryParams = new Dictionary<string, string>
            {
                ["client_id"] = providerOptions.ClientId,
                ["redirect_uri"] = providerOptions.RedirectUri,
                ["response_type"] = "code",
                ["scope"] = providerOptions.Scope,
                ["state"] = state
            };

            var queryString = string.Join("&", queryParams.Select(p => $"{p.Key}={HttpUtility.UrlEncode(p.Value)}"));
            return $"{providerOptions.AuthorizationEndpoint}?{queryString}";
        }

        /// <summary>
        /// 处理OAuth回调
        /// </summary>
        public async Task<HbtOAuthUserInfo> HandleCallbackAsync(string provider, string code, string state)
        {
            var options = await GetOptionsAsync();
            if (!options.Enabled)
                throw new InvalidOperationException("OAuth is disabled");

            if (!options.Providers.TryGetValue(provider, out var providerOptions))
                throw new InvalidOperationException($"OAuth provider '{provider}' not found");

            // 获取访问令牌
            var tokenResponse = await _httpClient.PostAsync(providerOptions.TokenEndpoint, new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    ["client_id"] = providerOptions.ClientId,
                    ["client_secret"] = providerOptions.ClientSecret,
                    ["code"] = code,
                    ["redirect_uri"] = providerOptions.RedirectUri,
                    ["grant_type"] = "authorization_code"
                }));

            tokenResponse.EnsureSuccessStatusCode();
            var tokenJson = JObject.Parse(await tokenResponse.Content.ReadAsStringAsync());
            var accessToken = tokenJson["access_token"].ToString();

            // 获取用户信息
            var userInfoRequest = new HttpRequestMessage(HttpMethod.Get, providerOptions.UserInfoEndpoint);
            userInfoRequest.Headers.Add("Authorization", $"Bearer {accessToken}");
            var userInfoResponse = await _httpClient.SendAsync(userInfoRequest);
            userInfoResponse.EnsureSuccessStatusCode();
            var userInfoJson = JObject.Parse(await userInfoResponse.Content.ReadAsStringAsync());

            return new HbtOAuthUserInfo
            {
                Provider = provider,
                OpenId = userInfoJson["sub"]?.ToString(),
                UserName = userInfoJson["preferred_username"]?.ToString(),
                NickName = userInfoJson["name"]?.ToString(),
                Email = userInfoJson["email"]?.ToString(),
                Avatar = userInfoJson["picture"]?.ToString()
            };
        }
    }
}