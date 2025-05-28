//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtJwtHandler.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 10:00
// 版本号 : V0.0.1
// 描述    : JWT令牌处理器实现
//===================================================================

#nullable enable

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Common.Constants;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices.Extensions;
using Lean.Hbt.Domain.IServices.Security;

namespace Lean.Hbt.Infrastructure.Authentication
{
    /// <summary>
    /// JWT处理器实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    public class HbtJwtHandler : IHbtJwtHandler
    {
        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly IHbtLogger _logger;

        private readonly HbtJwtOptions _jwtOptions;
        private readonly IDistributedCache? _distributedCache;
        private readonly bool _useDistributedCache;
        private readonly IHbtRepository<HbtUserTenant> _userTenantRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置接口</param>
        /// <param name="logger">日志接口</param>
        /// <param name="jwtOptions">JWT配置</param>
        /// <param name="userTenantRepository">用户租户仓库接口</param>
        /// <param name="distributedCache">分布式缓存接口</param>
        public HbtJwtHandler(
            IConfiguration configuration,
            IHbtLogger logger,
            IOptions<HbtJwtOptions> jwtOptions,
            IHbtRepository<HbtUserTenant> userTenantRepository,
            IDistributedCache? distributedCache = null)
        {
            _logger = logger;
            _jwtOptions = jwtOptions.Value;
            _distributedCache = distributedCache;
            _userTenantRepository = userTenantRepository;
            _useDistributedCache = _distributedCache != null && configuration.GetValue<bool>("UseDistributedCache", false);
        }

        /// <summary>
        /// 生成访问令牌
        /// </summary>
        public async Task<string> GenerateAccessTokenAsync(HbtUser user, HbtTenant tenant, string[] roles, string[] permissions)
        {
            try
            {
                _logger.Info("开始生成访问令牌，JWT配置信息: {@JwtConfig}", new
                {
                    SecretKeyLength = _jwtOptions?.SecretKey?.Length ?? 0,
                    Issuer = _jwtOptions?.Issuer,
                    Audience = _jwtOptions?.Audience,
                    ExpirationMinutes = _jwtOptions?.ExpirationMinutes,
                    UseDistributedCache = _useDistributedCache
                });

                if (string.IsNullOrEmpty(_jwtOptions?.SecretKey))
                {
                    _logger.Error("JWT SecretKey 未配置");
                    throw new HbtException("JWT配置错误：SecretKey未配置", "JWT_CONFIG_ERROR");
                }

                var claims = new List<Claim>
                {
                    new Claim("uid", user.Id.ToString()),          // 用户ID
                    new Claim("unm", user.UserName),               // 用户名
                    new Claim("nnm", user.NickName ?? string.Empty), // 昵称
                    new Claim("enm", user.EnglishName ?? string.Empty), // 英文名
                    new Claim("tid", tenant.Id.ToString()),        // 租户ID
                    new Claim("tnm", tenant.TenantName),           // 租户名称
                    new Claim("jti", Guid.NewGuid().ToString()),   // JWT ID
                    new Claim("typ", user.UserType.ToString()),    // 用户类型
                    new Claim("adm", (user.UserType == 2).ToString()) // 管理员标记
                };

                // 添加角色声明
                if (roles != null && roles.Length > 0)
                {
                    claims.AddRange(roles.Select(role => new Claim("rol", role)));
                }

                // 添加权限声明
                if (permissions != null && permissions.Length > 0)
                {
                    claims.AddRange(permissions.Select(permission => new Claim("pms", permission)));
                }

                // 验证租户信息
                if (tenant == null || tenant.Id <= 0)
                {
                    _logger.Error("生成访问令牌失败：租户信息无效");
                    throw new HbtException("租户信息无效", HbtConstants.ErrorCodes.InvalidTenant);
                }

                // 验证用户租户关系
                var userTenant = await _userTenantRepository.GetFirstAsync(x => x.UserId == user.Id && x.TenantId == tenant.Id);
                if (userTenant == null)
                {
                    _logger.Error("生成访问令牌失败：用户不属于指定租户");
                    throw new HbtException("用户不属于指定租户", HbtConstants.ErrorCodes.InvalidTenant);
                }

                if (userTenant.Status != 0)
                {
                    _logger.Error("生成访问令牌失败：用户在当前租户中已被禁用");
                    throw new HbtException("用户在当前租户中已被禁用", HbtConstants.ErrorCodes.UserDisabled);
                }

                _logger.Info("JWT Claims: {@Claims}", claims.Select(c => new { c.Type, c.Value }));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes),
                    signingCredentials: credentials);

                // 使用自定义的JwtSecurityTokenHandler
                var handler = new JwtSecurityTokenHandler
                {
                    MapInboundClaims = false
                };

                var tokenString = handler.WriteToken(token);
                _logger.Info("访问令牌生成成功: Length={Length}, Claims={@Claims}", 
                    tokenString?.Length ?? 0, 
                    claims.Select(c => new { c.Type, c.Value }));

                if (string.IsNullOrEmpty(tokenString))
                {
                    throw new HbtException("生成访问令牌失败：令牌为空", "JWT_GENERATE_ERROR");
                }

                return tokenString;
            }
            catch (Exception ex)
            {
                _logger.Error("生成访问令牌时发生错误: {Message}", ex.Message);
                throw new HbtException("生成访问令牌失败", "JWT_GENERATE_ERROR", ex);
            }
        }

        /// <summary>
        /// 生成刷新令牌
        /// </summary>
        public Task<string> GenerateRefreshTokenAsync()
        {
            return Task.FromResult(Guid.NewGuid().ToString("N"));
        }

        /// <summary>
        /// 验证访问令牌
        /// </summary>
        public Task<bool> ValidateAccessTokenAsync(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidIssuer = _jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtOptions.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    NameClaimType = "unm",
                    RoleClaimType = "rol",
                    RequireExpirationTime = true,
                    RequireSignedTokens = true,
                    ValidateTokenReplay = true,
                    RequireAudience = true
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                // 检查必要的Claims
                var userId = principal.FindFirst("uid")?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    _logger.Warn("Token验证失败: 缺少用户ID声明");
                    return Task.FromResult(false);
                }

                var userName = principal.FindFirst("unm")?.Value;
                if (string.IsNullOrEmpty(userName))
                {
                    _logger.Warn("Token验证失败: 缺少用户名声明");
                    return Task.FromResult(false);
                }

                var tenantId = principal.FindFirst("tid")?.Value;
                if (string.IsNullOrEmpty(tenantId))
                {
                    _logger.Warn("Token验证失败: 缺少租户ID声明");
                    return Task.FromResult(false);
                }

                _logger.Info("Token验证成功: UserId={UserId}, UserName={UserName}, TenantId={TenantId}",
                    userId, userName, tenantId);

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.Error("Token验证失败: {Message}", ex.Message);
                return Task.FromResult(false);
            }
        }

        /// <summary>
        /// 验证刷新令牌
        /// </summary>
        public Task<bool> ValidateRefreshTokenAsync(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidIssuer = _jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtOptions.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        /// <summary>
        /// 从令牌中获取用户ID
        /// </summary>
        public long GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "uid");
            if (userIdClaim == null)
            {
                throw new SecurityTokenException("Token missing required claim: uid");
            }
            return long.Parse(userIdClaim.Value);
        }
    }
}