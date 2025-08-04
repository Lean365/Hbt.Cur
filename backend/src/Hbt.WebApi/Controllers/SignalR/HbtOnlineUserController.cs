//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOnlineUserController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 在线用户控制器
//===================================================================

global using Hbt.Cur.Domain.IServices.Extensions;
using Hbt.Cur.Application.Dtos.SignalR;
using Hbt.Cur.Application.Services.SignalR;

namespace Hbt.Cur.WebApi.Controllers.SignalR
{
    /// <summary>
    /// 在线用户控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    [Route("api/[controller]", Name = "在线用户")]
    [ApiController]
    [ApiModule("signalr", "实时通讯")]
    [Authorize]
    public class HbtOnlineUserController : HbtBaseController
    {
        private readonly IHbtOnlineUserService _onlineUserService;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="onlineUserService">在线用户服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        /// <param name="logger">日志服务</param>
        public HbtOnlineUserController(
            IHbtOnlineUserService onlineUserService,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization,
            IHbtLogger logger) : base(logger, currentUser, localization)
        {
            _onlineUserService = onlineUserService;

        }

        /// <summary>
        /// 获取在线用户分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>在线用户分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("signalr:online:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtOnlineUserQueryDto query)
        {
            // 确保查询对象不为空
            query ??= new HbtOnlineUserQueryDto();

            // 设置默认分页参数
            if (query.PageIndex <= 0) query.PageIndex = 1;
            if (query.PageSize <= 0) query.PageSize = 10;

            var result = await _onlineUserService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 导出在线用户数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        [HbtPerm("signalr:online:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtOnlineUserQueryDto query, [FromQuery] string sheetName = "在线用户信息")
        {
            var result = await _onlineUserService.ExportAsync(query, sheetName);
            return Success(result);
        }

        /// <summary>
        /// 强制用户下线
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>是否成功</returns>
        [HttpPost("force-offline/{connectionId}")]
        [HbtPerm("signalr:online:delete")]
        public async Task<IActionResult> ForceOfflineAsync(string connectionId)
        {
            var result = await _onlineUserService.DeleteOnlineUserAsync(connectionId, _currentUser.UserName);
            return Success(result);
        }

        /// <summary>
        /// 清理过期用户
        /// </summary>
        /// <param name="minutes">超时时间(分钟)</param>
        /// <returns>清理数量</returns>
        [HttpPost("cleanup")]
        [HbtPerm("signalr:online:cleanup")]
        public async Task<IActionResult> CleanupExpiredUsersAsync([FromQuery] int minutes = 20)
        {
            var result = await _onlineUserService.CleanupExpiredUsersAsync(minutes);
            return Success(result);
        }

        /// <summary>
        /// 更新用户心跳
        /// </summary>
        [HttpPost("heartbeat")]
        public async Task<IActionResult> UpdateHeartbeatAsync()
        {
            var count = await _onlineUserService.UpdateHeartbeatAsync();
            if (count > 0)
            {
                return Ok(HbtApiResult.Success($"已更新{count}个用户的心跳"));
            }
            return Ok(HbtApiResult.Success("没有在线用户需要更新"));
        }
    }
}