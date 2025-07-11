#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : WorkflowNodeExecutorBase.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流节点执行器基类
//===================================================================

using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.IServices.Extensions;

namespace Lean.Hbt.Application.Services.Workflow.Engine.Executors
{
    /// <summary>
    /// 工作流节点执行器基类
    /// </summary>
    public abstract class HbtNodeExecutorBase : IHbtNodeExecutor
    {
        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly IHbtLogger _logger;

        /// <summary>
        /// 节点模板仓储
        /// </summary>
        protected readonly IHbtRepository<HbtNodeTemplate> _nodeTemplateRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="nodeTemplateRepository"></param>
        protected HbtNodeExecutorBase(IHbtLogger logger, IHbtRepository<HbtNodeTemplate> nodeTemplateRepository)
        {
            _logger = logger;
            _nodeTemplateRepository = nodeTemplateRepository;
        }

        /// <summary>
        /// 节点类型
        /// </summary>
        protected abstract int NodeType { get; }

        /// <summary>
        /// 执行节点内部逻辑
        /// </summary>
        protected abstract Task<HbtNodeResult> ExecuteInternalAsync(
            HbtInstance instance,
            HbtNode node,
            Dictionary<string, object>? variables = null);

        /// <summary>
        /// 执行节点
        /// </summary>
        public async Task<HbtNodeResult> ExecuteAsync(
            HbtInstance instance,
            HbtNode node,
            Dictionary<string, object>? variables = null)
        {
            try
            {
                // 获取节点类型
                var nodeTemplate = await _nodeTemplateRepository.GetByIdAsync(node.NodeTemplateId);
                var nodeType = nodeTemplate?.NodeType ?? 0;
                
                _logger.Info($"开始执行节点: 实例ID={instance.Id}, 节点ID={node.Id}, 节点类型={nodeType}");

                // 验证节点类型
                if (!CanHandle(nodeType))
                {
                    var error = $"节点类型不匹配: 期望={NodeType}, 实际={nodeType}";
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
        public bool CanHandle(int nodeType)
        {
            return nodeType == NodeType;
        }

        /// <summary>
        /// 创建成功结果
        /// </summary>
        protected HbtNodeResult CreateSuccessResult(Dictionary<string, object>? outputVariables = null)
        {
            return new HbtNodeResult
            {
                Success = true,
                OutputVariables = outputVariables
            };
        }

        /// <summary>
        /// 创建失败结果
        /// </summary>
        protected HbtNodeResult CreateFailureResult(string errorMessage)
        {
            return new HbtNodeResult
            {
                Success = false,
                ErrorMessage = errorMessage
            };
        }
    }
}