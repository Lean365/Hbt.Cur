//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtExceptionLogController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 异常日志控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Audit;
using Lean.Hbt.Application.Services.Audit;
using Lean.Hbt.Domain.IServices.Admin;

namespace Lean.Hbt.WebApi.Controllers.Audit
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
        /// <param name="localization">本地化服务</param>
        public HbtExceptionLogController(IHbtExceptionLogService exceptionLogService, IHbtLocalizationService localization) : base(localization)
        {
            _exceptionLogService = exceptionLogService;
        }

        /// <summary>
        /// 获取异常日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>异常日志分页列表</returns>
        [HttpGet]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] HbtExceptionLogQueryDto query)
        {
            var result = await _exceptionLogService.GetPagedListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取异常日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>异常日志详情</returns>
        [HttpGet("{logId}")]
        public async Task<IActionResult> GetAsync(long logId)
        {
            var result = await _exceptionLogService.GetAsync(logId);
            return Success(result);
        }

        /// <summary>
        /// 导出异常日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtExceptionLogQueryDto query)
        {
            var result = await _exceptionLogService.ExportAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 清空异常日志
        /// </summary>
        /// <returns>是否成功</returns>
        [HttpDelete("clear")]
        public async Task<IActionResult> ClearAsync()
        {
            var result = await _exceptionLogService.ClearAsync();
            return Success(result);
        }
    }
}