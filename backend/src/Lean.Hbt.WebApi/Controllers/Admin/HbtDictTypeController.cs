//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDictTypeController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 字典类型控制器
//===================================================================

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Application.Dtos.Admin;
using Lean.Hbt.Application.Services.Admin;

namespace Lean.Hbt.WebApi.Controllers.Admin
{
    /// <summary>
    /// 字典类型控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-17
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class HbtDictTypeController : HbtBaseController
    {
        private readonly IHbtDictTypeService _dictTypeService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dictTypeService">字典类型服务</param>
        public HbtDictTypeController(IHbtDictTypeService dictTypeService)
        {
            _dictTypeService = dictTypeService;
        }

        /// <summary>
        /// 获取字典类型分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>字典类型分页列表</returns>
        [HttpGet]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] HbtDictTypeQueryDto query)
        {
            var result = await _dictTypeService.GetPagedListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取字典类型详情
        /// </summary>
        /// <param name="dictTypeId">字典类型ID</param>
        /// <returns>字典类型详情</returns>
        [HttpGet("{dictTypeId}")]
        public async Task<IActionResult> GetAsync(long dictTypeId)
        {
            var result = await _dictTypeService.GetAsync(dictTypeId);
            return Success(result);
        }

        /// <summary>
        /// 创建字典类型
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>字典类型ID</returns>
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] HbtDictTypeCreateDto input)
        {
            var result = await _dictTypeService.InsertAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新字典类型
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtDictTypeUpdateDto input)
        {
            var result = await _dictTypeService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除字典类型
        /// </summary>
        /// <param name="dictTypeId">字典类型ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{dictTypeId}")]
        public async Task<IActionResult> DeleteAsync(long dictTypeId)
        {
            var result = await _dictTypeService.DeleteAsync(dictTypeId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除字典类型
        /// </summary>
        /// <param name="dictTypeIds">字典类型ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] dictTypeIds)
        {
            var result = await _dictTypeService.BatchDeleteAsync(dictTypeIds);
            return Success(result);
        }

        /// <summary>
        /// 导入字典类型数据
        /// </summary>
        /// <param name="dictTypes">字典类型数据列表</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        public async Task<IActionResult> ImportAsync([FromBody] List<HbtDictTypeImportDto> dictTypes)
        {
            var result = await _dictTypeService.ImportAsync(dictTypes);
            return Success(result);
        }

        /// <summary>
        /// 导出字典类型数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>导出数据列表</returns>
        [HttpGet("export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtDictTypeQueryDto query)
        {
            var result = await _dictTypeService.ExportAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <returns>模板数据</returns>
        [HttpGet("template")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _dictTypeService.GetTemplateAsync();
            return Success(result);
        }

        /// <summary>
        /// 更新字典类型状态
        /// </summary>
        /// <param name="dictTypeId">字典类型ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{dictTypeId}/status")]
        public async Task<IActionResult> UpdateStatusAsync(long dictTypeId, [FromQuery] HbtStatus status)
        {
            var input = new HbtDictTypeStatusDto
            {
                DictTypeId = dictTypeId,
                Status = status
            };
            var result = await _dictTypeService.UpdateStatusAsync(input);
            return Success(result);
        }
    }
} 