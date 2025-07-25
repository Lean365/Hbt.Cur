using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Application.Services.Identity;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Common.Helpers;

namespace Lean.Hbt.WebApi.Controllers.Identity
{
    /// <summary>
    /// OAuth认证控制器
    /// </summary>
    [Route("api/[controller]", Name = "OAuth认证")]
    [ApiController]
    [ApiModule("identity", "身份认证")]
    public class HbtOAuthController : HbtBaseController
    {
        private readonly IHbtOAuthService _oauthService;
        private readonly IHbtIdentitySessionManager _sessionManager;
        protected readonly IHbtRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 获取用户仓储
        /// </summary>
        private IHbtRepository<HbtUser> UserRepository => _repositoryFactory.GetAuthRepository<HbtUser>();

        /// <summary>
        /// 构造函数
        /// <param name="oauthService">OAuth服务</param>
        /// <param name="sessionManager">会话管理器</param>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志</param>
        /// <param name="currentUser">当前用户</param>
        /// <param name="localization">本地化</param>
        /// </summary>
        public HbtOAuthController(
            IHbtOAuthService oauthService,
            IHbtIdentitySessionManager sessionManager,
            IHbtRepositoryFactory repositoryFactory,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, currentUser, localization)
        {
            _oauthService = oauthService;
            _sessionManager = sessionManager;
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取OAuth授权地址
        /// </summary>
        /// <param name="provider">OAuth提供商</param>
        /// <returns>授权地址</returns>
        [HttpGet("authorize/{provider}")]
        [AllowAnonymous]
        public async Task<IActionResult> Authorize(string provider)
        {
            var state = Guid.NewGuid().ToString("N");
            var url = await _oauthService.GetAuthorizationUrlAsync(provider, state);
            return Ok(url);
        }

        /// <summary>
        /// 微信登录
        /// </summary>
        /// <returns>授权地址</returns>
        [HttpPost("wechat")]
        [AllowAnonymous]
        public async Task<IActionResult> WeChatLogin()
        {
            try
            {
                var authUrl = await _oauthService.GetAuthorizationUrlAsync("wechat");
                return Success(new { success = true, authUrl }, "获取微信授权地址成功");
            }
            catch (Exception ex)
            {
                return Error($"微信登录失败: {ex.Message}");
            }
        }

        /// <summary>
        /// QQ登录
        /// </summary>
        /// <returns>授权地址</returns>
        [HttpPost("qq")]
        [AllowAnonymous]
        public async Task<IActionResult> QQLogin()
        {
            try
            {
                var authUrl = await _oauthService.GetAuthorizationUrlAsync("qq");
                return Success(new { success = true, authUrl }, "获取QQ授权地址成功");
            }
            catch (Exception ex)
            {
                return Error($"QQ登录失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 微博登录
        /// </summary>
        /// <returns>授权地址</returns>
        [HttpPost("weibo")]
        [AllowAnonymous]
        public async Task<IActionResult> WeiboLogin()
        {
            try
            {
                var authUrl = await _oauthService.GetAuthorizationUrlAsync("weibo");
                return Success(new { success = true, authUrl }, "获取微博授权地址成功");
            }
            catch (Exception ex)
            {
                return Error($"微博登录失败: {ex.Message}");
            }
        }

        /// <summary>
        /// GitHub登录
        /// </summary>
        /// <returns>授权地址</returns>
        [HttpPost("github")]
        [AllowAnonymous]
        public async Task<IActionResult> GitHubLogin()
        {
            try
            {
                var authUrl = await _oauthService.GetAuthorizationUrlAsync("github");
                return Success(new { success = true, authUrl }, "获取GitHub授权地址成功");
            }
            catch (Exception ex)
            {
                return Error($"GitHub登录失败: {ex.Message}");
            }
        }

        /// <summary>
        /// Google登录
        /// </summary>
        /// <returns>授权地址</returns>
        [HttpPost("google")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleLogin()
        {
            try
            {
                var authUrl = await _oauthService.GetAuthorizationUrlAsync("google");
                return Success(new { success = true, authUrl }, "获取Google授权地址成功");
            }
            catch (Exception ex)
            {
                return Error($"Google登录失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 钉钉登录
        /// </summary>
        /// <returns>授权地址</returns>
        [HttpPost("dingtalk")]
        [AllowAnonymous]
        public async Task<IActionResult> DingTalkLogin()
        {
            try
            {
                var authUrl = await _oauthService.GetAuthorizationUrlAsync("dingtalk");
                return Success(new { success = true, authUrl }, "获取钉钉授权地址成功");
            }
            catch (Exception ex)
            {
                return Error($"钉钉登录失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 处理OAuth回调
        /// </summary>
        /// <param name="provider">OAuth提供商</param>
        /// <param name="code">授权码</param>
        /// <param name="state">状态码</param>
        /// <returns>登录结果</returns>
        [HttpGet("callback/{provider}")]
        [AllowAnonymous]
        public async Task<IActionResult> Callback(string provider, string code, string state)
        {
            var loginResult = await _oauthService.HandleCallbackAsync(provider, code, state);

            if (!loginResult.Success)
            {
                return Error("OAuth登录失败");
            }

            return Ok(loginResult);
        }

        #region OAuth账号管理

        /// <summary>
        /// 获取OAuth账号分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>OAuth账号分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("identity:oauth:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtOAuthQueryDto query)
        {
            var result = await _oauthService.GetListAsync(query);
            return Success(result, _localization.L("OAuth.List.Success"));
        }

        /// <summary>
        /// 获取OAuth账号详情
        /// </summary>
        /// <param name="oauthId">OAuth账号ID</param>
        /// <returns>OAuth账号详情</returns>
        [HttpGet("{oauthId}")]
        [HbtPerm("identity:oauth:query")]
        public async Task<IActionResult> GetByIdAsync(long oauthId)
        {
            var result = await _oauthService.GetByIdAsync(oauthId);
            return Success(result, _localization.L("OAuth.Get.Success"));
        }

        /// <summary>
        /// 创建OAuth账号
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>OAuth账号ID</returns>
        [HttpPost]
        [HbtLog("创建OAuth账号")]
        [HbtPerm("identity:oauth:create")]
        public async Task<IActionResult> CreateAsync([FromBody] HbtOAuthCreateDto input)
        {
            var result = await _oauthService.CreateAsync(input);
            return Success(result, _localization.L("OAuth.Insert.Success"));
        }

        /// <summary>
        /// 更新OAuth账号
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [HbtLog("更新OAuth账号")]
        [HbtPerm("identity:oauth:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtOAuthUpdateDto input)
        {
            var result = await _oauthService.UpdateAsync(input);
            return Success(result, _localization.L("OAuth.Update.Success"));
        }

        /// <summary>
        /// 删除OAuth账号
        /// </summary>
        /// <param name="oauthId">OAuth账号ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{oauthId}")]
        [HbtLog("删除OAuth账号")]
        [HbtPerm("identity:oauth:delete")]
        public async Task<IActionResult> DeleteAsync(long oauthId)
        {
            try
            {
                var result = await _oauthService.DeleteAsync(oauthId);
                return Success(result, _localization.L("OAuth.Delete.Success"));
            }
            catch (HbtException ex)
            {
                return Error(ex.Message);
            }
        }

        /// <summary>
        /// 批量删除OAuth账号
        /// </summary>
        /// <param name="oauthIds">OAuth账号ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [HbtPerm("identity:oauth:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] oauthIds)
        {
            var result = await _oauthService.BatchDeleteAsync(oauthIds);
            return Success(result, _localization.L("OAuth.BatchDelete.Success"));
        }

        /// <summary>
        /// 导入OAuth账号数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [HbtPerm("identity:oauth:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var importList = await HbtExcelHelper.ImportAsync<HbtOAuthImportDto>(stream, "Sheet1");
            var result = await _oauthService.ImportAsync(importList);
            return Success(new { success = result.success, failed = result.failed, errors = result.errors }, _localization.L("OAuth.Import.Success"));
        }

        /// <summary>
        /// 导出OAuth账号数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        [HbtPerm("identity:oauth:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtOAuthQueryDto query)
        {
            var exportList = await _oauthService.ExportAsync(query);
            var result = await HbtExcelHelper.ExportAsync(exportList, "OAuth账号信息");
            Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 获取OAuth账号导入模板
        /// </summary>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [HbtPerm("identity:oauth:export")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var template = await _oauthService.GetImportTemplateAsync();
            var result = await HbtExcelHelper.ExportAsync(template, "OAuth账号模板");
            Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 更新OAuth账号状态
        /// </summary>
        /// <param name="oauthId">OAuth账号ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{oauthId}/status")]
        [HbtPerm("identity:oauth:update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatusAsync(long oauthId, [FromQuery] int status)
        {
            var input = new HbtOAuthStatusDto
            {
                OAuthId = oauthId,
                Status = status
            };
            var result = await _oauthService.UpdateStatusAsync(input);
            return Success(result, _localization.L("OAuth.Status.Success"));
        }

        /// <summary>
        /// 批量更新OAuth账号状态
        /// </summary>
        /// <param name="oauthIds">OAuth账号ID集合</param>
        /// <param name="status">状态</param>
        /// <param name="remark">备注</param>
        /// <returns>是否成功</returns>
        [HttpPut("batch/status")]
        [HbtPerm("identity:oauth:update")]
        public async Task<IActionResult> BatchUpdateStatusAsync([FromBody] long[] oauthIds, [FromQuery] int status, [FromQuery] string? remark = null)
        {
            var result = await _oauthService.BatchUpdateStatusAsync(oauthIds, status, remark);
            return Success(result, _localization.L("OAuth.BatchStatus.Success"));
        }

        #endregion

        #region OAuth绑定解绑

        /// <summary>
        /// 绑定OAuth账号
        /// </summary>
        /// <param name="input">绑定对象</param>
        /// <returns>是否成功</returns>
        [HttpPost("bind")]
        [HbtLog("绑定OAuth账号")]
        [HbtPerm("identity:oauth:bind")]
        public async Task<IActionResult> BindOAuthAccountAsync([FromBody] HbtOAuthBindDto input)
        {
            var result = await _oauthService.BindOAuthAccountAsync(input);
            return Success(result, _localization.L("OAuth.Bind.Success"));
        }

        /// <summary>
        /// 解绑OAuth账号
        /// </summary>
        /// <param name="input">解绑对象</param>
        /// <returns>是否成功</returns>
        [HttpPost("unbind")]
        [HbtLog("解绑OAuth账号")]
        [HbtPerm("identity:oauth:unbind")]
        public async Task<IActionResult> UnbindOAuthAccountAsync([FromBody] HbtOAuthUnbindDto input)
        {
            var result = await _oauthService.UnbindOAuthAccountAsync(input);
            return Success(result, _localization.L("OAuth.Unbind.Success"));
        }

        /// <summary>
        /// 获取用户绑定的OAuth账号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>绑定的OAuth账号列表</returns>
        [HttpGet("user/{userId}/accounts")]
        [HbtPerm("identity:oauth:query")]
        public async Task<IActionResult> GetUserOAuthAccountsAsync(long userId)
        {
            var result = await _oauthService.GetUserOAuthAccountsAsync(userId);
            return Success(result, _localization.L("OAuth.UserAccounts.Success"));
        }

        #endregion

        #region OAuth提供商管理

        /// <summary>
        /// 获取支持的OAuth提供商列表
        /// </summary>
        /// <returns>提供商列表</returns>
        [HttpGet("providers")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProvidersAsync()
        {
            var result = await _oauthService.GetProvidersAsync();
            return Success(result, _localization.L("OAuth.Providers.Success"));
        }

        #endregion
    }
}