//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOperLogController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 操作日志控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Audit;
using Lean.Hbt.Application.Services.Audit;
using Lean.Hbt.Domain.IServices.Admin;
using Lean.Hbt.Infrastructure.Security.Attributes;

namespace Lean.Hbt.WebApi.Controllers.Audit
{
    /// <summary>
    /// 操作日志控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    [Route("api/[controller]", Name = "操作日志")]
    [ApiController]
    [ApiModule("audit", "审计日志")]

    public class HbtOperLogController : HbtBaseController
    {
        private readonly IHbtOperLogService _operLogService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="operLogService">操作日志服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtOperLogController(IHbtOperLogService operLogService, IHbtLocalizationService localization) : base(localization)
        {
            _operLogService = operLogService;
        }

        /// <summary>
        /// 获取操作日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>操作日志分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("audit:operlog:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtOperLogQueryDto query)
        {
            var result = await _operLogService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取操作日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>操作日志详情</returns>
        [HttpGet("{logId}")]
        [HbtPerm("audit:operlog:query")]
        public async Task<IActionResult> GetByIdAsync(long logId)
        {
            var result = await _operLogService.GetByIdAsync(logId);
            return Success(result);
        }

        /// <summary>
        /// 导出操作日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        [HbtPerm("audit:operlog:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtOperLogQueryDto query, [FromQuery] string sheetName = "操作日志")
        {
            var result = await _operLogService.ExportAsync(query, sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"操作日志_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 清空操作日志
        /// </summary>
        /// <returns>是否成功</returns>
        [HttpDelete("clear")]
        [HbtPerm("audit:operlog:clear")]
        public async Task<IActionResult> ClearAsync()
        {
            var result = await _operLogService.ClearAsync();
            return Success(result);
        }
    }
}