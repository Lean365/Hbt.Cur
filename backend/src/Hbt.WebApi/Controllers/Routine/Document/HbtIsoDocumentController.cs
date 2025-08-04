//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtIsoDocumentController.cs
// 创建者 : Lean365
// 创建时间: 2024-12-01 10:00
// 版本号 : V0.0.1
// 描述   : ISO文档控制器
//===================================================================

using Hbt.Cur.Application.Dtos.Routine.Document.Iso;
using Hbt.Cur.Application.Services.Routine.Document.Regulations;
using Microsoft.AspNetCore.Mvc;

namespace Hbt.Cur.WebApi.Controllers.Routine.Document
{
    /// <summary>
    /// ISO文档控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-12-01
    /// </remarks>
    [Route("api/[controller]", Name = "ISO文档")]
    [ApiController]
    [ApiModule("routine", "ISO文档管理")]
    public class HbtIsoDocumentController : HbtBaseController
    {
        private readonly IHbtIsoDocumentService _isoDocumentService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtIsoDocumentController(
            IHbtIsoDocumentService isoDocumentService,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, currentUser, localization)
        {
            _isoDocumentService = isoDocumentService;
        }

        /// <summary>
        /// 获取ISO文档分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>ISO文档分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("routine:iso:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtIsoDocumentQueryDto query)
        {
            var result = await _isoDocumentService.GetListAsync(query);
            return Success(result, _localization.L("IsoDocument.List.Success"));
        }

        /// <summary>
        /// 获取ISO文档详情
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <returns>ISO文档详情</returns>
        [HttpGet("{id}")]
        [HbtPerm("routine:iso:query")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _isoDocumentService.GetByIdAsync(id);
            return Success(result, _localization.L("IsoDocument.Get.Success"));
        }

        /// <summary>
        /// 创建ISO文档
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>文档ID</returns>
        [HttpPost]
        [HbtLog("创建ISO文档")]
        [HbtPerm("routine:iso:create")]
        public async Task<IActionResult> CreateAsync([FromBody] HbtIsoDocumentCreateDto input)
        {
            var result = await _isoDocumentService.CreateAsync(input);
            return Success(result, _localization.L("IsoDocument.Insert.Success"));
        }

        /// <summary>
        /// 更新ISO文档
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [HbtLog("更新ISO文档")]
        [HbtPerm("routine:iso:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtIsoDocumentUpdateDto input)
        {
            var result = await _isoDocumentService.UpdateAsync(input);
            return Success(result, _localization.L("IsoDocument.Update.Success"));
        }

        /// <summary>
        /// 删除ISO文档
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{id}")]
        [HbtLog("删除ISO文档")]
        [HbtPerm("routine:iso:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _isoDocumentService.DeleteAsync(id);
            return Success(result, _localization.L("IsoDocument.Delete.Success"));
        }

        /// <summary>
        /// 批量删除ISO文档
        /// </summary>
        /// <param name="ids">文档ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [HbtPerm("routine:iso:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _isoDocumentService.BatchDeleteAsync(ids);
            return Success(result, _localization.L("IsoDocument.BatchDelete.Success"));
        }

        /// <summary>
        /// 获取ISO文档树形结构
        /// </summary>
        /// <param name="parentId">父级ID</param>
        /// <returns>树形结构</returns>
        [HttpGet("tree")]
        [HbtPerm("routine:iso:query")]
        public async Task<IActionResult> GetTreeAsync([FromQuery] long? parentId = null)
        {
            var result = await _isoDocumentService.GetTreeAsync(parentId);
            return Success(result, _localization.L("IsoDocument.Tree.Success"));
        }

        /// <summary>
        /// 导入ISO文档数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [HbtPerm("routine:iso:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _isoDocumentService.ImportAsync(stream, "HbtIsoDocument");
            return Success(new { success, fail }, _localization.L("IsoDocument.Import.Success"));
        }

        /// <summary>
        /// 导出ISO文档数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        [HbtPerm("routine:iso:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtIsoDocumentQueryDto query)
        {
            var result = await _isoDocumentService.ExportAsync(query, "HbtIsoDocument");
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
            return File(result.content, contentType, result.fileName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [HbtPerm("routine:iso:export")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _isoDocumentService.GetTemplateAsync("HbtIsoDocument");
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 处理ISO文档请求审批完成后的自动更新
        /// </summary>
        /// <param name="requestId">请求ID</param>
        /// <returns>更新结果</returns>
        [HttpPost("process-request/{requestId}")]
        [HbtPerm("routine:iso:process")]
        public async Task<IActionResult> ProcessRequestCompletionAsync(long requestId)
        {
            var result = await _isoDocumentService.ProcessRequestCompletionAsync(requestId);
            return Success(result, _localization.L("IsoDocument.Process.Success"));
        }
    }
} 