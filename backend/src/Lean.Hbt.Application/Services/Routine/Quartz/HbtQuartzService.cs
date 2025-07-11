//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtQuartzService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 定时任务服务实现
//===================================================================

using Lean.Hbt.Application.Services.Routine.Quartz.Jobs;
using Microsoft.AspNetCore.Http;
using Quartz;
namespace Lean.Hbt.Application.Services.Routine.Quartz
{
    /// <summary>
    /// 定时任务服务实现
    /// </summary>
    public class HbtQuartzService : HbtBaseService, IHbtQuartzService
    {
        private readonly IHbtRepository<HbtQuartz> _taskRepository;
        private readonly IScheduler _scheduler;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="taskRepository">任务仓储</param>
        /// <param name="scheduler">调度器</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtQuartzService(
            IHbtLogger logger,
            IHbtRepository<HbtQuartz> taskRepository,
            IScheduler scheduler,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _taskRepository = taskRepository;
            _scheduler = scheduler;
        }

        /// <summary>
        /// 获取定时任务分页列表
        /// </summary>
        public async Task<HbtPagedResult<HbtQuartzDto>> GetListAsync(HbtQuartzQueryDto query)
        {
            var exp = Expressionable.Create<HbtQuartz>();

            if (!string.IsNullOrEmpty(query.QuartzName))
                exp.And(x => x.QuartzName.Contains(query.QuartzName));

            if (!string.IsNullOrEmpty(query.QuartzGroupName))
                exp.And(x => x.QuartzGroupName == query.QuartzGroupName);

            if (query.QuartzType.HasValue)
                exp.And(x => x.QuartzType == query.QuartzType.Value);

            if (query.Status.HasValue)
                exp.And(x => x.Status == query.Status.Value);

            if (query.StartTime.HasValue)
                exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query.EndTime.HasValue)
                exp.And(x => x.CreateTime <= query.EndTime.Value);

            var result = await _taskRepository.GetPagedListAsync(
                exp.ToExpression(),
                query.PageIndex,
                query.PageSize,
                x => x.Id,
                OrderByType.Asc);

