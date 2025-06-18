#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : ParallelNodeExecutor.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 并行节点执行器
//===================================================================

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.IServices.Extensions;

namespace Lean.Hbt.Application.Services.Workflow.Engine.Executors
{
    /// <summary>
    /// 并行节点执行器
    /// </summary>
    public class HbtParallelNodeExecutor : HbtNodeExecutorBase
    {
        private readonly IHbtRepository<HbtTransition> _transitionRepository;
        private readonly IHbtRepository<HbtParallelBranch> _parallelBranchRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtParallelNodeExecutor(
            IHbtRepository<HbtTransition> transitionRepository,
            IHbtRepository<HbtParallelBranch> parallelBranchRepository,
            IHbtLogger logger) : base(logger)
        {
            _transitionRepository = transitionRepository;
            _parallelBranchRepository = parallelBranchRepository;
        }

        /// <summary>
        /// 节点类型
        /// </summary>
        protected override int NodeType => 2; // 并行节点类型值为2

        /// <summary>
        /// 执行节点
        /// </summary>
        protected override async Task<HbtNodeResult> ExecuteInternalAsync(
            HbtInstance instance,
            HbtNode node,
            Dictionary<string, object>? variables = null)
        {
            // 获取所有并行分支
            var transitions = await _transitionRepository.GetListAsync(x => x.SourceNodeId == node.Id);
            if (!transitions.Any())
            {
                return CreateFailureResult("并行节点没有配置分支转换");
            }

            // 创建并行分支状态记录
            foreach (var transition in transitions)
            {
                var branch = new HbtParallelBranch
                {
                    InstanceId = instance.Id,
                    ParallelNodeId = node.Id,
                    BranchTransitionId = transition.Id,
                    IsCompleted = 0
                };

                await _parallelBranchRepository.CreateAsync(branch);
            }

            // 返回成功结果，包含所有并行分支的转换ID
            var outputVariables = new Dictionary<string, object>
            {
                { "ParallelTransitions", transitions.Select(x => x.Id).ToList() }
            };

            return CreateSuccessResult(outputVariables);
        }
    }
}