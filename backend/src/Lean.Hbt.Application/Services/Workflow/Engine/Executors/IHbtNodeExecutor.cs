#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtNodeExecutor.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流节点执行器接口
//===================================================================

using Lean.Hbt.Application.Services.Workflow.Engine;

namespace Lean.Hbt.Application.Services.Workflow.Engine.Executors
{
    /// <summary>
    /// 工作流节点执行器接口
    /// </summary>
    public interface IHbtNodeExecutor
    {
        /// <summary>
        /// 执行节点
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <param name="nodeId">节点ID</param>
        /// <param name="nodeType">节点类型</param>
        /// <param name="nodeConfig">节点配置(JSON格式)</param>
        /// <param name="variables">工作流变量</param>
        /// <returns>节点执行结果</returns>
        Task<HbtApproveResult> ExecuteAsync(
            long instanceId,
            string nodeId,
            string nodeType,
            string? nodeConfig,
            Dictionary<string, object>? variables = null);

        /// <summary>
        /// 是否可以处理该类型节点
        /// </summary>
        /// <param name="nodeType">节点类型</param>
        /// <returns>是否可以处理</returns>
        bool CanHandle(string nodeType);

        /// <summary>
        /// 获取节点类型描述
        /// </summary>
        /// <param name="nodeType">节点类型</param>
        /// <returns>类型描述</returns>
        string GetNodeTypeDescription(string nodeType);
    }
}