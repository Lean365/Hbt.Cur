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
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.Utils;
using SqlSugar;

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
            exp.And(x => x.Status == query.Status.Value);

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
        if (!string.IsNullOrEmpty(input.TenantName))
            await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "TenantName", input.TenantName);

        if (!string.IsNullOrEmpty(input.TenantCode))
            await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "TenantCode", input.TenantCode);

        if (!string.IsNullOrEmpty(input.ContactEmail))
            await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "ContactEmail", input.ContactEmail);

        var tenant = input.Adapt<HbtTenant>();
        tenant.Status = 0; // 0 表示正常状态

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
        var tenant = await _repository.GetByIdAsync(input.TenantId);
        if (tenant == null)
            throw new HbtException($"租户不存在: {input.TenantId}");

        // 验证字段是否已存在
        await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "TenantName", input.TenantName, input.TenantId);
        await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "TenantCode", input.TenantCode, input.TenantId);
        await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "ContactEmail", input.ContactEmail, input.TenantId);

        tenant.TenantName = input.TenantName;
        tenant.TenantCode = input.TenantCode;
        tenant.ContactUser = input.ContactUser;
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
    /// 导入租户数据
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
    public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
    {
        var tenants = await HbtExcelHelper.ImportAsync<HbtTenantImportDto>(fileStream, sheetName);
        if (tenants == null || !tenants.Any())
            return (0, 0);

        int success = 0;
        int fail = 0;

        foreach (var tenant in tenants)
        {
            try
            {
                // 验证字段是否已存在
                if (!string.IsNullOrEmpty(tenant.TenantName))
                    await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "TenantName", tenant.TenantName);

                if (!string.IsNullOrEmpty(tenant.TenantCode))
                    await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "TenantCode", tenant.TenantCode);

                if (!string.IsNullOrEmpty(tenant.ContactEmail))
                    await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "ContactEmail", tenant.ContactEmail);

                var entity = tenant.Adapt<HbtTenant>();
                entity.Status = 0; // 0 表示正常状态

                var result = await _repository.InsertAsync(entity);
                if (result > 0)
                    success++;
                else
                    fail++;
            }
            catch (Exception ex)
            {
                fail++;
            }
        }

        return (success, fail);
    }

    /// <summary>
    /// 导出租户数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件字节数组</returns>
    public async Task<byte[]> ExportAsync(HbtTenantQueryDto query, string sheetName = "Sheet1")
    {
        var exp = Expressionable.Create<HbtTenant>();

        if (!string.IsNullOrEmpty(query.TenantCode))
            exp.And(x => x.TenantCode.Contains(query.TenantCode));

        if (!string.IsNullOrEmpty(query.TenantName))
            exp.And(x => x.TenantName.Contains(query.TenantName));

        if (query.Status.HasValue)
            exp.And(x => x.Status == query.Status.Value);

        var tenants = await _repository.GetListAsync(exp.ToExpression());
        var exportData = tenants.Adapt<List<HbtTenantExportDto>>();

        return await HbtExcelHelper.ExportAsync(exportData, sheetName);
    }

    /// <summary>
    /// 获取租户导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件字节数组</returns>
    public async Task<byte[]> GetTemplateAsync(string sheetName = "Sheet1")
    {
        return await HbtExcelHelper.GenerateTemplateAsync<HbtTenantTemplateDto>(sheetName);
    }

    /// <summary>
    /// 更新租户状态
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <param name="status">新状态(0:正常,1:禁用)</param>
    /// <returns>返回更新后的租户状态信息</returns>
    public async Task<HbtTenantStatusDto> UpdateStatusAsync(long id, int status)
    {
        var tenant = await _repository.GetByIdAsync(id);
        if (tenant == null)
            throw new HbtException($"租户不存在: {id}");

        tenant.Status = status;
        var result = await _repository.UpdateAsync(tenant);
        if (result <= 0)
            throw new HbtException($"更新租户状态失败: {id}");

        var statusDto = new HbtTenantStatusDto
        {
            TenantId = tenant.Id,
            Status = tenant.Status
        };

        return statusDto;
    }
}