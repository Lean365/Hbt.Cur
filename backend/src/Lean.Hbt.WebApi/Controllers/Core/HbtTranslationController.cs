//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTranslationController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 翻译控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Core;
using Lean.Hbt.Application.Services.Core;

namespace Lean.Hbt.WebApi.Controllers.Core
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
        /// <param name="logger">日志服务</param>
        public HbtTranslationController(IHbtTranslationService translationService,
                        IHbtLocalizationService localization,
            IHbtLogger logger) : base(localization, logger)
        {
            _translationService = translationService;
        }

        /// <summary>
        /// 获取翻译分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>翻译分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("admin:language:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtTranslationQueryDto query)
        {
            var result = await _translationService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取翻译详情
        /// </summary>
        /// <param name="transId">翻译ID</param>
        /// <returns>翻译详情</returns>
        [HttpGet("{transId}")]
        [HbtPerm("admin:language:query")]
        public async Task<IActionResult> GetByIdAsync(long transId)
        {
            var result = await _translationService.GetByIdAsync(transId);
            return Success(result);
        }

        /// <summary>
        /// 创建翻译
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>翻译ID</returns>
        [HttpPost]
        [HbtPerm("admin:language:create")]
        public async Task<IActionResult> CreateAsync([FromBody] HbtTranslationCreateDto input)
        {
            var result = await _translationService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新翻译
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [HbtPerm("admin:language:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtTranslationUpdateDto input)
        {
            var result = await _translationService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除翻译
        /// </summary>
        /// <param name="transId">翻译ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{transId}")]
        [HbtPerm("admin:language:delete")]
        public async Task<IActionResult> DeleteAsync(long transId)
        {
            var result = await _translationService.DeleteAsync(transId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除翻译
        /// </summary>
        /// <param name="transIds">翻译ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [HbtPerm("admin:language:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] transIds)
        {
            var result = await _translationService.BatchDeleteAsync(transIds);
            return Success(result);
        }

        /// <summary>
        /// 导入翻译数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [HbtPerm("admin:language:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "翻译数据")
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
        [HbtPerm("admin:language:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtTranslationQueryDto query, [FromQuery] string sheetName = "翻译数据")
        {
            var (_, content) = await _translationService.ExportAsync(query, sheetName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"翻译数据_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [HbtPerm("admin:language:query")]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "翻译数据导入模板")
        {
            var (_, content) = await _translationService.GetTemplateAsync(sheetName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"翻译数据导入模板_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 更新翻译状态
        /// </summary>
        /// <param name="transId">翻译ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{transId}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatusAsync(long transId, [FromQuery] int status)
        {
            var input = new HbtTranslationStatusDto
            {
                TransId = transId,
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