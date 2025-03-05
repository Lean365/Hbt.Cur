using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.Identity
{
    /// <summary>
    /// OAuth登录结果DTO
    /// </summary>
    public class HbtOAuthLoginDto
    {
        /// <summary>
        /// 访问令牌
        /// </summary>
        [Required]
        public string AccessToken { get; set; } = string.Empty;

        /// <summary>
        /// 刷新令牌
        /// </summary>
        [Required]
        public string RefreshToken { get; set; } = string.Empty;

        /// <summary>
        /// 过期时间(分钟)
        /// </summary>
        [Required]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 昵称
        /// </summary>
        [Required]
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 头像
        /// </summary>
        public string? Avatar { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 是否需要选择租户
        /// </summary>
        public bool NeedSelectTenant { get; set; }
    }
} 