            return new HbtPagedResult<HbtQuartzDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtQuartzDto>>()
            };
        }

        /// <summary>
        /// 获取定时任务详情
        /// </summary>
        public async Task<HbtQuartzDto> GetByIdAsync(long taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new HbtException(L("Quartz.NotFound", taskId));

            return task.Adapt<HbtQuartzDto>();
        }

        /// <summary>
        /// 创建定时任务
        /// </summary>
        public async Task<long> CreateAsync(HbtQuartzCreateDto input)
        {
            var task = input.Adapt<HbtQuartz>();
            task.CreateTime = DateTime.Now;

            // 验证任务是否已存在
            var existingTask = await _taskRepository.GetFirstAsync(x => x.QuartzName == input.QuartzName && x.QuartzGroupName == input.QuartzGroupName);
            if (existingTask != null)
                throw new HbtException(L("Quartz.AlreadyExists", input.QuartzName));

            var result = await _taskRepository.CreateAsync(task);
            if (result <= 0)
                throw new HbtException(L("Quartz.CreateFailed"));

            // 如果任务状态为启用，则立即启动
            if (task.Status == 1)
                await StartAsync(task.Id);

            return task.Id;
        }

        /// <summary>
        /// 更新定时任务
        /// </summary>
        public async Task<bool> UpdateAsync(long taskId, HbtQuartzDto input)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new HbtException(L("Quartz.NotFound", taskId));

            // 如果任务正在运行，需要先停止
            if (task.Status == 1)
                await StopAsync(taskId);

            // 更新任务信息
            input.Adapt(task);
            var result = await _taskRepository.UpdateAsync(task);
            if (result <= 0)
                throw new HbtException(L("Quartz.UpdateFailed"));

            // 如果更新后的状态为启用，则重新启动任务
            if (task.Status == 1)
                await StartAsync(taskId);

            return true;
        }

        /// <summary>
        /// 删除定时任务
        /// </summary>
        public async Task<bool> DeleteAsync(long taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new HbtException(L("Quartz.NotFound", taskId));

            // 如果任务正在运行，需要先停止
            if (task.Status == 1)
                await StopAsync(taskId);

            var result = await _taskRepository.DeleteAsync(taskId);
            return result > 0;
        }

        /// <summary>
        /// 批量删除定时任务
        /// </summary>
        public async Task<bool> BatchDeleteAsync(long[] taskIds)
        {
            if (taskIds == null || taskIds.Length == 0)
                throw new HbtException(L("Quartz.SelectToDelete"));

            foreach (var taskId in taskIds)
            {
                await DeleteAsync(taskId);
            }
            return true;
        }

        /// <summary>
        /// 执行定时任务
        /// </summary>
        public async Task<bool> ExecuteAsync(long taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new HbtException(L("Quartz.NotFound", taskId));

            try
            {
                return await HbtQuartzHelper.TriggerJobAsync(task.QuartzName, task.QuartzGroupName);
            }
            catch (Exception ex)
            {
                _logger.Error(L("Quartz.ExecuteFailed", taskId), ex);
                throw new HbtException(L("Quartz.ExecuteFailed", taskId));
            }
        }

        /// <summary>
        /// 启动定时任务
        /// </summary>
        public async Task<bool> StartAsync(long taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new HbtException(L("Quartz.NotFound", taskId));

            try
            {
                return await HbtQuartzHelper.AddOrUpdateJobAsync<HbtQuartzJob>(task.QuartzName, task.QuartzGroupName, task.QuartzCronExpression);
            }
            catch (Exception ex)
            {
                _logger.Error(L("Quartz.StartFailed", taskId), ex);
                throw new HbtException(L("Quartz.StartFailed", taskId));
            }
        }

        /// <summary>
        /// 停止定时任务
        /// </summary>
        public async Task<bool> StopAsync(long taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new HbtException(L("Quartz.NotFound", taskId));

            try
            {
                return await HbtQuartzHelper.DeleteJobAsync(task.QuartzName, task.QuartzGroupName);
            }
            catch (Exception ex)
            {
                _logger.Error(L("Quartz.StopFailed", taskId), ex);
                throw new HbtException(L("Quartz.StopFailed", taskId));
            }
        }

        /// <summary>
        /// 暂停定时任务
        /// </summary>
        public async Task<bool> PauseAsync(long taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new HbtException(L("Quartz.NotFound", taskId));

            try
            {
                return await HbtQuartzHelper.PauseJobAsync(task.QuartzName, task.QuartzGroupName);
            }
            catch (Exception ex)
            {
                _logger.Error(L("Quartz.PauseFailed", taskId), ex);
                throw new HbtException(L("Quartz.PauseFailed", taskId));
            }
        }

        /// <summary>
        /// 恢复定时任务
        /// </summary>
        public async Task<bool> ResumeAsync(long taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new HbtException(L("Quartz.NotFound", taskId));

            try
            {
                return await HbtQuartzHelper.ResumeJobAsync(task.QuartzName, task.QuartzGroupName);
            }
            catch (Exception ex)
            {
                _logger.Error(L("Quartz.ResumeFailed", taskId), ex);
                throw new HbtException(L("Quartz.ResumeFailed", taskId));
            }
        }

        /// <summary>
        /// 立即执行一次定时任务
        /// </summary>
        public async Task<bool> TriggerAsync(long taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new HbtException(L("Quartz.NotFound", taskId));

            try
            {
                await _scheduler.TriggerJob(new JobKey(task.QuartzName, task.QuartzGroupName));
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(L("Quartz.TriggerFailed", taskId), ex);
                throw new HbtException(L("Quartz.TriggerFailed", taskId));
            }
        }

        /// <summary>
        /// 导出定时任务数据
        /// </summary>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtQuartzQueryDto query, string sheetName = "Quartz")
        {
            try
            {
                var list = await _taskRepository.GetListAsync(KpQuartzQueryExpression(query));
                return await HbtExcelHelper.ExportAsync(list.Adapt<List<HbtQuartzExportDto>>(), sheetName, L("Quartz.ExportTitle"));
            }
            catch (Exception ex)
            {
                _logger.Error(L("Quartz.ExportFailed"), ex);
                throw new HbtException(L("Quartz.ExportFailed"));
            }
        }

        private Expression<Func<HbtQuartz, bool>> KpQuartzQueryExpression(HbtQuartzQueryDto query)
        {
            return Expressionable.Create<HbtQuartz>()
                .AndIF(!string.IsNullOrEmpty(query.QuartzName), x => x.QuartzName.Contains(query.QuartzName))
                .AndIF(!string.IsNullOrEmpty(query.QuartzGroupName), x => x.QuartzGroupName == query.QuartzGroupName)
                .AndIF(query.QuartzType.HasValue, x => x.QuartzType == query.QuartzType.Value)
                .AndIF(query.Status.HasValue, x => x.Status == query.Status.Value)
                .AndIF(query.StartTime.HasValue, x => x.CreateTime >= query.StartTime.Value)
                .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime.Value)
                .ToExpression();
        }
    }
}