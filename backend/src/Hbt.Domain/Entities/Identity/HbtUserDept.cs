//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtUserDept.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 11:40
// 版本号 : V0.0.1
// 描述    : 用户部门关联实体类
//===================================================================

using SqlSugar;

namespace Hbt.Domain.Entities.Identity
{
    /// <summary>
    /// 用户部门关联实体
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    [SugarTable("hbt_identity_user_dept", TableDescription = "用户部门关联表")]
    [SugarIndex("ix_user_dept", nameof(UserId), OrderByType.Asc, nameof(DeptId), OrderByType.Asc, true)]
    public class HbtUserDept : HbtBaseEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long UserId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [SugarColumn(ColumnName = "dept_id", ColumnDescription = "部门ID", ColumnDataType = "bigint", IsNullable = false)]
        public long DeptId { get; set; }

        /// <summary>
        /// 用户导航属性
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(UserId))]
        public HbtUser? User { get; set; }

        /// <summary>
        /// 部门导航属性
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(DeptId))]
        public HbtDept? Dept { get; set; }
    }
}