#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowTimedTriggerJob.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流定时触发任务Job
//===================================================================

using System.Text.Json;
using Lean.Hbt.Application.Services.Workflow.Engine;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.Data;
using Lean.Hbt.Domain.Entities.Workflow;
using Microsoft.Extensions.DependencyInjection;

namespace Lean.Hbt.Application.Services.Workflow.Jobs
{
    /// <summary>
    /// 工作流定时触发任务
    /// </summary>
    public class HbtWorkflowTimedTriggerJob : HbtWorkflowScheduledTaskJob
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        /// <param name="scheduledTaskService"></param>
        /// <param name="workflowEngine"></param>
        public HbtWorkflowTimedTriggerJob(
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
                _logger.Error($"定时触发任务参数为空,任务ID:{task.Id}");
                return;
            }

            try
            {
                var parameters = JsonSerializer.Deserialize<Dictionary<string, object>>(task.TaskParameters);
                if (parameters == null || !parameters.ContainsKey("workflowDefinitionId"))
                {
                    _logger.Error($"定时触发任务参数缺少workflowDefinitionId,任务ID:{task.Id}");
                    return;
                }

                // 获取工作流定义ID
                var workflowDefinitionId = Convert.ToInt64(parameters["workflowDefinitionId"]);
                var workflowDefinition = await dbContext.Client.Queryable<HbtWorkflowDefinition>()
                    .FirstAsync(d => d.Id == workflowDefinitionId);

                if (workflowDefinition == null)
                {
                    _logger.Error($"未找到工作流定义,ID:{workflowDefinitionId}");
                    return;
                }

                if (workflowDefinition.Status != HbtWorkflowStatus.Published)
                {
                    _logger.Error($"工作流定义[{workflowDefinitionId}]状态为{workflowDefinition.Status},无法启动新实例");
                    return;
                }

                // 获取标题和发起人ID
                var title = parameters.ContainsKey("title") ? parameters["title"].ToString() : workflowDefinition.WorkflowName;
                var initiatorId = parameters.ContainsKey("initiatorId") ? Convert.ToInt64(parameters["initiatorId"]) : 0;
                var formData = parameters.ContainsKey("formData") ? parameters["formData"].ToString() : "{}";

                // 提取工作流变量
                var variables = new Dictionary<string, object>();
                foreach (var param in parameters)
                {
                    if (!new[] { "workflowDefinitionId", "title", "initiatorId", "formData" }.Contains(param.Key))
                    {
                        variables[param.Key] = param.Value;
                    }
                }

                // 启动工作流实例
                var instanceId = await _workflowEngine.StartAsync(workflowDefinitionId, title!, initiatorId, formData, variables);

                // 记录操作历史
                var history = new HbtWorkflowHistory
                {
                    WorkflowInstanceId = instanceId,
                    OperationType = (int)HbtWorkflowOperationType.Create,
                    OperatorId = initiatorId,
                    OperatorName = "System",
                    OperationComment = "定时触发创建工作流实例",
                    OperationTime = DateTime.Now
                };

                await dbContext.Client.Insertable(history).ExecuteCommandAsync();

                _logger.Info($"定时触发任务[{task.Id}]已创建并启动工作流实例[{instanceId}]");
            }
            catch (JsonException ex)
            {
                _logger.Error($"解析定时触发任务参数失败,任务ID:{task.Id},错误:{ex.Message}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error($"执行定时触发任务失败,任务ID:{task.Id},错误:{ex.Message}", ex);
                throw;
            }
        }
    }
}