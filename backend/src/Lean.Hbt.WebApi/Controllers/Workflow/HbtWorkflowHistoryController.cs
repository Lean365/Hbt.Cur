//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtWorkflowHistoryController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流历史控制器
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
    /// 工作流历史控制器
    /// </summary>
    [Route("api/[controller]", Name = "工作流历史")]
    [ApiController]
    [ApiModule("workflow", "工作流")]
    public class HbtWorkflowHistoryController : HbtBaseController
    {
        private readonly IHbtWorkflowHistoryService _workflowHistoryService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtWorkflowHistoryController(
            IHbtWorkflowHistoryService workflowHistoryService,
            IHbtLocalizationService localization) : base(localization)
        {
            _workflowHistoryService = workflowHistoryService;
        }

        /// <summary>
        /// 获取工作流历史分页列表
        /// </summary>
        [HttpGet("list")]
        [HbtPerm("workflow:history:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtWorkflowHistoryQueryDto query)
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
        [HbtPerm("workflow:history:insert")]
        public async Task<IActionResult> CreateAsync([FromBody] HbtWorkflowHistoryCreateDto input)
        {
            var result = await _workflowHistoryService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新工作流历史
        /// </summary>
        [HttpPut]
        [HbtPerm("workflow:history:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtWorkflowHistoryUpdateDto input)
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
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "Sheet1")
        {
            if (file == null || file.Length == 0)
                return Error("请选择要导入的文件");

            using var stream = file.OpenReadStream();
            var result = await _workflowHistoryService.ImportAsync(stream, sheetName);
            return Success(result);
        }

        /// <summary>
        /// 导出工作流历史数据
        /// </summary>
        [HttpGet("export")]
        [HbtPerm("workflow:history:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtWorkflowHistoryQueryDto query, [FromQuery] string sheetName = "Sheet1")
        {
            var data = await _workflowHistoryService.GetListAsync(query);
            var result = await _workflowHistoryService.ExportAsync(data.Rows, sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"工作流历史数据_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        [HttpGet("template")]
        [HbtPerm("workflow:history:query")]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "Sheet1")
        {
            var result = await _workflowHistoryService.GetTemplateAsync(sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"工作流历史导入模板_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 获取工作流实例的历史记录
        /// </summary>
        [HttpGet("instance/{workflowInstanceId}")]
        [HbtPerm("workflow:history:query")]
        public async Task<IActionResult> GetHistoriesByWorkflowInstanceAsync(long workflowInstanceId)
        {
            var result = await _workflowHistoryService.GetHistoriesByWorkflowInstanceAsync(workflowInstanceId);
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