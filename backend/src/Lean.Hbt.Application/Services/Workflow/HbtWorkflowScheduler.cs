#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowScheduler.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流任务调度器
//===================================================================

using Lean.Hbt.Application.Services.Workflow.Jobs;
using Lean.Hbt.Domain.IServices.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流调度器
    /// </summary>
    public class HbtWorkflowScheduler : HbtBaseService, IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private IScheduler _scheduler;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider">服务提供者</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtWorkflowScheduler(
            IServiceProvider serviceProvider,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 启动工作流调度器
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                // 创建调度器工厂
                var factory = new StdSchedulerFactory();
                _scheduler = await factory.GetScheduler(cancellationToken);

                // 设置Job工厂
                _scheduler.JobFactory = new HbtWorkflowJobFactory(_serviceProvider);

                // 启动调度器
                await _scheduler.Start(cancellationToken);
                _logger.Info("工作流调度器已启动");
            }
            catch (Exception ex)
            {
                _logger.Error("启动工作流调度器失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 停止工作流调度器
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                if (_scheduler != null)
                {
                    await _scheduler.Shutdown(cancellationToken);
                    _logger.Info("工作流调度器已停止");
                }
            }
            catch (Exception ex)
            {
                _logger.Error("停止工作流调度器失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 调度任务
        /// </summary>
        public async Task ScheduleJobAsync(long taskId, int taskType, DateTime scheduledTime)
        {
            try
            {
                var jobType = GetJobType(taskType);
                var jobKey = new JobKey($"job_{taskId}", "workflow");
                var triggerKey = new TriggerKey($"trigger_{taskId}", "workflow");

                // 创建Job
                var jobDetail = JobBuilder.Create(jobType)
                    .WithIdentity(jobKey)
                    .UsingJobData("TaskId", taskId)
                    .Build();

                // 创建Trigger
                var trigger = TriggerBuilder.Create()
                    .WithIdentity(triggerKey)
                    .StartAt(scheduledTime)
                    .Build();

                // 调度任务
                await _scheduler.ScheduleJob(jobDetail, trigger);
                _logger.Info($"已调度任务[{taskId}],类型:{taskType},计划执行时间:{scheduledTime:yyyy-MM-dd HH:mm:ss}");
            }
            catch (Exception ex)
            {
                _logger.Error($"调度任务[{taskId}]失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 取消任务
        /// </summary>
        public async Task UnscheduleJobAsync(long taskId)
        {
            try
            {
                var triggerKey = new TriggerKey($"trigger_{taskId}", "workflow");
                await _scheduler.UnscheduleJob(triggerKey);
                _logger.Info($"已取消任务[{taskId}]的调度");
            }
            catch (Exception ex)
            {
                _logger.Error($"取消任务[{taskId}]的调度失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 获取任务类型对应的Job类型
        /// </summary>
        /// <param name="taskType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private Type GetJobType(int taskType)
        {
            return taskType switch
            {
                1 => typeof(HbtWorkflowTimeoutReminderJob), // 超时提醒
                2 => typeof(HbtWorkflowAutoExecutionJob), // 自动执行
                3 => typeof(HbtWorkflowTimedTriggerJob), // 定时触发
                4 => typeof(HbtWorkflowDelayedExecutionJob), // 延迟执行
                5 => typeof(HbtWorkflowPeriodicExecutionJob), // 周期执行
                _ => throw new ArgumentException($"不支持的任务类型:{taskType}")
            };
        }
    }

    /// <summary>
    /// 工作流Job工厂
    /// </summary>
    public class HbtWorkflowJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider"></param>
        public HbtWorkflowJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 创建Job实例
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="scheduler"></param>
        /// <returns></returns>
        /// <exception cref="SchedulerException"></exception>
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                // 从服务容器中获取Job实例
                var jobType = bundle.JobDetail.JobType;
                var job = (IJob)ActivatorUtilities.CreateInstance(_serviceProvider, jobType);
                return job;
            }
            catch (Exception ex)
            {
                throw new SchedulerException($"创建Job实例失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 任务完成
        /// </summary>
        /// <param name="job"></param>
        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }

    /// <summary>
    /// 工作流任务扫描Job
    /// </summary>
    public class HbtWorkflowTaskScannerJob : IJob
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHbtLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider">服务提供者</param>
        /// <param name="logger">日志服务</param>
        public HbtWorkflowTaskScannerJob(IServiceProvider serviceProvider, IHbtLogger logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var taskService = scope.ServiceProvider.GetRequiredService<IHbtWorkflowScheduledTaskService>();
                var scheduler = scope.ServiceProvider.GetRequiredService<HbtWorkflowScheduler>();

                // 获取待执行的任务
                var tasks = await taskService.GetPendingTasksAsync();

                foreach (var task in tasks)
                {
                    try
                    {
                        // 调度任务
                        var taskEntity = new Domain.Entities.Workflow.HbtWorkflowScheduledTask
                        {
                            Id = task.WorkflowScheduledTaskId,
                            WorkflowInstanceId = task.WorkflowInstanceId,
                            NodeId = task.NodeId,
                            TaskType = task.TaskType,
                            ScheduledTime = task.ScheduledTime,
                            TaskParameters = task.TaskParameters
                        };

                        await scheduler.ScheduleJobAsync(task.WorkflowScheduledTaskId, task.TaskType, task.ScheduledTime);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"调度任务{task.WorkflowScheduledTaskId}失败", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("扫描待执行任务失败", ex);
                throw;
            }
        }
    }
}