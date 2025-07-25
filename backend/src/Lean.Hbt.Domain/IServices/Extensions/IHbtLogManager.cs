//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtAuditsLog.cs
// 创建者 : Lean365
// 创建时间: 2024-03-05 18:00
// 版本号 : V0.0.1
// 描述   : 审计日志接口
//===================================================================

using System.Text.Json.Nodes;

namespace Lean.Hbt.Domain.IServices.Extensions;

/// <summary>
/// 基础日志管理接口
/// </summary>
public interface IHbtLogManager
{
    /// <summary>
    /// 记录错误日志
    /// </summary>
    void Error(string message, Exception ex);

    /// <summary>
    /// 记录严重错误日志
    /// </summary>
    void Fatal(string message, Exception ex);
}

/// <summary>
/// 审计日志管理接口
/// </summary>
public interface IHbtAuditLogManager
{
    /// <summary>
    /// 记录审计日志
    /// </summary>
    Task LogAuditAsync(
        string module,
        string operation,
        string method,
        string? parameters = null,
        string? result = null,
        long elapsed = 0);
}

/// <summary>
/// 差异日志管理接口
/// </summary>
public interface IHbtSqlDiffLogManager
{
    /// <summary>
    /// 记录数据变更日志
    /// </summary>
    Task LogDbDiffAsync(
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
        string? afterData = null);
}

/// <summary>
/// 操作日志管理接口
/// </summary>
public interface IHbtOperLogManager
{
    /// <summary>
    /// 记录操作日志
    /// </summary>
    Task LogOperationAsync(
        string tableName,
        string OperType,
        string businessKey,
        string? requestParam = null,
        string? location = null,
        bool success = true,
        string? errorMsg = null);
}

/// <summary>
/// 异常日志管理接口
/// </summary>
public interface IHbtExceptionLogManager
{
    /// <summary>
    /// 记录异常日志
    /// </summary>
    Task LogExceptionAsync(
        Exception ex,
        string? method = null,
        string? parameters = null);
}