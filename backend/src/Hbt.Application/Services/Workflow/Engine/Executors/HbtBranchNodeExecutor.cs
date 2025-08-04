#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtBranchNodeExecutor.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流分支节点执行器
//===================================================================

using Hbt.Application.Services.Workflow.Engine;
using Hbt.Application.Services.Workflow.Engine.Expressions;
using Hbt.Application.Services.Workflow.Engine.Resolvers;
using Hbt.Domain.IServices;
using System.Text.Json;

namespace Hbt.Application.Services.Workflow.Engine.Executors
{
    /// <summary>
    /// 工作流分支节点执行器
    /// </summary>
    public class HbtBranchNodeExecutor : HbtNodeExecutorBase
    {
        /// <summary>
        /// 表达式引擎
        /// </summary>
        private readonly IHbtExpressionEngine _expressionEngine;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="approverResolver">审批人解析器</param>
        /// <param name="expressionEngine">表达式引擎</param>
        public HbtBranchNodeExecutor(
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtApproverResolver approverResolver,
            IHbtExpressionEngine expressionEngine) : base(logger, currentUser, approverResolver)
        {
            _expressionEngine = expressionEngine ?? throw new ArgumentNullException(nameof(expressionEngine));
        }

        /// <inheritdoc/>
        public override bool CanHandle(string nodeType)
        {
            return nodeType.Equals("branch", StringComparison.OrdinalIgnoreCase);
        }

        /// <inheritdoc/>
        public override string GetNodeTypeDescription(string nodeType)
        {
            return "分支节点";
        }

        /// <inheritdoc/>
        public override async Task<HbtApproveResult> ExecuteAsync(
            long instanceId,
            string nodeId,
            string nodeType,
            string? nodeConfig,
            Dictionary<string, object>? variables = null)
        {
            try
            {
                LogNodeExecution(instanceId, nodeId, "开始执行分支节点");

                // 解析节点配置
                var config = ParseConfig<BranchNodeConfig>(nodeConfig);
                if (config == null)
                {
                    var error = "分支节点配置无效";
                    LogNodeError(instanceId, nodeId, new InvalidOperationException(error));
                    return CreateFailureResult(error);
                }

                // 执行条件表达式
                if (!string.IsNullOrEmpty(config.Condition))
                {
                    var conditionMet = await _expressionEngine.EvaluateAsync(config.Condition, variables);
                    if (!conditionMet)
                    {
                        var error = "分支条件不满足";
                        LogNodeExecution(instanceId, nodeId, error);
                        return CreateFailureResult(error);
                    }
                }

                // 分支节点执行成功，返回下一个节点信息
                LogNodeExecution(instanceId, nodeId, "分支节点执行完成");
                return CreateSuccessResult(
                    nextNodeId: config.NextNodeId,
                    nextNodeName: config.NextNodeName,
                    workflowStatus: 1); // 1 表示运行中
            }
            catch (Exception ex)
            {
                LogNodeError(instanceId, nodeId, ex);
                return CreateFailureResult($"分支节点执行失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 分支节点配置
        /// </summary>
        private class BranchNodeConfig
        {
            /// <summary>
            /// 分支条件
            /// </summary>
            public string? Condition { get; set; }

            /// <summary>
            /// 下一个节点ID
            /// </summary>
            public string? NextNodeId { get; set; }

            /// <summary>
            /// 下一个节点名称
            /// </summary>
            public string? NextNodeName { get; set; }

            /// <summary>
            /// 是否允许多分支
            /// </summary>
            public bool AllowMultipleBranches { get; set; } = false;
        }
    }
}