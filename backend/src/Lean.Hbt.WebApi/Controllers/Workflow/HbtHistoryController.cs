//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtHistoryController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流历史控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Application.Services.Workflow;

namespace Lean.Hbt.WebApi.Controllers.Workflow
{
    /// <summary>
    /// 工作流历史控制器
    /// </summary>
    [Route("api/[controller]", Name = "工作流历史")]
    [ApiController]
    [ApiModule("workflow", "工作流")]
    public class HbtHistoryController : HbtBaseController
    {
        private readonly IHbtHistoryService _workflowHistoryService;

        /// <summary>
        /// 构造函数
        /// <param name="workflowHistoryService">工作流历史服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="currentTenant">当前租户服务</param>
        /// <param name="localization">本地化服务</param>
        /// </summary>
        public HbtHistoryController(
            IHbtHistoryService workflowHistoryService,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtCurrentTenant currentTenant,
            IHbtLocalizationService localization) : base(logger, currentUser, currentTenant, localization)
        {
            _workflowHistoryService = workflowHistoryService;
        }

        /// <summary>
        /// 获取工作流历史分页列表
        /// </summary>
        [HttpGet("list")]
        [HbtPerm("workflow:history:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtHistoryQueryDto query)
        {
            var result = await _workflowHistoryService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取工作流历史详情
        /// </summary>
        [HttpGet("{id}")]
        [HbtPerm("workflow:history:query")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _workflowHistoryService.GetByIdAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 创建工作流历史
        /// </summary>
        [HttpPost]
        [HbtPerm("workflow:history:create")]
        public async Task<IActionResult> CreateAsync([FromBody] HbtHistoryCreateDto input)
        {
            var result = await _workflowHistoryService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新工作流历史
        /// </summary>
        [HttpPut]
        [HbtPerm("workflow:history:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtHistoryUpdateDto input)
        {
            var result = await _workflowHistoryService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除工作流历史
        /// </summary>
        [HttpDelete("{id}")]
        [HbtPerm("workflow:history:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _workflowHistoryService.DeleteAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 批量删除工作流历史
        /// </summary>
        [HttpDelete("batch")]
        [HbtPerm("workflow:history:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _workflowHistoryService.BatchDeleteAsync(ids);
            return Success(result);
        }

        /// <summary>
        /// 导入工作流历史数据
        /// </summary>
        [HttpPost("import")]
        [HbtPerm("workflow:history:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _workflowHistoryService.ImportAsync(stream, "Sheet1");
            return Success(new { success, fail }, _localization.L("History.Import.Success"));
        }

        /// <summary>
        /// 导出工作流历史数据
        /// </summary>
        [HttpGet("export")]
        [HbtPerm("workflow:history:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtHistoryQueryDto query)
        {
            var result = await _workflowHistoryService.ExportAsync(query, "Sheet1");
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
        [HbtPerm("workflow:history:query")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _workflowHistoryService.GetTemplateAsync();
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 获取工作流实例的历史记录
        /// </summary>
        [HttpGet("instance/{InstanceId}")]
        [HbtPerm("workflow:history:query")]
        public async Task<IActionResult> GetHistoriesByWorkflowInstanceAsync(long InstanceId)
        {
            var result = await _workflowHistoryService.GetHistoriesByWorkflowInstanceAsync(InstanceId);
            return Success(result);
        }

        /// <summary>
        /// 获取工作流节点的历史记录
        /// </summary>
        [HttpGet("node/{workflowNodeId}")]
        [HbtPerm("workflow:history:query")]
        public async Task<IActionResult> GetHistoriesByWorkflowNodeAsync(long workflowNodeId)
        {
            var result = await _workflowHistoryService.GetHistoriesByWorkflowNodeAsync(workflowNodeId);
            return Success(result);
        }

        /// <summary>
        /// 获取用户的操作历史记录
        /// </summary>
        [HttpGet("operator/{operatorId}")]
        [HbtPerm("workflow:history:query")]
        public async Task<IActionResult> GetHistoriesByOperatorAsync(long operatorId)
        {
            var result = await _workflowHistoryService.GetHistoriesByOperatorAsync(operatorId);
            return Success(result);
        }

        /// <summary>
        /// 清理历史记录
        /// </summary>
        [HttpPost("cleanup")]
        [HbtPerm("workflow:history:delete")]
        public async Task<IActionResult> CleanupHistoriesAsync([FromQuery] int days)
        {
            var result = await _workflowHistoryService.CleanupHistoriesAsync(days);
            return Success(result);
        }
    }
}