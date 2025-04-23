//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDictDataController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 字典数据控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Core;
using Lean.Hbt.Application.Services.Core;
using Lean.Hbt.Domain.IServices.Extensions;

namespace Lean.Hbt.WebApi.Controllers.Core
{
    /// <summary>
    /// 字典数据控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-17
    /// </remarks>
    [Route("api/[controller]", Name = "字典数据")]
    [ApiController]
    [ApiModule("admin", "系统管理")]
    public class HbtDictDataController : HbtBaseController
    {
        private readonly IHbtDictDataService _dictDataService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dictDataService">字典数据服务</param>
        /// <param name="localization">本地化服务</param>
        /// <param name="logger">日志服务</param>
        public HbtDictDataController(IHbtDictDataService dictDataService,
        
        IHbtLocalizationService localization,
        IHbtLogger logger) : base(localization, logger)
        {
            _dictDataService = dictDataService;
        }

        /// <summary>
        /// 获取字典数据分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>字典数据分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("admin:dict:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtDictDataQueryDto query)
        {
            var result = await _dictDataService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取字典数据详情
        /// </summary>
        /// <param name="dictDataId">字典数据ID</param>
        /// <returns>字典数据详情</returns>
        [HttpGet("{dictDataId}")]
        [HbtPerm("admin:dict:query")]
        public async Task<IActionResult> GetByIdAsync(long dictDataId)
        {
            var result = await _dictDataService.GetByIdAsync(dictDataId);
            return Success(result);
        }

        /// <summary>
        /// 创建字典数据
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>字典数据ID</returns>
        [HttpPost]
        [HbtPerm("admin:dict:create")]
        public async Task<IActionResult> CreateAsync([FromBody] HbtDictDataCreateDto input)
        {
            var result = await _dictDataService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新字典数据
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [HbtPerm("admin:dict:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtDictDataUpdateDto input)
        {
            var result = await _dictDataService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除字典数据
        /// </summary>
        /// <param name="dictDataId">字典数据ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{dictDataId}")]
        [HbtPerm("admin:dict:delete")]
        public async Task<IActionResult> DeleteAsync(long dictDataId)
        {
            var result = await _dictDataService.DeleteAsync(dictDataId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除字典数据
        /// </summary>
        /// <param name="dictDataIds">字典数据ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [HbtPerm("admin:dict:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] dictDataIds)
        {
            var result = await _dictDataService.BatchDeleteAsync(dictDataIds);
            return Success(result);
        }

        /// <summary>
        /// 导入字典数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [HbtPerm("admin:dict:import")]
        [ProducesResponseType(typeof((int Success, int Fail)), StatusCodes.Status200OK)]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "字典数据")
        {
            if (file == null || file.Length == 0)
                return BadRequest("请选择要导入的文件");

            using var stream = file.OpenReadStream();
            var result = await _dictDataService.ImportAsync(stream, sheetName);
            return Ok(result);
        }

        /// <summary>
        /// 导出字典数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        [HbtPerm("admin:dict:export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtDictDataQueryDto query, [FromQuery] string sheetName = "字典数据")
        {
            var (_, content) = await _dictDataService.ExportAsync(query, sheetName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"字典数据_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [HbtPerm("admin:dict:query")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "字典数据导入模板")
        {
            var (_, content) = await _dictDataService.GetTemplateAsync(sheetName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"字典数据导入模板_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 根据字典类型查询字典数据列表
        /// </summary>
        /// <param name="dictType">字典类型</param>
        /// <returns>字典数据列表</returns>
        [HttpGet("type/{dictType}")]
        [HbtPerm("admin:dict:query")]
        public async Task<IActionResult> GetDictDataByTypeAsync([FromRoute] string dictType)
        {
            if (string.IsNullOrEmpty(dictType))
            {
                return BadRequest("字典类型不能为空");
            }

            var result = await _dictDataService.GetListByDictTypeAsync(dictType);
            return Success(result);
        }

        /// <summary>
        /// 更新字典数据状态
        /// </summary>
        /// <param name="dictDataId">字典数据ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{dictDataId}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatusAsync(long dictDataId, [FromQuery] int status)
        {
            var input = new HbtDictDataStatusDto
            {
                DictDataId = dictDataId,
                Status = status
            };
            var result = await _dictDataService.UpdateStatusAsync(input);
            return Success(result);
        }
    }
}