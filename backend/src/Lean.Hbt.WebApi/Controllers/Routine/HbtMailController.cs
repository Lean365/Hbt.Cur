//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtMailController.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 邮件控制器
//===================================================================

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lean.Hbt.Application.Dtos.Routine;
using Lean.Hbt.Application.Services.Routine;
using Lean.Hbt.Domain.IServices.Admin;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Lean.Hbt.WebApi.Controllers.Routine
{
    /// <summary>
    /// 邮件控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    [Route("api/[controller]", Name = "邮件管理")]
    [ApiController]
    [ApiModule("routine", "常规功能")]
    public class HbtMailController : HbtBaseController
    {
        private readonly IHbtMailService _mailService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mailService">邮件服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtMailController(IHbtMailService mailService, IHbtLocalizationService localization) : base(localization)
        {
            _mailService = mailService;
        }

        /// <summary>
        /// 获取邮件分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>邮件分页列表</returns>
        [HttpGet]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] HbtMailQueryDto query)
        {
            var result = await _mailService.GetPagedListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取邮件详情
        /// </summary>
        /// <param name="mailId">邮件ID</param>
        /// <returns>邮件详情</returns>
        [HttpGet("{mailId}")]
        public async Task<IActionResult> GetAsync(long mailId)
        {
            var result = await _mailService.GetAsync(mailId);
            return Success(result);
        }

        /// <summary>
        /// 创建邮件
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>邮件ID</returns>
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] HbtMailCreateDto input)
        {
            var result = await _mailService.InsertAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新邮件
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtMailUpdateDto input)
        {
            var result = await _mailService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="mailId">邮件ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{mailId}")]
        public async Task<IActionResult> DeleteAsync(long mailId)
        {
            var result = await _mailService.DeleteAsync(mailId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除邮件
        /// </summary>
        /// <param name="mailIds">邮件ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] mailIds)
        {
            var result = await _mailService.BatchDeleteAsync(mailIds);
            return Success(result);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="input">发送邮件参数</param>
        /// <returns>是否成功</returns>
        [HttpPost("send")]
        public async Task<IActionResult> SendAsync([FromBody] HbtMailSendDto input)
        {
            var result = await _mailService.SendAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 批量发送邮件
        /// </summary>
        /// <param name="inputs">发送邮件参数集合</param>
        /// <returns>发送结果</returns>
        [HttpPost("batch-send")]
        public async Task<IActionResult> BatchSendAsync([FromBody] List<HbtMailSendDto> inputs)
        {
            var result = await _mailService.BatchSendAsync(inputs);
            return Success(result);
        }

        /// <summary>
        /// 导出邮件数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtMailQueryDto query, [FromQuery] string sheetName = "邮件信息")
        {
            var result = await _mailService.ExportAsync(query, sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"邮件信息_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }
    }
} 