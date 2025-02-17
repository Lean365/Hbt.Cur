//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtAuditsLog.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 16:30
// 版本号 : V.0.0.1
// 描述    : 审计日志接口
//===================================================================

namespace Lean.Hbt.Domain.IServices
{
    /// <summary>
    /// 审计日志接口
    /// </summary>
    public interface IHbtAuditsLog
    {
        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="module">模块</param>
        /// <param name="operation">操作</param>
        /// <param name="method">方法</param>
        /// <param name="parameters">参数</param>
        /// <param name="result">结果</param>
        /// <param name="elapsed">耗时(ms)</param>
        /// <returns>任务</returns>
        Task LogOperationAsync(long userId, string userName, string module, string operation, string method, string parameters, string result, long elapsed);

        /// <summary>
        /// 记录登录日志
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="ipAddress">IP地址</param>
        /// <param name="userAgent">用户代理</param>
        /// <param name="result">结果</param>
        /// <param name="message">消息</param>
        /// <returns>任务</returns>
        Task LogLoginAsync(long userId, string userName, string ipAddress, string userAgent, bool result, string message);

        /// <summary>
        /// 记录异常日志
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="method">方法</param>
        /// <param name="parameters">参数</param>
        /// <param name="exception">异常</param>
        /// <returns>任务</returns>
        Task LogExceptionAsync(long userId, string userName, string method, string parameters, Exception exception);
    }
}