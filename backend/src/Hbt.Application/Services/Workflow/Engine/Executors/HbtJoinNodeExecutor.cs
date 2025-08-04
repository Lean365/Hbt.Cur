#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtJoinNodeExecutor.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流汇聚节点执行器
//===================================================================

using Hbt.Application.Services.Workflow.Engine;
using Hbt.Application.Services.Workflow.Engine.Resolvers;
using Hbt.Domain.IServices;

namespace Hbt.Application.Services.Workflow.Engine.Executors
{
    /// <summary>
    /// 工作流汇聚节点执行器
    /// </summary>
    public class HbtJoinNodeExecutor : HbtNodeExecutorBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="approverResolver">审批人解析器</param>
        public HbtJoinNodeExecutor(
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtApproverResolver approverResolver) : base(logger, currentUser, approverResolver)
        {
        }

        /// <inheritdoc/>
        public override bool CanHandle(string nodeType)
        {
            return nodeType.Equals("join", StringComparison.OrdinalIgnoreCase);
        }

        /// <inheritdoc/>
        public override string GetNodeTypeDescription(string nodeType)
        {
            return "汇聚节点";
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
                LogNodeExecution(instanceId, nodeId, "开始执行汇聚节点");

                // 汇聚节点表示等待所有并行分支完成后继续执行
                // 这里简化实现，直接返回成功

                LogNodeExecution(instanceId, nodeId, "汇聚节点执行完成");
                return CreateSuccessResult(
                    nextNodeId: null, // 下一个节点由工作流引擎根据配置确定
                    nextNodeName: null,
                    workflowStatus: 1); // 1 表示运行中
            }
            catch (Exception ex)
            {
                LogNodeError(instanceId, nodeId, ex);
                return CreateFailureResult($"汇聚节点执行失败: {ex.Message}");
            }
        }
    }
} 