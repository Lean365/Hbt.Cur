//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtExceptionLogController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 异常日志控制器
//===================================================================

using Hbt.Cur.Application.Dtos.Audit;
using Hbt.Cur.Application.Services.Audit;

namespace Hbt.Cur.WebApi.Controllers.Audit
{
    /// <summary>
    /// 异常日志控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    [Route("api/[controller]", Name = "异常日志")]
    [ApiController]
    [ApiModule("audit", "审计日志")]
    public class HbtExceptionLogController : HbtBaseController
    {
        private readonly IHbtExceptionLogService _exceptionLogService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exceptionLogService">异常日志服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtExceptionLogController(
            IHbtExceptionLogService exceptionLogService,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, currentUser, localization)
        {
            _exceptionLogService = exceptionLogService;
        }

        /// <summary>
        /// 获取异常日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>异常日志分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("audit:exceptionlog:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtExceptionLogQueryDto query)
        {
            var result = await _exceptionLogService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取异常日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>异常日志详情</returns>
        [HttpGet("{logId}")]
        [HbtPerm("audit:exceptionlog:query")]
        public async Task<IActionResult> GetByIdAsync(long logId)
        {
            var result = await _exceptionLogService.GetByIdAsync(logId);
            return Success(result);
        }

        /// <summary>
        /// 导出异常日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        [HbtPerm("audit:exceptionlog:export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtExceptionLogQueryDto query, [FromQuery] string sheetName = "异常日志数据")
        {
            var result = await _exceptionLogService.ExportAsync(query, sheetName);
            var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
                ? "application/zip"
                : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // 只在 filename* 用 UTF-8 编码，filename 用 ASCII
            var safeFileName = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(result.fileName));
            Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
            return File(result.content, contentType, result.fileName);
        }

        /// <summary>
        /// 清空异常日志
        /// </summary>
        /// <returns>是否成功</returns>
        [HttpDelete("clear")]
        [HbtPerm("audit:exceptionlog:clear")]
        public async Task<IActionResult> ClearAsync()
        {
            var result = await _exceptionLogService.ClearAsync();
            return Success(result);
        }
    }
}