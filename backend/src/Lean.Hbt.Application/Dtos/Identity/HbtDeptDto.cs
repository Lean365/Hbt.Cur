//===================================================================
// 项目名 : Lean.Hbt.Application
// 文件名 : HbtDeptDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 11:30
// 版本号 : V0.0.1
// 描述   : 部门数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.Identity
{
    /// <summary>
    /// 部门基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtDeptDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDeptDto()
        {
            DeptName = string.Empty;
            Leader = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            Children = new List<HbtDeptDto>();
            Remark = string.Empty;
            CreateBy = string.Empty;
            UpdateBy = string.Empty;
            DeleteBy = string.Empty;
           
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long DeptId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [MaxLength(50, ErrorMessage = "部门名称长度不能超过50个字符")]
        public string DeptName { get; set; } = string.Empty;

        /// <summary>
        /// 父部门ID
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Range(0, 9999, ErrorMessage = "显示顺序必须在0-9999之间")]
        public int OrderNum { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        [MaxLength(50, ErrorMessage = "负责人长度不能超过50个字符")]
        public string Leader { get; set; } = string.Empty;

        /// <summary>
        /// 联系电话
        /// </summary>
        [MaxLength(20, ErrorMessage = "联系电话长度不能超过20个字符")]
        [RegularExpression(@"^1[3-9]\d{9}$", ErrorMessage = "联系电话格式不正确")]
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(100, ErrorMessage = "邮箱长度不能超过100个字符")]
        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 用户数
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        /// 部门状态（0正常 1停用）
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime DeleteTime { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string DeleteBy { get; set; } = string.Empty;

        /// <summary>
        /// 子部门列表
        /// </summary>
        public List<HbtDeptDto> Children { get; set; }


    }

    /// <summary>
    /// 部门查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtDeptQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        [MaxLength(50, ErrorMessage = "部门名称长度不能超过50个字符")]
        public string? DeptName { get; set; }

        /// <summary>
        /// 部门状态（0正常 1停用）
        /// </summary>
        public int? Status { get; set; }
    }

    /// <summary>
    /// 部门创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtDeptCreateDto
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        [Required(ErrorMessage = "部门名称不能为空")]
        [MaxLength(50, ErrorMessage = "部门名称长度不能超过50个字符")]
        public string DeptName { get; set; } = string.Empty;

        /// <summary>
        /// 父部门ID
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Required(ErrorMessage = "显示顺序不能为空")]
        [Range(0, 9999, ErrorMessage = "显示顺序必须在0-9999之间")]
        public int OrderNum { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        [MaxLength(20, ErrorMessage = "负责人长度不能超过20个字符")]
        public string? Leader { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [MaxLength(11, ErrorMessage = "联系电话长度不能超过11个字符")]
        [RegularExpression(@"^1[3-9]\d{9}$", ErrorMessage = "联系电话格式不正确")]
        public string? Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(50, ErrorMessage = "邮箱长度不能超过50个字符")]
        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        public string? Email { get; set; }

        /// <summary>
        /// 用户数
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        /// 部门状态（0正常 1停用）
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
    }

    /// <summary>
    /// 部门更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtDeptUpdateDto : HbtDeptCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "部门ID不能为空")]
        [AdaptMember("Id")]
        public long DeptId { get; set; }
    }

    /// <summary>
    /// 部门导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtDeptExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDeptExportDto()
        {
            DeptName = string.Empty;
            Leader = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 父部门ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string Leader { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 用户数
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        /// 部门状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime DeleteTime { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string DeleteBy { get; set; } = string.Empty;
    }

    /// <summary>
    /// 部门状态更新对象
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtDeptStatusDto
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        [Required(ErrorMessage = "部门ID不能为空")]
        public long DeptId { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public int Status { get; set; }
    }

    /// <summary>
    /// 部门导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtDeptImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDeptImportDto()
        {
            DeptName = string.Empty;
            ParentDeptName = string.Empty;
            Leader = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            Status = 0;
        }

        /// <summary>
        /// 部门名称
        /// </summary>
        [Required(ErrorMessage = "部门名称不能为空")]
        [MaxLength(50, ErrorMessage = "部门名称长度不能超过50个字符")]
        public string DeptName { get; set; }

        /// <summary>
        /// 父部门名称
        /// </summary>
        public string ParentDeptName { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Range(0, 9999, ErrorMessage = "显示顺序必须在0-9999之间")]
        public int OrderNum { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        [MaxLength(50, ErrorMessage = "负责人长度不能超过50个字符")]
        public string Leader { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [MaxLength(20, ErrorMessage = "联系电话长度不能超过20个字符")]
        [RegularExpression(@"^1[3-9]\d{9}$", ErrorMessage = "联系电话格式不正确")]
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(100, ErrorMessage = "邮箱长度不能超过100个字符")]
        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        public string Email { get; set; }

        /// <summary>
        /// 用户数
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        /// 状态（正常/停用）
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public int Status { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        [Required(ErrorMessage = "租户ID不能为空")]
        [Range(0, 9999, ErrorMessage = "租户ID必须在0-9999之间")]
        public long TenantId { get; set; }
    }

    /// <summary>
    /// 部门导入模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtDeptTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDeptTemplateDto()
        {
            DeptName = "系统管理部";
            ParentDeptName = "总公司";
            OrderNum = 1;
            Leader = "张三";
            Phone = "13800138000";
            Email = "admin@lean365.com";
            Status = 0;
        }

        /// <summary>
        /// 部门名称
        /// </summary>
        [Required(ErrorMessage = "部门名称不能为空")]
        [MaxLength(50, ErrorMessage = "部门名称长度不能超过50个字符")]
        public string DeptName { get; set; }

        /// <summary>
        /// 父部门名称
        /// </summary>
        public string ParentDeptName { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Range(0, 9999, ErrorMessage = "显示顺序必须在0-9999之间")]
        public int OrderNum { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        [MaxLength(50, ErrorMessage = "负责人长度不能超过50个字符")]
        public string Leader { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [MaxLength(20, ErrorMessage = "联系电话长度不能超过20个字符")]
        [RegularExpression(@"^1[3-9]\d{9}$", ErrorMessage = "联系电话格式不正确")]
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(100, ErrorMessage = "邮箱长度不能超过100个字符")]
        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        public string Email { get; set; }

        /// <summary>
        /// 用户数
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        /// 状态（正常/停用）
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public int Status { get; set; }
    }
}