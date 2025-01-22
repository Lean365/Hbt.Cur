//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtRateLimitMiddleware.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:00
// 版本号 : V1.0.0
// 描述    : 限流中间件
//===================================================================

using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Infrastructure.Security
{
    /// <summary>
    /// 限流中间件,基于令牌桶算法实现
    /// </summary>
    public class HbtRateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ConcurrentDictionary<string, TokenBucket> _buckets = new();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next">请求委托</param>
        public HbtRateLimitMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="context">HTTP上下文</param>
        /// <returns>异步任务</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var bucket = _buckets.GetOrAdd(ip, _ => new TokenBucket());

            if (!bucket.TryTake())
            {
                context.Response.StatusCode = 429; // Too Many Requests
                await context.Response.WriteAsJsonAsync(new { message = "请求过于频繁,请稍后再试" });
                return;
            }

            await _next(context);
        }

        /// <summary>
        /// 令牌桶
        /// </summary>
        private class TokenBucket
        {
            private const int Capacity = 100; // 桶容量
            private const int RefillRate = 10; // 每秒补充速率
            private double _tokens;
            private DateTime _lastRefill;
            private readonly object _lock = new();

            public TokenBucket()
            {
                _tokens = Capacity;
                _lastRefill = DateTime.UtcNow;
            }

            public bool TryTake()
            {
                lock (_lock)
                {
                    RefillTokens();
                    if (_tokens < 1) return false;
                    _tokens--;
                    return true;
                }
            }

            private void RefillTokens()
            {
                var now = DateTime.UtcNow;
                var elapsed = (now - _lastRefill).TotalSeconds;
                var tokensToAdd = elapsed * RefillRate;
                _tokens = Math.Min(Capacity, _tokens + tokensToAdd);
                _lastRefill = now;
            }
        }
    }
} 