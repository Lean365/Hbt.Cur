//===================================================================
// 项目名 : Lean.Hbt.WebApi
// 文件名 : HbtQuartzLogController.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 任务日志控制器
//===================================================================

using System.Threading.Tasks;
using Lean.Hbt.Application.Dtos.Audit;
using Lean.Hbt.Application.Services.Audit;
using Lean.Hbt.Domain.IServices.Admin;
using Microsoft.AspNetCore.Mvc;

namespace Lean.Hbt.WebApi.Controllers.Audit
{
    /// <summary>
    /// 任务日志控制器
    /// </summary>
    [Route("api/[controller]", Name = "任务日志")]
    [ApiController]
    [ApiModule("audit", "审计日志")]
    public class HbtQuartzLogController : HbtBaseController
    {
        private readonly IHbtQuartzLogService _quartzLogService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="quartzLogService">任务日志服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtQuartzLogController(IHbtQuartzLogService quartzLogService, IHbtLocalizationService localization) : base(localization)
        {
            _quartzLogService = quartzLogService;
        }

        /// <summary>
        /// 获取任务日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>任务日志分页列表</returns>
        [HttpGet]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] HbtQuartzLogQueryDto query)
        {
            var result = await _quartzLogService.GetPagedAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取任务日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>任务日志详情</returns>
        [HttpGet("{logId}")]
        public async Task<IActionResult> GetAsync(long logId)
        {
            var result = await _quartzLogService.GetAsync(logId);
            return Success(result);
        }

        /// <summary>
        /// 导出任务日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtQuartzLogQueryDto query, [FromQuery] string sheetName = "任务日志数据")
        {
            var result = await _quartzLogService.ExportAsync(query, sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"任务日志_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 清空任务日志
        /// </summary>
        /// <returns>是否成功</returns>
        [HttpDelete("clear")]
        public async Task<IActionResult> ClearAsync()
        {
            var result = await _quartzLogService.ClearAsync();
            return Success(result);
        }
    }
} 