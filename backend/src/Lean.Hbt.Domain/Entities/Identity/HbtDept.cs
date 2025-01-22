#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDept.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 部门实体类
//===================================================================

using Lean.Hbt.Common.Enums;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Identity
{
    /// <summary>
    /// 部门实体
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    [SugarTable("hbt_dept", "部门表")]
    [SugarIndex("ix_dept_name", nameof(DeptName), OrderByType.Asc, true)]
    [SugarIndex("ix_tenant_dept", nameof(TenantId), OrderByType.Asc, nameof(DeptName), OrderByType.Asc, true)]
    public class HbtDept : HbtBaseEntity
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        [SugarColumn(ColumnName = "dept_name", ColumnDescription = "部门名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string DeptName { get; set; } = null!;

        /// <summary>
        /// 父部门ID
        /// </summary>
        [SugarColumn(ColumnName = "parent_id", ColumnDescription = "父部门ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ParentId { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "显示顺序", ColumnDataType = "int", IsNullable = false)]
        public int OrderNum { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        [SugarColumn(ColumnName = "leader", ColumnDescription = "负责人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Leader { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [SugarColumn(ColumnName = "phone", ColumnDescription = "联系电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [SugarColumn(ColumnName = "email", ColumnDescription = "邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Email { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态（0正常 1停用）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public HbtStatus Status { get; set; } = HbtStatus.Normal;

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
        /// 角色部门关联
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtRoleDept.DeptId))]
        public List<HbtRoleDept>? RoleDepts { get; set; }

        /// <summary>
        /// 用户部门关联
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtUserDept.DeptId))]
        public List<HbtUserDept>? UserDepts { get; set; }

        /// <summary>
        /// 父部门导航属性
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ParentId))]
        public HbtDept? Parent { get; set; }

        /// <summary>
        /// 子部门导航属性
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(ParentId))]
        public List<HbtDept>? Children { get; set; }
    }
}