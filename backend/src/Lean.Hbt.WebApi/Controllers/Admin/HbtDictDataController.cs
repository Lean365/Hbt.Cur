//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDictDataController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 字典数据控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Admin;
using Lean.Hbt.Application.Services.Admin;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.IServices.Admin;

namespace Lean.Hbt.WebApi.Controllers.Admin
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
    [ApiModule("system", "系统管理")]
    public class HbtDictDataController : HbtBaseController
    {
        private readonly IHbtDictDataService _dictDataService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dictDataService">字典数据服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtDictDataController(IHbtDictDataService dictDataService, IHbtLocalizationService localization) : base(localization)
        {
            _dictDataService = dictDataService;
        }

        /// <summary>
        /// 获取字典数据分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>字典数据分页列表</returns>
        [HttpGet]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] HbtDictDataQueryDto query)
        {
            var result = await _dictDataService.GetPagedListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取字典数据详情
        /// </summary>
        /// <param name="dictDataId">字典数据ID</param>
        /// <returns>字典数据详情</returns>
        [HttpGet("{dictDataId}")]
        public async Task<IActionResult> GetAsync(long dictDataId)
        {
            var result = await _dictDataService.GetAsync(dictDataId);
            return Success(result);
        }

        /// <summary>
        /// 创建字典数据
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>字典数据ID</returns>
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] HbtDictDataCreateDto input)
        {
            var result = await _dictDataService.InsertAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新字典数据
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
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
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] dictDataIds)
        {
            var result = await _dictDataService.BatchDeleteAsync(dictDataIds);
            return Success(result);
        }

        /// <summary>
        /// 导入字典数据
        /// </summary>
        /// <param name="dictDatas">字典数据列表</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        public async Task<IActionResult> ImportAsync([FromBody] List<HbtDictDataImportDto> dictDatas)
        {
            var result = await _dictDataService.ImportAsync(dictDatas);
            return Success(result);
        }

        /// <summary>
        /// 导出字典数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>导出数据列表</returns>
        [HttpGet("export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtDictDataQueryDto query)
        {
            var result = await _dictDataService.ExportAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <returns>模板数据</returns>
        [HttpGet("template")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _dictDataService.GetTemplateAsync();
            return Success(result);
        }

        /// <summary>
        /// 更新字典数据状态
        /// </summary>
        /// <param name="dictDataId">字典数据ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{dictDataId}/status")]
        public async Task<IActionResult> UpdateStatusAsync(long dictDataId, [FromQuery] HbtStatus status)
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