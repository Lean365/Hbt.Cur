#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : WorkflowNodeExecutorBase.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流节点执行器基类
//===================================================================

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Application.Services.Workflow.Engine.Executors
{
    /// <summary>
    /// 工作流节点执行器基类
    /// </summary>
    public abstract class WorkflowNodeExecutorBase : IWorkflowNodeExecutor
    {
        protected readonly IHbtLogger _logger;

        protected WorkflowNodeExecutorBase(IHbtLogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 节点类型
        /// </summary>
        protected abstract HbtWorkflowNodeType NodeType { get; }

        /// <summary>
        /// 执行节点内部逻辑
        /// </summary>
        protected abstract Task<HbtWorkflowNodeResult> ExecuteInternalAsync(
            HbtWorkflowInstance instance,
            HbtWorkflowNode node,
            Dictionary<string, object>? variables = null);

        /// <summary>
        /// 执行节点
        /// </summary>
        public async Task<HbtWorkflowNodeResult> ExecuteAsync(
            HbtWorkflowInstance instance,
            HbtWorkflowNode node,
            Dictionary<string, object>? variables = null)
        {
            try
            {
                _logger.Info($"开始执行节点: 实例ID={instance.Id}, 节点ID={node.Id}, 节点类型={node.NodeType}");
                
                // 验证节点类型
                if (!CanHandle(node.NodeType))
                {
                    var error = $"节点类型不匹配: 期望={NodeType}, 实际={node.NodeType}";
                    _logger.Error(error);
                    return CreateFailureResult(error);
                }

                // 执行具体节点逻辑
                var result = await ExecuteInternalAsync(instance, node, variables);
                
                // 记录执行结果
                if (result.Success)
                {
                    _logger.Info($"节点执行成功: 实例ID={instance.Id}, 节点ID={node.Id}");
                }
                else
                {
                    _logger.Error($"节点执行失败: 实例ID={instance.Id}, 节点ID={node.Id}, 错误={result.ErrorMessage}");
                }

                return result;
            }
            catch (Exception ex)
            {
                var error = $"节点执行异常: {ex.Message}";
                _logger.Error(error, ex);
                return CreateFailureResult(error);
            }
        }

        /// <summary>
        /// 是否可以处理该类型节点
        /// </summary>
        public bool CanHandle(HbtWorkflowNodeType nodeType)
        {
            return nodeType == NodeType;
        }

        /// <summary>
        /// 创建成功结果
        /// </summary>
        protected HbtWorkflowNodeResult CreateSuccessResult(Dictionary<string, object>? outputVariables = null)
        {
            return new HbtWorkflowNodeResult
            {
                Success = true,
                OutputVariables = outputVariables
            };
        }

        /// <summary>
        /// 创建失败结果
        /// </summary>
        protected HbtWorkflowNodeResult CreateFailureResult(string errorMessage)
        {
            return new HbtWorkflowNodeResult
            {
                Success = false,
                ErrorMessage = errorMessage
            };
        }
    }
} 