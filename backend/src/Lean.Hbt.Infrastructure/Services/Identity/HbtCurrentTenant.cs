//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtCurrentTenant.cs
// 创建者 : Lean365
// 创建时间: 2024-02-18 02:30
// 版本号 : V1.0.0
// 描述    : 当前租户实现
//===================================================================

using System.Threading;
using Microsoft.AspNetCore.Http;
using Lean.Hbt.Domain.IServices.Extensions;

namespace Lean.Hbt.Infrastructure.Services.Identity
{
    /// <summary>
    /// 当前租户实现
    /// </summary>
    public class HbtCurrentTenant : IHbtCurrentTenant
    {
        private static AsyncLocal<long?> _currentTenantId = new AsyncLocal<long?>();
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtCurrentTenant(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 获取当前租户ID
        /// </summary>
        public long TenantId
        {
            get
            {
                var tenantId = _httpContextAccessor.HttpContext?.User.FindFirst("tid")?.Value;
                return tenantId != null ? long.Parse(tenantId) : 0;
            }
        }

        /// <summary>
        /// 获取当前租户名称
        /// </summary>
        public string TenantName
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User.FindFirst("tnm")?.Value ?? string.Empty;
            }
        }

        /// <summary>
        /// 获取或设置当前租户ID（静态属性）
        /// </summary>
        public static long? CurrentTenantId
        {
            get => _currentTenantId.Value;
            set => _currentTenantId.Value = value;
        }

        /// <summary>
        /// 清除当前租户信息
        /// </summary>
        public static void Clear()
        {
            CurrentTenantId = null;
        }
    }
}