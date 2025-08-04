#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtNodeExecutorBase.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流节点执行器基类
//===================================================================

using Hbt.Application.Services.Workflow.Engine;
using Hbt.Application.Services.Workflow.Engine.Resolvers;
using Hbt.Domain.IServices;
using System.Text.Json;

namespace Hbt.Application.Services.Workflow.Engine.Executors
{
    /// <summary>
    /// 工作流节点执行器基类
    /// </summary>
    public abstract class HbtNodeExecutorBase : IHbtNodeExecutor
    {
        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly IHbtLogger _logger;
        
        /// <summary>
        /// 当前用户服务
        /// </summary>
        protected readonly IHbtCurrentUser _currentUser;
        
        /// <summary>
        /// 审批人解析器
        /// </summary>
        protected readonly IHbtApproverResolver _approverResolver;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="approverResolver">审批人解析器</param>
        protected HbtNodeExecutorBase(
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtApproverResolver approverResolver)
        {
            _logger = logger;
            _currentUser = currentUser;
            _approverResolver = approverResolver;
        }

        /// <inheritdoc/>
        public abstract Task<HbtApproveResult> ExecuteAsync(
            long instanceId,
            string nodeId,
            string nodeType,
            string? nodeConfig,
            Dictionary<string, object>? variables = null);

        /// <inheritdoc/>
        /// <summary>
        /// 是否可以处理该类型节点
        /// </summary>
        /// <param name="nodeType">节点类型</param>
        /// <returns>是否可以处理</returns>
        public abstract bool CanHandle(string nodeType);

        /// <inheritdoc/>
        /// <summary>
        /// 获取节点类型描述
        /// </summary>
        /// <param name="nodeType">节点类型</param>
        /// <returns>类型描述</returns>
        public abstract string GetNodeTypeDescription(string nodeType);

        #region 保护方法

        /// <summary>
        /// 解析节点配置
        /// </summary>
        /// <typeparam name="T">配置类型</typeparam>
        /// <param name="nodeConfig">节点配置JSON</param>
        /// <returns>配置对象</returns>
        protected T? ParseConfig<T>(string? nodeConfig) where T : class
        {
            if (string.IsNullOrEmpty(nodeConfig))
                return null;

            try
            {
                return JsonSerializer.Deserialize<T>(nodeConfig);
            }
            catch (Exception ex)
            {
                _logger.Error($"解析节点配置失败: {ex.Message}", ex);
                return null;
            }
        }

        /// <summary>
        /// 创建成功结果
        /// </summary>
        /// <param name="nextNodeId">下一个节点ID</param>
        /// <param name="nextNodeName">下一个节点名称</param>
        /// <param name="workflowStatus">工作流状态</param>
        /// <param name="outputVariables">输出变量</param>
        /// <returns>执行结果</returns>
        protected HbtApproveResult CreateSuccessResult(
            string? nextNodeId = null,
            string? nextNodeName = null,
            int workflowStatus = 1,
            Dictionary<string, object>? outputVariables = null)
        {
            return new HbtApproveResult
            {
                Success = true,
                NextNodeId = nextNodeId,
                NextNodeName = nextNodeName,
                WorkflowStatus = workflowStatus,
                OutputVariables = outputVariables
            };
        }

        /// <summary>
        /// 创建失败结果
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        /// <returns>执行结果</returns>
        protected HbtApproveResult CreateFailureResult(string errorMessage)
        {
            return new HbtApproveResult
            {
                Success = false,
                ErrorMessage = errorMessage
            };
        }

        /// <summary>
        /// 记录节点执行日志
        /// </summary>
        /// <param name="instanceId">实例ID</param>
        /// <param name="nodeId">节点ID</param>
        /// <param name="message">日志消息</param>
        protected void LogNodeExecution(long instanceId, string nodeId, string message)
        {
            _logger.Info($"节点执行 - 实例[{instanceId}] 节点[{nodeId}]: {message}");
        }

        /// <summary>
        /// 记录节点执行错误
        /// </summary>
        /// <param name="instanceId">实例ID</param>
        /// <param name="nodeId">节点ID</param>
        /// <param name="error">错误信息</param>
        protected void LogNodeError(long instanceId, string nodeId, Exception error)
        {
            _logger.Error($"节点执行错误 - 实例[{instanceId}] 节点[{nodeId}]: {error.Message}", error);
        }

        #endregion
    }
}