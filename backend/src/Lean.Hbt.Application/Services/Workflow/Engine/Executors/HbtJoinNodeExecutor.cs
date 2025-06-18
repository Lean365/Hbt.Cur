#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : JoinNodeExecutor.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 汇聚节点执行器
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
    /// 汇聚节点执行器
    /// </summary>
    public class HbtJoinNodeExecutor : HbtNodeExecutorBase
    {
        private readonly IHbtRepository<HbtTransition> _transitionRepository;
        private readonly IHbtRepository<HbtParallelBranch> _parallelBranchRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtJoinNodeExecutor(
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
        protected override int NodeType => 4; // 4 表示汇合节点

        /// <summary>
        /// 执行节点
        /// </summary>
        protected override async Task<HbtNodeResult> ExecuteInternalAsync(
            HbtInstance instance,
            HbtNode node,
            Dictionary<string, object>? variables = null)
        {
            // 获取所有进入该节点的转换
            var incomingTransitions = await _transitionRepository.GetListAsync(x => x.TargetNodeId == node.Id);
            if (!incomingTransitions.Any())
            {
                return CreateFailureResult("汇聚节点没有配置进入转换");
            }

            // 获取对应的并行节点
            var parallelNodeId = incomingTransitions.First().SourceNodeId;

            // 获取并行分支状态
            var branches = await _parallelBranchRepository.GetListAsync(x =>
                x.InstanceId == instance.Id &&
                x.ParallelNodeId == parallelNodeId);

            // 检查所有分支是否都已完成
            if (branches.Any(x => x.IsCompleted == 0))
            {
                return CreateFailureResult("存在未完成的并行分支");
            }

            // 所有分支都已完成，可以继续执行
            return CreateSuccessResult();
        }
    }
} 