//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtFormController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流表单控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Application.Services.Workflow;

namespace Lean.Hbt.WebApi.Controllers.Workflow
{
    /// <summary>
    /// 工作流表单控制器
    /// </summary>
    [Route("api/[controller]", Name = "工作流表单")]
    [ApiController]
    [ApiModule("workflow", "工作流")]
    public class HbtFormController : HbtBaseController
    {
        private readonly IHbtFormService _workflowFormService;

        /// <summary>
        /// 构造函数
        /// <param name="workflowFormService">工作流表单服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        /// </summary>
        public HbtFormController(
            IHbtFormService workflowFormService,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, currentUser, localization)
        {
            _workflowFormService = workflowFormService;
        }

        /// <summary>
        /// 获取工作流表单分页列表
        /// </summary>
        [HttpGet("list")]
        [HbtPerm("workflow:form:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtFormQueryDto query)
        {
            var result = await _workflowFormService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取工作流表单详情
        /// </summary>
        [HttpGet("{id}")]
        [HbtPerm("workflow:form:query")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _workflowFormService.GetByIdAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 创建工作流表单
        /// </summary>
        [HttpPost]
        [HbtPerm("workflow:form:create")]
        public async Task<IActionResult> CreateAsync([FromBody] HbtFormCreateDto input)
        {
            var result = await _workflowFormService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新工作流表单
        /// </summary>
        [HttpPut]
        [HbtPerm("workflow:form:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtFormUpdateDto input)
        {
            var result = await _workflowFormService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除工作流表单
        /// </summary>
        [HttpDelete("{id}")]
        [HbtPerm("workflow:form:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _workflowFormService.DeleteAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 批量删除工作流表单
        /// </summary>
        [HttpDelete("batch")]
        [HbtPerm("workflow:form:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _workflowFormService.BatchDeleteAsync(ids);
            return Success(result);
        }

        /// <summary>
        /// 导入工作流表单数据
        /// </summary>
        [HttpPost("import")]
        [HbtPerm("workflow:form:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _workflowFormService.ImportAsync(stream, "Sheet1");
            return Success(new { success, fail }, _localization.L("Form.Import.Success"));
        }

        /// <summary>
        /// 导出工作流表单数据
        /// </summary>
        [HttpGet("export")]
        [HbtPerm("workflow:form:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtFormQueryDto query)
        {
            var result = await _workflowFormService.ExportAsync(query, "Sheet1");
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
        [HbtPerm("workflow:form:query")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _workflowFormService.GetTemplateAsync();
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 获取工作流定义的所有表单
        /// </summary>
        [HttpGet("definition/{definitionId}")]
        [HbtPerm("workflow:form:query")]
        public async Task<IActionResult> GetFormsByWorkflowDefinitionAsync(long definitionId)
        {
            var result = await _workflowFormService.GetFormsByWorkflowDefinitionAsync(definitionId);
            return Success(result);
        }

        /// <summary>
        /// 修改表单状态
        /// </summary>
        [HttpPut("{id}/status")]
        [HbtPerm("workflow:form:update")]
        public async Task<IActionResult> ChangeStatusAsync(long id, [FromQuery] int status)
        {
            var result = await _workflowFormService.ChangeStatusAsync(id, status);
            return Success(result);
        }

        /// <summary>
        /// 获取表单选项列表
        /// </summary>
        [HttpGet("options")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOptionsAsync()
        {
            var result = await _workflowFormService.GetOptionsAsync();
            return Success(result);
        }
    }
} 