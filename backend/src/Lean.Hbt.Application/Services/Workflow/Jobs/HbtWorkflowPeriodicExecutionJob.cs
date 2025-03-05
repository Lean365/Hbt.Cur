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

            // 获取工作流任务信息
            var workflowTask = await dbContext.Client.Queryable<HbtWorkflowTask>()
                .Where(t => t.WorkflowInstanceId == task.WorkflowInstanceId && t.NodeId == task.NodeId)
                .FirstAsync();

            if (workflowTask == null)
            {
                _logger.Warn($"未找到工作流任务,实例ID:{task.WorkflowInstanceId},节点ID:{task.NodeId}");
                return;
            }

            if (workflowTask.Status != 0) // 0 表示待处理状态
            {
                _logger.Info($"工作流任务[{workflowTask.Id}]状态为{workflowTask.Status},不需要周期执行");
                return;
            }

            try
            {
                // 解析任务参数
                var parameters = string.IsNullOrEmpty(task.TaskParameters)
                    ? new Dictionary<string, object>()
                    : JsonSerializer.Deserialize<Dictionary<string, object>>(task.TaskParameters);

                // 执行工作流引擎
                await _workflowEngine.ExecuteNodeAsync(task.WorkflowInstanceId, task.NodeId, parameters);

                // 更新任务状态为已完成
                await dbContext.Client.Updateable<HbtWorkflowTask>()
                    .SetColumns(t => new HbtWorkflowTask
                    {
                        Status = 3, // 3 表示已完成状态
                        Result = "周期执行成功",
                        UpdateTime = DateTime.Now
                    })
                    .Where(t => t.Id == workflowTask.Id)
                    .ExecuteCommandAsync();

                // 记录操作历史
                var history = new HbtWorkflowHistory
                {
                    WorkflowInstanceId = task.WorkflowInstanceId,
                    NodeId = task.NodeId,
                    OperationType = 5, // 周期执行
                    OperationResult = 1, // 成功
                    OperationComment = "周期执行成功",
                    OperationTime = DateTime.Now
                };

                await dbContext.Client.Insertable(history).ExecuteCommandAsync();

                _logger.Info($"工作流任务[{workflowTask.Id}]周期执行成功");

                // 如果有Cron表达式,创建下一次执行的任务
                if (parameters.ContainsKey("cronExpression"))
                {
                    var cronExpression = parameters["cronExpression"].ToString();
                    if (!string.IsNullOrEmpty(cronExpression))
                    {
                        var cronTrigger = new CronExpression(cronExpression);
                        var nextFireTime = cronTrigger.GetNextValidTimeAfter(DateTime.Now);

                        if (nextFireTime.HasValue)
                        {
                            var nextTask = new HbtWorkflowScheduledTask
                            {
                                WorkflowInstanceId = task.WorkflowInstanceId,
                                NodeId = task.NodeId,
                                TaskType = 5, // 周期执行
                                ScheduledTime = nextFireTime.Value.DateTime,
                                Status = 0, // 待处理
                                TaskParameters = task.TaskParameters
                            };

                            await dbContext.Client.Insertable(nextTask).ExecuteCommandAsync();
                            _logger.Info($"已创建下一次周期执行任务,计划执行时间:{nextFireTime.Value:yyyy-MM-dd HH:mm:ss}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"工作流任务[{workflowTask.Id}]周期执行失败: {ex.Message}", ex);

                // 更新任务状态为失败
                await dbContext.Client.Updateable<HbtWorkflowTask>()
                    .SetColumns(t => new HbtWorkflowTask
                    {
                        Status = 4, // 4 表示失败状态
                        Result = $"周期执行失败: {ex.Message}",
                        UpdateTime = DateTime.Now
                    })
                    .Where(t => t.Id == workflowTask.Id)
                    .ExecuteCommandAsync();

                // 记录操作历史
                var history = new HbtWorkflowHistory
                {
                    WorkflowInstanceId = task.WorkflowInstanceId,
                    NodeId = task.NodeId,
                    OperationType = 5, // 周期执行
                    OperationResult = 2, // 失败
                    OperationComment = $"周期执行失败: {ex.Message}",
                    OperationTime = DateTime.Now
                };

                await dbContext.Client.Insertable(history).ExecuteCommandAsync();

                throw;
            }
        }
    }
}