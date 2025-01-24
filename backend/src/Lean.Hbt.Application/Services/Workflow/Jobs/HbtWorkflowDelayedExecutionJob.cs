#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowDelayedExecutionJob.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流延迟执行任务Job
//===================================================================

using System;
using System.Text.Json;
using System.Threading.Tasks;
using Lean.Hbt.Application.Services.Workflow.Engine;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Lean.Hbt.Application.Services.Workflow.Jobs
{
    /// <summary>
    /// 工作流延迟执行任务
    /// </summary>
    public class HbtWorkflowDelayedExecutionJob : HbtWorkflowScheduledTaskJob
    {
        public HbtWorkflowDelayedExecutionJob(
            IServiceProvider serviceProvider,
            IHbtLogger logger,
            IHbtWorkflowScheduledTaskService scheduledTaskService,
            IHbtWorkflowEngine workflowEngine)
            : base(serviceProvider, logger, scheduledTaskService, workflowEngine)
        {
        }

        protected override async Task ExecuteTaskAsync(HbtWorkflowScheduledTask task)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IHbtDbContext>();

            // 解析任务参数
            if (string.IsNullOrEmpty(task.TaskParameters))
            {
                _logger.Error($"延迟执行任务参数为空,任务ID:{task.Id}");
                return;
            }

            try
            {
                var parameters = JsonSerializer.Deserialize<Dictionary<string, object>>(task.TaskParameters);
                if (parameters == null || !parameters.ContainsKey("transitionId"))
                {
                    _logger.Error($"延迟执行任务参数缺少transitionId,任务ID:{task.Id}");
                    return;
                }

                // 获取转换ID
                var transitionId = Convert.ToInt64(parameters["transitionId"]);

                // 提取工作流变量
                var variables = new Dictionary<string, object>();
                foreach (var param in parameters)
                {
                    if (param.Key != "transitionId")
                    {
                        variables[param.Key] = param.Value;
                    }
                }

                // 执行工作流转换
                var result = await _workflowEngine.ExecuteTransitionAsync(task.WorkflowInstanceId, transitionId, variables);
                if (!result.Success)
                {
                    _logger.Error($"执行工作流转换失败,任务ID:{task.Id},错误:{result.ErrorMessage}");
                    throw new InvalidOperationException(result.ErrorMessage);
                }

                // 记录操作历史
                var history = new HbtWorkflowHistory
                {
                    WorkflowInstanceId = task.WorkflowInstanceId,
                    NodeId = task.NodeId,
                    OperationType = (int)HbtWorkflowOperationType.Submit,
                    OperatorId = 0,
                    OperatorName = "System",
                    OperationComment = "延迟执行工作流转换",
                    OperationTime = DateTime.Now
                };

                await dbContext.Client.Insertable(history).ExecuteCommandAsync();

                _logger.Info($"延迟执行任务[{task.Id}]已执行工作流转换[{transitionId}]");

                // 检查是否需要创建下一个延迟执行任务
                if (parameters.ContainsKey("nextDelay"))
                {
                    var nextDelay = Convert.ToInt32(parameters["nextDelay"]);
                    if (nextDelay > 0)
                    {
                        var nextTask = new HbtWorkflowScheduledTask
                        {
                            WorkflowInstanceId = task.WorkflowInstanceId,
                            NodeId = task.NodeId,
                            TaskType = HbtWorkflowScheduledTaskType.DelayedExecution,
                            ScheduledTime = DateTime.Now.AddMinutes(nextDelay),
                            Status = HbtWorkflowScheduledTaskStatus.Pending,
                            TaskParameters = JsonSerializer.Serialize(new Dictionary<string, object>
                            {
                                ["transitionId"] = transitionId,
                                ["nextDelay"] = nextDelay
                            })
                        };

                        await dbContext.Client.Insertable(nextTask).ExecuteCommandAsync();
                        _logger.Info($"已创建下一个延迟执行任务,延迟{nextDelay}分钟");
                    }
                }
            }
            catch (JsonException ex)
            {
                _logger.Error($"解析延迟执行任务参数失败,任务ID:{task.Id},错误:{ex.Message}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error($"执行延迟执行任务失败,任务ID:{task.Id},错误:{ex.Message}", ex);
                throw;
            }
        }
    }
} 
