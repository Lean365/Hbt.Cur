//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtRoleMenu.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 11:55
// 版本号 : V0.0.1
// 描述    : 角色菜单关联实体类
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Identity
{
    /// <summary>
    /// 角色菜单关联实体
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    [SugarTable("hbt_id_role_menu", "角色菜单关联表")]
    [SugarIndex("ix_role_menu", nameof(RoleId), OrderByType.Asc, nameof(MenuId), OrderByType.Asc, true)]
    public class HbtRoleMenu : HbtBaseEntity
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [SugarColumn(ColumnName = "role_id", ColumnDescription = "角色ID", ColumnDataType = "bigint", IsNullable = false)]
        public long RoleId { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        [SugarColumn(ColumnName = "menu_id", ColumnDescription = "菜单ID", ColumnDataType = "bigint", IsNullable = false)]
        public long MenuId { get; set; }

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
        /// 角色导航属性
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(RoleId))]
        public HbtRole? Role { get; set; }

        /// <summary>
        /// 菜单导航属性
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(MenuId))]
        public HbtMenu? Menu { get; set; }
    }
}