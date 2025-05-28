#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTenantLogService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 租户审计日志服务实现
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.Audit;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices.Extensions;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Application.Dtos.Audit;
using Microsoft.AspNetCore.Http;
using SqlSugar;

namespace Lean.Hbt.Application.Services.Audit;

/// <summary>
/// 租户审计日志服务实现
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
public class HbtTenantLogService : HbtBaseService, IHbtTenantLogService
{
    private readonly IHbtRepository<HbtTenantLog> _auditLogRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="auditLogRepository">租户审计日志仓储</param>
    /// <param name="logger">日志服务</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="currentTenant">当前租户服务</param>
    /// <param name="localization">本地化服务</param>
    public HbtTenantLogService(
        IHbtRepository<HbtTenantLog> auditLogRepository,
        IHbtLogger logger,
        IHttpContextAccessor httpContextAccessor,
        IHbtCurrentUser currentUser,
        IHbtCurrentTenant currentTenant,
        IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, currentTenant, localization)
    {
        _auditLogRepository = auditLogRepository ?? throw new ArgumentNullException(nameof(auditLogRepository));
    }

    /// <summary>
    /// 构建查询条件
    /// </summary>
    private Expression<Func<HbtTenantLog, bool>> BuildQueryExpression(HbtTenantLogQueryDto query)
    {
        return Expressionable.Create<HbtTenantLog>()
            .AndIF(query.TenantId.HasValue, x => x.TenantId == query.TenantId.Value)
            .AndIF(!string.IsNullOrEmpty(query.Action), x => x.Action.Contains(query.Action))
            .AndIF(query.StartTime.HasValue, x => x.CreateTime >= query.StartTime.Value)
            .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime.Value)
            .AndIF(true, x => x.IsDeleted == 0)
            .ToExpression();
    }

    /// <summary>
    /// 获取审计日志分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>返回分页结果</returns>
    public async Task<HbtPagedResult<HbtTenantLogDto>> GetListAsync(HbtTenantLogQueryDto query)
    {
        query ??= new HbtTenantLogQueryDto();

        var result = await _auditLogRepository.GetPagedListAsync(
            BuildQueryExpression(query),
            query.PageIndex,
            query.PageSize,
            x => x.CreateTime,
            OrderByType.Desc);

        return new HbtPagedResult<HbtTenantLogDto>
        {
            TotalNum = result.TotalNum,
            PageIndex = query.PageIndex,
            PageSize = query.PageSize,
            Rows = result.Rows.Adapt<List<HbtTenantLogDto>>()
        };
    }

    /// <summary>
    /// 获取审计日志详情
    /// </summary>
    /// <param name="id">日志ID</param>
    /// <returns>返回审计日志详情</returns>
    public async Task<HbtTenantLogDto> GetByIdAsync(long id)
    {
        var log = await _auditLogRepository.GetByIdAsync(id);
        return log == null ? throw new HbtException(L("Audit.TenantLog.NotFound", id)) : log.Adapt<HbtTenantLogDto>();
    }

    /// <summary>
    /// 创建审计日志
    /// </summary>
    /// <param name="dto">创建DTO</param>
    /// <returns>审计日志ID</returns>
    public async Task<long> CreateAsync(HbtTenantLogCreateDto dto)
    {
        var auditLog = dto.Adapt<HbtTenantLog>();
        return await _auditLogRepository.CreateAsync(auditLog) > 0 ? auditLog.Id : throw new HbtException(L("Audit.TenantLog.CreateFailed"));
    }

    /// <summary>
    /// 更新审计日志
    /// </summary>
    /// <param name="dto">更新DTO</param>
    /// <returns>是否成功</returns>
    public async Task<bool> UpdateAsync(HbtTenantLogUpdateDto dto)
    {
        var auditLog = await _auditLogRepository.GetByIdAsync(dto.Id)
            ?? throw new HbtException(L("Audit.TenantLog.NotFound", dto.Id));

        dto.Adapt(auditLog);
        return await _auditLogRepository.UpdateAsync(auditLog) > 0;
    }

    /// <summary>
    /// 删除审计日志
    /// </summary>
    /// <param name="id">日志ID</param>
    /// <returns>是否成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        var log = await _auditLogRepository.GetByIdAsync(id)
            ?? throw new HbtException(L("Audit.TenantLog.NotFound", id));

        return await _auditLogRepository.DeleteAsync(id) > 0;
    }

    /// <summary>
    /// 批量删除审计日志
    /// </summary>
    /// <param name="ids">日志ID集合</param>
    /// <returns>是否成功</returns>
    public async Task<bool> BatchDeleteAsync(long[] ids)
    {
        if (ids == null || ids.Length == 0) return false;
        return await _auditLogRepository.DeleteRangeAsync(ids.Cast<object>().ToList()) > 0;
    }

    /// <summary>
    /// 导出审计日志
    /// </summary>
    public async Task<(string fileName, byte[] content)> ExportAsync(HbtTenantLogQueryDto query, string sheetName = "TenantLog")
    {
        var list = await _auditLogRepository.GetListAsync(BuildQueryExpression(query));
        return await HbtExcelHelper.ExportAsync(list.Adapt<List<HbtTenantLogExportDto>>(), sheetName, L("Audit.TenantLog.ExportTitle"));
    }

    /// <summary>
    /// 清理审计日志
    /// </summary>
    public async Task<bool> ClearAsync()
    {
        var result = await _auditLogRepository.DeleteAsync((Expression<Func<HbtTenantLog, bool>>)(x => true));
        return result > 0;
    }
} 