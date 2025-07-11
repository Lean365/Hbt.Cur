//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtQuartzController.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 定时任务控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Routine;
using Lean.Hbt.Application.Services.Routine;
using Lean.Hbt.Domain.IServices.Extensions;

namespace Lean.Hbt.WebApi.Controllers.Routine
{
    /// <summary>
    /// 定时任务控制器
    /// </summary>
    [Route("api/[controller]", Name = "定时任务")]
    [ApiController]
    [ApiModule("routine", "日常办公")]
    public class HbtQuartzController : HbtBaseController
    {
        private readonly IHbtQuartzService _quartzTaskService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="quartzTaskService">定时任务服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtQuartzController(
            IHbtQuartzService quartzTaskService,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, currentUser, localization)
        {
            _quartzTaskService = quartzTaskService;
        }

        /// <summary>
        /// 获取定时任务分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>定时任务分页列表</returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtQuartzQueryDto query)
        {
            var result = await _quartzTaskService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取定时任务详情
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <returns>定时任务详情</returns>
        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetByIdAsync(long taskId)
        {
            var result = await _quartzTaskService.GetByIdAsync(taskId);
            return Success(result);
        }

        /// <summary>
        /// 创建定时任务
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>任务ID</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] HbtQuartzCreateDto input)
        {
            var result = await _quartzTaskService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新定时任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="input">更新对象</param>
        /// <returns>操作结果</returns>
        [HttpPut("{taskId}")]
        public async Task<IActionResult> UpdateAsync(long taskId, [FromBody] HbtQuartzDto input)
        {
            var result = await _quartzTaskService.UpdateAsync(taskId, input);
            return Success(result);
        }

        /// <summary>
        /// 删除定时任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <returns>操作结果</returns>
        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteAsync(long taskId)
        {
            var result = await _quartzTaskService.DeleteAsync(taskId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除定时任务
        /// </summary>
        /// <param name="taskIds">任务ID数组</param>
        /// <returns>操作结果</returns>
        [HttpDelete("batch")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] taskIds)
        {
            var result = await _quartzTaskService.BatchDeleteAsync(taskIds);
            return Success(result);
        }

        /// <summary>
        /// 启动定时任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <returns>操作结果</returns>
        [HttpPost("{taskId}/start")]
        public async Task<IActionResult> StartAsync(long taskId)
        {
            var result = await _quartzTaskService.StartAsync(taskId);
            return Success(result);
        }

        /// <summary>
        /// 停止定时任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <returns>操作结果</returns>
        [HttpPost("{taskId}/stop")]
        public async Task<IActionResult> StopAsync(long taskId)
        {
            var result = await _quartzTaskService.StopAsync(taskId);
            return Success(result);
        }

        /// <summary>
        /// 立即执行定时任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <returns>操作结果</returns>
        [HttpPost("{taskId}/execute")]
        public async Task<IActionResult> ExecuteAsync(long taskId)
        {
            var result = await _quartzTaskService.ExecuteAsync(taskId);
            return Success(result);
        }

        /// <summary>
        /// 导出定时任务数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtQuartzQueryDto query)
        {
            var result = await _quartzTaskService.ExportAsync(query);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }
    }
}