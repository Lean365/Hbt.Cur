//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtCsrfMiddleware.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:00
// 版本号 : V1.0.0
// 描述    : CSRF防护中间件
//===================================================================

using System.Security.Cryptography;
using Lean.Hbt.Common.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Lean.Hbt.Infrastructure.Security
{
    /// <summary>
    /// CSRF防护中间件
    /// </summary>
    public class HbtCsrfMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDistributedCache _cache;
        private readonly HbtSecurityOptions _options;
        private readonly ILogger<HbtCsrfMiddleware> _logger;
        private const string CsrfTokenHeader = "X-CSRF-Token";
        private const string CsrfTokenCookie = "XSRF-TOKEN";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        /// <param name="cache"></param>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        public HbtCsrfMiddleware(
            RequestDelegate next,
            IDistributedCache cache,
            IOptions<HbtSecurityOptions> options,
            ILogger<HbtCsrfMiddleware> logger)
        {
            _next = next;
            _cache = cache;
            _options = options.Value;
            _logger = logger;
        }

        /// <summary>
        /// 调用中间件处理CSRF防护
        /// </summary>
        /// <param name="context">HTTP上下文</param>
        /// <returns>处理结果</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation("[CSRF] Processing request: {Method} {Path}", context.Request.Method, context.Request.Path);
            
            // 1. 检查是否需要跳过CSRF验证
            if (ShouldSkipCsrfCheck(context))
            {
                _logger.LogInformation("[CSRF] Skipping CSRF check");
                await _next(context);
                return;
            }

            // 2. 处理GET请求
            if (context.Request.Method == "GET")
            {
                // 生成新的CSRF Token
                var token = GenerateCsrfToken();
                
                _logger.LogInformation("[CSRF] Setting CSRF token cookie: {Token}", token);
                
                // 设置Cookie
                context.Response.Cookies.Append(CsrfTokenCookie, token, GetCookieOptions(context));

                // 同时在响应头中返回token
                context.Response.Headers.Append("X-CSRF-Token", token);

                // 缓存Token
                await CacheTokenAsync(token);
                
                _logger.LogInformation("[CSRF] Generated new CSRF token: {Token}", token);
            }
            // 3. 处理非GET请求
            else if (!await ValidateCsrfTokenAsync(context))
            {
                _logger.LogWarning("[CSRF] CSRF validation failed");
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new { message = "CSRF Token验证失败" });
                return;
            }

            await _next(context);
        }

        /// <summary>
        /// 生成CSRF Token
        /// </summary>
        private string GenerateCsrfToken()
        {
            var bytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 缓存Token
        /// </summary>
        private async Task CacheTokenAsync(string token)
        {
            await _cache.SetStringAsync(
                $"csrf:token:{token}",
                token,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_options.CsrfTokenExpirationMinutes)
                }
            );
        }

        /// <summary>
        /// 验证CSRF Token
        /// </summary>
        private async Task<bool> ValidateCsrfTokenAsync(HttpContext context)
        {
            // 1. 首先尝试从请求头获取Token
            var requestToken = context.Request.Headers[CsrfTokenHeader].ToString();
            
            // 2. 如果请求头中没有，则尝试从请求参数获取
            if (string.IsNullOrEmpty(requestToken) && context.Request.HasFormContentType)
            {
                requestToken = context.Request.Form["csrf_token"].ToString();
            }
            
            _logger.LogInformation("[CSRF] Request token: {Token}", requestToken);

            // 3. 获取Cookie中的Token
            var cookieToken = context.Request.Cookies[CsrfTokenCookie];
            _logger.LogInformation("[CSRF] Cookie token: {Token}", cookieToken);

            if (string.IsNullOrEmpty(requestToken) || string.IsNullOrEmpty(cookieToken))
            {
                _logger.LogWarning("[CSRF] Missing CSRF token in request or cookie");
                return false;
            }

            // 4. 验证Token是否匹配
            if (requestToken != cookieToken)
            {
                _logger.LogWarning("[CSRF] CSRF token mismatch");
                return false;
            }

            // 5. 验证Token是否有效
            var cachedToken = await _cache.GetStringAsync($"csrf:token:{requestToken}");
            _logger.LogInformation("[CSRF] Cached token: {Token}", cachedToken);
            
            return !string.IsNullOrEmpty(cachedToken);
        }

        private bool ShouldSkipCsrfCheck(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();
            _logger.LogInformation("[CSRF] Checking if should skip CSRF for path: {Path}", path);
            
            // 跳过OPTIONS请求
            if (context.Request.Method == "OPTIONS")
            {
                _logger.LogInformation("[CSRF] Skipping CSRF check for OPTIONS request");
                return true;
            }
            
            // 跳过认证相关的路径
            if (path != null && (
                path.Contains("/login") ||
                path.Contains("/auth") ||
                path.Contains("/token") ||
                path.Contains("/oauth") ||
                path.Contains("/hbtcaptcha")))
            {
                _logger.LogInformation("[CSRF] Skipping CSRF check for auth path: {Path}", path);
                return true;
            }

            // 跳过静态文件
            if (path != null && (
                path.EndsWith(".js") ||
                path.EndsWith(".css") ||
                path.EndsWith(".html") ||
                path.EndsWith(".jpg") ||
                path.EndsWith(".png") ||
                path.EndsWith(".gif") ||
                path.EndsWith(".ico") ||
                path.EndsWith(".woff") ||
                path.EndsWith(".woff2")))
            {
                _logger.LogInformation("[CSRF] Skipping CSRF check for static file: {Path}", path);
                return true;
            }

            return false;
        }

        private CookieOptions GetCookieOptions(HttpContext context)
        {
            return new CookieOptions
            {
                HttpOnly = false,  // 允许 JavaScript 访问
                Secure = context.Request.IsHttps,  // 根据请求协议设置
                SameSite = SameSiteMode.None,  // 允许跨站点请求
                Path = "/",
                MaxAge = TimeSpan.FromMinutes(_options.CsrfTokenExpirationMinutes)
            };
        }
    }
}