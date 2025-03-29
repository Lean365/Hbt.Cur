//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : ICurrentUser.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 当前用户接口
//===================================================================

namespace Lean.Hbt.Domain.Identity
{
    /// <summary>
    /// 当前用户接口
    /// </summary>
    public interface IHbtCurrentUser
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
        /// 租户ID
        /// </summary>
        long TenantId { get; }

        /// <summary>
        /// 是否已认证
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// 用户类型（1：超级管理员，2：普通用户）
        /// </summary>
        int UserType { get; }
    }
} 