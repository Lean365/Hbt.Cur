using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices.Admin;
using Lean.Hbt.Domain.Repositories;

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
        private readonly IHbtRepository<HbtUser> _userRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtOAuthController(
            IHbtOAuthService oauthService,
            IHbtIdentitySessionManager sessionManager,
            IHbtRepository<HbtUser> userRepository,
            IHbtLocalizationService localization) : base(localization)
        {
            _oauthService = oauthService;
            _sessionManager = sessionManager;
            _userRepository = userRepository;
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
            var userInfo = await _oauthService.HandleCallbackAsync(provider, code, state);

            // 查找或创建用户
            var user = await _userRepository.GetInfoAsync(u => u.UserName == userInfo.UserName);
            if (user == null)
            {
                user = new HbtUser
                {
                    UserName = userInfo.UserName,
                    NickName = userInfo.NickName,
                    Email = userInfo.Email,
                    Avatar = userInfo.Avatar,
                    UserType = 2, // OAuth 用户类型
                    Status = 0 // 0 表示正常状态
                };
                await _userRepository.CreateAsync(user);
            }

            // 创建会话
            var sessionId = await _sessionManager.CreateSessionAsync(
                user.Id.ToString(),
                user.UserName,
                HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
                HttpContext.Request.Headers["User-Agent"].ToString()
            );

            // 返回登录结果
            var result = new HbtOAuthLoginDto
            {
                AccessToken = null, // 需要选择租户后才生成token
                NeedSelectTenant = true, // 标识需要选择租户
                UserName = user.UserName,
                NickName = user.NickName,
                Avatar = user.Avatar,
                Email = user.Email
            };

            return Ok(result);
        }

        /// <summary>
        /// OAuth用户选择租户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="tenantId">选择的租户ID</param>
        /// <returns>登录结果</returns>
        [HttpPost("select-tenant")]
        [AllowAnonymous]
        public async Task<IActionResult> SelectTenantAsync(string userName, long tenantId)
        {
            var user = await _userRepository.GetInfoAsync(u => u.UserName == userName);
            if (user == null)
            {
                return NotFound("用户不存在");
            }

            // 更新用户的租户ID
            user.TenantId = tenantId;
            await _userRepository.UpdateAsync(user);

            // 创建会话
            var sessionId = await _sessionManager.CreateSessionAsync(
                user.Id.ToString(),
                user.UserName,
                HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
                HttpContext.Request.Headers["User-Agent"].ToString()
            );

            // 返回登录结果
            var result = new HbtOAuthLoginDto
            {
                AccessToken = sessionId,
                RefreshToken = Guid.NewGuid().ToString("N"),
                ExpiresIn = 3600,
                UserName = user.UserName,
                NickName = user.NickName,
                Avatar = user.Avatar,
                Email = user.Email,
                NeedSelectTenant = false
            };

            return Ok(result);
        }
    }
}