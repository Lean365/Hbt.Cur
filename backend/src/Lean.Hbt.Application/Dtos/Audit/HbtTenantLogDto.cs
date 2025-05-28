// ================================================
// 文件：HbtTenantLogDto.cs
// 功能：租户审计日志DTO
// 作者：Hbt
// 时间：2024-03-21
// 说明：包含租户审计日志的基础、查询、创建、更新、删除和导出DTO
// ================================================

using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Dtos.Audit;

/// <summary>
/// 租户审计日志基础DTO
/// </summary>
public class HbtTenantLogDto
{
    /// <summary>
    /// 日志ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    public long TenantId { get; set; }

    /// <summary>
    /// 租户名称
    /// </summary>
    public string? TenantName { get; set; }

    /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 用户名称
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 操作类型
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// 操作详情
    /// </summary>
    public string? Details { get; set; }

    /// <summary>
    /// IP地址
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    /// 用户代理
    /// </summary>
    public string? UserAgent { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// 租户审计日志查询DTO
/// </summary>
public class HbtTenantLogQueryDto : HbtPagedQuery
{
    /// <summary>
    /// 租户ID
    /// </summary>
    public long? TenantId { get; set; }

    /// <summary>
    /// 用户ID
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// 操作类型
    /// </summary>
    public string? Action { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }
}

/// <summary>
/// 租户审计日志创建DTO
/// </summary>
public class HbtTenantLogCreateDto
{
    /// <summary>
    /// 租户ID
    /// </summary>
    public long TenantId { get; set; }

    /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 操作类型
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// 操作详情
    /// </summary>
    public string? Details { get; set; }

    /// <summary>
    /// IP地址
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    /// 用户代理
    /// </summary>
    public string? UserAgent { get; set; }
}

/// <summary>
/// 租户审计日志更新DTO
/// </summary>
public class HbtTenantLogUpdateDto : HbtTenantLogCreateDto
{
    /// <summary>
    /// 日志ID
    /// </summary>
    public long Id { get; set; }
}

/// <summary>
/// 租户审计日志删除DTO
/// </summary>
public class HbtTenantLogDeleteDto
{
    /// <summary>
    /// 日志ID列表
    /// </summary>
    public List<long> Ids { get; set; } = new();
}

/// <summary>
/// 租户审计日志导出DTO
/// </summary>
public class HbtTenantLogExportDto
{
    /// <summary>
    /// 租户ID
    /// </summary>
    public long? TenantId { get; set; }

    /// <summary>
    /// 用户ID
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// 操作类型
    /// </summary>
    public string? Action { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 导出格式
    /// </summary>
    public string Format { get; set; } = "xlsx";
} 