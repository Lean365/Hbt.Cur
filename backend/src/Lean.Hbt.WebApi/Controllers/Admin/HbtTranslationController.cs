//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTranslationController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 翻译控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Admin;
using Lean.Hbt.Application.Services.Admin;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.IServices.Admin;

namespace Lean.Hbt.WebApi.Controllers.Admin
{
    /// <summary>
    /// 翻译控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-22
    /// </remarks>
    [Route("api/[controller]", Name = "翻译")]
    [ApiController]
    [ApiModule("admin", "系统管理")]
    public class HbtTranslationController : HbtBaseController
    {
        private readonly IHbtTranslationService _translationService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="translationService">翻译服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtTranslationController(IHbtTranslationService translationService, IHbtLocalizationService localization) : base(localization)
        {
            _translationService = translationService;
        }

        /// <summary>
        /// 获取翻译分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>翻译分页列表</returns>
        [HttpGet]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] HbtTranslationQueryDto query)
        {
            var result = await _translationService.GetPagedListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取翻译详情
        /// </summary>
        /// <param name="translationId">翻译ID</param>
        /// <returns>翻译详情</returns>
        [HttpGet("{translationId}")]
        public async Task<IActionResult> GetAsync(long translationId)
        {
            var result = await _translationService.GetAsync(translationId);
            return Success(result);
        }

        /// <summary>
        /// 创建翻译
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>翻译ID</returns>
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] HbtTranslationCreateDto input)
        {
            var result = await _translationService.InsertAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新翻译
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtTranslationUpdateDto input)
        {
            var result = await _translationService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除翻译
        /// </summary>
        /// <param name="translationId">翻译ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{translationId}")]
        public async Task<IActionResult> DeleteAsync(long translationId)
        {
            var result = await _translationService.DeleteAsync(translationId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除翻译
        /// </summary>
        /// <param name="translationIds">翻译ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] translationIds)
        {
            var result = await _translationService.BatchDeleteAsync(translationIds);
            return Success(result);
        }

        /// <summary>
        /// 导入翻译数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "翻译信息")
        {
            if (file == null || file.Length == 0)
                return Error("请选择要导入的文件");

            using var stream = file.OpenReadStream();
            var result = await _translationService.ImportAsync(stream, sheetName);
            return Success(result);
        }

        /// <summary>
        /// 导出翻译数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtTranslationQueryDto query, [FromQuery] string sheetName = "翻译信息")
        {
            var result = await _translationService.ExportAsync(query, sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"翻译数据_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "翻译信息")
        {
            var result = await _translationService.GetTemplateAsync(sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"翻译导入模板_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 更新翻译状态
        /// </summary>
        /// <param name="translationId">翻译ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{translationId}/status")]
        public async Task<IActionResult> UpdateStatusAsync(long translationId, [FromQuery] HbtStatus status)
        {
            var input = new HbtTranslationStatusDto
            {
                TranslationId = translationId,
                Status = status
            };
            var result = await _translationService.UpdateStatusAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 获取指定语言的翻译值
        /// </summary>
        /// <param name="langCode">语言代码</param>
        /// <param name="transKey">翻译键</param>
        /// <returns>翻译值</returns>
        [HttpGet("value")]
        public async Task<IActionResult> GetTransValueAsync([FromQuery] string langCode, [FromQuery] string transKey)
        {
            var result = await _translationService.GetTransValueAsync(langCode, transKey);
            return Success(result);
        }

        /// <summary>
        /// 获取指定模块的翻译列表
        /// </summary>
        /// <param name="langCode">语言代码</param>
        /// <param name="moduleName">模块名称</param>
        /// <returns>翻译列表</returns>
        [HttpGet("module")]
        public async Task<IActionResult> GetListByModuleAsync([FromQuery] string langCode, [FromQuery] string moduleName)
        {
            var result = await _translationService.GetListByModuleAsync(langCode, moduleName);
            return Success(result);
        }

        /// <summary>
        /// 获取转置后的翻译数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>转置后的翻译数据</returns>
        /// <remarks>
        /// 返回数据结构:
        /// {
        ///   "rows": [
        ///     {
        ///       "transKey": "key1",
        ///       "zh-CN": "中文翻译",
        ///       "en-US": "English Translation"
        ///     },
        ///     {
        ///       "transKey": "key2",
        ///       "zh-CN": "中文翻译2",
        ///       "en-US": "English Translation2"
        ///     }
        ///   ],
        ///   "totalNum": 100,
        ///   "pageIndex": 1,
        ///   "pageSize": 10
        /// }
        /// </remarks>
        [HttpGet("transposed")]
        public async Task<IActionResult> GetTransposedDataAsync([FromQuery] HbtTranslationQueryDto query)
        {
            var result = await _translationService.GetTransposedDataAsync(query);
            return Success(result);
        }
    }
}