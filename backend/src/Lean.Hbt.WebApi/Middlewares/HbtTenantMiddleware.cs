using Lean.Hbt.Infrastructure.Services.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Lean.Hbt.WebApi.Middlewares
{
    /// <summary>
    /// 租户中间件,用于从请求中获取租户信息并设置到租户上下文中
    /// </summary>
    public class HbtTenantMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HbtTenantMiddleware> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public HbtTenantMiddleware(RequestDelegate next, ILogger<HbtTenantMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// 调用中间件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                _logger.LogInformation("[租户中间件] 开始处理请求: {Path}", context.Request.Path);

                // 1. 从请求头中获取租户ID
                if (context.Request.Headers.TryGetValue("X-Tenant-Id", out var tenantId))
                {
                    if (long.TryParse(tenantId, out var tid))
                    {
                        HbtCurrentTenant.CurrentTenantId = tid;
                        _logger.LogInformation("[租户中间件] 从请求头获取租户ID: {TenantId}", tid);
                    }
                }

                // 2. 如果请求头中没有租户ID,尝试从Claims中获取
                if (HbtCurrentTenant.CurrentTenantId == null && context.User?.Identity?.IsAuthenticated == true)
                {
                    var tenantClaim = context.User.FindFirst("tid");
                    if (tenantClaim != null && long.TryParse(tenantClaim.Value, out var tid))
                    {
                        HbtCurrentTenant.CurrentTenantId = tid;
                        _logger.LogInformation("[租户中间件] 从JWT获取租户ID: {TenantId}", tid);
                    }
                }

                _logger.LogInformation("[租户中间件] 当前租户ID: {TenantId}", HbtCurrentTenant.CurrentTenantId);

                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[租户中间件] 处理请求时发生错误");
                throw;
            }
            finally
            {
                // 请求结束时清除租户上下文
                HbtCurrentTenant.Clear();
                _logger.LogInformation("[租户中间件] 清除租户上下文");
            }
        }
    }
}