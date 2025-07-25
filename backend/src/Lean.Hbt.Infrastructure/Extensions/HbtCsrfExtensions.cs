//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtCsrfExtensions.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:00
// 版本号 : V0.0.1
// 描述    : CSRF防护中间件扩展
//===================================================================

using Microsoft.AspNetCore.Builder;
using Lean.Hbt.Infrastructure.Security;

namespace Lean.Hbt.Infrastructure.Extensions
{
    /// <summary>
    /// CSRF防护中间件扩展
    /// </summary>
    public static class HbtCsrfExtensions
    {
        /// <summary>
        /// 使用CSRF防护中间件
        /// </summary>
        public static IApplicationBuilder UseHbtCsrf(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HbtCsrfMiddleware>();
        }
    }
} 