//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDictTypeController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 字典类型控制器
//===================================================================

using Hbt.Cur.Application.Dtos.Routine.Core;
using Hbt.Cur.Application.Services.Core;

namespace Hbt.Cur.WebApi.Controllers.Routine.Core
{
    /// <summary>
    /// 字典类型控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-17
    /// </remarks>
    [Route("api/[controller]", Name = "字典类型")]
    [ApiController]
    [ApiModule("core", "系统管理")]
    public class HbtDictTypeController : HbtBaseController
    {
        private readonly IHbtDictTypeService _dictTypeService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dictTypeService">字典类型服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtDictTypeController(
            IHbtDictTypeService dictTypeService,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, currentUser, localization)
        {
            _dictTypeService = dictTypeService;
        }

        /// <summary>
        /// 获取字典类型分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>字典类型分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("core:dict:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtDictTypeQueryDto query)
        {
            var result = await _dictTypeService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取字典类型详情
        /// </summary>
        /// <param name="dictTypeId">字典类型ID</param>
        /// <returns>字典类型详情</returns>
        [HttpGet("{dictTypeId}")]
        [HbtPerm("core:dict:query")]
        public async Task<IActionResult> GetByIdAsync(long dictTypeId)
        {
            var result = await _dictTypeService.GetByIdAsync(dictTypeId);
            return Success(result);
        }

        /// <summary>
        /// 根据字典类型获取详情
        /// </summary>
        /// <param name="type">字典类型</param>
        /// <returns>字典类型详情</returns>
        [HttpGet("type/{type}")]
        [HbtPerm("core:dict:query")]
        public async Task<IActionResult> GetByTypeAsync(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                return Error("字典类型不能为空");
            }
            var result = await _dictTypeService.GetByTypeAsync(type);
            return Success(result);
        }

        /// <summary>
        /// 创建字典类型
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>字典类型ID</returns>
        [HttpPost]
        [HbtPerm("core:dict:create")]
        public async Task<IActionResult> CreateAsync([FromBody] HbtDictTypeCreateDto input)
        {
            var result = await _dictTypeService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新字典类型
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [HbtPerm("core:dict:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtDictTypeUpdateDto input)
        {
            var result = await _dictTypeService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除字典类型
        /// </summary>
        /// <param name="dictTypeId">字典类型ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{dictTypeId}")]
        [HbtPerm("core:dict:delete")]
        public async Task<IActionResult> DeleteAsync(long dictTypeId)
        {
            var result = await _dictTypeService.DeleteAsync(dictTypeId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除字典类型
        /// </summary>
        /// <param name="dictTypeIds">字典类型ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [HbtPerm("core:dict:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] dictTypeIds)
        {
            var result = await _dictTypeService.BatchDeleteAsync(dictTypeIds);
            return Success(result);
        }

        /// <summary>
        /// 导入字典类型
        /// </summary>
        /// <param name="file">字典类型数据文件</param>
        /// <param name="sheetName">表格名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [HbtPerm("core:dict:import")]
        [ProducesResponseType(typeof((int Success, int Fail)), StatusCodes.Status200OK)]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "字典类型")
        {
            if (file == null || file.Length == 0)
                return BadRequest(_localization.L("DictType.Import.FileRequired"));

            using var stream = file.OpenReadStream();
            var (success, fail) = await _dictTypeService.ImportAsync(stream, sheetName);
            return Success(new { success, fail }, _localization.L("DictType.Import.Success"));
        }

        /// <summary>
        /// 导出字典类型
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">表格名称</param>
        /// <returns>导出数据列表</returns>
        [HttpGet("export")]
        [HbtPerm("core:dict:export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtDictTypeQueryDto query, [FromQuery] string sheetName = "字典类型")
        {
            var result = await _dictTypeService.ExportAsync(query, sheetName);
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
        /// <param name="sheetName">表格名称</param>
        /// <returns>模板数据</returns>
        [HttpGet("template")]
        [HbtPerm("core:dict:query")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "字典类型")
        {
            var result = await _dictTypeService.GetTemplateAsync(sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 更新字典类型状态
        /// </summary>
        /// <param name="dictTypeId">字典类型ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{dictTypeId}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatusAsync(long dictTypeId, [FromQuery] int status)
        {
            var input = new HbtDictTypeStatusDto
            {
                DictTypeId = dictTypeId,
                Status = status
            };
            var result = await _dictTypeService.UpdateStatusAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 执行字典SQL脚本
        /// </summary>
        /// <param name="dictTypeId">字典类型ID</param>
        /// <returns>字典数据列表</returns>
        [HttpGet("executeSql/{dictTypeId}")]
        [HbtPerm("core:dict:list")]
        public async Task<IActionResult> ExecuteDictSqlAsync(long dictTypeId)
        {
            var dictType = await _dictTypeService.GetByIdAsync(dictTypeId);
            if (dictType == null)
            {
                throw new HbtException("字典类型不存在");
            }

            if (string.IsNullOrEmpty(dictType.SqlScript))
            {
                throw new HbtException("SQL脚本为空");
            }

            var result = await _dictTypeService.ExecuteDictSqlAsync(dictType.SqlScript);

            // 填充字典类型信息
            foreach (var item in result)
            {
                item.DictType = dictType.DictType;
            }

            return Success(result);
        }
    }
}