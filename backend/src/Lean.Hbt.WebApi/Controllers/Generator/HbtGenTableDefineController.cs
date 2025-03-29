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
using Lean.Hbt.Application.Services.Generator;
using Lean.Hbt.Application.Dtos.Generator;
using Lean.Hbt.Domain.Entities.Generator;
using Lean.Hbt.Domain.IServices.Admin;

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
    /// <param name="localization">本地化服务</param>
    public HbtGenTableDefineController(
        IHbtGenTableDefineService service,
        IHbtLocalizationService localization) : base(localization)
    {
        _service = service;
    }

    /// <summary>
    /// 获取表定义列表
    /// </summary>
    /// <param name="input">查询条件</param>
    /// <returns>表定义列表</returns>
    [HttpGet("list")]
    [HbtPerm("generator:tabledefine:list")]
    public async Task<IActionResult> GetList([FromQuery] HbtGenTableDefineQueryDto input)
    {
        var list = await _service.GetListAsync(input);
        return Success(list);
    }

    /// <summary>
    /// 获取表定义详情
    /// </summary>
    /// <param name="id">表定义ID</param>
    /// <returns>表定义详情</returns>
    [HttpGet("{id}")]
    [HbtPerm("generator:tabledefine:query")]
    public async Task<IActionResult> GetInfo(long id)
    {
        var info = await _service.GetByIdAsync(id);
        return Success(info);
    }

    /// <summary>
    /// 创建表定义
    /// </summary>
    /// <param name="input">创建参数</param>
    /// <returns>创建结果</returns>
    [HttpPost]
    [HbtPerm("generator:tabledefine:create")]
    public async Task<IActionResult> Create([FromBody] HbtGenTableDefineCreateDto input)
    {
        var result = await _service.CreateAsync(input);
        return Success(result);
    }

    /// <summary>
    /// 更新表定义
    /// </summary>
    /// <param name="input">更新参数</param>
    /// <returns>更新结果</returns>
    [HttpPut]
    [HbtPerm("generator:tabledefine:update")]
    public async Task<IActionResult> Update([FromBody] HbtGenTableDefineUpdateDto input)
    {
        var result = await _service.UpdateAsync(input);
        return Success(result);
    }

    /// <summary>
    /// 删除表定义
    /// </summary>
    /// <param name="id">表定义ID</param>
    /// <returns>删除结果</returns>
    [HttpDelete("{id}")]
    [HbtPerm("generator:tabledefine:delete")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _service.DeleteAsync(id);
        return Success(result);
    }

    /// <summary>
    /// 批量删除表定义
    /// </summary>
    /// <param name="ids">表定义ID数组</param>
    /// <returns>删除结果</returns>
    [HttpDelete("batch/{ids}")]
    [HbtPerm("generator:tabledefine:delete")]
    public async Task<IActionResult> BatchDelete(long[] ids)
    {
        var result = await _service.BatchDeleteAsync(ids);
        return Success(result);
    }

    /// <summary>
    /// 导入表定义
    /// </summary>
    /// <param name="input">导入参数</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [HbtPerm("generator:tabledefine:import")]
    public async Task<IActionResult> Import([FromBody] HbtGenTableDefineImportDto input)
    {
        var result = await _service.ImportTablesAsync(input);
        return Success(result);
    }

    /// <summary>
    /// 导出表定义
    /// </summary>
    /// <returns>导出结果</returns>
    [HttpGet("export")]
    [HbtPerm("generator:tabledefine:export")]
    public async Task<IActionResult> Export()
    {
        var result = await _service.ExportTablesAsync();
        return Success(result);
    }

    /// <summary>
    /// 获取表定义模板
    /// </summary>
    /// <returns>模板结果</returns>
    [HttpGet("template")]
    [HbtPerm("generator:tabledefine:template")]
    public async Task<IActionResult> GetTemplate()
    {
        var result = await _service.GetTemplateAsync();
        return Success(result);
    }

    /// <summary>
    /// 初始化表结构
    /// </summary>
    /// <param name="input">初始化参数</param>
    /// <returns>初始化结果</returns>
    [HttpPost("initialize")]
    [HbtPerm("generator:tabledefine:initialize")]
    public async Task<IActionResult> Initialize([FromBody] HbtGenTableDefineCreateDto input)
    {
        var result = await _service.InitializeTablesAsync(input);
        return Success(result);
    }

    /// <summary>
    /// 同步表结构
    /// </summary>
    /// <param name="id">表定义ID</param>
    /// <returns>同步结果</returns>
    [HttpPost("sync/{id}")]
    [HbtPerm("generator:tabledefine:sync")]
    public async Task<IActionResult> SyncTable(long id)
    {
        var result = await _service.SyncTableAsync(id);
        return Success(result);
    }
}
