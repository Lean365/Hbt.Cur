//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLocalizationExtensions.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 本地化扩展方法
//===================================================================

using Lean.Hbt.Domain.IServices.Admin;
using Lean.Hbt.Infrastructure.Services;
using Lean.Hbt.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Lean.Hbt.Infrastructure.Extensions;

/// <summary>
/// 本地化扩展方法
/// </summary>
public static class HbtLocalizationExtensions
{
    /// <summary>
    /// 添加本地化服务
    /// </summary>
    public static IServiceCollection AddHbtLocalization(this IServiceCollection services)
    {
        // 注册HttpContext访问器
        services.AddHttpContextAccessor();

        // 注册本地化服务（单例模式）
        services.AddSingleton<IHbtLocalizationService, HbtLocalizationService>();

        return services;
    }

    /// <summary>
    /// 使用本地化服务
    /// </summary>
    public static IApplicationBuilder UseHbtLocalization(this IApplicationBuilder app)
    {
        // 添加本地化中间件
        app.UseMiddleware<HbtLocalizationMiddleware>();

        return app;
    }
} 