//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : IHbtLogger.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-16 10:00
// 版本号 : V0.0.1
// 描述    : 日志服务接口
//===================================================================

namespace Lean.Hbt.Infrastructure.Logging
{
    /// <summary>
    /// 日志服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    public interface IHbtLogger
    {
        /// <summary>
        /// 记录追踪日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="args">格式化参数</param>
        void Trace(string message, params object[] args);

        /// <summary>
        /// 记录调试日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="args">格式化参数</param>
        void Debug(string message, params object[] args);

        /// <summary>
        /// 记录信息日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="args">格式化参数</param>
        void Info(string message, params object[] args);

        /// <summary>
        /// 记录警告日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="args">格式化参数</param>
        void Warn(string message, params object[] args);

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="args">格式化参数</param>
        void Error(string message, params object[] args);

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="message">日志消息</param>
        /// <param name="args">格式化参数</param>
        void Error(Exception ex, string message, params object[] args);

        /// <summary>
        /// 记录致命错误日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="args">格式化参数</param>
        void Fatal(string message, params object[] args);

        /// <summary>
        /// 记录致命错误日志
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="message">日志消息</param>
        /// <param name="args">格式化参数</param>
        void Fatal(Exception ex, string message, params object[] args);
    }
} 