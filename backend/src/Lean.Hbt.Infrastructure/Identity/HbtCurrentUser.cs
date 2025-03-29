//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : CurrentUser.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 当前用户实现类
//===================================================================

using System.Security.Claims;
using Lean.Hbt.Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Lean.Hbt.Infrastructure.Identity
{
    /// <summary>
    /// 当前用户实现类
    /// </summary>
    /// <remarks>
    /// 用于获取当前登录用户的相关信息，包括：
    /// 1. 用户ID
    /// 2. 用户名
    /// 3. 租户ID
    /// 4. 认证状态
    /// </remarks>
    public class HbtCurrentUser : IHbtCurrentUser
    {
        /// <summary>
        /// HTTP上下文访问器
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<HbtCurrentUser> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpContextAccessor">HTTP上下文访问器，用于获取当前请求的上下文信息</param>
        /// <param name="logger">日志记录器</param>
        public HbtCurrentUser(IHttpContextAccessor httpContextAccessor, ILogger<HbtCurrentUser> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        /// <summary>
        /// 获取当前用户ID
        /// </summary>
        /// <remarks>
        /// 从HTTP上下文中获取用户ID，如果未找到则返回0
        /// </remarks>
        public long UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst("user_id");
                return userIdClaim != null ? long.Parse(userIdClaim.Value) : 0;
            }
        }

        /// <summary>
        /// 获取当前用户名
        /// </summary>
        /// <remarks>
        /// 从HTTP上下文中获取用户名，如果未找到则返回 Hbt365
        /// </remarks>
        public string UserName
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "Hbt365";
            }
        }

        /// <summary>
        /// 获取当前租户ID
        /// </summary>
        /// <remarks>
        /// 从HTTP上下文中获取租户ID，如果未找到则返回0
        /// </remarks>
        public long TenantId
        {
            get
            {
                var tenantIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst("TenantId");
                return tenantIdClaim != null ? long.Parse(tenantIdClaim.Value) : 0;
            }
        }

        /// <summary>
        /// 获取当前用户是否已认证
        /// </summary>
        /// <remarks>
        /// 检查当前用户的认证状态，如果未认证或上下文不存在则返回false
        /// </remarks>
        public bool IsAuthenticated
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
            }
        }

        /// <summary>
        /// 获取当前用户类型（0：系统用户，1：普通用户，2：管理员，3：OAuth用户）
        /// </summary>
        public int UserType
        {
            get
            {
                var userTypeClaim = _httpContextAccessor.HttpContext?.User.FindFirst("user_type");
                if (userTypeClaim == null)
                {
                    _logger.LogWarning("未找到用户类型声明");
                    return 1;
                }

                // 打印所有 Claims
                var claims = _httpContextAccessor.HttpContext?.User.Claims;
                if (claims != null)
                {
                    _logger.LogInformation("当前用户的所有声明：");
                    foreach (var claim in claims)
                    {
                        _logger.LogInformation("Claim类型: {ClaimType}, 值: {ClaimValue}", claim.Type, claim.Value);
                    }
                }

                return int.Parse(userTypeClaim.Value);
            }
        }
    }
} 