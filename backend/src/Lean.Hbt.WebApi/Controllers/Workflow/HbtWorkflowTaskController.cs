//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtWorkflowTaskController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流任务控制器
//===================================================================

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Application.Services.Workflow;
using Lean.Hbt.Domain.IServices.Admin;
using Lean.Hbt.Infrastructure.Security.Attributes;
using Lean.Hbt.Infrastructure.Swagger;

namespace Lean.Hbt.WebApi.Controllers.Workflow
{
    /// <summary>
    /// 工作流任务控制器
    /// </summary>
    [Route("api/[controller]", Name = "工作流任务")]
    [ApiController]
    [ApiModule("workflow", "工作流")]
    public class HbtWorkflowTaskController : HbtBaseController
    {
        private readonly IHbtWorkflowTaskService _workflowTaskService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtWorkflowTaskController(
            IHbtWorkflowTaskService workflowTaskService,
            IHbtLocalizationService localization) : base(localization)
        {
            _workflowTaskService = workflowTaskService;
        }

        /// <summary>
        /// 获取工作流任务分页列表
        /// </summary>
        [HttpGet]
        [HbtPerm("workflow:task:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtWorkflowTaskQueryDto query)
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
        [HbtPerm("workflow:task:insert")]
        public async Task<IActionResult> CreateAsync([FromBody] HbtWorkflowTaskCreateDto input)
        {
            var result = await _workflowTaskService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新工作流任务
        /// </summary>
        [HttpPut]
        [HbtPerm("workflow:task:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtWorkflowTaskUpdateDto input)
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
        public async Task<IActionResult> ImportAsync([FromBody] List<HbtWorkflowTaskImportDto> tasks)
        {
            var result = await _workflowTaskService.ImportAsync(tasks);
            return Success(result);
        }

        /// <summary>
        /// 导出工作流任务数据
        /// </summary>
        [HttpGet("export")]
        [HbtPerm("workflow:task:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtWorkflowTaskQueryDto query)
        {
            var result = await _workflowTaskService.ExportAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        [HttpGet("template")]
        [HbtPerm("workflow:task:query")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _workflowTaskService.GetTemplateAsync();
            return Success(result);
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
        public async Task<IActionResult> CompleteAsync(long id, [FromQuery] string result, [FromQuery] string comment)
        {
            var success = await _workflowTaskService.CompleteAsync(id, result, comment);
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
    }
} 