//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginExtendController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V1.0.0
// 描述    : 登录扩展信息控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Application.Services.Identity;
using Lean.Hbt.Domain.IServices.Admin;

namespace Lean.Hbt.WebApi.Controllers.Identity
{
    /// <summary>
    /// 登录扩展信息控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-22
    /// </remarks>
    [Route("api/[controller]", Name = "登录扩展")]
    [ApiController]
    [ApiModule("identity", "身份认证")]
    public class HbtLoginExtendController : HbtBaseController
    {
        private readonly IHbtLoginExtendService _loginExtendService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="loginExtendService">登录扩展信息服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtLoginExtendController(IHbtLoginExtendService loginExtendService, IHbtLocalizationService localization) : base(localization)
        {
            _loginExtendService = loginExtendService;
        }

        /// <summary>
        /// 获取登录扩展信息分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页列表</returns>
        [HttpGet]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] HbtLoginExtendQueryDto query)
        {
            var result = await _loginExtendService.GetPagedListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 导出登录扩展信息
        /// </summary>
        /// <param name="data">要导出的数据</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出数据列表</returns>
        [HttpPost("export")]
        public async Task<IActionResult> ExportAsync([FromBody] IEnumerable<HbtLoginExtendDto> data, [FromQuery] string sheetName = "登录扩展信息")
        {
            var result = await _loginExtendService.ExportAsync(data, sheetName);
            return Success(result);
        }

        /// <summary>
        /// 更新用户登录信息
        /// </summary>
        /// <param name="request">更新请求</param>
        /// <returns>更新后的信息</returns>
        [HttpPut("login")]
        public async Task<IActionResult> UpdateLoginInfoAsync([FromBody] HbtLoginExtendUpdateDto request)
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
        public async Task<IActionResult> UpdateOnlinePeriodAsync([FromBody] HbtLoginExtendOnlinePeriodUpdateDto request)
        {
            var result = await _loginExtendService.UpdateOnlinePeriodAsync(request);
            return Success(result);
        }

        /// <summary>
        /// 获取用户登录扩展信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>登录扩展信息</returns>
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