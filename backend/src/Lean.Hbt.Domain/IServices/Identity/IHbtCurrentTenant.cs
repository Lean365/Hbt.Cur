//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtCurrentTenant.cs
// 创建者 : Lean365
// 创建时间: 2024-03-05 17:30
// 版本号 : V1.0.0
// 描述   : 当前租户接口
//===================================================================

namespace Lean.Hbt.Domain.IServices.Identity;

/// <summary>
/// 当前租户接口
/// </summary>
public interface IHbtCurrentTenant
{
    /// <summary>
    /// 获取当前租户ID
    /// </summary>
    long TenantId { get; }

    /// <summary>
    /// 获取当前租户名称
    /// </summary>
    string TenantName { get; }
}