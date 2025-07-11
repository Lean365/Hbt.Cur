//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDefinitionController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流定义控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Application.Services.Workflow;

namespace Lean.Hbt.WebApi.Controllers.Workflow
{
    /// <summary>
    /// 工作流定义控制器
    /// </summary>
    [Route("api/[controller]", Name = "工作流定义")]
    [ApiController]
    [ApiModule("workflow", "工作流")]
    public class HbtDefinitionController : HbtBaseController
    {
        private readonly IHbtDefinitionService _workflowDefinitionService;

        /// <summary>
        /// 构造函数
        /// <param name="workflowDefinitionService">工作流定义服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        /// </summary>
        public HbtDefinitionController(
            IHbtDefinitionService workflowDefinitionService,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, currentUser, localization)
        {
            _workflowDefinitionService = workflowDefinitionService;
        }

        /// <summary>
        /// 获取工作流定义分页列表
        /// </summary>
        [HttpGet("list")]
        [HbtPerm("workflow:definition:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtDefinitionQueryDto query)
        {
            var result = await _workflowDefinitionService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取工作流定义详情
        /// </summary>
        [HttpGet("{id}")]
        [HbtPerm("workflow:definition:query")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _workflowDefinitionService.GetByIdAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 创建工作流定义
        /// </summary>
        [HttpPost]
        [HbtPerm("workflow:definition:create")]
        public async Task<IActionResult> CreateAsync([FromBody] HbtDefinitionCreateDto input)
        {
            var result = await _workflowDefinitionService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新工作流定义
        /// </summary>
        [HttpPut]
        [HbtPerm("workflow:definition:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtDefinitionUpdateDto input)
        {
            var result = await _workflowDefinitionService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除工作流定义
        /// </summary>
        [HttpDelete("{id}")]
        [HbtPerm("workflow:definition:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _workflowDefinitionService.DeleteAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 批量删除工作流定义
        /// </summary>
        [HttpDelete("batch")]
        [HbtPerm("workflow:definition:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _workflowDefinitionService.BatchDeleteAsync(ids);
            return Success(result);
        }

        /// <summary>
        /// 导入工作流定义数据
        /// </summary>
        [HttpPost("import")]
        [HbtPerm("workflow:definition:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _workflowDefinitionService.ImportAsync(stream, "Sheet1");
            return Success(new { success, fail }, _localization.L("Definition.Import.Success"));
        }

        /// <summary>
        /// 导出工作流定义数据
        /// </summary>
        [HttpGet("export")]
        [HbtPerm("workflow:definition:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtDefinitionQueryDto query)
        {
            var result = await _workflowDefinitionService.ExportAsync(query, "Sheet1");
            var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
                ? "application/zip"
                : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // 只在 filename* 用 UTF-8 编码，filename 用 ASCII
            var safeFileName = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(result.fileName));
            Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
            return File(result.content, contentType, result.fileName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        [HttpGet("template")]
        [HbtPerm("workflow:definition:query")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _workflowDefinitionService.GetTemplateAsync();
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 更新工作流定义状态
        /// </summary>
        [HttpPut("{id}/status")]
        [HbtPerm("workflow:definition:update")]
        public async Task<IActionResult> UpdateStatusAsync(long id, [FromBody] HbtDefinitionStatusDto input)
        {
            input.DefinitionId = id;
            var result = await _workflowDefinitionService.UpdateStatusAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 升级工作流定义版本
        /// </summary>
        /// <param name="id">工作流定义ID</param>
        /// <returns>升级后的版本号</returns>
        [HttpPost("{id}/upgrade-version")]
        [HbtPerm("workflow:definition:upgrade")]
        public async Task<IActionResult> UpgradeVersionAsync(long id)
        {
            var newVersion = await _workflowDefinitionService.UpgradeVersionAsync(id);
            return Ok(new { version = newVersion });
        }

        /// <summary>
        /// 获取工作流定义选项列表
        /// </summary>
        /// <param name="includeDisabled">是否包含已停用的定义</param>
        /// <returns>工作流定义选项列表</returns>
        [HttpGet("options")]
        [HbtPerm("workflow:definition:query")]
        public async Task<IActionResult> GetOptionsAsync([FromQuery] bool includeDisabled = false)
        {
            var result = await _workflowDefinitionService.GetOptionsAsync(includeDisabled);
            return Success(result);
        }
    }
}