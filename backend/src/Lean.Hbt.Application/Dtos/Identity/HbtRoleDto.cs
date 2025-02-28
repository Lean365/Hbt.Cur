//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtRoleDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 角色数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;
using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.Application.Dtos.Identity
{
    /// <summary>
    /// 角色基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtRoleDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtRoleDto()
        {
            RoleName = string.Empty;
            RoleKey = string.Empty;
            MenuIds = new List<long>();
            DeptIds = new List<long>();
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 角色ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 角色标识
        /// </summary>
        public string RoleKey { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 数据范围
        /// </summary>
        public HbtDataScope DataScope { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public HbtStatus Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 菜单ID列表
        /// </summary>
        public List<long>? MenuIds { get; set; }

        /// <summary>
        /// 部门ID列表
        /// </summary>
        public List<long>? DeptIds { get; set; }
    }

    /// <summary>
    /// 角色查询对象
    /// </summary>
    public class HbtRoleQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string? RoleName { get; set; }

        /// <summary>
        /// 角色标识
        /// </summary>
        public string? RoleKey { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public HbtStatus? Status { get; set; }
    }

    /// <summary>
    /// 角色创建对象
    /// </summary>
    public class HbtRoleCreateDto
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 角色标识
        /// </summary>
        public string RoleKey { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 数据范围
        /// </summary>
        public HbtDataScope DataScope { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public HbtStatus Status { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public long TenantId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 菜单ID列表
        /// </summary>
        public List<long>? MenuIds { get; set; }

        /// <summary>
        /// 部门ID列表
        /// </summary>
        public List<long>? DeptIds { get; set; }
    }

    /// <summary>
    /// 角色更新对象
    /// </summary>
    public class HbtRoleUpdateDto : HbtRoleCreateDto
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public long Id { get; set; }
    }

    /// <summary>
    /// 角色导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtRoleExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtRoleExportDto()
        {
            RoleName = string.Empty;
            RoleKey = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 角色标识
        /// </summary>
        public string RoleKey { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 数据范围
        /// </summary>
        public HbtDataScope DataScope { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public HbtStatus Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 角色状态更新对象
    /// </summary>
    public class HbtRoleStatusDto
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public HbtStatus Status { get; set; }
    }

    /// <summary>
    /// 角色导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtRoleImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtRoleImportDto()
        {
            RoleName = string.Empty;
            RoleKey = string.Empty;
            DataScope = string.Empty;
            Status = string.Empty;
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Required(ErrorMessage = "角色名称不能为空")]
        public string RoleName { get; set; }

        /// <summary>
        /// 角色标识
        /// </summary>
        [Required(ErrorMessage = "角色标识不能为空")]
        public string RoleKey { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 数据范围（1=全部数据权限,2=自定义数据权限,3=本部门数据权限,4=本部门及以下数据权限,5=仅本人数据权限）
        /// </summary>
        [Required(ErrorMessage = "数据范围不能为空")]
        public string DataScope { get; set; }

        /// <summary>
        /// 状态（0=正常,1=停用）
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public string Status { get; set; }
    }

    /// <summary>
    /// 角色导入模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtRoleTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtRoleTemplateDto()
        {
            RoleName = "系统管理员";
            RoleKey = "admin";
            OrderNum = 1;
            DataScope = "全部数据权限";
            Status = "正常";
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Required(ErrorMessage = "角色名称不能为空")]
        public string RoleName { get; set; } = "系统管理员";

        /// <summary>
        /// 角色标识
        /// </summary>
        [Required(ErrorMessage = "角色标识不能为空")]
        public string RoleKey { get; set; } = "admin";

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 1;

        /// <summary>
        /// 数据范围（1=全部数据权限,2=自定义数据权限,3=本部门数据权限,4=本部门及以下数据权限,5=仅本人数据权限）
        /// </summary>
        [Required(ErrorMessage = "数据范围不能为空")]
        public string DataScope { get; set; } = "全部数据权限";

        /// <summary>
        /// 状态（0=正常,1=停用）
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public string Status { get; set; } = "正常";
    }
}