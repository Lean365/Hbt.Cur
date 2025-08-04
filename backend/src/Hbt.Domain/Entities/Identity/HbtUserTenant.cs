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

namespace Hbt.Cur.Domain.Entities.Identity;

/// <summary>
/// 用户租户关联实体
/// </summary>
[SugarTable("hbt_identity_user_tenant", TableDescription = "用户租户关联表")]
[SugarIndex("ix_user_tenant", nameof(UserId), OrderByType.Asc, nameof(ConfigId), OrderByType.Asc, true)]
public class HbtUserTenant : HbtBaseEntity
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
    public long UserId { get; set; }

    /// <summary>
    /// 租户配置ID
    /// </summary>
    [SugarColumn(ColumnName = "config_id", ColumnDescription = "配置ID", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? ConfigId { get; set; }

    /// <summary>
    /// 用户导航属性
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(UserId))]
    public HbtUser? User { get; set; }

    /// <summary>
    /// 租户导航属性
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(ConfigId))]
    public HbtTenant? Tenant { get; set; }
} 