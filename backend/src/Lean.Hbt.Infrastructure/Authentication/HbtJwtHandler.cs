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

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置接口</param>
        public HbtJwtHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 生成JWT令牌
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="roles">用户角色列表</param>
        /// <returns>JWT令牌字符串</returns>
        public string GenerateToken(string userId, string userName, IEnumerable<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Name, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["HbtJwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["HbtJwt:Issuer"],
                audience: _configuration["HbtJwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["HbtJwt:ExpiryInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// 验证JWT令牌
        /// </summary>
        /// <param name="token">JWT令牌字符串</param>
        /// <returns>验证通过返回声明主体，否则返回null</returns>
        public ClaimsPrincipal? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["HbtJwt:SecretKey"]);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["HbtJwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["HbtJwt:Audience"],
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
} 