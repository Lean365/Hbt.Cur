//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtWorkflowVariableController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流变量控制器
//===================================================================

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Application.Services.Workflow;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.IServices.Admin;
using Lean.Hbt.Infrastructure.Security.Attributes;
using Lean.Hbt.Infrastructure.Swagger;

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
   
    public class HbtWorkflowVariableController : HbtBaseController
    {
        private readonly IHbtWorkflowVariableService _workflowVariableService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="workflowVariableService">工作流变量服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtWorkflowVariableController(IHbtWorkflowVariableService workflowVariableService, IHbtLocalizationService localization) : base(localization)
        {
            _workflowVariableService = workflowVariableService;
        }

        /// <summary>
        /// 获取工作流变量分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>工作流变量分页列表</returns>
        [HttpGet]
        [HbtPerm("workflow:variable:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtWorkflowVariableQueryDto query)
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
        public async Task<IActionResult> CreateAsync([FromBody] HbtWorkflowVariableCreateDto input)
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
        public async Task<IActionResult> UpdateAsync([FromBody] HbtWorkflowVariableUpdateDto input)
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
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [HbtPerm("workflow:variable:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "Sheet1")
        {
            if (file == null || file.Length == 0)
                return Error("请选择要导入的文件");

            using var stream = file.OpenReadStream();
            var result = await _workflowVariableService.ImportAsync(stream, sheetName);
            return Success(result);
        }

        /// <summary>
        /// 导出工作流变量数据
        /// </summary>
        /// <param name="data">导出数据</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        [HbtPerm("workflow:variable:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] IEnumerable<HbtWorkflowVariableDto> data, [FromQuery] string sheetName = "Sheet1")
        {
            var result = await _workflowVariableService.ExportAsync(data, sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"工作流变量数据_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [HbtPerm("workflow:variable:query")]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "Sheet1")
        {
            var result = await _workflowVariableService.GetTemplateAsync(sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"工作流变量导入模板_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 获取工作流实例的所有变量
        /// </summary>
        /// <param name="workflowInstanceId">工作流实例ID</param>
        /// <returns>变量列表</returns>
        [HttpGet("instance/{workflowInstanceId}")]
        [HbtPerm("workflow:variable:query")]
        public async Task<IActionResult> GetVariablesByWorkflowInstanceAsync(long workflowInstanceId)
        {
            var result = await _workflowVariableService.GetVariablesByWorkflowInstanceAsync(workflowInstanceId);
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
        /// <param name="workflowInstanceId">工作流实例ID</param>
        /// <param name="variableName">变量名称</param>
        /// <returns>变量值</returns>
        [HttpGet("value")]
        [HbtPerm("workflow:variable:query")]
        public async Task<IActionResult> GetVariableValueAsync([FromQuery] long workflowInstanceId, [FromQuery] string variableName)
        {
            var result = await _workflowVariableService.GetVariableValueAsync(workflowInstanceId, variableName);
            return Success(result);
        }

        /// <summary>
        /// 设置工作流变量值
        /// </summary>
        /// <param name="workflowInstanceId">工作流实例ID</param>
        /// <param name="variableName">变量名称</param>
        /// <param name="variableValue">变量值</param>
        /// <returns>是否成功</returns>
        [HttpPut("value")]
        [HbtPerm("workflow:variable:update")]
        public async Task<IActionResult> SetVariableValueAsync([FromQuery] long workflowInstanceId, [FromQuery] string variableName, [FromBody] string variableValue)
        {
            var result = await _workflowVariableService.SetVariableValueAsync(workflowInstanceId, variableName, variableValue);
            return Success(result);
        }
    }
} 
