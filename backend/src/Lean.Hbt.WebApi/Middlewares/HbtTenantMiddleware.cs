using Lean.Hbt.Infrastructure.Data.Contexts;

namespace Lean.Hbt.WebApi.Middlewares
{
    /// <summary>
    /// 租户中间件,用于从请求中获取租户信息并设置到租户上下文中
    /// </summary>
    public class HbtTenantMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        public HbtTenantMiddleware(RequestDelegate next)
        {
            _next = next;
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
                // 1. 从请求头中获取租户ID
                if (context.Request.Headers.TryGetValue("X-Tenant-Id", out var tenantId))
                {
                    if (long.TryParse(tenantId, out var tid))
                    {
                        HbtTenantContext.CurrentTenantId = tid;
                    }
                }

                // 2. 如果请求头中没有租户ID,尝试从Claims中获取
                if (HbtTenantContext.CurrentTenantId == null && context.User?.Identity?.IsAuthenticated == true)
                {
                    var tenantClaim = context.User.FindFirst("tenant_id");
                    if (tenantClaim != null && long.TryParse(tenantClaim.Value, out var tid))
                    {
                        HbtTenantContext.CurrentTenantId = tid;
                    }
                }

                await _next(context);
            }
            finally
            {
                // 请求结束时清除租户上下文
                HbtTenantContext.Clear();
            }
        }
    }
}