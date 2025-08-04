//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTranslationCache.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 翻译缓存服务实现
//===================================================================

using System.Collections.Concurrent;
using Hbt.Cur.Domain.Entities.Routine.Core;

namespace Hbt.Cur.Infrastructure.Services
{
    /// <summary>
    /// 翻译缓存服务实现
    /// </summary>
    public class HbtTranslationCache : IHbtTranslationCache
    {
        private readonly IHbtRepository<HbtTranslation> _translationRepository;

        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly IHbtLogger _logger;

        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, string>> _translations;
        private readonly ConcurrentHashSet<string> _supportedLanguages;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="translationRepository">翻译仓储</param>
        /// <param name="logger">日志服务</param>
        public HbtTranslationCache(
            IHbtRepository<HbtTranslation> translationRepository,
            IHbtLogger logger
)
        {
            _translationRepository = translationRepository;
            _logger = logger;
            _translations = new ConcurrentDictionary<string, ConcurrentDictionary<string, string>>();
            _supportedLanguages = new ConcurrentHashSet<string>();
            InitializeTranslations().GetAwaiter().GetResult();
        }

        /// <summary>
        /// 获取指定语言和键的翻译
        /// </summary>
        public string? GetTranslation(string langCode, string key)
        {
            try
            {
                if (_translations.TryGetValue(langCode, out var langTranslations) &&
                    langTranslations.TryGetValue(key, out var translation))
                {
                    return translation;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.Error("获取翻译失败: {LangCode}, {Key}", langCode, key, ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 初始化翻译数据
        /// </summary>
        private async Task InitializeTranslations()
        {
            try
            {
                var translations = await _translationRepository.GetListAsync(t => t.Status == 0);
                foreach (var translation in translations)
                {
                    var langDict = _translations.GetOrAdd(translation.LangCode,
                        new ConcurrentDictionary<string, string>());
                    langDict.TryAdd(translation.TransKey, translation.TransValue);
                    _supportedLanguages.Add(translation.LangCode);
                }
                _logger.Info("翻译数据初始化成功，共加载 {Count} 条记录", translations.Count);
            }
            catch (Exception ex)
            {
                _logger.Error("初始化翻译数据失败", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 重新加载翻译数据
        /// </summary>
        public async Task ReloadAsync()
        {
            try
            {
                _translations.Clear();
                _supportedLanguages.Clear();
                await InitializeTranslations();
                _logger.Info("翻译数据重新加载成功");
            }
            catch (Exception ex)
            {
                _logger.Error("重新加载翻译数据失败", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 获取支持的语言列表
        /// </summary>
        public Task<IEnumerable<string>> GetSupportedLanguagesAsync()
        {
            try
            {
                return Task.FromResult<IEnumerable<string>>(_supportedLanguages.ToArray());
            }
            catch (Exception ex)
            {
                _logger.Error("获取支持的语言列表失败", ex.Message);
                throw;
            }
        }
    }

    /// <summary>
    /// 线程安全的HashSet实现
    /// </summary>
    public class ConcurrentHashSet<T> : IEnumerable<T>
    {
        private readonly ConcurrentDictionary<T, byte> _dictionary;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ConcurrentHashSet()
        {
            _dictionary = new ConcurrentDictionary<T, byte>();
        }

        /// <summary>
        /// 添加元素
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(T item)
        {
            return _dictionary.TryAdd(item, 0);
        }

        /// <summary>
        /// 清空集合
        /// </summary>
        public void Clear()
        {
            _dictionary.Clear();
        }

        /// <summary>
        /// 判断是否包含元素
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return _dictionary.ContainsKey(item);
        }

        /// <summary>
        /// 获取集合的枚举器
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            return _dictionary.TryRemove(item, out _);
        }

        /// <summary>
        /// 获取集合的枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _dictionary.Keys.GetEnumerator();
        }

        /// <summary>
        /// 获取集合的枚举器
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}