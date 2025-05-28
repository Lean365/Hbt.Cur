//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtCurrentTenant.cs
// 创建者 : Lean365
// 创建时间: 2024-02-18 02:30
// 版本号 : V1.0.0
// 描述    : 当前租户实现
//===================================================================

#nullable enable

using System.Threading;
using Microsoft.AspNetCore.Http;
using Lean.Hbt.Domain.IServices.Extensions;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Constants;
using Microsoft.Extensions.Configuration;

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
                // 从JWT中获取租户ID
                var tenantIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("tid");
                if (tenantIdClaim != null && long.TryParse(tenantIdClaim.Value, out var tenantId))
                {
                    return tenantId;
                }

                // 如果JWT中没有租户ID，则返回静态属性中的值
                return _currentTenantId.Value.HasValue ? _currentTenantId.Value.Value : -1;
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
        /// 设置当前租户ID
        /// </summary>
        public void SetTenantId(long tenantId)
        {
            _currentTenantId.Value = tenantId;
        }

        /// <summary>
        /// 清除当前租户信息
        /// </summary>
        public void Clear()
        {
            _currentTenantId.Value = null;
        }
    }
}