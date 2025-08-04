//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDictDataController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 字典数据控制器
//===================================================================

using Hbt.Cur.Application.Dtos.Routine.Core;
using Hbt.Cur.Application.Services.Core;
using Hbt.Cur.Domain.IServices.Extensions;

namespace Hbt.Cur.WebApi.Controllers.Routine.Core
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
    [ApiModule("core", "系统管理")]
    public class HbtDictDataController : HbtBaseController
    {
        private readonly IHbtDictDataService _dictDataService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dictDataService">字典数据服务</param>
        /// <param name="localization">本地化服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        public HbtDictDataController(IHbtDictDataService dictDataService,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization,
            IHbtLogger logger) : base(logger, currentUser, localization)
        {
            _dictDataService = dictDataService;
        }

        /// <summary>
        /// 获取字典数据分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>字典数据分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("core:dict:list")]
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
        [HbtPerm("core:dict:query")]
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
        [HbtPerm("core:dict:create")]
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
        [HbtPerm("core:dict:update")]
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
        [HbtPerm("core:dict:delete")]
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
        [HbtPerm("core:dict:delete")]
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
        [HbtPerm("core:dict:import")]
        [ProducesResponseType(typeof((int Success, int Fail)), StatusCodes.Status200OK)]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "字典数据")
        {
            if (file == null || file.Length == 0)
                return BadRequest(_localization.L("Dict.Import.FileRequired"));

            using var stream = file.OpenReadStream();
            var (success, fail) = await _dictDataService.ImportAsync(stream, sheetName);
            return Success(new { success, fail }, _localization.L("Dict.Import.Success"));
        }

        /// <summary>
        /// 导出字典数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        [HbtPerm("core:dict:export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtDictDataQueryDto query, [FromQuery] string sheetName = "字典数据")
        {
            var result = await _dictDataService.ExportAsync(query, sheetName);
            var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
                ? "application/zip"
                : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // 只在 filename* 用 UTF-8 编码，filename 用 ASCII
            var safeFileName = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(result.fileName));
            Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
            return File(result.content, contentType, result.fileName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [HbtPerm("core:dict:query")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "字典数据导入模板")
        {
            var result = await _dictDataService.GetTemplateAsync(sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 根据字典类型查询字典数据列表
        /// </summary>
        /// <param name="dictType">字典类型</param>
        /// <returns>字典数据列表</returns>
        [HttpGet("type/{dictType}")]
        [HbtPerm("core:dict:query")]
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