#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtEndNodeExecutor.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流结束节点执行器
//===================================================================

using Hbt.Application.Services.Workflow.Engine;
using Hbt.Application.Services.Workflow.Engine.Resolvers;
using Hbt.Domain.IServices;

namespace Hbt.Application.Services.Workflow.Engine.Executors
{
    /// <summary>
    /// 工作流结束节点执行器
    /// </summary>
    public class HbtEndNodeExecutor : HbtNodeExecutorBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="approverResolver">审批人解析器</param>
        public HbtEndNodeExecutor(
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtApproverResolver approverResolver) : base(logger, currentUser, approverResolver)
        {
        }

        /// <inheritdoc/>
        public override bool CanHandle(string nodeType)
        {
            return nodeType.Equals("end", StringComparison.OrdinalIgnoreCase);
        }

        /// <inheritdoc/>
        public override string GetNodeTypeDescription(string nodeType)
        {
            return "结束节点";
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
                LogNodeExecution(instanceId, nodeId, "开始执行结束节点");

                // 结束节点表示工作流完成
                // 工作流状态将变为已完成(2)

                LogNodeExecution(instanceId, nodeId, "结束节点执行完成，工作流结束");
                return CreateSuccessResult(
                    nextNodeId: null,
                    nextNodeName: null,
                    workflowStatus: 2); // 2 表示已完成
            }
            catch (Exception ex)
            {
                LogNodeError(instanceId, nodeId, ex);
                return CreateFailureResult($"结束节点执行失败: {ex.Message}");
            }
        }
    }
}