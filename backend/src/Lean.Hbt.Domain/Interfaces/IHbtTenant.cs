namespace Lean.Hbt.Domain.Interfaces
{
    /// <summary>
    /// 租户实体接口
    /// </summary>
    public interface IHbtTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        long TenantId { get; set; }
    }
} 