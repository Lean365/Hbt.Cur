//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : ITenantContext.cs
// 创建者 : Lean365
// 创建时间: 2024-02-18 02:30
// 版本号 : V1.0.0
// 描述    : 租户上下文接口
//===================================================================

namespace Lean.Hbt.Domain.IServices.Tenant
{
    /// <summary>
    /// 租户上下文接口
    /// </summary>
    public interface ITenantContext
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        long TenantId { get; }

        /// <summary>
        /// 租户名称
        /// </summary>
        string TenantName { get; }
    }
} 