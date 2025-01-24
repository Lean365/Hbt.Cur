//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOnlineUserController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 在线用户控制器
//===================================================================

using Lean.Hbt.Application.Dtos.RealTime;
using Lean.Hbt.Application.Services.RealTime;
using Lean.Hbt.Domain.IServices.Admin;

namespace Lean.Hbt.WebApi.Controllers.RealTime
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
    [ApiModule("realtime", "实时通讯")]
    public class HbtOnlineUserController : HbtBaseController
    {
        private readonly IHbtOnlineUserService _onlineUserService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="onlineUserService">在线用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtOnlineUserController(IHbtOnlineUserService onlineUserService, IHbtLocalizationService localization) : base(localization)
        {
            _onlineUserService = onlineUserService;
        }

        /// <summary>
        /// 获取在线用户分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>在线用户分页列表</returns>
        [HttpGet]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] HbtOnlineUserQueryDto query)
        {
            var result = await _onlineUserService.GetPagedListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 导出在线用户数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
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
        [HttpDelete("{connectionId}")]
        public async Task<IActionResult> ForceOfflineAsync(string connectionId)
        {
            var result = await _onlineUserService.DeleteOnlineUserAsync(connectionId);
            return Success(result);
        }

        /// <summary>
        /// 清理过期用户
        /// </summary>
        /// <param name="minutes">超时时间(分钟)</param>
        /// <returns>清理数量</returns>
        [HttpPost("cleanup")]
        public async Task<IActionResult> CleanupExpiredUsersAsync([FromQuery] int minutes = 20)
        {
            var result = await _onlineUserService.CleanupExpiredUsersAsync(minutes);
            return Success(result);
        }
    }
}