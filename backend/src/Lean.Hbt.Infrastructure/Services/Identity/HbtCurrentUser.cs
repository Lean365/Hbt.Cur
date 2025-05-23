//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : CurrentUser.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 当前用户实现类
//===================================================================

using System.Security.Claims;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Constants;
using Lean.Hbt.Domain.IServices.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Lean.Hbt.Infrastructure.Services.Identity
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
    /// 5. 用户类型
    /// 6. 英文名称
    /// 7. 角色和权限
    /// </remarks>
    public class HbtCurrentUser : IHbtCurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<HbtCurrentUser> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="logger">日志记录器</param>
        public HbtCurrentUser(IHttpContextAccessor httpContextAccessor, ILogger<HbtCurrentUser> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        /// <summary>
        /// 获取当前用户ID
        /// </summary>
        public long UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst("uid");
                return userIdClaim != null ? long.Parse(userIdClaim.Value) : 0;
            }
        }

        /// <summary>
        /// 获取当前用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User.FindFirst("unm")?.Value ?? "Hbt365";
            }
        }

        /// <summary>
        /// 获取当前用户昵称
        /// </summary>
        public string NickName
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User.FindFirst("nnm")?.Value ?? string.Empty;
            }
        }

        /// <summary>
        /// 获取当前用户英文名称
        /// </summary>
        public string EnglishName
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User.FindFirst("enm")?.Value ?? string.Empty;
            }
        }

        /// <summary>
        /// 获取当前用户类型
        /// </summary>
        public int UserType
        {
            get
            {
                var userTypeClaim = _httpContextAccessor.HttpContext?.User.FindFirst("typ");
                return userTypeClaim != null ? int.Parse(userTypeClaim.Value) : 1; // 默认为普通用户
            }
        }

        /// <summary>
        /// 获取当前租户ID
        /// </summary>
        public long TenantId
        {
            get
            {
                var tenantIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst("tid");
                if (tenantIdClaim == null)
                {
                    _logger.LogWarning("未找到租户ID声明，请确保用户已正确登录并包含租户信息");
                    throw new HbtException("未找到租户信息，请重新登录", HbtConstants.ErrorCodes.Unauthorized);
                }
                return long.Parse(tenantIdClaim.Value);
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
        /// 获取当前用户角色列表
        /// </summary>
        public string[] Roles
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User.FindAll("rol").Select(c => c.Value).ToArray() ?? Array.Empty<string>();
            }
        }

        /// <summary>
        /// 获取当前用户权限列表
        /// </summary>
        public string[] Permissions
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User.FindAll("pms").Select(c => c.Value).ToArray() ?? Array.Empty<string>();
            }
        }

        /// <summary>
        /// 获取当前用户是否已认证
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
            }
        }

        /// <summary>
        /// 获取当前用户是否为超级管理员
        /// </summary>
        public bool IsAdmin
        {
            get
            {
                return Roles.Contains("Hbt365");
            }
        }
    }
} 