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

            if (workflowTask.Status != 0) // 待处理
            {
                _logger.Info($"工作流任务[{workflowTask.Id}]状态为{workflowTask.Status},不需要超时提醒");
                return;
            }

            try
            {
                // 记录操作历史
                var history = new HbtWorkflowHistory
                {
                    WorkflowInstanceId = task.WorkflowInstanceId,
                    NodeId = task.NodeId,
                    OperationType = 1, // 超时提醒
                    OperationResult = 1, // 成功
                    OperationComment = "已发送超时提醒",
                    OperationTime = DateTime.Now
                };

                await dbContext.Client.Insertable(history).ExecuteCommandAsync();

                _logger.Info($"工作流任务[{workflowTask.Id}]已发送超时提醒");
            }
            catch (Exception ex)
            {
                _logger.Error($"工作流任务[{workflowTask.Id}]发送超时提醒失败: {ex.Message}", ex);

                // 记录操作历史
                var history = new HbtWorkflowHistory
                {
                    WorkflowInstanceId = task.WorkflowInstanceId,
                    NodeId = task.NodeId,
                    OperationType = 1, // 超时提醒
                    OperationResult = 2, // 失败
                    OperationComment = $"发送超时提醒失败: {ex.Message}",
                    OperationTime = DateTime.Now
                };

                await dbContext.Client.Insertable(history).ExecuteCommandAsync();

                throw;
            }
        }
    }
}