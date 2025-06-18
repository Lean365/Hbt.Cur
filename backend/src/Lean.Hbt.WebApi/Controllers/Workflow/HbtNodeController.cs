//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtNodeController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流节点控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Application.Services.Workflow;

namespace Lean.Hbt.WebApi.Controllers.Workflow
{
    /// <summary>
    /// 工作流节点控制器
    /// </summary>
    [Route("api/[controller]", Name = "工作流节点")]
    [ApiController]
    [ApiModule("workflow", "工作流")]
    public class HbtNodeController : HbtBaseController
    {
        private readonly IHbtNodeService _workflowNodeService;

        /// <summary>
        /// 构造函数
        /// <param name="workflowNodeService">工作流节点服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="currentTenant">当前租户服务</param>
        /// <param name="localization">本地化服务</param>
        /// </summary>
        public HbtNodeController(
            IHbtNodeService workflowNodeService,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtCurrentTenant currentTenant,
            IHbtLocalizationService localization) : base(logger, currentUser, currentTenant, localization)
        {
            _workflowNodeService = workflowNodeService;
        }

        /// <summary>
        /// 获取工作流节点分页列表
        /// </summary>
        [HttpGet("list")]
        [HbtPerm("workflow:node:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtNodeQueryDto query)
        {
            var result = await _workflowNodeService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取工作流节点详情
        /// </summary>
        [HttpGet("{id}")]
        [HbtPerm("workflow:node:query")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _workflowNodeService.GetByIdAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 创建工作流节点
        /// </summary>
        [HttpPost]
        [HbtPerm("workflow:node:create")]
        public async Task<IActionResult> CreateAsync([FromBody] HbtNodeCreateDto input)
        {
            var result = await _workflowNodeService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新工作流节点
        /// </summary>
        [HttpPut]
        [HbtPerm("workflow:node:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtNodeUpdateDto input)
        {
            var result = await _workflowNodeService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除工作流节点
        /// </summary>
        [HttpDelete("{id}")]
        [HbtPerm("workflow:node:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _workflowNodeService.DeleteAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 批量删除工作流节点
        /// </summary>
        [HttpDelete("batch")]
        [HbtPerm("workflow:node:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _workflowNodeService.BatchDeleteAsync(ids);
            return Success(result);
        }

        /// <summary>
        /// 导入工作流节点数据
        /// </summary>
        [HttpPost("import")]
        [HbtPerm("workflow:node:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _workflowNodeService.ImportAsync(stream, "Sheet1");
            return Success(new { success, fail }, _localization.L("Node.Import.Success"));
        }

        /// <summary>
        /// 导出工作流节点数据
        /// </summary>
        [HttpGet("export")]
        [HbtPerm("workflow:node:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtNodeQueryDto query)
        {
            var result = await _workflowNodeService.ExportAsync(query, "Sheet1");
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
        [HbtPerm("workflow:node:query")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _workflowNodeService.GetTemplateAsync();
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 获取工作流定义的所有节点
        /// </summary>
        [HttpGet("definition/{DefinitionId}")]
        [HbtPerm("workflow:node:query")]
        public async Task<IActionResult> GetNodesByWorkflowDefinitionAsync(long DefinitionId)
        {
            var result = await _workflowNodeService.GetNodesByWorkflowDefinitionAsync(DefinitionId);
            return Success(result);
        }

        /// <summary>
        /// 获取指定节点的子节点列表
        /// </summary>
        [HttpGet("{nodeId}/children")]
        [HbtPerm("workflow:node:query")]
        public async Task<IActionResult> GetChildNodesAsync(long nodeId)
        {
            var result = await _workflowNodeService.GetChildNodesAsync(nodeId);
            return Success(result);
        }

        /// <summary>
        /// 更新节点排序号
        /// </summary>
        [HttpPut("{id}/sort")]
        [HbtPerm("workflow:node:update")]
        public async Task<IActionResult> UpdateSortAsync(long id, [FromQuery] int sort)
        {
            var result = await _workflowNodeService.UpdateSortAsync(id, sort);
            return Success(result);
        }
    }
}