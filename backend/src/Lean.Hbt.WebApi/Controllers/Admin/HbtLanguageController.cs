//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLanguageController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 语言控制器
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
    /// 语言控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-22
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class HbtLanguageController : HbtBaseController
    {
        private readonly IHbtLanguageService _languageService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="languageService">语言服务</param>
        public HbtLanguageController(IHbtLanguageService languageService)
        {
            _languageService = languageService;
        }

        /// <summary>
        /// 获取语言分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>语言分页列表</returns>
        [HttpGet]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] HbtLanguageQueryDto query)
        {
            var result = await _languageService.GetPagedListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取语言详情
        /// </summary>
        /// <param name="languageId">语言ID</param>
        /// <returns>语言详情</returns>
        [HttpGet("{languageId}")]
        public async Task<IActionResult> GetAsync(long languageId)
        {
            var result = await _languageService.GetAsync(languageId);
            return Success(result);
        }

        /// <summary>
        /// 创建语言
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>语言ID</returns>
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] HbtLanguageCreateDto input)
        {
            var result = await _languageService.InsertAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新语言
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtLanguageUpdateDto input)
        {
            var result = await _languageService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除语言
        /// </summary>
        /// <param name="languageId">语言ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{languageId}")]
        public async Task<IActionResult> DeleteAsync(long languageId)
        {
            var result = await _languageService.DeleteAsync(languageId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除语言
        /// </summary>
        /// <param name="languageIds">语言ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] languageIds)
        {
            var result = await _languageService.BatchDeleteAsync(languageIds);
            return Success(result);
        }

        /// <summary>
        /// 导入语言数据
        /// </summary>
        /// <param name="languages">语言数据列表</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        public async Task<IActionResult> ImportAsync([FromBody] List<HbtLanguageImportDto> languages)
        {
            var result = await _languageService.ImportAsync(languages);
            return Success(result);
        }

        /// <summary>
        /// 导出语言数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>导出数据列表</returns>
        [HttpGet("export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtLanguageQueryDto query)
        {
            var result = await _languageService.ExportAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <returns>模板数据</returns>
        [HttpGet("template")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _languageService.GetTemplateAsync();
            return Success(result);
        }

        /// <summary>
        /// 更新语言状态
        /// </summary>
        /// <param name="languageId">语言ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{languageId}/status")]
        public async Task<IActionResult> UpdateStatusAsync(long languageId, [FromQuery] HbtStatus status)
        {
            var input = new HbtLanguageStatusDto
            {
                LanguageId = languageId,
                Status = status
            };
            var result = await _languageService.UpdateStatusAsync(input);
            return Success(result);
        }
    }
} 