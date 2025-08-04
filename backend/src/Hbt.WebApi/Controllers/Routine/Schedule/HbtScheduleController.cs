//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtScheduleController.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 日程控制器
//===================================================================

using Hbt.Application.Dtos.Routine;
using Hbt.Application.Services.Routine;

namespace Hbt.WebApi.Controllers.Routine
{
    /// <summary>
    /// 日程控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    [Route("api/[controller]", Name = "日程管理")]
    [ApiController]
    [ApiModule("routine", "日常办公")]
    public class HbtScheduleController : HbtBaseController
    {
        private readonly IHbtScheduleService _scheduleService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="scheduleService">日程服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtScheduleController(
            IHbtScheduleService scheduleService,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, currentUser, localization)
        {
            _scheduleService = scheduleService;
        }

        /// <summary>
        /// 获取日程分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>日程分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("routine:schedule:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtScheduleQueryDto query)
        {
            var result = await _scheduleService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取日程详情
        /// </summary>
        /// <param name="scheduleId">日程ID</param>
        /// <returns>日程详情</returns>
        [HttpGet("{scheduleId}")]
        public async Task<IActionResult> GetByIdAsync(long scheduleId)
        {
            var result = await _scheduleService.GetByIdAsync(scheduleId);
            return Success(result);
        }

        /// <summary>
        /// 创建日程
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>日程ID</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] HbtScheduleCreateDto input)
        {
            var result = await _scheduleService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新日程
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtScheduleUpdateDto input)
        {
            var result = await _scheduleService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除日程
        /// </summary>
        /// <param name="scheduleId">日程ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{scheduleId}")]
        public async Task<IActionResult> DeleteAsync(long scheduleId)
        {
            var result = await _scheduleService.DeleteAsync(scheduleId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除日程
        /// </summary>
        /// <param name="scheduleIds">日程ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] scheduleIds)
        {
            var result = await _scheduleService.BatchDeleteAsync(scheduleIds);
            return Success(result);
        }

        /// <summary>
        /// 导入日程数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "日程信息")
        {
            using var stream = file.OpenReadStream();
            var result = await _scheduleService.ImportAsync(stream, sheetName);
            return Success(result);
        }

        /// <summary>
        /// 导出日程数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtScheduleQueryDto query, [FromQuery] string sheetName = "日程信息")
        {
            var result = await _scheduleService.ExportAsync(query, sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "日程信息")
        {
            var result = await _scheduleService.GetTemplateAsync(sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }
    }
} 