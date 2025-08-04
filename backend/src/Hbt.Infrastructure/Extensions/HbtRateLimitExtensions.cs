//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtRateLimitExtensions.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:00
// 版本号 : V0.0.1
// 描述    : 限流中间件扩展方法
//===================================================================

using Microsoft.AspNetCore.Builder;
using Hbt.Cur.Infrastructure.Security;

namespace Hbt.Cur.Infrastructure.Extensions
{
    /// <summary>
    /// 限流中间件扩展方法
    /// </summary>
    public static class HbtRateLimitExtensions
    {
        /// <summary>
        /// 使用限流中间件
        /// </summary>
        /// <param name="app">应用程序构建器</param>
        /// <returns>应用程序构建器</returns>
        public static IApplicationBuilder UseHbtRateLimit(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HbtRateLimitMiddleware>();
        }
    }
} 