//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginEnvLogController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V1.0.0
// 描述    : 登录环境日志信息控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Audit;
using Lean.Hbt.Application.Services.Audit;

namespace Lean.Hbt.WebApi.Controllers.Audit
{
    /// <summary>
    /// 登录环境日志信息控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-22
    /// </remarks>
    [Route("api/[controller]", Name = "登录环境日志")]
    [ApiController]
    [ApiModule("identity", "身份认证")]
    public class HbtLoginEnvLogController : HbtBaseController
    {
        private readonly IHbtLoginEnvLogService _loginExtendService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="loginExtendService">登录环境日志信息服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtLoginEnvLogController(
            IHbtLoginEnvLogService loginExtendService,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, currentUser, localization)
        {
            _loginExtendService = loginExtendService;
        }

        /// <summary>
        /// 获取登录环境日志信息分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页列表</returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtLoginEnvLogQueryDto query)
        {
            var result = await _loginExtendService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 导出登录环境日志信息
        /// </summary>
        /// <param name="data">要导出的数据</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出数据列表</returns>
        [HttpPost("export")]
        [HbtPerm("audit:loginenv:export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportAsync([FromBody] IEnumerable<HbtLoginEnvLogDto> data, [FromQuery] string sheetName = "登录环境日志信息")
        {
            var result = await _loginExtendService.ExportAsync(data, sheetName);
            var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
                ? "application/zip"
                : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // 只在 filename* 用 UTF-8 编码，filename 用 ASCII
            var safeFileName = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(result.fileName));
            Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
            return File(result.content, contentType, result.fileName);
        }

        /// <summary>
        /// 更新用户登录信息
        /// </summary>
        /// <param name="request">更新请求</param>
        /// <returns>更新后的信息</returns>
        [HttpPut("login")]
        public async Task<IActionResult> UpdateLoginInfoAsync([FromBody] HbtLoginEnvLogUpdateDto request)
        {
            var result = await _loginExtendService.UpdateLoginInfoAsync(request);
            return Success(result);
        }

        /// <summary>
        /// 更新用户离线信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>更新后的信息</returns>
        [HttpPut("{userId}/offline")]
        public async Task<IActionResult> UpdateOfflineInfoAsync(long userId)
        {
            var result = await _loginExtendService.UpdateOfflineInfoAsync(userId);
            return Success(result);
        }

        /// <summary>
        /// 更新用户在线时段
        /// </summary>
        /// <param name="request">在线时段更新请求</param>
        /// <returns>更新后的信息</returns>
        [HttpPut("online-period")]
        public async Task<IActionResult> UpdateOnlinePeriodAsync([FromBody] HbtLoginEnvLogOnlinePeriodUpdateDto request)
        {
            var result = await _loginExtendService.UpdateOnlinePeriodAsync(request);
            return Success(result);
        }

        /// <summary>
        /// 获取用户登录环境日志信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>登录环境日志信息</returns>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserIdAsync(long userId)
        {
            var result = await _loginExtendService.GetByUserIdAsync(userId);
            if (result == null)
                return NotFound();
            return Success(result);
        }
    }
}