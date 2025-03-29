//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtQuartzTaskService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 定时任务服务实现
//===================================================================

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.Routine;
using Lean.Hbt.Application.Dtos.Routine;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Domain.Repositories;
using SqlSugar;
using Mapster;
using Quartz;

namespace Lean.Hbt.Application.Services.Routine
{
    /// <summary>
    /// 定时任务服务实现
    /// </summary>
    public class HbtQuartzTaskService : IHbtQuartzTaskService
    {
        private readonly ILogger<HbtQuartzTaskService> _logger;
        private readonly IHbtRepository<HbtQuartzTask> _taskRepository;
        private readonly IScheduler _scheduler;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="taskRepository">任务仓储</param>
        /// <param name="scheduler">调度器</param>
        public HbtQuartzTaskService(
            ILogger<HbtQuartzTaskService> logger,
            IHbtRepository<HbtQuartzTask> taskRepository,
            IScheduler scheduler)
        {
            _logger = logger;
            _taskRepository = taskRepository;
            _scheduler = scheduler;
        }

        /// <summary>
        /// 获取定时任务分页列表
        /// </summary>
        public async Task<HbtPagedResult<HbtQuartzTaskDto>> GetListAsync(HbtQuartzTaskQueryDto query)
        {
            var exp = Expressionable.Create<HbtQuartzTask>();

            if (!string.IsNullOrEmpty(query.TaskName))
                exp.And(x => x.TaskName.Contains(query.TaskName));

            if (!string.IsNullOrEmpty(query.TaskGroupName))
                exp.And(x => x.TaskGroupName.Contains(query.TaskGroupName));

            if (query.TaskType.HasValue)
                exp.And(x => x.TaskType == query.TaskType.Value);

            if (query.TaskTriggerType.HasValue)
                exp.And(x => x.TaskTriggerType == query.TaskTriggerType.Value);

            if (query.TaskStatus.HasValue)
                exp.And(x => x.TaskStatus == query.TaskStatus.Value);

            if (query.StartTime.HasValue)
                exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query.EndTime.HasValue)
                exp.And(x => x.CreateTime <= query.EndTime.Value);

        // 2.查询数据
        var result = await _taskRepository.GetPagedListAsync(
            exp.ToExpression(),
            query.PageIndex,
            query.PageSize,
            x => x.CreateTime,
            OrderByType.Desc);

        // 3.转换数据
        return new HbtPagedResult<HbtQuartzTaskDto>
        {
            TotalNum = result.TotalNum,
            PageIndex = query.PageIndex,
            PageSize = query.PageSize,
            Rows = result.Rows.Select(x => x.Adapt<HbtQuartzTaskDto>()).ToList()
        };
        }

        /// <summary>
        /// 获取定时任务详情
        /// </summary>
        public async Task<HbtQuartzTaskDto> GetByIdAsync(long taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new HbtException($"定时任务不存在: {taskId}");

            return task.Adapt<HbtQuartzTaskDto>();
        }

        /// <summary>
        /// 创建定时任务
        /// </summary>
        public async Task<long> CreateAsync(HbtQuartzTaskCreateDto input)
        {
            var task = input.Adapt<HbtQuartzTask>();
            task.CreateTime = DateTime.Now;

            // 验证任务是否已存在
            var existingTask = await _taskRepository.GetInfoAsync(x => x.TaskName == input.TaskName && x.TaskGroupName == input.TaskGroupName);
            if (existingTask != null)
                throw new HbtException($"任务已存在: {input.TaskName}");

            var result = await _taskRepository.CreateAsync(task);
            if (result <= 0)
                throw new HbtException("创建定时任务失败");

            // 如果任务状态为启用，则立即启动
            if (task.TaskStatus == 1)
                await StartAsync(task.Id);

            return task.Id;
        }

        /// <summary>
        /// 更新定时任务
        /// </summary>
        public async Task<bool> UpdateAsync(long taskId, HbtQuartzTaskDto input)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new HbtException($"定时任务不存在: {taskId}");

            // 如果任务正在运行，需要先停止
            if (task.TaskStatus == 1)
                await StopAsync(taskId);

            // 更新任务信息
            input.Adapt(task);
            var result = await _taskRepository.UpdateAsync(task);
            if (result <= 0)
                throw new HbtException("更新定时任务失败");

