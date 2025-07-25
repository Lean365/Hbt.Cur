//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtFormController.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 表单控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Application.Services.Workflow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lean.Hbt.WebApi.Controllers.Workflow;

/// <summary>
/// 表单控制器
/// </summary>
[ApiController]
[Route("api/[controller]", Name = "工作流表单")]
[ApiModule("workflow", "工作流管理")]
[Authorize]
public class HbtFormController : HbtBaseController
{
    private readonly IHbtFormService _formService;

    public HbtFormController(IHbtFormService formService, 
    IHbtLogger logger, 
    IHbtCurrentUser currentUser, 
    IHbtLocalizationService localizationService) : 
    base(logger, currentUser, localizationService)
    {
        _formService = formService ?? throw new ArgumentNullException(nameof(formService));
    }

    /// <summary>
    /// 获取表单定义列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [HbtPerm("workflow:manage:form:list")]
    public async Task<IActionResult> GetListAsync([FromQuery] HbtFormQueryDto query)
    {
        try
        {
            var result = await _formService.GetListAsync(query);
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 根据ID获取表单定义
    /// </summary>
    /// <param name="id">表单定义ID</param>
    /// <returns>表单定义</returns>
    [HttpGet("{id}")]
    [HbtPerm("workflow:manage:form:query")]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        try
        {
            var result = await _formService.GetByIdAsync(id);
            if (result == null)
                return Error("表单定义不存在");

            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 根据键获取表单定义
    /// </summary>
    /// <param name="formKey">表单键</param>
    /// <returns>表单定义</returns>
    [HttpGet("key/{formKey}")]
    [HbtPerm("workflow:manage:form:query")]
    public async Task<IActionResult> GetByKeyAsync(string formKey)
    {
        try
        {
            var result = await _formService.GetByKeyAsync(formKey);
            if (result == null)
                return Error("表单定义不存在");

            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 创建表单定义
    /// </summary>
    /// <param name="input">表单定义创建DTO</param>
    /// <returns>表单定义ID</returns>
    [HttpPost]
    [HbtPerm("workflow:manage:form:create")]
    public async Task<IActionResult> CreateAsync([FromBody] HbtFormCreateDto input)
    {
        try
        {
            if (input == null)
                return Error("请求参数不能为空");

            var id = await _formService.CreateAsync(input);
            return Success(id, "表单定义创建成功");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 更新表单定义
    /// </summary>
    /// <param name="input">表单定义更新DTO</param>
    /// <returns>是否成功</returns>
    [HttpPut]
    [HbtPerm("workflow:manage:form:update")]
    public async Task<IActionResult> UpdateAsync([FromBody] HbtFormUpdateDto input)
    {
        try
        {
            if (input == null)
                return Error("请求参数不能为空");

            var result = await _formService.UpdateAsync(input.FormId, input);
            if (result)
                return Success(true, "表单定义更新成功");
            else
                return Error("表单定义不存在");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 删除表单定义
    /// </summary>
    /// <param name="id">表单定义ID</param>
    /// <returns>是否成功</returns>
    [HttpDelete("{id}")]
    [HbtPerm("workflow:manage:form:delete")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        try
        {
            var result = await _formService.DeleteAsync(id);
            if (result)
                return Success(true, "表单定义删除成功");
            else
                return Error("表单定义不存在");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 批量删除表单定义
    /// </summary>
    /// <param name="ids">表单定义ID数组</param>
    /// <returns>是否成功</returns>
    [HttpDelete("batch")]
    [HbtPerm("workflow:manage:form:delete")]
    public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
    {
        try
        {
            if (ids == null || ids.Length == 0)
                return Error("请选择要删除的表单定义");

            var result = await _formService.BatchDeleteAsync(ids);
            if (result)
                return Success(true, "批量删除表单定义成功");
            else
                return Error("批量删除表单定义失败");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 更新表单状态
    /// </summary>
    /// <param name="id">表单定义ID</param>
    /// <param name="status">新状态</param>
    /// <returns>是否成功</returns>
    [HttpPut("{id}/status")]
    [HbtPerm("workflow:manage:form:update")]
    public async Task<IActionResult> UpdateStatusAsync(long id, [FromQuery] int status)
    {
        try
        {
            var result = await _formService.UpdateStatusAsync(id, status);
            if (result)
                return Success(true, "表单状态更新成功");
            else
                return Error("表单定义不存在");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 获取我的表单列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("my/{userId}")]
    [HbtPerm("workflow:manage:form:list")]
    public async Task<IActionResult> GetMyFormsAsync(long userId, [FromQuery] HbtFormQueryDto query)
    {
        try
        {
            var result = await _formService.GetMyFormsAsync(userId, query);
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
    [HbtPerm("workflow:manage:form:import")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "Sheet1")
    {
        try
        {
            var (fileName, content) = await _formService.GetTemplateAsync(sheetName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导入表单数据
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [HbtPerm("workflow:manage:form:import")]
    public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "Sheet1")
    {
        try
        {
            if (file == null || file.Length == 0)
                return Error("请选择要导入的文件");

            using var stream = file.OpenReadStream();
            var result = await _formService.ImportAsync(stream, sheetName);
            return Success(result, $"导入完成，成功：{result.success}，失败：{result.fail}");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 导出表单数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件</returns>
    [HttpGet("export")]
    [HbtPerm("workflow:manage:form:export")]
    public async Task<IActionResult> ExportAsync([FromQuery] HbtFormQueryDto query, [FromQuery] string sheetName = "Sheet1")
    {
        try
        {
            var (fileName, content) = await _formService.ExportAsync(query, sheetName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
