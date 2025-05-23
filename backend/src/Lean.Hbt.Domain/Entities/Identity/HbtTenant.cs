#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTenant.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 租户实体类
//===================================================================

namespace Lean.Hbt.Domain.Entities.Identity;

/// <summary>
/// 租户实体
/// </summary>
[SugarTable("hbt_identity_tenant", TableDescription = "租户表")]
[SugarIndex("ix_tenant_name", nameof(TenantName), OrderByType.Asc, true)]
public class HbtTenant : HbtBaseEntity
{
    /// <summary>
    /// 租户名称
    /// </summary>
    [SugarColumn(ColumnName = "tenant_name", ColumnDescription = "租户名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]

    public string TenantName { get; set; } = string.Empty;

    /// <summary>
    /// 租户编码
    /// </summary>
    [SugarColumn(ColumnName = "tenant_code", ColumnDescription = "租户编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]

    public string TenantCode { get; set; } = string.Empty;

    /// <summary>
    /// 联系人
    /// </summary>
    [SugarColumn(ColumnName = "contact_user", ColumnDescription = "联系人", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]

    public string? ContactUser { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    [SugarColumn(ColumnName = "contact_phone", ColumnDescription = "联系电话", Length = 11, ColumnDataType = "nvarchar", IsNullable = true)]

    public string? ContactPhone { get; set; }

    /// <summary>
    /// 联系邮箱
    /// </summary>
    [SugarColumn(ColumnName = "contact_email", ColumnDescription = "联系邮箱", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]

    public string ContactEmail { get; set; } = string.Empty;

    /// <summary>
    /// 租户地址
    /// </summary>
    [SugarColumn(ColumnName = "address", ColumnDescription = "联系地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]

    public string? Address { get; set; }

    /// <summary>
    /// 许可证
    /// </summary>
    [SugarColumn(ColumnName = "license", ColumnDescription = "许可证", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
  
    public string? License { get; set; }

    /// <summary>
    /// 最大用户数
    /// </summary>
    [SugarColumn(ColumnName = "max_user_count", ColumnDescription = "最大用户数", ColumnDataType = "int", IsNullable = false, DefaultValue = "9")]

    public int MaxUserCount { get; set; } = 9;

    /// <summary>
    /// 过期时间
    /// </summary>
    [SugarColumn(ColumnName = "expire_time", ColumnDescription = "过期时间", ColumnDataType = "datetime", IsNullable = true)]

    public DateTime? ExpireTime { get; set; }

    /// <summary>
    /// 是否默认（0否 1是）
    /// </summary>
    [SugarColumn(ColumnName = "is_default", ColumnDescription = "是否默认", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]

    public int IsDefault { get; set; } = 0;

    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    [SugarColumn(ColumnName = "db_connection", ColumnDescription = "数据库连接字符串", Length = 500, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]

    public string DbConnection { get; set; } = string.Empty;

    /// <summary>
    /// 租户域名
    /// </summary>
    [SugarColumn(ColumnName = "domain", ColumnDescription = "租户域名", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]

    public string Domain { get; set; } = string.Empty;

    /// <summary>
    /// 租户Logo
    /// </summary>
    [SugarColumn(ColumnName = "logo_url", ColumnDescription = "租户Logo", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]

    public string? LogoUrl { get; set; }

    /// <summary>
    /// 租户主题
    /// </summary>
    [SugarColumn(ColumnName = "theme", ColumnDescription = "租户主题", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]

    public string? Theme { get; set; }

    /// <summary>
    /// 授权开始时间
    /// </summary>
    [SugarColumn(ColumnName = "license_start_time", ColumnDescription = "起始授权", ColumnDataType = "datetime", IsNullable = false, DefaultValue = "GETDATE()")]

    public DateTime? LicenseStartTime { get; set; }=DateTime.Now;

    /// <summary>
    /// 授权结束时间
    /// </summary>
    [SugarColumn(ColumnName = "license_end_time", ColumnDescription = "结束授权", ColumnDataType = "datetime", IsNullable = false)]

    public DateTime? LicenseEndTime { get; set; }


    /// <summary>
    /// 状态（0正常 1停用）
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
   
    public int Status { get; set; } = 0;
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