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
                // 检查是否已经有Cookie Token
                var existingToken = context.Request.Cookies[CsrfTokenCookie];
                if (string.IsNullOrEmpty(existingToken))
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

        /// <summary>
        /// 验证CSRF Token
        /// </summary>
        private async Task<bool> ValidateCsrfTokenAsync(HttpContext context)
        {
            try
            {
                // 1. 获取请求头中的Token
                var requestToken = context.Request.Headers[CsrfTokenHeader].ToString();
                _logger.LogInformation("[CSRF] 请求Token: {Token}", requestToken);

                // 2. 获取Cookie中的Token
                var cookieToken = context.Request.Cookies[CsrfTokenCookie];
                _logger.LogInformation("[CSRF] Cookie Token: {Token}", cookieToken);

                if (string.IsNullOrEmpty(requestToken) || string.IsNullOrEmpty(cookieToken))
                {
                    _logger.LogWarning("[CSRF] 请求或Cookie中缺少Token");
                    return false;
                }

                // 3. 验证Token是否匹配
                if (requestToken != cookieToken)
                {
                    _logger.LogWarning("[CSRF] Token不匹配");
                    return false;
                }

                // 4. 验证Token是否在缓存中
                var cacheKey = $"csrf:token:{requestToken}";
                _logger.LogInformation("[CSRF] 开始验证缓存Token: {Key}", cacheKey);
                
                var cachedToken = await _cache.GetStringAsync(cacheKey);
                _logger.LogInformation("[CSRF] 缓存Token: {Token}", cachedToken);
                
                if (string.IsNullOrEmpty(cachedToken))
                {
                    _logger.LogWarning("[CSRF] 缓存中未找到Token");
                    return false;
                }

                // 5. 刷新Token的缓存时间
                await _cache.SetStringAsync(
                    cacheKey,
                    requestToken,
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_options.CsrfTokenExpirationMinutes)
                    }
                );

                _logger.LogInformation("[CSRF] Token验证成功");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[CSRF] Token验证失败");
                return false;
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
                SameSite = SameSiteMode.Lax,  // 使用宽松的 SameSite 策略
                Path = "/",
                MaxAge = TimeSpan.FromMinutes(_options.CsrfTokenExpirationMinutes)
            };
        }
    }
}