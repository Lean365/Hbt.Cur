//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtCacheFactory.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-17 16:30
// 版本号 : V0.0.1
// 描述    : 缓存工厂实现
//===================================================================

using Hbt.Domain.IServices.Caching;

namespace Hbt.Infrastructure.Caching
{
    /// <summary>
    /// 缓存工厂实现
    /// </summary>
    public class HbtCacheFactory : IHbtCacheFactory
    {
        /// <summary>
        /// 内存缓存
        /// </summary>
        public IHbtMemoryCache Memory { get; }

        /// <summary>
        /// Redis缓存
        /// </summary>
        public IHbtRedisCache Redis { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtCacheFactory(IHbtMemoryCache memory, IHbtRedisCache redis)
        {
            Memory = memory;
            Redis = redis;
        }
    }
} 