#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtNodeExecutor.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流节点执行器接口
//===================================================================

using Lean.Hbt.Domain.Entities.Workflow;

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
        /// <param name="instance">工作流实例</param>
        /// <param name="node">当前节点</param>
        /// <param name="variables">节点变量</param>
        /// <returns>节点执行结果</returns>
        Task<HbtNodeResult> ExecuteAsync(
            HbtInstance instance,
            HbtNode node,
            Dictionary<string, object>? variables = null);

        /// <summary>
        /// 是否可以处理该类型节点
        /// </summary>
        /// <param name="nodeType">节点类型</param>
        /// <returns>是否可以处理</returns>
        bool CanHandle(int nodeType);
    }
}