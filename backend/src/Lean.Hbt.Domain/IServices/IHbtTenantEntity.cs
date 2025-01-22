namespace Lean.Hbt.Domain.IServices
{
    /// <summary>
    /// 租户实体接口
    /// </summary>
    public interface IHbtTenantEntity
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        long TenantId { get; set; }
    }
} 