//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtVariableController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流变量控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Application.Services.Workflow;

namespace Lean.Hbt.WebApi.Controllers.Workflow
{
    /// <summary>
    /// 工作流变量控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    [Route("api/[controller]", Name = "工作流变量")]
    [ApiController]
    [ApiModule("workflow", "工作流")]
    public class HbtVariableController : HbtBaseController
    {
        private readonly IHbtVariableService _workflowVariableService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="workflowVariableService">工作流变量服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="currentTenant">当前租户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtVariableController(
            IHbtVariableService workflowVariableService,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtCurrentTenant currentTenant,
            IHbtLocalizationService localization) : base(logger, currentUser, currentTenant, localization)
        {
            _workflowVariableService = workflowVariableService;
        }

        /// <summary>
        /// 获取工作流变量分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>工作流变量分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("workflow:variable:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtVariableQueryDto query)
        {
            var result = await _workflowVariableService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取工作流变量详情
        /// </summary>
        /// <param name="id">工作流变量ID</param>
        /// <returns>工作流变量详情</returns>
        [HttpGet("{id}")]
        [HbtPerm("workflow:variable:query")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _workflowVariableService.GetByIdAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 创建工作流变量
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>工作流变量ID</returns>
        [HttpPost]
        [HbtPerm("workflow:variable:create")]
        public async Task<IActionResult> CreateAsync([FromBody] HbtVariableCreateDto input)
        {
            var result = await _workflowVariableService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新工作流变量
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [HbtPerm("workflow:variable:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtVariableUpdateDto input)
        {
            var result = await _workflowVariableService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除工作流变量
        /// </summary>
        /// <param name="id">工作流变量ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{id}")]
        [HbtPerm("workflow:variable:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _workflowVariableService.DeleteAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 批量删除工作流变量
        /// </summary>
        /// <param name="ids">工作流变量ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [HbtPerm("workflow:variable:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _workflowVariableService.BatchDeleteAsync(ids);
            return Success(result);
        }

        /// <summary>
        /// 导入工作流变量数据
        /// </summary>
        [HttpPost("import")]
        [HbtPerm("workflow:variable:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _workflowVariableService.ImportAsync(stream, "Sheet1");
            return Success(new { success, fail }, _localization.L("Variable.Import.Success"));
        }

        /// <summary>
        /// 导出工作流变量数据
        /// </summary>
        [HttpGet("export")]
        [HbtPerm("workflow:variable:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtVariableQueryDto query)
        {
            var result = await _workflowVariableService.ExportAsync(query, "Sheet1");
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
        [HbtPerm("workflow:variable:query")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _workflowVariableService.GetTemplateAsync();
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 获取工作流实例的所有变量
        /// </summary>
        /// <param name="InstanceId">工作流实例ID</param>
        /// <returns>变量列表</returns>
        [HttpGet("instance/{InstanceId}")]
        [HbtPerm("workflow:variable:query")]
        public async Task<IActionResult> GetVariablesByWorkflowInstanceAsync(long InstanceId)
        {
            var result = await _workflowVariableService.GetVariablesByWorkflowInstanceAsync(InstanceId);
            return Success(result);
        }

        /// <summary>
        /// 获取工作流节点的所有变量
        /// </summary>
        /// <param name="workflowNodeId">工作流节点ID</param>
        /// <returns>变量列表</returns>
        [HttpGet("node/{workflowNodeId}")]
        [HbtPerm("workflow:variable:query")]
        public async Task<IActionResult> GetVariablesByWorkflowNodeAsync(long workflowNodeId)
        {
            var result = await _workflowVariableService.GetVariablesByWorkflowNodeAsync(workflowNodeId);
            return Success(result);
        }

        /// <summary>
        /// 获取工作流变量值
        /// </summary>
        /// <param name="InstanceId">工作流实例ID</param>
        /// <param name="variableName">变量名称</param>
        /// <returns>变量值</returns>
        [HttpGet("value")]
        [HbtPerm("workflow:variable:query")]
        public async Task<IActionResult> GetVariableValueAsync([FromQuery] long InstanceId, [FromQuery] string variableName)
        {
            var result = await _workflowVariableService.GetVariableValueAsync(InstanceId, variableName);
            return Success(result);
        }

        /// <summary>
        /// 设置工作流变量值
        /// </summary>
        /// <param name="InstanceId">工作流实例ID</param>
        /// <param name="variableName">变量名称</param>
        /// <param name="variableValue">变量值</param>
        /// <returns>是否成功</returns>
        [HttpPut("value")]
        [HbtPerm("workflow:variable:update")]
        public async Task<IActionResult> SetVariableValueAsync([FromQuery] long InstanceId, [FromQuery] string variableName, [FromBody] string variableValue)
        {
            var result = await _workflowVariableService.SetVariableValueAsync(InstanceId, variableName, variableValue);
            return Success(result);
        }
    }
}