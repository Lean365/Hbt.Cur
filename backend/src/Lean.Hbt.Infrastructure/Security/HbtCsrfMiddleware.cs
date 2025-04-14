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

            // 2. 获取现有的Cookie Token
            var cookieToken = context.Request.Cookies[CsrfTokenCookie];
            
            // 3. 处理GET请求
            if (context.Request.Method == "GET")
            {
                // 只在没有Cookie Token时生成新的
                if (string.IsNullOrEmpty(cookieToken))
                {
                    // 生成新的CSRF Token
                    var token = GenerateCsrfToken();
                    
                    _logger.LogInformation("[CSRF] Setting CSRF token cookie: {Token}", token);
                    
                    // 设置Cookie
                    context.Response.Cookies.Append(CsrfTokenCookie, token, GetCookieOptions(context));

                    // 同时在响应头中返回token
                    context.Response.Headers.Append(CsrfTokenHeader, token);

                    // 缓存Token
                    await CacheTokenAsync(token);
                    
                    _logger.LogInformation("[CSRF] Generated new CSRF token: {Token}", token);
                }
                else
                {
                    // 如果已有Token，刷新缓存时间
                    await CacheTokenAsync(cookieToken);
                    _logger.LogInformation("[CSRF] Refreshed existing CSRF token: {Token}", cookieToken);
                }
                
                await _next(context);
                return;
            }
            
            // 4. 处理非GET请求
            var requestToken = context.Request.Headers[CsrfTokenHeader].ToString();
            
            _logger.LogInformation("[CSRF] Request Token: {Token}", requestToken);
            _logger.LogInformation("[CSRF] Cookie Token: {Token}", cookieToken);

            if (string.IsNullOrEmpty(requestToken) || string.IsNullOrEmpty(cookieToken))
            {
                _logger.LogWarning("[CSRF] Missing token in request or cookie");
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new { message = "CSRF Token验证失败" });
                return;
            }

            if (requestToken != cookieToken)
            {
                _logger.LogWarning("[CSRF] Token mismatch - Request: {RequestToken}, Cookie: {CookieToken}", 
                    requestToken, cookieToken);
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new { message = "CSRF Token验证失败" });
                return;
            }

            // 验证Token是否在缓存中
            var cacheKey = $"csrf:token:{cookieToken}";
            var cachedToken = await _cache.GetStringAsync(cacheKey);
            
            if (string.IsNullOrEmpty(cachedToken))
            {
                _logger.LogWarning("[CSRF] Token not found in cache");
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new { message = "CSRF Token已过期" });
                return;
            }

            // Token验证通过，刷新过期时间
            await CacheTokenAsync(cookieToken);
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
            try 
            {
                var cacheKey = $"csrf:token:{token}";
                _logger.LogInformation("[CSRF] 开始缓存Token: {Key}", cacheKey);
                
                await _cache.SetStringAsync(
                    cacheKey,
                    token,
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_options.CsrfTokenExpirationMinutes)
                    }
                );
                
                // 验证Token是否成功缓存
                var cachedValue = await _cache.GetStringAsync(cacheKey);
                _logger.LogInformation("[CSRF] Token缓存结果: {Value}", cachedValue);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[CSRF] Token缓存失败");
                throw;
            }
        }

        private bool ShouldSkipCsrfCheck(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();
            var method = context.Request.Method;
            _logger.LogInformation("[CSRF] Checking path: {Method} {Path}", method, path);

            // 如果路径为空，不跳过验证
            if (string.IsNullOrEmpty(path))
            {
                _logger.LogInformation("[CSRF] Path is empty, not skipping");
                return false;
            }

            // 处理查询参数
            var pathWithoutQuery = path.Split('?')[0];
            _logger.LogInformation("[CSRF] Path without query: {Path}", pathWithoutQuery);

            // 1. 检查是否是SignalR请求
            if (pathWithoutQuery.StartsWith("/signalr"))
            {
                _logger.LogInformation("[CSRF] Skipping SignalR path: {Path}", pathWithoutQuery);
                return true;
            }

            // 2. 检查是否是OPTIONS请求
            if (method.Equals("OPTIONS", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation("[CSRF] Skipping OPTIONS request");
                return true;
            }

            // 3. 检查是否是其他需要跳过的路径
            var skipPaths = new[]
            {
                "/api/hbtauth/login",
                "/api/hbtauth/logout",
                "/api/hbtauth/check-login",  // 添加 check-login 路径
                "/api/hbtlanguage/supported",
                "/api/hbtonlineuser/force-offline",  // 添加强制下线接口
                "/swagger",
                "/_framework",
                "/_vs"
            };

            foreach (var skipPath in skipPaths)
            {
                if (pathWithoutQuery.StartsWith(skipPath, StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogInformation("[CSRF] Skipping path: {Path} matches pattern: {Pattern}", pathWithoutQuery, skipPath);
                    return true;
                }
            }

            _logger.LogInformation("[CSRF] Path requires CSRF validation: {Path}", pathWithoutQuery);
            return false;
        }

        private CookieOptions GetCookieOptions(HttpContext context)
        {
            return new CookieOptions
            {
                HttpOnly = false,  // 允许 JavaScript 访问
                Secure = true,     // 始终使用 HTTPS
                SameSite = SameSiteMode.None,  // 允许跨站点请求
                Path = "/",
                MaxAge = TimeSpan.FromMinutes(_options.CsrfTokenExpirationMinutes),
                Domain = null // 让浏览器自动设置域名
            };
        }
    }
}