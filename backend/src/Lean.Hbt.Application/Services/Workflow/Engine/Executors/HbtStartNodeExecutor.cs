#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtStartNodeExecutor.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流开始节点执行器
//===================================================================

using Lean.Hbt.Application.Services.Workflow.Engine;
using Lean.Hbt.Application.Services.Workflow.Engine.Resolvers;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Application.Services.Workflow.Engine.Executors
{
    /// <summary>
    /// 工作流开始节点执行器
    /// </summary>
    public class HbtStartNodeExecutor : HbtNodeExecutorBase
    {
        public HbtStartNodeExecutor(
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtApproverResolver approverResolver) : base(logger, currentUser, approverResolver)
        {
        }

        /// <inheritdoc/>
        public override bool CanHandle(string nodeType)
        {
            return nodeType.Equals("start", StringComparison.OrdinalIgnoreCase);
        }

        /// <inheritdoc/>
        public override string GetNodeTypeDescription(string nodeType)
        {
            return "开始节点";
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
                LogNodeExecution(instanceId, nodeId, "开始执行开始节点");

                // 开始节点通常不需要特殊处理，直接返回成功
                // 实际的下一个节点由工作流引擎根据配置确定

                LogNodeExecution(instanceId, nodeId, "开始节点执行完成");
                return CreateSuccessResult();
            }
            catch (Exception ex)
            {
                LogNodeError(instanceId, nodeId, ex);
                return CreateFailureResult($"开始节点执行失败: {ex.Message}");
            }
        }
    }
} 