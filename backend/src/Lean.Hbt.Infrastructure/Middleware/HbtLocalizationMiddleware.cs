//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLocalizationMiddleware.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 本地化中间件
//===================================================================

using Microsoft.AspNetCore.Http;
using Lean.Hbt.Domain.IServices.Admin;

namespace Lean.Hbt.Infrastructure.Middleware;

/// <summary>
/// 本地化中间件
/// </summary>
public class HbtLocalizationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHbtLocalizationService _localizationService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next">请求委托</param>
    /// <param name="localizationService">本地化服务</param>
    public HbtLocalizationMiddleware(RequestDelegate next, IHbtLocalizationService localizationService)
    {
        _next = next;
        _localizationService = localizationService;
    }

    /// <summary>
    /// 处理请求
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    public async Task InvokeAsync(HttpContext context)
    {
        // 1. 从请求头获取语言
        var langHeader = context.Request.Headers["Accept-Language"].ToString();
        if (!string.IsNullOrEmpty(langHeader))
        {
            var lang = langHeader.Split(',')[0];
            _localizationService.SetLanguage(lang);
        }
        // 2. 如果请求头没有语言信息，尝试从Cookie获取
        else if (context.Request.Cookies.TryGetValue("lang", out var langCookie))
        {
            _localizationService.SetLanguage(langCookie);
        }

        await _next(context);
    }
} 