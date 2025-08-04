#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IWorkflowCache.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V0.0.1
// 描述    : 工作流缓存接口
//===================================================================

using Hbt.Cur.Domain.Entities.Workflow;

namespace Hbt.Cur.Application.Services.Workflow.Engine.Cache
{
    /// <summary>
    /// 工作流缓存接口
    /// </summary>
    public interface IHbtWorkflowCache
    {
        /// <summary>
        /// 获取工作流节点
        /// </summary>
        Task<HbtScheme?> GetNodeAsync(long nodeId);

        /// <summary>
        /// 设置工作流节点缓存
        /// </summary>
        Task SetNodeAsync(HbtScheme node, TimeSpan? expiry = null);

        /// <summary>
        /// 移除工作流节点缓存
        /// </summary>
        Task RemoveNodeAsync(long nodeId);

        /// <summary>
        /// 获取工作流定义
        /// </summary>
        Task<HbtScheme?> GetDefinitionAsync(long definitionId);

        /// <summary>
        /// 设置工作流定义缓存
        /// </summary>
        Task SetDefinitionAsync(HbtScheme definition, TimeSpan? expiry = null);

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