//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtAuthController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V1.0.0
// 描述    : 基础认证控制器
//===================================================================

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Common.Extensions;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Application.Services.Identity;
using Lean.Hbt.Application.Services;
using Lean.Hbt.Infrastructure.Security;
using System.Security.Claims;
using Lean.Hbt.Domain.IServices.Admin;
using System.Security.Cryptography;
using System;
using Lean.Hbt.Common.Constants;
using System.ComponentModel;
using Lean.Hbt.Domain.IServices.Security;

namespace Lean.Hbt.WebApi.Controllers.Identity
{
    /// <summary>
    /// 基础认证控制器
    /// </summary>
    /// <remarks>
    /// 本控制器负责处理:
    /// 1. 用户名密码登录
    /// 2. 用户登出
    /// 3. 刷新访问令牌
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    [ApiModule("identity", "身份认证")]
    public class HbtAuthController : HbtBaseController
    {
        private readonly IHbtLoginService _loginService;
        private readonly ILogger<HbtAuthController> _logger;
        private readonly IHbtLoginPolicy _loginPolicy;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtAuthController(
            IHbtLoginService loginService,
            IHbtLocalizationService localization,
            IHbtLoginPolicy loginPolicy,
            ILogger<HbtAuthController> logger) : base(localization)
        {
            _loginService = loginService;
            _logger = logger;
            _loginPolicy = loginPolicy;
        }

