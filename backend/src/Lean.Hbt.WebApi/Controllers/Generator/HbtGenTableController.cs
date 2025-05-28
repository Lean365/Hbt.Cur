#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenTableController.cs
// 创建者 : Lean365
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 代码生成表控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Generator;
using Lean.Hbt.Application.Services.Generator;
using Lean.Hbt.Application.Services.Generator.CodeGenerator;
using Lean.Hbt.Domain.Entities.Generator;
using Mapster;

namespace Lean.Hbt.WebApi.Controllers.Generator;

/// <summary>
/// 代码生成表控制器
/// </summary>
[Route("api/[controller]", Name = "代码生成表")]
[ApiController]
[ApiModule("generator", "代码生成")]
public class HbtGenTableController : HbtBaseController
{
    private readonly IHbtGenTableService _genTableService;
    private readonly IHbtCodeGeneratorService _codeGeneratorService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="genTableService">代码生成表服务</param>
    /// <param name="codeGeneratorService">代码生成服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="currentTenant">当前租户服务</param>
    /// <param name="localization">本地化服务</param>
    public HbtGenTableController(
        IHbtGenTableService genTableService,
        IHbtCodeGeneratorService codeGeneratorService,
        IHbtLogger logger,
        IHbtCurrentUser currentUser,
        IHbtCurrentTenant currentTenant,
        IHbtLocalizationService localization) : base(logger, currentUser, currentTenant, localization)
    {
        _genTableService = genTableService;
        _codeGeneratorService = codeGeneratorService;
    }

    /// <summary>
    /// 获取代码生成表分页列表
    /// </summary>
    /// <param name="query">查询参数</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [HbtPerm("generator:table:list")]
    public async Task<IActionResult> GetList([FromQuery] HbtGenTableQueryDto query)
    {
        var result = await _genTableService.GetListAsync(query);
        return Success(result);
    }

    /// <summary>
    /// 获取代码生成表详情
    /// </summary>
    /// <param name="id">主键ID</param>
    /// <returns>代码生成表信息</returns>
    [HttpGet("{id}")]
    [HbtPerm("generator:table:query")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await _genTableService.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        return Success(result);
    }

    /// <summary>
    /// 获取表字段列表
    /// </summary>
    /// <param name="tableId">表ID</param>
    /// <returns>字段列表</returns>
    [HttpGet("columns/{tableId}")]
    [HbtPerm("generator:table:columns")]
    public async Task<IActionResult> GetColumns(long tableId)
    {
        var result = await _genTableService.GetColumnListAsync(tableId);
        return Success(result);
    }

    /// <summary>
    /// 创建代码生成表
    /// </summary>
    /// <param name="input">创建参数</param>
    /// <returns>创建结果</returns>
    [HttpPost]
    [HbtPerm("generator:table:create")]
    public async Task<IActionResult> Create([FromBody] HbtGenTableCreateDto input)
    {
        var result = await _genTableService.CreateAsync(input);
        return Success(result);
    }

    /// <summary>
    /// 更新代码生成表
    /// </summary>
    /// <param name="input">更新参数</param>
    /// <returns>更新结果</returns>
    [HttpPut]
    [HbtPerm("generator:table:update")]
    public async Task<IActionResult> Update([FromBody] HbtGenTableUpdateDto input)
    {
        var result = await _genTableService.UpdateAsync(input);
        return Success(result);
    }

    /// <summary>
    /// 删除代码生成表
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>删除结果</returns>
    [HttpDelete("{id}")]
    [HbtPerm("generator:table:delete")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _genTableService.DeleteAsync(id);
        return Success(result);
    }

    /// <summary>
    /// 导入表
    /// </summary>
    /// <param name="input">导入参数</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [HbtPerm("generator:table:import")]
    public async Task<IActionResult> ImportTable([FromBody] HbtGenTableImportDto input)
    {
        var result = await _genTableService.ImportTablesAsync(input);
        return Success(result);
    }

    /// <summary>
    /// 获取数据库列表
    /// </summary>
    /// <returns>数据库列表</returns>
    [HttpGet("databases")]
    [HbtPerm("generator:table:databases")]
    public async Task<IActionResult> GetDatabasesByDb()
    {
        var result = await _genTableService.GetDatabaseListByDbAsync();
        return Success(result);
    }

