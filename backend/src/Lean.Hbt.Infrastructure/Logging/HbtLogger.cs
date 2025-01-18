//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLogger.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 19:10
// 版本号 : V0.0.1
// 描述   : 日志实现类
//===================================================================

using NLog;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Infrastructure.Logging;

/// <summary>
/// 日志实现类
/// </summary>
public class HbtLogger : IHbtLogger
{
    private readonly ILogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtLogger()
    {
        _logger = LogManager.GetCurrentClassLogger();
    }

    /// <inheritdoc/>
    public void Debug(string message)
    {
        _logger.Debug(message);
    }

    /// <inheritdoc/>
    public void Debug(string message, params object[] args)
    {
        _logger.Debug(message, args);
    }

    /// <inheritdoc/>
    public void Info(string message)
    {
        _logger.Info(message);
    }

    /// <inheritdoc/>
    public void Info(string message, params object[] args)
    {
        _logger.Info(message, args);
    }

    /// <inheritdoc/>
    public void Warn(string message)
    {
        _logger.Warn(message);
    }

    /// <inheritdoc/>
    public void Warn(string message, params object[] args)
    {
        _logger.Warn(message, args);
    }

    /// <inheritdoc/>
    public void Error(string message)
    {
        _logger.Error(message);
    }

    /// <inheritdoc/>
    public void Error(string message, Exception ex)
    {
        _logger.Error(ex, message);
    }

    /// <inheritdoc/>
    public void Error(string message, params object[] args)
    {
        _logger.Error(message, args);
    }

    /// <inheritdoc/>
    public void Fatal(string message)
    {
        _logger.Fatal(message);
    }

    /// <inheritdoc/>
    public void Fatal(string message, Exception ex)
    {
        _logger.Fatal(ex, message);
    }

    /// <inheritdoc/>
    public void Fatal(string message, params object[] args)
    {
        _logger.Fatal(message, args);
    }
} 