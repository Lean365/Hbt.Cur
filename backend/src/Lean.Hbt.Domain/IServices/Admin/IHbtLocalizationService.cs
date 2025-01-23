//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtLocalizationService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 本地化服务接口
//===================================================================

namespace Lean.Hbt.Domain.IServices.Admin;

/// <summary>
/// 本地化服务接口
/// </summary>
public interface IHbtLocalizationService
{
    /// <summary>
    /// 获取当前语言的翻译值
    /// </summary>
    /// <param name="key">翻译键</param>
    /// <param name="args">格式化参数</param>
    /// <returns>翻译后的文本</returns>
    string L(string key, params object[] args);

    /// <summary>
    /// 获取指定语言的翻译值
    /// </summary>
    /// <param name="langCode">语言代码</param>
    /// <param name="key">翻译键</param>
    /// <param name="args">格式化参数</param>
    /// <returns>翻译后的文本</returns>
    string L(string langCode, string key, params object[] args);

    /// <summary>
    /// 获取当前语言代码
    /// </summary>
    string CurrentLanguage { get; }

    /// <summary>
    /// 设置当前语言
    /// </summary>
    /// <param name="langCode">语言代码</param>
    void SetLanguage(string langCode);
}