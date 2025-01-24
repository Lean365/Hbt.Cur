#nullable enable

//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : IWorkflowApproverResolver.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流审批人解析器接口
//===================================================================

using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.Models.Workflow;
namespace Lean.Hbt.Application.Services.Workflow.Engine.Resolvers
{
    /// <summary>
    /// 工作流审批人解析器接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public interface IWorkflowApproverResolver
    {
        /// <summary>
        /// 解析审批人
        /// </summary>
        /// <param name="instance">工作流实例</param>
        /// <param name="node">当前节点</param>
        /// <param name="nodeConfig">节点配置</param>
        /// <returns>审批人ID列表</returns>
        Task<List<long>> ResolveApproversAsync(
            HbtWorkflowInstance instance,
            HbtWorkflowNode node,
            HbtWorkflowNodeConfig nodeConfig);
    }
} 