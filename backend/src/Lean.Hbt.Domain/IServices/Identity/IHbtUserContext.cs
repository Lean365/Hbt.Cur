//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtUserContext.cs
// 创建者 : Lean365
// 创建时间: 2024-03-05 17:00
// 版本号 : V1.0.0
// 描述   : 用户上下文接口
//===================================================================

namespace Lean.Hbt.Domain.IServices.Identity;

/// <summary>
/// 用户上下文接口
/// </summary>
public interface IHbtUserContext
{
    /// <summary>
    /// 用户ID
    /// </summary>
    long UserId { get; }

    /// <summary>
    /// 用户名
    /// </summary>
    string UserName { get; }

    /// <summary>
    /// 昵称
    /// </summary>
    string NickName { get; }

    /// <summary>
    /// 租户ID
    /// </summary>
    long TenantId { get; }

    /// <summary>
    /// 租户名称
    /// </summary>
    string TenantName { get; }

    /// <summary>
    /// 用户角色列表
    /// </summary>
    string[] Roles { get; }

    /// <summary>
    /// 用户权限列表
    /// </summary>
    string[] Permissions { get; }

    /// <summary>
    /// 是否已认证
    /// </summary>
    bool IsAuthenticated { get; }

    /// <summary>
    /// 是否为超级管理员
    /// </summary>
    bool IsAdmin { get; }
}