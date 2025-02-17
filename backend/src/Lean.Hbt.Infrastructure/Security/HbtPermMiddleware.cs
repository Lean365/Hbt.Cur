//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPermMiddleware.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:00
// 版本号 : V1.0.0
// 描述    : 权限中间件
//===================================================================

using Microsoft.AspNetCore.Http;
using Lean.Hbt.Infrastructure.Security.Attributes;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace Lean.Hbt.Infrastructure.Security
{
    /// <summary>
    /// 权限中间件,用于验证用户是否拥有访问权限
    /// </summary>
    public class HbtPermMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next">请求委托</param>
        public HbtPermMiddleware(RequestDelegate next)
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
            // 获取当前请求的Endpoint
            var endpoint = context.GetEndpoint();
            if (endpoint == null)
            {
                await _next(context);
                return;
            }

            // 获取权限特性
            var permAttribute = endpoint.Metadata.GetMetadata<HbtPermAttribute>();
            if (permAttribute == null)
            {
                await _next(context);
                return;
            }

            // 从Claims中获取用户权限列表
            var userPerms = context.User.Claims
                .Where(c => c.Type == "permissions")
                .Select(c => c.Value)
                .ToList();

            // 验证用户是否拥有所需权限
            if (!userPerms.Contains(permAttribute.Permission))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsJsonAsync(new { message = "无访问权限" });
                return;
            }

            await _next(context);
        }
    }
}