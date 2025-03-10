//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtUserController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 用户控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Application.Services.Identity;
using Lean.Hbt.Domain.IServices.Admin;

namespace Lean.Hbt.WebApi.Controllers.Identity
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-17
    /// </remarks>
    [Route("api/[controller]", Name = "用户")]
    [ApiController]
    [ApiModule("identity", "身份认证")]
    public class HbtUserController : HbtBaseController
    {
        private readonly IHbtUserService _userService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userService">用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtUserController(IHbtUserService userService, IHbtLocalizationService localization)
            : base(localization)
        {
            _userService = userService;
        }

        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>用户分页列表</returns>
        [HttpGet]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] HbtUserQueryDto query)
        {
            var result = await _userService.GetPagedListAsync(query);
            return Success(result, _localization.L("User.List.Success"));
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户详情</returns>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAsync(long userId)
        {
            var result = await _userService.GetAsync(userId);
            return Success(result, _localization.L("User.Get.Success"));
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>用户ID</returns>
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] HbtUserCreateDto input)
        {
            var result = await _userService.InsertAsync(input);
            return Success(result, _localization.L("User.Insert.Success"));
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtUserUpdateDto input)
        {
            var result = await _userService.UpdateAsync(input);
            return Success(result, _localization.L("User.Update.Success"));
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteAsync(long userId)
        {
            var result = await _userService.DeleteAsync(userId);
            return Success(result, _localization.L("User.Delete.Success"));
        }

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="userIds">用户ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] userIds)
        {
            var result = await _userService.BatchDeleteAsync(userIds);
            return Success(result, _localization.L("User.BatchDelete.Success"));
        }

        /// <summary>
        /// 导入用户数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var result = await _userService.ImportAsync(stream, "Sheet1");
            return Success(result, _localization.L("User.Import.Success"));
        }

        /// <summary>
        /// 导出用户数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtUserQueryDto query)
        {
            var result = await _userService.ExportAsync(query, "Sheet1");
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "用户数据.xlsx");
        }

        /// <summary>
        /// 获取用户导入模板
        /// </summary>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _userService.GetTemplateAsync("Sheet1");
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "用户导入模板.xlsx");
        }

        /// <summary>
        /// 更新用户状态
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{userId}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatusAsync(long userId, [FromQuery] int status)
        {
            var input = new HbtUserStatusDto
            {
                UserId = userId,
                Status = status
            };
            var result = await _userService.UpdateStatusAsync(input);
            return Success(result, _localization.L("User.Status.Success"));
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="input">重置密码对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] HbtUserResetPwdDto input)
        {
            var result = await _userService.ResetPasswordAsync(input);
            return Success(result, _localization.L("User.ResetPassword.Success"));
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="input">修改密码对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] HbtUserChangePwdDto input)
        {
            var result = await _userService.ChangePasswordAsync(input);
            return Success(result, _localization.L("User.ChangePassword.Success"));
        }
    }
}