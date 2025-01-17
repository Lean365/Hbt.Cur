//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtNLogger.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-16 10:00
// 版本号 : V0.0.1
// 描述    : NLog日志实现
//===================================================================

using NLog;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Infrastructure.Logging
{
    /// <summary>
    /// NLog日志实现
    /// </summary>
    public class HbtNLogger : IHbtLogger
    {
        private readonly ILogger _logger;

        public HbtNLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Debug(string message, params object[] args)
        {
            _logger.Debug(message, args);
        }

        public void Error(string message, params object[] args)
        {
            _logger.Error(message, args);
        }

        public void Error(Exception ex, string message, params object[] args)
        {
            _logger.Error(ex, message, args);
        }

        public void Fatal(string message, params object[] args)
        {
            _logger.Fatal(message, args);
        }

        public void Fatal(Exception ex, string message, params object[] args)
        {
            _logger.Fatal(ex, message, args);
        }

        public void Information(string message, params object[] args)
        {
            _logger.Info(message, args);
        }

        public void Warning(string message, params object[] args)
        {
            _logger.Warn(message, args);
        }
    }
} 