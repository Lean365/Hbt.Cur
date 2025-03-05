//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDeptController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 部门控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Application.Services.Identity;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.IServices.Admin;

namespace Lean.Hbt.WebApi.Controllers.Identity
{
    /// <summary>
    /// 部门控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    [Route("api/[controller]", Name = "部门")]
    [ApiController]
    [ApiModule("identity", "身份认证")]
    public class HbtDeptController : HbtBaseController
    {
        private readonly IHbtDeptService _deptService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="deptService">部门服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtDeptController(IHbtDeptService deptService, IHbtLocalizationService localization) : base(localization)
        {
            _deptService = deptService;
        }

        /// <summary>
        /// 获取部门分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>部门分页列表</returns>
        [HttpGet]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] HbtDeptQueryDto query)
        {
            var result = await _deptService.GetPagedListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取部门详情
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <returns>部门详情</returns>
        [HttpGet("{deptId}")]
        public async Task<IActionResult> GetAsync(long deptId)
        {
            var result = await _deptService.GetAsync(deptId);
            return Success(result);
        }

        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>部门ID</returns>
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] HbtDeptCreateDto input)
        {
            var result = await _deptService.InsertAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtDeptUpdateDto input)
        {
            var result = await _deptService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{deptId}")]
        public async Task<IActionResult> DeleteAsync(long deptId)
        {
            var result = await _deptService.DeleteAsync(deptId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除部门
        /// </summary>
        /// <param name="deptIds">部门ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] List<long> deptIds)
        {
            var result = await _deptService.BatchDeleteAsync(deptIds);
            return Success(result);
        }

        /// <summary>
        /// 导入部门数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        public async Task<IActionResult> ImportAsync([FromForm] IFormFile file, [FromQuery] string sheetName = "部门数据")
        {
            using var stream = file.OpenReadStream();
            var result = await _deptService.ImportAsync(stream, sheetName);
            return Success(result);
        }

        /// <summary>
        /// 导出部门数据
        /// </summary>
        /// <param name="data">要导出的数据</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件</returns>
        [HttpPost("export")]
        public async Task<IActionResult> ExportAsync([FromBody] IEnumerable<HbtDeptExportDto> data, [FromQuery] string sheetName = "部门信息")
        {
            var result = await _deptService.ExportAsync(data, sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"部门信息_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        public async Task<IActionResult> GenerateTemplateAsync([FromQuery] string sheetName = "部门导入模板")
        {
            var result = await _deptService.GenerateTemplateAsync(sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"部门导入模板_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 获取部门树形结构
        /// </summary>
        /// <returns>返回树形部门列表</returns>
        [HttpGet("tree")]
        [ProducesResponseType(typeof(HbtApiResult<List<HbtDeptDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTreeAsync()
        {
            var depts = await _deptService.GetTreeAsync(0);
            return Success(depts);
        }

        /// <summary>
        /// 更新部门状态
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{deptId}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatusAsync(long deptId, [FromQuery] int status)
        {
            var result = await _deptService.UpdateStatusAsync(deptId, status);
            return Success(result);
        }
    }
}