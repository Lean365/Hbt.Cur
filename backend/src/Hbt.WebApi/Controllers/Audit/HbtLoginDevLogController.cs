//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginDevLogController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 登录设备日志控制器
//===================================================================

using Hbt.Application.Dtos.Audit;
using Hbt.Application.Services.Audit;

namespace Hbt.WebApi.Controllers.Audit
{
    /// <summary>
    /// 登录设备日志控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-22
    /// </remarks>
    [Route("api/[controller]", Name = "登录设备")]
    [ApiController]
    [ApiModule("identity", "身份认证")]
    public class HbtLoginDevLogController : HbtBaseController
    {
        private readonly IHbtLoginDevLogService _deviceExtendService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="deviceExtendService">登录设备日志服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtLoginDevLogController(
            IHbtLoginDevLogService deviceExtendService,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, currentUser, localization)
        {
            _deviceExtendService = deviceExtendService;
        }

        /// <summary>
        /// 获取登录设备日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页列表</returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtLoginDevLogQueryDto query)
        {
            var result = await _deviceExtendService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 导出登录设备日志
        /// </summary>
        /// <param name="data">要导出的数据</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出数据列表</returns>
        [HttpPost("export")]
        [HbtPerm("audit:logindev:export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportAsync([FromBody] IEnumerable<HbtLoginDevLogDto> data, [FromQuery] string sheetName = "登录设备日志")
        {
            var result = await _deviceExtendService.ExportAsync(data, sheetName);
            var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
                ? "application/zip"
                : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // 只在 filename* 用 UTF-8 编码，filename 用 ASCII
            var safeFileName = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(result.fileName));
            Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
            return File(result.content, contentType, result.fileName);
        }

        /// <summary>
        /// 更新设备信息
        /// </summary>
        /// <param name="request">更新请求</param>
        /// <returns>更新后的信息</returns>
        [HttpPut("device")]
        public async Task<IActionResult> UpdateDeviceInfoAsync([FromBody] HbtLoginDevLogUpdateDto request)
        {
            var result = await _deviceExtendService.UpdateDeviceInfoAsync(request);
            return Success(result);
        }

        /// <summary>
        /// 更新设备离线信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deviceId">设备ID</param>
        /// <returns>更新后的信息</returns>
        [HttpPut("{userId}/{deviceId}/offline")]
        public async Task<IActionResult> UpdateOfflineInfoAsync(long userId, string deviceId)
        {
            var result = await _deviceExtendService.UpdateOfflineInfoAsync(userId, deviceId);
            return Success(result);
        }

        /// <summary>
        /// 更新设备在线时段
        /// </summary>
        /// <param name="request">在线时段更新请求</param>
        /// <returns>更新后的信息</returns>
        [HttpPut("online-period")]
        public async Task<IActionResult> UpdateOnlinePeriodAsync([FromBody] HbtDeviceOnlinePeriodUpdateDto request)
        {
            var result = await _deviceExtendService.UpdateOnlinePeriodAsync(request);
            return Success(result);
        }

        /// <summary>
        /// 获取用户的登录设备日志
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deviceId">设备ID</param>
        /// <returns>登录设备日志</returns>
        [HttpGet("{userId}/{deviceId}")]
        public async Task<IActionResult> GetByUserIdAndDeviceIdAsync(long userId, string deviceId)
        {
            var result = await _deviceExtendService.GetByUserIdAndDeviceIdAsync(userId, deviceId);
            if (result == null)
                return NotFound();
            return Success(result);
        }

        /// <summary>
        /// 获取用户的所有登录设备日志
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>登录设备日志列表</returns>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserIdAsync(long userId)
        {
            var result = await _deviceExtendService.GetByUserIdAsync(userId);
            return Success(result);
        }
    }
}