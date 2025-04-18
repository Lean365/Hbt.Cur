//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtAuditLogController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 审计日志控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Audit;
using Lean.Hbt.Application.Services.Audit;
using Lean.Hbt.Domain.IServices.Admin;

namespace Lean.Hbt.WebApi.Controllers.Audit
{
    /// <summary>
    /// 审计日志控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    [Route("api/[controller]", Name = "审计日志")]
    [ApiController]
    [ApiModule("audit", "审计日志")]
    public class HbtAuditLogController : HbtBaseController
    {
        private readonly IHbtAuditsLogService _auditLogService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="auditLogService">审计日志服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtAuditLogController(IHbtAuditsLogService auditLogService, IHbtLocalizationService localization) : base(localization)
        {
            _auditLogService = auditLogService;
        }

        /// <summary>
        /// 获取审计日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>审计日志分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("audit:auditlog:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtAuditLogQueryDto query)
        {
            var result = await _auditLogService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取审计日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>审计日志详情</returns>
        [HttpGet("{logId}")]
        [HbtPerm("audit:auditlog:query")]
        public async Task<IActionResult> GetByIdAsync(long logId)
        {
            var result = await _auditLogService.GetByIdAsync(logId);
            return Success(result);
        }

        /// <summary>
        /// 导出审计日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        [HbtPerm("audit:auditlog:export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtAuditLogQueryDto query, [FromQuery] string sheetName = "审计日志数据")
        {
            var result = await _auditLogService.ExportAsync(query, sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"审计日志_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 清空审计日志
        /// </summary>
        /// <returns>是否成功</returns>
        [HttpDelete("clear")]
        [HbtPerm("audit:auditlog:clear")]
        public async Task<IActionResult> ClearAsync()
        {
            var result = await _auditLogService.ClearAsync();
            return Success(result);
        }
    }
}