//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtRegulationController.cs
// 创建者 : Lean365
// 创建时间: 2024-12-01 10:00
// 版本号 : V0.0.1
// 描述   : 规章制度控制器
//===================================================================

using Hbt.Cur.Application.Dtos.Routine.Document;
using Hbt.Cur.Application.Services.Routine.Document.Regulations;
using Microsoft.AspNetCore.Mvc;

namespace Hbt.Cur.WebApi.Controllers.Routine.Document
{
    /// <summary>
    /// 规章制度控制器
    /// </summary>
    [Route("api/[controller]", Name = "规章制度")]
    [ApiController]
    [ApiModule("routine", "规章制度管理")]
    public class HbtRegulationController : HbtBaseController
    {
        private readonly IHbtRegulationService _regulationService;

        public HbtRegulationController(
            IHbtRegulationService regulationService,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, currentUser, localization)
        {
            _regulationService = regulationService;
        }

        [HttpGet("list")]
        [HbtPerm("routine:regulation:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtRegulationQueryDto query)
        {
            var result = await _regulationService.GetListAsync(query);
            return Success(result, _localization.L("Regulation.List.Success"));
        }

        [HttpGet("{id}")]
        [HbtPerm("routine:regulation:query")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _regulationService.GetByIdAsync(id);
            return Success(result, _localization.L("Regulation.Get.Success"));
        }

        [HttpPost]
        [HbtLog("创建规章制度")]
        [HbtPerm("routine:regulation:create")]
        public async Task<IActionResult> CreateAsync([FromBody] HbtRegulationCreateDto input)
        {
            var result = await _regulationService.CreateAsync(input);
            return Success(result, _localization.L("Regulation.Insert.Success"));
        }

        [HttpPut]
        [HbtLog("更新规章制度")]
        [HbtPerm("routine:regulation:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtRegulationUpdateDto input)
        {
            var result = await _regulationService.UpdateAsync(input);
            return Success(result, _localization.L("Regulation.Update.Success"));
        }

        [HttpDelete("{id}")]
        [HbtLog("删除规章制度")]
        [HbtPerm("routine:regulation:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _regulationService.DeleteAsync(id);
            return Success(result, _localization.L("Regulation.Delete.Success"));
        }

        [HttpDelete("batch")]
        [HbtPerm("routine:regulation:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _regulationService.BatchDeleteAsync(ids);
            return Success(result, _localization.L("Regulation.BatchDelete.Success"));
        }

        [HttpGet("tree")]
        [HbtPerm("routine:regulation:query")]
        public async Task<IActionResult> GetTreeAsync([FromQuery] long? parentId = null)
        {
            var result = await _regulationService.GetTreeAsync(parentId);
            return Success(result, _localization.L("Regulation.Tree.Success"));
        }

        [HttpPost("import")]
        [HbtPerm("routine:regulation:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _regulationService.ImportAsync(stream, "HbtRegulation");
            return Success(new { success, fail }, _localization.L("Regulation.Import.Success"));
        }

        [HttpGet("export")]
        [HbtPerm("routine:regulation:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtRegulationQueryDto query)
        {
            var result = await _regulationService.ExportAsync(query, "HbtRegulation");
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
            return File(result.content, contentType, result.fileName);
        }

        [HttpGet("template")]
        [HbtPerm("routine:regulation:export")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _regulationService.GetTemplateAsync("HbtRegulation");
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }
    }
}