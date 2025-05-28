namespace Lean.Hbt.Domain.IServices.Extensions;

/// <summary>
/// 租户实体接口
/// </summary>
public interface ITenantEntity
{
    /// <summary>
    /// 租户ID
    /// </summary>
    long TenantId { get; set; }
} 