//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtAuditLog.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 10:00
// 版本号 : V.0.0.1
// 描述    : 审计日志实现
//===================================================================

using System.Text.Json;
using Hbt.Domain.Entities.Audit;
using Hbt.Infrastructure.Data.Contexts;
using Microsoft.AspNetCore.Http;

namespace Hbt.Infrastructure.Security;

/// <summary>
/// 日志记录器实现
/// </summary>
public class HbtLogManager :
    IHbtLogManager,
    IHbtSqlDiffLogManager,
    IHbtOperLogManager,
    IHbtExceptionLogManager
{
    private readonly IHbtLogger _logger;
    private readonly HbtDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHbtCurrentUser _currentUser;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtLogManager(
        IHbtLogger logger,
        HbtDbContext context,
        IHttpContextAccessor httpContextAccessor,
        IHbtCurrentUser currentUser)
    {
        _logger = logger;
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _currentUser = currentUser;

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
    private int GetLogLevel(string OperType)
    {
        return OperType.ToLower() switch
        {
            // 新增类操作 - 信息级别
            "create" or "insert" or "add" => 1,
            // 修改类操作 - 警告级别
            "update" or "modify" or "edit" or "alter" => 2,
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
        var log = new HbtSqlDiffLog
        {
            DiffType = changeType,
            TableName = tableName,
            BusinessName = changeDescription ?? string.Empty,
            PrimaryKey = columnName,
            BeforeData = beforeData,
            AfterData = afterData,
            DiffFields = JsonSerializer.Serialize(new
            {
                OldDataType = oldDataType,
                NewDataType = newDataType,
                OldLength = oldLength,
                NewLength = newLength,
                OldIsNullable = oldIsNullable,
                NewIsNullable = newIsNullable
            }),
            ExecuteSql = executeSql,
            SqlParameters = sqlParameters,

            CreateBy = _currentUser.UserName,
            CreateTime = DateTime.Now
        };

        try
        {
            await _context.Client.Insertable(log).ExecuteCommandAsync();
            _logger.Info($"记录数据变更日志成功: {_currentUser.UserName} 对表 {tableName} 执行了 {changeType} 操作");
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
        string OperType,
        string businessKey,
        string? requestParam = null,
        string? location = null,
        bool success = true,
        string? errorMsg = null)
    {
        var log = new HbtOperLog
        {
            OperModule = tableName,
            OperType = OperType,
            OperTableName = tableName,
            OperBusinessKey = businessKey,
            OperRequestMethod = _httpContextAccessor.HttpContext?.Request.Method ?? "Unknown",
            OperRequestParam = requestParam,
            OperIpAddress = GetClientIpAddress(),
            OperLocation = location,
            OperStatus = success ? 0 : 1,
            OperErrorMsg = errorMsg,
            CreateBy = _currentUser.UserName,
            CreateTime = DateTime.Now
        };

        try
        {
            await _context.Client.Insertable(log).ExecuteCommandAsync();
            _logger.Info($"记录操作日志成功: {_currentUser.UserName} 在 {tableName} 执行了 {OperType} 操作");
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
            UserId = _currentUser.UserId,
            UserName = _currentUser.UserName,

            Method = method ?? _httpContextAccessor.HttpContext?.Request.Path ?? "Unknown",
            Parameters = parameters,
            ExceptionType = ex.GetType().FullName ?? "Unknown",
            ExceptionMessage = ex.Message ?? "No message",
            StackTrace = ex.StackTrace ?? "No stack trace",
            IpAddress = GetClientIpAddress(),
            UserAgent = GetUserAgent(),
            CreateBy = _currentUser.UserName,
            CreateTime = DateTime.Now
        };

        try
        {
            await _context.Client.Insertable(log).ExecuteCommandAsync();
            _logger.Error($"记录异常日志成功: {_currentUser.UserName} 在执行 {method ?? "Unknown"} 时发生异常", ex);
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
    /// 记录错误日志
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
}