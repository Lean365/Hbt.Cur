#nullable enable

using Lean.Hbt.Common.Constants;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Domain.IServices.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Lean.Hbt.WebApi.Middlewares;

/// <summary>
/// 租户中间件
/// </summary>
/// <remarks>
/// 1. 从请求头或JWT中获取租户ID
/// 2. 设置租户上下文
/// </remarks>
public class HbtTenantMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<HbtTenantMiddleware> _logger;
    private readonly IConfiguration _configuration;
    private readonly IHbtCurrentTenant _currentTenant;
    private readonly IHbtCurrentUser _currentUser;
    private readonly bool _tenantEnabled;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtTenantMiddleware(
        RequestDelegate next,
        ILogger<HbtTenantMiddleware> logger,
        IConfiguration configuration,
        IHbtCurrentTenant currentTenant,
        IHbtCurrentUser currentUser)
    {
        _next = next;
        _logger = logger;
        _configuration = configuration;
        _currentTenant = currentTenant;
        _currentUser = currentUser;
        _tenantEnabled = _configuration.GetValue<bool>("Tenant:Enabled", false);
    }

    /// <summary>
    /// 中间件执行方法
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            _logger.LogInformation("[租户中间件] 开始处理请求: {Path}", context.Request.Path);

            // 如果租户功能未启用，直接跳过处理
            if (!_tenantEnabled)
            {
                _logger.LogInformation("[租户中间件] 租户功能未启用，跳过处理");
                await _next(context);
                return;
            }

            // 1. 从请求头中获取租户ID
            if (context.Request.Headers.TryGetValue("X-Tenant-Id", out var tenantId))
            {
                if (long.TryParse(tenantId, out var tid))
                {
                    _currentTenant.SetTenantId(tid);
                    _logger.LogInformation("[租户中间件] 从请求头获取租户ID: {TenantId}", tid);
                }
                else
                {
                    _logger.LogWarning("[租户中间件] 请求头中的租户ID格式无效: {TenantId}", tenantId);
                }
            }

            // 2. 如果请求头中没有租户ID,尝试从Claims中获取
            if (_currentUser.IsAuthenticated)
            {
                var tenantClaim = context.User.FindFirst("tid");
                if (tenantClaim != null && long.TryParse(tenantClaim.Value, out var tid))
                {
                    _currentTenant.SetTenantId(tid);
                    _logger.LogInformation("[租户中间件] 从JWT获取租户ID: {TenantId}", tid);
                }
            }

            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[租户中间件] 处理请求时发生错误");
            throw;
        }
    }
}