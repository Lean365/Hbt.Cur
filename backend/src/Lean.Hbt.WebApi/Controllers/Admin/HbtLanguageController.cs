//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLanguageController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 语言控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Admin;
using Lean.Hbt.Application.Services.Admin;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.IServices.Admin;
using Lean.Hbt.Domain.Repositories;
using Mapster;
using SqlSugar;

namespace Lean.Hbt.WebApi.Controllers.Admin
{
    /// <summary>
    /// 语言控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-22
    /// </remarks>
    [Route("api/[controller]", Name = "语言")]
    [ApiController]
    [ApiModule("admin", "系统管理")]
    public class HbtLanguageController : HbtBaseController
    {
        private readonly IHbtLanguageService _languageService;
        private readonly IHbtRepository<HbtLanguage> _languageRepository;
        private readonly ILogger<HbtLanguageController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="languageService">语言服务</param>
        /// <param name="localization">本地化服务</param>
        /// <param name="languageRepository">语言仓库</param>
        /// <param name="logger">日志服务</param>
        public HbtLanguageController(
            IHbtLanguageService languageService,
            IHbtLocalizationService localization,
            IHbtRepository<HbtLanguage> languageRepository,
            ILogger<HbtLanguageController> logger) : base(localization)
        {
            _languageService = languageService;
            _languageRepository = languageRepository;
            _logger = logger;
        }

        /// <summary>
        /// 获取语言分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>语言分页列表</returns>
        [HttpGet]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] HbtLanguageQueryDto query)
        {
            var result = await _languageService.GetPagedListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取语言详情
        /// </summary>
        /// <param name="languageId">语言ID</param>
        /// <returns>语言详情</returns>
        [HttpGet("{languageId}")]
        public async Task<IActionResult> GetAsync(long languageId)
        {
            var result = await _languageService.GetAsync(languageId);
            return Success(result);
        }

        /// <summary>
        /// 创建语言
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>语言ID</returns>
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] HbtLanguageCreateDto input)
        {
            var result = await _languageService.InsertAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新语言
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtLanguageUpdateDto input)
        {
            var result = await _languageService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除语言
        /// </summary>
        /// <param name="languageId">语言ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{languageId}")]
        public async Task<IActionResult> DeleteAsync(long languageId)
        {
            var result = await _languageService.DeleteAsync(languageId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除语言
        /// </summary>
        /// <param name="languageIds">语言ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] languageIds)
        {
            var result = await _languageService.BatchDeleteAsync(languageIds);
            return Success(result);
        }

        /// <summary>
        /// 导入语言数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [ProducesResponseType(typeof((int Success, int Fail)), StatusCodes.Status200OK)]
        public async Task<IActionResult> ImportAsync([FromForm] IFormFile file, [FromQuery] string sheetName = "语言信息")
        {
            if (file == null || file.Length == 0)
                return BadRequest("请选择要导入的文件");

            using var stream = file.OpenReadStream();
            var result = await _languageService.ImportAsync(stream, sheetName);
            return Ok(result);
        }

        /// <summary>
        /// 导出语言数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtLanguageQueryDto query, [FromQuery] string sheetName = "语言信息")
        {
            var result = await _languageService.ExportAsync(query, sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"语言信息_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "语言信息")
        {
            var result = await _languageService.GetTemplateAsync(sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"语言信息导入模板_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 更新语言状态
        /// </summary>
        /// <param name="languageId">语言ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{languageId}/status")]
        public async Task<IActionResult> UpdateStatusAsync(long languageId, [FromQuery] HbtStatus status)
        {
            var input = new HbtLanguageStatusDto
            {
                LanguageId = languageId,
                Status = status
            };
            var result = await _languageService.UpdateStatusAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 获取支持的语言列表
        /// </summary>
        /// <returns>语言列表</returns>
        [HttpGet("supported")]
        public async Task<IActionResult> GetSupportedLanguagesAsync()
        {
            try
            {
                var exp = Expressionable.Create<HbtLanguage>();
                exp.And(x => x.Status == HbtStatus.Normal);
                exp.And(x => x.IsDeleted == 0);

                var list = await _languageRepository.GetListAsync(exp.ToExpression());

                if (list == null || !list.Any())
                {
                    // 如果没有数据，返回默认语言列表
                    list = new List<HbtLanguage>
                    {
                        new HbtLanguage
                        {
                            Id = 1,
                            LangCode = "zh-CN",
                            LangName = "简体中文",
                            OrderNum = 1,
                            Status = HbtStatus.Normal,
                            IsDefault = true,
                            CreateBy = "system",
                            CreateTime = DateTime.Now,
                            IsDeleted = 0
                        },
                        new HbtLanguage
                        {
                            Id = 2,
                            LangCode = "en-US",
                            LangName = "English",
                            OrderNum = 2,
                            Status = HbtStatus.Normal,
                            IsDefault = false,
                            CreateBy = "system",
                            CreateTime = DateTime.Now,
                            IsDeleted = 0
                        }
                    };
                }

                var result = list.OrderBy(x => x.OrderNum).Adapt<List<HbtLanguageDto>>();
                return Ok(new { code = 200, msg = "操作成功", data = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取支持的语言列表时发生错误");
                return Ok(new { code = 500, msg = "获取语言列表失败：" + ex.Message });
            }
        }
    }
}