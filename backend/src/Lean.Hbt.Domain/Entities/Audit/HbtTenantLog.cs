// ================================================
// 文件：HbtTenantLog.cs
// 功能：租户审计日志实体
// 作者：Hbt
// 时间：2024-03-21
// 说明：租户审计日志实体类,包含租户ID、用户ID、操作类型、操作详情、IP地址、用户代理等字段
// ================================================

using SqlSugar;
using Lean.Hbt.Domain.Entities.Identity;

namespace Lean.Hbt.Domain.Entities.Audit;

/// <summary>
/// 租户审计日志实体
/// </summary>
[SugarTable("hbt_audit_tenant_log", TableDescription = "租户审计日志表")]
[SugarIndex("index_tenant_id", nameof(TenantId), OrderByType.Asc)]
[SugarIndex("index_user_id", nameof(UserId), OrderByType.Asc)]
[SugarIndex("index_create_time", nameof(CreateTime), OrderByType.Desc)]
public class HbtTenantLog : HbtBaseEntity
{
    /// <summary>
    /// 租户ID
    /// </summary>
    [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = false)]
    public long TenantId { get; set; }

    /// <summary>
    /// 用户ID
    /// </summary>
    [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
    public long UserId { get; set; }

    /// <summary>
    /// 操作类型
    /// </summary>
    [SugarColumn(ColumnName = "action", ColumnDescription = "操作类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// 操作详情
    /// </summary>
    [SugarColumn(ColumnName = "details", ColumnDescription = "操作详情", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Details { get; set; }

    /// <summary>
    /// IP地址
    /// </summary>
    [SugarColumn(ColumnName = "ip_address", ColumnDescription = "IP地址", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? IpAddress { get; set; }

    /// <summary>
    /// 用户代理
    /// </summary>
    [SugarColumn(ColumnName = "user_agent", ColumnDescription = "用户代理", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? UserAgent { get; set; }

    /// <summary>
    /// 租户导航属性
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(TenantId))]
    public HbtTenant? Tenant { get; set; }

    /// <summary>
    /// 用户导航属性
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(UserId))]
    public HbtUser? User { get; set; }
} 