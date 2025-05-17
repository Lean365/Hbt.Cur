#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTenant.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 租户实体类
//===================================================================

using System.ComponentModel;
using Lean.Hbt.Common.Enums;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Identity;

/// <summary>
/// 租户实体
/// </summary>
[SugarTable("hbt_identity_tenant", TableDescription = "租户表")]
[SugarIndex("ix_tenant_name", nameof(TenantName), OrderByType.Asc, true)]
public class HbtTenant : HbtBaseEntity
{
    /// <summary>
    /// 租户ID
    /// </summary>
    [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = false)]
    public long TenantId { get; set; }

    /// <summary>
    /// 租户名称
    /// </summary>
    [SugarColumn(ColumnName = "tenant_name", ColumnDescription = "租户名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    [Description("租户名称")]
    public string TenantName { get; set; } = string.Empty;

    /// <summary>
    /// 租户编码
    /// </summary>
    [SugarColumn(ColumnName = "tenant_code", ColumnDescription = "租户编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    [Description("租户编码")]
    public string TenantCode { get; set; } = string.Empty;

    /// <summary>
    /// 联系人
    /// </summary>
    [SugarColumn(ColumnName = "contact_user", ColumnDescription = "联系人", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
    [Description("联系人")]
    public string? ContactUser { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    [SugarColumn(ColumnName = "contact_phone", ColumnDescription = "联系电话", Length = 11, ColumnDataType = "nvarchar", IsNullable = true)]
    [Description("联系电话")]
    public string? ContactPhone { get; set; }

    /// <summary>
    /// 联系邮箱
    /// </summary>
    [SugarColumn(ColumnDescription = "联系邮箱", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    [Description("联系邮箱")]
    public string ContactEmail { get; set; } = string.Empty;

    /// <summary>
    /// 租户地址
    /// </summary>
    [SugarColumn(ColumnName = "address", ColumnDescription = "联系地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
    [Description("租户地址")]
    public string? Address { get; set; }

    /// <summary>
    /// 许可证
    /// </summary>
    [SugarColumn(ColumnName = "license", ColumnDescription = "许可证", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
    [Description("许可证")]
    public string? License { get; set; }

    /// <summary>
    /// 过期时间
    /// </summary>
    [SugarColumn(ColumnName = "expire_time", ColumnDescription = "过期时间", ColumnDataType = "datetime", IsNullable = true)]
    [Description("过期时间")]
    public DateTime? ExpireTime { get; set; }

    /// <summary>
    /// 状态（0正常 1停用）
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态（0正常 1停用）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    [Description("状态（0正常 1停用）")]
    public int Status { get; set; } = 0;

    /// <summary>
    /// 是否默认（0否 1是）
    /// </summary>
    [SugarColumn(ColumnName = "is_default", ColumnDescription = "是否默认（0否 1是）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    [Description("是否默认（0否 1是）")]
    public int IsDefault { get; set; } = 0;

    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    [SugarColumn(ColumnDescription = "数据库连接字符串", Length = 500, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    [Description("数据库连接字符串")]
    public string DbConnection { get; set; } = string.Empty;

    /// <summary>
    /// 租户域名
    /// </summary>
    [SugarColumn(ColumnDescription = "租户域名", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    [Description("租户域名")]
    public string Domain { get; set; } = string.Empty;

    /// <summary>
    /// 租户Logo
    /// </summary>
    [SugarColumn(ColumnDescription = "租户Logo", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
    [Description("租户Logo")]
    public string? LogoUrl { get; set; }

    /// <summary>
    /// 租户主题
    /// </summary>
    [SugarColumn(ColumnDescription = "租户主题", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    [Description("租户主题")]
    public string? Theme { get; set; }

    /// <summary>
    /// 授权开始时间
    /// </summary>
    [SugarColumn(ColumnDescription = "授权开始时间", ColumnDataType = "datetime", IsNullable = false)]
    [Description("授权开始时间")]
    public DateTime? LicenseStartTime { get; set; }

    /// <summary>
    /// 授权结束时间
    /// </summary>
    [SugarColumn(ColumnDescription = "授权结束时间", ColumnDataType = "datetime", IsNullable = false)]
    [Description("授权结束时间")]
    public DateTime? LicenseEndTime { get; set; }

    /// <summary>
    /// 最大用户数
    /// </summary>
    [SugarColumn(ColumnDescription = "最大用户数", ColumnDataType = "int", IsNullable = false)]
    [Description("最大用户数")]
    public int MaxUserCount { get; set; }

    /// <summary>
    /// 用户导航属性
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(HbtUser.TenantId))]
    public List<HbtUser>? Users { get; set; }

    /// <summary>
    /// 部门导航属性
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(HbtDept.TenantId))]
    public List<HbtDept>? Depts { get; set; }

    /// <summary>
    /// 岗位导航属性
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(HbtPost.TenantId))]
    public List<HbtPost>? Posts { get; set; }

    /// <summary>
    /// 角色导航属性
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(HbtRole.TenantId))]
    public List<HbtRole>? Roles { get; set; }

    /// <summary>
    /// 菜单导航属性
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(HbtMenu.TenantId))]
    public List<HbtMenu>? Menus { get; set; }
}