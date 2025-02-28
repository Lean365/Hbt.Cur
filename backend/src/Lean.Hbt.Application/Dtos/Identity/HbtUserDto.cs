//===================================================================
// 项目名: Lean.Hbt.Application
// 文件名: HbtUserDto.cs
// 创建者: Lean365
// 创建时间: 2024-01-17
// 版本号: V0.0.1
// 描述: 用户数据传输对象
//===================================================================

using System;
using System.ComponentModel.DataAnnotations;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Models;
using System.Collections.Generic;

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
            EnglishName = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
            Avatar = string.Empty;
            TenantName = string.Empty;
            CreateTime = DateTime.Now;
            Roles = new List<string>();
            Permissions = new List<string>();
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 用户类型（0系统用户 1普通用户）
        /// </summary>
        public HbtUserType UserType { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public HbtGender Gender { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 状态(0正常 1停用)
        /// </summary>
        public HbtStatus Status { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName => Status.ToString();

        /// <summary>
        /// 租户ID
        /// </summary>
        public long TenantId { get; set; }

        /// <summary>
        /// 租户名称
        /// </summary>
        public string TenantName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 角色列表
        /// </summary>
        public List<string> Roles { get; set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        public List<string> Permissions { get; set; }
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
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(50, ErrorMessage = "用户名长度不能超过50个字符")]
        public string UserName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(20, ErrorMessage = "手机号码长度不能超过20个字符")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 状态(0正常 1停用)
        /// </summary>
        public HbtStatus? Status { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public HbtUserType? UserType { get; set; }

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
            EnglishName = string.Empty;
            Password = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
            Avatar = string.Empty;
            RoleIds = new List<long>();
            PostIds = new List<long>();
            Remark = string.Empty;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空")]
        [MaxLength(50, ErrorMessage = "用户名长度不能超过50个字符")]
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Required(ErrorMessage = "昵称不能为空")]
        [MaxLength(50, ErrorMessage = "昵称长度不能超过50个字符")]
        public string NickName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        [MaxLength(50, ErrorMessage = "英文名称长度不能超过50个字符")]
        public string EnglishName { get; set; }

        /// <summary>
        /// 用户类型（0系统用户 1普通用户）
        /// </summary>
        public HbtUserType UserType { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        [MinLength(6, ErrorMessage = "密码长度不能少于6个字符")]
        [MaxLength(20, ErrorMessage = "密码长度不能超过20个字符")]
        public string Password { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(20, ErrorMessage = "手机号码长度不能超过20个字符")]
        [RegularExpression(@"^1[3-9]\d{9}$", ErrorMessage = "手机号码格式不正确")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(50, ErrorMessage = "邮箱长度不能超过50个字符")]
        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        public string Email { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public HbtGender Gender { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [MaxLength(200, ErrorMessage = "头像地址长度不能超过200个字符")]
        public string Avatar { get; set; }

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
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string Remark { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public long TenantId { get; set; }
    }

    /// <summary>
    /// 用户更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-17
    /// </remarks>
    public class HbtUserUpdateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtUserUpdateDto()
        {
            NickName = string.Empty;
            EnglishName = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
            Avatar = string.Empty;
            RoleIds = new List<long>();
            PostIds = new List<long>();
            Remark = string.Empty;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public long UserId { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        [Required(ErrorMessage = "租户ID不能为空")]
        public long TenantId { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Required(ErrorMessage = "昵称不能为空")]
        [MaxLength(50, ErrorMessage = "昵称长度不能超过50个字符")]
        public string NickName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        [MaxLength(50, ErrorMessage = "英文名称长度不能超过50个字符")]
        public string EnglishName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(20, ErrorMessage = "手机号码长度不能超过20个字符")]
        [RegularExpression(@"^1[3-9]\d{9}$", ErrorMessage = "手机号码格式不正确")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(50, ErrorMessage = "邮箱长度不能超过50个字符")]
        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        public string Email { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public HbtGender Gender { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [MaxLength(200, ErrorMessage = "头像地址长度不能超过200个字符")]
        public string Avatar { get; set; }

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
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string Remark { get; set; }
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
            EnglishName = string.Empty;
            UserType = string.Empty;
            Password = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
            Gender = string.Empty;
            Avatar = string.Empty;
            DeptName = string.Empty;
            RoleNames = string.Empty;
            PostNames = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 用户类型（0系统用户 1普通用户）
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 性别(0=未知,1=男,2=女)
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 角色名称列表，逗号分隔
        /// </summary>
        public string RoleNames { get; set; }

        /// <summary>
        /// 岗位名称列表，逗号分隔
        /// </summary>
        public string PostNames { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
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
            EnglishName = string.Empty;
            UserType = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
            Gender = string.Empty;
            Avatar = string.Empty;
            DeptName = string.Empty;
            RoleNames = string.Empty;
            PostNames = string.Empty;
            Status = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

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
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 角色名称列表
        /// </summary>
        public string RoleNames { get; set; }

        /// <summary>
        /// 岗位名称列表
        /// </summary>
        public string PostNames { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

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
            EnglishName = string.Empty;
            UserType = string.Empty;
            Password = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
            Gender = string.Empty;
            Avatar = string.Empty;
            DeptName = string.Empty;
            RoleNames = string.Empty;
            PostNames = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 用户类型(0=系统用户,1=普通用户)
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 性别(0=未知,1=男,2=女)
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 角色名称列表，多个逗号分隔
        /// </summary>
        public string RoleNames { get; set; }

        /// <summary>
        /// 岗位名称列表，多个逗号分隔
        /// </summary>
        public string PostNames { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
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
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public long UserId { get; set; }

        /// <summary>
        /// 状态(0正常 1停用)
        /// </summary>
        public HbtStatus Status { get; set; }
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
        public string Password { get; set; }
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
        public string OldPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [Required(ErrorMessage = "新密码不能为空")]
        [MinLength(6, ErrorMessage = "新密码长度不能少于6个字符")]
        [MaxLength(20, ErrorMessage = "新密码长度不能超过20个字符")]
        public string NewPassword { get; set; }
    }
} 