//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtRole.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 角色实体类
//===================================================================

using Lean.Hbt.Common.Enums;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Identity
{
    /// <summary>
    /// 角色实体
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    [SugarTable("hbt_role", "角色表")]
    [SugarIndex("ix_role_key", nameof(RoleKey), OrderByType.Asc, true)]
    [SugarIndex("ix_tenant_role", nameof(TenantId), OrderByType.Asc, nameof(RoleName), OrderByType.Asc, true)]
    public class HbtRole : HbtBaseEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [SugarColumn(ColumnName = "role_name", ColumnDescription = "角色名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RoleName { get; set; }

        /// <summary>
        /// 角色标识
        /// </summary>
        [SugarColumn(ColumnName = "role_key", ColumnDescription = "角色标识", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RoleKey { get; set; }

        /// <summary>
        /// 角色排序
        /// </summary>
        [SugarColumn(ColumnName = "role_sort", ColumnDescription = "角色排序", ColumnDataType = "int", IsNullable = false)]
        public int RoleSort { get; set; }

        /// <summary>
        /// 数据范围（1：全部数据权限 2：自定数据权限 3：本部门数据权限 4：本部门及以下数据权限 5：仅本人数据权限）
        /// </summary>
        [SugarColumn(ColumnName = "data_scope", ColumnDescription = "数据范围", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public HbtDataScope DataScope { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态（0正常 1停用）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public HbtStatus Status { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long TenantId { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(TenantId))]
        public HbtTenant Tenant { get; set; }

        /// <summary>
        /// 角色菜单关联
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtRoleMenu.RoleId))]
        public List<HbtRoleMenu> RoleMenus { get; set; }

        /// <summary>
        /// 角色部门关联
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtRoleDept.RoleId))]
        public List<HbtRoleDept> RoleDepts { get; set; }

        /// <summary>
        /// 用户角色关联
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtUserRole.RoleId))]
        public List<HbtUserRole> UserRoles { get; set; }
    }
}