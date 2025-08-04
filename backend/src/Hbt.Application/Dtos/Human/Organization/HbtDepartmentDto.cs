//===================================================================
// 项目名: Hbt.Application
// 文件名: HbtDepartmentDto.cs
// 创建者: Lean365
// 创建时间: 2024-01-17
// 版本号: V0.0.1
// 描述: 部门数据传输对象
//===================================================================

using System;
using System.Collections.Generic;

namespace Hbt.Application.Dtos.Human.Organization
{
    /// <summary>
    /// 部门基础DTO
    /// </summary>
    public class HbtDepartmentDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDepartmentDto()
        {
            DeptCode = string.Empty;
            DeptName = string.Empty;
            DeptShortName = string.Empty;
            EnglishName = string.Empty;
            DeptPath = string.Empty;
            Phone = string.Empty;
            Fax = string.Empty;
            Extension = string.Empty;
            Email = string.Empty;
            Address = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// 部门ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 部门编码
        /// </summary>
        public string DeptCode { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; } = string.Empty;

        /// <summary>
        /// 部门简称
        /// </summary>
        public string? DeptShortName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string? EnglishName { get; set; }

        /// <summary>
        /// 上级部门ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 部门层级
        /// </summary>
        public int DeptLevel { get; set; }

        /// <summary>
        /// 部门路径
        /// </summary>
        public string? DeptPath { get; set; }

        /// <summary>
        /// 部门负责人ID
        /// </summary>
        public long? ManagerId { get; set; }

