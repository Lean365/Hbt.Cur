#nullable enable

using Lean.Hbt.Application.Services.Audit;
using Lean.Hbt.Application.Dtos.Audit;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Lean.Hbt.WebApi.Middlewares;

/// <summary>
/// 租户日志中间件
/// </summary>
public class HbtTenantLogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<HbtTenantLogMiddleware> _logger;
    private readonly IHbtCurrentTenant _currentTenant;
    private readonly IHbtCurrentUser _currentUser;
    private readonly IHbtTenantLogService _logService;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtTenantLogMiddleware(
        RequestDelegate next,
        ILogger<HbtTenantLogMiddleware> logger,
        IHbtCurrentTenant currentTenant,
        IHbtCurrentUser currentUser,
        IHbtTenantLogService logService)
    {
        _next = next;
        _logger = logger;
        _currentTenant = currentTenant;
        _currentUser = currentUser;
        _logService = logService;
    }

    /// <summary>
    /// 中间件执行方法
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // 检查是否是登录相关的API
            if (IsLoginApi(context.Request.Path))
            {
                await _next(context);
                return;
            }

            // 检查是否是认证用户
            if (!_currentUser.IsAuthenticated)
            {
                await _next(context);
                return;
            }

            // 获取当前租户ID
            var tenantId = _currentTenant.TenantId;
            // 租户ID为0（无效）或-1（未启用）时跳过日志记录
            if (tenantId == 0 || tenantId == -1)
            {
                await _next(context);
                return;
            }

            // 记录日志
            await RecordLogAsync(context);

            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "租户日志中间件发生错误");
            throw;
        }
    }

    /// <summary>
    /// 检查是否是登录相关的API
    /// </summary>
    private bool IsLoginApi(PathString path)
    {
        var noLogPaths = new[]
        {
            "/api/hbtauth/login",
            "/api/hbtauth/register",
            "/api/hbtauth/refresh-token",
            "/api/hbtauth/logout",
            "/api/hbtauth/info",
            "/api/hbtauth/captcha",
            "/api/hbtauth/verify-captcha",
            "/api/hbtauth/salt",
            "/api/hbtauth/check-login",
            "/api/hbtauth/lockout",
            "/api/hbtauth/attempts",
            "/api/hbtlanguage/options",
            "/api/hbtlanguage/supported",
            "/api/hbttenant/options"
        };

        return noLogPaths.Any(p => path.StartsWithSegments(p));
    }

    /// <summary>
    /// 记录日志
    /// </summary>
    private async Task RecordLogAsync(HttpContext context)
    {
        try
        {
            var action = GetActionFromPath(context.Request.Path);
            if (string.IsNullOrEmpty(action))
            {
                return;
            }

            var details = await GetRequestDetailsAsync(context);
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();
            var userAgent = context.Request.Headers["User-Agent"].ToString();

            await _logService.CreateAsync(new HbtTenantLogCreateDto
            {
                TenantId = _currentTenant.TenantId,
                UserId = _currentUser.UserId,
                Action = action,
                Details = details,
                IpAddress = ipAddress,
                UserAgent = userAgent
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "记录租户日志失败");
        }
    }

    /// <summary>
    /// 从路径获取操作类型
    /// </summary>
    private string GetActionFromPath(PathString path)
    {
        var segments = path.Value?.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (segments == null || segments.Length < 3)
        {
            return string.Empty;
        }

        var method = path.Value?.ToUpper();
        var action = segments[2].ToUpper();

        return $"{method}_{action}";
    }

    /// <summary>
    /// 获取请求详情
    /// </summary>
    private async Task<string?> GetRequestDetailsAsync(HttpContext context)
    {
        try
        {
            if (context.Request.ContentLength == null || context.Request.ContentLength == 0)
            {
                return null;
            }

            context.Request.EnableBuffering();
            using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            return body;
        }
        catch
        {
            return null;
        }
    }
}