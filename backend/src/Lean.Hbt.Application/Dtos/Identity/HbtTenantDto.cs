//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTenantDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 18:00
// 版本号 : V0.0.1
// 描述   : 租户数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.Identity;

/// <summary>
/// 租户状态传输对象
/// </summary>
public class HbtTenantStatusDto
{
    /// <summary>
    /// 租户ID
    /// </summary>
    [AdaptMember("Id")]
    public long TenantId { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 租户基础传输对象
/// </summary>
public class HbtTenantDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtTenantDto()
    {
        TenantName = string.Empty;
        TenantCode = string.Empty;
        ContactPerson = string.Empty;
        ContactPhone = string.Empty;
        ContactEmail = string.Empty;
        Address = string.Empty;
        Domain = string.Empty;
        LogoUrl = string.Empty;
        Theme = string.Empty;
    }

    /// <summary>
    /// 租户ID
    /// </summary>
    [AdaptMember("Id")]
    public long TenantId { get; set; }

    /// <summary>
    /// 租户名称
    /// </summary>
    [Required]
    public string TenantName { get; set; }

    /// <summary>
    /// 租户编码
    /// </summary>
    [Required]
    public string TenantCode { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>
    [Required]
    public string ContactPerson { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    [Required]
    public string ContactPhone { get; set; }

    /// <summary>
    /// 联系邮箱
    /// </summary>
    [Required]
    public string ContactEmail { get; set; }

    /// <summary>
    /// 租户地址
    /// </summary>
    [Required]
    public string Address { get; set; }

    /// <summary>
    /// 租户域名
    /// </summary>
    [Required]
    public string Domain { get; set; }

    /// <summary>
    /// 租户Logo
    /// </summary>
    [Required]
    public string LogoUrl { get; set; }

    /// <summary>
    /// 租户主题
    /// </summary>
    [Required]
    public string Theme { get; set; }

    /// <summary>
    /// 授权开始时间
    /// </summary>
    public DateTime? LicenseStartTime { get; set; }

    /// <summary>
    /// 授权结束时间
    /// </summary>
    public DateTime? LicenseEndTime { get; set; }

    /// <summary>
    /// 最大用户数
    /// </summary>
    public int MaxUserCount { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// 租户查询传输对象
/// </summary>
public class HbtTenantQueryDto : HbtPagedQuery
{
    /// <summary>
    /// 租户编码
    /// </summary>
    public string? TenantCode { get; set; }

    /// <summary>
    /// 租户名称
    /// </summary>
    public string? TenantName { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>
    public string? ContactPerson { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string? ContactPhone { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }
}

/// <summary>
/// 租户创建传输对象
/// </summary>
public class HbtTenantCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtTenantCreateDto()
    {
        TenantName = string.Empty;
        TenantCode = string.Empty;
        ContactUser = string.Empty;
        ContactPhone = string.Empty;
        ContactEmail = string.Empty;
        Address = string.Empty;
        Domain = string.Empty;
        DbConnection = string.Empty;
        LogoUrl = string.Empty;
        Theme = string.Empty;
    }

    /// <summary>
    /// 租户名称
    /// </summary>
    [Required(ErrorMessage = "租户名称不能为空")]
    [MaxLength(50, ErrorMessage = "租户名称长度不能超过50个字符")]
    public string TenantName { get; set; }

    /// <summary>
    /// 租户编码
    /// </summary>
    [Required(ErrorMessage = "租户编码不能为空")]
    [MaxLength(50, ErrorMessage = "租户编码长度不能超过50个字符")]
    public string TenantCode { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>
    [Required(ErrorMessage = "联系人不能为空")]
    [MaxLength(20, ErrorMessage = "联系人长度不能超过20个字符")]
    public string ContactUser { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    [Required(ErrorMessage = "联系电话不能为空")]
    [MaxLength(20, ErrorMessage = "联系电话长度不能超过20个字符")]
    public string ContactPhone { get; set; }

    /// <summary>
    /// 联系邮箱
    /// </summary>
    [Required(ErrorMessage = "联系邮箱不能为空")]
    [MaxLength(50, ErrorMessage = "联系邮箱长度不能超过50个字符")]
    [EmailAddress(ErrorMessage = "联系邮箱格式不正确")]
    public string ContactEmail { get; set; }

    /// <summary>
    /// 租户地址
    /// </summary>
    [MaxLength(200, ErrorMessage = "租户地址长度不能超过200个字符")]
    public string Address { get; set; }

    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    [Required(ErrorMessage = "数据库连接字符串不能为空")]
    [MaxLength(500, ErrorMessage = "数据库连接字符串长度不能超过500个字符")]
    public string DbConnection { get; set; }

    /// <summary>
    /// 租户域名
    /// </summary>
    [MaxLength(100, ErrorMessage = "租户域名长度不能超过100个字符")]
    public string Domain { get; set; }

    /// <summary>
    /// 租户Logo
    /// </summary>
    [MaxLength(200, ErrorMessage = "租户Logo长度不能超过200个字符")]
    public string LogoUrl { get; set; }

    /// <summary>
    /// 租户主题
    /// </summary>
    [MaxLength(50, ErrorMessage = "租户主题长度不能超过50个字符")]
    public string Theme { get; set; }

    /// <summary>
    /// 授权开始时间
    /// </summary>
    public DateTime? LicenseStartTime { get; set; }

    /// <summary>
    /// 授权结束时间
    /// </summary>
    public DateTime? LicenseEndTime { get; set; }

    /// <summary>
    /// 最大用户数
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "最大用户数必须大于0")]
    public int MaxUserCount { get; set; }
}

/// <summary>
/// 租户更新传输对象
/// </summary>
public class HbtTenantUpdateDto : HbtTenantCreateDto
{
    /// <summary>
    /// 租户ID
    /// </summary>
    [Required(ErrorMessage = "租户ID不能为空")]
    [AdaptMember("Id")]
    public long TenantId { get; set; }
}

/// <summary>
/// 租户导出传输对象
/// </summary>
public class HbtTenantExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtTenantExportDto()
    {
        TenantName = string.Empty;
        TenantCode = string.Empty;
        ContactPerson = string.Empty;
        ContactPhone = string.Empty;
        ContactEmail = string.Empty;
        Address = string.Empty;
        Domain = string.Empty;
    }

    /// <summary>
    /// 租户名称
    /// </summary>
    public string TenantName { get; set; }

    /// <summary>
    /// 租户编码
    /// </summary>
    public string TenantCode { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>
    public string ContactPerson { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string ContactPhone { get; set; }

    /// <summary>
    /// 联系邮箱
    /// </summary>
    public string ContactEmail { get; set; }

    /// <summary>
    /// 租户地址
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// 租户域名
    /// </summary>
    public string Domain { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// 租户导入模板传输对象
/// </summary>
public class HbtTenantTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtTenantTemplateDto()
    {
        TenantName = string.Empty;
        TenantCode = string.Empty;
        ContactPerson = string.Empty;
        ContactPhone = string.Empty;
        ContactEmail = string.Empty;
        Address = string.Empty;
        Domain = string.Empty;
        Theme = string.Empty;
        LicenseStartTime = string.Empty;
        LicenseEndTime = string.Empty;
        MaxUserCount = string.Empty;
    }

    /// <summary>
    /// 租户编码
    /// </summary>
    public string TenantCode { get; set; }

    /// <summary>
    /// 租户名称
    /// </summary>
    public string TenantName { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>
    public string ContactPerson { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string ContactPhone { get; set; }

    /// <summary>
    /// 联系邮箱
    /// </summary>
    public string ContactEmail { get; set; }

    /// <summary>
    /// 租户地址
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// 租户域名
    /// </summary>
    public string Domain { get; set; }

    /// <summary>
    /// 租户主题
    /// </summary>
    public string Theme { get; set; }

    /// <summary>
    /// 授权开始时间
    /// </summary>
    public string LicenseStartTime { get; set; }

    /// <summary>
    /// 授权结束时间
    /// </summary>
    public string LicenseEndTime { get; set; }

    /// <summary>
    /// 最大用户数
    /// </summary>
    public string MaxUserCount { get; set; }
}

/// <summary>
/// 租户导入传输对象
/// </summary>
public class HbtTenantImportDto
{
    /// <summary>
    /// 租户名称
    /// </summary>
    [Description("租户名称")]
    public string TenantName { get; set; } = string.Empty;

    /// <summary>
    /// 租户编码
    /// </summary>
    [Description("租户编码")]
    public string TenantCode { get; set; } = string.Empty;

    /// <summary>
    /// 联系人
    /// </summary>
    [Description("联系人")]
    public string? ContactPerson { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    [Description("联系电话")]
    public string? ContactPhone { get; set; }

    /// <summary>
    /// 联系邮箱
    /// </summary>
    [Description("联系邮箱")]
    public string? ContactEmail { get; set; }

    /// <summary>
    /// 域名
    /// </summary>
    [Description("域名")]
    public string? Domain { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    [Description("地址")]
    public string? Address { get; set; }

    /// <summary>
    /// Logo地址
    /// </summary>
    [Description("Logo地址")]
    public string? LogoUrl { get; set; }

    /// <summary>
    /// 主题
    /// </summary>
    [Description("主题")]
    public string? Theme { get; set; }

    /// <summary>
    /// 许可证开始时间
    /// </summary>
    [Description("许可证开始时间")]
    public DateTime? LicenseStartTime { get; set; }

    /// <summary>
    /// 许可证结束时间
    /// </summary>
    [Description("许可证结束时间")]
    public DateTime? LicenseEndTime { get; set; }

    /// <summary>
    /// 最大用户数
    /// </summary>
    [Description("最大用户数")]
    public int? MaxUserCount { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [Description("备注")]
    public string? Remark { get; set; }
}