//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOfficialDocumentController.cs
// 创建者 : Lean365
// 创建时间: 2024-12-01 10:00
// 版本号 : V0.0.1
// 描述   : 公文文档控制器
//===================================================================

using Hbt.Cur.Application.Dtos.Routine.Document;
using Hbt.Cur.Application.Services.Routine.Document;
using Microsoft.AspNetCore.Mvc;

namespace Hbt.Cur.WebApi.Controllers.Routine.Document
{
    /// <summary>
    /// 公文文档控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024
    /// </remarks>
    [Route("api/[controller]", Name = "公文文档")]
    [ApiController]
    [ApiModule("routine", "公文文档管理")]
    public class HbtOfficialDocumentController : HbtBaseController
    {
        private readonly IHbtOfficialDocumentService _officialDocumentService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="officialDocumentService">公文文档服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtOfficialDocumentController(
            IHbtOfficialDocumentService officialDocumentService,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, currentUser, localization)
        {
            _officialDocumentService = officialDocumentService;
        }

        /// <summary>
        /// 获取公文文档分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>公文文档分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("routine:officialdocument:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtOfficialDocumentQueryDto query)
        {
            var result = await _officialDocumentService.GetListAsync(query);
            return Success(result, _localization.L("OfficialDocument.List.Success"));
        }

        /// <summary>
        /// 获取公文文档详情
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <returns>公文文档详情</returns>
        [HttpGet("{id}")]
        [HbtPerm("routine:officialdocument:query")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _officialDocumentService.GetByIdAsync(id);
            return Success(result, _localization.L("OfficialDocument.Get.Success"));
        }

        /// <summary>
        /// 创建公文文档
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>文档ID</returns>
        [HttpPost]
        [HbtLog("创建公文文档")]
        [HbtPerm("routine:officialdocument:create")]
        public async Task<IActionResult> CreateAsync([FromBody] HbtOfficialDocumentCreateDto input)
        {
            var result = await _officialDocumentService.CreateAsync(input);
            return Success(result, _localization.L("OfficialDocument.Insert.Success"));
        }

        /// <summary>
        /// 更新公文文档
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [HbtLog("更新公文文档")]
        [HbtPerm("routine:officialdocument:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtOfficialDocumentUpdateDto input)
        {
            var result = await _officialDocumentService.UpdateAsync(input);
            return Success(result, _localization.L("OfficialDocument.Update.Success"));
        }

        /// <summary>
        /// 删除公文文档
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{id}")]
        [HbtLog("删除公文文档")]
        [HbtPerm("routine:officialdocument:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _officialDocumentService.DeleteAsync(id);
            return Success(result, _localization.L("OfficialDocument.Delete.Success"));
        }

        /// <summary>
        /// 批量删除公文文档
        /// </summary>
        /// <param name="ids">文档ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [HbtPerm("routine:officialdocument:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _officialDocumentService.BatchDeleteAsync(ids);
            return Success(result, _localization.L("OfficialDocument.BatchDelete.Success"));
        }

        /// <summary>
        /// 获取公文文档树形结构
        /// </summary>
        /// <param name="parentId">父级ID</param>
        /// <returns>树形结构</returns>
        [HttpGet("tree")]
        [HbtPerm("routine:officialdocument:query")]
        public async Task<IActionResult> GetTreeAsync([FromQuery] long? parentId = null)
        {
            var result = await _officialDocumentService.GetTreeAsync(parentId);
            return Success(result, _localization.L("OfficialDocument.Tree.Success"));
        }

        /// <summary>
        /// 导入公文文档数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [HbtPerm("routine:officialdocument:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _officialDocumentService.ImportAsync(stream, "HbtOfficialDocument");
            return Success(new { success, fail }, _localization.L("OfficialDocument.Import.Success"));
        }

        /// <summary>
        /// 导出公文文档数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        [HbtPerm("routine:officialdocument:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtOfficialDocumentQueryDto query)
        {
            var result = await _officialDocumentService.ExportAsync(query, "HbtOfficialDocument");
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
            return File(result.content, contentType, result.fileName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [HbtPerm("routine:officialdocument:export")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _officialDocumentService.GetTemplateAsync("HbtOfficialDocument");
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }
    }
} 