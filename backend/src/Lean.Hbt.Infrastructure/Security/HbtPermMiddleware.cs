//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPermMiddleware.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:00
// 版本号 : V1.0.0
// 描述    : 权限中间件
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Infrastructure.Security.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Infrastructure.Security
{
    /// <summary>
    /// 权限中间件,用于验证用户是否拥有访问权限
    /// </summary>
    public class HbtPermMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly IHbtLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next">请求委托</param>
        /// <param name="logger">日志记录器</param>
        public HbtPermMiddleware(RequestDelegate next,
            IHbtLogger logger
                )
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
            _logger.Debug("[权限中间件] 开始处理请求: {Path}", context.Request.Path);

            // 获取当前请求的Endpoint
            var endpoint = context.GetEndpoint();
            if (endpoint == null)
            {
                _logger.Debug("[权限中间件] 未找到Endpoint,跳过权限验证");
                await _next(context);
                return;
            }

            // 检查是否有AllowAnonymous特性
            var allowAnonymous = endpoint.Metadata.GetMetadata<AllowAnonymousAttribute>();
            if (allowAnonymous != null)
            {
                _logger.Debug("[权限中间件] 发现AllowAnonymous特性,完全跳过权限验证");
                await _next(context);
                return;
            }

            // 获取权限特性
            var permAttribute = endpoint.Metadata.GetMetadata<HbtPermAttribute>();
            if (permAttribute == null)
            {
                _logger.Debug("[权限中间件] 未找到权限特性,跳过权限验证");
                await _next(context);
                return;
            }

            // 检查用户是否已认证
            if (!context.User.Identity?.IsAuthenticated ?? true)
            {
                _logger.Info("[权限中间件] 用户未认证,返回401");
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { message = "未认证" });
                return;
            }

            _logger.Debug("[权限中间件] 需要的权限: {Permission}", permAttribute.Permission);

            // 仅从Claims中获取用户ID
            var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "uid");
            if (userIdClaim == null)
            {
                _logger.Warn("[权限中间件] 未找到uid Claim");
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { message = "无效的认证信息" });
                return;
            }

            if (!long.TryParse(userIdClaim.Value, out long userId))
            {
                _logger.Warn("[权限中间件] uid Claim格式无效: {Value}", userIdClaim.Value);
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { message = "无效的用户ID" });
                return;
            }

            var isAdmin = context.User.Claims.Any(c => c.Type == "adm" && c.Value == "true");

            _logger.Debug("[权限中间件] 用户信息: UserId={UserId}, IsAdmin={IsAdmin}", userId, isAdmin);

            if (isAdmin)
            {
                _logger.Debug("[权限中间件] 用户是管理员，自动通过权限验证");
                await _next(context);
                return;
            }

            // 从请求作用域获取仓储实例
            var userRepository = context.RequestServices.GetRequiredService<IHbtRepository<HbtUser>>();

            // 直接获取用户权限列表
            var permissions = await userRepository.GetUserPermissionsAsync(userId);

            //_logger.Info("[权限中间件] 用户权限列表: {Permissions}", string.Join(", ", permissions));

            // 检查具体权限
            var hasPermission = permissions.Contains(permAttribute.Permission);
            _logger.Info("[权限中间件] 权限验证结果: {Result}, 所需权限: {Permission}",
                hasPermission ? "通过" : "未通过",
                permAttribute.Permission);

            if (!hasPermission)
            {
                _logger.Warn("[权限中间件] 用户 {UserId} 没有所需权限 {Permission}", userId, permAttribute.Permission);
                context.Response.StatusCode = 403;
                await context.Response.WriteAsJsonAsync(new { message = "无访问权限" });
                return;
            }

            await _next(context);
        }
    }
}