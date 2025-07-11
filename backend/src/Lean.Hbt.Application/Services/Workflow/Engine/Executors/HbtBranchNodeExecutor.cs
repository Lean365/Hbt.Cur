#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : BranchNodeExecutor.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 分支节点执行器
//===================================================================

using System.Text.Json;
using Lean.Hbt.Application.Services.Workflow.Engine.Expressions;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.IServices.Extensions;
using Lean.Hbt.Domain.Models.Workflow;
using Lean.Hbt.Domain.Repositories;

namespace Lean.Hbt.Application.Services.Workflow.Engine.Executors
{
    /// <summary>
    /// 分支节点执行器
    /// </summary>
    public class HbtBranchNodeExecutor : HbtNodeExecutorBase
    {
        private readonly IHbtRepository<HbtTransition> _transitionRepository;
        private readonly IHbtExpressionEngine _expressionEngine;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtBranchNodeExecutor(
            IHbtRepository<HbtTransition> transitionRepository,
            IHbtExpressionEngine expressionEngine,
            IHbtLogger logger,
            IHbtRepository<HbtNodeTemplate> nodeTemplateRepository) : base(logger, nodeTemplateRepository)
        {
            _transitionRepository = transitionRepository;
            _expressionEngine = expressionEngine;
        }

        /// <summary>
        /// 节点类型
        /// </summary>
        protected override int NodeType => 3; // 3 表示分支节点

        /// <summary>
        /// 执行节点
        /// </summary>
        protected override async Task<HbtNodeResult> ExecuteInternalAsync(
            HbtInstance instance,
            HbtNode node,
            Dictionary<string, object>? variables = null)
        {
            // 获取所有可用的转换
            var transitions = await _transitionRepository.GetListAsync(x => x.SourceActivityId == node.NodeTemplateId);
            if (!transitions.Any())
            {
                return CreateFailureResult("分支节点没有配置转换");
            }

            // 解析节点配置
            var nodeTemplate = await _nodeTemplateRepository.GetByIdAsync(node.NodeTemplateId);
            var config = JsonSerializer.Deserialize<HbtNodeConfig>(nodeTemplate?.NodeConfig ?? "{}");
            if (config == null)
            {
                return CreateFailureResult("分支节点配置无效");
            }

            // 获取满足条件的转换
            var availableTransitions = new List<long>();
            foreach (var transition in transitions)
            {
                if (string.IsNullOrEmpty(transition.Condition))
                {
                    // 如果没有条件,作为默认分支
                    availableTransitions.Add(transition.Id);
                    continue;
                }

                // 执行条件表达式
                var conditionMet = await _expressionEngine.EvaluateAsync(transition.Condition, variables);
                if (conditionMet)
                {
                    availableTransitions.Add(transition.Id);

                    // 如果不是多分支模式,找到第一个满足条件的分支后就退出
                    if (!config.AllowMultipleBranches)
                    {
                        break;
                    }
                }
            }

            if (!availableTransitions.Any())
            {
                return CreateFailureResult("没有满足条件的分支");
            }

            // 返回成功结果,包含所有可用的转换ID
            var outputVariables = new Dictionary<string, object>
            {
                { "AvailableTransitions", availableTransitions }
            };

            return CreateSuccessResult(outputVariables);
        }
    }
}