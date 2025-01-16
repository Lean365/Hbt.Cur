//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtRole.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-16 11:30
// 版本号 : V0.0.1
// 描述    : 角色实体
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Domain.Entities.Identity
{
    /// <summary>
    /// 角色实体
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    public class HbtRole : HbtBaseEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        /// <summary>
        /// 角色编码
        /// </summary>
        [Required]
        [StringLength(50)]
        public string RoleCode { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 数据范围（1：全部数据权限 2：自定数据权限 3：本部门数据权限 4：本部门及以下数据权限 5：仅本人数据权限）
        /// </summary>
        public int DataScope { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 删除标志（0代表存在 1代表删除）
        /// </summary>
        public int DelFlag { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public long TenantId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        public string Remark { get; set; }

        /// <summary>
        /// 角色菜单关联
        /// </summary>
        public virtual ICollection<HbtRoleMenu> RoleMenus { get; set; }

        /// <summary>
        /// 角色用户关联
        /// </summary>
        public virtual ICollection<HbtUserRole> UserRoles { get; set; }
    }
} 