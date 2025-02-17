//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtWorkflowNodeController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流节点控制器
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
    /// 工作流节点控制器
    /// </summary>
    [Route("api/[controller]", Name = "工作流节点")]
    [ApiController]
    [ApiModule("workflow", "工作流")]
    public class HbtWorkflowNodeController : HbtBaseController
    {
        private readonly IHbtWorkflowNodeService _workflowNodeService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtWorkflowNodeController(
            IHbtWorkflowNodeService workflowNodeService,
            IHbtLocalizationService localization) : base(localization)
        {
            _workflowNodeService = workflowNodeService;
        }

        /// <summary>
        /// 获取工作流节点分页列表
        /// </summary>
        [HttpGet]
        [HbtPerm("workflow:node:list")]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] HbtWorkflowNodeQueryDto query)
        {
            var result = await _workflowNodeService.GetPagedListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取工作流节点详情
        /// </summary>
        [HttpGet("{id}")]
        [HbtPerm("workflow:node:query")]
        public async Task<IActionResult> GetAsync(long id)
        {
            var result = await _workflowNodeService.GetAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 创建工作流节点
        /// </summary>
        [HttpPost]
        [HbtPerm("workflow:node:insert")]
        public async Task<IActionResult> InsertAsync([FromBody] HbtWorkflowNodeCreateDto input)
        {
            var result = await _workflowNodeService.InsertAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新工作流节点
        /// </summary>
        [HttpPut]
        [HbtPerm("workflow:node:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtWorkflowNodeUpdateDto input)
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
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "Sheet1")
        {
            if (file == null || file.Length == 0)
                return Error("请选择要导入的文件");

            using var stream = file.OpenReadStream();
            var result = await _workflowNodeService.ImportAsync(stream, sheetName);
            return Success(result);
        }

        /// <summary>
        /// 导出工作流节点数据
        /// </summary>
        [HttpGet("export")]
        [HbtPerm("workflow:node:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtWorkflowNodeQueryDto query, [FromQuery] string sheetName = "Sheet1")
        {
            var data = await _workflowNodeService.GetPagedListAsync(query);
            var result = await _workflowNodeService.ExportAsync(data.Rows, sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"工作流节点数据_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        [HttpGet("template")]
        [HbtPerm("workflow:node:query")]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "Sheet1")
        {
            var result = await _workflowNodeService.GetTemplateAsync(sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"工作流节点导入模板_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 获取工作流定义的所有节点
        /// </summary>
        [HttpGet("definition/{workflowDefinitionId}")]
        [HbtPerm("workflow:node:query")]
        public async Task<IActionResult> GetNodesByWorkflowDefinitionAsync(long workflowDefinitionId)
        {
            var result = await _workflowNodeService.GetNodesByWorkflowDefinitionAsync(workflowDefinitionId);
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
