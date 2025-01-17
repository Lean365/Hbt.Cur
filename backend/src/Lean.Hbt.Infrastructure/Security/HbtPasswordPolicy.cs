//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtPasswordPolicy.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-16 10:00
// 版本号 : V0.0.1
// 描述    : 密码策略实现
//===================================================================

using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;

namespace Lean.Hbt.Infrastructure.Security
{
    /// <summary>
    /// 密码策略实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    public class HbtPasswordPolicy : IHbtPasswordPolicy
    {
        private readonly PasswordPolicyOptions _options;

        public HbtPasswordPolicy(IOptions<HbtSecurityOptions> options)
        {
            _options = options.Value.PasswordPolicy;
        }

        /// <summary>
        /// 验证密码强度
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>验证结果</returns>
        public (bool IsValid, string Message) ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return (false, "密码不能为空");
            }

            if (password.Length < _options.MinLength)
            {
                return (false, $"密码长度不能小于{_options.MinLength}位");
            }

            if (password.Length > _options.MaxLength)
            {
                return (false, $"密码长度不能大于{_options.MaxLength}位");
            }

            if (_options.RequireDigit && !password.Any(char.IsDigit))
            {
                return (false, "密码必须包含数字");
            }

            if (_options.RequireLowercase && !password.Any(char.IsLower))
            {
                return (false, "密码必须包含小写字母");
            }

            if (_options.RequireUppercase && !password.Any(char.IsUpper))
            {
                return (false, "密码必须包含大写字母");
            }

            if (_options.RequireSpecialChar && !password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                return (false, "密码必须包含特殊字符");
            }

            return (true, "密码验证通过");
        }

        /// <summary>
        /// 生成密码哈希
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>密码哈希</returns>
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
        }

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="passwordHash">密码哈希</param>
        /// <returns>是否匹配</returns>
        public bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
} 