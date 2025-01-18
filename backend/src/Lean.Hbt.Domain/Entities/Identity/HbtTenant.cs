//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTenant.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 17:30
// 版本号 : V0.0.1
// 描述   : 租户实体
//===================================================================

using System.ComponentModel;
using Lean.Hbt.Common.Enums;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Identity;

/// <summary>
/// 租户实体
/// </summary>
[SugarTable("hbt_tenant", TableDescription = "租户表")]
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
    [SugarColumn(ColumnDescription = "租户名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    [Description("租户名称")]
    public string TenantName { get; set; }

    /// <summary>
    /// 租户编码
    /// </summary>
    [SugarColumn(ColumnDescription = "租户编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    [Description("租户编码")]
    public string TenantCode { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>
    [SugarColumn(ColumnDescription = "联系人", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    [Description("联系人")]
    public string ContactPerson { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    [SugarColumn(ColumnDescription = "联系电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    [Description("联系电话")]
    public string ContactPhone { get; set; }

    /// <summary>
    /// 联系邮箱
    /// </summary>
    [SugarColumn(ColumnDescription = "联系邮箱", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    [Description("联系邮箱")]
    public string ContactEmail { get; set; }

    /// <summary>
    /// 租户地址
    /// </summary>
    [SugarColumn(ColumnDescription = "租户地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
    [Description("租户地址")]
    public string Address { get; set; }

    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    [SugarColumn(ColumnDescription = "数据库连接字符串", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    [Description("数据库连接字符串")]
    public string DbConnection { get; set; }

    /// <summary>
    /// 租户域名
    /// </summary>
    [SugarColumn(ColumnDescription = "租户域名", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    [Description("租户域名")]
    public string Domain { get; set; }

    /// <summary>
    /// 租户Logo
    /// </summary>
    [SugarColumn(ColumnDescription = "租户Logo", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
    [Description("租户Logo")]
    public string LogoUrl { get; set; }

    /// <summary>
    /// 租户主题
    /// </summary>
    [SugarColumn(ColumnDescription = "租户主题", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    [Description("租户主题")]
    public string Theme { get; set; }

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
    /// 租户状态
    /// </summary>
    [SugarColumn(ColumnDescription = "租户状态", ColumnDataType = "int", IsNullable = false)]
    [Description("租户状态")]
    public HbtStatus Status { get; set; }
}