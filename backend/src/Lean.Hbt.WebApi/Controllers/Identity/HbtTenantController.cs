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

namespace Lean.Hbt.WebApi.Controllers.Identity;

/// <summary>
/// 租户管理
/// </summary>
[Route("api/[controller]", Name = "租户")]
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
    /// <param name="logger">日志服务</param>
    public HbtTenantController(IHbtTenantService tenantService,
                                IHbtLocalizationService localization,
            IHbtLogger logger) : base(localization, logger)
    {
        _tenantService = tenantService;
    }

    /// <summary>
    /// 获取租户列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>租户列表</returns>
    [HttpGet("list")]
    [HbtPerm("identity:tenant:list")]
    public async Task<IActionResult> GetListAsync([FromQuery] HbtTenantQueryDto query)
    {
        var result = await _tenantService.GetListAsync(query);
        return Ok(HbtApiResult.Success(result));
    }

    /// <summary>
    /// 获取租户详情
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <returns>租户详情</returns>
    [HttpGet("{id}")]
    [HbtPerm("identity:tenant:query")]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        var result = await _tenantService.GetByIdAsync(id);
        return Ok(HbtApiResult.Success(result));
    }

    /// <summary>
    /// 创建租户
    /// </summary>
    /// <param name="input">创建对象</param>
    /// <returns>租户ID</returns>
    [HttpPost]
    [HbtPerm("identity:tenant:create")]
    public async Task<IActionResult> CreateAsync([FromBody] HbtTenantCreateDto input)
    {
        var result = await _tenantService.CreateAsync(input);
        return Ok(HbtApiResult.Success(result));
    }

    /// <summary>
    /// 更新租户
    /// </summary>
    /// <param name="input">更新对象</param>
    /// <returns>是否成功</returns>
    [HttpPut]
    [HbtPerm("identity:tenant:update")]
    public async Task<IActionResult> UpdateAsync([FromBody] HbtTenantUpdateDto input)
    {
        var result = await _tenantService.UpdateAsync(input);
        return Ok(HbtApiResult.Success(result));
    }

    /// <summary>
    /// 删除租户
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <returns>是否成功</returns>
    [HttpDelete("{id}")]
    [HbtPerm("identity:tenant:delete")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        var result = await _tenantService.DeleteAsync(id);
        return Ok(HbtApiResult.Success(result));
    }

    /// <summary>
    /// 批量删除租户
    /// </summary>
    /// <param name="ids">租户ID集合</param>
    /// <returns>是否成功</returns>
    [HttpDelete("batch")]
    [HbtPerm("identity:tenant:delete")]
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
    [HbtPerm("identity:tenant:import")]
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
    [HbtPerm("identity:tenant:export")]
    public async Task<IActionResult> ExportAsync([FromQuery] HbtTenantQueryDto query)
    {
        var (fileName, content) = await _tenantService.ExportAsync(query, "Sheet1");
        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }

    /// <summary>
    /// 获取租户导入模板
    /// </summary>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    [HbtPerm("identity:tenant:import")]
    public async Task<IActionResult> GetTemplateAsync()
    {
        var (fileName, content) = await _tenantService.GetTemplateAsync("Sheet1");
        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }

    /// <summary>
    /// 更新租户状态
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <param name="status">状态</param>
    /// <returns>更新后的租户状态信息</returns>
    [HttpPut("{id}/status/{status}")]
    [HbtPerm("identity:tenant:update")]
    public async Task<HbtTenantStatusDto> UpdateStatus(long id, int status)
    {
        return await _tenantService.UpdateStatusAsync(id, status);
    }

    /// <summary>
    /// 获取租户选项列表
    /// </summary>
    /// <returns>租户选项列表</returns>
    [HttpGet("options")]
    [AllowAnonymous]
    public async Task<IActionResult> GetOptionsAsync()
    {
        var result = await _tenantService.GetOptionsAsync();
        return Ok(HbtApiResult.Success(result));
    }
}