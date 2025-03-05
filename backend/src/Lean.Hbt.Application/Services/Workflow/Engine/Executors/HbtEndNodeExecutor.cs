#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : EndNodeExecutor.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 结束节点执行器
//===================================================================

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Application.Services.Workflow.Engine.Executors
{
    /// <summary>
    /// 结束节点执行器
    /// </summary>
    public class HbtEndNodeExecutor : HbtWorkflowNodeExecutorBase
    {
        private readonly IHbtRepository<HbtWorkflowInstance> _instanceRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtEndNodeExecutor(
            IHbtRepository<HbtWorkflowInstance> instanceRepository,
            IHbtLogger logger) : base(logger)
        {
            _instanceRepository = instanceRepository;
        }

        /// <summary>
        /// 节点类型
        /// </summary>
        protected override int NodeType => 5; // 5 表示结束节点

        /// <summary>
        /// 执行结束节点
        /// </summary>
        protected override async Task<HbtWorkflowNodeResult> ExecuteInternalAsync(
            HbtWorkflowInstance instance,
            HbtWorkflowNode node,
            Dictionary<string, object>? variables = null)
        {
            try
            {
                _logger.Info($"开始执行结束节点: 实例ID={instance.Id}, 节点ID={node.Id}");

                // 更新实例状态为已完成
                instance.Status = 2; // 2 表示已完成状态
                instance.EndTime = DateTime.Now;
                await _instanceRepository.UpdateAsync(instance);

                _logger.Info($"工作流实例[{instance.Id}]已完成");
                return new HbtWorkflowNodeResult { Success = true };
            }
            catch (Exception ex)
            {
                _logger.Error($"执行结束节点失败: {ex.Message}", ex);
                return new HbtWorkflowNodeResult { Success = false, ErrorMessage = ex.Message };
            }
        }
    }
}