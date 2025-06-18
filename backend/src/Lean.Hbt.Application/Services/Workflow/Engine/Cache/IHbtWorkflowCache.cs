#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IWorkflowCache.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流缓存接口
//===================================================================

namespace Lean.Hbt.Application.Services.Workflow.Engine.Cache
{
    /// <summary>
    /// 工作流缓存接口
    /// </summary>
    public interface IHbtWorkflowCache
    {
        /// <summary>
        /// 获取工作流节点
        /// </summary>
        Task<HbtNode?> GetNodeAsync(long nodeId);

        /// <summary>
        /// 设置工作流节点缓存
        /// </summary>
        Task SetNodeAsync(HbtNode node, TimeSpan? expiry = null);

        /// <summary>
        /// 移除工作流节点缓存
        /// </summary>
        Task RemoveNodeAsync(long nodeId);

        /// <summary>
        /// 获取工作流定义
        /// </summary>
        Task<HbtDefinition?> GetDefinitionAsync(long definitionId);

        /// <summary>
        /// 设置工作流定义缓存
        /// </summary>
        Task SetDefinitionAsync(HbtDefinition definition, TimeSpan? expiry = null);

        /// <summary>
        /// 移除工作流定义缓存
        /// </summary>
        Task RemoveDefinitionAsync(long definitionId);

        /// <summary>
        /// 获取工作流实例
        /// </summary>
        Task<HbtInstance?> GetInstanceAsync(long instanceId);

        /// <summary>
        /// 设置工作流实例缓存
        /// </summary>
        Task SetInstanceAsync(HbtInstance instance, TimeSpan? expiry = null);

        /// <summary>
        /// 移除工作流实例缓存
        /// </summary>
        Task RemoveInstanceAsync(long instanceId);
    }
}