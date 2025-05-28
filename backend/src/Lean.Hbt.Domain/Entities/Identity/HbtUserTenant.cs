#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtUserTenant.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 用户租户关联实体类
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Identity;

/// <summary>
/// 用户租户关联实体
/// </summary>
[SugarTable("hbt_identity_user_tenant", TableDescription = "用户租户关联表")]
[SugarIndex("ix_user_tenant", nameof(UserId), OrderByType.Asc, nameof(TenantId), OrderByType.Asc, true)]
public class HbtUserTenant : HbtBaseEntity
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
    public long UserId { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = false)]
    public long TenantId { get; set; }

    /// <summary>
    /// 是否默认租户（0否 1是）
    /// </summary>
    [SugarColumn(ColumnName = "is_default", ColumnDescription = "是否默认租户", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsDefault { get; set; } = 0;

    /// <summary>
    /// 在该租户中的状态（0正常 1停用）
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;

    /// <summary>
    /// 用户导航属性
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(UserId))]
    public HbtUser? User { get; set; }

    /// <summary>
    /// 租户导航属性
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(TenantId))]
    public HbtTenant? Tenant { get; set; }
} 