    /// <summary>
    /// 获取表列表
    /// </summary>
    /// <param name="databaseName">数据库名称</param>
    /// <returns>表列表</returns>
    [HttpGet("tables/{databaseName}")]
    [HbtPerm("generator:table:tables")]
    public async Task<IActionResult> GetTablesByDb(string databaseName)
    {
        var result = await _genTableService.GetTableListByDbAsync(databaseName);
        return Success(result);
    }

    /// <summary>
    /// 获取表字段列表
    /// </summary>
    /// <param name="databaseName">数据库名称</param>
    /// <param name="tableName">表名</param>
    /// <returns>字段列表</returns>
    [HttpGet("columns/{databaseName}/{tableName}")]
    [HbtPerm("generator:table:columns")]
    public async Task<IActionResult> GetTableColumnsByDb(string databaseName, string tableName)
    {
        var result = await _genTableService.GetTableColumnListByDbAsync(databaseName, tableName);
        return Success(result);
    }

    /// <summary>
    /// 导入表及其所有字段信息
    /// </summary>
    /// <param name="databaseName">数据库名</param>
    /// <param name="tableName">表名</param>
    /// <returns>操作结果</returns>
    [HttpPost("import-table-and-columns/{databaseName}/{tableName}")]
    [HbtPerm("generator:table:import")]
    public async Task<IActionResult> ImportTableAndColumns(string databaseName, string tableName)
    {
        var result = await _genTableService.ImportTableAndColumnsAsync(databaseName, tableName);
        return result ? Success("导入成功") : Error("导入失败");
    }

    /// <summary>
    /// 同步表结构
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>同步结果</returns>
    [HttpPost("sync/{id}")]
    [HbtPerm("generator:table:sync")]
    public async Task<IActionResult> SyncTable(long id)
    {
        var result = await _genTableService.SyncTableAsync(id);
        return Success(result);
    }

    /// <summary>
    /// 预览代码
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>预览结果</returns>
    [HttpGet("preview/{id}")]
    [HbtPerm("generator:table:preview")]
    public async Task<IActionResult> PreviewCode(long id)
    {
        var table = await _genTableService.GetByIdAsync(id);
        if (table == null)
        {
            return Error("表不存在");
        }

        var result = await _codeGeneratorService.PreviewCodeAsync(table.Adapt<HbtGenTable>());
        return Success(result);
    }

    /// <summary>
    /// 生成代码
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>生成结果</returns>
    [HttpPost("generate/{id}")]
    [HbtPerm("generator:table:generate")]
    public async Task<IActionResult> GenerateCode(long id)
    {
        var table = await _genTableService.GetByIdAsync(id);
        if (table == null)
        {
            return Error("表不存在");
        }

        var result = await _codeGeneratorService.GenerateCodeAsync(table.Adapt<HbtGenTable>());
        return Success(result);
    }

    /// <summary>
    /// 批量生成代码
    /// </summary>
    /// <param name="ids">表ID列表</param>
    /// <returns>生成结果</returns>
    [HttpPost("generate/batch")]
    [HbtPerm("generator:table:generate")]
    public async Task<IActionResult> BatchGenerateCode([FromBody] long[] ids)
    {
        var results = new List<bool>();
        foreach (var id in ids)
        {
            var table = await _genTableService.GetByIdAsync(id);
            if (table == null)
            {
                continue;
            }

            var result = await _codeGeneratorService.GenerateCodeAsync(table.Adapt<HbtGenTable>());
            results.Add(result);
        }

        return Success(results);
    }

    /// <summary>
    /// 下载代码
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>下载结果</returns>
    [HttpGet("download/{id}")]
    [HbtPerm("generator:table:download")]
    public async Task<IActionResult> DownloadCode(long id)
    {
        var table = await _genTableService.GetByIdAsync(id);
        if (table == null)
        {
            return Error("表不存在");
        }

        var result = await _codeGeneratorService.DownloadCodeAsync(table.Adapt<HbtGenTable>());
        return File(result, "application/zip", "code.zip");
    }
}