            // 如果更新后的状态为启用，则重新启动任务
            if (task.TaskStatus == 1)
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
                throw new HbtException($"定时任务不存在: {taskId}");

            // 如果任务正在运行，需要先停止
            if (task.TaskStatus == 1)
                await StopAsync(taskId);

            var result = await _taskRepository.DeleteAsync(taskId);
            return result > 0;
        }

        /// <summary>
        /// 批量删除定时任务
        /// </summary>
        public async Task<bool> BatchDeleteAsync(long[] taskIds)
        {
            foreach (var taskId in taskIds)
            {
                await DeleteAsync(taskId);
            }
            return true;
        }

        /// <summary>
        /// 启动定时任务
        /// </summary>
        public async Task<bool> StartAsync(long taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new HbtException($"定时任务不存在: {taskId}");

            // 创建任务
            var jobDetail = JobBuilder.Create<IJob>()
                .WithIdentity(task.TaskName, task.TaskGroupName)
                .UsingJobData("taskId", task.Id)
                .Build();

            // 创建触发器
            ITrigger trigger;
            if (task.TaskTriggerType == 0) // Simple触发器
            {
                trigger = TriggerBuilder.Create()
                    .WithIdentity($"{task.TaskName}_trigger", task.TaskGroupName)
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(task.TaskInterval)
                        .RepeatForever())
                    .Build();
            }
            else // Cron触发器
            {
                trigger = TriggerBuilder.Create()
                    .WithIdentity($"{task.TaskName}_trigger", task.TaskGroupName)
                    .WithCronSchedule(task.TaskCronExpression)
                    .Build();
            }

            // 设置开始时间和结束时间
            if (task.TaskStartTime.HasValue)
                trigger.GetTriggerBuilder().StartAt(task.TaskStartTime.Value);
            if (task.TaskEndTime.HasValue)
                trigger.GetTriggerBuilder().EndAt(task.TaskEndTime.Value);

            // 调度任务
            await _scheduler.ScheduleJob(jobDetail, trigger);

            // 更新任务状态
            task.TaskStatus = 1;
            await _taskRepository.UpdateAsync(task);

            return true;
        }

        /// <summary>
        /// 停止定时任务
        /// </summary>
        public async Task<bool> StopAsync(long taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new HbtException($"定时任务不存在: {taskId}");

            // 暂停任务
            await _scheduler.PauseJob(new JobKey(task.TaskName, task.TaskGroupName));
            // 移除任务
            await _scheduler.DeleteJob(new JobKey(task.TaskName, task.TaskGroupName));

            // 更新任务状态
            task.TaskStatus = 0;
            await _taskRepository.UpdateAsync(task);

            return true;
        }

        /// <summary>
        /// 立即执行定时任务
        /// </summary>
        public async Task<bool> ExecuteAsync(long taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new HbtException($"定时任务不存在: {taskId}");

            await _scheduler.TriggerJob(new JobKey(task.TaskName, task.TaskGroupName));
            return true;
        }

        /// <summary>
        /// 导出定时任务数据
        /// </summary>
        public async Task<byte[]> ExportAsync(HbtQuartzTaskQueryDto query, string sheetName = "定时任务数据")
        {
            var exp = Expressionable.Create<HbtQuartzTask>();

            if (!string.IsNullOrEmpty(query.TaskName))
                exp.And(x => x.TaskName.Contains(query.TaskName));

            if (!string.IsNullOrEmpty(query.TaskGroupName))
                exp.And(x => x.TaskGroupName.Contains(query.TaskGroupName));

            if (query.TaskType.HasValue)
                exp.And(x => x.TaskType == query.TaskType.Value);

            if (query.TaskTriggerType.HasValue)
                exp.And(x => x.TaskTriggerType == query.TaskTriggerType.Value);

            if (query.TaskStatus.HasValue)
                exp.And(x => x.TaskStatus == query.TaskStatus.Value);

            var list = await _taskRepository.GetListAsync(exp.ToExpression());
            var result = list.Adapt<List<HbtQuartzTaskExportDto>>();

            return await HbtExcelHelper.ExportAsync(result, sheetName);
        }
    }
} 