        /// <summary>
        /// 生成随机盐值
        /// </summary>
        private string GenerateRandomSalt()
        {
            var saltBytes = new byte[32]; // 32字节的盐值
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginDto">登录信息</param>
        /// <returns>登录结果</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] HbtLoginDto loginDto)
        {
            try 
            {
                _logger.LogInformation("[登录控制器] 开始处理登录请求: {Username}", loginDto.UserName);
                var result = await _loginService.LoginAsync(loginDto);
                _logger.LogInformation("[登录控制器] 登录成功: {Username}", loginDto.UserName);
                return Success(result);
            }
            catch (HbtException ex)
            {
                _logger.LogWarning("[登录控制器] 登录失败: {Username}, 错误代码: {Code}, 消息: {Message}", 
                    loginDto.UserName, ex.Code, ex.Message);
                
                // 根据错误类型返回不同的状态码
                if (ex.Code == int.Parse(HbtConstants.ErrorCodes.ValidationFailed))
                {
                    var remainingSeconds = ex.Data["RemainingSeconds"] as int?;
                    var failedAttempts = ex.Data["FailedAttempts"] as int?;
                    var remainingAttempts = ex.Data["RemainingAttempts"] as int?;
                    
                    // 如果有剩余等待时间，说明账号被锁定
                    if (remainingSeconds.HasValue && remainingSeconds.Value > 0)
                    {
                        var minutes = Math.Ceiling(remainingSeconds.Value / 60.0);
                        var isAdmin = loginDto.UserName.ToLower() == "admin";
                        var lockoutMinutes = isAdmin ? 30 : 10;
                        
                        var message = $"账号已被锁定，请等待{minutes}分钟后再试\n{(isAdmin ? "管理员" : "普通用户")}账号锁定时间为{lockoutMinutes}分钟";
                        
                        return StatusCode(429, new { 
                            code = ex.Code, 
                            msg = message,
                            remainingSeconds = remainingSeconds.Value,
                            failedAttempts = failedAttempts ?? 1,
                            remainingAttempts = remainingAttempts ?? 0
                        });
                    }
                    
                    // 需要验证码但未提供
                    if (string.IsNullOrEmpty(loginDto.CaptchaToken))
                    {
                        return StatusCode(429, new { 
                            code = ex.Code, 
                            msg = "请完成验证码验证",
                            remainingSeconds = 0,
                            failedAttempts = failedAttempts ?? 1,
                            remainingAttempts = remainingAttempts ?? 0,
                            needCaptcha = true
                        });
                    }
                    
                    // 验证码验证失败
                    return StatusCode(429, new { 
                        code = ex.Code, 
                        msg = ex.Message,
                        remainingSeconds = 0,
                        failedAttempts = failedAttempts ?? 1,
                        remainingAttempts = remainingAttempts ?? 0,
                        needCaptcha = true
                    });
                }
                
                // 用户名或密码错误返回401
                if (ex.Code == int.Parse(HbtConstants.ErrorCodes.Unauthorized))
                {
                    return StatusCode(401, new { code = ex.Code, msg = ex.Message });
                }
                
                // 用户或租户被禁用返回403
                if (ex.Code == int.Parse(HbtConstants.ErrorCodes.Forbidden))
                {
                    return StatusCode(403, new { code = ex.Code, msg = ex.Message });
                }
                
                // 其他业务错误返回400
                return BadRequest(new { code = ex.Code, msg = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[登录控制器] 登录过程中发生错误: {Message}", ex.Message);
                return StatusCode(500, new { code = int.Parse(HbtConstants.ErrorCodes.ServerError), msg = "服务器内部错误" });
            }
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns>用户信息</returns>
        [HttpGet("info")]
        [Authorize]
        public async Task<IActionResult> GetUserInfo()
        {
            var userId = HttpContext.User.FindFirst("user_id")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("[用户信息] 未找到用户ID");
                return Unauthorized(new { code = HbtConstants.ErrorCodes.Unauthorized, msg = "未登录" });
            }

            _logger.LogInformation("[用户信息] 开始获取用户信息: UserId={UserId}", userId);
            var result = await _loginService.GetUserInfoAsync(long.Parse(userId));
            _logger.LogInformation("[用户信息] 获取成功: UserId={UserId}", userId);
            return Success(result);
        }

        /// <summary>
        /// 用户登出
        /// </summary>
        /// <param name="isSystemRestart">是否是系统重启导致的登出</param>
        /// <returns>操作结果</returns>
        [HttpPost("logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout([FromQuery] bool isSystemRestart = false)
        {
            try
            {
                _logger.LogInformation("[登出控制器] 开始处理登出请求, 是否是系统重启: {IsSystemRestart}", isSystemRestart);
                
                var userId = HttpContext.User.FindFirst("user_id")?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogInformation("[登出控制器] 未找到用户ID，直接返回成功");
                    return Success();
                }

                // 如果是系统重启导致的登出，直接返回成功
                if (isSystemRestart)
                {
                    _logger.LogInformation("[登出控制器] 系统重启导致的登出，直接返回成功");
                    return Success();
                }

                // 正常登出流程
                _logger.LogInformation("[登出控制器] 用户ID: {UserId}", userId);
                var result = await _loginService.LogoutAsync(long.Parse(userId));
                
                if (result)
                {
                    _logger.LogInformation("[登出控制器] 登出成功: UserId={UserId}", userId);
                    return Success();
                }
                else
                {
                    _logger.LogWarning("[登出控制器] 登出失败: UserId={UserId}", userId);
                    return Error(_localization.L("User.LogoutFailed"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[登出控制器] 登出过程中发生错误: {Message}", ex.Message);
                return StatusCode(500, new { code = int.Parse(HbtConstants.ErrorCodes.ServerError), msg = "服务器内部错误" });
            }
        }

        /// <summary>
        /// 刷新访问令牌
        /// </summary>
        /// <param name="refreshToken">刷新令牌</param>
        /// <returns>新的访问令牌</returns>
        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var result = await _loginService.RefreshTokenAsync(refreshToken);
            return Success(result);
        }

        /// <summary>
        /// 获取用户盐值
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>盐值和迭代次数</returns>
        [HttpGet("salt")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSalt([FromQuery] string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return Error(_localization.L("User.UsernameRequired"));
            }

            var result = await _loginService.GetUserSaltAsync(username);
            if (!result.HasValue)
            {
                // 为了安全考虑，即使用户不存在也返回一个随机盐值
                return Ok(new HbtApiResult<object>
                {
                    Code = 200,
                    Msg = "获取盐值成功",
                    Data = new { 
                        salt = GenerateRandomSalt(),
                        iterations = 100000 
                    }
                });
            }

            var (salt, iterations) = result.Value;
            return Ok(new HbtApiResult<object>
            {
                Code = 200,
                Msg = "获取盐值成功",
                Data = new { 
                    salt = salt,
                    iterations = iterations 
                }
            });
        }

        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>解锁结果</returns>
        [HttpPost("unlock/{username}")]
        [Description("解锁用户")]
        public async Task<HbtApiResult<bool>> UnlockUserAsync(string username)
        {
            _logger.LogInformation("正在解锁用户: {Username}", username);
            await _loginPolicy.RecordSuccessfulLoginAsync(username);
            return HbtApiResult<bool>.Success(true);
        }

        /// <summary>
        /// 获取用户登录尝试剩余次数
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>剩余尝试次数</returns>
        [HttpGet("attempts/{username}")]
        [Description("获取用户登录尝试剩余次数")]
        [AllowAnonymous]
        public async Task<HbtApiResult<int>> GetRemainingAttemptsAsync(string username)
        {
            _logger.LogInformation("正在获取用户登录尝试剩余次数: {Username}", username);
            var remainingAttempts = await _loginPolicy.GetRemainingAttemptsAsync(username);
            return HbtApiResult<int>.Success(remainingAttempts);
        }

        /// <summary>
        /// 获取账户锁定剩余时间(秒)
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>剩余锁定时间(秒)</returns>
        [HttpGet("lockout/{username}")]
        [Description("获取账户锁定剩余时间")]
        [AllowAnonymous]
        public async Task<HbtApiResult<int>> GetLockoutRemainingSecondsAsync(string username)
        {
            _logger.LogInformation("正在获取用户账户锁定剩余时间: {Username}", username);
            var remainingSeconds = await _loginPolicy.GetLockoutRemainingSecondsAsync(username);
            return HbtApiResult<int>.Success(remainingSeconds);
        }
    }
} 