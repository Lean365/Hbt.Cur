//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtJwtHandler.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-16 10:00
// 版本号 : V0.0.1
// 描述    : JWT令牌处理器实现
//===================================================================

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lean.Hbt.Domain.IServices.Security;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Domain.IServices.Admin;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lean.Hbt.Common.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

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
        /// 配置接口
        /// </summary>
        private readonly IConfiguration _configuration;
        private readonly IHbtLocalizationService _localizationService;
        private readonly IHbtRepository<HbtUser> _userRepository;
        private readonly ILogger<HbtJwtHandler> _logger;
        private readonly HbtJwtOptions _jwtOptions;
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _distributedCache;
        private readonly bool _useDistributedCache;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置接口</param>
        /// <param name="localizationService">本地化服务</param>
        /// <param name="userRepository">用户仓储</param>
        /// <param name="logger">日志接口</param>
        /// <param name="jwtOptions">JWT配置</param>
        /// <param name="memoryCache">内存缓存接口</param>
        /// <param name="distributedCache">分布式缓存接口</param>
        public HbtJwtHandler(
            IConfiguration configuration, 
            IHbtLocalizationService localizationService,
            IHbtRepository<HbtUser> userRepository,
            ILogger<HbtJwtHandler> logger,
            IOptions<HbtJwtOptions> jwtOptions,
            IMemoryCache memoryCache,
            IDistributedCache distributedCache = null)
        {
            _configuration = configuration;
            _localizationService = localizationService;
            _userRepository = userRepository;
            _logger = logger;
            _jwtOptions = jwtOptions.Value;
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
            _useDistributedCache = _distributedCache != null && configuration.GetValue<bool>("UseDistributedCache", false);
        }

        /// <summary>
        /// 生成访问令牌
        /// </summary>
        public async Task<string> GenerateAccessTokenAsync(HbtUser user, HbtTenant tenant, string[] roles, string[] permissions)
        {
            try
            {
                _logger.LogInformation("开始生成访问令牌，JWT配置信息: {@JwtConfig}", new
                {
                    SecretKeyLength = _jwtOptions?.SecretKey?.Length ?? 0,
                    Issuer = _jwtOptions?.Issuer,
                    Audience = _jwtOptions?.Audience,
                    ExpirationMinutes = _jwtOptions?.ExpirationMinutes,
                    UseDistributedCache = _useDistributedCache
                });

                if (string.IsNullOrEmpty(_jwtOptions?.SecretKey))
                {
                    _logger.LogError("JWT SecretKey 未配置");
                    throw new HbtException("JWT配置错误：SecretKey未配置", "JWT_CONFIG_ERROR");
                }

                var claims = new List<Claim>
                {
                    new Claim("uid", user.Id.ToString()),          // 用户ID
                    new Claim("unm", user.UserName),               // 用户名
                    new Claim("nnm", user.NickName ?? string.Empty), // 昵称
                    new Claim("enm", user.EnglishName ?? string.Empty), // 英文名
                    new Claim("tid", user.TenantId.ToString()),    // 租户ID
                    new Claim("tnm", tenant.TenantName),           // 租户名称
                    new Claim("jti", Guid.NewGuid().ToString()),   // JWT ID
                    new Claim("typ", user.UserType.ToString()),    // 用户类型
                    new Claim("adm", (user.UserType == 2).ToString()) // 管理员标记
                };

                // 添加角色和权限
                if (roles?.Any() == true)
                {
                    // 只添加第一个角色，其他角色通过权限控制
                    claims.Add(new Claim("rol", roles[0]));        // 主要角色
                }

                if (permissions?.Any() == true)
                {
                    // 生成权限缓存key
                    var permissionKey = $"pms:{user.Id}:{Guid.NewGuid():N}";
                    var expiration = TimeSpan.FromMinutes(_jwtOptions.ExpirationMinutes);

                    if (_useDistributedCache)
                    {
                        // 使用分布式缓存(Redis)
                        var permissionsJson = JsonSerializer.Serialize(permissions);
                        var permissionsBytes = Encoding.UTF8.GetBytes(permissionsJson);
                        var options = new DistributedCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = expiration
                        };
                        await _distributedCache.SetAsync(permissionKey, permissionsBytes, options);
                    }
                    else
                    {
                        // 使用内存缓存
                        var cacheOptions = new MemoryCacheEntryOptions()
                            .SetAbsoluteExpiration(expiration);
                        _memoryCache.Set(permissionKey, permissions, cacheOptions);
                    }
                    
                    // JWT中只存储权限的引用key
                    claims.Add(new Claim("pms_key", permissionKey));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes),
                    signingCredentials: credentials);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                _logger.LogInformation("访问令牌生成成功: Length={Length}", tokenString?.Length ?? 0);

                return tokenString;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "生成访问令牌时发生错误: {Message}", ex.Message);
                throw new HbtException("生成访问令牌失败", "JWT_GENERATE_ERROR", ex);
            }
        }

        /// <summary>
        /// 生成刷新令牌
        /// </summary>
        public async Task<string> GenerateRefreshTokenAsync()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 验证访问令牌
        /// </summary>
        public async Task<bool> ValidateAccessTokenAsync(string token)
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

                // 检查必要的Claims
                var userId = principal.FindFirst("uid")?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    throw new SecurityTokenException("Token missing required claim: uid");
                }

                var userName = principal.FindFirst("unm")?.Value;
                if (string.IsNullOrEmpty(userName))
                {
                    throw new SecurityTokenException("Token missing required claim: unm");
                }

                var tenantId = principal.FindFirst("tid")?.Value;
                if (string.IsNullOrEmpty(tenantId))
                {
                    throw new SecurityTokenException("Token missing required claim: tid");
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "验证访问令牌时发生错误: {Message}", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 验证刷新令牌
        /// </summary>
        public async Task<bool> ValidateRefreshTokenAsync(string token)
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
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 从令牌中获取用户ID
        /// </summary>
        public long GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var userIdClaim = jwtToken.Claims.First(c => c.Type == "uid");
            return long.Parse(userIdClaim.Value);
        }
    }
} 