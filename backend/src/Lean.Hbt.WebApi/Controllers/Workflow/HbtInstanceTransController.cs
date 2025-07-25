//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtInstanceTransController.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流实例流转历史控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Application.Services.Workflow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lean.Hbt.WebApi.Controllers.Workflow;

/// <summary>
/// 工作流实例流转历史控制器
/// </summary>
[ApiController]
[Route("api/[controller]", Name = "工作流流转历史")]
[ApiModule("workflow", "工作流管理")]
[Authorize]
public class HbtInstanceTransController : HbtBaseController
{
    private readonly IHbtInstanceTransService _instanceTransService;

    public HbtInstanceTransController(
        IHbtInstanceTransService instanceTransService,
        IHbtLogger logger,
        IHbtCurrentUser currentUser,
        IHbtLocalizationService localizationService) : base(logger, currentUser, localizationService)
    {
        _instanceTransService = instanceTransService ?? throw new ArgumentNullException(nameof(instanceTransService));
    }

    /// <summary>
    /// 获取工作流实例流转列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [HbtPerm("workflow:manage:trans:list")]
    public async Task<IActionResult> GetListAsync([FromQuery] HbtInstanceTransQueryDto query)
    {
        try
        {
            var result = await _instanceTransService.GetListAsync(query);
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 根据ID获取工作流实例流转
    /// </summary>
    /// <param name="id">工作流实例流转ID</param>
    /// <returns>工作流实例流转</returns>
    [HttpGet("{id}")]
    [HbtPerm("workflow:manage:trans:query")]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        try
        {
            var result = await _instanceTransService.GetByIdAsync(id);
            if (result == null)
                return Error("工作流实例流转不存在");

            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 创建工作流实例流转
    /// </summary>
    /// <param name="input">工作流实例流转创建DTO</param>
    /// <returns>工作流实例流转ID</returns>
    [HttpPost]
    [HbtPerm("workflow:manage:trans:create")]
    public async Task<IActionResult> CreateAsync([FromBody] HbtInstanceTransCreateDto input)
    {
        try
        {
            if (input == null)
                return Error("请求参数不能为空");

            var id = await _instanceTransService.CreateAsync(input);
            return Success(id, "工作流实例流转创建成功");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 删除工作流实例流转
    /// </summary>
    /// <param name="id">工作流实例流转ID</param>
    /// <returns>是否成功</returns>
    [HttpDelete("{id}")]
    [HbtPerm("workflow:manage:trans:delete")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        try
        {
            var result = await _instanceTransService.DeleteAsync(id);
            if (result)
                return Success(true, "工作流实例流转删除成功");
            else
                return Error("工作流实例流转不存在");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 批量删除工作流实例流转
    /// </summary>
    /// <param name="ids">工作流实例流转ID数组</param>
    /// <returns>是否成功</returns>
    [HttpDelete("batch")]
    [HbtPerm("workflow:manage:trans:delete")]
    public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
    {
        try
        {
            if (ids == null || ids.Length == 0)
                return Error("请选择要删除的工作流实例流转");

            var result = await _instanceTransService.BatchDeleteAsync(ids);
            if (result)
                return Success(true, "批量删除工作流实例流转成功");
            else
                return Error("批量删除工作流实例流转失败");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 根据实例ID获取流转列表
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <returns>流转列表</returns>
    [HttpGet("instance/{instanceId}")]
    [HbtPerm("workflow:manage:trans:query")]
    public async Task<IActionResult> GetByInstanceIdAsync(long instanceId)
    {
        try
        {
            var query = new HbtInstanceTransQueryDto { InstanceId = instanceId };
            var result = await _instanceTransService.GetListAsync(query);
            return Success(result.Rows);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 根据实例ID获取流转分页列表
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("instance/{instanceId}/paged")]
    [HbtPerm("workflow:manage:trans:query")]
    public async Task<IActionResult> GetByInstanceIdPagedAsync(long instanceId, [FromQuery] HbtInstanceTransQueryDto query)
    {
        try
        {
            query.InstanceId = instanceId;
            var result = await _instanceTransService.GetListAsync(query);
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
    [HbtPerm("workflow:manage:trans:import")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "Sheet1")
    {
        try
        {
            var (fileName, content) = await _instanceTransService.GetTemplateAsync(sheetName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导入工作流实例流转数据
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [HbtPerm("workflow:manage:trans:import")]
    public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "Sheet1")
    {
        try
        {
            if (file == null || file.Length == 0)
                return Error("请选择要导入的文件");

            using var stream = file.OpenReadStream();
            var result = await _instanceTransService.ImportAsync(stream, sheetName);
            return Success(result, $"导入完成，成功：{result.success}，失败：{result.fail}");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 导出工作流实例流转数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件</returns>
    [HttpGet("export")]
    [HbtPerm("workflow:manage:trans:export")]
    public async Task<IActionResult> ExportAsync([FromQuery] HbtInstanceTransQueryDto query, [FromQuery] string sheetName = "Sheet1")
    {
        try
        {
            var (fileName, content) = await _instanceTransService.ExportAsync(query, sheetName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 获取我的待办任务（当前用户为处理人且状态为待处理）
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页待办任务</returns>
    [HttpGet("my/todo")]
    [HbtPerm("workflow:manage:trans:todo")]
    public async Task<IActionResult> GetUserTodoListAsync([FromQuery] HbtInstanceTransQueryDto query)
    {
        // 这里假设NodeId或其它业务字段可用于筛选当前用户的待办
        query.TransState = 0; // 0=待处理
        query.CreateBy = _currentUser.UserName;
        var result = await _instanceTransService.GetListAsync(query);
        return Success(result);
    }

    /// <summary>
    /// 获取我的已办任务（当前用户为处理人且状态为已处理）
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页已办任务</returns>
    [HttpGet("my/done")]
    [HbtPerm("workflow:manage:trans:done")]
    public async Task<IActionResult> GetUserDoneListAsync([FromQuery] HbtInstanceTransQueryDto query)
    {
        query.TransState = 1; // 1=已处理
        query.CreateBy = _currentUser.UserName;
        var result = await _instanceTransService.GetListAsync(query);
        return Success(result);
    }

    /// <summary>
    /// 获取我的流程列表（当前用户发起的流程）
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页流程列表</returns>
    [HttpGet("my/process")]
    [HbtPerm("workflow:manage:trans:process")]
    public async Task<IActionResult> GetUserProcessListAsync([FromQuery] HbtInstanceTransQueryDto query)
    {
        // 查询当前用户发起的流程，通常TransState为0表示进行中
        query.CreateBy = _currentUser.UserName;
        var result = await _instanceTransService.GetListAsync(query);
        return Success(result);
    }
} 