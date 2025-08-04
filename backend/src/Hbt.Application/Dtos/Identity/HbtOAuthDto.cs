using System.ComponentModel.DataAnnotations;

namespace Hbt.Application.Dtos.Identity
{
    /// <summary>
    /// OAuth基础DTO（与HbtOAuth实体字段严格对应）
    /// </summary>
    public class HbtOAuthDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtOAuthDto()
        {
            Provider = string.Empty;
            OAuthUserId = string.Empty;
            OAuthUserName = string.Empty;
            OAuthNickName = string.Empty;
            OAuthEmail = string.Empty;
            OAuthAvatar = string.Empty;
        }

        /// <summary>
        /// OAuth ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long OAuthId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// OAuth提供商
        /// </summary>
        public string Provider { get; set; } = string.Empty;

        /// <summary>
        /// OAuth用户ID
        /// </summary>
        public string OAuthUserId { get; set; } = string.Empty;

        /// <summary>
        /// OAuth用户名
        /// </summary>
        public string OAuthUserName { get; set; } = string.Empty;

        /// <summary>
        /// OAuth用户昵称
        /// </summary>
        public string? OAuthNickName { get; set; }

        /// <summary>
        /// OAuth用户邮箱
        /// </summary>
        public string? OAuthEmail { get; set; }

        /// <summary>
        /// OAuth用户头像
        /// </summary>
        public string? OAuthAvatar { get; set; }

        /// <summary>
        /// 绑定时间
        /// </summary>
        public DateTime BindTime { get; set; }

