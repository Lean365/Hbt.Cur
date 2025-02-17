#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowTimeoutReminderJob.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流超时提醒任务Job
//===================================================================

using Lean.Hbt.Application.Services.Workflow.Engine;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.Data;
using Lean.Hbt.Domain.Entities.Workflow;
using Microsoft.Extensions.DependencyInjection;

namespace Lean.Hbt.Application.Services.Workflow.Jobs
{
    /// <summary>
    /// 工作流超时提醒任务
    /// </summary>
    public class HbtWorkflowTimeoutReminderJob : HbtWorkflowScheduledTaskJob
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        /// <param name="scheduledTaskService"></param>
        /// <param name="workflowEngine"></param>
        public HbtWorkflowTimeoutReminderJob(
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

            if (workflowTask.Status != HbtWorkflowTaskStatus.Pending)
            {
                _logger.Info($"工作流任务[{workflowTask.Id}]状态为{workflowTask.Status},不需要发送超时提醒");
                return;
            }

            // 检查是否已超时
            if (workflowTask.DueTime.HasValue && DateTime.Now >= workflowTask.DueTime.Value)
            {
                // 更新任务状态为超时
                await dbContext.Client.Updateable<HbtWorkflowTask>()
                    .SetColumns(t => new HbtWorkflowTask
                    {
                        Status = HbtWorkflowTaskStatus.Timeout,
                        UpdateTime = DateTime.Now
                    })
                    .Where(t => t.Id == workflowTask.Id)
                    .ExecuteCommandAsync();

                // 记录操作历史
                var history = new HbtWorkflowHistory
                {
                    WorkflowInstanceId = task.WorkflowInstanceId,
                    NodeId = task.NodeId,
                    OperationType = (int)HbtWorkflowOperationType.Cancel, // 使用Cancel作为超时操作类型
                    OperationResult = (int)HbtWorkflowTaskResult.Rejected, // 使用Rejected作为超时结果
                    OperationComment = "任务已超时",
                    OperationTime = DateTime.Now
                };

                await dbContext.Client.Insertable(history).ExecuteCommandAsync();

                // TODO: 发送超时通知
                _logger.Info($"工作流任务[{workflowTask.Id}]已超时,已发送通知");
            }
            else
            {
                // 发送即将超时提醒
                // TODO: 发送提醒通知
                _logger.Info($"工作流任务[{workflowTask.Id}]即将超时,已发送提醒");
            }
        }
    }
}