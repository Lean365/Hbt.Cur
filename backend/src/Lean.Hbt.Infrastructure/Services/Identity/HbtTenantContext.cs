//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTenantContext.cs
// 创建者 : Lean365
// 创建时间: 2024-02-18 02:30
// 版本号 : V1.0.0
// 描述    : 租户上下文实现
//===================================================================

using System.Threading;
using Microsoft.AspNetCore.Http;
using Lean.Hbt.Domain.IServices.Identity;

namespace Lean.Hbt.Infrastructure.Services.Identity
{
    /// <summary>
    /// 租户上下文实现
    /// </summary>
    public class HbtTenantContext : IHbtTenantContext
    {
        private static AsyncLocal<long?> _currentTenantId = new AsyncLocal<long?>();
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtTenantContext(IHttpContextAccessor httpContextAccessor)
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
                // 1. 首先尝试从请求头中获取租户ID
                if (_httpContextAccessor.HttpContext?.Request.Headers.TryGetValue("X-Tenant-Id", out var headerTenantId) == true)
                {
                    if (long.TryParse(headerTenantId, out var id))
                    {
                        CurrentTenantId = id;
                        return id;
                    }
                }

                // 2. 如果请求头中没有，尝试从JWT令牌中获取
                var tenantId = _httpContextAccessor.HttpContext?.User.FindFirst("tenant_id")?.Value;
                if (!string.IsNullOrEmpty(tenantId) && long.TryParse(tenantId, out var tid))
                {
                    CurrentTenantId = tid;
                    return tid;
                }

                // 3. 如果都没有，返回当前存储的租户ID或默认值0
                return CurrentTenantId ?? 0;
            }
        }

        /// <summary>
        /// 获取当前租户名称
        /// </summary>
        public string TenantName
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User.FindFirst("tenant_name")?.Value ?? string.Empty;
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