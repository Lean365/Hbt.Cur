#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtAutoExecutionJob.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流自动执行任务Job
//===================================================================

using System.Text.Json;
using Lean.Hbt.Application.Services.Workflow.Engine;
using Lean.Hbt.Domain.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Lean.Hbt.Application.Services.Workflow.Jobs
{
    /// <summary>
    /// 工作流自动执行任务
    /// </summary>
    public class HbtAutoExecutionJob : HbtScheduledTaskJob
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        /// <param name="scheduledTaskService"></param>
        /// <param name="workflowEngine"></param>
        public HbtAutoExecutionJob(
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
                _logger.Info($"工作流任务[{workflowTask.Id}]状态为{workflowTask.Status},不需要自动执行");
                return;
            }

            try
            {
                // 解析任务参数
                var parameters = string.IsNullOrEmpty(task.TaskParameters)
                    ? new Dictionary<string, object>()
                    : JsonSerializer.Deserialize<Dictionary<string, object>>(task.TaskParameters);

                // 执行工作流引擎
                await _workflowEngine.ExecuteNodeAsync(task.InstanceId, task.NodeId, parameters);

                // 更新任务状态为已完成
                await dbContext.Client.Updateable<HbtProcessTask>()
                    .SetColumns(t => new HbtProcessTask
                    {
                        Status = 3, // 已完成
                        Result = 1, // 1 表示成功
                        UpdateTime = DateTime.Now
                    })
                    .Where(t => t.Id == workflowTask.Id)
                    .ExecuteCommandAsync();

                // 记录操作历史
                var history = new HbtHistory
                {
                    InstanceId = task.InstanceId,
                    NodeId = task.NodeId,
                    OperationType = 2, // 自动执行
                    OperationResult = 1, // 成功
                    OperationComment = "自动执行成功",
                    OperationTime = DateTime.Now
                };

                await dbContext.Client.Insertable(history).ExecuteCommandAsync();

                _logger.Info($"工作流任务[{workflowTask.Id}]自动执行成功");
            }
            catch (Exception ex)
            {
                _logger.Error($"工作流任务[{workflowTask.Id}]自动执行失败: {ex.Message}", ex);

                // 更新任务状态为已失败
                await dbContext.Client.Updateable<HbtProcessTask>()
                    .SetColumns(t => new HbtProcessTask
                    {
                        Status = 4, // 已失败
                        Result = 2, // 2 表示失败
                        UpdateTime = DateTime.Now
                    })
                    .Where(t => t.Id == workflowTask.Id)
                    .ExecuteCommandAsync();

                // 记录操作历史
                var history = new HbtHistory
                {
                    InstanceId = task.InstanceId,
                    NodeId = task.NodeId,
                    OperationType = 2, // 自动执行
                    OperationResult = 2, // 失败
                    OperationComment = $"自动执行失败: {ex.Message}",
                    OperationTime = DateTime.Now
                };

                await dbContext.Client.Insertable(history).ExecuteCommandAsync();

                throw;
            }
        }
    }
}