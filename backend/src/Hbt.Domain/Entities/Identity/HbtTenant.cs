#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTenant.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 租户实体类
//===================================================================

using SqlSugar;

namespace Hbt.Domain.Entities.Identity;

/// <summary>
/// 租户实体
/// </summary>
[SugarTable("hbt_identity_tenant", TableDescription = "租户表")]
[SugarIndex("ix_tenant_code", nameof(TenantCode), OrderByType.Asc, true)]
public class HbtTenant : HbtBaseEntity
{
    /// <summary>
    /// 租户配置
    /// </summary>
    [SugarColumn(ColumnName = "config_id", ColumnDescription = "租户配置", ColumnDataType = "nvarchar", IsNullable = false)]
    public string ConfigId { get; set; } = string.Empty;

    /// <summary>
    /// 租户名称
    /// </summary>
    [SugarColumn(ColumnName = "tenant_name", ColumnDescription = "租户名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string TenantName { get; set; } = string.Empty;

    /// <summary>
    /// 租户编码
    /// </summary>
    [SugarColumn(ColumnName = "tenant_code", ColumnDescription = "租户编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
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
    [SugarColumn(ColumnName = "contact_email", ColumnDescription = "联系邮箱", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ContactEmail { get; set; } = string.Empty;

    /// <summary>
    /// 租户地址
    /// </summary>
    [SugarColumn(ColumnName = "address", ColumnDescription = "联系地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Address { get; set; }

    /// <summary>
    /// 许可证类型
    /// </summary>
    [SugarColumn(ColumnName = "license_type", ColumnDescription = "许可证类型", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? LicenseType { get; set; }

    /// <summary>
    /// 许可注册码
    /// </summary>
    [SugarColumn(ColumnName = "license_key", ColumnDescription = "许可注册码", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? LicenseKey { get; set; }

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
    /// 许可证开始时间
    /// </summary>
    [SugarColumn(ColumnName = "license_start_time", ColumnDescription = "许可证开始时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? LicenseStartTime { get; set; }

    /// <summary>
    /// 许可证结束时间
    /// </summary>
    [SugarColumn(ColumnName = "license_end_time", ColumnDescription = "许可证结束时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? LicenseEndTime { get; set; }

    /// <summary>
    /// 租户域名
    /// </summary>
    [SugarColumn(ColumnName = "domain", ColumnDescription = "租户域名", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
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
    /// 租户状态（0正常 1停用）
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "租户状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;



    /// <summary>
    /// 用户租户关联列表
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(HbtUserTenant.ConfigId))]
    public List<HbtUserTenant>? UserTenants { get; set; }
}