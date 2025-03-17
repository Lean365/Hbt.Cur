//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTenantController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 19:15
// 版本号 : V0.0.1
// 描述   : 租户控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Application.Services.Identity;
using Lean.Hbt.Domain.IServices.Admin;

namespace Lean.Hbt.WebApi.Controllers.Identity;

/// <summary>
/// 租户管理
/// </summary>
[Route("api/admin/[controller]", Name = "租户")]
[ApiController]
[ApiModule("identity", "身份认证")]
public class HbtTenantController : HbtBaseController
{
    private readonly IHbtTenantService _tenantService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="tenantService">租户服务</param>
    /// <param name="localization">本地化服务</param>
    public HbtTenantController(IHbtTenantService tenantService, IHbtLocalizationService localization) : base(localization)
    {
        _tenantService = tenantService;
    }

    /// <summary>
    /// 获取租户列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>租户列表</returns>
    [HttpGet("list")]
    public async Task<HbtPagedResult<HbtTenantDto>> GetPagedListAsync([FromQuery] HbtTenantQueryDto query)
    {
        return await _tenantService.GetPagedListAsync(query);
    }

    /// <summary>
    /// 获取租户详情
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <returns>租户详情</returns>
    [HttpGet("{id}")]
    public async Task<HbtTenantDto> GetAsync(long id)
    {
        return await _tenantService.GetAsync(id);
    }

    /// <summary>
    /// 创建租户
    /// </summary>
    /// <param name="input">创建对象</param>
    /// <returns>租户ID</returns>
    [HttpPost]
    public async Task<long> InsertAsync([FromBody] HbtTenantCreateDto input)
    {
        return await _tenantService.InsertAsync(input);
    }

    /// <summary>
    /// 更新租户
    /// </summary>
    /// <param name="input">更新对象</param>
    /// <returns>是否成功</returns>
    [HttpPut]
    public async Task<bool> UpdateAsync([FromBody] HbtTenantUpdateDto input)
    {
        return await _tenantService.UpdateAsync(input);
    }

    /// <summary>
    /// 删除租户
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <returns>是否成功</returns>
    [HttpDelete("{id}")]
    public async Task<bool> DeleteAsync(long id)
    {
        return await _tenantService.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除租户
    /// </summary>
    /// <param name="ids">租户ID集合</param>
    /// <returns>是否成功</returns>
    [HttpDelete("batch")]
    public async Task<bool> BatchDeleteAsync([FromBody] long[] ids)
    {
        return await _tenantService.BatchDeleteAsync(ids);
    }

    /// <summary>
    /// 导入租户数据
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    public async Task<(int success, int fail)> ImportAsync(IFormFile file)
    {
        using var stream = file.OpenReadStream();
        return await _tenantService.ImportAsync(stream, "Sheet1");
    }

    /// <summary>
    /// 导出租户数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>Excel文件</returns>
    [HttpGet("export")]
    public async Task<IActionResult> ExportAsync([FromQuery] HbtTenantQueryDto query)
    {
        var result = await _tenantService.ExportAsync(query, "Sheet1");
        return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "租户数据.xlsx");
    }

    /// <summary>
    /// 获取租户导入模板
    /// </summary>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    public async Task<IActionResult> GetTemplateAsync()
    {
        var result = await _tenantService.GetTemplateAsync("Sheet1");
        return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "租户导入模板.xlsx");
    }

    /// <summary>
    /// 更新租户状态
    /// </summary>
    /// <param name="tenantId">租户ID</param>
    /// <param name="status">状态</param>
    /// <returns>是否成功</returns>
    [HttpPut("{tenantId}/status")]
    public async Task<IActionResult> UpdateStatusAsync(long tenantId, [FromQuery] int status)
    {
        var result = await _tenantService.UpdateStatusAsync(tenantId, status);
        return Success(result);
    }

    /// <summary>
    /// 获取租户选项列表
    /// </summary>
    /// <returns>租户选项列表</returns>
    [HttpGet("options")]
    public async Task<IActionResult> GetOptionsAsync()
    {
        var result = await _tenantService.GetOptionsAsync();
        return Success(result);
    }
}