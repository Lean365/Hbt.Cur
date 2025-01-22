using Microsoft.AspNetCore.Builder;
using Lean.Hbt.WebApi.Middlewares;

namespace Lean.Hbt.WebApi.Extensions
{
    /// <summary>
    /// 租户中间件扩展方法
    /// </summary>
    public static class HbtTenantExtensions
    {
        /// <summary>
        /// 使用租户中间件
        /// </summary>
        public static IApplicationBuilder UseHbtTenant(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HbtTenantMiddleware>();
        }
    }
} 