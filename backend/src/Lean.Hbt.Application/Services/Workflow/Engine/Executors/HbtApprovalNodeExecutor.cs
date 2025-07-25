#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtApprovalNodeExecutor.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流审批节点执行器
//===================================================================

using Lean.Hbt.Application.Services.Workflow.Engine;
using Lean.Hbt.Application.Services.Workflow.Engine.Resolvers;
using Lean.Hbt.Domain.IServices;
using System.Text.Json;

namespace Lean.Hbt.Application.Services.Workflow.Engine.Executors
{
    /// <summary>
    /// 工作流审批节点执行器
    /// </summary>
    public class HbtApprovalNodeExecutor : HbtNodeExecutorBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="approverResolver">审批人解析器</param>
        public HbtApprovalNodeExecutor(
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtApproverResolver approverResolver) : base(logger, currentUser, approverResolver)
        {
        }

        /// <inheritdoc/>
        public override bool CanHandle(string nodeType)
        {
            return nodeType.Equals("approval", StringComparison.OrdinalIgnoreCase);
        }

        /// <inheritdoc/>
        public override string GetNodeTypeDescription(string nodeType)
        {
            return "审批节点";
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
                LogNodeExecution(instanceId, nodeId, "开始执行审批节点");

                // 解析节点配置
                var config = ParseConfig<ApprovalNodeConfig>(nodeConfig);
                if (config == null)
                {
                    var error = "审批节点配置无效";
                    LogNodeError(instanceId, nodeId, new InvalidOperationException(error));
                    return CreateFailureResult(error);
                }

                // 解析审批人
                var approvers = await _approverResolver.ResolveApproversAsync(
                    instanceId, nodeId, config.ApproverType, config.ApproverConfig, variables);

                if (!approvers.Any())
                {
                    var error = "未找到有效的审批人";
                    LogNodeError(instanceId, nodeId, new InvalidOperationException(error));
                    return CreateFailureResult(error);
                }

                LogNodeExecution(instanceId, nodeId, $"成功解析审批人，数量：{approvers.Count}");

                // 审批节点需要等待审批，所以返回等待状态
                LogNodeExecution(instanceId, nodeId, "审批节点执行完成，等待审批");
                return CreateSuccessResult(
                    nextNodeId: null, // 审批节点不自动流转到下一个节点
                    nextNodeName: null,
                    workflowStatus: 1); // 1 表示运行中
            }
            catch (Exception ex)
            {
                LogNodeError(instanceId, nodeId, ex);
                return CreateFailureResult($"审批节点执行失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 审批节点配置
        /// </summary>
        private class ApprovalNodeConfig
        {
            /// <summary>
            /// 审批人类型
            /// </summary>
            public int ApproverType { get; set; }

            /// <summary>
            /// 审批人配置
            /// </summary>
            public string? ApproverConfig { get; set; }

            /// <summary>
            /// 是否需要所有审批人同意
            /// </summary>
            public bool RequireAllApproval { get; set; } = true;
        }
    }
}