//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTenantDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 19:15
// 版本号 : V0.0.1
// 描述   : 租户传输对象
//===================================================================

namespace Lean.Hbt.Application.Dtos.Identity;

/// <summary>
/// 租户传输对象
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
        ContactUser = string.Empty;
        ContactPhone = string.Empty;
        ContactEmail = string.Empty;
        Address = string.Empty;
        LicenseType = string.Empty;
        LicenseKey = string.Empty;
        Domain = string.Empty;
        LogoUrl = string.Empty;
        Theme = string.Empty;
        Remark = string.Empty;
        CreateBy = string.Empty;
        UpdateBy = string.Empty;
        DeleteBy = string.Empty;
        ConfigId = string.Empty;
    }

    /// <summary>
    /// 租户ID
    /// </summary>
    [AdaptMember("Id")]
    public long TenantId { get; set; }

    /// <summary>
    /// ConfigId（多库唯一标识）
    /// </summary>
    public string ConfigId { get; set; }

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
    public string ContactUser { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string ContactPhone { get; set; }

    /// <summary>
    /// 联系邮箱
    /// </summary>
    public string ContactEmail { get; set; }

    /// <summary>
    /// 联系地址
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// 许可证类型
    /// </summary>
    public string? LicenseType { get; set; }

    /// <summary>
    /// 许可注册码
    /// </summary>
    public string? LicenseKey { get; set; }

    /// <summary>
    /// 过期时间
    /// </summary>
    public DateTime? ExpireTime { get; set; }

    /// <summary>
    /// 租户域名
    /// </summary>
    public string Domain { get; set; }

    /// <summary>
    /// 租户Logo
    /// </summary>
    public string? LogoUrl { get; set; }

    /// <summary>
    /// 租户主题
    /// </summary>
    public string? Theme { get; set; }

    /// <summary>
    /// 许可证开始时间
    /// </summary>
    public DateTime? LicenseStartTime { get; set; }

    /// <summary>
    /// 许可证结束时间
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
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public string CreateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 更新人
    /// </summary>
    public string UpdateBy { get; set; }

    /// <summary>
    /// 是否删除
    /// </summary>
    public int IsDeleted { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    public DateTime? DeleteTime { get; set; }

    /// <summary>
    /// 删除人
    /// </summary>
    public string? DeleteBy { get; set; }
}

/// <summary>
/// 租户查询传输对象
/// </summary>
public class HbtTenantQueryDto : HbtPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtTenantQueryDto()
    {
        TenantCode = string.Empty;
        TenantName = string.Empty;
        ContactUser = string.Empty;
        ContactPhone = string.Empty;
        ConfigId = string.Empty;
    }

    /// <summary>
    /// ConfigId（多库唯一标识）
    /// </summary>
    public string ConfigId { get; set; }

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
    public string? ContactUser { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string? ContactPhone { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? BeginTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }
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
        LicenseType = string.Empty;
        LicenseKey = string.Empty;
        Domain = string.Empty;
        LogoUrl = string.Empty;
        Theme = string.Empty;
        ConfigId = string.Empty;
    }

    /// <summary>
    /// ConfigId（多库唯一标识）
    /// </summary>
    public string ConfigId { get; set; }

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
    public string ContactUser { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string ContactPhone { get; set; }

    /// <summary>
    /// 联系邮箱
    /// </summary>
    public string ContactEmail { get; set; }

    /// <summary>
    /// 联系地址
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// 许可证类型
    /// </summary>
    public string? LicenseType { get; set; }

    /// <summary>
    /// 许可注册码
    /// </summary>
    public string? LicenseKey { get; set; }

    /// <summary>
    /// 过期时间
    /// </summary>
    public DateTime? ExpireTime { get; set; }

    /// <summary>
    /// 租户域名
    /// </summary>
    public string Domain { get; set; }

    /// <summary>
    /// 租户Logo
    /// </summary>
    public string? LogoUrl { get; set; }

    /// <summary>
    /// 租户主题
    /// </summary>
    public string? Theme { get; set; }

    /// <summary>
    /// 许可证开始时间
    /// </summary>
    public DateTime? LicenseStartTime { get; set; }

    /// <summary>
    /// 许可证结束时间
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
}

/// <summary>
/// 租户更新传输对象
/// </summary>
public class HbtTenantUpdateDto : HbtTenantCreateDto
{
    /// <summary>
    /// 租户ID
    /// </summary>
    public long TenantId { get; set; }
}

/// <summary>
/// 租户状态传输对象
/// </summary>
public class HbtTenantStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtTenantStatusDto()
    {
        StatusName = string.Empty;
        ConfigId = string.Empty;
    }

    /// <summary>
    /// ConfigId（多库唯一标识）
    /// </summary>
    public string ConfigId { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    public long TenantId { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 状态名称
    /// </summary>
    public string StatusName { get; set; }
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
        TenantCode = string.Empty;
        TenantName = string.Empty;
        ContactUser = string.Empty;
        ContactPhone = string.Empty;
        ContactEmail = string.Empty;
        Address = string.Empty;
        LicenseType = string.Empty;
        LicenseKey = string.Empty;
        Domain = string.Empty;
        Theme = string.Empty;
        LicenseStartTime = string.Empty;
        LicenseEndTime = string.Empty;
        MaxUserCount = string.Empty;
        ConfigId = string.Empty;
    }

    /// <summary>
    /// ConfigId（多库唯一标识）
    /// </summary>
    public string ConfigId { get; set; }

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
    public string ContactUser { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string ContactPhone { get; set; }

    /// <summary>
    /// 联系邮箱
    /// </summary>
    public string ContactEmail { get; set; }

    /// <summary>
    /// 联系地址
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// 许可证类型
    /// </summary>
    public string LicenseType { get; set; }

    /// <summary>
    /// 许可注册码
    /// </summary>
    public string LicenseKey { get; set; }

    /// <summary>
    /// 租户域名
    /// </summary>
    public string Domain { get; set; }

    /// <summary>
    /// 租户主题
    /// </summary>
    public string Theme { get; set; }

    /// <summary>
    /// 许可证开始时间
    /// </summary>
    public string LicenseStartTime { get; set; }

    /// <summary>
    /// 许可证结束时间
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
    /// 构造函数
    /// </summary>
    public HbtTenantImportDto()
    {
        TenantName = string.Empty;
        TenantCode = string.Empty;
        ContactUser = string.Empty;
        ContactPhone = string.Empty;
        ContactEmail = string.Empty;
        Address = string.Empty;
        LicenseType = string.Empty;
        LicenseKey = string.Empty;
        Domain = string.Empty;
        ConfigId = string.Empty;
    }

    /// <summary>
    /// ConfigId（多库唯一标识）
    /// </summary>
    public string ConfigId { get; set; }

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
    public string? ContactUser { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string? ContactPhone { get; set; }

    /// <summary>
    /// 联系邮箱
    /// </summary>
    public string? ContactEmail { get; set; }

    /// <summary>
    /// 联系地址
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// 许可证类型
    /// </summary>
    public string? LicenseType { get; set; }

    /// <summary>
    /// 许可注册码
    /// </summary>
    public string? LicenseKey { get; set; }

    /// <summary>
    /// 租户域名
    /// </summary>
    public string? Domain { get; set; }
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
        ContactUser = string.Empty;
        ContactPhone = string.Empty;
        ContactEmail = string.Empty;
        Address = string.Empty;
        LicenseType = string.Empty;
        LicenseKey = string.Empty;
        Domain = string.Empty;
        ConfigId = string.Empty;
    }

    /// <summary>
    /// ConfigId（多库唯一标识）
    /// </summary>
    public string ConfigId { get; set; }

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
    public string ContactUser { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string ContactPhone { get; set; }

    /// <summary>
    /// 联系邮箱
    /// </summary>
    public string ContactEmail { get; set; }

    /// <summary>
    /// 联系地址
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// 许可证类型
    /// </summary>
    public string LicenseType { get; set; }

    /// <summary>
    /// 许可注册码
    /// </summary>
    public string LicenseKey { get; set; }

    /// <summary>
    /// 租户域名
    /// </summary>
    public string Domain { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}