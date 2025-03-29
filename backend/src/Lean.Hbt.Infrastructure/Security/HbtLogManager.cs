//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtAuditLog.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 10:00
// 版本号 : V.0.0.1
// 描述    : 审计日志实现
//===================================================================

using Lean.Hbt.Domain.Entities.Audit;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Domain.IServices.Audit;
using Lean.Hbt.Domain.IServices.Identity;
using Lean.Hbt.Infrastructure.Data.Contexts;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using SqlSugar;

namespace Lean.Hbt.Infrastructure.Security;

/// <summary>
/// 日志记录器实现
/// </summary>
public class HbtLogManager : 
    IHbtLogManager,
    IHbtAuditLogManager,
    IHbtDbDiffLogManager,
    IHbtOperLogManager,
    IHbtExceptionLogManager
{
    private readonly IHbtLogger _logger;
    private readonly HbtDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHbtUserContext _userContext;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtLogManager(
        IHbtLogger logger,
        HbtDbContext context,
        IHttpContextAccessor httpContextAccessor,
        IHbtUserContext userContext)
    {
        _logger = logger;
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _userContext = userContext;

        // 配置SqlSugar差异日志
        StaticConfig.CompleteInsertableFunc = it =>
        {
            var method = it.GetType().GetMethod("EnableDiffLogEvent");
            if (method != null)
            {
                method.Invoke(it, new object[] { null });
            }
        };

        StaticConfig.CompleteUpdateableFunc = it =>
        {
            var method = it.GetType().GetMethod("EnableDiffLogEvent");
            if (method != null)
            {
                method.Invoke(it, new object[] { null });
            }
        };

        StaticConfig.CompleteDeleteableFunc = it =>
        {
            var method = it.GetType().GetMethod("EnableDiffLogEvent");
            if (method != null)
            {
                method.Invoke(it, new object[] { null });
            }
        };
    }

    /// <summary>
    /// 获取日志级别
    /// </summary>
    private int GetLogLevel(string operationType)
    {
        return operationType.ToLower() switch
        {
            // 新增类操作 - 信息级别
            "create" or "insert" or "add" => 1,
            // 修改类操作 - 警告级别
            "update" or "modify" or "edit" or"alter" => 2,
            // 删除类操作 - 高危级别
            "delete" or "remove" or "drop" => 3,
            // 异常情况 - 错误级别
            "error" or "exception" => 4,
            // 默认为信息级别
            _ => 1
        };
    }

    /// <summary>
    /// 记录数据变更日志
    /// </summary>
    public async Task LogDbDiffAsync(
        string tableName,
        string changeType,
        string? columnName = null,
        string? oldDataType = null,
        string? newDataType = null,
        int? oldLength = null,
        int? newLength = null,
        int? oldIsNullable = null,
        int? newIsNullable = null,
        string? changeDescription = null,
        string? executeSql = null,
        string? sqlParameters = null,
        string? beforeData = null,
        string? afterData = null)
    {
        var log = new HbtDbDiffLog
        {
            LogLevel = GetLogLevel(changeType),
            TableName = tableName,
            ChangeType = changeType,
            ColumnName = columnName,
            OldDataType = oldDataType,
            NewDataType = newDataType,
            OldLength = oldLength,
            NewLength = newLength,
            OldIsNullable = oldIsNullable,
            NewIsNullable = newIsNullable,
            ChangeDescription = changeDescription ?? string.Empty,
            ExecuteSql = executeSql,
            SqlParameters = sqlParameters,
            BeforeData = beforeData,
            AfterData = afterData,
            TenantId = _userContext.TenantId,
            CreateBy = _userContext.UserName,
            CreateTime = DateTime.Now
        };

        try
        {
            await _context.Client.Insertable(log).ExecuteCommandAsync();
            _logger.Info($"记录数据变更日志成功: {_userContext.UserName} 对表 {tableName} 执行了 {changeType} 操作");
        }
        catch (Exception ex)
        {
            _logger.Error("记录数据变更日志失败", ex);
            throw;
        }
    }

    /// <summary>
    /// 记录操作日志
    /// </summary>
    public async Task LogOperationAsync(
        string tableName,
        string operationType,
        string businessKey,
        string? requestParam = null,
        string? location = null,
        bool success = true,
        string? errorMsg = null)
    {
        var log = new HbtOperLog
        {
            LogLevel = success ? GetLogLevel(operationType) : 4, // 失败时使用错误级别
            UserId = _userContext.UserId,
            UserName = _userContext.UserName,
            TenantId = _userContext.TenantId,
            TableName = tableName,
            OperationType = operationType,
            BusinessKey = businessKey,
            RequestMethod = _httpContextAccessor.HttpContext?.Request.Method ?? "Unknown",
            RequestParam = requestParam,
            IpAddress = GetClientIpAddress(),
            Location = location,
            Status = success ? 0 : 1,
            ErrorMsg = errorMsg,
            CreateBy = _userContext.UserName,
            CreateTime = DateTime.Now
        };

        try
        {
            await _context.Client.Insertable(log).ExecuteCommandAsync();
            _logger.Info($"记录操作日志成功: {_userContext.UserName} 在 {tableName} 执行了 {operationType} 操作");
        }
        catch (Exception ex)
        {
            _logger.Error("记录操作日志失败", ex);
            throw;
        }
    }

    /// <summary>
    /// 记录异常日志
    /// </summary>
    public async Task LogExceptionAsync(
        Exception ex,
        string? method = null,
        string? parameters = null)
    {
        var log = new HbtExceptionLog
        {
            LogLevel = 4, // 异常统一使用错误级别
            UserId = _userContext.UserId,
            UserName = _userContext.UserName,
            TenantId = _userContext.TenantId,
            Method = method ?? _httpContextAccessor.HttpContext?.Request.Path ?? "Unknown",
            Parameters = parameters,
            ExceptionType = ex.GetType().FullName ?? "Unknown",
            ExceptionMessage = ex.Message ?? "No message",
            StackTrace = ex.StackTrace ?? "No stack trace",
            IpAddress = GetClientIpAddress(),
            UserAgent = GetUserAgent(),
            CreateBy = _userContext.UserName,
            CreateTime = DateTime.Now
        };

        try
        {
            await _context.Client.Insertable(log).ExecuteCommandAsync();
            _logger.Error($"记录异常日志成功: {_userContext.UserName} 在执行 {method ?? "Unknown"} 时发生异常", ex);
        }
        catch (Exception innerEx)
        {
            _logger.Error("记录异常日志失败", innerEx);
            throw;
        }
    }

    /// <summary>
    /// 获取客户端IP地址
    /// </summary>
    private string GetClientIpAddress()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null) return "127.0.0.1";

        var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault() ??
                 context.Request.Headers["X-Real-IP"].FirstOrDefault() ??
                 context.Connection.RemoteIpAddress?.ToString();

        return ip ?? "127.0.0.1";
    }

    /// <summary>
    /// 获取客户端UserAgent
    /// </summary>
    private string GetUserAgent()
    {
        var context = _httpContextAccessor.HttpContext;
        return context?.Request.Headers["User-Agent"].ToString() ?? "Unknown";
    }

    /// <summary>
    /// 记录日志
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public void Error(string message, Exception ex)
    {
        _logger.Error(message, ex);
    }

    /// <summary>
    /// 记录日志
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public void Fatal(string message, Exception ex)
    {
        _logger.Fatal(message, ex);
    }

    /// <summary>
    /// 记录审计日志
    /// </summary>
    public async Task LogAuditAsync(
        string module,
        string operation,
        string method,
        string? parameters = null,
        string? result = null,
        long elapsed = 0)
    {
        var log = new HbtAuditLog
        {
            LogLevel = GetLogLevel(operation),
            UserId = _userContext.UserId,
            UserName = _userContext.UserName,
            TenantId = _userContext.TenantId,
            Module = module,
            Operation = operation,
            Method = method,
            Parameters = parameters,
            Result = result,
            Elapsed = elapsed,
            IpAddress = GetClientIpAddress(),
            UserAgent = GetUserAgent(),
            RequestUrl = _httpContextAccessor.HttpContext?.Request.Path ?? string.Empty,
            RequestMethod = _httpContextAccessor.HttpContext?.Request.Method ?? string.Empty,
            CreateBy = _userContext.UserName,
            CreateTime = DateTime.Now
        };

        try
        {
            await _context.Client.Insertable(log).ExecuteCommandAsync();
            _logger.Info($"记录审计日志成功: {_userContext.UserName} 在 {module} 模块执行了 {operation} 操作");
        }
        catch (Exception ex)
        {
            _logger.Error("记录审计日志失败", ex);
            throw;
        }
    }
}