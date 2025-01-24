#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IWorkflowCache.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流缓存接口
//===================================================================

using System;
using System.Threading.Tasks;
using Lean.Hbt.Domain.Entities.Workflow;

namespace Lean.Hbt.Application.Services.Workflow.Engine.Cache
{
    /// <summary>
    /// 工作流缓存接口
    /// </summary>
    public interface IWorkflowCache
    {
        /// <summary>
        /// 获取工作流节点
        /// </summary>
        Task<HbtWorkflowNode?> GetNodeAsync(long nodeId);

        /// <summary>
        /// 设置工作流节点缓存
        /// </summary>
        Task SetNodeAsync(HbtWorkflowNode node, TimeSpan? expiry = null);

        /// <summary>
        /// 移除工作流节点缓存
        /// </summary>
        Task RemoveNodeAsync(long nodeId);

        /// <summary>
        /// 获取工作流定义
        /// </summary>
        Task<HbtWorkflowDefinition?> GetDefinitionAsync(long definitionId);

        /// <summary>
        /// 设置工作流定义缓存
        /// </summary>
        Task SetDefinitionAsync(HbtWorkflowDefinition definition, TimeSpan? expiry = null);

        /// <summary>
        /// 移除工作流定义缓存
        /// </summary>
        Task RemoveDefinitionAsync(long definitionId);

        /// <summary>
        /// 获取工作流实例
        /// </summary>
        Task<HbtWorkflowInstance?> GetInstanceAsync(long instanceId);

        /// <summary>
        /// 设置工作流实例缓存
        /// </summary>
        Task SetInstanceAsync(HbtWorkflowInstance instance, TimeSpan? expiry = null);

        /// <summary>
        /// 移除工作流实例缓存
        /// </summary>
        Task RemoveInstanceAsync(long instanceId);
    }
} 