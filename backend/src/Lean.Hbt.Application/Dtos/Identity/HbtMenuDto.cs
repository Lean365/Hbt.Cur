//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtMenuDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 菜单数据传输对象
//===================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Dtos.Identity
{
    /// <summary>
    /// 菜单基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtMenuDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMenuDto()
        {
            MenuName = string.Empty;
            TransKey = string.Empty;
            Path = string.Empty;
            Component = string.Empty;
            Perms = string.Empty;
            Icon = string.Empty;
            Children = new List<HbtMenuDto>();
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public long MenuId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 翻译Key
        /// </summary>
        public string TransKey { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 组件路径
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 是否为外链
        /// </summary>
        public HbtYesNo IsFrame { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public HbtMenuType MenuType { get; set; }

        /// <summary>
        /// 显示状态
        /// </summary>
        public HbtVisible Visible { get; set; }

        /// <summary>
        /// 菜单状态
        /// </summary>
        public HbtStatus Status { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        public string Perms { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public List<HbtMenuDto> Children { get; set; }
    }

    /// <summary>
    /// 菜单查询对象
    /// </summary>
    public class HbtMenuQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string? MenuName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public HbtStatus? Status { get; set; }
    }

    /// <summary>
    /// 菜单创建对象
    /// </summary>
    public class HbtMenuCreateDto
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        [Required(ErrorMessage = "菜单名称不能为空")]
        public required string MenuName { get; set; }

        /// <summary>
        /// 翻译Key
        /// </summary>
        public string? TransKey { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// 组件路径
        /// </summary>
        public string? Component { get; set; }

        /// <summary>
        /// 是否为外链
        /// </summary>
        public HbtYesNo IsFrame { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public HbtMenuType MenuType { get; set; }

        /// <summary>
        /// 显示状态
        /// </summary>
        public HbtVisible Visible { get; set; }

        /// <summary>
        /// 菜单状态
        /// </summary>
        public HbtStatus Status { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        public string? Perms { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public long TenantId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 菜单更新对象
    /// </summary>
    public class HbtMenuUpdateDto : HbtMenuCreateDto
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public required long MenuId { get; set; }
    }

    /// <summary>
    /// 菜单导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtMenuExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMenuExportDto()
        {
            MenuName = string.Empty;
            TransKey = string.Empty;
            Path = string.Empty;
            Component = string.Empty;
            Perms = string.Empty;
            Icon = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 翻译Key
        /// </summary>
        public string TransKey { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 组件路径
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 是否为外链
        /// </summary>
        public HbtYesNo IsFrame { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public HbtMenuType MenuType { get; set; }

        /// <summary>
        /// 显示状态
        /// </summary>
        public HbtVisible Visible { get; set; }

        /// <summary>
        /// 菜单状态
        /// </summary>
        public HbtStatus Status { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        public string Perms { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 菜单状态更新对象
    /// </summary>
    public class HbtMenuStatusDto
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public required long MenuId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public required HbtStatus Status { get; set; }
    }

    /// <summary>
    /// 菜单排序DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtMenuOrderDto
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public required long MenuId { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public required int OrderNum { get; set; }
    }

    /// <summary>
    /// 菜单导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtMenuImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMenuImportDto()
        {
            MenuName = string.Empty;
            TransKey = string.Empty;
            Path = string.Empty;
            Component = string.Empty;
            IsFrame = string.Empty;
            MenuType = string.Empty;
            Visible = string.Empty;
            Status = string.Empty;
            Perms = string.Empty;
            Icon = string.Empty;
        }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [Required(ErrorMessage = "菜单名称不能为空")]
        public string MenuName { get; set; }

        /// <summary>
        /// 翻译Key
        /// </summary>
        public string TransKey { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 组件路径
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 是否为外链（0否 1是）
        /// </summary>
        [Required(ErrorMessage = "是否为外链不能为空")]
        public string IsFrame { get; set; }

        /// <summary>
        /// 菜单类型（0目录 1菜单 2按钮）
        /// </summary>
        [Required(ErrorMessage = "菜单类型不能为空")]
        public string MenuType { get; set; }

        /// <summary>
        /// 显示状态（0显示 1隐藏）
        /// </summary>
        [Required(ErrorMessage = "显示状态不能为空")]
        public string Visible { get; set; }

        /// <summary>
        /// 菜单状态（0正常 1停用）
        /// </summary>
        [Required(ErrorMessage = "菜单状态不能为空")]
        public string Status { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        public string Perms { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }
    }

    /// <summary>
    /// 菜单导入模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtMenuTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMenuTemplateDto()
        {
            MenuName = "系统管理";
            TransKey = "system";
            Path = "/system";
            Component = "Layout";
            IsFrame = "否";
            MenuType = "目录";
            Visible = "显示";
            Status = "正常";
            Perms = "system:*:*";
            Icon = "system";
        }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [Required(ErrorMessage = "菜单名称不能为空")]
        public string MenuName { get; set; }

        /// <summary>
        /// 翻译Key
        /// </summary>
        public string TransKey { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 组件路径
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 是否为外链（0否 1是）
        /// </summary>
        [Required(ErrorMessage = "是否为外链不能为空")]
        public string IsFrame { get; set; }

        /// <summary>
        /// 菜单类型（0目录 1菜单 2按钮）
        /// </summary>
        [Required(ErrorMessage = "菜单类型不能为空")]
        public string MenuType { get; set; }

        /// <summary>
        /// 显示状态（0显示 1隐藏）
        /// </summary>
        [Required(ErrorMessage = "显示状态不能为空")]
        public string Visible { get; set; }

        /// <summary>
        /// 菜单状态（0正常 1停用）
        /// </summary>
        [Required(ErrorMessage = "菜单状态不能为空")]
        public string Status { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        public string Perms { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }
    }
} 