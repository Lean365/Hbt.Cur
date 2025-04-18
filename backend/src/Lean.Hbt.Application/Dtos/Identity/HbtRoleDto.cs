//===================================================================
// 项目名 : Lean.Hbt.Application
// 文件名 : HbtRoleDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 11:30
// 版本号 : V0.0.1
// 描述   : 角色数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

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
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long RoleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; } = string.Empty;

        /// <summary>
        /// 角色标识
        /// </summary>
        public string RoleKey { get; set; } = string.Empty;

        /// <summary>
        /// 排序号
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
        /// 租户ID
        /// </summary>
        public long TenantId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

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
    /// 角色查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtRoleQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [MaxLength(50, ErrorMessage = "角色名称长度不能超过50个字符")]
        public string? RoleName { get; set; }

        /// <summary>
        /// 角色标识
        /// </summary>
        [MaxLength(100, ErrorMessage = "角色标识长度不能超过100个字符")]
        public string? RoleKey { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int? Status { get; set; }
    }

    /// <summary>
    /// 角色创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtRoleCreateDto
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [Required(ErrorMessage = "角色名称不能为空")]
        [MaxLength(50, ErrorMessage = "角色名称长度不能超过50个字符")]
        public string RoleName { get; set; } = string.Empty;

        /// <summary>
        /// 角色标识
        /// </summary>
        [Required(ErrorMessage = "角色标识不能为空")]
        [MaxLength(100, ErrorMessage = "角色标识长度不能超过100个字符")]
        public string RoleKey { get; set; } = string.Empty;

        /// <summary>
        /// 排序号
        /// </summary>
        [Required(ErrorMessage = "排序号不能为空")]
        [Range(0, 9999, ErrorMessage = "排序号必须在0-9999之间")]
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
        /// 租户ID
        /// </summary>
        [Required(ErrorMessage = "租户ID不能为空")]
        [Range(0, 9999, ErrorMessage = "租户ID必须在0-9999之间")]
        public long TenantId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "备注长度不能超过500个字符")]
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
    /// 角色更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtRoleUpdateDto : HbtRoleCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "角色ID不能为空")]
        public long RoleId { get; set; }
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
        public string RoleName { get; set; } = string.Empty;

        /// <summary>
        /// 角色标识
        /// </summary>
        public string RoleKey { get; set; } = string.Empty;

        /// <summary>
        /// 排序号
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
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 角色状态DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtRoleStatusDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "角色ID不能为空")]
        public long RoleId { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public int Status { get; set; }
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
            Status = 0;
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Required(ErrorMessage = "角色名称不能为空")]
        public string RoleName { get; set; } = string.Empty;

        /// <summary>
        /// 角色标识
        /// </summary>
        [Required(ErrorMessage = "角色标识不能为空")]
        public string RoleKey { get; set; } = string.Empty;

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 数据范围（1：全部数据权限 2：自定数据权限 3：本部门数据权限 4：本部门及以下数据权限 5：仅本人数据权限）
        /// </summary>
        [Required(ErrorMessage = "数据范围不能为空")]
        public string DataScope { get; set; } = string.Empty;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public int Status { get; set; } = 0;
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
            RoleKey = "Hbt365";
            OrderNum = 1;
            DataScope = 0;
            Status = 0;
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
        public string RoleKey { get; set; } = "Hbt365";

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 1;

        /// <summary>
        /// 数据范围（1：全部数据权限 2：自定数据权限 3：本部门数据权限 4：本部门及以下数据权限 5：仅本人数据权限）
        /// </summary>
        [Required(ErrorMessage = "数据范围不能为空")]
        public int DataScope { get; set; } = 0;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public int Status { get; set; } = 0;
    }
}