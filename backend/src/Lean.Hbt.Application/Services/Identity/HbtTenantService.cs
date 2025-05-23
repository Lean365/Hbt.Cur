//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTenantService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 19:15
// 版本号 : V0.0.1
// 描述   : 租户服务实现
//===================================================================

using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Identity;

/// <summary>
/// 租户服务实现
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
public class HbtTenantService : HbtBaseService, IHbtTenantService
{
    private readonly IHbtRepository<HbtTenant> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtTenantService(
        IHbtRepository<HbtTenant> repository,
        IHbtLogger logger,
        IHttpContextAccessor httpContextAccessor,
        IHbtLocalizationService localization,
        IHbtCurrentUser currentUser) : base(logger, httpContextAccessor, currentUser, localization)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    /// <summary>
    /// 构建租户查询条件
    /// </summary>
    private Expression<Func<HbtTenant, bool>> HbtTenantQueryExpression(HbtTenantQueryDto query)
    {
        var exp = Expressionable.Create<HbtTenant>();

        if (!string.IsNullOrEmpty(query.TenantName))
            exp.And(x => x.TenantName.Contains(query.TenantName));

        if (!string.IsNullOrEmpty(query.TenantCode))
            exp.And(x => x.TenantCode.Contains(query.TenantCode));

        if (query.Status.HasValue)
            exp.And(x => x.Status == query.Status.Value);

        return exp.ToExpression();
    }

    /// <summary>
    /// 获取租户分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>返回分页结果</returns>
    public async Task<HbtPagedResult<HbtTenantDto>> GetListAsync(HbtTenantQueryDto query)
    {
        var exp = HbtTenantQueryExpression(query);

        var result = await _repository.GetPagedListAsync(
            exp,
            query.PageIndex,
            query.PageSize,
            x => x.Id,
            OrderByType.Asc);

        return new HbtPagedResult<HbtTenantDto>
        {
            Rows = result.Rows.Adapt<List<HbtTenantDto>>(),
            TotalNum = result.TotalNum,
            PageIndex = query.PageIndex,
            PageSize = query.PageSize
        };
    }

    /// <summary>
    /// 获取租户详情
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <returns>返回租户详情</returns>
    public async Task<HbtTenantDto> GetByIdAsync(long id)
    {
        var tenant = await _repository.GetByIdAsync(id);
        if (tenant == null)
            throw new HbtException(L("Common.NotExists"));

        return tenant.Adapt<HbtTenantDto>();
    }

