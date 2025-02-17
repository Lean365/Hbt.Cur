//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtMemoryCache.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-17 16:30
// 版本号 : V0.0.1
// 描述    : 内存缓存实现
//===================================================================

using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Domain.IServices.Caching;

namespace Lean.Hbt.Infrastructure.Caching
{
    /// <summary>
    /// 内存缓存实现
    /// </summary>
    public class HbtMemoryCache : IHbtMemoryCache
    {
        private readonly IMemoryCache _cache;
        private readonly ConcurrentDictionary<string, DateTime?> _keys;
        private readonly MemoryCacheEntryOptions _defaultOptions;
        private readonly HbtCacheConfigManager _configManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMemoryCache(
            IMemoryCache cache, 
            HbtCacheConfigManager configManager)
        {
            _cache = cache;
            _configManager = configManager;
            _keys = new ConcurrentDictionary<string, DateTime?>();
            _defaultOptions = new MemoryCacheEntryOptions();
            InitializeDefaultOptions();
        }

        private async void InitializeDefaultOptions()
        {
            var options = await _configManager.GetCurrentOptionsAsync();
            _defaultOptions.SlidingExpiration = options.EnableSlidingExpiration 
                ? TimeSpan.FromMinutes(options.DefaultExpirationMinutes)
                : null;
            _defaultOptions.Size = options.Memory.SizeLimit;
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        public bool Set<T>(string key, T value, TimeSpan? expiry = null)
        {
            try
            {
                var options = new MemoryCacheEntryOptions();
                if (expiry.HasValue)
                {
                    options.AbsoluteExpirationRelativeToNow = expiry;
                }
                else
                {
                    options = _defaultOptions;
                }

                options.RegisterPostEvictionCallback(OnPostEviction);
                _cache.Set(key, value, options);
                _keys.TryAdd(key, expiry.HasValue ? DateTime.Now.Add(expiry.Value) : null);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        public T? Get<T>(string key)
        {
            return _cache.TryGetValue(key, out object? value) ? (T?)value : default;
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        public bool Remove(string key)
        {
            try
            {
                _cache.Remove(key);
                _keys.TryRemove(key, out _);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断缓存是否存在
        /// </summary>
        public bool Exists(string key)
        {
            return _cache.TryGetValue(key, out _);
        }

        /// <summary>
        /// 根据模式搜索键
        /// </summary>
        public List<string> SearchKeys(string pattern)
        {
            var regex = new Regex(pattern.Replace("*", ".*"));
            return _keys.Keys.Where(k => regex.IsMatch(k)).ToList();
        }

        /// <summary>
        /// 获取或添加缓存
        /// </summary>
        public T GetOrAdd<T>(string key, Func<T> factory, TimeSpan? expiry = null)
        {
            if (_cache.TryGetValue(key, out object? value))
            {
                return (T)value;
            }

            var result = factory();
            Set(key, result, expiry);
            return result;
        }

        private void OnPostEviction(object key, object? value, EvictionReason reason, object? state)
        {
            if (key is string strKey)
            {
                _keys.TryRemove(strKey, out _);
            }
        }
    }
} 
