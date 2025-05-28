#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenConfigController.cs
// 创建者 : Lean365
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 代码生成配置控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Generator;
using Lean.Hbt.Application.Services.Generator;

namespace Lean.Hbt.WebApi.Controllers.Generator;

/// <summary>
/// 代码生成配置控制器
/// </summary>
[Route("api/[controller]", Name = "代码生成配置")]
[ApiController]
[ApiModule("generator", "代码生成")]
public class HbtGenConfigController : HbtBaseController
{
    private readonly IHbtGenConfigService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service">代码生成配置服务</param>
    /// <param name="localization">本地化服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="currentTenant">当前租户服务</param>
    public HbtGenConfigController(
        IHbtGenConfigService service,
        IHbtCurrentUser currentUser,
        IHbtCurrentTenant currentTenant,
        IHbtLocalizationService localization,
        IHbtLogger logger) : base(logger, currentUser, currentTenant, localization)
    {
        _service = service;
    }

    /// <summary>
    /// 获取代码生成配置列表
    /// </summary>
    /// <param name="input">查询条件</param>
    /// <returns>代码生成配置列表</returns>
    [HttpGet("list")]
    [HbtPerm("generator:config:list")]
    public async Task<IActionResult> GetList([FromQuery] HbtGenConfigQueryDto input)
    {
        var list = await _service.GetListAsync(input);
        return Success(list);
    }

    /// <summary>
    /// 获取代码生成配置详情
    /// </summary>
    /// <param name="id">主键ID</param>
    /// <returns>代码生成配置详情</returns>
    [HttpGet("{id}")]
    [HbtPerm("generator:config:query")]
    public async Task<IActionResult> GetById(long id)
    {
        var entity = await _service.GetByIdAsync(id);
        if (entity == null)
        {
            return NotFound();
        }
        return Success(entity);
    }

    /// <summary>
    /// 创建代码生成配置
    /// </summary>
    /// <param name="input">代码生成配置信息</param>
    /// <returns>创建结果</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] HbtGenConfigCreateDto input)
    {
        var entity = await _service.CreateAsync(input);
        return Success(entity);
    }

    /// <summary>
    /// 更新代码生成配置
    /// </summary>
    /// <param name="input">代码生成配置信息</param>
    /// <returns>更新结果</returns>
    [HttpPut]
    [HbtPerm("generator:config:update")]
    public async Task<IActionResult> Update([FromBody] HbtGenConfigUpdateDto input)
    {
        var entity = await _service.UpdateAsync(input);
        return Success(entity);
    }

    /// <summary>
    /// 删除代码生成配置
    /// </summary>
    /// <param name="id">主键ID</param>
    /// <returns>删除结果</returns>
    [HttpDelete("{id}")]
    [HbtPerm("generator:config:delete")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _service.DeleteAsync(id);
        return Success(result);
    }

    /// <summary>
    /// 批量删除代码生成配置
    /// </summary>
    /// <param name="ids">主键ID集合</param>
    /// <returns>删除结果</returns>
    [HttpPost("batch-delete")]
    [HbtPerm("generator:config:delete")]
    public async Task<IActionResult> BatchDelete([FromBody] long[] ids)
    {
        var result = await _service.BatchDeleteAsync(ids);
        return Success(result);
    }

    /// <summary>
    /// 导入代码生成配置
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [HbtPerm("generator:config:import")]
    public async Task<IActionResult> Import(IFormFile file)
    {
        using var stream = file.OpenReadStream();
        var result = await _service.ImportConfigsAsync(stream, file.FileName);
        return Success(result);
    }

    /// <summary>
    /// 导出代码生成配置
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>导出结果</returns>
    [HttpGet("export")]
    [HbtPerm("generator:config:export")]
    public async Task<IActionResult> Export([FromQuery] HbtGenConfigQueryDto query)
    {
        var result = await _service.ExportConfigsAsync(query);
        return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <returns>模板文件</returns>
    [HttpGet("template")]
    [HbtPerm("generator:config:import")]
    public async Task<IActionResult> GetTemplate()
    {
        var result = await _service.GetTemplateAsync();
        return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
    }
}