//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSessionSecurityExtensions.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:00
// 版本号 : V1.0.0
// 描述    : 会话安全中间件扩展
//===================================================================

using Microsoft.AspNetCore.Builder;
using Lean.Hbt.Infrastructure.Security;

namespace Lean.Hbt.Infrastructure.Extensions
{
    /// <summary>
    /// 会话安全中间件扩展
    /// </summary>
    public static class HbtSessionSecurityExtensions
    {
        /// <summary>
        /// 使用会话安全中间件
        /// </summary>
        public static IApplicationBuilder UseHbtSessionSecurity(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HbtSessionSecurityMiddleware>();
        }
    }
} 