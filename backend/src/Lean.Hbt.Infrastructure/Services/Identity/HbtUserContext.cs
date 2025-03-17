//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtUserContext.cs
// 创建者 : Lean365
// 创建时间: 2024-03-05 17:00
// 版本号 : V1.0.0
// 描述   : 用户上下文实现
//===================================================================

using Microsoft.AspNetCore.Http;
using Lean.Hbt.Domain.IServices.Identity;

namespace Lean.Hbt.Infrastructure.Services.Identity;

/// <summary>
/// 用户上下文实现
/// </summary>
public class HbtUserContext : IHbtUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    public HbtUserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// 获取用户ID
    /// </summary>
    public long UserId => long.Parse(_httpContextAccessor.HttpContext?.User.FindFirst("user_id")?.Value ?? "0");

    /// <summary>
    /// 获取用户名
    /// </summary>
    public string UserName => _httpContextAccessor.HttpContext?.User.Identity?.Name ?? string.Empty;

    /// <summary>
    /// 获取用户昵称
    /// </summary>
    public string NickName => _httpContextAccessor.HttpContext?.User.FindFirst("nick_name")?.Value ?? string.Empty;

    /// <summary>
    /// 获取租户ID
    /// </summary>
    public long TenantId => long.Parse(_httpContextAccessor.HttpContext?.User.FindFirst("tenant_id")?.Value ?? "0");

    /// <summary>
    /// 获取租户名称
    /// </summary>
    public string TenantName => _httpContextAccessor.HttpContext?.User.FindFirst("tenant_name")?.Value ?? string.Empty;

    /// <summary>
    /// 获取用户角色列表
    /// </summary>
    public string[] Roles => _httpContextAccessor.HttpContext?.User.FindAll("role").Select(c => c.Value).ToArray() ?? Array.Empty<string>();

    /// <summary>
    /// 获取用户权限列表
    /// </summary>
    public string[] Permissions => _httpContextAccessor.HttpContext?.User.FindAll("permissions").Select(c => c.Value).ToArray() ?? Array.Empty<string>();

    /// <summary>
    /// 判断用户是否已认证
    /// </summary>
    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

    /// <summary>
    /// 判断是否为超级管理员
    /// </summary>
    public bool IsAdmin => Roles.Contains("admin");
}