        /// <summary>
        /// 是否为主要账号（0否 1是）
        /// </summary>
        public int IsPrimary { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// OAuth查询DTO
    /// </summary>
    public class HbtOAuthQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtOAuthQueryDto()
        {
            Provider = string.Empty;
            OAuthUserId = string.Empty;
            OAuthUserName = string.Empty;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// OAuth提供商
        /// </summary>
        public string? Provider { get; set; }

        /// <summary>
        /// OAuth用户ID
        /// </summary>
        public string? OAuthUserId { get; set; }

        /// <summary>
        /// OAuth用户名
        /// </summary>
        public string? OAuthUserName { get; set; }

        /// <summary>
        /// 是否为主要账号（0否 1是）
        /// </summary>
        public int? IsPrimary { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 绑定开始时间
        /// </summary>
        public DateTime? BindTimeStart { get; set; }

        /// <summary>
        /// 绑定结束时间
        /// </summary>
        public DateTime? BindTimeEnd { get; set; }
    }

    /// <summary>
    /// OAuth创建DTO
    /// </summary>
    public class HbtOAuthCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtOAuthCreateDto()
        {
            Provider = string.Empty;
            OAuthUserId = string.Empty;
            OAuthUserName = string.Empty;
            OAuthNickName = string.Empty;
            OAuthEmail = string.Empty;
            OAuthAvatar = string.Empty;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public long UserId { get; set; }

        /// <summary>
        /// OAuth提供商
        /// </summary>
        [Required(ErrorMessage = "OAuth提供商不能为空")]
        [StringLength(50, ErrorMessage = "OAuth提供商长度不能超过50个字符")]
        public string Provider { get; set; } = string.Empty;

        /// <summary>
        /// OAuth用户ID
        /// </summary>
        [Required(ErrorMessage = "OAuth用户ID不能为空")]
        [StringLength(100, ErrorMessage = "OAuth用户ID长度不能超过100个字符")]
        public string OAuthUserId { get; set; } = string.Empty;

        /// <summary>
        /// OAuth用户名
        /// </summary>
        [Required(ErrorMessage = "OAuth用户名不能为空")]
        [StringLength(100, ErrorMessage = "OAuth用户名长度不能超过100个字符")]
        public string OAuthUserName { get; set; } = string.Empty;

        /// <summary>
        /// OAuth用户昵称
        /// </summary>
        [StringLength(100, ErrorMessage = "OAuth用户昵称长度不能超过100个字符")]
        public string? OAuthNickName { get; set; }

        /// <summary>
        /// OAuth用户邮箱
        /// </summary>
        [EmailAddress(ErrorMessage = "OAuth用户邮箱格式不正确")]
        [StringLength(100, ErrorMessage = "OAuth用户邮箱长度不能超过100个字符")]
        public string? OAuthEmail { get; set; }

        /// <summary>
        /// OAuth用户头像
        /// </summary>
        [StringLength(500, ErrorMessage = "OAuth用户头像长度不能超过500个字符")]
        public string? OAuthAvatar { get; set; }

        /// <summary>
        /// 是否为主要账号（0否 1是）
        /// </summary>
        public int IsPrimary { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string? Remark { get; set; }
    }

    /// <summary>
    /// OAuth更新DTO
    /// </summary>
    public class HbtOAuthUpdateDto : HbtOAuthCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtOAuthUpdateDto() : base()
        {
        }

        /// <summary>
        /// OAuth ID
        /// </summary>
        [AdaptMember("Id")]
        [Required(ErrorMessage = "OAuth ID不能为空")]
        public long OAuthId { get; set; }
    }

    /// <summary>
    /// OAuth导入DTO
    /// </summary>
    public class HbtOAuthImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtOAuthImportDto()
        {
            Provider = string.Empty;
            OAuthUserId = string.Empty;
            OAuthUserName = string.Empty;
            OAuthNickName = string.Empty;
            OAuthEmail = string.Empty;
            OAuthAvatar = string.Empty;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// OAuth提供商
        /// </summary>
        public string Provider { get; set; } = string.Empty;

        /// <summary>
        /// OAuth用户ID
        /// </summary>
        public string OAuthUserId { get; set; } = string.Empty;

        /// <summary>
        /// OAuth用户名
        /// </summary>
        public string OAuthUserName { get; set; } = string.Empty;

        /// <summary>
        /// OAuth用户昵称
        /// </summary>
        public string? OAuthNickName { get; set; }

        /// <summary>
        /// OAuth用户邮箱
        /// </summary>
        public string? OAuthEmail { get; set; }

        /// <summary>
        /// OAuth用户头像
        /// </summary>
        public string? OAuthAvatar { get; set; }

        /// <summary>
        /// 是否为主要账号（0否 1是）
        /// </summary>
        public int IsPrimary { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; } = string.Empty;
    }

    /// <summary>
    /// OAuth导出DTO
    /// </summary>
    public class HbtOAuthExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtOAuthExportDto()
        {
            Provider = string.Empty;
            OAuthUserId = string.Empty;
            OAuthUserName = string.Empty;
            OAuthNickName = string.Empty;
            OAuthEmail = string.Empty;
            OAuthAvatar = string.Empty;
            UserName = string.Empty;
            NickName = string.Empty;
        }

        /// <summary>
        /// OAuth ID
        /// </summary>
        public long OAuthId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// OAuth提供商
        /// </summary>
        public string Provider { get; set; } = string.Empty;

        /// <summary>
        /// OAuth用户ID
        /// </summary>
        public string OAuthUserId { get; set; } = string.Empty;

        /// <summary>
        /// OAuth用户名
        /// </summary>
        public string OAuthUserName { get; set; } = string.Empty;

        /// <summary>
        /// OAuth用户昵称
        /// </summary>
        public string? OAuthNickName { get; set; }

        /// <summary>
        /// OAuth用户邮箱
        /// </summary>
        public string? OAuthEmail { get; set; }

        /// <summary>
        /// OAuth用户头像
        /// </summary>
        public string? OAuthAvatar { get; set; }

        /// <summary>
        /// 绑定时间
        /// </summary>
        public DateTime BindTime { get; set; }

        /// <summary>
        /// 是否为主要账号
        /// </summary>
        public string IsPrimary { get; set; } = string.Empty;

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// OAuth模板DTO
    /// </summary>
    public class HbtOAuthTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtOAuthTemplateDto()
        {
            Provider = string.Empty;
            OAuthUserId = string.Empty;
            OAuthUserName = string.Empty;
            OAuthNickName = string.Empty;
            OAuthEmail = string.Empty;
            OAuthAvatar = string.Empty;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// OAuth提供商
        /// </summary>
        public string Provider { get; set; } = string.Empty;

        /// <summary>
        /// OAuth用户ID
        /// </summary>
        public string OAuthUserId { get; set; } = string.Empty;

        /// <summary>
        /// OAuth用户名
        /// </summary>
        public string OAuthUserName { get; set; } = string.Empty;

        /// <summary>
        /// OAuth用户昵称
        /// </summary>
        public string? OAuthNickName { get; set; }

        /// <summary>
        /// OAuth用户邮箱
        /// </summary>
        public string? OAuthEmail { get; set; }

        /// <summary>
        /// OAuth用户头像
        /// </summary>
        public string? OAuthAvatar { get; set; }

        /// <summary>
        /// 是否为主要账号（0否 1是）
        /// </summary>
        public int IsPrimary { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; } = string.Empty;
    }

    /// <summary>
    /// OAuth状态DTO
    /// </summary>
    public class HbtOAuthStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtOAuthStatusDto()
        {
        }

        /// <summary>
        /// OAuth ID
        /// </summary>
        [AdaptMember("Id")]
        [Required(ErrorMessage = "OAuth ID不能为空")]
        public long OAuthId { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string? Remark { get; set; } = string.Empty;
    }

    /// <summary>
    /// OAuth绑定DTO
    /// </summary>
    public class HbtOAuthBindDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtOAuthBindDto()
        {
            Provider = string.Empty;
            OAuthUserId = string.Empty;
            OAuthUserInfo = new();
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public long UserId { get; set; }

        /// <summary>
        /// OAuth提供商
        /// </summary>
        [Required(ErrorMessage = "OAuth提供商不能为空")]
        public string Provider { get; set; } = string.Empty;

        /// <summary>
        /// OAuth用户ID
        /// </summary>
        [Required(ErrorMessage = "OAuth用户ID不能为空")]
        public string OAuthUserId { get; set; } = string.Empty;

        /// <summary>
        /// OAuth用户信息
        /// </summary>
        public object OAuthUserInfo { get; set; }
    }

    /// <summary>
    /// OAuth解绑DTO
    /// </summary>
    public class HbtOAuthUnbindDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtOAuthUnbindDto()
        {
            Provider = string.Empty;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public long UserId { get; set; }

        /// <summary>
        /// OAuth提供商
        /// </summary>
        [Required(ErrorMessage = "OAuth提供商不能为空")]
        public string Provider { get; set; } = string.Empty;
    }

    /// <summary>
    /// OAuth登录结果DTO
    /// </summary>
    public class HbtOAuthLoginResultDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtOAuthLoginResultDto()
        {
            AccessToken = string.Empty;
            RefreshToken = string.Empty;
            UserName = string.Empty;
            NickName = string.Empty;
            Avatar = string.Empty;
            Email = string.Empty;
        }

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
    }
}