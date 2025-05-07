//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowDefinitionController.cs
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
    public class HbtWorkflowDefinitionController : HbtBaseController
    {
        private readonly IHbtWorkflowDefinitionService _workflowDefinitionService;

        /// <summary>
        /// 构造函数
        /// <param name="workflowDefinitionService">工作流定义服务</param>
        /// <param name="localization">本地化服务</param>
        /// <param name="logger">日志服务</param>
        /// </summary>
        public HbtWorkflowDefinitionController(
            IHbtWorkflowDefinitionService workflowDefinitionService,
                        IHbtLocalizationService localization,
            IHbtLogger logger) : base(localization, logger)
        {
            _workflowDefinitionService = workflowDefinitionService;
        }

        /// <summary>
        /// 获取工作流定义分页列表
        /// </summary>
        [HttpGet("list")]
        [HbtPerm("workflow:definition:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtWorkflowDefinitionQueryDto query)
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
        public async Task<IActionResult> CreateAsync([FromBody] HbtWorkflowDefinitionCreateDto input)
        {
            var result = await _workflowDefinitionService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新工作流定义
        /// </summary>
        [HttpPut]
        [HbtPerm("workflow:definition:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtWorkflowDefinitionUpdateDto input)
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
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "Sheet1")
        {
            if (file == null || file.Length == 0)
                return Error("请选择要导入的文件");

            using var stream = file.OpenReadStream();
            var result = await _workflowDefinitionService.ImportAsync(stream, sheetName);
            return Success(result);
        }

        /// <summary>
        /// 导出工作流定义数据
        /// </summary>
        [HttpGet("export")]
        [HbtPerm("workflow:definition:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtWorkflowDefinitionQueryDto query, [FromQuery] string sheetName = "Sheet1")
        {
            var data = await _workflowDefinitionService.GetListAsync(query);
            var result = await _workflowDefinitionService.ExportAsync(data.Rows, sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        [HttpGet("template")]
        [HbtPerm("workflow:definition:query")]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "Sheet1")
        {
            var result = await _workflowDefinitionService.GetTemplateAsync(sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 更新工作流定义状态
        /// </summary>
        [HttpPut("{id}/status")]
        [HbtPerm("workflow:definition:update")]
        public async Task<IActionResult> UpdateStatusAsync(long id, [FromBody] HbtWorkflowDefinitionStatusDto input)
        {
            input.WorkflowDefinitionId = id;
            var result = await _workflowDefinitionService.UpdateStatusAsync(input);
            return Success(result);
        }
    }
}