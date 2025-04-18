//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbDiffLogController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 数据库差异日志控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Audit;
using Lean.Hbt.Application.Services.Audit;
using Lean.Hbt.Domain.IServices.Admin;

namespace Lean.Hbt.WebApi.Controllers.Audit
{
    /// <summary>
    /// 数据库差异日志控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    [Route("api/[controller]", Name = "数据库差异日志")]
    [ApiController]
    [ApiModule("audit", "审计日志")]

    public class HbtDbDiffLogController : HbtBaseController
    {
        private readonly IHbtDbDiffLogService _dbDiffLogService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbDiffLogService">数据库差异日志服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtDbDiffLogController(IHbtDbDiffLogService dbDiffLogService, IHbtLocalizationService localization) : base(localization)
        {
            _dbDiffLogService = dbDiffLogService;
        }

        /// <summary>
        /// 获取数据库差异日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>数据库差异日志分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("audit:difflog:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtDbDiffLogQueryDto query)
        {
            var result = await _dbDiffLogService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取数据库差异日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>数据库差异日志详情</returns>
        [HttpGet("{logId}")]
        [HbtPerm("audit:difflog:query")]
        public async Task<IActionResult> GetByIdAsync(long logId)
        {
            var result = await _dbDiffLogService.GetByIdAsync(logId);
            return Success(result);
        }

        /// <summary>
        /// 导出数据库差异日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        [HbtPerm("audit:difflog:export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtDbDiffLogQueryDto query, [FromQuery] string sheetName = "数据库差异日志数据")
        {
            var result = await _dbDiffLogService.ExportAsync(query, sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"数据库差异日志_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 清空数据库差异日志
        /// </summary>
        /// <returns>是否成功</returns>
        [HttpDelete("clear")]
        [HbtPerm("audit:difflog:clear")]
        public async Task<IActionResult> ClearAsync()
        {
            var result = await _dbDiffLogService.ClearAsync();
            return Success(result);
        }
    }
}