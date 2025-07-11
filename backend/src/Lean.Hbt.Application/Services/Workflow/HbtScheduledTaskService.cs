#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtScheduledTaskService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流定时任务服务实现
//===================================================================

using Lean.Hbt.Domain.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流定时任务服务实现
    /// </summary>
    public class HbtScheduledTaskService : HbtBaseService, IHbtScheduledTaskService
    {
        private readonly IHbtDbContext _dbContext;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtScheduledTaskService(
            IHbtLogger logger,
            IHbtDbContext dbContext,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<long> CreateAsync(long InstanceId, long nodeId, int taskType, DateTime scheduledTime, string? parameters = null)
        {
            try
            {
                var task = new HbtScheduledTask
                {
                    InstanceId = InstanceId,
                    NodeId = nodeId,
                    TaskType = taskType,
                    ScheduledTime = scheduledTime,
                    Status = 0, // 待处理
                    RetryCount = 0,
                    MaxRetryCount = 3, // 默认最大重试次数为3
                    TaskParameters = parameters
                };

                await _dbContext.Client.Insertable(task).ExecuteCommandAsync();
                return task.Id;
            }
            catch (Exception ex)
            {
                _logger.Error(L("WorkflowScheduledTask.Create.Failed"), ex);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> CancelAsync(long taskId)
        {
            try
            {
                var task = await _dbContext.Client.Queryable<HbtScheduledTask>()
                    .FirstAsync(t => t.Id == taskId);

                if (task == null)
                {
                    _logger.Warn(L("WorkflowScheduledTask.NotFound", taskId));
                    return false;
                }

                if (task.Status != 0) // 待处理
                {
                    _logger.Warn(L("WorkflowScheduledTask.CannotCancel", taskId, task.Status));
                    return false;
                }

                var result = await _dbContext.Client.Updateable<HbtScheduledTask>()
                    .SetColumns(t => new HbtScheduledTask
                    {
                        Status = 2, // 已取消
                        UpdateTime = DateTime.Now
                    })
                    .Where(t => t.Id == taskId && t.Status == 0) // 待处理
                    .ExecuteCommandAsync() > 0;

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(L("WorkflowScheduledTask.Cancel.Failed"), ex);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> ExecuteAsync(long taskId)
        {
            try
            {
                var task = await _dbContext.Client.Queryable<HbtScheduledTask>()
                    .FirstAsync(t => t.Id == taskId);

                if (task == null)
                {
                    _logger.Warn(L("WorkflowScheduledTask.NotFound", taskId));
                    return false;
                }

                if (task.Status != 0) // 待处理
                {
                    _logger.Warn(L("WorkflowScheduledTask.CannotExecute", taskId, task.Status));
                    return false;
                }

                // 更新任务状态为执行中
                await _dbContext.Client.Updateable<HbtScheduledTask>()
                    .SetColumns(t => new HbtScheduledTask
                    {
                        Status = 1, // 执行中
                        ExecutedTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    })
                    .Where(t => t.Id == taskId && t.Status == 0) // 待处理
                    .ExecuteCommandAsync();

                try
                {
                    // 根据任务类型执行相应的业务逻辑
                    switch (task.TaskType)
                    {
                        case 1: // 超时提醒
                            // 执行超时提醒任务
                            break;

                        case 2: // 自动执行
                            // 执行自动执行任务
                            break;

                        case 3: // 定时触发
                            // 执行定时触发任务
                            break;

                        case 4: // 延迟执行
                            // 执行延迟执行任务
                            break;

                        case 5: // 周期执行
                            // 执行周期执行任务
                            break;

                        default:
                            throw new NotSupportedException(L("WorkflowScheduledTask.UnsupportedTaskType", task.TaskType));
                    }

                    // 更新任务状态为已完成
                    await UpdateStatusAsync(taskId, 3); // 已完成
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("WorkflowScheduledTask.Execute.Failed", taskId), ex);

                    // 更新重试次数和状态
                    if (task.RetryCount >= task.MaxRetryCount)
                    {
                        await UpdateStatusAsync(taskId, 4, ex.Message); // 已失败
                    }
                    else
                    {
                        await _dbContext.Client.Updateable<HbtScheduledTask>()
                            .SetColumns(t => new HbtScheduledTask
                            {
                                Status = 0, // 待处理
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
                _logger.Error(L("WorkflowScheduledTask.Execute.Failed"), ex);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<List<HbtScheduledTaskDto>> GetPendingTasksAsync(int batchSize = 100)
        {
            try
            {
                var tasks = await _dbContext.Client.Queryable<HbtScheduledTask>()
                    .LeftJoin<HbtInstance>((t, i) => t.InstanceId == i.Id)
                    .LeftJoin<HbtNode>((t, i, n) => t.NodeId == n.Id)
                    .LeftJoin<HbtNodeTemplate>((t, i, n, nt) => n.NodeTemplateId == nt.Id)
                    .Where(t => t.Status == 0 && t.ScheduledTime <= DateTime.Now) // 待处理
                    .OrderBy(t => t.ScheduledTime)
                    .Take(batchSize)
                    .Select((t, i, n, nt) => new HbtScheduledTaskDto
                    {
                        ScheduledTaskId = t.Id,
                        InstanceId = t.InstanceId,
                        NodeId = t.NodeId,
                        TaskType = t.TaskType,
                        ScheduledTime = t.ScheduledTime,
                        ExecutedTime = t.ExecutedTime,
                        Status = t.Status,
                        RetryCount = t.RetryCount,
                        MaxRetryCount = t.MaxRetryCount,
                        ErrorMessage = t.ErrorMessage,
                        TaskParameters = t.TaskParameters,
                        InstanceName = i.InstanceName,
                        NodeName = nt.NodeName
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
        public async Task<bool> UpdateStatusAsync(long taskId, int status, string? errorMessage = null)
        {
            try
            {
                var result = await _dbContext.Client.Updateable<HbtScheduledTask>()
                    .SetColumns(t => new HbtScheduledTask
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
                var expireTime = DateTime.Now.AddDays(-days);
                var result = await _dbContext.Client.Deleteable<HbtScheduledTask>()
                    .Where(t => t.Status == 3 || t.Status == 4 || t.Status == 2) // 已完成、已失败、已取消
                    .Where(t => t.UpdateTime <= expireTime)
                    .ExecuteCommandAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(L("WorkflowScheduledTask.Cleanup.Failed"), ex);
                throw;
            }
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtScheduledTaskImportDto>(sheetName);
        }

        /// <summary>
        /// 导入定时任务数据
        /// </summary>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
        {
            try
            {
                var tasks = await HbtExcelHelper.ImportAsync<HbtScheduledTaskImportDto>(fileStream, sheetName);
                if (!tasks.Any())
                    return (0, 0);

                int success = 0, fail = 0;

                foreach (var task in tasks)
                {
                    try
                    {
                        var entity = task.Adapt<HbtScheduledTask>();
                        entity.CreateTime = DateTime.Now;
                        entity.CreateBy = _currentUser.UserName;
                        entity.Status = 0;

                        var result = await _dbContext.Client.Insertable(entity).ExecuteCommandAsync();
                        if (result > 0)
                            success++;
                        else
                            fail++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn($"导入定时任务失败: {ex.Message}");
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error("导入定时任务数据失败", ex);
                throw new HbtException("导入定时任务数据失败");
            }
        }

        /// <summary>
        /// 导出定时任务数据
        /// </summary>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtScheduledTaskQueryDto query, string sheetName = "Sheet1")
        {
            var list = await _dbContext.Client.Queryable<HbtScheduledTask>()
                .WhereIF(query.InstanceId.HasValue, t => t.InstanceId == query.InstanceId)
                .WhereIF(query.WorkflowNodeId.HasValue, t => t.NodeId == query.WorkflowNodeId)
                .WhereIF(query.TaskType.HasValue, t => t.TaskType == query.TaskType)
                .WhereIF(query.Status.HasValue, t => t.Status == query.Status)
                .ToListAsync();

            var exportList = list.Adapt<List<HbtScheduledTaskExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportList, sheetName, "定时任务数据");
        }
    }
}