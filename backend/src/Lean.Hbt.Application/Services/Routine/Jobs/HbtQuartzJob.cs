//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtQuartzJob.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 定时任务执行器
//===================================================================

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.Entities.Routine;
using Lean.Hbt.Domain.Entities.Audit;
using Lean.Hbt.Common.Exceptions;

namespace Lean.Hbt.Application.Services.Routine.Jobs
{
    /// <summary>
    /// 定时任务执行器
    /// </summary>
    [DisallowConcurrentExecution]
    public class HbtQuartzJob : IJob
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<HbtQuartzJob> _logger;
        private readonly IHbtRepository<HbtQuartzTask> _taskRepository;
        private readonly IHbtRepository<HbtQuartzLog> _logRepository;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtQuartzJob(
            IServiceProvider serviceProvider,
            ILogger<HbtQuartzJob> logger,
            IHbtRepository<HbtQuartzTask> taskRepository,
            IHbtRepository<HbtQuartzLog> logRepository,
            IHttpClientFactory httpClientFactory)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _taskRepository = taskRepository;
            _logRepository = logRepository;
            _httpClient = httpClientFactory.CreateClient("QuartzJob");
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        public async Task Execute(IJobExecutionContext context)
        {
            var taskId = context.JobDetail.JobDataMap.GetLong("taskId");
            if (taskId <= 0)
            {
                _logger.LogError("任务ID为空，无法执行任务");
                return;
            }

            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
            {
                _logger.LogError($"未找到ID为{taskId}的任务");
                return;
            }

            var startTime = DateTime.Now;
            var success = false;
            var message = string.Empty;

            try
            {
                switch (task.TaskType)
                {
                    case 1: // 程序集
                        await ExecuteAssemblyAsync(task);
                        break;
                    case 2: // 网络请求
                        await ExecuteHttpRequestAsync(task);
                        break;
                    case 3: // SQL语句
                        await ExecuteSqlAsync(task);
                        break;
                    default:
                        throw new HbtException($"不支持的任务类型: {task.TaskType}");
                }

                success = true;
                message = "执行成功";
            }
            catch (Exception ex)
            {
                success = false;
                message = $"执行失败: {ex.Message}";
                _logger.LogError(ex, $"执行任务[{task.TaskName}]失败");
            }

            var endTime = DateTime.Now;
            var elapsed = (int)(endTime - startTime).TotalMilliseconds;

            // 更新任务信息
            task.TaskLastRunTime = startTime;
            task.TaskExecuteCount++;
            await _taskRepository.UpdateAsync(task);

            // 记录执行日志
            var log = new HbtQuartzLog
            {
                LogTaskId = task.Id,
                LogTaskName = task.TaskName,
                LogGroupName = task.TaskGroupName,
                LogExecuteTime = startTime,
                LogExecuteDuration = elapsed,
                LogStatus = success ? 1 : 0,
                LogErrorInfo = message
            };

            await _logRepository.CreateAsync(log);
        }

        /// <summary>
        /// 执行程序集
        /// </summary>
        private async Task ExecuteAssemblyAsync(HbtQuartzTask task)
        {
            if (string.IsNullOrEmpty(task.TaskAssemblyName) || string.IsNullOrEmpty(task.TaskClassName))
                throw new HbtException("程序集名称或类名为空");

            try
            {
                var assembly = System.Reflection.Assembly.Load(task.TaskAssemblyName);
                var type = assembly.GetType(task.TaskClassName);
                if (type == null)
                    throw new HbtException($"未找到类型: {task.TaskClassName}");

                var instance = ActivatorUtilities.CreateInstance(_serviceProvider, type);
                var method = type.GetMethod("Execute");
                if (method == null)
                    throw new HbtException($"未找到Execute方法");

                if (method.ReturnType == typeof(Task))
                {
                    await (Task)method.Invoke(instance, new object[] { task.TaskExecuteParams });
                }
                else
                {
                    method.Invoke(instance, new object[] { task.TaskExecuteParams });
                }
            }
            catch (Exception ex)
            {
                throw new HbtException($"执行程序集失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 执行HTTP请求
        /// </summary>
        private async Task ExecuteHttpRequestAsync(HbtQuartzTask task)
        {
            if (string.IsNullOrEmpty(task.TaskApiUrl))
                throw new HbtException("API地址为空");

            try
            {
                var method = new HttpMethod(task.TaskRequestMethod ?? "GET");
                var request = new HttpRequestMessage(method, task.TaskApiUrl);

                if (!string.IsNullOrEmpty(task.TaskExecuteParams))
                {
                    if (method == HttpMethod.Get)
                    {
                        // 对于GET请求，参数应该已经包含在URL中
                    }
                    else
                    {
                        request.Content = new StringContent(task.TaskExecuteParams, System.Text.Encoding.UTF8, "application/json");
                    }
                }

                var response = await _httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    throw new HbtException($"HTTP请求失败: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                }
            }
            catch (Exception ex)
            {
                throw new HbtException($"执行HTTP请求失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        private async Task ExecuteSqlAsync(HbtQuartzTask task)
        {
            if (string.IsNullOrEmpty(task.TaskSql))
                throw new HbtException("SQL语句为空");

            try
            {
                var result = await _taskRepository.SqlSugarClient.Ado.ExecuteCommandAsync(task.TaskSql);
                if (result <= 0)
                {
                    throw new HbtException("SQL执行未影响任何行");
                }
            }
            catch (Exception ex)
            {
                throw new HbtException($"执行SQL失败: {ex.Message}", ex);
            }
        }
    }
} 