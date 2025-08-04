//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtEngineController.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流引擎控制器
//===================================================================

using Hbt.Cur.Application.Dtos.Workflow;
using Hbt.Cur.Application.Services.Workflow.Engine;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hbt.Cur.WebApi.Controllers.Workflow;

/// <summary>
/// 工作流引擎控制器
/// </summary>
[ApiController]
[Route("api/[controller]", Name = "工作流引擎")]
[ApiModule("workflow", "工作流管理")]
[Authorize]
public class HbtEngineController : HbtBaseController
{
    private readonly IHbtWorkflowEngine _workflowEngine;

    public HbtEngineController(IHbtWorkflowEngine workflowEngine,
        IHbtLogger logger,
        IHbtCurrentUser currentUser,
        IHbtLocalizationService localizationService) : base(logger, currentUser, localizationService)
    {
        _workflowEngine = workflowEngine ?? throw new ArgumentNullException(nameof(workflowEngine));
    }

    /// <summary>
    /// 启动工作流实例
    /// </summary>
    /// <param name="dto">启动参数</param>
    /// <returns>工作流实例ID</returns>
    [HttpPost("start")]
    [HbtPerm("workflow:engine:execution:start")]
    public async Task<IActionResult> StartAsync([FromBody] HbtWorkflowStartDto dto)
    {
        try
        {
            if (dto == null)
                return Error("请求参数不能为空");

            var instanceId = await _workflowEngine.StartAsync(dto);
            return Success(instanceId, "工作流启动成功");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 暂停工作流实例
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <param name="reason">暂停原因</param>
    /// <returns>是否成功</returns>
    [HttpPost("{instanceId}/suspend")]
    [HbtPerm("workflow:engine:execution:suspend")]
    public async Task<IActionResult> SuspendAsync(long instanceId, [FromQuery] string reason = "手动暂停")
    {
        try
        {
            await _workflowEngine.SuspendAsync(instanceId, reason);
            return Success(true, "工作流暂停成功");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 恢复工作流实例
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <returns>是否成功</returns>
    [HttpPost("{instanceId}/resume")]
    [HbtPerm("workflow:engine:execution:resume")]
    public async Task<IActionResult> ResumeAsync(long instanceId)
    {
        try
        {
            await _workflowEngine.ResumeAsync(instanceId);
            return Success(true, "工作流恢复成功");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 终止工作流实例
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <param name="reason">终止原因</param>
    /// <returns>是否成功</returns>
    [HttpPost("{instanceId}/terminate")]
    [HbtPerm("workflow:engine:execution:terminate")]
    public async Task<IActionResult> TerminateAsync(long instanceId, [FromQuery] string reason)
    {
        try
        {
            if (string.IsNullOrEmpty(reason))
                return Error("终止原因不能为空");

            await _workflowEngine.TerminateAsync(instanceId, reason);
            return Success(true, "工作流终止成功");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 审批工作流
    /// </summary>
    /// <param name="dto">审批参数</param>
    /// <returns>是否成功</returns>
    [HttpPost("approve")]
    [HbtPerm("workflow:engine:signoff:approve")]
    public async Task<IActionResult> ApproveAsync([FromBody] HbtWorkflowApproveDto dto)
    {
        try
        {
            if (dto == null)
                return Error("请求参数不能为空");

            var result = await _workflowEngine.ApproveAsync(dto);
            if (result)
                return Success(true, "审批操作成功");
            else
                return Error("审批操作失败");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 获取工作流实例状态
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <returns>工作流实例状态</returns>
    [HttpGet("{instanceId}/status")]
    [HbtPerm("workflow:engine:execution:query")]
    public async Task<IActionResult> GetStatusAsync(long instanceId)
    {
        try
        {
            var result = await _workflowEngine.GetStatusAsync(instanceId);
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 获取工作流实例可用转换列表
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <returns>可用转换列表</returns>
    [HttpGet("{instanceId}/transitions")]
    [HbtPerm("workflow:engine:execution:query")]
    public async Task<IActionResult> GetAvailableTransitionsAsync(long instanceId)
    {
        try
        {
            var result = await _workflowEngine.GetAvailableTransitionsAsync(instanceId);
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 获取工作流实例当前节点信息
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <returns>当前节点信息</returns>
    [HttpGet("{instanceId}/current-node")]
    [HbtPerm("workflow:engine:execution:query")]
    public async Task<IActionResult> GetCurrentNodeAsync(long instanceId)
    {
        try
        {
            var result = await _workflowEngine.GetCurrentNodeAsync(instanceId);
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 获取工作流实例变量
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <returns>工作流变量字典</returns>
    [HttpGet("{instanceId}/variables")]
    [HbtPerm("workflow:engine:execution:query")]
    public async Task<IActionResult> GetVariablesAsync(long instanceId)
    {
        try
        {
            var result = await _workflowEngine.GetVariablesAsync(instanceId);
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 设置工作流实例变量
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <param name="variables">变量字典</param>
    /// <returns>是否成功</returns>
    [HttpPut("{instanceId}/variables")]
    [HbtPerm("workflow:engine:execution:update")]
    public async Task<IActionResult> SetVariablesAsync(long instanceId, [FromBody] Dictionary<string, object> variables)
    {
        try
        {
            if (variables == null)
                return Error("变量不能为空");

            await _workflowEngine.SetVariablesAsync(instanceId, variables);
            return Success(true, "变量设置成功");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 获取工作流实例历史记录
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <returns>历史记录列表</returns>
    [HttpGet("{instanceId}/history")]
    [HbtPerm("workflow:engine:execution:query")]
    public async Task<IActionResult> GetHistoryAsync(long instanceId)
    {
        try
        {
            var result = await _workflowEngine.GetHistoryAsync(instanceId);
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 获取工作流实例操作记录
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <returns>操作记录列表</returns>
    [HttpGet("{instanceId}/operations")]
    [HbtPerm("workflow:engine:execution:query")]
    public async Task<IActionResult> GetOperationsAsync(long instanceId)
    {
        try
        {
            var result = await _workflowEngine.GetOperationsAsync(instanceId);
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    // 转换历史相关接口

    /// <summary>
    /// 获取转换历史列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>转换历史列表</returns>
    [HttpGet("transition/list")]
    [HbtPerm("workflow:engine:execution:query")]
    public async Task<IActionResult> GetTransitionListAsync([FromQuery] HbtTransitionQueryDto query)
    {
        try
        {
            var result = await _workflowEngine.GetTransitionListAsync(query);
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 获取转换历史详情
    /// </summary>
    /// <param name="transitionId">转换ID</param>
    /// <returns>转换历史详情</returns>
    [HttpGet("transition/{transitionId}")]
    [HbtPerm("workflow:engine:execution:query")]
    public async Task<IActionResult> GetTransitionAsync(string transitionId)
    {
        try
        {
            var result = await _workflowEngine.GetTransitionAsync(transitionId);
            if (result == null)
                return Error("转换历史不存在");

            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

} 