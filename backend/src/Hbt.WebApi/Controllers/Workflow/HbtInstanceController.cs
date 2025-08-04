//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtInstanceController.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流实例控制器
//===================================================================

using Hbt.Application.Dtos.Workflow;
using Hbt.Application.Services.Workflow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hbt.WebApi.Controllers.Workflow;

/// <summary>
/// 工作流实例控制器
/// </summary>
[ApiController]
[Route("api/[controller]", Name = "工作流实例")]
[ApiModule("workflow", "工作流管理")]
[Authorize]
public class HbtInstanceController : HbtBaseController
{
    private readonly IHbtInstanceService _instanceService;

    public HbtInstanceController(IHbtInstanceService instanceService, 
    IHbtLogger logger, 
    IHbtCurrentUser currentUser, 
    IHbtLocalizationService localizationService) : 
    base(logger, currentUser, localizationService)
    {
        _instanceService = instanceService ?? throw new ArgumentNullException(nameof(instanceService));
    }

    /// <summary>
    /// 获取工作流实例列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [HbtPerm("workflow:manage:instance:list")]
    public async Task<IActionResult> GetListAsync([FromQuery] HbtInstanceQueryDto query)
    {
        try
        {
            var result = await _instanceService.GetListAsync(query);
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 根据ID获取工作流实例
    /// </summary>
    /// <param name="id">工作流实例ID</param>
    /// <returns>工作流实例</returns>
    [HttpGet("{id}")]
    [HbtPerm("workflow:manage:instance:query")]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        try
        {
            var result = await _instanceService.GetByIdAsync(id);
            if (result == null)
                return Error("工作流实例不存在");

            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 根据业务键获取工作流实例
    /// </summary>
    /// <param name="businessKey">业务键</param>
    /// <returns>工作流实例</returns>
    [HttpGet("business/{businessKey}")]
    [HbtPerm("workflow:manage:instance:query")]
    public async Task<IActionResult> GetByBusinessKeyAsync(string businessKey)
    {
        try
        {
            var result = await _instanceService.GetByBusinessKeyAsync(businessKey);
            if (result == null)
                return Error("工作流实例不存在");

            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 创建工作流实例
    /// </summary>
    /// <param name="input">工作流实例创建DTO</param>
    /// <returns>工作流实例ID</returns>
    [HttpPost]
    [HbtPerm("workflow:manage:instance:create")]
    public async Task<IActionResult> CreateAsync([FromBody] HbtInstanceCreateDto input)
    {
        try
        {
            if (input == null)
                return Error("请求参数不能为空");

            var id = await _instanceService.CreateAsync(input);
            return Success(id, "工作流实例创建成功");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 更新工作流实例
    /// </summary>
    /// <param name="input">工作流实例更新DTO</param>
    /// <returns>是否成功</returns>
    [HttpPut]
    [HbtPerm("workflow:manage:instance:update")]
    public async Task<IActionResult> UpdateAsync([FromBody] HbtInstanceUpdateDto input)
    {
        try
        {
            if (input == null)
                return Error("请求参数不能为空");

            var result = await _instanceService.UpdateAsync(input.InstanceId, input);
            if (result)
                return Success(true, "工作流实例更新成功");
            else
                return Error("工作流实例不存在");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 删除工作流实例
    /// </summary>
    /// <param name="id">工作流实例ID</param>
    /// <returns>是否成功</returns>
    [HttpDelete("{id}")]
    [HbtPerm("workflow:manage:instance:delete")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        try
        {
            var result = await _instanceService.DeleteAsync(id);
            if (result)
                return Success(true, "工作流实例删除成功");
            else
                return Error("工作流实例不存在");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 批量删除工作流实例
    /// </summary>
    /// <param name="ids">工作流实例ID数组</param>
    /// <returns>是否成功</returns>
    [HttpDelete("batch")]
    [HbtPerm("workflow:manage:instance:delete")]
    public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
    {
        try
        {
            if (ids == null || ids.Length == 0)
                return Error("请选择要删除的工作流实例");

            var result = await _instanceService.BatchDeleteAsync(ids);
            if (result)
                return Success(true, "批量删除工作流实例成功");
            else
                return Error("批量删除工作流实例失败");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 更新工作流实例状态
    /// </summary>
    /// <param name="id">工作流实例ID</param>
    /// <param name="status">新状态</param>
    /// <param name="reason">原因</param>
    /// <returns>是否成功</returns>
    [HttpPut("{id}/status")]
    [HbtPerm("workflow:manage:instance:update")]
    public async Task<IActionResult> UpdateStatusAsync(long id, [FromQuery] int status, [FromQuery] string? reason = null)
    {
        try
        {
            var result = await _instanceService.UpdateStatusAsync(id, status, reason);
            if (result)
                return Success(true, "工作流实例状态更新成功");
            else
                return Error("工作流实例不存在");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 设置工作流实例变量
    /// </summary>
    /// <param name="id">工作流实例ID</param>
    /// <param name="variables">变量字典</param>
    /// <returns>是否成功</returns>
    [HttpPut("{id}/variables")]
    [HbtPerm("workflow:manage:instance:update")]
    public async Task<IActionResult> SetVariablesAsync(long id, [FromBody] Dictionary<string, object> variables)
    {
        try
        {
            if (variables == null)
                return Error("变量不能为空");

            var result = await _instanceService.SetVariablesAsync(id, variables);
            if (result)
                return Success(true, "工作流实例变量设置成功");
            else
                return Error("工作流实例不存在");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 获取我的工作流实例列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("my")]
    [HbtPerm("workflow:manage:instance:list")]
    public async Task<IActionResult> GetMyInstancesAsync([FromQuery] HbtInstanceQueryDto query)
    {
        try
        {
            var result = await _instanceService.GetMyInstancesAsync(_currentUser.UserId, query);
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
    [HbtPerm("workflow:manage:instance:import")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "Sheet1")
    {
        try
        {
            var (fileName, content) = await _instanceService.GetTemplateAsync(sheetName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导入工作流实例数据
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [HbtPerm("workflow:manage:instance:import")]
    public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "Sheet1")
    {
        try
        {
            if (file == null || file.Length == 0)
                return Error("请选择要导入的文件");

            using var stream = file.OpenReadStream();
            var result = await _instanceService.ImportAsync(stream, sheetName);
            return Success(result, $"导入完成，成功：{result.success}，失败：{result.fail}");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 导出工作流实例数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件</returns>
    [HttpGet("export")]
    [HbtPerm("workflow:manage:instance:export")]
    public async Task<IActionResult> ExportAsync([FromQuery] HbtInstanceQueryDto query, [FromQuery] string sheetName = "Sheet1")
    {
        try
        {
            var (fileName, content) = await _instanceService.ExportAsync(query, sheetName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
