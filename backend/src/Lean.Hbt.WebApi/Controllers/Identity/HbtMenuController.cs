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
using Lean.Hbt.Domain.IServices.Admin;
using Lean.Hbt.Domain.IServices.Identity;

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
        private readonly IHbtCurrentTenant _currentTenant;
        private readonly ILogger<HbtMenuController> _logger;
        private readonly IHbtCurrentUser _currentUser;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="menuService">菜单服务</param>
        /// <param name="localization">本地化服务</param>
        /// <param name="tenantContext">租户上下文</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">用户上下文</param>
        public HbtMenuController(
            IHbtMenuService menuService,
            IHbtLocalizationService localization,
            IHbtCurrentTenant tenantContext,
            ILogger<HbtMenuController> logger,
            IHbtCurrentUser currentUser) : base(localization)
        {
            _menuService = menuService;
            _currentTenant = tenantContext;
            _logger = logger;
            _currentUser = currentUser;
        }

        /// <summary>
        /// 获取菜单分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>菜单分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("identity:menu:query")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtMenuQueryDto query)
        {
            var result = await _menuService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <returns>菜单详情</returns>
        [HttpGet("{menuId}")]
        [HbtPerm("identity:menu:query")]
        public async Task<IActionResult> GetByIdAsync(long menuId)
        {
            var result = await _menuService.GetByIdAsync(menuId);
            return Success(result);
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>菜单ID</returns>
        [HttpPost]
        [HbtPerm("identity:menu:create")]
        public async Task<IActionResult> CreateAsync([FromBody] HbtMenuCreateDto input)
        {
            var result = await _menuService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [HbtPerm("identity:menu:update")]
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
        [HbtPerm("identity:menu:delete")]
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
        [HbtPerm("identity:menu:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] menuIds)
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
        [HbtPerm("identity:menu:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "Sheet1")
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
        [HbtPerm("identity:menu:export")]
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
        [HbtPerm("identity:menu:query")]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HbtPerm("identity:menu:update")]
        public async Task<IActionResult> UpdateStatusAsync(long menuId, [FromQuery] int status)
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
        [HbtPerm("identity:menu:update")]
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

        /// <summary>
        /// 获取菜单树形结构
        /// </summary>
        /// <returns>返回树形菜单列表</returns>
        [HttpGet("tree")]
        [HbtPerm("identity:menu:query")]
        [ProducesResponseType(typeof(HbtApiResult<List<HbtMenuDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTreeAsync()
        {
            var menus = await _menuService.GetTreeAsync();
            return Success(menus);
        }

        /// <summary>
        /// 获取当前用户的菜单树
        /// </summary>
        /// <returns>当前用户的菜单树</returns>
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUserMenusAsync()
        {
            var userId = _currentUser.UserId;
            var result = await _menuService.GetCurrentUserMenusAsync(userId);
            return Success(result);
        }

        /// <summary>
        /// 获取菜单选项列表
        /// </summary>
        /// <returns>菜单选项列表</returns>
        [HttpGet("options")]
        [HbtPerm("identity:menu:query")]
        public async Task<IActionResult> GetOptionsAsync()
        {
            var result = await _menuService.GetOptionsAsync();
            return Success(result);
        }
    }
}