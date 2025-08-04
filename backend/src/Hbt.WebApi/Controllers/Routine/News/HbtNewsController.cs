#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtNewsController.cs
// 创建者 : Lean365
// 创建时间:2024-12-01 10:00// 版本号 : V1.0
// 描述   : 新闻控制器
//===================================================================

namespace Hbt.Cur.WebApi.Controllers.Routine.News
{
    /// <summary>
    /// 新闻控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024
    /// </remarks>
    [Route("api/[controller]", Name = "新闻")]
    [ApiController]
    [ApiModule("routine", "新闻管理")]
    public class HbtNewsController : HbtBaseController
    {
        private readonly IHbtNewsService _newsService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="newsService">新闻服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtNewsController(
            IHbtNewsService newsService,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, currentUser, localization)
        {
            _newsService = newsService;
        }

        /// <summary>
        /// 获取新闻分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>新闻分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("routine:news:hot:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtNewsQueryDto query)
        {
            var result = await _newsService.GetListAsync(query);
            return Success(result, _localization.L("News.List.Success"));
        }

        /// <summary>
        /// 获取新闻详情
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns>新闻详情</returns>
        [HttpGet("{id}")]
        [HbtPerm("routine:news:hot:query")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _newsService.GetByIdAsync(id);
            return Success(result, _localization.L("News.Get.Success"));
        }

        /// <summary>
        /// 创建新闻
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>新闻ID</returns>
        [HttpPost]
        [HbtLog("创建新闻")]
        [HbtPerm("routine:news:hot:create")]
        public async Task<IActionResult> CreateAsync([FromBody] HbtNewsCreateDto input)
        {
            var result = await _newsService.CreateAsync(input);
            return Success(result, _localization.L("News.Insert.Success"));
        }

        /// <summary>
        /// 更新新闻
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [HbtLog("更新新闻")]
        [HbtPerm("routine:news:hot:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtNewsUpdateDto input)
        {
            var result = await _newsService.UpdateAsync(input);
            return Success(result, _localization.L("News.Update.Success"));
        }

        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{id}")]
        [HbtLog("删除新闻")]
        [HbtPerm("routine:news:hot:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _newsService.DeleteAsync(id);
            return Success(result, _localization.L("News.Delete.Success"));
        }

        /// <summary>
        /// 批量删除新闻
        /// </summary>
        /// <param name="ids">新闻ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [HbtPerm("routine:news:hot:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _newsService.BatchDeleteAsync(ids);
            return Success(result, _localization.L("News.BatchDelete.Success"));
        }

        /// <summary>
        /// 更新新闻状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("status")]
        [HbtLog("更新新闻状态")]
        [HbtPerm("routine:news:hot:update")]
        public async Task<IActionResult> UpdateStatusAsync([FromBody] HbtNewsStatusDto input)
        {
            var result = await _newsService.UpdateStatusAsync(input);
            return Success(result, _localization.L("News.StatusUpdate.Success"));
        }

        /// <summary>
        /// 获取热门新闻
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>热门新闻列表</returns>
        [HttpGet("hot")]
        [HbtPerm("routine:news:hot:query")]
        public async Task<IActionResult> GetHotNewsAsync([FromQuery] int count = 10)
        {
            var result = await _newsService.GetHotNewsAsync(count);
            return Success(result, _localization.L("News.Hot.Success"));
        }

        /// <summary>
        /// 获取推荐新闻
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>推荐新闻列表</returns>
        [HttpGet("recommended")]
        [HbtPerm("routine:news:hot:query")]
        public async Task<IActionResult> GetRecommendedNewsAsync([FromQuery] int count = 10)
        {
            var result = await _newsService.GetRecommendNewsAsync(count);
            return Success(result, _localization.L("News.Recommended.Success"));
        }

        /// <summary>
        /// 搜索新闻
        /// </summary>
        /// <param name="keyword">搜索关键词</param>
        /// <param name="count">获取数量</param>
        /// <returns>新闻列表</returns>
        [HttpGet("search")]
        [HbtPerm("routine:news:hot:query")]
        public async Task<IActionResult> SearchNewsAsync([FromQuery] string keyword, [FromQuery] int count = 20)
        {
            var result = await _newsService.SearchNewsAsync(keyword, count);
            return Success(result, _localization.L("News.Search.Success"));
        }

        /// <summary>
        /// 导入新闻数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [HbtPerm("routine:news:hot:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _newsService.ImportAsync(stream, "HbtNews");
            return Success(new { success, fail }, _localization.L("News.Import.Success"));
        }

        /// <summary>
        /// 导出新闻数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        [HbtPerm("routine:news:hot:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtNewsQueryDto query)
        {
            var result = await _newsService.ExportAsync(query, "HbtNews");
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
            return File(result.content, contentType, result.fileName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [HbtPerm("routine:news:hot:export")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _newsService.GetTemplateAsync("HbtNews");
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }
    }
}