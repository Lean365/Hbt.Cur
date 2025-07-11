#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTimeoutReminderJob.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流超时提醒任务Job
//===================================================================

using Lean.Hbt.Application.Services.Workflow.Engine;
using Lean.Hbt.Domain.Data;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.IServices.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Lean.Hbt.Application.Services.Workflow.Jobs
{
    /// <summary>
    /// 工作流超时提醒任务
    /// </summary>
    public class HbtTimeoutReminderJob : HbtScheduledTaskJob
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        /// <param name="scheduledTaskService"></param>
        /// <param name="workflowEngine"></param>
        public HbtTimeoutReminderJob(
            IServiceProvider serviceProvider,
            IHbtLogger logger,
            IHbtScheduledTaskService scheduledTaskService,
            IHbtWorkflowEngine workflowEngine)
            : base(serviceProvider, logger, scheduledTaskService, workflowEngine)
        {
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        protected override async Task ExecuteTaskAsync(HbtScheduledTask task)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IHbtDbContext>();

            // 获取工作流任务信息
            var workflowTask = await dbContext.Client.Queryable<HbtProcessTask>()
                .Where(t => t.InstanceId == task.InstanceId && t.NodeId == task.NodeId)
                .FirstAsync();

            if (workflowTask == null)
            {
                _logger.Warn($"未找到工作流任务,实例ID:{task.InstanceId},节点ID:{task.NodeId}");
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
                var history = new HbtHistory
                {
                    InstanceId = task.InstanceId,
                    NodeId = task.NodeId,
                    OperationType = 2, // 超时提醒
                    OperationResult = 1, // 成功
                    OperationComment = "超时提醒发送成功",
                    CreateBy = "System",
                    CreateTime = DateTime.Now,
                    UpdateBy = "System",
                    UpdateTime = DateTime.Now
                };

                await dbContext.Client.Insertable(history).ExecuteCommandAsync();

                _logger.Info($"工作流任务[{workflowTask.Id}]超时提醒发送成功");
            }
            catch (Exception ex)
            {
                _logger.Error($"工作流任务[{workflowTask.Id}]超时提醒发送失败: {ex.Message}", ex);

                // 记录操作历史
                var history = new HbtHistory
                {
                    InstanceId = task.InstanceId,
                    NodeId = task.NodeId,
                    OperationType = 2, // 超时提醒
                    OperationResult = 2, // 失败
                    OperationComment = $"超时提醒发送失败: {ex.Message}",
                    CreateBy = "System",
                    CreateTime = DateTime.Now,
                    UpdateBy = "System",
                    UpdateTime = DateTime.Now
                };

                await dbContext.Client.Insertable(history).ExecuteCommandAsync();

                throw;
            }
        }
    }
}