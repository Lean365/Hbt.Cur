#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtBaseService.cs
// 创建者 : Claude
// 创建时间: 2024-03-21
// 版本号 : V0.0.1
// 描述   : 基础服务实现
//===================================================================

using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Domain.IServices.Extensions;
using Lean.Hbt.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace Lean.Hbt.Application.Services;

/// <summary>
/// 基础服务实现
/// </summary>
public class HbtBaseService
{
    /// <summary>
    /// HTTP上下文访问器
    /// </summary>
    protected readonly IHttpContextAccessor _httpContextAccessor;
    /// <summary>
    /// 日志记录器
    /// </summary>
    protected readonly IHbtLogger _logger;
    /// <summary>
    /// 当前用户服务
    /// </summary>
    protected readonly IHbtCurrentUser _currentUser;
    /// <summary>
    /// 本地化服务
    /// </summary>
    protected readonly IHbtLocalizationService _localization;
    /// <summary>
    /// 当前租户服务
    /// </summary>
    protected readonly IHbtCurrentTenant _currentTenant;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger">日志服务</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="currentTenant">当前租户服务</param>
    /// <param name="localization">本地化服务</param>
    public HbtBaseService(
        IHbtLogger logger, 
        IHttpContextAccessor httpContextAccessor,
        IHbtCurrentUser currentUser,
        IHbtCurrentTenant currentTenant,
        IHbtLocalizationService localization)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(httpContextAccessor);
        ArgumentNullException.ThrowIfNull(currentUser);
        ArgumentNullException.ThrowIfNull(currentTenant);
        ArgumentNullException.ThrowIfNull(localization);

        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _currentUser = currentUser;
        _currentTenant = currentTenant;
        _localization = localization;
    }

    /// <summary>
    /// 获取当前租户ID
    /// </summary>
    protected long CurrentTenantId => _currentTenant.TenantId;

    /// <summary>
    /// 获取当前租户名称
    /// </summary>
    protected string CurrentTenantName => _currentTenant.TenantName;

    /// <summary>
    /// 验证当前租户ID是否匹配
    /// </summary>
    /// <param name="tenantId">要验证的租户ID</param>
    /// <exception cref="HbtException">当租户ID不匹配时抛出异常</exception>
    protected void ValidateTenantId(long tenantId)
    {
        if (CurrentTenantId != tenantId)
            throw new HbtException(L("Identity.Tenant.Mismatch"), "403");
    }

    /// <summary>
    /// 获取本地化字符串
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="args">参数</param>
    /// <returns>本地化字符串</returns>
    protected string L(string key, params object[] args)
    {
        return _localization.L(key, args);
    }

    /// <summary>
    /// 获取本地化字符串
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="culture">文化</param>
    /// <param name="args">参数</param>
    /// <returns>本地化字符串</returns>
    protected string L(string key, string culture, params object[] args)
    {
        return _localization.L(key, culture, args);
    }

    /// <summary>
    /// 获取本地化字符串（异步）
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="args">参数</param>
    /// <returns>本地化字符串</returns>
    protected Task<string> GetLocalizedStringAsync(string key, params object[] args)
    {
        return _localization.GetLocalizedStringAsync(key, args);
    }

    /// <summary>
    /// 获取当前文化
    /// </summary>
    /// <returns>当前文化</returns>
    protected CultureInfo GetCurrentCulture()
    {
        return _localization.GetCurrentCulture();
    }

    /// <summary>
    /// 设置当前文化
    /// </summary>
    /// <param name="culture">文化</param>
    protected void SetCurrentCulture(CultureInfo culture)
    {
        _localization.SetCurrentCulture(culture);
    }

    /// <summary>
    /// 设置语言
    /// </summary>
    /// <param name="language">语言</param>
    protected void SetLanguage(string language)
    {
        _localization.SetLanguage(language);
    }

    /// <summary>
    /// 刷新本地化缓存
    /// </summary>
    protected Task RefreshLocalizationCacheAsync()
    {
        return _localization.RefreshLocalizationCacheAsync();
    }
}
