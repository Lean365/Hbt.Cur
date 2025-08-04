//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtCacheFactory.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 16:30
// 版本号 : V0.0.1
// 描述    : 缓存工厂接口
//===================================================================

namespace Hbt.Domain.IServices.Caching
{
    /// <summary>
    /// 缓存工厂接口
    /// </summary>
    public interface IHbtCacheFactory
    {
        /// <summary>
        /// 获取内存缓存
        /// </summary>
        IHbtMemoryCache Memory { get; }

        /// <summary>
        /// 获取Redis缓存
        /// </summary>
        IHbtRedisCache Redis { get; }
    }
}