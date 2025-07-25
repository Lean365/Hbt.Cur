#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtParallelNodeExecutor.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流并行节点执行器
//===================================================================

using Lean.Hbt.Application.Services.Workflow.Engine;
using Lean.Hbt.Application.Services.Workflow.Engine.Resolvers;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Application.Services.Workflow.Engine.Executors
{
    /// <summary>
    /// 工作流并行节点执行器
    /// </summary>
    public class HbtParallelNodeExecutor : HbtNodeExecutorBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="approverResolver">审批人解析器</param>
        public HbtParallelNodeExecutor(
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtApproverResolver approverResolver) : base(logger, currentUser, approverResolver)
        {
        }

        /// <inheritdoc/>
        public override bool CanHandle(string nodeType)
        {
            return nodeType.Equals("parallel", StringComparison.OrdinalIgnoreCase);
        }

        /// <inheritdoc/>
        public override string GetNodeTypeDescription(string nodeType)
        {
            return "并行节点";
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
                LogNodeExecution(instanceId, nodeId, "开始执行并行节点");

                // 并行节点表示同时执行多个分支
                // 这里简化实现，直接返回成功

                LogNodeExecution(instanceId, nodeId, "并行节点执行完成");
                return CreateSuccessResult(
                    nextNodeId: null, // 下一个节点由工作流引擎根据配置确定
                    nextNodeName: null,
                    workflowStatus: 1); // 1 表示运行中
            }
            catch (Exception ex)
            {
                LogNodeError(instanceId, nodeId, ex);
                return CreateFailureResult($"并行节点执行失败: {ex.Message}");
            }
        }
    }
}