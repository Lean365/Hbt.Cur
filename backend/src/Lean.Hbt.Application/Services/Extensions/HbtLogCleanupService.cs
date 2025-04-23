#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLogCleanupService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-19 11:00
// 版本号 : V.0.0.1
// 描述    : 日志清理服务
//===================================================================

using Lean;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Domain.Entities.Core;
using Lean.Hbt.Domain.Entities.Audit;
using Lean.Hbt.Domain.IServices.Extensions;
using Lean.Hbt.Domain.Repositories;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace Lean.Hbt.Application.Services.Extensions
{
    /// <summary>
    /// 日志清理服务
    /// </summary>
    public class HbtLogCleanupService : IHbtLogCleanupService
    {
        private readonly IHbtRepository<HbtOperLog> _operLogRepository;
        private readonly IHbtRepository<HbtLoginLog> _loginLogRepository;
        private readonly IHbtRepository<HbtExceptionLog> _exceptionLogRepository;
        private readonly IHbtRepository<HbtSqlDiffLog> _dbDiffLogRepository;
        private readonly IHbtRepository<HbtConfig> _sysConfigRepository;
        private readonly HbtLogCleanupOptions _defaultOptions;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtLogCleanupService(
            IHbtRepository<HbtOperLog> operLogRepository,
            IHbtRepository<HbtLoginLog> loginLogRepository,
            IHbtRepository<HbtExceptionLog> exceptionLogRepository,
            IHbtRepository<HbtSqlDiffLog> dbDiffLogRepository,
            IHbtRepository<HbtConfig> sysConfigRepository,
            IOptions<HbtLogCleanupOptions> options)
        {
            _operLogRepository = operLogRepository;
            _loginLogRepository = loginLogRepository;
            _exceptionLogRepository = exceptionLogRepository;
            _dbDiffLogRepository = dbDiffLogRepository;
            _sysConfigRepository = sysConfigRepository;
            _defaultOptions = options.Value;
        }

        /// <summary>
        /// 获取日志清理配置
        /// </summary>
        public async Task<HbtLogCleanupOptions> GetConfigAsync()
        {
            var configs = await _sysConfigRepository.GetListAsync();
            var options = new HbtLogCleanupOptions();

            // 从系统配置中获取配置值，如果不存在则使用默认值
            options.Enabled = GetConfigValue(configs, "log.cleanup.enabled", _defaultOptions.Enabled);
            options.Interval = GetConfigValue(configs, "log.cleanup.interval", _defaultOptions.Interval);
            options.OperLogRetentionDays = GetConfigValue(configs, "log.cleanup.oper.retention", _defaultOptions.OperLogRetentionDays);
            options.LoginLogRetentionDays = GetConfigValue(configs, "log.cleanup.login.retention", _defaultOptions.LoginLogRetentionDays);
            options.ExceptionLogRetentionDays = GetConfigValue(configs, "log.cleanup.exception.retention", _defaultOptions.ExceptionLogRetentionDays);
            options.DbDiffLogRetentionDays = GetConfigValue(configs, "log.cleanup.dbdiff.retention", _defaultOptions.DbDiffLogRetentionDays);
            options.BatchSize = GetConfigValue(configs, "log.cleanup.batch.size", _defaultOptions.BatchSize);

            return options;
        }

        /// <summary>
        /// 执行日志清理
        /// </summary>
        public async Task CleanupAsync()
        {
            var options = await GetConfigAsync();
            if (!options.Enabled) return;

            var now = DateTime.Now;
            var batchSize = options.BatchSize;

            // 清理操作日志
            await CleanupOperLogs(now.AddDays(-options.OperLogRetentionDays), batchSize);

            // 清理登录日志
            await CleanupLoginLogs(now.AddDays(-options.LoginLogRetentionDays), batchSize);

            // 清理异常日志
            await CleanupExceptionLogs(now.AddDays(-options.ExceptionLogRetentionDays), batchSize);

            // 清理差异日志
            await CleanupDbDiffLogs(now.AddDays(-options.DbDiffLogRetentionDays), batchSize);
        }

        private T GetConfigValue<T>(List<HbtConfig> configs, string key, T defaultValue)
        {
            var config = configs.FirstOrDefault(c => c.ConfigKey == key);
            if (config == null) return defaultValue;

            try
            {
                return (T)Convert.ChangeType(config.ConfigValue, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }

        private async Task CleanupOperLogs(DateTime beforeTime, int batchSize)
        {
            while (true)
            {
                var logs = await _operLogRepository.GetListAsync(l => l.CreateTime < beforeTime);
                if (!logs.Any()) break;

                await _operLogRepository.DeleteAsync(logs.Take(batchSize).ToList());
            }
        }

        private async Task CleanupLoginLogs(DateTime beforeTime, int batchSize)
        {
            while (true)
            {
                var logs = await _loginLogRepository.GetListAsync(l => l.CreateTime < beforeTime);
                if (!logs.Any()) break;

                await _loginLogRepository.DeleteAsync(logs.Take(batchSize).ToList());
            }
        }

        private async Task CleanupExceptionLogs(DateTime beforeTime, int batchSize)
        {
            while (true)
            {
                var logs = await _exceptionLogRepository.GetListAsync(l => l.CreateTime < beforeTime);
                if (!logs.Any()) break;

                await _exceptionLogRepository.DeleteAsync(logs.Take(batchSize).ToList());
            }
        }

        private async Task CleanupDbDiffLogs(DateTime beforeTime, int batchSize)
        {
            while (true)
            {
                var logs = await _dbDiffLogRepository.GetListAsync(l => l.CreateTime < beforeTime);
                if (!logs.Any()) break;

                await _dbDiffLogRepository.DeleteAsync(logs.Take(batchSize).ToList());
            }
        }
    }
} 
