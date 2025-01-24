//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPostController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 岗位控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Application.Services.Identity;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.IServices.Admin;

namespace Lean.Hbt.WebApi.Controllers.Identity
{
    /// <summary>
    /// 岗位控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    [Route("api/[controller]", Name = "岗位")]
    [ApiController]
    [ApiModule("identity", "身份认证")]
    public class HbtPostController : HbtBaseController
    {
        private readonly IHbtPostService _postService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="postService">岗位服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtPostController(IHbtPostService postService, IHbtLocalizationService localization) : base(localization)
        {
            _postService = postService;
        }

        /// <summary>
        /// 获取岗位分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>岗位分页列表</returns>
        [HttpGet]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] HbtPostQueryDto query)
        {
            var result = await _postService.GetPagedListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取岗位详情
        /// </summary>
        /// <param name="postId">岗位ID</param>
        /// <returns>岗位详情</returns>
        [HttpGet("{postId}")]
        public async Task<IActionResult> GetAsync(long postId)
        {
            var result = await _postService.GetAsync(postId);
            return Success(result);
        }

        /// <summary>
        /// 创建岗位
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>岗位ID</returns>
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] HbtPostCreateDto input)
        {
            var result = await _postService.InsertAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新岗位
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtPostUpdateDto input)
        {
            var result = await _postService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除岗位
        /// </summary>
        /// <param name="postId">岗位ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeleteAsync(long postId)
        {
            var result = await _postService.DeleteAsync(postId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除岗位
        /// </summary>
        /// <param name="postIds">岗位ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] postIds)
        {
            var result = await _postService.BatchDeleteAsync(postIds);
            return Success(result);
        }

        /// <summary>
        /// 导入岗位数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        public async Task<IActionResult> ImportAsync([FromForm] IFormFile file, [FromQuery] string sheetName = "Sheet1")
        {
            using var stream = file.OpenReadStream();
            var result = await _postService.ImportAsync(stream, sheetName);
            return Success(result);
        }

        /// <summary>
        /// 导出岗位数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtPostQueryDto query, [FromQuery] string sheetName = "岗位数据")
        {
            var result = await _postService.ExportAsync(query, sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "岗位数据.xlsx");
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入模板Excel文件</returns>
        [HttpGet("template")]
        public async Task<IActionResult> GetImportTemplateAsync([FromQuery] string sheetName = "岗位导入模板")
        {
            var result = await _postService.GenerateTemplateAsync(sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "岗位导入模板.xlsx");
        }

        /// <summary>
        /// 更新岗位状态
        /// </summary>
        /// <param name="postId">岗位ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{postId}/status")]
        public async Task<IActionResult> UpdateStatusAsync(long postId, [FromQuery] HbtStatus status)
        {
            var input = new HbtPostStatusDto
            {
                PostId = postId,
                Status = status
            };
            var result = await _postService.UpdateStatusAsync(input);
            return Success(result);
        }
    }
}