//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtCacheConfigManager.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述    : 缓存配置管理器
//===================================================================

using Microsoft.Extensions.Options;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.Entities.Routine.Core;

namespace Lean.Hbt.Infrastructure.Caching
{
    /// <summary>
    /// 缓存配置管理器
    /// </summary>
    public class HbtCacheConfigManager
    {
        private readonly IOptions<HbtCacheOptions> _options;
        private readonly IHbtRepository<HbtConfig> _configRepository;
        private HbtCacheOptions _currentOptions;
        private readonly object _lock = new object();
        private DateTime _lastUpdateTime;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtCacheConfigManager(
            IOptions<HbtCacheOptions> options,
            IHbtRepository<HbtConfig> configRepository)
        {
            _options = options;
            _configRepository = configRepository;
            _currentOptions = options.Value;
            _lastUpdateTime = DateTime.MinValue;
        }

        /// <summary>
        /// 获取当前配置
        /// </summary>
        public async Task<HbtCacheOptions> GetCurrentOptionsAsync()
        {
            // 检查是否需要更新配置
            if (DateTime.Now - _lastUpdateTime > TimeSpan.FromMinutes(5))
            {
                var shouldUpdate = false;
                lock (_lock)
                {
                    if (DateTime.Now - _lastUpdateTime > TimeSpan.FromMinutes(5))
                    {
                        shouldUpdate = true;
                    }
                }

                if (shouldUpdate)
                {
                    await UpdateOptionsFromDatabaseAsync();
                    lock (_lock)
                    {
                        _lastUpdateTime = DateTime.Now;
                    }
                }
            }
            return _currentOptions;
        }

        /// <summary>
        /// 从数据库更新配置
        /// </summary>
        private async Task UpdateOptionsFromDatabaseAsync()
        {
            try
            {
                // 从数据库获取缓存配置
                var configs = await _configRepository.GetListAsync(x => x.ConfigKey.StartsWith("Cache:"));
                if (configs?.Any() != true)
                {
                    return;
                }

                // 创建新的配置对象
                var newOptions = new HbtCacheOptions
                {
                    Provider = GetConfigValue(configs, "Cache:Provider", _options.Value.Provider),
                    DefaultExpirationMinutes = GetConfigValue(configs, "Cache:DefaultExpiration", _options.Value.DefaultExpirationMinutes),
                    EnableSlidingExpiration = GetConfigValue(configs, "Cache:SlidingExpiration", _options.Value.EnableSlidingExpiration),
                    EnableMultiLevelCache = GetConfigValue(configs, "Cache:MultiLevel", _options.Value.EnableMultiLevelCache),
                    Memory = new HbtMemoryCacheOptions
                    {
                        SizeLimit = GetConfigValue(configs, "Cache:Memory:SizeLimit", _options.Value.Memory.SizeLimit),
                        CompactionThreshold = GetConfigValue(configs, "Cache:Memory:CompactionThreshold", _options.Value.Memory.CompactionThreshold),
                        ExpirationScanFrequency = GetConfigValue(configs, "Cache:Memory:ExpirationScanFrequency", _options.Value.Memory.ExpirationScanFrequency)
                    },
                    Redis = new HbtRedisCacheOptions
                    {
                        InstanceName = GetConfigValue(configs, "Cache:Redis:InstanceName", _options.Value.Redis.InstanceName),
                        DefaultDatabase = GetConfigValue(configs, "Cache:Redis:DefaultDatabase", _options.Value.Redis.DefaultDatabase),
                        EnableCompression = GetConfigValue(configs, "Cache:Redis:EnableCompression", _options.Value.Redis.EnableCompression),
                        CompressionThreshold = GetConfigValue(configs, "Cache:Redis:CompressionThreshold", _options.Value.Redis.CompressionThreshold)
                    }
                };

                _currentOptions = newOptions;
            }
            catch (Exception)
            {
                // 如果数据库读取失败,使用appsettings.json中的配置
                _currentOptions = _options.Value;
            }
        }

        private T GetConfigValue<T>(IEnumerable<HbtConfig> configs, string key, T defaultValue)
        {
            var config = configs.FirstOrDefault(x => x.ConfigKey == key);
            if (config == null)
            {
                return defaultValue;
            }

            try
            {
                return (T)Convert.ChangeType(config.ConfigValue, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}