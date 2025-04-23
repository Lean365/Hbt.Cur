#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : ApprovalNodeExecutor.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 审批节点执行器
//===================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.Models.Workflow;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Application.Services.Workflow.Engine.Resolvers;
using Lean.Hbt.Domain.IServices.Extensions;

namespace Lean.Hbt.Application.Services.Workflow.Engine.Executors
{
    /// <summary>
    /// 审批节点执行器
    /// </summary>
    public class HbtApprovalNodeExecutor : HbtWorkflowNodeExecutorBase
    {
        private readonly IHbtRepository<HbtWorkflowTask> _taskRepository;
        private readonly IHbtWorkflowApproverResolver _approverResolver;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtApprovalNodeExecutor(
            IHbtLogger logger,
            IHbtRepository<HbtWorkflowTask> taskRepository,
            IHbtWorkflowApproverResolver approverResolver) 
            : base(logger)
        {
            _taskRepository = taskRepository;
            _approverResolver = approverResolver;
        }

        /// <summary>
        /// 节点类型
        /// </summary>
        protected override int NodeType => 2; // 2 表示审批节点

        /// <summary>
        /// 执行审批节点
        /// </summary>
        protected override async Task<HbtWorkflowNodeResult> ExecuteInternalAsync(
            HbtWorkflowInstance instance,
            HbtWorkflowNode node,
            Dictionary<string, object>? variables = null)
        {
            try
            {
                _logger.Info($"开始执行审批节点: 实例ID={instance.Id}, 节点ID={node.Id}");

                // 解析节点配置
                var config = JsonSerializer.Deserialize<HbtWorkflowNodeConfig>(node.NodeConfig);
                if (config == null)
                {
                    var error = $"节点配置无效: 节点ID={node.Id}";
                    _logger.Error(error);
                    return CreateFailureResult(error);
                }

                // 解析审批人
                _logger.Info($"开始解析审批人: 审批类型={config.ApproverType}");
                var approvers = await _approverResolver.ResolveApproversAsync(instance, node, config);
                if (!approvers.Any())
                {
                    var error = "未找到有效的审批人";
                    _logger.Error(error);
                    return CreateFailureResult(error);
                }
                _logger.Info($"成功解析审批人: 数量={approvers.Count}");

                // 创建审批任务
                foreach (var approverId in approvers)
                {
                    var task = new HbtWorkflowTask
                    {
                        WorkflowInstanceId = instance.Id,
                        NodeId = node.Id,
                        AssigneeId = approverId,
                        TaskType = 2, // 2 表示审批任务
                        Status = 0, // 0 表示待处理状态
                        CreateTime = DateTime.Now
                    };

                    await _taskRepository.CreateAsync(task);
                    _logger.Info($"已创建审批任务: 任务ID={task.Id}, 审批人ID={approverId}");
                }

                _logger.Info($"审批节点执行完成: 实例ID={instance.Id}, 节点ID={node.Id}");
                return CreateSuccessResult();
            }
            catch (JsonException ex)
            {
                var error = $"节点配置解析失败: {ex.Message}";
                _logger.Error(error, ex);
                return CreateFailureResult(error);
            }
            catch (Exception ex)
            {
                var error = $"审批节点执行异常: {ex.Message}";
                _logger.Error(error, ex);
                return CreateFailureResult(error);
            }
        }
    }
} 