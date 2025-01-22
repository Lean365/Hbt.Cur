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
            // 1. 处理GET请求
            if (context.Request.Method == "GET")
            {
                // 生成新的CSRF Token
                var token = GenerateCsrfToken();

                // 设置Cookie
                context.Response.Cookies.Append(CsrfTokenCookie, token, new CookieOptions
                {
                    HttpOnly = false,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                // 缓存Token
                await CacheTokenAsync(token);
            }
            // 2. 处理非GET请求
            else if (!await ValidateCsrfTokenAsync(context))
            {
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
            if (string.IsNullOrEmpty(requestToken))
                return false;

            // 2. 获取Cookie中的Token
            var cookieToken = context.Request.Cookies[CsrfTokenCookie];
            if (string.IsNullOrEmpty(cookieToken))
                return false;

            // 3. 验证Token是否匹配
            if (requestToken != cookieToken)
                return false;

            // 4. 验证Token是否有效
            var cachedToken = await _cache.GetStringAsync($"csrf:token:{requestToken}");
            return !string.IsNullOrEmpty(cachedToken);
        }
    }
}