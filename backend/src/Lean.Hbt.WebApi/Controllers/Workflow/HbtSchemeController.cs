//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSchemeController.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流定义控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Application.Services.Workflow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lean.Hbt.WebApi.Controllers.Workflow;

/// <summary>
/// 工作流定义控制器
/// </summary>
[ApiController]
[Route("api/[controller]", Name = "工作流定义")]
[ApiModule("workflow", "工作流管理")]
[Authorize]
public class HbtSchemeController : HbtBaseController
{
    private readonly IHbtSchemeService _schemeService;

    public HbtSchemeController(IHbtSchemeService schemeService, 
    IHbtLogger logger, 
    IHbtCurrentUser currentUser, 
    IHbtLocalizationService localizationService) : 
    base(logger, currentUser, localizationService)
    {
        _schemeService = schemeService ?? throw new ArgumentNullException(nameof(schemeService));
    }

    /// <summary>
    /// 获取工作流定义列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [HbtPerm("workflow:manage:scheme:list")]
    public async Task<IActionResult> GetListAsync([FromQuery] HbtSchemeQueryDto query)
    {
        try
        {
            var result = await _schemeService.GetListAsync(query);
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 根据ID获取工作流定义
    /// </summary>
    /// <param name="id">工作流定义ID</param>
    /// <returns>工作流定义</returns>
    [HttpGet("{id}")]
    [HbtPerm("workflow:manage:scheme:query")]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        try
        {
            var result = await _schemeService.GetByIdAsync(id);
            if (result == null)
                return Error("工作流定义不存在");

            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 根据键获取工作流定义
    /// </summary>
    /// <param name="schemeKey">工作流定义键</param>
    /// <returns>工作流定义</returns>
    [HttpGet("key/{schemeKey}")]
    [HbtPerm("workflow:manage:scheme:query")]
    public async Task<IActionResult> GetByKeyAsync(string schemeKey)
    {
        try
        {
            var result = await _schemeService.GetByKeyAsync(schemeKey);
            if (result == null)
                return Error("工作流定义不存在");

            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 创建工作流定义
    /// </summary>
    /// <param name="input">工作流定义创建DTO</param>
    /// <returns>工作流定义ID</returns>
    [HttpPost]
    [HbtPerm("workflow:manage:scheme:create")]
    public async Task<IActionResult> CreateAsync([FromBody] HbtSchemeCreateDto input)
    {
        try
        {
            if (input == null)
                return Error("请求参数不能为空");

            var id = await _schemeService.CreateAsync(input);
            return Success(id, "工作流定义创建成功");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 更新工作流定义
    /// </summary>
    /// <param name="input">工作流定义更新DTO</param>
    /// <returns>是否成功</returns>
    [HttpPut]
    [HbtPerm("workflow:manage:scheme:update")]
    public async Task<IActionResult> UpdateAsync([FromBody] HbtSchemeUpdateDto input)
    {
        try
        {
            if (input == null)
                return Error("请求参数不能为空");

            var result = await _schemeService.UpdateAsync(input.SchemeId, input);
            if (result)
                return Success(true, "工作流定义更新成功");
            else
                return Error("工作流定义不存在");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 删除工作流定义
    /// </summary>
    /// <param name="id">工作流定义ID</param>
    /// <returns>是否成功</returns>
    [HttpDelete("{id}")]
    [HbtPerm("workflow:manage:scheme:delete")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        try
        {
            var result = await _schemeService.DeleteAsync(id);
            if (result)
                return Success(true, "工作流定义删除成功");
            else
                return Error("工作流定义不存在");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 批量删除工作流定义
    /// </summary>
    /// <param name="ids">工作流定义ID数组</param>
    /// <returns>是否成功</returns>
    [HttpDelete("batch")]
    [HbtPerm("workflow:manage:scheme:delete")]
    public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
    {
        try
        {
            if (ids == null || ids.Length == 0)
                return Error("请选择要删除的工作流定义");

            var result = await _schemeService.BatchDeleteAsync(ids);
            if (result)
                return Success(true, "批量删除工作流定义成功");
            else
                return Error("批量删除工作流定义失败");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 更新工作流定义状态
    /// </summary>
    /// <param name="id">工作流定义ID</param>
    /// <param name="status">新状态</param>
    /// <param name="reason">原因</param>
    /// <returns>是否成功</returns>
    [HttpPut("{id}/status")]
    [HbtPerm("workflow:manage:scheme:update")]
    public async Task<IActionResult> UpdateStatusAsync(long id, [FromQuery] int status, [FromQuery] string? reason = null)
    {
        try
        {
            var result = await _schemeService.UpdateStatusAsync(id, status, reason);
            if (result)
                return Success(true, "工作流定义状态更新成功");
            else
                return Error("工作流定义不存在");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 获取我的工作流定义列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("my")]
    [HbtPerm("workflow:manage:scheme:list")]
    public async Task<IActionResult> GetMySchemesAsync([FromQuery] HbtSchemeQueryDto query)
    {
        try
        {
            var result = await _schemeService.GetMySchemesAsync(_currentUser.UserId, query);
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    [HbtPerm("workflow:manage:scheme:import")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "Sheet1")
    {
        try
        {
            var (fileName, content) = await _schemeService.GetTemplateAsync(sheetName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导入工作流定义数据
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [HbtPerm("workflow:manage:scheme:import")]
    public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "Sheet1")
    {
        try
        {
            if (file == null || file.Length == 0)
                return Error("请选择要导入的文件");

            using var stream = file.OpenReadStream();
            var result = await _schemeService.ImportAsync(stream, sheetName);
            return Success(result, $"导入完成，成功：{result.success}，失败：{result.fail}");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 导出工作流定义数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件</returns>
    [HttpGet("export")]
    [HbtPerm("workflow:manage:scheme:export")]
    public async Task<IActionResult> ExportAsync([FromQuery] HbtSchemeQueryDto query, [FromQuery] string sheetName = "Sheet1")
    {
        try
        {
            var (fileName, content) = await _schemeService.ExportAsync(query, sheetName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
