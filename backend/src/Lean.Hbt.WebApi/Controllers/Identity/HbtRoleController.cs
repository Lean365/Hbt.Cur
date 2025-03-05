//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtRoleController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 角色控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Application.Services.Identity;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.IServices.Admin;

namespace Lean.Hbt.WebApi.Controllers.Identity
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    [Route("api/[controller]", Name = "角色")]
    [ApiController]
    [ApiModule("identity", "身份认证")]
    public class HbtRoleController : HbtBaseController
    {
        private readonly IHbtRoleService _roleService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="roleService">角色服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtRoleController(IHbtRoleService roleService, IHbtLocalizationService localization) : base(localization)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// 获取角色分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>角色分页列表</returns>
        [HttpGet]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] HbtRoleQueryDto query)
        {
            var result = await _roleService.GetPagedListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取角色详情
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>角色详情</returns>
        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetAsync(long roleId)
        {
            var result = await _roleService.GetAsync(roleId);
            return Success(result);
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>角色ID</returns>
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] HbtRoleCreateDto input)
        {
            var result = await _roleService.InsertAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtRoleUpdateDto input)
        {
            var result = await _roleService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteAsync(long roleId)
        {
            var result = await _roleService.DeleteAsync(roleId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="roleIds">角色ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] roleIds)
        {
            var result = await _roleService.BatchDeleteAsync(roleIds);
            return Success(result);
        }

        /// <summary>
        /// 导入角色数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        public async Task<IActionResult> ImportAsync([FromForm] IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var result = await _roleService.ImportAsync(stream, "Sheet1");
            return Success(result, _localization.L("Role.Import.Success"));
        }

        /// <summary>
        /// 导出角色数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtRoleQueryDto query)
        {
            var result = await _roleService.ExportAsync(query, "Sheet1");
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "角色数据.xlsx");
        }

        /// <summary>
        /// 获取角色导入模板
        /// </summary>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _roleService.GetTemplateAsync("Sheet1");
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "角色导入模板.xlsx");
        }

        /// <summary>
        /// 更新角色状态
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{roleId}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatusAsync(long roleId, [FromQuery] int status)
        {
            var input = new HbtRoleStatusDto
            {
                RoleId = roleId,
                Status = status
            };
            var result = await _roleService.UpdateStatusAsync(input);
            return Success(result);
        }
    }
}