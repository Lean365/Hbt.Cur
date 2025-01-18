//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTenantService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 19:15
// 版本号 : V0.0.1
// 描述   : 租户服务实现
//===================================================================

using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.Repositories;
using SqlSugar;

namespace Lean.Hbt.Application.Services.Identity;

/// <summary>
/// 租户服务实现
/// </summary>
public class HbtTenantService : IHbtTenantService
{
    private readonly IHbtRepository<HbtTenant> _tenantRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="tenantRepository">租户仓储</param>
    public HbtTenantService(IHbtRepository<HbtTenant> tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    /// <inheritdoc/>
    public async Task<HbtPagedResult<HbtTenantDto>> GetPagedListAsync(HbtTenantQueryDto query)
    {
        var exp = Expressionable.Create<HbtTenant>();
        
        if (!string.IsNullOrEmpty(query.TenantName))
            exp.And(x => x.TenantName.Contains(query.TenantName));
        if (!string.IsNullOrEmpty(query.TenantCode))
            exp.And(x => x.TenantCode.Contains(query.TenantCode));
        if (!string.IsNullOrEmpty(query.ContactPerson))
            exp.And(x => x.ContactPerson.Contains(query.ContactPerson));
        if (!string.IsNullOrEmpty(query.ContactPhone))
            exp.And(x => x.ContactPhone.Contains(query.ContactPhone));
        if (query.Status.HasValue)
            exp.And(x => x.Status == query.Status);

        RefAsync<int> total = 0;
        var items = await _tenantRepository.AsQueryable()
            .Where(exp.ToExpression())
            .OrderBy("Id")
            .Select(x => new HbtTenantDto
            {
                Id = x.Id,
                TenantName = x.TenantName,
                TenantCode = x.TenantCode,
                ContactPerson = x.ContactPerson,
                ContactPhone = x.ContactPhone,
                ContactEmail = x.ContactEmail,
                Domain = x.Domain,
                Status = x.Status,
                Address = x.Address,
                LogoUrl = x.LogoUrl,
                Theme = x.Theme,
                LicenseStartTime = x.LicenseStartTime,
                LicenseEndTime = x.LicenseEndTime,
                MaxUserCount = x.MaxUserCount,
                CreateTime = x.CreateTime
            })
            .ToPageListAsync(query.PageIndex, query.PageSize, total);

        return new HbtPagedResult<HbtTenantDto>
        {
            TotalNum = total,
            PageIndex = query.PageIndex,
            PageSize = query.PageSize,
            Rows = items
        };
    }

    /// <inheritdoc/>
    public async Task<HbtTenantDto> GetAsync(long id)
    {
        var tenant = await _tenantRepository.GetByIdAsync(id);
        return new HbtTenantDto
        {
            Id = tenant.Id,
            TenantName = tenant.TenantName,
            TenantCode = tenant.TenantCode,
            ContactPerson = tenant.ContactPerson,
            ContactPhone = tenant.ContactPhone,
            ContactEmail = tenant.ContactEmail,
            Domain = tenant.Domain,
            Status = tenant.Status,
            Address = tenant.Address,
            LogoUrl = tenant.LogoUrl,
            Theme = tenant.Theme,
            LicenseStartTime = tenant.LicenseStartTime,
            LicenseEndTime = tenant.LicenseEndTime,
            MaxUserCount = tenant.MaxUserCount,
            CreateTime = tenant.CreateTime
        };
    }

    /// <inheritdoc/>
    public async Task<long> CreateAsync(HbtTenantCreateDto input)
    {
        var tenant = new HbtTenant
        {
            TenantName = input.TenantName,
            TenantCode = input.TenantCode,
            ContactPerson = input.ContactPerson,
            ContactPhone = input.ContactPhone,
            ContactEmail = input.ContactEmail,
            Domain = input.Domain,
            Status = HbtStatus.Normal,
            Address = input.Address,
            LogoUrl = input.LogoUrl,
            Theme = input.Theme,
            LicenseStartTime = input.LicenseStartTime,
            LicenseEndTime = input.LicenseEndTime,
            MaxUserCount = input.MaxUserCount
        };

        await _tenantRepository.InsertAsync(tenant);
        return tenant.Id;
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateAsync(HbtTenantUpdateDto input)
    {
        var tenant = await _tenantRepository.GetByIdAsync(input.Id);
        tenant.TenantName = input.TenantName;
        tenant.TenantCode = input.TenantCode;
        tenant.ContactPerson = input.ContactPerson;
        tenant.ContactPhone = input.ContactPhone;
        tenant.ContactEmail = input.ContactEmail;
        tenant.Domain = input.Domain;
        tenant.Address = input.Address;
        tenant.LogoUrl = input.LogoUrl;
        tenant.Theme = input.Theme;
        tenant.LicenseStartTime = input.LicenseStartTime;
        tenant.LicenseEndTime = input.LicenseEndTime;
        tenant.MaxUserCount = input.MaxUserCount;

        return await _tenantRepository.UpdateAsync(tenant);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(long id)
    {
        return await _tenantRepository.DeleteByIdAsync(id);
    }

    /// <inheritdoc/>
    public async Task<bool> BatchDeleteAsync(long[] ids)
    {
        return await _tenantRepository.DeleteAsync(x => ids.Contains(x.Id));
    }

    /// <inheritdoc/>
    public async Task<List<HbtTenantExportDto>> ExportAsync(HbtTenantQueryDto query)
    {
        var exp = Expressionable.Create<HbtTenant>();
        
        if (!string.IsNullOrEmpty(query.TenantName))
            exp.And(x => x.TenantName.Contains(query.TenantName));
        if (!string.IsNullOrEmpty(query.TenantCode))
            exp.And(x => x.TenantCode.Contains(query.TenantCode));
        if (!string.IsNullOrEmpty(query.ContactPerson))
            exp.And(x => x.ContactPerson.Contains(query.ContactPerson));
        if (!string.IsNullOrEmpty(query.ContactPhone))
            exp.And(x => x.ContactPhone.Contains(query.ContactPhone));
        if (query.Status.HasValue)
            exp.And(x => x.Status == query.Status);

        var items = await _tenantRepository.AsQueryable()
            .Where(exp.ToExpression())
            .OrderBy("Id")
            .Select(x => new HbtTenantExportDto
            {
                TenantName = x.TenantName,
                TenantCode = x.TenantCode,
                ContactPerson = x.ContactPerson,
                ContactPhone = x.ContactPhone,
                ContactEmail = x.ContactEmail,
                Domain = x.Domain,
                Address = x.Address,
                CreateTime = x.CreateTime
            })
            .ToListAsync();

        return items;
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateStatusAsync(long id, HbtStatus status)
    {
        var tenant = await _tenantRepository.GetByIdAsync(id);
        tenant.Status = status;

        return await _tenantRepository.UpdateAsync(tenant);
    }
} 