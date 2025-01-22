//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtRoleDept.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 11:35
// 版本号 : V0.0.1
// 描述    : 角色部门关联实体类
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Identity
{
    /// <summary>
    /// 角色部门关联实体
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    [SugarTable("hbt_role_dept", "角色部门关联表")]
    [SugarIndex("ix_role_dept", nameof(RoleId), OrderByType.Asc, nameof(DeptId), OrderByType.Asc, true)]
    public class HbtRoleDept : HbtBaseEntity
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [SugarColumn(ColumnName = "role_id", ColumnDescription = "角色ID", ColumnDataType = "bigint", IsNullable = false)]
        public long RoleId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [SugarColumn(ColumnName = "dept_id", ColumnDescription = "部门ID", ColumnDataType = "bigint", IsNullable = false)]
        public long DeptId { get; set; }

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
        /// 部门导航属性
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(DeptId))]
        public HbtDept? Dept { get; set; }
    }
}