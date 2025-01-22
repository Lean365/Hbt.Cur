//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSqlInjectionExtensions.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:00
// 版本号 : V1.0.0
// 描述    : SQL注入防护中间件扩展
//===================================================================

using Microsoft.AspNetCore.Builder;
using Lean.Hbt.Infrastructure.Security;

namespace Lean.Hbt.Infrastructure.Extensions
{
    /// <summary>
    /// SQL注入防护中间件扩展
    /// </summary>
    public static class HbtSqlInjectionExtensions
    {
        /// <summary>
        /// 使用SQL注入防护中间件
        /// </summary>
        public static IApplicationBuilder UseHbtSqlInjection(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HbtSqlInjectionMiddleware>();
        }
    }
} 