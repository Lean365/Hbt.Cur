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
using Lean.Hbt.Domain.IServices.Extensions;

namespace Lean.Hbt.Application.Services.Workflow.Engine.Executors
{
    /// <summary>
    /// 结束节点执行器
    /// </summary>
    public class HbtEndNodeExecutor : HbtNodeExecutorBase
    {
        private readonly IHbtRepository<HbtInstance> _instanceRepository;
        private readonly IHbtRepository<HbtHistory> _historyRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtEndNodeExecutor(
            IHbtRepository<HbtInstance> instanceRepository,
            IHbtRepository<HbtHistory> historyRepository,
            IHbtLogger logger,
            IHbtRepository<HbtNodeTemplate> nodeTemplateRepository) : base(logger, nodeTemplateRepository)
        {
            _instanceRepository = instanceRepository;
            _historyRepository = historyRepository;
        }

        /// <summary>
        /// 节点类型
        /// </summary>
        protected override int NodeType => 5; // 5 表示结束节点

        /// <summary>
        /// 执行结束节点
        /// </summary>
        protected override async Task<HbtNodeResult> ExecuteInternalAsync(
            HbtInstance instance,
            HbtNode node,
            Dictionary<string, object>? variables = null)
        {
            try
            {
                _logger.Info($"开始执行结束节点: 实例ID={instance.Id}, 节点ID={node.Id}");

                // 更新实例状态为已完成
                instance.Status = 2; // 2 表示已完成状态
                instance.EndTime = DateTime.Now;
                await _instanceRepository.UpdateAsync(instance);

                // 记录工作流结束历史
                await CreateHistoryRecordAsync(instance.Id, node.Id, 5, instance.InitiatorId, 1, "工作流正常结束");

                _logger.Info($"工作流实例[{instance.Id}]已完成");
                return new HbtNodeResult { Success = true };
            }
            catch (Exception ex)
            {
                _logger.Error($"执行结束节点失败: {ex.Message}", ex);
                
                // 记录工作流异常结束历史
                await CreateHistoryRecordAsync(instance.Id, node.Id, 5, instance.InitiatorId, 2, $"工作流异常结束: {ex.Message}");
                
                return new HbtNodeResult { Success = false, ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        /// 创建历史记录
        /// </summary>
        private async Task CreateHistoryRecordAsync(long instanceId, long nodeId, int operationType, long operatorId, int? operationResult = null, string? operationComment = null)
        {
            try
            {
                var history = new HbtHistory
                {
                    InstanceId = instanceId,
                    NodeId = nodeId,
                    OperationType = operationType,
                    OperationResult = operationResult,
                    OperationComment = operationComment ?? string.Empty,
                    CreateBy = operatorId.ToString(),
                    CreateTime = DateTime.Now,
                    UpdateBy = operatorId.ToString(),
                    UpdateTime = DateTime.Now
                };

                await _historyRepository.CreateAsync(history);
                _logger.Info($"创建历史记录成功: 实例ID={instanceId}, 节点ID={nodeId}, 操作类型={operationType}");
            }
            catch (Exception ex)
            {
                _logger.Error($"创建历史记录失败: {ex.Message}", ex);
                // 历史记录创建失败不应该影响主流程，所以只记录日志，不抛出异常
            }
        }
    }
}