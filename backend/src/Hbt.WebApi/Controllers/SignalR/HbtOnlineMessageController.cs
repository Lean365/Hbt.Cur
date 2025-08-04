//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOnlineMessageController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 在线消息控制器
//===================================================================

using Hbt.Application.Dtos.SignalR;
using Hbt.Application.Services.SignalR;

namespace Hbt.WebApi.Controllers.SignalR
{
    /// <summary>
    /// 在线消息控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    [Route("api/[controller]", Name = "在线消息")]
    [ApiController]
    [ApiModule("signalr", "在线消息")]
    public class HbtOnlineMessageController : HbtBaseController
    {
        private readonly IHbtOnlineMessageService _onlineMessageService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="onlineMessageService">在线消息服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtOnlineMessageController(
            IHbtOnlineMessageService onlineMessageService,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, currentUser, localization)
        {
            _onlineMessageService = onlineMessageService;
        }

        /// <summary>
        /// 获取在线消息分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>在线消息分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("signalr:message:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtOnlineMessageQueryDto query)
        {
            var result = await _onlineMessageService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取消息详情
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <returns>消息详情</returns>
        [HttpGet("{id}")]
        [HbtPerm("signalr:message:query")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _onlineMessageService.GetMessageAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 导出在线消息数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        [HbtPerm("signalr:message:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtOnlineMessageQueryDto query, [FromQuery] string sheetName = "在线消息信息")
        {
            var result = await _onlineMessageService.ExportAsync(query, sheetName);
            return Success(result);
        }

        /// <summary>
        /// 保存消息
        /// </summary>
        /// <param name="input">消息信息</param>
        /// <returns>消息ID</returns>
        [HttpPost]
        [HbtPerm("signalr:message:create")]
        public async Task<IActionResult> SaveAsync([FromBody] HbtOnlineMessageDto input)
        {
            var result = await _onlineMessageService.SaveMessageAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{id}")]
        [HbtPerm("signalr:message:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _onlineMessageService.DeleteMessageAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 清理过期消息
        /// </summary>
        /// <param name="days">保留天数</param>
        /// <returns>清理数量</returns>
        [HttpPost("cleanup")]
        [HbtPerm("signalr:message:cleanup")]
        public async Task<IActionResult> CleanupExpiredMessagesAsync([FromQuery] int days = 7)
        {
            var result = await _onlineMessageService.CleanupExpiredMessagesAsync(days);
            return Success(result);
        }

        /// <summary>
        /// 标记消息为已读
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <returns>是否成功</returns>
        [HttpPut("{id}/read")]
        [HbtPerm("signalr:message:read")]
        public async Task<IActionResult> MarkAsReadAsync(long id)
        {
            var result = await _onlineMessageService.MarkAsReadAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 标记所有消息为已读
        /// </summary>
        /// <returns>标记的消息数量</returns>
        [HttpPut("read-all")]
        [HbtPerm("signalr:message:read")]
        public async Task<IActionResult> MarkAllAsReadAsync()
        {
            var userIdClaim = User.FindFirst("uid");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            var userId = long.Parse(userIdClaim.Value);

            var result = await _onlineMessageService.MarkAllAsReadAsync(userId);
            return Success(result);
        }

        /// <summary>
        /// 标记消息为未读
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <returns>是否成功</returns>
        [HttpPut("{id}/unread")]
        [HbtPerm("signalr:message:read")]
        public async Task<IActionResult> MarkAsUnreadAsync(long id)
        {
            var result = await _onlineMessageService.MarkAsUnreadAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 标记所有消息为未读
        /// </summary>
        /// <returns>标记的消息数量</returns>
        [HttpPut("unread-all")]
        [HbtPerm("signalr:message:read")]
        public async Task<IActionResult> MarkAllAsUnreadAsync()
        {
            var userIdClaim = User.FindFirst("uid");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            var userId = long.Parse(userIdClaim.Value);

            var result = await _onlineMessageService.MarkAllAsUnreadAsync(userId);
            return Success(result);
        }
    }
}