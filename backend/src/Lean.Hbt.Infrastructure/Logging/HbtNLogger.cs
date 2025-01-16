//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtNLogger.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-16 10:00
// 版本号 : V0.0.1
// 描述    : NLog日志服务实现
//===================================================================

using NLog;
using ILogger = NLog.ILogger;

namespace Lean.Hbt.Infrastructure.Logging
{
    /// <summary>
    /// NLog日志服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    public class HbtNLogger : IHbtLogger
    {
        /// <summary>
        /// NLog日志记录器
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="loggerName">日志记录器名称</param>
        public HbtNLogger(string loggerName)
        {
            _logger = LogManager.GetLogger(loggerName);
        }

        /// <summary>
        /// 记录追踪日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="args">格式化参数</param>
        public void Trace(string message, params object[] args)
        {
            _logger.Trace(message, args);
        }

        /// <summary>
        /// 记录调试日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="args">格式化参数</param>
        public void Debug(string message, params object[] args)
        {
            _logger.Debug(message, args);
        }

        /// <summary>
        /// 记录信息日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="args">格式化参数</param>
        public void Info(string message, params object[] args)
        {
            _logger.Info(message, args);
        }

        /// <summary>
        /// 记录警告日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="args">格式化参数</param>
        public void Warn(string message, params object[] args)
        {
            _logger.Warn(message, args);
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="args">格式化参数</param>
        public void Error(string message, params object[] args)
        {
            _logger.Error(message, args);
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="message">日志消息</param>
        /// <param name="args">格式化参数</param>
        public void Error(Exception ex, string message, params object[] args)
        {
            _logger.Error(ex, message, args);
        }

        /// <summary>
        /// 记录致命错误日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="args">格式化参数</param>
        public void Fatal(string message, params object[] args)
        {
            _logger.Fatal(message, args);
        }

        /// <summary>
        /// 记录致命错误日志
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="message">日志消息</param>
        /// <param name="args">格式化参数</param>
        public void Fatal(Exception ex, string message, params object[] args)
        {
            _logger.Fatal(ex, message, args);
        }
    }
} 