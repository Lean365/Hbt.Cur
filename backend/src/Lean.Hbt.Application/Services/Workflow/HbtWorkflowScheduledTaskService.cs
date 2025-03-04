#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowScheduledTaskService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流定时任务服务实现
//===================================================================

using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.Data;
using Lean.Hbt.Domain.Entities.Workflow;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流定时任务服务实现
    /// </summary>
    public class HbtWorkflowScheduledTaskService : IHbtWorkflowScheduledTaskService
    {
        private readonly IHbtDbContext _dbContext;
        private readonly IHbtLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtWorkflowScheduledTaskService(IHbtDbContext dbContext, IHbtLogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<long> CreateAsync(long workflowInstanceId, long nodeId, HbtWorkflowScheduledTaskType taskType, DateTime scheduledTime, string? parameters = null)
        {
            try
            {
                var task = new HbtWorkflowScheduledTask
                {
                    WorkflowInstanceId = workflowInstanceId,
                    NodeId = nodeId,
                    TaskType = taskType,
                    ScheduledTime = scheduledTime,
                    Status = HbtWorkflowScheduledTaskStatus.Pending,
                    RetryCount = 0,
                    MaxRetryCount = 3, // 默认最大重试次数为3
                    TaskParameters = parameters
                };

                await _dbContext.Client.Insertable(task).ExecuteCommandAsync();
                return task.Id;
            }
            catch (Exception ex)
            {
                _logger.Error($"创建定时任务失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> CancelAsync(long taskId)
        {
            try
            {
                var task = await _dbContext.Client.Queryable<HbtWorkflowScheduledTask>()
                    .FirstAsync(t => t.Id == taskId);

                if (task == null)
                {
                    _logger.Warn($"未找到ID为{taskId}的定时任务");
                    return false;
                }

                if (task.Status != HbtWorkflowScheduledTaskStatus.Pending)
                {
                    _logger.Warn($"定时任务{taskId}状态为{task.Status},无法取消");
                    return false;
                }

                var result = await _dbContext.Client.Updateable<HbtWorkflowScheduledTask>()
                    .SetColumns(t => new HbtWorkflowScheduledTask
                    {
                        Status = HbtWorkflowScheduledTaskStatus.Cancelled,
                        UpdateTime = DateTime.Now
                    })
                    .Where(t => t.Id == taskId && t.Status == HbtWorkflowScheduledTaskStatus.Pending)
                    .ExecuteCommandAsync() > 0;

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"取消定时任务失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> ExecuteAsync(long taskId)
        {
            try
            {
                var task = await _dbContext.Client.Queryable<HbtWorkflowScheduledTask>()
                    .FirstAsync(t => t.Id == taskId);

                if (task == null)
                {
                    _logger.Warn($"未找到ID为{taskId}的定时任务");
                    return false;
                }

                if (task.Status != HbtWorkflowScheduledTaskStatus.Pending)
                {
                    _logger.Warn($"定时任务{taskId}状态为{task.Status},无法执行");
                    return false;
                }

                // 更新任务状态为执行中
                await _dbContext.Client.Updateable<HbtWorkflowScheduledTask>()
                    .SetColumns(t => new HbtWorkflowScheduledTask
                    {
                        Status = HbtWorkflowScheduledTaskStatus.Running,
                        ExecutedTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    })
                    .Where(t => t.Id == taskId && t.Status == HbtWorkflowScheduledTaskStatus.Pending)
                    .ExecuteCommandAsync();

                try
                {
                    // TODO: 根据任务类型执行相应的业务逻辑
                    switch (task.TaskType)
                    {
                        case HbtWorkflowScheduledTaskType.TimeoutReminder:
                            // 执行超时提醒任务
                            break;

                        case HbtWorkflowScheduledTaskType.AutoExecution:
                            // 执行自动执行任务
                            break;

                        case HbtWorkflowScheduledTaskType.TimedTrigger:
                            // 执行定时触发任务
                            break;

                        case HbtWorkflowScheduledTaskType.DelayedExecution:
                            // 执行延迟执行任务
                            break;

                        case HbtWorkflowScheduledTaskType.PeriodicExecution:
                            // 执行周期执行任务
                            break;

                        default:
                            throw new NotSupportedException($"不支持的任务类型: {task.TaskType}");
                    }

                    // 更新任务状态为已完成
                    await UpdateStatusAsync(taskId, HbtWorkflowScheduledTaskStatus.Completed);
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.Error($"执行定时任务{taskId}失败: {ex.Message}", ex);

                    // 更新重试次数和状态
                    if (task.RetryCount >= task.MaxRetryCount)
                    {
                        await UpdateStatusAsync(taskId, HbtWorkflowScheduledTaskStatus.Failed, ex.Message);
                    }
                    else
                    {
                        await _dbContext.Client.Updateable<HbtWorkflowScheduledTask>()
                            .SetColumns(t => new HbtWorkflowScheduledTask
                            {
                                Status = HbtWorkflowScheduledTaskStatus.Pending,
                                RetryCount = t.RetryCount + 1,
                                ErrorMessage = ex.Message,
                                UpdateTime = DateTime.Now
                            })
                            .Where(t => t.Id == taskId)
                            .ExecuteCommandAsync();
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"执行定时任务失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<List<HbtWorkflowScheduledTaskDto>> GetPendingTasksAsync(int batchSize = 100)
        {
            try
            {
                var tasks = await _dbContext.Client.Queryable<HbtWorkflowScheduledTask>()
                    .LeftJoin<HbtWorkflowInstance>((t, i) => t.WorkflowInstanceId == i.Id)
                    .LeftJoin<HbtWorkflowNode>((t, i, n) => t.NodeId == n.Id)
                    .Where(t => t.Status == HbtWorkflowScheduledTaskStatus.Pending && t.ScheduledTime <= DateTime.Now)
                    .OrderBy(t => t.ScheduledTime)
                    .Take(batchSize)
                    .Select((t, i, n) => new HbtWorkflowScheduledTaskDto
                    {
                        WorkflowScheduledTaskId = t.Id,
                        WorkflowInstanceId = t.WorkflowInstanceId,
                        NodeId = t.NodeId,
                        TaskType = t.TaskType,
                        ScheduledTime = t.ScheduledTime,
                        ExecutedTime = t.ExecutedTime,
                        Status = t.Status,
                        RetryCount = t.RetryCount,
                        MaxRetryCount = t.MaxRetryCount,
                        ErrorMessage = t.ErrorMessage,
                        TaskParameters = t.TaskParameters,
                        WorkflowTitle = i.WorkflowTitle,
                        NodeName = n.NodeName
                    })
                    .ToListAsync();

                return tasks;
            }
            catch (Exception ex)
            {
                _logger.Error($"获取待执行的定时任务列表失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateStatusAsync(long taskId, HbtWorkflowScheduledTaskStatus status, string? errorMessage = null)
        {
            try
            {
                var result = await _dbContext.Client.Updateable<HbtWorkflowScheduledTask>()
                    .SetColumns(t => new HbtWorkflowScheduledTask
                    {
                        Status = status,
                        ErrorMessage = errorMessage,
                        UpdateTime = DateTime.Now
                    })
                    .Where(t => t.Id == taskId)
                    .ExecuteCommandAsync() > 0;

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"更新定时任务状态失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<int> CleanupExpiredTasksAsync(int days = 30)
        {
            try
            {
                var cutoffDate = DateTime.Now.AddDays(-days);
                var result = await _dbContext.Client.Deleteable<HbtWorkflowScheduledTask>()
                    .Where(t => t.Status != HbtWorkflowScheduledTaskStatus.Pending && t.CreateTime < cutoffDate)
                    .ExecuteCommandAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"清理过期定时任务失败: {ex.Message}", ex);
                throw;
            }
        }
    }
}