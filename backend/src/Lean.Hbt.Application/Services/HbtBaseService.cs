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
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Security.Claims;

namespace Lean.Hbt.Application.Services;

/// <summary>
/// 基础服务实现
/// </summary>
public class HbtBaseService : IHbtLocalizationService, IHbtCurrentUser
{
    /// <summary>
    /// HTTP上下文访问器
    /// </summary>
    protected readonly IHttpContextAccessor _httpContextAccessor;
    /// <summary>
    /// 日志记录器
    /// </summary>
    protected readonly IHbtLogger _logger;
    private CultureInfo _currentCulture = CultureInfo.CurrentCulture;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger">日志服务</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    public HbtBaseService(IHbtLogger logger, IHttpContextAccessor httpContextAccessor)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(httpContextAccessor);

        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    #region IHbtLocalizationService 实现

    /// <summary>
    /// 获取本地化字符串
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="args">参数</param>
    /// <returns>本地化字符串</returns>
    public string L(string key, params object[] args)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        return string.Format(CultureInfo.CurrentCulture, key, args);
    }

    /// <summary>
    /// 获取本地化字符串
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="culture">文化</param>
    /// <param name="args">参数</param>
    /// <returns>本地化字符串</returns>
    public string L(string key, string culture, params object[] args)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        ArgumentException.ThrowIfNullOrEmpty(culture);
        
        try
        {
            return string.Format(new CultureInfo(culture), key, args);
        }
        catch (CultureNotFoundException)
        {
            _logger.Warn($"无效的文化标识符: {culture}，使用默认语言");
            return string.Format(CultureInfo.CurrentCulture, key, args);
        }
    }

    /// <summary>
    /// 获取本地化字符串（异步）
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="args">参数</param>
    /// <returns>本地化字符串</returns>
    public Task<string> GetLocalizedStringAsync(string key, params object[] args)
    {
        return Task.FromResult(L(key, args));
    }

    /// <summary>
    /// 获取当前文化
    /// </summary>
    /// <returns>当前文化</returns>
    public CultureInfo GetCurrentCulture()
    {
        return _currentCulture;
    }

    /// <summary>
    /// 设置当前文化
    /// </summary>
    /// <param name="culture">文化</param>
    public void SetCurrentCulture(CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(culture);
        _currentCulture = culture;
    }

    /// <summary>
    /// 设置语言
    /// </summary>
    /// <param name="language">语言</param>
    public void SetLanguage(string language)
    {
        ArgumentException.ThrowIfNullOrEmpty(language);
        _currentCulture = new CultureInfo(language);
    }

    /// <summary>
    /// 刷新本地化缓存
    /// </summary>
    public Task RefreshLocalizationCacheAsync()
    {
        return Task.CompletedTask;
    }

    #endregion

    #region IHbtCurrentUser 实现

    /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId
    {
        get
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId == null ? 0 : long.Parse(userId);
        }
    }

    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);

    /// <summary>
    /// 昵称
    /// </summary>
    public string? NickName => _httpContextAccessor.HttpContext?.User?.FindFirstValue("NickName");

    /// <summary>
    /// 英文名
    /// </summary>
    public string? EnglishName => _httpContextAccessor.HttpContext?.User?.FindFirstValue("EnglishName");

    /// <summary>
    /// 用户类型
    /// </summary>
    public int UserType
    {
        get
        {
            var userType = _httpContextAccessor.HttpContext?.User?.FindFirstValue("UserType");
            return userType == null ? 0 : int.Parse(userType);
        }
    }

    /// <summary>
    /// 租户ID
    /// </summary>
    public long TenantId
    {
        get
        {
            var tenantId = _httpContextAccessor.HttpContext?.User?.FindFirstValue("TenantId");
            return tenantId == null ? 0 : long.Parse(tenantId);
        }
    }

    /// <summary>
    /// 租户名称
    /// </summary>
    public string? TenantName => _httpContextAccessor.HttpContext?.User?.FindFirstValue("TenantName");

    /// <summary>
    /// 角色列表
    /// </summary>
    public string[] Roles => _httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role).Select(x => x.Value).ToArray() ?? Array.Empty<string>();

    /// <summary>
    /// 权限列表
    /// </summary>
    public string[] Permissions => _httpContextAccessor.HttpContext?.User?.FindAll("Permission").Select(x => x.Value).ToArray() ?? Array.Empty<string>();

    /// <summary>
    /// 是否为管理员
    /// </summary>
    public bool IsAdmin => Roles.Contains("admin");

    /// <summary>
    /// 是否已认证
    /// </summary>
    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    #endregion
}
