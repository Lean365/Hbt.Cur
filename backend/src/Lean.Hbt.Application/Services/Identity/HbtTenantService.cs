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
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.Utils;
using SqlSugar;
using Mapster;
using SqlSugar.Extensions;
using System.Linq.Expressions;

namespace Lean.Hbt.Application.Services.Identity;

/// <summary>
/// 租户服务实现
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
public class HbtTenantService : IHbtTenantService
{
    private readonly IHbtRepository<HbtTenant> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtTenantService(IHbtRepository<HbtTenant> repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 获取租户分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>返回分页结果</returns>
    public async Task<HbtPagedResult<HbtTenantDto>> GetPagedListAsync(HbtTenantQueryDto query)
    {
        var exp = Expressionable.Create<HbtTenant>();
        
        if (!string.IsNullOrEmpty(query.TenantCode))
            exp.And(x => x.TenantCode.Contains(query.TenantCode));
            
        if (!string.IsNullOrEmpty(query.TenantName))
            exp.And(x => x.TenantName.Contains(query.TenantName));
            
        if (query.Status.HasValue)
            exp.And(x => x.Status == query.Status);

        var result = await _repository.GetPagedListAsync(exp.ToExpression(), query.PageIndex, query.PageSize);

        return new HbtPagedResult<HbtTenantDto>
        {
            TotalNum = result.total,
            PageIndex = query.PageIndex,
            PageSize = query.PageSize,
            Rows = result.list.Adapt<List<HbtTenantDto>>()
        };
    }

    /// <summary>
    /// 获取租户详情
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <returns>返回租户详情</returns>
    public async Task<HbtTenantDto> GetAsync(long id)
    {
        var tenant = await _repository.GetByIdAsync(id);
        if (tenant == null)
            throw new HbtException($"租户不存在: {id}");

        return tenant.Adapt<HbtTenantDto>();
    }

    /// <summary>
    /// 创建租户
    /// </summary>
    /// <param name="input">租户创建信息</param>
    /// <returns>返回新创建的租户ID</returns>
    public async Task<long> InsertAsync(HbtTenantCreateDto input)
    {
        // 验证字段是否已存在
        await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "TenantName", input.TenantName);
        await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "TenantCode", input.TenantCode);
        await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "ContactEmail", input.ContactEmail);

        var tenant = input.Adapt<HbtTenant>();
        tenant.Status = HbtStatus.Normal;

        var result = await _repository.InsertAsync(tenant);
        return result > 0 ? tenant.Id : 0;
    }

    /// <summary>
    /// 更新租户
    /// </summary>
    /// <param name="input">租户更新信息</param>
    /// <returns>返回是否更新成功</returns>
    public async Task<bool> UpdateAsync(HbtTenantUpdateDto input)
    {
        var tenant = await _repository.GetByIdAsync(input.Id);
        if (tenant == null)
            throw new HbtException($"租户不存在: {input.Id}");

        // 验证字段是否已存在
        await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "TenantName", input.TenantName, input.Id);
        await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "TenantCode", input.TenantCode, input.Id);
        await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "ContactEmail", input.ContactEmail, input.Id);

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

        var result = await _repository.UpdateAsync(tenant);
        return result > 0;
    }

    /// <summary>
    /// 删除租户
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <returns>返回是否删除成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        var result = await _repository.DeleteAsync(id);
        return result > 0;
    }

    /// <summary>
    /// 批量删除租户
    /// </summary>
    /// <param name="ids">租户ID数组</param>
    /// <returns>返回是否删除成功</returns>
    public async Task<bool> BatchDeleteAsync(long[] ids)
    {
        var result = await _repository.DeleteRangeAsync(ids.Cast<object>().ToList());
        return result > 0;
    }

    /// <summary>
    /// 导出租户数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>返回导出的租户数据列表</returns>
    public async Task<List<HbtTenantExportDto>> ExportAsync(HbtTenantQueryDto query)
    {
        var exp = Expressionable.Create<HbtTenant>();
        
        if (!string.IsNullOrEmpty(query.TenantCode))
            exp.And(x => x.TenantCode.Contains(query.TenantCode));
            
        if (!string.IsNullOrEmpty(query.TenantName))
            exp.And(x => x.TenantName.Contains(query.TenantName));
            
        if (query.Status.HasValue)
            exp.And(x => x.Status == query.Status);

        var tenants = await _repository.GetListAsync(exp.ToExpression());
        return tenants.Adapt<List<HbtTenantExportDto>>();
    }

    /// <summary>
    /// 更新租户状态
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <param name="status">新状态</param>
    /// <returns>返回是否更新成功</returns>
    public async Task<bool> UpdateStatusAsync(long id, HbtStatus status)
    {
        var tenant = await _repository.GetByIdAsync(id);
        if (tenant == null)
            throw new HbtException($"租户不存在: {id}");

        tenant.Status = status;
        var result = await _repository.UpdateAsync(tenant);
        return result > 0;
    }
}