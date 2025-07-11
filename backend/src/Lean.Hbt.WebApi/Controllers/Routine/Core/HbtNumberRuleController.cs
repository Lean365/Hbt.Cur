//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtNumberRuleController.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 单号规则控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Routine.Core;
using Lean.Hbt.Application.Services.Core;

namespace Lean.Hbt.WebApi.Controllers.Routine.Core
{
    /// <summary>
    /// 单号规则控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    [Route("api/[controller]", Name = "单号规则管理")]
    [ApiController]
    [ApiModule("routine", "日常办公")]
    public class HbtNumberRuleController : HbtBaseController
    {
        private readonly IHbtNumberRuleService _numberRuleService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="numberRuleService">单号规则服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtNumberRuleController(
            IHbtNumberRuleService numberRuleService,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, currentUser, localization)
        {
            _numberRuleService = numberRuleService;
        }

        /// <summary>
        /// 获取单号规则分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>单号规则分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("routine:numberrule:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtNumberRuleQueryDto query)
        {
            var result = await _numberRuleService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取单号规则详情
        /// </summary>
        /// <param name="numberRuleId">单号规则ID</param>
        /// <returns>单号规则详情</returns>
        [HttpGet("{numberRuleId}")]
        public async Task<IActionResult> GetByIdAsync(long numberRuleId)
        {
            var result = await _numberRuleService.GetByIdAsync(numberRuleId);
            return Success(result);
        }

        /// <summary>
        /// 创建单号规则
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>单号规则ID</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] HbtNumberRuleCreateDto input)
        {
            var result = await _numberRuleService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新单号规则
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtNumberRuleUpdateDto input)
        {
            var result = await _numberRuleService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除单号规则
        /// </summary>
        /// <param name="numberRuleId">单号规则ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{numberRuleId}")]
        public async Task<IActionResult> DeleteAsync(long numberRuleId)
        {
            var result = await _numberRuleService.DeleteAsync(numberRuleId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除单号规则
        /// </summary>
        /// <param name="numberRuleIds">单号规则ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] numberRuleIds)
        {
            var result = await _numberRuleService.BatchDeleteAsync(numberRuleIds);
            return Success(result);
        }

        /// <summary>
        /// 导入单号规则数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "单号规则信息")
        {
            using var stream = file.OpenReadStream();
            var result = await _numberRuleService.ImportAsync(stream, sheetName);
            return Success(result);
        }

        /// <summary>
        /// 导出单号规则数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtNumberRuleQueryDto query, [FromQuery] string sheetName = "单号规则信息")
        {
            var result = await _numberRuleService.ExportAsync(query, sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "单号规则信息")
        {
            var result = await _numberRuleService.GetTemplateAsync(sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }


    }
} 