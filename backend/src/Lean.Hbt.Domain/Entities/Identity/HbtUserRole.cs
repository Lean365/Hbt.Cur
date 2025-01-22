//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtUserRole.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 12:00
// 版本号 : V0.0.1
// 描述    : 用户角色关联实体类
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Identity
{
    /// <summary>
    /// 用户角色关联实体
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    [SugarTable("hbt_user_role", "用户角色关联表")]
    [SugarIndex("ix_user_role", nameof(UserId), OrderByType.Asc, nameof(RoleId), OrderByType.Asc, true)]
    public class HbtUserRole : HbtBaseEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [SugarColumn(ColumnName = "role_id", ColumnDescription = "角色ID", ColumnDataType = "bigint", IsNullable = false)]
        public long RoleId { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long TenantId { get; set; }

        /// <summary>
        /// 租户导航属性
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(TenantId))]
        public HbtTenant? Tenant { get; set; }

        /// <summary>
        /// 用户导航属性
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(UserId))]
        public HbtUser? User { get; set; }

        /// <summary>
        /// 角色导航属性
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(RoleId))]
        public HbtRole? Role { get; set; }
    }
}