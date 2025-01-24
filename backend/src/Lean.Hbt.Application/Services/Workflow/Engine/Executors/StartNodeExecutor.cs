#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : StartNodeExecutor.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 开始节点执行器
//===================================================================

using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Application.Services.Workflow.Engine.Executors
{
    /// <summary>
    /// 开始节点执行器
    /// </summary>
    public class StartNodeExecutor : WorkflowNodeExecutorBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public StartNodeExecutor(IHbtLogger logger) : base(logger)
        {
        }

        /// <summary>
        /// 节点类型
        /// </summary>
        protected override HbtWorkflowNodeType NodeType => HbtWorkflowNodeType.Start;

        /// <summary>
        /// 执行节点
        /// </summary>
        protected override Task<HbtWorkflowNodeResult> ExecuteInternalAsync(
            HbtWorkflowInstance instance,
            HbtWorkflowNode node,
            Dictionary<string, object>? variables = null)
        {
            // 开始节点不需要特殊处理，直接返回成功
            return Task.FromResult(CreateSuccessResult());
        }
    }
} 