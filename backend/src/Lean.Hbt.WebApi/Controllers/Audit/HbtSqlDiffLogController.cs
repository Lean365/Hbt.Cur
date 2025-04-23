#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSqlDiffLogController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-19 11:00
// 版本号 : V.0.0.1
// 描述    : 差异日志控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Audit;
using Lean.Hbt.Application.Services.Audit;

namespace Lean.Hbt.WebApi.Controllers.Audit
{
    /// <summary>
    /// 差异日志控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-19
    /// </remarks>
    [Route("api/[controller]", Name = "差异日志")]
    [ApiController]
    [ApiModule("audit", "审计日志")]
    public class HbtSqlDiffLogController : HbtBaseController
    {
        private readonly IHbtSqlDiffLogService _sqlDiffLogService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlDiffLogService">差异日志服务</param>
        /// <param name="localization">本地化服务</param>
        /// <param name="logger">日志服务</param>
        public HbtSqlDiffLogController(IHbtSqlDiffLogService sqlDiffLogService,
                        IHbtLocalizationService localization,
            IHbtLogger logger) : base(localization, logger)
        {
            _sqlDiffLogService = sqlDiffLogService;
        }

        /// <summary>
        /// 获取差异日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>差异日志分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("audit:sqldifflog:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtSqlDiffLogQueryDto query)
        {
            var result = await _sqlDiffLogService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取差异日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>差异日志详情</returns>
        [HttpGet("{logId}")]
        [HbtPerm("audit:sqldifflog:query")]
        public async Task<IActionResult> GetByIdAsync(long logId)
        {
            var result = await _sqlDiffLogService.GetByIdAsync(logId);
            return Success(result);
        }

        /// <summary>
        /// 导出差异日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        [HbtPerm("audit:sqldifflog:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtSqlDiffLogQueryDto query, [FromQuery] string sheetName = "差异日志")
        {
            var (_, content) = await _sqlDiffLogService.ExportAsync(query, sheetName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"差异日志_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 清空差异日志
        /// </summary>
        /// <returns>是否成功</returns>
        [HttpDelete("clear")]
        [HbtPerm("audit:sqldifflog:clear")]
        public async Task<IActionResult> ClearAsync()
        {
            var result = await _sqlDiffLogService.ClearAsync();
            return Success(result);
        }
    }
}