//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtInstanceOperController.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流实例操作记录控制器
//===================================================================

using Hbt.Cur.Application.Dtos.Workflow;
using Hbt.Cur.Application.Services.Workflow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hbt.Cur.WebApi.Controllers.Workflow;

/// <summary>
/// 工作流实例操作记录控制器
/// </summary>
[ApiController]
[Route("api/[controller]", Name = "工作流操作记录")]
[ApiModule("workflow", "工作流管理")]
[Authorize]
public class HbtInstanceOperController : HbtBaseController
{
    private readonly IHbtInstanceOperService _instanceOperService;

    public HbtInstanceOperController(
        IHbtInstanceOperService instanceOperService,
        IHbtLogger logger,
        IHbtCurrentUser currentUser,
        IHbtLocalizationService localizationService) : base(logger, currentUser, localizationService)
    {
        _instanceOperService = instanceOperService ?? throw new ArgumentNullException(nameof(instanceOperService));
    }

    /// <summary>
    /// 获取工作流实例操作列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [HbtPerm("workflow:manage:oper:list")]
    public async Task<IActionResult> GetListAsync([FromQuery] HbtInstanceOperQueryDto query)
    {
        try
        {
            var result = await _instanceOperService.GetListAsync(query);
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 根据ID获取工作流实例操作
    /// </summary>
    /// <param name="id">工作流实例操作ID</param>
    /// <returns>工作流实例操作</returns>
    [HttpGet("{id}")]
    [HbtPerm("workflow:manage:oper:query")]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        try
        {
            var result = await _instanceOperService.GetByIdAsync(id);
            if (result == null)
                return Error("工作流实例操作不存在");

            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 创建审批操作
    /// </summary>
    /// <param name="input">审批参数</param>
    /// <returns>操作ID</returns>
    [HttpPost("approve")]
    [HbtPerm("workflow:manage:oper:create")]
    public async Task<IActionResult> CreateApproveAsync([FromBody] HbtInstanceApproveDto input)
    {
        try
        {
            if (input == null)
                return Error("请求参数不能为空");

            var id = await _instanceOperService.CreateApproveAsync(input);
            return Success(id, "审批操作创建成功");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 创建工作流实例操作
    /// </summary>
    /// <param name="input">工作流实例操作创建DTO</param>
    /// <returns>工作流实例操作ID</returns>
    [HttpPost]
    [HbtPerm("workflow:manage:oper:create")]
    public async Task<IActionResult> CreateAsync([FromBody] HbtInstanceOperCreateDto input)
    {
        try
        {
            if (input == null)
                return Error("请求参数不能为空");

            var id = await _instanceOperService.CreateAsync(input);
            return Success(id, "工作流实例操作创建成功");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 删除工作流实例操作
    /// </summary>
    /// <param name="id">工作流实例操作ID</param>
    /// <returns>是否成功</returns>
    [HttpDelete("{id}")]
    [HbtPerm("workflow:manage:oper:delete")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        try
        {
            var result = await _instanceOperService.DeleteAsync(id);
            if (result)
                return Success(true, "工作流实例操作删除成功");
            else
                return Error("工作流实例操作不存在");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 批量删除工作流实例操作
    /// </summary>
    /// <param name="ids">工作流实例操作ID数组</param>
    /// <returns>是否成功</returns>
    [HttpDelete("batch")]
    [HbtPerm("workflow:manage:oper:delete")]
    public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
    {
        try
        {
            if (ids == null || ids.Length == 0)
                return Error("请选择要删除的工作流实例操作");

            var result = await _instanceOperService.BatchDeleteAsync(ids);
            if (result)
                return Success(true, "批量删除工作流实例操作成功");
            else
                return Error("批量删除工作流实例操作失败");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 获取我的操作列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("my/{userId}")]
    [HbtPerm("workflow:manage:oper:list")]
    public async Task<IActionResult> GetMyOperationsAsync(long userId, [FromQuery] HbtInstanceOperQueryDto query)
    {
        try
        {
            var result = await _instanceOperService.GetMyOperationsAsync(userId, query);
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 根据实例ID获取操作列表
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <returns>操作列表</returns>
    [HttpGet("instance/{instanceId}")]
    [HbtPerm("workflow:manage:oper:query")]
    public async Task<IActionResult> GetByInstanceIdAsync(long instanceId)
    {
        try
        {
            var query = new HbtInstanceOperQueryDto { InstanceId = instanceId };
            var result = await _instanceOperService.GetListAsync(query);
            return Success(result.Rows);
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
    [HbtPerm("workflow:manage:oper:import")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "Sheet1")
    {
        try
        {
            var (fileName, content) = await _instanceOperService.GetTemplateAsync(sheetName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导入工作流实例操作数据
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [HbtPerm("workflow:manage:oper:import")]
    public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "Sheet1")
    {
        try
        {
            if (file == null || file.Length == 0)
                return Error("请选择要导入的文件");

            using var stream = file.OpenReadStream();
            var result = await _instanceOperService.ImportAsync(stream, sheetName);
            return Success(result, $"导入完成，成功：{result.success}，失败：{result.fail}");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 导出工作流实例操作数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件</returns>
    [HttpGet("export")]
    [HbtPerm("workflow:manage:oper:export")]
    public async Task<IActionResult> ExportAsync([FromQuery] HbtInstanceOperQueryDto query, [FromQuery] string sheetName = "Sheet1")
    {
        try
        {
            var (fileName, content) = await _instanceOperService.ExportAsync(query, sheetName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
} 