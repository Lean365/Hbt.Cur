//===================================================================
// 项目名 : Hbt.WebApi
// 文件名 : HbtNoticeController.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 通知控制器
//===================================================================

using Hbt.Application.Dtos.Routine;
using Hbt.Application.Services.Routine;

namespace Hbt.WebApi.Controllers.Routine
{
    /// <summary>
    /// 通知控制器
    /// </summary>
    [Route("api/[controller]", Name = "通知管理")]
    [ApiController]
    [ApiModule("routine", "常规功能")]
    [Authorize]
    public class HbtNoticeController : HbtBaseController
    {
        private readonly IHbtNoticeService _noticeService;
 

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="noticeService">通知服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtNoticeController(
            IHbtNoticeService noticeService,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, currentUser, localization)
        {
            _noticeService = noticeService;

        }

        /// <summary>
        /// 获取通知分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>通知分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("routine:notice:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtNoticeQueryDto query)
        {
            var result = await _noticeService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取通知详情
        /// </summary>
        /// <param name="id">通知ID</param>
        /// <returns>通知详情</returns>
        [HttpGet("{id}")]
        [HbtPerm("routine:notice:query")]
        public async Task<HbtNoticeDto> GetByIdAsync(long id)
        {
            return await _noticeService.GetByIdAsync(id);
        }

        /// <summary>
        /// 创建通知
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>通知ID</returns>
        [HttpPost]
        [HbtPerm("routine:notice:create")]
        public async Task<long> CreateAsync([FromBody] HbtNoticeCreateDto input)
        {
            return await _noticeService.CreateAsync(input);
        }

        /// <summary>
        /// 更新通知
        /// </summary>
        /// <param name="id">通知ID</param>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("{id}")]
        [HbtPerm("routine:notice:update")]
        public async Task<bool> UpdateAsync(long id, [FromBody] HbtNoticeDto input)
        {
            return await _noticeService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除通知
        /// </summary>
        /// <param name="id">通知ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{id}")]
        [HbtPerm("routine:notice:delete")]
        public async Task<bool> DeleteAsync(long id)
        {
            return await _noticeService.DeleteAsync(id);
        }

        /// <summary>
        /// 批量删除通知
        /// </summary>
        /// <param name="ids">通知ID数组</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [HbtPerm("routine:notice:delete")]
        public async Task<bool> BatchDeleteAsync([FromBody] long[] ids)
        {
            return await _noticeService.BatchDeleteAsync(ids);
        }

        /// <summary>
        /// 导出通知数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        [HbtPerm("routine:notice:export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<FileResult> ExportAsync([FromQuery] HbtNoticeQueryDto query)
        {
            var data = await _noticeService.ExportAsync(query);
            return File(data.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", data.fileName);
        }

        /// <summary>
        /// 发布通知
        /// </summary>
        /// <param name="id">通知ID</param>
        /// <returns>是否成功</returns>
        [HttpPost("{id}/publish")]
        [HbtPerm("routine:notice:publish")]
        public async Task<bool> PublishAsync(long id)
        {
            return await _noticeService.PublishAsync(id);
        }

        /// <summary>
        /// 关闭通知
        /// </summary>
        /// <param name="id">通知ID</param>
        /// <returns>是否成功</returns>
        [HttpPost("{id}/close")]
        [HbtPerm("routine:notice:close")]
        public async Task<bool> CloseAsync(long id)
        {
            return await _noticeService.CloseAsync(id);
        }

        /// <summary>
        /// 标记通知已读
        /// </summary>
        /// <param name="id">通知ID</param>
        /// <returns>是否成功</returns>
        [HttpPost("{id}/read")]
        [HbtPerm("routine:notice:read")]
        public async Task<bool> MarkAsReadAsync(long id)
        {
            return await _noticeService.MarkAsReadAsync(id);
        }

        /// <summary>
        /// 标记所有通知已读
        /// </summary>
        [HttpPost("read-all")]
        [HbtPerm("routine:notice:read")]
        public async Task<int> MarkAllAsReadAsync()
        {
            return await _noticeService.MarkAllAsReadAsync(_currentUser.UserId);
        }

        /// <summary>
        /// 标记通知未读
        /// </summary>
        /// <param name="id">通知ID</param>
        /// <returns>是否成功</returns>
        [HttpPost("{id}/unread")]
        [HbtPerm("routine:notice:read")]
        public async Task<bool> MarkAsUnreadAsync(long id)
        {
            return await _noticeService.MarkAsUnreadAsync(id);
        }

        /// <summary>
        /// 标记所有通知未读
        /// </summary>
        [HttpPost("unread-all")]
        [HbtPerm("routine:notice:read")]
        public async Task<int> MarkAllAsUnreadAsync()
        {
            return await _noticeService.MarkAllAsUnreadAsync(_currentUser.UserId);
        }

        /// <summary>
        /// 确认通知
        /// </summary>
        /// <param name="id">通知ID</param>
        /// <returns>是否成功</returns>
        [HttpPost("{id}/confirm")]
        [HbtPerm("routine:notice:confirm")]
        public async Task<bool> ConfirmAsync(long id)
        {
            return await _noticeService.ConfirmAsync(id);
        }
    }
}