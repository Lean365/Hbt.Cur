//===================================================================
// 项目名: Hbt.Application
// 文件名: HbtUserDto.cs
// 创建者: Lean365
// 创建时间: 2024-01-17
// 版本号: V0.0.1
// 描述: 用户数据传输对象
//===================================================================

using System;
using System.Collections.Generic;

namespace Hbt.Application.Dtos.Identity
{
    /// <summary>
    /// 用户基础DTO（与HbtUser实体字段严格对应）
    /// </summary>
    public class HbtUserDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtUserDto()
        {
            UserName = string.Empty;
            NickName = string.Empty;
            FullName = string.Empty;
            RealName = string.Empty;
            EnglishName = string.Empty;
            Password = string.Empty;
            Salt = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
            Avatar = string.Empty;
            LockReason = string.Empty;
        }



        /// <summary>
        /// 用户ID（适配字段）
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
        /// 全名
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; } = string.Empty;

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; } = string.Empty;

        /// <summary>
        /// 用户类型（0系统用户 1普通用户 2管理员 3OAuth用户）
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 盐值
        /// </summary>
        public string Salt { get; set; } = string.Empty;

        /// <summary>
        /// 密码迭代次数
        /// </summary>
        public int Iterations { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

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
        /// 最后修改密码时间
        /// </summary>
        public DateTime? LastPasswordChangeTime { get; set; }

        /// <summary>
        /// 锁定结束时间
        /// </summary>
        public DateTime? LockEndTime { get; set; }

        /// <summary>
        /// 锁定原因
        /// </summary>
        public string? LockReason { get; set; }

        /// <summary>
        /// 是否锁定（0否 1是）
        /// </summary>
        public int IsLock { get; set; }

        /// <summary>
        /// 错误次数限制
        /// </summary>
        public int ErrorLimit { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginCount { get; set; }

        /// <summary>
        /// 角色ID列表
        /// </summary>
        public List<long> RoleIds { get; set; } = new List<long>();

        /// <summary>
        /// 岗位ID列表
        /// </summary>
        public List<long> PostIds { get; set; } = new List<long>();

        /// <summary>
        /// 部门ID列表
        /// </summary>
        public List<long> DeptIds { get; set; } = new List<long>();

        /// <summary>
        /// 租户配置ID列表
        /// </summary>
        public List<string> ConfigIds { get; set; } = new List<string>();

        /// <summary>
        /// 用户租户关联列表
        /// </summary>
        public List<HbtUserTenantDto>? UserTenants { get; set; }

        /// <summary>
        /// 用户角色关联列表
        /// </summary>
        public List<HbtUserRoleDto>? UserRoles { get; set; }

        /// <summary>
        /// 用户部门关联列表
        /// </summary>
        public List<HbtUserDeptDto>? UserDepts { get; set; }

        /// <summary>
        /// 用户岗位关联列表
        /// </summary>
        public List<HbtUserPostDto>? UserPosts { get; set; }
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
 
        public string? UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>

        public string? NickName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>

        public string? PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>

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
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = string.Empty;

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
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

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
        /// 部门ID
        /// </summary>
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
        /// 租户配置ID列表
        /// </summary>
        public string[]? ConfigIds { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
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
        [AdaptMember("Id")]
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
            ConfigId = string.Empty;
            Remark = string.Empty;
        }
        /// <summary>
        /// 租户配置ID
        /// </summary>
        public string ConfigId { get; set; } = string.Empty;

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
            ConfigId = string.Empty;
            Remark = string.Empty;
        }
        /// <summary>
        /// 租户配置ID
        /// </summary>
        public string ConfigId { get; set; } = string.Empty;

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
        [AdaptMember("Id")]
        public long UserId { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
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
        [AdaptMember("Id")]
        public long UserId { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
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
        [AdaptMember("Id")]
        public long UserId { get; set; }

        /// <summary>
        /// 旧密码
        /// </summary>
        public string OldPassword { get; set; } = string.Empty;

        /// <summary>
        /// 新密码
        /// </summary>
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
        [AdaptMember("Id")]
        public long UserId { get; set; }

        /// <summary>
        /// 锁定状态（0正常 1临时锁定30分钟 2永久锁定需要人工干预）
        /// </summary>
        public int IsLock { get; set; }

        /// <summary>
        /// 错误次数限制（0是3次 1是5次）
        /// </summary>
        public int ErrorLimit { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; } = string.Empty;
    }
}