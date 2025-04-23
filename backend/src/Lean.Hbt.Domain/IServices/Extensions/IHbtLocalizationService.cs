//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtLocalizationService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 本地化服务接口
//===================================================================

using System.Globalization;

namespace Lean.Hbt.Domain.IServices.Extensions;

/// <summary>
/// 本地化服务接口
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
public interface IHbtLocalizationService
{
    /// <summary>
    /// 获取本地化文本
    /// </summary>
    /// <param name="key">本地化键</param>
    /// <param name="args">格式化参数</param>
    /// <returns>本地化文本</returns>
    Task<string> GetLocalizedStringAsync(string key, params object[] args);

    /// <summary>
    /// 获取当前语言文化
    /// </summary>
    /// <returns>当前语言文化</returns>
    CultureInfo GetCurrentCulture();

    /// <summary>
    /// 设置当前语言文化
    /// </summary>
    /// <param name="culture">语言文化</param>
    void SetCurrentCulture(CultureInfo culture);

    /// <summary>
    /// 设置当前语言
    /// </summary>
    /// <param name="langCode">语言代码</param>
    void SetLanguage(string langCode);

    /// <summary>
    /// 从数据库刷新本地化缓存
    /// </summary>
    Task RefreshLocalizationCacheAsync();

    /// <summary>
    /// 获取当前语言的翻译值
    /// </summary>
    string L(string key, params object[] args);

    /// <summary>
    /// 获取指定语言的翻译值
    /// </summary>
    string L(string langCode, string key, params object[] args);
}