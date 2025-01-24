//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtMenuController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 菜单控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Application.Services.Identity;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.IServices.Admin;

namespace Lean.Hbt.WebApi.Controllers.Identity
{
    /// <summary>
    /// 菜单控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    [Route("api/[controller]", Name = "菜单")]
    [ApiController]
    [ApiModule("identity", "身份认证")]
    public class HbtMenuController : HbtBaseController
    {
        private readonly IHbtMenuService _menuService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="menuService">菜单服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtMenuController(IHbtMenuService menuService, IHbtLocalizationService localization) : base(localization)
        {
            _menuService = menuService;
        }

        /// <summary>
        /// 获取菜单分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>菜单分页列表</returns>
        [HttpGet]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] HbtMenuQueryDto query)
        {
            var result = await _menuService.GetPagedListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <returns>菜单详情</returns>
        [HttpGet("{menuId}")]
        public async Task<IActionResult> GetAsync(long menuId)
        {
            var result = await _menuService.GetAsync(menuId);
            return Success(result);
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>菜单ID</returns>
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] HbtMenuCreateDto input)
        {
            var result = await _menuService.InsertAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtMenuUpdateDto input)
        {
            var result = await _menuService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{menuId}")]
        public async Task<IActionResult> DeleteAsync(long menuId)
        {
            var result = await _menuService.DeleteAsync(menuId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除菜单
        /// </summary>
        /// <param name="menuIds">菜单ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] List<long> menuIds)
        {
            var result = await _menuService.BatchDeleteAsync(menuIds);
            return Success(result);
        }

        /// <summary>
        /// 导入菜单数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        public async Task<IActionResult> ImportAsync([FromForm] IFormFile file, [FromQuery] string sheetName = "Sheet1")
        {
            using var stream = file.OpenReadStream();
            var result = await _menuService.ImportAsync(stream, sheetName);
            return Success(result);
        }

        /// <summary>
        /// 导出菜单数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtMenuQueryDto query, [FromQuery] string sheetName = "菜单数据")
        {
            var result = await _menuService.ExportAsync(query, sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "菜单数据.xlsx");
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入模板Excel文件</returns>
        [HttpGet("template")]
        public async Task<IActionResult> GetImportTemplateAsync([FromQuery] string sheetName = "菜单导入模板")
        {
            var result = await _menuService.GenerateTemplateAsync(sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "菜单导入模板.xlsx");
        }

        /// <summary>
        /// 更新菜单状态
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{menuId}/status")]
        public async Task<IActionResult> UpdateStatusAsync(long menuId, [FromQuery] HbtStatus status)
        {
            var input = new HbtMenuStatusDto
            {
                MenuId = menuId,
                Status = status
            };
            var result = await _menuService.UpdateStatusAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新菜单排序
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <param name="orderNum">显示顺序</param>
        /// <returns>是否成功</returns>
        [HttpPut("{menuId}/order")]
        public async Task<IActionResult> UpdateOrderAsync(long menuId, [FromQuery] int orderNum)
        {
            var input = new HbtMenuOrderDto
            {
                MenuId = menuId,
                OrderNum = orderNum
            };
            var result = await _menuService.UpdateOrderAsync(input);
            return Success(result);
        }
    }
}