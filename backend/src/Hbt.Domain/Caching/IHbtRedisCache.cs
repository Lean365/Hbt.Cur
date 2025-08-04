using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hbt.Domain.Caching
{
    /// <summary>
    /// 定义HbtRedis缓存接口
    /// </summary>
    public interface IHbtRedisCache
    {
        /// <summary>
        /// 根据模式搜索键
        /// </summary>
        /// <param name="pattern">搜索模式</param>
        /// <returns>匹配的键列表</returns>
        Task<List<string>> SearchKeysAsync(string pattern);
    }
} 