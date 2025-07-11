//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流控制器
//===================================================================

using Microsoft.AspNetCore.Mvc;
using Lean.Hbt.Application.Services.Workflow;
using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Application.Services.Workflow.Engine;

namespace Lean.Hbt.WebApi.Controllers.Workflow
{
    /// <summary>
    /// 工作流控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [ApiModule("workflow", "工作流")]
    public class HbtWorkflowController : HbtBaseController
    {
        private readonly IHbtInstanceService _instanceService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="instanceService">工作流实例服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtWorkflowController(
            IHbtInstanceService instanceService,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, currentUser, localization)
        {
            _instanceService = instanceService;
        }

        /// <summary>
        /// 启动工作流实例
        /// </summary>
        /// <param name="input">启动参数</param>
        /// <returns>工作流实例ID</returns>
        [HttpPost("start")]
        [HbtPerm("workflow:instance:start")]
        public async Task<IActionResult> StartWorkflowAsync([FromBody] HbtWorkflowStartDto input)
        {
            try
            {
                var instanceId = await _instanceService.StartWorkflowAsync(input);
                return Success(instanceId, "工作流实例启动成功");
            }
            catch (Exception ex)
            {
                _logger.Error($"启动工作流实例失败: {ex.Message}", ex);
                return Error($"启动工作流实例失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 暂停工作流实例
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>操作结果</returns>
        [HttpPost("suspend/{instanceId}")]
        [HbtPerm("workflow:instance:suspend")]
        public async Task<IActionResult> SuspendWorkflowAsync(long instanceId)
        {
            try
            {
                var result = await _instanceService.SuspendWorkflowAsync(instanceId);
                return Success(result, "工作流实例已暂停");
            }
            catch (Exception ex)
            {
                _logger.Error($"暂停工作流实例失败: {ex.Message}", ex);
                return Error($"暂停工作流实例失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 恢复工作流实例
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>操作结果</returns>
        [HttpPost("resume/{instanceId}")]
        [HbtPerm("workflow:instance:resume")]
        public async Task<IActionResult> ResumeWorkflowAsync(long instanceId)
        {
            try
            {
                var result = await _instanceService.ResumeWorkflowAsync(instanceId);
                return Success(result, "工作流实例已恢复");
            }
            catch (Exception ex)
            {
                _logger.Error($"恢复工作流实例失败: {ex.Message}", ex);
                return Error($"恢复工作流实例失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 终止工作流实例
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <param name="reason">终止原因</param>
        /// <returns>操作结果</returns>
        [HttpPost("terminate/{instanceId}")]
        [HbtPerm("workflow:instance:terminate")]
        public async Task<IActionResult> TerminateWorkflowAsync(long instanceId, [FromQuery] string reason = "用户终止")
        {
            try
            {
                var result = await _instanceService.TerminateAsync(instanceId, reason);
                return Success(result, "工作流实例已终止");
            }
            catch (Exception ex)
            {
                _logger.Error($"终止工作流实例失败: {ex.Message}", ex);
                return Error($"终止工作流实例失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取工作流实例状态
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>工作流实例状态</returns>
        [HttpGet("status/{instanceId}")]
        [HbtPerm("workflow:instance:query")]
        public async Task<IActionResult> GetWorkflowStatusAsync(long instanceId)
        {
            try
            {
                var status = await _instanceService.GetWorkflowStatusAsync(instanceId);
                return Success(status, "获取工作流实例状态成功");
            }
            catch (Exception ex)
            {
                _logger.Error($"获取工作流实例状态失败: {ex.Message}", ex);
                return Error($"获取工作流实例状态失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取可用转换
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>可用转换列表</returns>
        [HttpGet("transitions/{instanceId}")]
        [HbtPerm("workflow:instance:query")]
        public async Task<IActionResult> GetAvailableTransitionsAsync(long instanceId)
        {
            try
            {
                var transitions = await _instanceService.GetAvailableTransitionsAsync(instanceId);
                return Success(transitions, "获取可用转换成功");
            }
            catch (Exception ex)
            {
                _logger.Error($"获取可用转换失败: {ex.Message}", ex);
                return Error($"获取可用转换失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 执行工作流转换
        /// </summary>
        /// <param name="input">转换执行参数</param>
        /// <returns>转换结果</returns>
        [HttpPost("execute-transition")]
        [HbtPerm("workflow:instance:execute")]
        public async Task<IActionResult> ExecuteTransitionAsync([FromBody] HbtTransitionExecuteDto input)
        {
            try
            {
                var result = await _instanceService.ExecuteTransitionAsync(input);
                return Success(result, "执行工作流转换成功");
            }
            catch (Exception ex)
            {
                _logger.Error($"执行工作流转换失败: {ex.Message}", ex);
                return Error($"执行工作流转换失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取工作流仪表盘统计数据
        /// </summary>
        [HttpGet("dashboard-stats")]
        [HbtPerm("workflow:dashboard:query")]
        public async Task<IActionResult> GetDashboardStatsAsync()
        {
            try
            {
                var stats = await _instanceService.GetDashboardStatsAsync();
                return Success(stats, "获取仪表盘统计数据成功");
            }
            catch (Exception ex)
            {
                _logger.Error($"获取仪表盘统计数据失败: {ex.Message}", ex);
                return Error($"获取仪表盘统计数据失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取最近活动
        /// </summary>
        [HttpGet("recent-activities")]
        [HbtPerm("workflow:dashboard:query")]
        public async Task<IActionResult> GetRecentActivitiesAsync()
        {
            try
            {
                var list = await _instanceService.GetRecentActivitiesAsync();
                return Success(list, "获取最近活动成功");
            }
            catch (Exception ex)
            {
                _logger.Error($"获取最近活动失败: {ex.Message}", ex);
                return Error($"获取最近活动失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取当前用户的流程实例
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="status">状态筛选(可选)</param>
        /// <returns>用户流程实例列表</returns>
        [HttpGet("user-workflows")]
        [HbtPerm("workflow:instance:query")]
        public async Task<IActionResult> GetUserWorkflowsAsync([FromQuery] long userId, [FromQuery] int? status = null)
        {
            try
            {
                var workflows = await _instanceService.GetUserWorkflowsAsync(userId, status);
                return Success(workflows, "获取用户流程实例成功");
            }
            catch (Exception ex)
            {
                _logger.Error($"获取用户流程实例失败: {ex.Message}", ex);
                return Error($"获取用户流程实例失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取当前用户发起的流程实例
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户发起的流程实例列表</returns>
        [HttpGet("user-initiated")]
        [HbtPerm("workflow:instance:query")]
        public async Task<IActionResult> GetUserInitiatedWorkflowsAsync([FromQuery] long userId)
        {
            try
            {
                var workflows = await _instanceService.GetUserInitiatedWorkflowsAsync(userId);
                return Success(workflows, "获取用户发起的流程实例成功");
            }
            catch (Exception ex)
            {
                _logger.Error($"获取用户发起的流程实例失败: {ex.Message}", ex);
                return Error($"获取用户发起的流程实例失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取当前用户参与的流程实例
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户参与的流程实例列表</returns>
        [HttpGet("user-participated")]
        [HbtPerm("workflow:instance:query")]
        public async Task<IActionResult> GetUserParticipatedWorkflowsAsync([FromQuery] long userId)
        {
            try
            {
                var workflows = await _instanceService.GetUserParticipatedWorkflowsAsync(userId);
                return Success(workflows, "获取用户参与的流程实例成功");
            }
            catch (Exception ex)
            {
                _logger.Error($"获取用户参与的流程实例失败: {ex.Message}", ex);
                return Error($"获取用户参与的流程实例失败: {ex.Message}");
            }
        }
    }
} 