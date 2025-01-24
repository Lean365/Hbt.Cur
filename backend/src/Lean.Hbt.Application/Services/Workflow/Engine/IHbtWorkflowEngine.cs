#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtWorkflowEngine.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流引擎接口
//===================================================================

using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Hbt.Application.Dtos.Workflow;

namespace Lean.Hbt.Application.Services.Workflow.Engine
{
    /// <summary>
    /// 工作流引擎接口
    /// </summary>
    public interface IHbtWorkflowEngine
    {
        /// <summary>
        /// 启动工作流实例
        /// </summary>
        /// <param name="definitionId">工作流定义ID</param>
        /// <param name="title">工作流标题</param>
        /// <param name="initiatorId">发起人ID</param>
        /// <param name="formData">表单数据</param>
        /// <param name="variables">工作流变量</param>
        /// <returns>工作流实例ID</returns>
        Task<long> StartAsync(long definitionId, string title, long initiatorId, string formData, Dictionary<string, object>? variables = null);

        /// <summary>
        /// 暂停工作流实例
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        Task SuspendAsync(long instanceId);

        /// <summary>
        /// 恢复工作流实例
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        Task ResumeAsync(long instanceId);

        /// <summary>
        /// 终止工作流实例
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <param name="reason">终止原因</param>
        Task TerminateAsync(long instanceId, string reason);

        /// <summary>
        /// 执行工作流节点
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <param name="nodeId">节点ID</param>
        /// <param name="variables">节点变量</param>
        /// <returns>节点执行结果</returns>
        Task<HbtWorkflowNodeResult> ExecuteNodeAsync(long instanceId, long nodeId, Dictionary<string, object>? variables = null);

        /// <summary>
        /// 执行工作流转换
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <param name="transitionId">转换ID</param>
        /// <param name="variables">转换变量</param>
        /// <returns>转换执行结果</returns>
        Task<HbtWorkflowTransitionResult> ExecuteTransitionAsync(long instanceId, long transitionId, Dictionary<string, object>? variables = null);

        /// <summary>
        /// 获取工作流实例状态
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>工作流实例状态</returns>
        Task<HbtWorkflowInstanceStatusDto> GetStatusAsync(long instanceId);

        /// <summary>
        /// 获取工作流实例可用转换列表
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>可用转换列表</returns>
        Task<List<HbtWorkflowTransitionDto>> GetAvailableTransitionsAsync(long instanceId);

        /// <summary>
        /// 获取工作流实例变量
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <param name="nodeId">节点ID(可选)</param>
        /// <returns>工作流变量字典</returns>
        Task<Dictionary<string, object>> GetVariablesAsync(long instanceId, long? nodeId = null);

        /// <summary>
        /// 设置工作流实例变量
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <param name="variables">变量字典</param>
        /// <param name="nodeId">节点ID(可选)</param>
        Task SetVariablesAsync(long instanceId, Dictionary<string, object> variables, long? nodeId = null);
    }
} 
