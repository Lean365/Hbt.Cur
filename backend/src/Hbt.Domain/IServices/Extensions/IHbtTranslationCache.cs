//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtTranslationCache.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 翻译缓存服务接口
//===================================================================

using System.Threading.Tasks;
using System.Collections.Generic;

namespace Hbt.Domain.IServices.Extensions
{
    /// <summary>
    /// 翻译缓存服务接口
    /// </summary>
    public interface IHbtTranslationCache
    {
        /// <summary>
        /// 获取指定语言和键的翻译
        /// </summary>
        /// <param name="langCode">语言代码</param>
        /// <param name="key">翻译键</param>
        /// <returns>翻译值，如果不存在则返回null</returns>
        string? GetTranslation(string langCode, string key);

        /// <summary>
        /// 重新加载翻译数据
        /// </summary>
        Task ReloadAsync();
    }
} 