        /// <summary>
        /// 部门类型(1=公司 2=部门 3=科室 4=小组)
        /// </summary>
        public int DeptType { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string? Fax { get; set; }

        /// <summary>
        /// 分机
        /// </summary>
        public string? Extension { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 部门地址
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// 部门描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 部门人数
        /// </summary>
        public int EmployeeCount { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 部门状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

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
        /// 上级部门
        /// </summary>
        public HbtDepartmentDto? Parent { get; set; }

        /// <summary>
        /// 子部门
        /// </summary>
        public List<HbtDepartmentDto>? Children { get; set; }

        /// <summary>
        /// 部门负责人
        /// </summary>
        public Employee.HbtEmployeeDto? Manager { get; set; }

        /// <summary>
        /// 部门员工
        /// </summary>
        public List<Employee.HbtEmployeeDto>? Employees { get; set; }
    }

    /// <summary>
    /// 部门查询DTO
    /// </summary>
    public class HbtDepartmentQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDepartmentQueryDto()
        {
            DeptCode = string.Empty;
            DeptName = string.Empty;
            EnglishName = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
        }

        /// <summary>
        /// 部门编码
        /// </summary>
        public string? DeptCode { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string? DeptName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string? EnglishName { get; set; }

        /// <summary>
        /// 上级部门ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 部门类型
        /// </summary>
        public int? DeptType { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 部门状态
        /// </summary>
        public int? Status { get; set; }
    }

    /// <summary>
    /// 部门创建DTO
    /// </summary>
    public class HbtDepartmentCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDepartmentCreateDto()
        {
            DeptCode = string.Empty;
            DeptName = string.Empty;
            DeptShortName = string.Empty;
            EnglishName = string.Empty;
            DeptPath = string.Empty;
            Phone = string.Empty;
            Fax = string.Empty;
            Extension = string.Empty;
            Email = string.Empty;
            Address = string.Empty;
            Description = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 部门编码
        /// </summary>
        public string DeptCode { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; } = string.Empty;

        /// <summary>
        /// 部门简称
        /// </summary>
        public string? DeptShortName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string? EnglishName { get; set; }

        /// <summary>
        /// 上级部门ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 部门层级
        /// </summary>
        public int DeptLevel { get; set; }

        /// <summary>
        /// 部门路径
        /// </summary>
        public string? DeptPath { get; set; }

        /// <summary>
        /// 部门负责人ID
        /// </summary>
        public long? ManagerId { get; set; }

        /// <summary>
        /// 部门类型(1=公司 2=部门 3=科室 4=小组)
        /// </summary>
        public int DeptType { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string? Fax { get; set; }

        /// <summary>
        /// 分机
        /// </summary>
        public string? Extension { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 部门地址
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// 部门描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 部门状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 部门更新DTO
    /// </summary>
    public class HbtDepartmentUpdateDto : HbtDepartmentCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDepartmentUpdateDto() : base()
        {
        }

        /// <summary>
        /// 部门ID
        /// </summary>
        [AdaptMember("Id")]
        public long DepartmentId { get; set; }
    }

    /// <summary>
    /// 部门删除DTO
    /// </summary>
    public class HbtDepartmentDeleteDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDepartmentDeleteDto()
        {
            Remark = string.Empty;
        }

        /// <summary>
        /// 部门ID
        /// </summary>
        [AdaptMember("Id")]
        public long DepartmentId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 部门导入DTO
    /// </summary>
    public class HbtDepartmentImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDepartmentImportDto()
        {
            DeptCode = string.Empty;
            DeptName = string.Empty;
            DeptShortName = string.Empty;
            EnglishName = string.Empty;
            ParentDeptName = string.Empty;
            ManagerName = string.Empty;
            DeptType = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            Address = string.Empty;
            Description = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 部门编码
        /// </summary>
        public string DeptCode { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; } = string.Empty;

        /// <summary>
        /// 部门简称
        /// </summary>
        public string? DeptShortName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string? EnglishName { get; set; }

        /// <summary>
        /// 上级部门名称
        /// </summary>
        public string? ParentDeptName { get; set; }

        /// <summary>
        /// 部门负责人姓名
        /// </summary>
        public string? ManagerName { get; set; }

        /// <summary>
        /// 部门类型
        /// </summary>
        public string DeptType { get; set; } = string.Empty;

        /// <summary>
        /// 电话
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 部门地址
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// 部门描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 部门状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 部门导出DTO
    /// </summary>
    public class HbtDepartmentExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDepartmentExportDto()
        {
            DeptCode = string.Empty;
            DeptName = string.Empty;
            DeptShortName = string.Empty;
            EnglishName = string.Empty;
            ParentDeptName = string.Empty;
            ManagerName = string.Empty;
            DeptType = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            Address = string.Empty;
            Description = string.Empty;
            Status = string.Empty;
            CreateTime = string.Empty;
        }

        /// <summary>
        /// 部门编码
        /// </summary>
        public string DeptCode { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; } = string.Empty;

        /// <summary>
        /// 部门简称
        /// </summary>
        public string? DeptShortName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string? EnglishName { get; set; }

        /// <summary>
        /// 上级部门名称
        /// </summary>
        public string? ParentDeptName { get; set; }

        /// <summary>
        /// 部门负责人姓名
        /// </summary>
        public string? ManagerName { get; set; }

        /// <summary>
        /// 部门类型
        /// </summary>
        public string DeptType { get; set; } = string.Empty;

        /// <summary>
        /// 电话
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 部门地址
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// 部门描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 部门人数
        /// </summary>
        public int EmployeeCount { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 部门状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; } = string.Empty;
    }

    /// <summary>
    /// 部门模板DTO
    /// </summary>
    public class HbtDepartmentTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDepartmentTemplateDto()
        {
            DeptCode = string.Empty;
            DeptName = string.Empty;
            DeptShortName = string.Empty;
            EnglishName = string.Empty;
            ParentDeptName = string.Empty;
            ManagerName = string.Empty;
            DeptType = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            Address = string.Empty;
            Description = string.Empty;
            Status = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 部门编码
        /// </summary>
        public string DeptCode { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; } = string.Empty;

        /// <summary>
        /// 部门简称
        /// </summary>
        public string? DeptShortName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string? EnglishName { get; set; }

        /// <summary>
        /// 上级部门名称
        /// </summary>
        public string? ParentDeptName { get; set; }

        /// <summary>
        /// 部门负责人姓名
        /// </summary>
        public string? ManagerName { get; set; }

        /// <summary>
        /// 部门类型
        /// </summary>
        public string DeptType { get; set; } = string.Empty;

        /// <summary>
        /// 电话
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 部门地址
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// 部门描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 部门状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
} 