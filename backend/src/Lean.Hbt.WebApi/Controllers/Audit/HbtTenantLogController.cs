//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTenantLogController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 租户审计日志控制器
//===================================================================

using Lean.Hbt.Common.Constants;
using Lean.Hbt.Domain.IServices.Extensions;
using Lean.Hbt.Application.Services.Audit;
using Lean.Hbt.Application.Dtos.Audit;
using Lean.Hbt.Domain.IServices.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lean.Hbt.WebApi.Controllers.Audit;

/// <summary>
/// 租户审计日志控制器
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
[Route("api/[controller]", Name = "租户审计日志")]
[ApiController]
[ApiModule("audit", "审计日志")]
[Authorize]
public class HbtTenantLogController : HbtBaseController
{
    private readonly IHbtTenantLogService _auditLogService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="auditLogService">租户审计日志服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="currentTenant">当前租户服务</param>
    /// <param name="localization">本地化服务</param>
    public HbtTenantLogController(
        IHbtTenantLogService auditLogService,
        IHbtLogger logger,
        IHbtCurrentUser currentUser,
        IHbtCurrentTenant currentTenant,
        IHbtLocalizationService localization) : base(logger, currentUser, currentTenant, localization)
    {
        _auditLogService = auditLogService;
    }

    /// <summary>
    /// 获取审计日志分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>审计日志分页列表</returns>
    [HttpGet("list")]
    [HbtPerm("audit:tenantlog:list")]
    public async Task<IActionResult> GetListAsync([FromQuery] HbtTenantLogQueryDto query)
    {
        var result = await _auditLogService.GetListAsync(query);
        return Success(result);
    }

    /// <summary>
    /// 获取审计日志详情
    /// </summary>
    /// <param name="id">日志ID</param>
    /// <returns>审计日志详情</returns>
    [HttpGet("{id}")]
    [HbtPerm("audit:tenantlog:query")]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        var result = await _auditLogService.GetByIdAsync(id);
        return Success(result);
    }

    /// <summary>
    /// 创建审计日志
    /// </summary>
    /// <param name="input">创建对象</param>
    /// <returns>日志ID</returns>
    [HttpPost]
    [HbtPerm("audit:tenantlog:create")]
    public async Task<IActionResult> CreateAsync([FromBody] HbtTenantLogCreateDto input)
    {
        var result = await _auditLogService.CreateAsync(input);
        return Success(result);
    }

    /// <summary>
    /// 更新审计日志
    /// </summary>
    /// <param name="input">更新对象</param>
    /// <returns>是否成功</returns>
    [HttpPut]
    [HbtPerm("audit:tenantlog:update")]
    public async Task<IActionResult> UpdateAsync([FromBody] HbtTenantLogUpdateDto input)
    {
        var result = await _auditLogService.UpdateAsync(input);
        return Success(result);
    }

    /// <summary>
    /// 删除审计日志
    /// </summary>
    /// <param name="id">日志ID</param>
    /// <returns>是否成功</returns>
    [HttpDelete("{id}")]
    [HbtPerm("audit:tenantlog:delete")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        var result = await _auditLogService.DeleteAsync(id);
        return Success(result);
    }

    /// <summary>
    /// 批量删除审计日志
    /// </summary>
    /// <param name="ids">日志ID集合</param>
    /// <returns>是否成功</returns>
    [HttpDelete("batch")]
    [HbtPerm("audit:tenantlog:delete")]
    public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
    {
        var result = await _auditLogService.BatchDeleteAsync(ids);
        return Success(result);
    }

    /// <summary>
    /// 导出审计日志数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>Excel文件</returns>
    [HttpGet("export")]
    [HbtPerm("audit:tenantlog:export")]
    public async Task<IActionResult> ExportAsync([FromQuery] HbtTenantLogQueryDto query)
    {
        var result = await _auditLogService.ExportAsync(query, "TenantLog");
        var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
            ? "application/zip"
            : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        // 只在 filename* 用 UTF-8 编码，filename 用 ASCII
        var safeFileName = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(result.fileName));
        Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
        return File(result.content, contentType, result.fileName);
    }

    /// <summary>
    /// 清理审计日志
    /// </summary>
    /// <returns>是否成功</returns>
    [HttpDelete("clear")]
    [HbtPerm("audit:tenantlog:clear")]
    public async Task<IActionResult> ClearAsync()
    {
        var result = await _auditLogService.ClearAsync();
        return Success(result);
    }
} 