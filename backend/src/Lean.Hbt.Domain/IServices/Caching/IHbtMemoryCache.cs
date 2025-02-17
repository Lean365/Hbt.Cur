using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Lean.Hbt.Domain.IServices.Caching
{
    /// <summary>
    /// 内存缓存接口
    /// </summary>
    public interface IHbtMemoryCache
    {
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns>是否设置成功</returns>
        bool Set<T>(string key, T value, TimeSpan? expiry = null);

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns>缓存值，不存在返回默认值</returns>
        T? Get<T>(string key);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>是否移除成功</returns>
        bool Remove(string key);

        /// <summary>
        /// 判断缓存是否存在
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>是否存在</returns>
        bool Exists(string key);

        /// <summary>
        /// 根据模式搜索键
        /// </summary>
        /// <param name="pattern">搜索模式</param>
        /// <returns>匹配的键列表</returns>
        List<string> SearchKeys(string pattern);

        /// <summary>
        /// 获取或添加缓存
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="factory">数据获取工厂</param>
        /// <param name="expiry">过期时间</param>
        /// <returns>缓存值</returns>
        T GetOrAdd<T>(string key, Func<T> factory, TimeSpan? expiry = null);
    }
}