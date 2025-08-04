//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLogActionFilter.cs
// 创建者 : Lean365
// 创建时间: 2024-03-20
// 版本号 : V.0.0.1
// 描述    : 操作日志过滤器
//===================================================================

using System.Text.Json;
using Hbt.Cur.Domain.IServices.Extensions;
using Hbt.Cur.Infrastructure.Security.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hbt.Cur.Infrastructure.Security.Filters;

/// <summary>
/// 操作日志过滤器
/// </summary>
public class HbtLogActionFilter : IAsyncActionFilter
{
    private readonly IHbtOperLogManager _logManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHbtCurrentUser _currentUser;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtLogActionFilter(
        IHbtOperLogManager logManager,
        IHttpContextAccessor httpContextAccessor,
        IHbtCurrentUser currentUser)
    {
        _logManager = logManager;
        _httpContextAccessor = httpContextAccessor;
        _currentUser = currentUser;
    }

    /// <summary>
    /// 执行过滤器
    /// </summary>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var logAttr = context.ActionDescriptor.EndpointMetadata.OfType<HbtLogAttribute>().FirstOrDefault();
        if (logAttr == null)
        {
            await next();
            return;
        }

        try
        {
            await next();
            var result = context.Result;
            
            // 记录操作日志
            await LogOperationAsync(context, logAttr, true);
        }
        catch (Exception ex)
        {
            await LogOperationAsync(context, logAttr, false, ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 记录操作日志
    /// </summary>
    private async Task LogOperationAsync(ActionExecutingContext context, HbtLogAttribute logAttr, bool success, string? errorMsg = null)
    {
        var tableName = GetTableName(context);
        var OperType = GetOperType(context);
        var businessKey = GetBusinessKey(context);
        var requestParam = GetRequestParam(context);

        await _logManager.LogOperationAsync(
            tableName: tableName,
            OperType: OperType,
            businessKey: businessKey,
            requestParam: requestParam,
            success: success,
            errorMsg: errorMsg
        );
    }

    /// <summary>
    /// 获取表名
    /// </summary>
    private string GetTableName(ActionExecutingContext context)
    {
        var controllerType = context.Controller.GetType();
        var moduleName = controllerType.Name.Replace("Controller", "");
        return moduleName;
    }

    /// <summary>
    /// 获取操作类型
    /// </summary>
    private string GetOperType(ActionExecutingContext context)
    {
        var methodName = context.ActionDescriptor.RouteValues["action"]?.ToLower() ?? string.Empty;
        if (methodName.StartsWith("create") || methodName.StartsWith("add"))
            return "create";
        if (methodName.StartsWith("update") || methodName.StartsWith("modify"))
            return "update";
        if (methodName.StartsWith("delete") || methodName.StartsWith("remove"))
            return "delete";
        if (methodName.StartsWith("get") || methodName.StartsWith("query"))
            return "query";
        return "other";
    }

    /// <summary>
    /// 获取业务主键
    /// </summary>
    private string GetBusinessKey(ActionExecutingContext context)
    {
        var args = context.ActionArguments;
        if (args.Count == 0) return string.Empty;

        // 尝试从参数中获取ID
        foreach (var arg in args.Values)
        {
            if (arg == null) continue;

            var idProperty = arg.GetType().GetProperty("Id");
            if (idProperty != null)
            {
                var idValue = idProperty.GetValue(arg);
                if (idValue != null)
                    return idValue.ToString() ?? string.Empty;
            }
        }

        return string.Empty;
    }

    /// <summary>
    /// 获取请求参数
    /// </summary>
    private string GetRequestParam(ActionExecutingContext context)
    {
        try
        {
            var args = context.ActionArguments;
            if (args.Count == 0) return string.Empty;

            var paramList = new List<object>();
            foreach (var arg in args.Values)
            {
                if (arg == null) continue;
                paramList.Add(arg);
            }

            return JsonSerializer.Serialize(paramList);
        }
        catch
        {
            return string.Empty;
        }
    }
} 