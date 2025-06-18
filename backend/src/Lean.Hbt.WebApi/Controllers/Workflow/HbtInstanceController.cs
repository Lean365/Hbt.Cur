//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtInstanceController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流实例控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Application.Services.Workflow;

namespace Lean.Hbt.WebApi.Controllers.Workflow
{
    /// <summary>
    /// 工作流实例控制器
    /// </summary>
    [Route("api/[controller]", Name = "工作流实例")]
    [ApiController]
    [ApiModule("workflow", "工作流")]
    public class HbtInstanceController : HbtBaseController
    {
        private readonly IHbtInstanceService _workflowInstanceService;

        /// <summary>
        /// 构造函数
        /// <param name="workflowInstanceService">工作流实例服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="currentTenant">当前租户服务</param>
        /// <param name="localization">本地化服务</param>
        /// <param name="logger">日志服务</param>
        /// </summary>
        public HbtInstanceController(
            IHbtInstanceService workflowInstanceService,
            IHbtCurrentUser currentUser,
            IHbtCurrentTenant currentTenant,
            IHbtLocalizationService localization,
            IHbtLogger logger) : base(logger, currentUser, currentTenant, localization)
        {
            _workflowInstanceService = workflowInstanceService;
        }

        /// <summary>
        /// 获取工作流实例分页列表
        /// </summary>
        [HttpGet("list")]
        [HbtPerm("workflow:instance:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtInstanceQueryDto query)
        {
            var result = await _workflowInstanceService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取工作流实例详情
        /// </summary>
        [HttpGet("{id}")]
        [HbtPerm("workflow:instance:query")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _workflowInstanceService.GetByIdAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 创建工作流实例
        /// </summary>
        [HttpPost]
        [HbtPerm("workflow:instance:create")]
        public async Task<IActionResult> CreateAsync([FromBody] HbtInstanceCreateDto input)
        {
            var result = await _workflowInstanceService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新工作流实例
        /// </summary>
        [HttpPut]
        [HbtPerm("workflow:instance:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtInstanceUpdateDto input)
        {
            var result = await _workflowInstanceService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除工作流实例
        /// </summary>
        [HttpDelete("{id}")]
        [HbtPerm("workflow:instance:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _workflowInstanceService.DeleteAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 批量删除工作流实例
        /// </summary>
        [HttpDelete("batch")]
        [HbtPerm("workflow:instance:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _workflowInstanceService.BatchDeleteAsync(ids);
            return Success(result);
        }

        /// <summary>
        /// 导入工作流实例数据
        /// </summary>
        [HttpPost("import")]
        [HbtPerm("workflow:instance:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _workflowInstanceService.ImportAsync(stream, "Sheet1");
            return Success(new { success, fail }, _localization.L("Instance.Import.Success"));
        }

        /// <summary>
        /// 导出工作流实例数据
        /// </summary>
        [HttpGet("export")]
        [HbtPerm("workflow:instance:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtInstanceQueryDto query)
        {
            var result = await _workflowInstanceService.ExportAsync(query, "Sheet1");
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
        [HbtPerm("workflow:instance:query")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _workflowInstanceService.GetTemplateAsync();
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 更新工作流实例状态
        /// </summary>
        [HttpPut("{id}/status")]
        [HbtPerm("workflow:instance:update")]
        public async Task<IActionResult> UpdateStatusAsync(long id, [FromBody] HbtInstanceStatusDto input)
        {
            input.InstanceId = id;
            var result = await _workflowInstanceService.UpdateStatusAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 提交工作流实例
        /// </summary>
        [HttpPost("{id}/submit")]
        [HbtPerm("workflow:instance:submit")]
        public async Task<IActionResult> SubmitAsync(long id)
        {
            var result = await _workflowInstanceService.SubmitAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 撤回工作流实例
        /// </summary>
        [HttpPost("{id}/withdraw")]
        [HbtPerm("workflow:instance:withdraw")]
        public async Task<IActionResult> WithdrawAsync(long id)
        {
            var result = await _workflowInstanceService.WithdrawAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 终止工作流实例
        /// </summary>
        [HttpPost("{id}/terminate")]
        [HbtPerm("workflow:instance:terminate")]
        public async Task<IActionResult> TerminateAsync(long id, [FromQuery] string reason)
        {
            var result = await _workflowInstanceService.TerminateAsync(id, reason);
            return Success(result);
        }
    }
}