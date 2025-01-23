//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLocalizationService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 本地化服务实现
//===================================================================

using System.Collections.Concurrent;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.IServices.Admin;
using Lean.Hbt.Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Infrastructure.Services;

/// <summary>
/// 本地化服务实现
/// </summary>
public class HbtLocalizationService : IHbtLocalizationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHbtRepository<HbtTranslation> _translationRepository;
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, string>> _translations;
    private const string DefaultLanguage = "zh-CN";
    private const string LanguageHeader = "Accept-Language";

    public HbtLocalizationService(
        IHttpContextAccessor httpContextAccessor,
        IHbtRepository<HbtTranslation> translationRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _translationRepository = translationRepository;
        _translations = new ConcurrentDictionary<string, ConcurrentDictionary<string, string>>();
        InitializeTranslations().GetAwaiter().GetResult();
    }

    /// <summary>
    /// 获取当前语言的翻译值
    /// </summary>
    public string L(string key, params object[] args)
    {
        return L(CurrentLanguage, key, args);
    }

    /// <summary>
    /// 获取指定语言的翻译值
    /// </summary>
    public string L(string langCode, string key, params object[] args)
    {
        if (string.IsNullOrEmpty(key))
            return string.Empty;

        // 1. 尝试获取指定语言的翻译
        if (_translations.TryGetValue(langCode, out var langTranslations) &&
            langTranslations.TryGetValue(key, out var translation))
        {
            return args.Length > 0 ? string.Format(translation, args) : translation;
        }

        // 2. 如果找不到，尝试使用默认语言
        if (langCode != DefaultLanguage &&
            _translations.TryGetValue(DefaultLanguage, out var defaultTranslations) &&
            defaultTranslations.TryGetValue(key, out var defaultTranslation))
        {
            return args.Length > 0 ? string.Format(defaultTranslation, args) : defaultTranslation;
        }

        // 3. 如果还是找不到，返回键名
        return key;
    }

    /// <summary>
    /// 获取当前语言代码
    /// </summary>
    public string CurrentLanguage
    {
        get
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
                return DefaultLanguage;

            // 1. 从请求头获取
            var langHeader = context.Request.Headers[LanguageHeader].ToString();
            if (!string.IsNullOrEmpty(langHeader))
                return langHeader.Split(',')[0];

            // 2. 从Cookie获取
            if (context.Request.Cookies.TryGetValue("lang", out var langCookie))
                return langCookie;

            return DefaultLanguage;
        }
    }

    /// <summary>
    /// 设置当前语言
    /// </summary>
    public void SetLanguage(string langCode)
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null)
            return;

        context.Response.Cookies.Append("lang", langCode, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddYears(1)
        });
    }

    /// <summary>
    /// 初始化翻译数据
    /// </summary>
    private async Task InitializeTranslations()
    {
        var translations = await _translationRepository.GetListAsync(t => t.Status == HbtStatus.Normal);
        foreach (var translation in translations)
        {
            var langDict = _translations.GetOrAdd(translation.LangCode,
                new ConcurrentDictionary<string, string>());
            langDict.TryAdd(translation.TransKey, translation.TransValue);
        }
    }

    /// <summary>
    /// 重新加载翻译数据
    /// </summary>
    public async Task ReloadTranslationsAsync()
    {
        _translations.Clear();
        await InitializeTranslations();
    }
}