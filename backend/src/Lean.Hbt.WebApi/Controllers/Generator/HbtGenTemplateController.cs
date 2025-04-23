//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenTemplateController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 代码生成模板控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Generator;
using Lean.Hbt.Application.Services.Generator;

namespace Lean.Hbt.WebApi.Controllers.Generator
{
    /// <summary>
    /// 代码生成模板控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-22
    /// </remarks>
    [Route("api/[controller]", Name = "代码生成模板")]
    [ApiController]
    [ApiModule("generator", "代码生成")]
    public class HbtGenTemplateController : HbtBaseController
    {
        private readonly IHbtGenTemplateService _templateService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="templateService">模板服务</param>
        /// <param name="localization">本地化服务</param>
        /// <param name="logger">日志服务</param>
        public HbtGenTemplateController(IHbtGenTemplateService templateService,
                        IHbtLocalizationService localization,
            IHbtLogger logger) : base(localization, logger)
        {
            _templateService = templateService;
        }

        /// <summary>
        /// 获取模板分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>模板分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("generator:template:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtGenTemplateQueryDto query)
        {
            var result = await _templateService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取模板详情
        /// </summary>
        /// <param name="templateId">模板ID</param>
        /// <returns>模板详情</returns>
        [HttpGet("{templateId}")]
        [HbtPerm("generator:template:query")]
        public async Task<IActionResult> GetByIdAsync(long templateId)
        {
            var result = await _templateService.GetByIdAsync(templateId);
            return Success(result);
        }

        /// <summary>
        /// 创建模板
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>模板ID</returns>
        [HttpPost]
        [HbtPerm("generator:template:create")]
        public async Task<IActionResult> CreateAsync([FromBody] HbtGenTemplateCreateDto input)
        {
            var result = await _templateService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新模板
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [HbtPerm("generator:template:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtGenTemplateUpdateDto input)
        {
            var result = await _templateService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="templateId">模板ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{templateId}")]
        [HbtPerm("generator:template:delete")]
        public async Task<IActionResult> DeleteAsync(long templateId)
        {
            var result = await _templateService.DeleteAsync(templateId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除模板
        /// </summary>
        /// <param name="templateIds">模板ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [HbtPerm("generator:template:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] templateIds)
        {
            var result = await _templateService.BatchDeleteAsync(templateIds);
            return Success(result);
        }

        /// <summary>
        /// 导入模板数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [HbtPerm("generator:template:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "代码生成模板")
        {
            if (file == null || file.Length == 0)
                return Error("请选择要导入的文件");

            using var stream = file.OpenReadStream();
            var result = await _templateService.ImportTemplatesAsync(stream, sheetName);
            return Success(result);
        }

        /// <summary>
        /// 导出模板数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        [HbtPerm("generator:template:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtGenTemplateQueryDto query, [FromQuery] string sheetName = "代码生成模板")
        {
            var (_, content) = await _templateService.ExportTemplatesAsync(query, sheetName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"代码生成模板_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [HbtPerm("generator:template:query")]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "代码生成模板导入模板")
        {
            var (fileName, content) = await _templateService.GetTemplateAsync(sheetName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        /// <summary>
        /// 更新模板状态
        /// </summary>
        /// <param name="templateId">模板ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{templateId}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HbtPerm("generator:template:update")]
        public async Task<IActionResult> UpdateStatusAsync(long templateId, [FromQuery] int status)
        {
            var input = new HbtGenTemplateStatusDto
            {
                TemplateId = templateId,
                Status = status
            };
            var result = await _templateService.UpdateStatusAsync(input);
            return Success(result);
        }
    }
}