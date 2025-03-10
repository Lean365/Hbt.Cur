//===================================================================
// 项目名 : Lean.Hbt.WebApi
// 文件名 : HbtNoticeController.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 通知控制器
//===================================================================

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Application.Dtos.Routine;
using Lean.Hbt.Application.Services.Routine;
using Lean.Hbt.Domain.IServices.Admin;
namespace Lean.Hbt.WebApi.Controllers.Routine
{
    /// <summary>
    /// 通知控制器
    /// </summary>
    [Route("api/routine/[controller]")]
    [ApiController]
    public class HbtNoticeController : HbtBaseController
    {
        private readonly IHbtNoticeService _noticeService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="noticeService">通知服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtNoticeController(
            IHbtNoticeService noticeService,
            IHbtLocalizationService localization) : base(localization)
        {
            _noticeService = noticeService;
        }

        /// <summary>
        /// 获取通知分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>通知分页列表</returns>
        [HttpGet("list")]
        public async Task<HbtPagedResult<HbtNoticeDto>> GetPagedListAsync([FromQuery] HbtNoticeQueryDto query)
        {
            return await _noticeService.GetPagedListAsync(query);
        }

        /// <summary>
        /// 获取通知详情
        /// </summary>
        /// <param name="noticeId">通知ID</param>
        /// <returns>通知详情</returns>
        [HttpGet("{noticeId}")]
        public async Task<HbtNoticeDto> GetAsync(long noticeId)
        {
            return await _noticeService.GetAsync(noticeId);
        }

        /// <summary>
        /// 创建通知
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>通知ID</returns>
        [HttpPost]
        public async Task<long> CreateAsync([FromBody] HbtNoticeCreateDto input)
        {
            return await _noticeService.CreateAsync(input);
        }

        /// <summary>
        /// 更新通知
        /// </summary>
        /// <param name="noticeId">通知ID</param>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("{noticeId}")]
        public async Task<bool> UpdateAsync(long noticeId, [FromBody] HbtNoticeDto input)
        {
            return await _noticeService.UpdateAsync(noticeId, input);
        }

        /// <summary>
        /// 删除通知
        /// </summary>
        /// <param name="noticeId">通知ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{noticeId}")]
        public async Task<bool> DeleteAsync(long noticeId)
        {
            return await _noticeService.DeleteAsync(noticeId);
        }

        /// <summary>
        /// 批量删除通知
        /// </summary>
        /// <param name="noticeIds">通知ID数组</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        public async Task<bool> BatchDeleteAsync([FromBody] long[] noticeIds)
        {
            return await _noticeService.BatchDeleteAsync(noticeIds);
        }

        /// <summary>
        /// 导出通知数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtNoticeQueryDto query)
        {
            var data = await _noticeService.ExportAsync(query);
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "通知数据.xlsx");
        }

        /// <summary>
        /// 发布通知
        /// </summary>
        /// <param name="noticeId">通知ID</param>
        /// <returns>是否成功</returns>
        [HttpPost("{noticeId}/publish")]
        public async Task<bool> PublishAsync(long noticeId)
        {
            return await _noticeService.PublishAsync(noticeId);
        }

        /// <summary>
        /// 关闭通知
        /// </summary>
        /// <param name="noticeId">通知ID</param>
        /// <returns>是否成功</returns>
        [HttpPost("{noticeId}/close")]
        public async Task<bool> CloseAsync(long noticeId)
        {
            return await _noticeService.CloseAsync(noticeId);
        }

        /// <summary>
        /// 标记通知已读
        /// </summary>
        /// <param name="noticeId">通知ID</param>
        /// <returns>是否成功</returns>
        [HttpPost("{noticeId}/read")]
        public async Task<bool> MarkAsReadAsync(long noticeId)
        {
            return await _noticeService.MarkAsReadAsync(noticeId);
        }

        /// <summary>
        /// 确认通知
        /// </summary>
        /// <param name="noticeId">通知ID</param>
        /// <returns>是否成功</returns>
        [HttpPost("{noticeId}/confirm")]
        public async Task<bool> ConfirmAsync(long noticeId)
        {
            return await _noticeService.ConfirmAsync(noticeId);
        }
    }
} 