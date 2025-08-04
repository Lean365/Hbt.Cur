//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPermExtensions.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:00
// 版本号 : V0.0.1
// 描述    : 权限中间件扩展方法
//===================================================================

using Hbt.Cur.Infrastructure.Security;
using Microsoft.AspNetCore.Builder;

namespace Hbt.Cur.Infrastructure.Extensions
{
    /// <summary>
    /// 权限中间件扩展方法
    /// </summary>
    public static class HbtPermExtensions
    {
        /// <summary>
        /// 使用权限中间件
        /// </summary>
        /// <param name="app">应用程序构建器</param>
        /// <returns>应用程序构建器</returns>
        public static IApplicationBuilder UseHbtPerm(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HbtPermMiddleware>();
        }
    }
}