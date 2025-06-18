//===================================================================
// 项目名: Lean.Hbt.Application
// 文件名: HbtUserDto.cs
// 创建者: Lean365
// 创建时间: 2024-01-17
// 版本号: V0.0.1
// 描述: 用户数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.Identity
{
    /// <summary>
    /// 用户基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-17
    /// </remarks>
    public class HbtUserDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtUserDto()
        {
            UserName = string.Empty;
            NickName = string.Empty;
            RealName = string.Empty;
            FullName = string.Empty;
            EnglishName = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
            Avatar = string.Empty;
            TenantName = string.Empty;
            Roles = new List<string>();
            Permissions = new List<string>();
            RoleIds = new List<long>();
            PostIds = new List<long>();
            DeptIds = new List<long>();
            Remark = string.Empty;
            CreateBy = string.Empty;
            UpdateBy = string.Empty;
            DeleteBy = string.Empty;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; } = string.Empty;

        /// <summary>
        /// 全名
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; } = string.Empty;


        /// <summary>
        /// 用户类型（0系统用户 1普通用户 2管理员 3OAuth用户）
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(50, ErrorMessage = "邮箱长度不能超过50个字符")]
        public string? Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// 性别（0未知 1男 2女）
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string? Avatar { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public long TenantId { get; set; }

        /// <summary>
        /// 租户名称
        /// </summary>
        public string TenantName { get; set; }

        /// <summary>
        /// 角色列表
        /// </summary>
        public List<string> Roles { get; set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        public List<string> Permissions { get; set; }

        /// <summary>
        /// 锁定状态（0正常 1临时锁定30分钟 2永久锁定需要人工干预）
        /// </summary>
        public int IsLock { get; set; } = 0;

        /// <summary>
        /// 错误次数限制（0是3次 1是5次）
        /// </summary>
        public int ErrorLimit { get; set; } = 0;

        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginCount { get; set; } = 0;

        /// <summary>
        /// 角色ID列表
        /// </summary>
        public List<long> RoleIds { get; set; }

        /// <summary>
        /// 岗位ID列表
        /// </summary>
        public List<long> PostIds { get; set; }

        /// <summary>
        /// 部门ID列表
        /// </summary>
        public List<long> DeptIds { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否删除（0未删除 1已删除）
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string? DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
    }

    /// <summary>
    /// 用户查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-17
    /// </remarks>
    public class HbtUserQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtUserQueryDto()
        {
            UserName = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(30, ErrorMessage = "用户名长度不能超过30个字符")]
        public string? UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(30, ErrorMessage = "昵称长度不能超过30个字符")]
        public string? NickName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(11, ErrorMessage = "手机号码长度不能超过11个字符")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(50, ErrorMessage = "邮箱长度不能超过50个字符")]
        public string? Email { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 用户类型（0系统用户 1普通用户 2管理员 3OAuth用户）
        /// </summary>
        public int? UserType { get; set; }

        /// <summary>
        /// 性别（0未知 1男 2女）
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public long? DeptId { get; set; }
    }

    /// <summary>
    /// 用户创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-17
    /// </remarks>
    public class HbtUserCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtUserCreateDto()
        {
            UserName = string.Empty;
            NickName = string.Empty;
            RealName = string.Empty;
            FullName = string.Empty;
            EnglishName = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
            Avatar = string.Empty;
            RoleIds = new List<long>();
            PostIds = new List<long>();
            DeptIds = new List<long>();
            DeptId = 0;
            Remark = string.Empty;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空")]
        [MaxLength(30, ErrorMessage = "用户名长度不能超过30个字符")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 昵称
        /// </summary>
        [Required(ErrorMessage = "昵称不能为空")]
        [MaxLength(30, ErrorMessage = "昵称长度不能超过30个字符")]
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        [MinLength(6, ErrorMessage = "密码长度不能少于6个字符")]
        [MaxLength(20, ErrorMessage = "密码长度不能超过20个字符")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 真实姓名
        /// </summary>
        [MaxLength(50, ErrorMessage = "真实姓名长度不能超过50个字符")]
        public string RealName { get; set; } = string.Empty;

        /// <summary>
        /// 全名
        /// </summary>
        [MaxLength(50, ErrorMessage = "全名长度不能超过50个字符")]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// 英文名称
        /// </summary>
        [MaxLength(50, ErrorMessage = "英文名称长度不能超过50个字符")]
        public string EnglishName { get; set; } = string.Empty;

        /// <summary>
        /// 用户类型（0系统用户 1普通用户 2管理员 3OAuth用户）
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(50, ErrorMessage = "邮箱长度不能超过50个字符")]
        public string? Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(11, ErrorMessage = "手机号码长度不能超过11个字符")]
        [RegularExpression(@"^1[3-9]\d{9}$", ErrorMessage = "手机号码格式不正确")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// 性别（0未知 1男 2女）
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [MaxLength(100, ErrorMessage = "头像地址长度不能超过100个字符")]
        public string? Avatar { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }


        /// <summary>
        /// 部门ID
        /// </summary>
        [Required(ErrorMessage = "部门不能为空")]
        public long DeptId { get; set; }

        /// <summary>
        /// 角色ID列表
        /// </summary>
        public List<long> RoleIds { get; set; }

        /// <summary>
        /// 岗位ID列表
        /// </summary>
        public List<long> PostIds { get; set; }

        /// <summary>
        /// 部门ID列表
        /// </summary>
        public List<long> DeptIds { get; set; }


        /// <summary>
        /// 租户ID列表
        /// </summary>
        public long[]? TenantIds { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 用户更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-17
    /// </remarks>
    public class HbtUserUpdateDto : HbtUserCreateDto
    {

        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        [Range(1, long.MaxValue, ErrorMessage = "用户ID必须大于0")]
        public long UserId { get; set; }


    }

    /// <summary>
    /// 用户导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-17
    /// </remarks>
    public class HbtUserImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtUserImportDto()
        {
            UserName = string.Empty;
            NickName = string.Empty;
            RealName = string.Empty;
            FullName = string.Empty;
            EnglishName = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
            Avatar = string.Empty;
            Remark = string.Empty;
        }
        /// <summary>
        /// 租户ID
        /// </summary>
        public long TenantId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; } = string.Empty;

        /// <summary>
        /// 全名
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; } = string.Empty;

        /// <summary>
        /// 用户类型(0=系统用户,1=普通用户)
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 性别(0=未知,1=男,2=女)
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; } = string.Empty;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; } = string.Empty;
    }

    /// <summary>
    /// 用户导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-17
    /// </remarks>
    public class HbtUserExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtUserExportDto()
        {
            UserName = string.Empty;
            NickName = string.Empty;
            RealName = string.Empty;
            FullName = string.Empty;
            EnglishName = string.Empty;
            UserType = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
            Gender = string.Empty;
            Avatar = string.Empty;
            DeptName = string.Empty;
            RoleNames = string.Empty;
            PostNames = string.Empty;
            Status = 0;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; } = string.Empty;

        /// <summary>
        /// 全名
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; } = string.Empty;

        /// <summary>
        /// 角色名称列表
        /// </summary>
        public string RoleNames { get; set; } = string.Empty;

        /// <summary>
        /// 岗位名称列表
        /// </summary>
        public string PostNames { get; set; } = string.Empty;

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 用户导入模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-17
    /// </remarks>
    public class HbtUserTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtUserTemplateDto()
        {
            UserName = string.Empty;
            NickName = string.Empty;
            RealName = string.Empty;
            FullName = string.Empty;
            EnglishName = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
            Avatar = string.Empty;
            Remark = string.Empty;
        }
        /// <summary>
        /// 租户ID
        /// </summary>
        public long TenantId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; } = string.Empty;

        /// <summary>
        /// 全名
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 用户类型(0=系统用户,1=普通用户)
        /// </summary>
        public int UserType { get; set; }


        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 性别(0=未知,1=男,2=女)
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; } = string.Empty;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; } = string.Empty;
    }

    /// <summary>
    /// 用户状态更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-17
    /// </remarks>
    public class HbtUserStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtUserStatusDto()
        {
        }

        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public long UserId { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public int Status { get; set; }
    }

    /// <summary>
    /// 重置密码DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-17
    /// </remarks>
    public class HbtUserResetPwdDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtUserResetPwdDto()
        {
            Password = string.Empty;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public long UserId { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [Required(ErrorMessage = "新密码不能为空")]
        [MinLength(6, ErrorMessage = "新密码长度不能少于6个字符")]
        [MaxLength(20, ErrorMessage = "新密码长度不能超过20个字符")]
        public string Password { get; set; } = string.Empty;
    }

    /// <summary>
    /// 修改密码DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-17
    /// </remarks>
    public class HbtUserChangePwdDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtUserChangePwdDto()
        {
            OldPassword = string.Empty;
            NewPassword = string.Empty;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public long UserId { get; set; }

        /// <summary>
        /// 旧密码
        /// </summary>
        [Required(ErrorMessage = "旧密码不能为空")]
        public string OldPassword { get; set; } = string.Empty;

        /// <summary>
        /// 新密码
        /// </summary>
        [Required(ErrorMessage = "新密码不能为空")]
        [MinLength(6, ErrorMessage = "新密码长度不能少于6个字符")]
        [MaxLength(20, ErrorMessage = "新密码长度不能超过20个字符")]
        public string NewPassword { get; set; } = string.Empty;
    }

    /// <summary>
    /// 用户解锁DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-22
    /// </remarks>
    public class HbtUserUnlockDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtUserUnlockDto()
        {
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public long UserId { get; set; }

        /// <summary>
        /// 锁定状态（0正常 1临时锁定30分钟 2永久锁定需要人工干预）
        /// </summary>
        [Required(ErrorMessage = "锁定状态不能为空")]
        public int IsLock { get; set; }

        /// <summary>
        /// 错误次数限制（0是3次 1是5次）
        /// </summary>
        [Required(ErrorMessage = "错误次数限制不能为空")]
        public int ErrorLimit { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required(ErrorMessage = "备注不能为空")]
        [MaxLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string? Remark { get; set; } = string.Empty;
    }
}