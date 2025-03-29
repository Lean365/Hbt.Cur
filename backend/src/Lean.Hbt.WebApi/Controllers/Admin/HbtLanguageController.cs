//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLanguageController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 多语言管理控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Admin;
using Lean.Hbt.Application.Services.Admin;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.IServices.Admin;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Infrastructure.Security.Attributes;
using Mapster;
using SqlSugar;

namespace Lean.Hbt.WebApi.Controllers.Admin
{
    /// <summary>
    /// 多语言管理控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-22
    /// 功能说明:
    /// 1. 提供多语言配置的增删改查功能
    /// 2. 支持多语言数据的导入导出
    /// 3. 管理系统支持的语言列表
    /// 4. 设置默认语言和语言状态
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
        /// 构造函数，注入所需服务
        /// </summary>
        /// <param name="languageService">语言服务</param>
        /// <param name="localization">本地化服务</param>
        /// <param name="languageRepository">语言仓储</param>
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
        /// <remarks>
        /// 支持按语言名称、代码、状态等条件筛选
        /// 返回分页后的语言配置列表
        /// </remarks>
        /// <param name="query">查询条件</param>
        /// <returns>语言分页列表</returns>
        [HttpGet]
        [HbtPerm("admin:language:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtLanguageQueryDto query)
        {
            var result = await _languageService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取语言详情
        /// </summary>
        /// <remarks>
        /// 根据语言ID获取单个语言配置的详细信息
        /// </remarks>
        /// <param name="langId">语言ID</param>
        /// <returns>语言详情</returns>
        [HttpGet("{langId}")]
        [HbtPerm("admin:language:query")]
        public async Task<IActionResult> GetByIdAsync(long langId)
        {
            var result = await _languageService.GetByIdAsync(langId);
            return Success(result);
        }

        /// <summary>
        /// 创建语言配置
        /// </summary>
        /// <remarks>
        /// 新增一个语言配置，包括语言代码、名称、排序等信息
        /// </remarks>
        /// <param name="input">创建对象</param>
        /// <returns>新创建的语言ID</returns>
        [HttpPost]
        [HbtPerm("admin:language:create")]
        public async Task<IActionResult> CreateAsync([FromBody] HbtLanguageCreateDto input)
        {
            var result = await _languageService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新语言配置
        /// </summary>
        /// <remarks>
        /// 修改现有语言配置的信息，如名称、排序、状态等
        /// </remarks>
        /// <param name="input">更新对象</param>
        /// <returns>更新结果</returns>
        [HttpPut]
        [HbtPerm("admin:language:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtLanguageUpdateDto input)
        {
            var result = await _languageService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除语言配置
        /// </summary>
        /// <remarks>
        /// 删除指定ID的语言配置
        /// 注意：删除前会检查该语言是否被使用
        /// </remarks>
        /// <param name="langId">语言ID</param>
        /// <returns>删除结果</returns>
        [HttpDelete("{langId}")]
        [HbtPerm("admin:language:delete")]
        public async Task<IActionResult> DeleteAsync(long langId)
        {
            var result = await _languageService.DeleteAsync(langId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除语言配置
        /// </summary>
        /// <remarks>
        /// 批量删除多个语言配置
        /// 注意：会检查所有语言是否可以删除
        /// </remarks>
        /// <param name="langIds">语言ID集合</param>
        /// <returns>批量删除结果</returns>
        [HttpDelete("batch")]
        [HbtPerm("admin:language:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] langIds)
        {
            var result = await _languageService.BatchDeleteAsync(langIds);
            return Success(result);
        }

    /// <summary>
    /// 导入语言数据
    /// </summary>
    /// <remarks>
    /// 从Excel文件导入语言配置数据
    /// 支持批量导入多条语言记录
    /// </remarks>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果，包含成功和失败数量</returns>
    [HttpPost("import")]
    [HbtPerm("admin:language:import")]
                
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "语言数据")
    {
      using var stream = file.OpenReadStream();
      var result = await _languageService.ImportAsync(stream, sheetName);
      return Success(result);
    }

        /// <summary>
        /// 导出语言数据
        /// </summary>
        /// <remarks>
        /// 将语言配置数据导出为Excel文件
        /// 支持按条件筛选要导出的数据
        /// </remarks>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        [HbtPerm("admin:language:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtLanguageQueryDto query, [FromQuery] string sheetName = "语言数据")
        {
            var result = await _languageService.ExportAsync(query, sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"语言数据_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <remarks>
        /// 下载语言数据导入的Excel模板
        /// 模板包含标准的字段格式和示例数据
        /// </remarks>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [HbtPerm("admin:language:query")]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "语言数据导入模板")
        {
            var result = await _languageService.GetTemplateAsync(sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"语言数据导入模板_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 更新语言状态
        /// </summary>
        /// <remarks>
        /// 修改语言的启用/禁用状态
        /// 状态：0-启用，1-禁用
        /// </remarks>
        /// <param name="LangId">语言ID</param>
        /// <param name="status">状态值</param>
        /// <returns>更新结果</returns>
        [HttpPut("{LangId}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HbtPerm("admin:language:update")]
        public async Task<IActionResult> UpdateStatusAsync(long LangId, [FromQuery] int status)
        {
            var input = new HbtLanguageStatusDto
            {
                LangId = LangId,
                Status = status
            };
            var result = await _languageService.UpdateStatusAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 获取系统支持的语言列表
        /// </summary>
        /// <remarks>
        /// 获取所有已启用的语言列表
        /// 如果没有配置则返回默认的中文和英文
        /// </remarks>
        /// <returns>语言列表</returns>
        [HttpGet("supported")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSupportedLanguagesAsync()
        {
            var result = await GetLanguagesAsync();
            return Success(result);
        }

    /// <summary>
    /// 获取所有语言列表
    /// </summary>
    /// <remarks>
    /// 获取系统中所有正常状态的语言配置
    /// 用于下拉选择等场景
    /// </remarks>
    [AllowAnonymous]
    private async Task<List<HbtLanguageDto>> GetLanguagesAsync()
    {
      var exp = Expressionable.Create<HbtLanguage>();
      exp.And(x => x.Status == 0); // 0 表示正常状态
      var languages = await _languageRepository.GetListAsync(exp.ToExpression());
      return languages.Adapt<List<HbtLanguageDto>>();
    }
    }
}