    /// <summary>
    /// 创建租户
    /// </summary>
    /// <param name="input">租户创建信息</param>
    /// <returns>返回新创建的租户ID</returns>
    public async Task<long> CreateAsync(HbtTenantCreateDto input)
    {
        // 验证字段是否已存在
        await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "TenantName", input.TenantName);
        await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "TenantCode", input.TenantCode);
        await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "ContactEmail", input.ContactEmail);

        var tenant = input.Adapt<HbtTenant>();
        var result = await _repository.CreateAsync(tenant);
        if (result > 0)
            _logger.Info(L("Common.AddSuccess"));

        return tenant.Id;
    }

    /// <summary>
    /// 更新租户
    /// </summary>
    /// <param name="input">租户更新信息</param>
    /// <returns>返回是否更新成功</returns>
    public async Task<bool> UpdateAsync(HbtTenantUpdateDto input)
    {
        var tenant = await _repository.GetByIdAsync(input.TenantId)
            ?? throw new HbtException(L("Common.NotExists"));

        // 验证字段是否已存在
        if (tenant.TenantName != input.TenantName)
            await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "TenantName", input.TenantName, input.TenantId);
        if (tenant.TenantCode != input.TenantCode)
            await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "TenantCode", input.TenantCode, input.TenantId);
        if (tenant.ContactEmail != input.ContactEmail)
            await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "ContactEmail", input.ContactEmail, input.TenantId);

        input.Adapt(tenant);
        return await _repository.UpdateAsync(tenant) > 0;
    }

    /// <summary>
    /// 删除租户
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <returns>返回是否删除成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        var tenant = await _repository.GetByIdAsync(id)
            ?? throw new HbtException(L("Common.NotExists"));

        return await _repository.DeleteAsync(tenant) > 0;
    }

    /// <summary>
    /// 批量删除租户
    /// </summary>
    /// <param name="tenantIds">租户ID列表</param>
    /// <returns>返回是否删除成功</returns>
    public async Task<bool> BatchDeleteAsync(long[] tenantIds)
    {
        if (tenantIds == null || tenantIds.Length == 0)
            throw new HbtException("请选择要删除的租户");

        return await _repository.DeleteRangeAsync(tenantIds.Cast<object>().ToList()) > 0;
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
    {
        return await HbtExcelHelper.GenerateTemplateAsync<HbtTenantTemplateDto>(sheetName);
    }

    /// <summary>
    /// 导入租户数据
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
    {
        try
        {
            var tenants = await HbtExcelHelper.ImportAsync<HbtTenantImportDto>(fileStream, sheetName);
            if (!tenants.Any())
                return (0, 0);

            int success = 0, fail = 0;

            foreach (var tenant in tenants)
            {
                try
                {
                    if (string.IsNullOrEmpty(tenant.TenantName) || string.IsNullOrEmpty(tenant.TenantCode))
                    {
                        _logger.Warn("导入租户失败: 租户名称或租户编码不能为空");
                        fail++;
                        continue;
                    }

                    await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "TenantName", tenant.TenantName);
                    await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "TenantCode", tenant.TenantCode);

                    var entity = tenant.Adapt<HbtTenant>();
                    entity.CreateTime = DateTime.Now;
                    entity.CreateBy = "system";

                    var result = await _repository.CreateAsync(entity);
                    if (result > 0)
                        success++;
                    else
                        fail++;
                }
                catch (Exception ex)
                {
                    _logger.Warn($"导入租户失败: {ex.Message}");
                    fail++;
                }
            }

            return (success, fail);
        }
        catch (Exception ex)
        {
            _logger.Error("导入租户数据失败", ex);
            throw new HbtException("导入租户数据失败");
        }
    }

    /// <summary>
    /// 导出租户数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(HbtTenantQueryDto query, string sheetName = "Sheet1")
    {
        try
        {
            var list = await _repository.GetListAsync(HbtTenantQueryExpression(query));
            var exportList = list.Adapt<List<HbtTenantExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportList, sheetName, "租户数据");
        }
        catch (Exception ex)
        {
            _logger.Error("导出租户数据失败", ex);
            throw new HbtException("导出租户数据失败");
        }
    }

    /// <summary>
    /// 更新租户状态
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <param name="status">状态</param>
    /// <returns>更新后的租户状态信息</returns>
    public async Task<HbtTenantStatusDto> UpdateStatusAsync(long id, int status)
    {
        var tenant = await _repository.GetByIdAsync(id);
        if (tenant == null)
            throw new HbtException(L("Common.NotExists"));

        tenant.Status = status;
        var result = await _repository.UpdateAsync(tenant);
        if (result <= 0)
            throw new HbtException(L("Common.UpdateFailed"));

        return new HbtTenantStatusDto
        {
            TenantId = tenant.Id,
            Status = tenant.Status
        };
    }

    /// <summary>
    /// 获取租户选项列表
    /// </summary>
    /// <returns>租户选项列表</returns>
    public async Task<List<HbtSelectOption>> GetOptionsAsync()
    {
        var tenants = await _repository.AsQueryable()
            .Where(t => t.Status == 0)  // 只获取正常状态的租户
            .OrderBy(t => t.Id)
            .Select(t => new HbtSelectOption
            {
                Label = t.TenantName,
                Value = t.Id,
            })
            .ToListAsync();
        return tenants;
    }
}