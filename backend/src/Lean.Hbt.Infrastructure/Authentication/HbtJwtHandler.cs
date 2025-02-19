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

namespace Lean.Hbt.Infrastructure.Authentication
{
    /// <summary>
    /// JWT令牌处理器实现
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

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置接口</param>
        /// <param name="localizationService">本地化服务</param>
        /// <param name="userRepository">用户仓储</param>
        public HbtJwtHandler(
            IConfiguration configuration, 
            IHbtLocalizationService localizationService,
            IHbtRepository<HbtUser> userRepository)
        {
            _configuration = configuration;
            _localizationService = localizationService;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 生成访问令牌
        /// </summary>
        public async Task<string> GenerateAccessTokenAsync(HbtUser user)
        {
            try 
            {
                // 获取用户权限和角色
                var permissions = await _userRepository.GetUserPermissionsAsync(user.Id);
                var roles = await _userRepository.GetUserRolesAsync(user.Id);
                
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("tenant_id", user.TenantId.ToString())
                };

                // 添加角色claims
                if (roles != null && roles.Any())
                {
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                }

                // 添加权限claims
                if (permissions != null && permissions.Any())
                {
                    foreach (var permission in permissions)
                    {
                        claims.Add(new Claim("permissions", permission));
                    }
                }

                var secretKey = _configuration["Jwt:SecretKey"] ?? 
                    throw HbtException.ValidationError(await _localizationService.GetLocalizedStringAsync("Jwt.SecretKeyNotConfigured"));
                
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"] ?? 
                        throw HbtException.ValidationError(await _localizationService.GetLocalizedStringAsync("Jwt.IssuerNotConfigured")),
                    audience: _configuration["Jwt:Audience"] ?? 
                        throw HbtException.ValidationError(await _localizationService.GetLocalizedStringAsync("Jwt.AudienceNotConfigured")),
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationMinutes"] ?? "30")),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw HbtException.ValidationError($"生成访问令牌失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 生成刷新令牌
        /// </summary>
        public async Task<string> GenerateRefreshTokenAsync()
        {
            return await Task.FromResult(Guid.NewGuid().ToString("N"));
        }

        /// <summary>
        /// 验证访问令牌
        /// </summary>
        public async Task<bool> ValidateAccessToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var secretKey = _configuration["Jwt:SecretKey"] ?? throw HbtException.ValidationError(await _localizationService.GetLocalizedStringAsync("Jwt.SecretKeyNotConfigured"));
                var key = Encoding.UTF8.GetBytes(secretKey);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"] ?? throw HbtException.ValidationError(await _localizationService.GetLocalizedStringAsync("Jwt.IssuerNotConfigured")),
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"] ?? throw HbtException.ValidationError(await _localizationService.GetLocalizedStringAsync("Jwt.AudienceNotConfigured")),
                    ClockSkew = TimeSpan.Zero
                }, out _);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 验证刷新令牌
        /// </summary>
        public async Task<bool> ValidateRefreshToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var secretKey = _configuration["Security:Jwt:RefreshSecretKey"] ?? throw HbtException.ValidationError(await _localizationService.GetLocalizedStringAsync("Jwt.RefreshSecretKeyNotConfigured"));
                var key = Encoding.UTF8.GetBytes(secretKey);

                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Security:Jwt:Issuer"] ?? throw HbtException.ValidationError(await _localizationService.GetLocalizedStringAsync("Jwt.IssuerNotConfigured")),
                    ValidateAudience = true,
                    ValidAudience = _configuration["Security:Jwt:Audience"] ?? throw HbtException.ValidationError(await _localizationService.GetLocalizedStringAsync("Jwt.AudienceNotConfigured")),
                    ClockSkew = TimeSpan.Zero
                }, out _);

                // 验证令牌类型
                var tokenType = principal.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
                return tokenType == "refresh";
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
            var userIdClaim = jwtToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub);
            return long.Parse(userIdClaim.Value);
        }
    }
} 