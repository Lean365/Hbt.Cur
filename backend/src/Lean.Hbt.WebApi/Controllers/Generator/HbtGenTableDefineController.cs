#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenTableDefineController.cs
// 创建者 : Claude
// 创建时间: 2024-03-21
// 版本号 : V0.0.1
// 描述   : 代码生成表定义控制器
//===================================================================

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Http;
using Lean.Hbt.Application.Dtos.Generator;
using Lean.Hbt.Application.Services.Generator;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Infrastructure.Security.Attributes;
using Lean.Hbt.Domain.IServices.Extensions;

namespace Lean.Hbt.WebApi.Controllers.Generator;

/// <summary>
/// 代码生成表定义控制器
/// </summary>
[Route("api/[controller]", Name = "代码生成表定义")]
[ApiController]
[ApiModule("generator", "代码生成")]
public class HbtGenTableDefineController : HbtBaseController
{
    private readonly IHbtGenTableDefineService _service;


    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service">代码生成表定义服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public HbtGenTableDefineController(
        IHbtGenTableDefineService service,
        IHbtLogger logger,
        IHbtCurrentUser currentUser,
        IHbtLocalizationService localization) : base(logger, currentUser, localization)
    {
        _service = service;

    }

    #region 表定义操作

    /// <summary>
    /// 获取表定义列表
    /// </summary>
    /// <param name="input">查询条件</param>
    /// <returns>表定义列表</returns>
    [HttpGet("tables/list")]
    [HbtPerm("generator:tabledefine:list")]
    public async Task<IActionResult> GetTableList([FromQuery] HbtGenTableDefineQueryDto input)
    {
        var list = await _service.GetTableListAsync(input);
        return Success(list);
    }

    /// <summary>
    /// 获取表定义详情
    /// </summary>
    /// <param name="id">表定义ID</param>
    /// <returns>表定义详情</returns>
    [HttpGet("tables/get/{id}")]
    [HbtPerm("generator:tabledefine:query")]
    public async Task<IActionResult> GetTableInfo(long id)
    {
        var info = await _service.GetTableByIdAsync(id);
        return Success(info);
    }

    /// <summary>
    /// 创建表定义
    /// </summary>
    /// <param name="input">创建参数</param>
    /// <returns>创建结果</returns>
    [HttpPost("tables/create")]
    [HbtPerm("generator:tabledefine:create")]
    public async Task<IActionResult> CreateTable([FromBody] HbtGenTableDefineCreateDto input)
    {
        var result = await _service.CreateTableAsync(input);
        return Success(result);
    }

    /// <summary>
    /// 更新表定义
    /// </summary>
    /// <param name="input">更新参数</param>
    /// <returns>更新结果</returns>
    [HttpPut("tables/update")]
    [HbtPerm("generator:tabledefine:update")]
    public async Task<IActionResult> UpdateTable([FromBody] HbtGenTableDefineUpdateDto input)
    {
        var result = await _service.UpdateTableAsync(input);
        return Success(result);
    }

    /// <summary>
    /// 删除表定义
    /// </summary>
    /// <param name="id">表定义ID</param>
    /// <returns>删除结果</returns>
    [HttpDelete("tables/delete/{id}")]
    [HbtPerm("generator:tabledefine:delete")]
    public async Task<IActionResult> DeleteTable(long id)
    {
        var result = await _service.DeleteTableAsync(id);
        return Success(result);
    }

    /// <summary>
    /// 批量删除表定义
    /// </summary>
    /// <param name="ids">表定义ID数组</param>
    /// <returns>删除结果</returns>
    [HttpDelete("tables/batch/delete/{ids}")]
    [HbtPerm("generator:tabledefine:delete")]
    public async Task<IActionResult> BatchDeleteTables(long[] ids)
    {
        var result = await _service.BatchDeleteTableAsync(ids);
        return Success(result);
    }

    /// <summary>
    /// 导入表定义
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("tables/import")]
    [HbtPerm("generator:tabledefine:import")]
    public async Task<IActionResult> ImportTable(IFormFile file, [FromQuery] string sheetName = "Sheet1")
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("请选择要导入的文件");
        }

        using var stream = file.OpenReadStream();
        var result = await _service.ImportTableAsync(stream, sheetName);
        return Success(result);
    }

    /// <summary>
    /// 导出表定义
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>导出结果</returns>
    [HttpGet("tables/export")]
    [HbtPerm("generator:tabledefine:export")]
    public async Task<IActionResult> ExportTable([FromQuery] HbtGenTableDefineQueryDto input)
    {
        var (fileName, content) = await _service.ExportTableAsync(input);
        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }

    /// <summary>
    /// 获取表定义模板
    /// </summary>
    /// <returns>模板结果</returns>
    [HttpGet("tables/template")]
    [HbtPerm("generator:tabledefine:template")]
    public async Task<IActionResult> GetTableTemplate()
    {
        var (fileName, content) = await _service.GetTableTemplateAsync();
        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }

    /// <summary>
    /// 初始化表结构
    /// </summary>
    /// <param name="input">初始化参数</param>
    /// <returns>初始化结果</returns>
    [HttpPost("tables/initialize")]
    [HbtPerm("generator:tabledefine:initialize")]
    public async Task<IActionResult> InitializeTable([FromBody] HbtGenTableDefineInitializeDto input)
    {
        var result = await _service.InitializeTableListAsync(input);
        return Success(result);
    }

    /// <summary>
    /// 同步表结构
    /// </summary>
    /// <param name="id">表定义ID</param>
    /// <returns>同步结果</returns>
    [HttpPost("tables/sync/{id}")]
    [HbtPerm("generator:tabledefine:sync")]
    public async Task<IActionResult> SyncTable(long id)
    {
        var result = await _service.SyncTableAsync(id);
        return Success(result);
    }

    #endregion

    #region 列定义操作

    /// <summary>
    /// 获取列定义列表
    /// </summary>
    /// <param name="query">查询参数</param>
    /// <returns>列定义列表</returns>
    [HttpGet("columns/list")]
    [HbtPerm("generator:tabledefine:list")]
    public async Task<IActionResult> GetColumnList([FromQuery] HbtGenColumnDefineQueryDto query)
    {
        var list = await _service.GetColumnListAsync(query);
        return Success(list);
    }

    /// <summary>
    /// 创建列定义
    /// </summary>
    /// <param name="input">列定义信息</param>
    /// <returns>创建结果</returns>
    [HttpPost("columns/create")]
    [HbtPerm("generator:tabledefine:create")]
    public async Task<IActionResult> CreateColumn([FromBody] HbtGenColumnDefineCreateDto input)
    {
        var result = await _service.CreateColumnAsync(input);
        return Success(result);
    }

    /// <summary>
    /// 更新列定义
    /// </summary>
    /// <param name="input">更新参数</param>
    /// <returns>更新后的列定义信息</returns>
    [HttpPut("columns/update")]
    [HbtPerm("generator:tabledefine:update")]
    public async Task<IActionResult> UpdateColumn([FromBody] HbtGenColumnDefineUpdateDto input)
    {
        var result = await _service.UpdateColumnAsync(input);
        return Success(result);
    }

    /// <summary>
    /// 删除列定义
    /// </summary>
    /// <param name="id">列定义ID</param>
    /// <returns>是否删除成功</returns>
    [HttpDelete("columns/delete/{id}")]
    [HbtPerm("generator:tabledefine:delete")]
    public async Task<IActionResult> DeleteColumn(long id)
    {
        var result = await _service.DeleteColumnAsync(id);
        return Success(result);
    }

    /// <summary>
    /// 批量删除列定义
    /// </summary>
    /// <param name="ids">列定义ID数组</param>
    /// <returns>是否删除成功</returns>
    [HttpDelete("columns/batch/delete/{ids}")]
    [HbtPerm("generator:tabledefine:delete")]
    public async Task<IActionResult> BatchDeleteColumns(long[] ids)
    {
        var result = await _service.BatchDeleteColumnsAsync(ids);
        return Success(result);
    }

    /// <summary>
    /// 导入列定义
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入的列定义列表</returns>
    [HttpPost("columns/import")]
    [HbtPerm("generator:tabledefine:import")]
    public async Task<IActionResult> ImportColumn(IFormFile file, [FromQuery] string sheetName = "Sheet1")
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("请选择要导入的文件");
        }

        using var stream = file.OpenReadStream();
        var result = await _service.ImportColumnAsync(stream, sheetName);
        return Success(result);
    }

    /// <summary>
    /// 导出列定义
    /// </summary>
    /// <param name="tableId">表ID</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导出的文件名和内容</returns>
    [HttpGet("columns/export")]
    [HbtPerm("generator:tabledefine:export")]
    public async Task<IActionResult> ExportColumn([FromQuery] long tableId, [FromQuery] string sheetName = "Sheet1")
    {
        var (fileName, content) = await _service.ExportColumnAsync(tableId, sheetName);
        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }

    /// <summary>
    /// 获取列定义模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件字节数组</returns>
    [HttpGet("columns/template")]
    [HbtPerm("generator:tabledefine:template")]
    public async Task<IActionResult> GetColumnTemplate([FromQuery] string sheetName = "Sheet1")
    {
        var (fileName, content) = await _service.GetColumnTemplateAsync(sheetName);
        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }

    #endregion
}