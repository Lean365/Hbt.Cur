#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowPeriodicExecutionJob.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流周期执行任务Job
//===================================================================

using System.Text.Json;
using Lean.Hbt.Application.Services.Workflow.Engine;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.Data;
using Lean.Hbt.Domain.Entities.Workflow;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Lean.Hbt.Application.Services.Workflow.Jobs
{
    /// <summary>
    /// 工作流周期执行任务
    /// </summary>
    public class HbtWorkflowPeriodicExecutionJob : HbtWorkflowScheduledTaskJob
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        /// <param name="scheduledTaskService"></param>
        /// <param name="workflowEngine"></param>
        public HbtWorkflowPeriodicExecutionJob(
            IServiceProvider serviceProvider,
            IHbtLogger logger,
            IHbtWorkflowScheduledTaskService scheduledTaskService,
            IHbtWorkflowEngine workflowEngine)
            : base(serviceProvider, logger, scheduledTaskService, workflowEngine)
        {
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        protected override async Task ExecuteTaskAsync(HbtWorkflowScheduledTask task)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IHbtDbContext>();

            // 解析任务参数
            if (string.IsNullOrEmpty(task.TaskParameters))
            {
                _logger.Error($"周期执行任务参数为空,任务ID:{task.Id}");
                return;
            }

            try
            {
                var parameters = JsonSerializer.Deserialize<Dictionary<string, object>>(task.TaskParameters);
                if (parameters == null || !parameters.ContainsKey("transitionId") || !parameters.ContainsKey("cronExpression"))
                {
                    _logger.Error($"周期执行任务参数缺少必要字段,任务ID:{task.Id}");
                    return;
                }

                // 获取转换ID和Cron表达式
                var transitionId = Convert.ToInt64(parameters["transitionId"]);
                var cronExpression = parameters["cronExpression"].ToString();

                // 提取工作流变量
                var variables = new Dictionary<string, object>();
                foreach (var param in parameters)
                {
                    if (!new[] { "transitionId", "cronExpression" }.Contains(param.Key))
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
                    OperationComment = "周期执行工作流转换",
                    OperationTime = DateTime.Now
                };

                await dbContext.Client.Insertable(history).ExecuteCommandAsync();

                _logger.Info($"周期执行任务[{task.Id}]已执行工作流转换[{transitionId}]");

                // 创建下一次执行的任务
                var cronTrigger = new CronExpression(cronExpression);
                var nextFireTime = cronTrigger.GetNextValidTimeAfter(DateTimeOffset.Now);
                if (nextFireTime.HasValue)
                {
                    var nextTask = new HbtWorkflowScheduledTask
                    {
                        WorkflowInstanceId = task.WorkflowInstanceId,
                        NodeId = task.NodeId,
                        TaskType = HbtWorkflowScheduledTaskType.PeriodicExecution,
                        ScheduledTime = nextFireTime.Value.DateTime,
                        Status = HbtWorkflowScheduledTaskStatus.Pending,
                        TaskParameters = task.TaskParameters // 使用相同的参数
                    };

                    await dbContext.Client.Insertable(nextTask).ExecuteCommandAsync();
                    _logger.Info($"已创建下一次周期执行任务,计划执行时间:{nextFireTime.Value:yyyy-MM-dd HH:mm:ss}");
                }
            }
            catch (JsonException ex)
            {
                _logger.Error($"解析周期执行任务参数失败,任务ID:{task.Id},错误:{ex.Message}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error($"执行周期执行任务失败,任务ID:{task.Id},错误:{ex.Message}", ex);
                throw;
            }
        }
    }
}