//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtAuthController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V1.0.0
// 描述    : 基础认证控制器
//===================================================================

using System.ComponentModel;
using System.Security.Cryptography;
using System.Text.Json;
using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Application.Services.Identity;
using Lean.Hbt.Common.Constants;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.Entities.SignalR;
using Lean.Hbt.Domain.IServices.Security;
using Lean.Hbt.Domain.IServices.SignalR;
using Lean.Hbt.Domain.Repositories;
using Microsoft.Extensions.Options;

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
        private readonly IHbtLoginPolicy _loginPolicy;
        private readonly IHbtSingleSignOnService _singleSignOnService;
        private readonly IHbtSignalRUserService _signalRUserService;
        private readonly IHbtUserService _userService;
        private readonly IHbtAuthService _loginService;
        private readonly IHbtRepository<HbtOnlineUser> _onlineUserRepository;
        private readonly IHbtRepository<HbtUser> _userRepository;
        private readonly IHbtDeviceIdGenerator _deviceIdGenerator;
        private readonly IOptions<HbtJwtOptions> _jwtOptions;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 构造函数
        /// <param name="loginPolicy">登录策略</param>
        /// <param name="singleSignOnService">单点登录服务</param>
        /// <param name="signalRUserService">SignalR用户服务</param>
        /// <param name="userService">用户服务</param>
        /// <param name="loginService">登录服务</param>
        /// <param name="onlineUserRepository">在线用户仓库</param>
        /// <param name="deviceIdGenerator">设备ID生成器</param>
        /// <param name="jwtOptions">JWT配置</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        /// <param name="configuration">配置服务</param>
        /// </summary>
        public HbtAuthController(
            IHbtLoginPolicy loginPolicy,
            IHbtSingleSignOnService singleSignOnService,
            IHbtSignalRUserService signalRUserService,
            IHbtUserService userService,
            IHbtAuthService loginService,
            IHbtRepository<HbtOnlineUser> onlineUserRepository,
            IHbtRepository<HbtUser> userRepository,
            IHbtDeviceIdGenerator deviceIdGenerator,
            IOptions<HbtJwtOptions> jwtOptions,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization,
            IConfiguration configuration) : base(logger, currentUser, localization)
        {
            _loginPolicy = loginPolicy;
            _singleSignOnService = singleSignOnService;
            _signalRUserService = signalRUserService;
            _userService = userService;
            _loginService = loginService;
            _onlineUserRepository = onlineUserRepository;
            _userRepository = userRepository;
            _deviceIdGenerator = deviceIdGenerator;
            _jwtOptions = jwtOptions;
            _configuration = configuration;
        }



        /// <summary>
        /// 检查用户是否可以登录
        /// </summary>
        /// <param name="loginDto">登录信息</param>
        /// <returns>检查结果</returns>
        [HttpPost("check-login")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckLogin([FromBody] HbtAuthDto loginDto)
        {
            try
            {
                _logger.Info("[登录检查] 开始检查用户登录状态: {Username}", loginDto.UserName);

                // 获取用户信息
                var user = await _userRepository.GetFirstAsync(u => u.UserName == loginDto.UserName);
                
                // 生成随机盐值（无论用户是否存在，都返回盐值以防止恶意攻击）
                var randomSalt = GenerateRandomSalt();
                var iterations = 10000; // 默认迭代次数

                if (user == null)
                {
                    _logger.Info("[登录检查] 用户不存在，返回随机盐值");
                    return Success(new { 
                        canLogin = true, 
                        existingDeviceInfo = (string?)null,
                        salt = randomSalt,
                        iterations = iterations
                    });
                }

                // 检查单点登录状态
                var loginResult = await _singleSignOnService.CheckLoginAsync(
                    user.Id,
                    user.UserName,
                    loginDto.DeviceInfo?.ToString() ?? string.Empty);
                
                var canLogin = loginResult.Item1;
                var existingDeviceInfo = loginResult.Item2;

                _logger.Info("[登录检查] 检查完成: {Username}, 是否可以登录: {CanLogin}, 现有设备信息: {DeviceInfo}",
                    loginDto.UserName, canLogin, existingDeviceInfo);

                return Success(new { 
                    canLogin, 
                    existingDeviceInfo,
                    salt = user.Salt,
                    iterations = user.Iterations
                });
            }
            catch (Exception ex)
            {
                _logger.Error("[登录检查] 检查过程中发生错误: {Message}", ex.Message);
                return Error("服务器内部错误", int.Parse(HbtConstants.ErrorCodes.ServerError));
            }
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
        public async Task<IActionResult> LoginAsync([FromBody] HbtAuthDto loginDto)
        {
            try
            {
                _logger.Info("[登录] 开始处理登录请求: UserName={UserName}", loginDto.UserName);

                var result = await _loginService.LoginAsync(loginDto);
                
                _logger.Info("[登录] 登录成功: UserName={UserName}", loginDto.UserName);

                return Success(result);
            }
            catch (HbtException ex)
            {
                _logger.Warn("[登录] 登录失败: UserName={UserName}, Error={Error}", loginDto.UserName, ex.Message);
                return Error(ex.Message, ex.Code);
            }
            catch (Exception ex)
            {
                _logger.Error("[登录] 登录异常: UserName={UserName}", loginDto.UserName, ex);
                return Error("服务器内部错误", 500);
            }
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns>用户信息</returns>
        [HttpGet("info")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetUserInfo()
        {
            _logger.Info("[用户信息] 开始获取用户信息");

            var userId = HttpContext.User.FindFirst("uid")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                _logger.Warn("[用户信息] 未找到用户ID");
                return Error("未登录", int.Parse(HbtConstants.ErrorCodes.Unauthorized));
            }

            // _logger.Info("[用户信息] 开始获取用户信息: UserId={UserId}", userId);
            var result = await _loginService.GetUserInfoAsync(long.Parse(userId));
            // _logger.Info("[用户信息] 获取成功: UserId={UserId}", userId);
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
                _logger.Info("[登出控制器] 开始处理登出请求, 是否是系统重启: {IsSystemRestart}", isSystemRestart);

                var userId = HttpContext.User.FindFirst("uid")?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    _logger.Info("[登出控制器] 未找到用户ID，直接返回成功");
                    return Success();
                }

                // 如果是系统重启导致的登出，直接返回成功
                if (isSystemRestart)
                {
                    _logger.Info("[登出控制器] 系统重启导致的登出，直接返回成功");
                    return Success();
                }

                // 正常登出流程
                _logger.Info("[登出控制器] 用户ID: {UserId}", userId);
                var result = await _loginService.LogoutAsync(long.Parse(userId));

                if (result)
                {
                    _logger.Info("[登出控制器] 登出成功: UserId={UserId}", userId);
                    return Success();
                }
                else
                {
                    _logger.Warn("[登出控制器] 登出失败: UserId={UserId}", userId);
                    return Error(_localization.L("User.LogoutFailed"));
                }
            }
            catch (Exception ex)
            {
                _logger.Error("[登出控制器] 登出过程中发生错误: {Message}", ex.Message);
                return Error("服务器内部错误", int.Parse(HbtConstants.ErrorCodes.ServerError));
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
            try
            {
                var result = await _loginService.RefreshTokenAsync(refreshToken);
                return Success(result);
            }
            catch (HbtException ex)
            {
                _logger.Warn("[刷新令牌] 刷新失败: Error={Error}", ex.Message);
                return Error(ex.Message, ex.Code);
            }
            catch (Exception ex)
            {
                _logger.Error("[刷新令牌] 刷新异常", ex);
                return Error("服务器内部错误", 500);
            }
        }

        /// <summary>
        /// 获取用户盐值
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>用户盐值信息</returns>
        [HttpGet("salt")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSalt([FromQuery] string username)
        {
            try
            {
                _logger.Info("[获取盐值] 开始获取用户盐值: {Username}", username);

                if (string.IsNullOrEmpty(username))
                {
                    return Error("用户名不能为空", 400);
                }

                var saltResult = await _loginService.GetUserSaltAsync(username);
                if (saltResult == null)
                {
                    _logger.Warn("[获取盐值] 用户不存在: {Username}", username);
                    return Error(_localization.L("User.NotFound"), int.Parse(HbtConstants.ErrorCodes.NotFound));
                }

                var (salt, iterations) = saltResult.Value;

                _logger.Info("[获取盐值] 获取成功: {Username}, SaltLength={SaltLength}, Iterations={Iterations}",
                    username, salt.Length, iterations);

                return Success(new { salt, iterations });
            }
            catch (Exception ex)
            {
                _logger.Error("[获取盐值] 获取过程中发生错误: {Message}", ex.Message);
                return Error("服务器内部错误", int.Parse(HbtConstants.ErrorCodes.ServerError));
            }
        }



        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>解锁结果</returns>
        [HttpPost("unlock/{username}")]
        [AllowAnonymous]
        public async Task<IActionResult> UnlockUserAsync(string username)
        {
            try
            {
                _logger.Info("正在解锁用户: {Username}", username);
                await _loginPolicy.RecordSuccessfulLoginAsync(username);
                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.Error("解锁用户失败: {Message}", ex.Message);
                return Error("解锁用户失败", 500);
            }
        }

        /// <summary>
        /// 获取用户登录尝试剩余次数
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>剩余尝试次数</returns>
        [HttpGet("attempts/{username}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRemainingAttemptsAsync(string username)
        {
            try
            {
                _logger.Info("正在获取用户登录尝试剩余次数: {Username}", username);
                var remainingAttempts = await _loginPolicy.GetRemainingAttemptsAsync(username);
                return Success(remainingAttempts);
            }
            catch (Exception ex)
            {
                _logger.Error("获取用户登录尝试剩余次数失败: {Message}", ex.Message);
                return Error("获取用户登录尝试剩余次数失败", 500);
            }
        }

        /// <summary>
        /// 获取账户锁定剩余时间(秒)
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>剩余锁定时间(秒)</returns>
        [HttpGet("lockout/{username}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLockoutRemainingSecondsAsync(string username)
        {
            try
            {
                _logger.Info("正在获取用户账户锁定剩余时间: {Username}", username);
                var remainingSeconds = await _loginPolicy.GetLockoutRemainingSecondsAsync(username);
                return Success(remainingSeconds);
            }
            catch (Exception ex)
            {
                _logger.Error("获取账户锁定剩余时间失败: {Message}", ex.Message);
                return Error("获取账户锁定剩余时间失败", 500);
            }
        }
    }
}