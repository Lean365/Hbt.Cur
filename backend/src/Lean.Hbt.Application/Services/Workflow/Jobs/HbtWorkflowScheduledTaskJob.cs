#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowScheduledTaskJob.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流定时任务Job基类
//===================================================================

using System;
using System.Threading.Tasks;
using Lean.Hbt.Application.Services.Workflow.Engine;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.Data;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Lean.Hbt.Application.Services.Workflow.Jobs
{
    /// <summary>
    /// 工作流调度任务基类
    /// </summary>
    public abstract class HbtWorkflowScheduledTaskJob : IJob
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly IHbtLogger _logger;
        protected readonly IHbtWorkflowScheduledTaskService _scheduledTaskService;
        protected readonly IHbtWorkflowEngine _workflowEngine;

        protected HbtWorkflowScheduledTaskJob(
            IServiceProvider serviceProvider,
            IHbtLogger logger,
            IHbtWorkflowScheduledTaskService scheduledTaskService,
            IHbtWorkflowEngine workflowEngine)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _scheduledTaskService = scheduledTaskService;
            _workflowEngine = workflowEngine;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var taskId = context.JobDetail.JobDataMap.GetLong("TaskId");
            if (taskId <= 0)
            {
                _logger.Error($"任务ID为空,无法执行调度任务");
                return;
            }

            try
            {
                // 获取任务信息
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IHbtDbContext>();
                var task = await dbContext.Client.Queryable<HbtWorkflowScheduledTask>()
                    .FirstAsync(t => t.Id == taskId);

                if (task == null)
                {
                    _logger.Error($"未找到ID为{taskId}的调度任务");
                    return;
                }

                if (task.Status != HbtWorkflowScheduledTaskStatus.Pending)
                {
                    _logger.Warn($"调度任务[{taskId}]状态为{task.Status},跳过执行");
                    return;
                }

                await _scheduledTaskService.UpdateStatusAsync(taskId, HbtWorkflowScheduledTaskStatus.Running);

                try
                {
                    await ExecuteTaskAsync(task);
                    await _scheduledTaskService.UpdateStatusAsync(taskId, HbtWorkflowScheduledTaskStatus.Completed);
                }
                catch (Exception ex)
                {
                    _logger.Error($"执行调度任务[{taskId}]失败: {ex.Message}", ex);
                    
                    if (task.RetryCount < task.MaxRetryCount)
                    {
                        await _scheduledTaskService.UpdateStatusAsync(taskId, HbtWorkflowScheduledTaskStatus.Pending);
                        
                        // 更新重试次数
                        await dbContext.Client.Updateable<HbtWorkflowScheduledTask>()
                            .SetColumns(t => new HbtWorkflowScheduledTask
                            {
                                RetryCount = t.RetryCount + 1,
                                UpdateTime = DateTime.Now
                            })
                            .Where(t => t.Id == taskId)
                            .ExecuteCommandAsync();
                        
                        var jobKey = new JobKey(taskId.ToString());
                        var triggerKey = new TriggerKey($"{taskId}_retry_{task.RetryCount + 1}");
                        
                        var trigger = TriggerBuilder.Create()
                            .WithIdentity(triggerKey)
                            .StartAt(DateTimeOffset.Now.AddMinutes(5)) // 5分钟后重试
                            .Build();
                            
                        await context.Scheduler.RescheduleJob(triggerKey, trigger);
                    }
                    else
                    {
                        await _scheduledTaskService.UpdateStatusAsync(taskId, HbtWorkflowScheduledTaskStatus.Failed, ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"处理调度任务[{taskId}]时发生错误: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 执行具体的调度任务
        /// </summary>
        protected abstract Task ExecuteTaskAsync(HbtWorkflowScheduledTask task);
    }
} 
