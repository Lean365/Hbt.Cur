//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLawController.cs
// 创建者 : Lean365
// 创建时间:2024-12-01 10:00// 版本号 : V1.0
// 描述   : 法律法规控制器
//===================================================================

using Hbt.Application.Dtos.Routine.Document;
using Hbt.Application.Services.Routine.Document;
using Microsoft.AspNetCore.Mvc;

namespace Hbt.WebApi.Controllers.Routine.Document
{
    /// <summary>
    /// 法律法规控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024
    /// </remarks>
    [Route("api/[controller]", Name = "法律法规")]
    [ApiController]
    [ApiModule("routine", "法律法规管理")]
    public class HbtLawController : HbtBaseController
    {
        private readonly IHbtLawService _lawService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="lawService">法律法规服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtLawController(
            IHbtLawService lawService,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, currentUser, localization)
        {
            _lawService = lawService;
        }

        /// <summary>
        /// 获取法律法规分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>法律法规分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("routine:law:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtLawQueryDto query)
        {
            var result = await _lawService.GetListAsync(query);
            return Success(result, _localization.L("Law.List.Success"));
        }

        /// <summary>
        /// 获取法律法规详情
        /// </summary>
        /// <param name="id">法律法规ID</param>
        /// <returns>法律法规详情</returns>
        [HttpGet("{id}")]
        [HbtPerm("routine:law:query")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _lawService.GetByIdAsync(id);
            return Success(result, _localization.L("Law.Get.Success"));
        }

        /// <summary>
        /// 创建法律法规
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>法律法规ID</returns>
        [HttpPost]
        [HbtLog("创建法律法规")]
        [HbtPerm("routine:law:create")]
        public async Task<IActionResult> CreateAsync([FromBody] HbtLawCreateDto input)
        {
            var result = await _lawService.CreateAsync(input);
            return Success(result, _localization.L("Law.Insert.Success"));
        }

        /// <summary>
        /// 更新法律法规
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [HbtLog("更新法律法规")]
        [HbtPerm("routine:law:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtLawUpdateDto input)
        {
            var result = await _lawService.UpdateAsync(input);
            return Success(result, _localization.L("Law.Update.Success"));
        }

        /// <summary>
        /// 删除法律法规
        /// </summary>
        /// <param name="id">法律法规ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{id}")]
        [HbtLog("删除法律法规")]
        [HbtPerm("routine:law:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _lawService.DeleteAsync(id);
            return Success(result, _localization.L("Law.Delete.Success"));
        }

        /// <summary>
        /// 批量删除法律法规
        /// </summary>
        /// <param name="ids">法律法规ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [HbtPerm("routine:law:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _lawService.BatchDeleteAsync(ids);
            return Success(result, _localization.L("Law.BatchDelete.Success"));
        }

        /// <summary>
        /// 获取法律法规树形结构
        /// </summary>
        /// <param name="parentId">父级ID</param>
        /// <returns>树形结构</returns>
        [HttpGet("tree")]
        [HbtPerm("routine:law:query")]
        public async Task<IActionResult> GetTreeAsync([FromQuery] long? parentId = null)
        {
            var result = await _lawService.GetTreeAsync(parentId);
            return Success(result, _localization.L("Law.Tree.Success"));
        }

        /// <summary>
        /// 导入法律法规数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [HbtPerm("routine:law:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _lawService.ImportAsync(stream, "HbtLaw");
            return Success(new { success, fail }, _localization.L("Law.Import.Success"));
        }

        /// <summary>
        /// 导出法律法规数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        [HbtPerm("routine:law:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtLawQueryDto query)
        {
            var result = await _lawService.ExportAsync(query, "HbtLaw");
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
            return File(result.content, contentType, result.fileName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [HbtPerm("routine:law:export")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _lawService.GetTemplateAsync("HbtLaw");
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }
    }
} 