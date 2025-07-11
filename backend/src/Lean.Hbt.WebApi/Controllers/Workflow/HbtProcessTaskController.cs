//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTaskController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流任务控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Application.Services.Workflow;

namespace Lean.Hbt.WebApi.Controllers.Workflow
{
    /// <summary>
    /// 工作流任务控制器
    /// </summary>
    [Route("api/[controller]", Name = "工作流任务")]
    [ApiController]
    [ApiModule("workflow", "工作流")]
    public class HbtProcessTaskController : HbtBaseController
    {
        private readonly IHbtProcessTaskService _workflowTaskService;

        /// <summary>
        /// 构造函数
        /// <param name="workflowTaskService">工作流任务服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        /// <param name="logger">日志服务</param>
        /// </summary>
        public HbtProcessTaskController(
            IHbtProcessTaskService workflowTaskService,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization,
            IHbtLogger logger) : base(logger, currentUser, localization)
        {
            _workflowTaskService = workflowTaskService;
        }

        /// <summary>
        /// 获取工作流任务分页列表
        /// </summary>
        [HttpGet("list")]
        [HbtPerm("workflow:task:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtProcessTaskQueryDto query)
        {
            var result = await _workflowTaskService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取工作流任务详情
        /// </summary>
        [HttpGet("{id}")]
        [HbtPerm("workflow:task:query")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _workflowTaskService.GetByIdAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 创建工作流任务
        /// </summary>
        [HttpPost]
        [HbtPerm("workflow:task:create")]
        public async Task<IActionResult> CreateAsync([FromBody] HbtTaskCreateDto input)
        {
            var result = await _workflowTaskService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新工作流任务
        /// </summary>
        [HttpPut]
        [HbtPerm("workflow:task:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtProcessTaskUpdateDto input)
        {
            var result = await _workflowTaskService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除工作流任务
        /// </summary>
        [HttpDelete("{id}")]
        [HbtPerm("workflow:task:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _workflowTaskService.DeleteAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 批量删除工作流任务
        /// </summary>
        [HttpDelete("batch")]
        [HbtPerm("workflow:task:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _workflowTaskService.BatchDeleteAsync(ids);
            return Success(result);
        }

        /// <summary>
        /// 导入工作流任务数据
        /// </summary>
        [HttpPost("import")]
        [HbtPerm("workflow:task:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _workflowTaskService.ImportAsync(stream, "Sheet1");
            return Success(new { success, fail }, _localization.L("Task.Import.Success"));
        }

        /// <summary>
        /// 导出工作流任务数据
        /// </summary>
        [HttpGet("export")]
        [HbtPerm("workflow:task:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtProcessTaskQueryDto query)
        {
            var result = await _workflowTaskService.ExportAsync(query, "Sheet1");
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
        [HbtPerm("workflow:task:query")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _workflowTaskService.GetTemplateAsync();
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 更新工作流任务状态
        /// </summary>
        [HttpPut("{id}/status")]
        [HbtPerm("workflow:task:update")]
        public async Task<IActionResult> UpdateStatusAsync(long id, [FromQuery] int status)
        {
            var result = await _workflowTaskService.UpdateStatusAsync(id, status);
            return Success(result);
        }

        /// <summary>
        /// 完成工作流任务
        /// </summary>
        [HttpPost("{id}/complete")]
        [HbtPerm("workflow:task:update")]
        public async Task<IActionResult> CompleteAsync(long id, [FromBody] HbtTaskCompleteDto input)
        {
            var success = await _workflowTaskService.CompleteAsync(id, input.Comment);
            return Success(success);
        }

        /// <summary>
        /// 转办工作流任务
        /// </summary>
        [HttpPost("{id}/transfer")]
        [HbtPerm("workflow:task:update")]
        public async Task<IActionResult> TransferAsync(long id, [FromQuery] long assigneeId, [FromQuery] string comment)
        {
            var success = await _workflowTaskService.TransferAsync(id, assigneeId, comment);
            return Success(success);
        }

        /// <summary>
        /// 同意工作流任务
        /// </summary>
        [HttpPost("{id}/approve")]
        [HbtPerm("workflow:task:approve")]
        public async Task<IActionResult> ApproveAsync(long id, [FromQuery] string comment)
        {
            var success = await _workflowTaskService.ApproveTaskAsync(id, comment);
            return Success(success);
        }

        /// <summary>
        /// 退回工作流任务
        /// </summary>
        [HttpPost("{id}/reject")]
        [HbtPerm("workflow:task:update")]
        public async Task<IActionResult> RejectAsync(long id, [FromQuery] string comment)
        {
            var success = await _workflowTaskService.RejectAsync(id, comment);
            return Success(success);
        }

        /// <summary>
        /// 撤销工作流任务
        /// </summary>
        [HttpPost("{id}/cancel")]
        [HbtPerm("workflow:task:update")]
        public async Task<IActionResult> CancelAsync(long id, [FromQuery] string comment)
        {
            var success = await _workflowTaskService.CancelAsync(id, comment);
            return Success(success);
        }

        /// <summary>
        /// 获取用户任务状态统计（基于Status状态统计）
        /// </summary>
        [HttpGet("user-status-stats/{userId}")]
        [HbtPerm("workflow:task:query")]
        public async Task<IActionResult> GetUserTaskStatusStatsAsync(long userId)
        {
            try
            {
                var stats = await _workflowTaskService.GetUserTaskStatusStatsAsync(userId);
                return Success(stats, "获取用户任务状态统计成功");
            }
            catch (Exception ex)
            {
                _logger.Error($"获取用户任务状态统计失败: {ex.Message}", ex);
                return Error($"获取用户任务状态统计失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取用户任务结果统计（基于Result结果统计）
        /// </summary>
        [HttpGet("user-result-stats/{userId}")]
        [HbtPerm("workflow:task:query")]
        public async Task<IActionResult> GetUserTaskResultStatsAsync(long userId)
        {
            try
            {
                var stats = await _workflowTaskService.GetUserTaskResultStatsAsync(userId);
                return Success(stats, "获取用户任务结果统计成功");
            }
            catch (Exception ex)
            {
                _logger.Error($"获取用户任务结果统计失败: {ex.Message}", ex);
                return Error($"获取用户任务结果统计失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取用户待办列表
        /// </summary>
        [HttpGet("user-todos/{userId}")]
        [HbtPerm("workflow:task:query")]
        public async Task<IActionResult> GetUserTodoListAsync(long userId, [FromQuery] int? status = null, [FromQuery] int limit = 5)
        {
            try
            {
                var todos = await _workflowTaskService.GetUserTodoListAsync(userId, status, limit);
                return Success(todos, "获取用户待办列表成功");
            }
            catch (Exception ex)
            {
                _logger.Error($"获取用户待办列表失败: {ex.Message}", ex);
                return Error($"获取用户待办列表失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取用户催办任务列表
        /// </summary>
        [HttpGet("user-urge/{userId}")]
        [HbtPerm("workflow:task:query")]
        public async Task<IActionResult> GetUserUrgeListAsync(long userId, [FromQuery] string urgeType = "overdue", [FromQuery] int limit = 5)
        {
            try
            {
                var urgeList = await _workflowTaskService.GetUserUrgeListAsync(userId, urgeType, limit);
                return Success(urgeList, "获取用户催办任务列表成功");
            }
            catch (Exception ex)
            {
                _logger.Error($"获取用户催办任务列表失败: {ex.Message}", ex);
                return Error($"获取用户催办任务列表失败: {ex.Message}");
            }
        }
    }
}