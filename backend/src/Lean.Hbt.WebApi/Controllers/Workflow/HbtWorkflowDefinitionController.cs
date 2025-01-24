//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtWorkflowDefinitionController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流定义控制器
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
        /// </summary>
        public HbtWorkflowDefinitionController(
            IHbtWorkflowDefinitionService workflowDefinitionService,
            IHbtLocalizationService localization) : base(localization)
        {
            _workflowDefinitionService = workflowDefinitionService;
        }

        /// <summary>
        /// 获取工作流定义分页列表
        /// </summary>
        [HttpGet]
        [HbtPermission("workflow:definition:list")]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] HbtWorkflowDefinitionQueryDto query)
        {
            var result = await _workflowDefinitionService.GetPagedListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取工作流定义详情
        /// </summary>
        [HttpGet("{id}")]
        [HbtPermission("workflow:definition:query")]
        public async Task<IActionResult> GetAsync(long id)
        {
            var result = await _workflowDefinitionService.GetAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 创建工作流定义
        /// </summary>
        [HttpPost]
        [HbtPermission("workflow:definition:insert")]
        public async Task<IActionResult> InsertAsync([FromBody] HbtWorkflowDefinitionCreateDto input)
        {
            var result = await _workflowDefinitionService.InsertAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新工作流定义
        /// </summary>
        [HttpPut]
        [HbtPermission("workflow:definition:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtWorkflowDefinitionUpdateDto input)
        {
            var result = await _workflowDefinitionService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除工作流定义
        /// </summary>
        [HttpDelete("{id}")]
        [HbtPermission("workflow:definition:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _workflowDefinitionService.DeleteAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 批量删除工作流定义
        /// </summary>
        [HttpDelete("batch")]
        [HbtPermission("workflow:definition:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _workflowDefinitionService.BatchDeleteAsync(ids);
            return Success(result);
        }

        /// <summary>
        /// 导入工作流定义数据
        /// </summary>
        [HttpPost("import")]
        [HbtPermission("workflow:definition:import")]
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
        [HbtPermission("workflow:definition:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtWorkflowDefinitionQueryDto query, [FromQuery] string sheetName = "Sheet1")
        {
            var data = await _workflowDefinitionService.GetPagedListAsync(query);
            var result = await _workflowDefinitionService.ExportAsync(data.Rows, sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"工作流定义数据_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        [HttpGet("template")]
        [HbtPermission("workflow:definition:query")]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "Sheet1")
        {
            var result = await _workflowDefinitionService.GetTemplateAsync(sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"工作流定义导入模板_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 更新工作流定义状态
        /// </summary>
        [HttpPut("{id}/status")]
        [HbtPermission("workflow:definition:update")]
        public async Task<IActionResult> UpdateStatusAsync(long id, [FromBody] HbtWorkflowDefinitionStatusDto input)
        {
            input.WorkflowDefinitionId = id;
            var result = await _workflowDefinitionService.UpdateStatusAsync(input);
            return Success(result);
        }
    }
} 