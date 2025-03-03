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
using Microsoft.Extensions.Logging;

namespace Lean.Hbt.Infrastructure.Security
{
    /// <summary>
    /// 权限中间件,用于验证用户是否拥有访问权限
    /// </summary>
    public class HbtPermMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HbtPermMiddleware> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next">请求委托</param>
        /// <param name="logger">日志记录器</param>
        public HbtPermMiddleware(RequestDelegate next, ILogger<HbtPermMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="context">HTTP上下文</param>
        /// <returns>异步任务</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation("[权限中间件] 开始处理请求: {Path}", context.Request.Path);

            // 获取当前请求的Endpoint
            var endpoint = context.GetEndpoint();
            if (endpoint == null)
            {
                _logger.LogInformation("[权限中间件] 未找到Endpoint,跳过权限验证");
                await _next(context);
                return;
            }

            // 获取权限特性
            var permAttribute = endpoint.Metadata.GetMetadata<HbtPermAttribute>();
            if (permAttribute == null)
            {
                _logger.LogInformation("[权限中间件] 未找到权限特性,跳过权限验证");
                await _next(context);
                return;
            }

            _logger.LogInformation("[权限中间件] 需要的权限: {Permission}", permAttribute.Permission);

            // 从Claims中获取用户权限列表
            var claims = context.User.Claims.ToList();
            _logger.LogInformation("[权限中间件] 用户Claims: {@Claims}", claims.Select(c => new { c.Type, c.Value }));

            var userPerms = claims
                .Where(c => c.Type == "permissions")
                .Select(c => c.Value)
                .ToList();

            _logger.LogInformation("[权限中间件] 用户权限列表: {@Permissions}", userPerms);

            // 验证用户是否拥有所需权限
            if (!userPerms.Contains(permAttribute.Permission))
            {
                _logger.LogWarning("[权限中间件] 权限验证失败: 用户没有所需权限 {Permission}", permAttribute.Permission);
                context.Response.StatusCode = 403;
                await context.Response.WriteAsJsonAsync(new { message = "无访问权限" });
                return;
            }

            _logger.LogInformation("[权限中间件] 权限验证通过");
            await _next(context);
        }
    }
}