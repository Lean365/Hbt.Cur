using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Lean.Hbt.Domain.Services;
using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.WebApi.Controllers.Identity
{
    /// <summary>
    /// OAuth认证控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HbtOAuthController : ControllerBase
    {
        private readonly IHbtOAuthService _oauthService;
        private readonly IHbtSessionManager _sessionManager;
        private readonly IHbtRepository<HbtUser> _userRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtOAuthController(
            IHbtOAuthService oauthService, 
            IHbtSessionManager sessionManager,
            IHbtRepository<HbtUser> userRepository)
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
            var user = await _userRepository.FirstOrDefaultAsync(u => u.UserName == userInfo.UserName);
            if (user == null)
            {
                user = new HbtUser
                {
                    UserName = userInfo.UserName,
                    NickName = userInfo.NickName,
                    Email = userInfo.Email,
                    Avatar = userInfo.Avatar,
                    UserType = HbtUserType.OAuth,
                    Status = HbtStatus.Normal
                };
                await _userRepository.InsertAsync(user);
            }
            
            // 创建会话
            var session = await _sessionManager.CreateSessionAsync(user.Id.ToString());
            
            // 返回登录结果
            var result = new HbtOAuthLoginDto
            {
                AccessToken = session.SessionId,
                RefreshToken = session.RefreshToken,
                ExpiresIn = session.ExpiresIn,
                UserName = user.UserName,
                NickName = user.NickName,
                Avatar = user.Avatar,
                Email = user.Email
            };
            
            return Ok(result);
        }
    }
} 
