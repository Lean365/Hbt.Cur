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
        private const string CsrfTokenHeader = "X-CSRF-Token";
        private const string CsrfTokenCookie = "XSRF-TOKEN";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        /// <param name="cache"></param>
        /// <param name="options"></param>
        public HbtCsrfMiddleware(
            RequestDelegate next,
            IDistributedCache cache,
            IOptions<HbtSecurityOptions> options)
        {
            _next = next;
            _cache = cache;
            _options = options.Value;
        }

        /// <summary>
        /// 调用中间件处理CSRF防护
        /// </summary>
        /// <param name="context">HTTP上下文</param>
        /// <returns>处理结果</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"Processing request: {context.Request.Method} {context.Request.Path}");
            Console.WriteLine($"Request headers: {string.Join(", ", context.Request.Headers.Select(h => $"{h.Key}={h.Value}"))}");
            
            // 1. 检查是否需要跳过CSRF验证
            if (ShouldSkipCsrfCheck(context))
            {
                Console.WriteLine("Skipping CSRF check");
                await _next(context);
                return;
            }

            // 2. 处理GET请求
            if (context.Request.Method == "GET")
            {
                // 生成新的CSRF Token
                var token = GenerateCsrfToken();
                
                Console.WriteLine($"Setting CSRF token cookie: {token}");
                
                // 设置Cookie
                context.Response.Cookies.Append(CsrfTokenCookie, token, new CookieOptions
                {
                    HttpOnly = false,
                    Secure = context.Request.IsHttps,
                    SameSite = SameSiteMode.Lax,
                    Path = "/"
                });

                // 缓存Token
                await CacheTokenAsync(token);
                
                Console.WriteLine($"Generated new CSRF token: {token}");
            }
            // 3. 处理非GET请求
            else if (!await ValidateCsrfTokenAsync(context))
            {
                Console.WriteLine("CSRF validation failed");
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
            // 1. 获取请求中的Token
            var requestToken = context.Request.Headers[CsrfTokenHeader].ToString();
            Console.WriteLine($"Request token: {requestToken}");

            // 2. 获取Cookie中的Token
            var cookieToken = context.Request.Cookies[CsrfTokenCookie];
            Console.WriteLine($"Cookie token: {cookieToken}");

            if (string.IsNullOrEmpty(requestToken) || string.IsNullOrEmpty(cookieToken))
            {
                Console.WriteLine("Missing CSRF token in request or cookie");
                return false;
            }

            // 3. 验证Token是否匹配
            if (requestToken != cookieToken)
            {
                Console.WriteLine("CSRF token mismatch");
                return false;
            }

            // 4. 验证Token是否有效
            var cachedToken = await _cache.GetStringAsync($"csrf:token:{requestToken}");
            Console.WriteLine($"Cached token: {cachedToken}");
            
            return !string.IsNullOrEmpty(cachedToken);
        }

        private bool ShouldSkipCsrfCheck(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();
            Console.WriteLine($"Checking if should skip CSRF for path: {path}");
            
            // 跳过OPTIONS请求
            if (context.Request.Method == "OPTIONS")
            {
                Console.WriteLine("Skipping CSRF check for OPTIONS request");
                return true;
            }
            
            // 跳过认证相关的路径
            if (path != null && (
                path.Contains("/login") ||
                path.Contains("/auth") ||
                path.Contains("/token") ||
                path.Contains("/oauth") ||
                path.Contains("/captcha")))
            {
                Console.WriteLine($"Skipping CSRF check for auth path: {path}");
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
                Console.WriteLine($"Skipping CSRF check for static file: {path}");
                return true;
            }

            return false;
        }
    }
}