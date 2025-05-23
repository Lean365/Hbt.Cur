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
        private readonly IHbtDeviceIdGenerator _deviceIdGenerator;
        private readonly IOptions<HbtJwtOptions> _jwtOptions;

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
        /// <param name="localization">本地化服务</param>
        /// <param name="logger">日志服务</param>
        /// </summary>
        public HbtAuthController(
            IHbtLoginPolicy loginPolicy,
            IHbtSingleSignOnService singleSignOnService,
            IHbtSignalRUserService signalRUserService,
            IHbtUserService userService,
            IHbtAuthService loginService,
            IHbtRepository<HbtOnlineUser> onlineUserRepository,
            IHbtDeviceIdGenerator deviceIdGenerator,
            IOptions<HbtJwtOptions> jwtOptions,
                        IHbtLocalizationService localization,
            IHbtLogger logger) : base(localization, logger)
        {
            _loginPolicy = loginPolicy;
            _singleSignOnService = singleSignOnService;
            _signalRUserService = signalRUserService;
            _userService = userService;
            _loginService = loginService;
            _onlineUserRepository = onlineUserRepository;
            _deviceIdGenerator = deviceIdGenerator;
            _jwtOptions = jwtOptions;
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
                var user = await _loginService.GetUserByUsernameAsync(loginDto.UserName);
                if (user == null)
                {
                    _logger.Info("[登录检查] 用户不存在，允许登录");
                    return Success(new { canLogin = true, existingDeviceInfo = (string?)null });
                }

                // 检查单点登录状态
                var (canLogin, existingDeviceInfo) = await _singleSignOnService.CheckLoginAsync(
                    user.Id,
                    user.UserName,
                    loginDto.DeviceInfo?.ToString() ?? string.Empty);

                _logger.Info("[登录检查] 检查完成: {Username}, 是否可以登录: {CanLogin}, 现有设备信息: {DeviceInfo}",
                    loginDto.UserName, canLogin, existingDeviceInfo);

                return Success(new { canLogin, existingDeviceInfo });
            }
            catch (Exception ex)
            {
                _logger.Error("[登录检查] 检查过程中发生错误: {Message}", ex.Message);
                return StatusCode(500, new { code = int.Parse(HbtConstants.ErrorCodes.ServerError), msg = "服务器内部错误" });
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
        public async Task<IActionResult> Login([FromBody] HbtAuthDto loginDto)
        {
            _logger.Info("[登录] 开始处理登录请求: {UserName}", loginDto.UserName);

            try
            {
                // 先检查用户是否可以登录
                var checkResult = await CheckLogin(loginDto);
                if (checkResult is OkObjectResult okResult &&
                    okResult.Value is HbtApiResult apiResult &&
                    apiResult.Data is { } data)
                {
                    var canLogin = (bool)data.GetType().GetProperty("canLogin")?.GetValue(data);
                    var existingDeviceInfo = (string?)data.GetType().GetProperty("existingDeviceInfo")?.GetValue(data);

                    _logger.Info("[登录] 检查结果: 是否可以登录={CanLogin}, 现有设备信息={DeviceInfo}",
                        canLogin, existingDeviceInfo);

                    if (!canLogin)
                    {
                        _logger.Warn("[登录] 用户不允许登录: {UserName}", loginDto.UserName);
                        return checkResult;
                    }
                }

                // 获取用户信息
                var user = await _loginService.GetUserByUsernameAsync(loginDto.UserName);
                if (user == null)
                {
                    _logger.Warn("[登录] 用户不存在: {UserName}", loginDto.UserName);
                    return Ok(new HbtApiResult
                    {
                        Code = int.Parse(HbtConstants.ErrorCodes.NotFound),
                        Msg = _localization.L("User.NotFound")
                    });
                }

                // 生成设备ID和连接ID
                var deviceInfoJson = JsonSerializer.Serialize(loginDto.DeviceInfo);
                var (deviceId, connectionId) = _deviceIdGenerator.GenerateIds(deviceInfoJson, user.Id.ToString());

                _logger.Info("[登录] 生成的设备信息: DeviceId={DeviceId}, ConnectionId={ConnectionId}",
                    deviceId, connectionId);

                // 确保设备信息不为空
                if (loginDto.DeviceInfo == null)
                {
                    loginDto.DeviceInfo = new HbtSignalRDevice();
                }

                loginDto.DeviceInfo.DeviceId = deviceId;
                loginDto.DeviceInfo.DeviceToken = connectionId;
                loginDto.DeviceInfo.TenantId = user.TenantId;

                // 验证登录信息
                var loginResult = await _loginService.LoginAsync(loginDto);
                if (loginResult == null)
                {
                    _logger.Warn("[登录] 登录验证失败: {UserName}", loginDto.UserName);
                    return Ok(new HbtApiResult
                    {
                        Code = int.Parse(HbtConstants.ErrorCodes.Unauthorized),
                        Msg = _localization.L("User.InvalidPassword")
                    });
                }

                // 生成登录结果
                var loginResultDto = new HbtLoginResultDto
                {
                    AccessToken = loginResult.AccessToken,
                    RefreshToken = loginResult.RefreshToken,
                    ExpiresIn = _jwtOptions.Value.ExpirationMinutes * 60,
                    UserInfo = loginResult.UserInfo
                };

                _logger.Info("[登录] 登录成功: {UserName}", loginDto.UserName);

                return Ok(new HbtApiResult
                {
                    Code = 200,
                    Msg = _localization.L("User.LoginSuccess"),
                    Data = loginResultDto
                });
            }
            catch (HbtException ex)
            {
                _logger.Error("[登录] 登录失败: {UserName}, {Message}, {Code}", loginDto.UserName, ex.Message, ex.Code);
                return Ok(new HbtApiResult
                {
                    Code = ex.Code,
                    Msg = ex.Message,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                _logger.Error("[登录] 登录异常: {UserName}, {Message}, {StackTrace}, {InnerException}",
                    loginDto.UserName, ex.Message, ex.StackTrace, ex.InnerException?.Message);
                return Ok(new HbtApiResult
                {
                    Code = 500,
                    Msg = _localization.L("Server.Error") + ": " + ex.Message,
                    Data = null
                });
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
            var userId = HttpContext.User.FindFirst("uid")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                _logger.Warn("[用户信息] 未找到用户ID");
                return Unauthorized(new { code = HbtConstants.ErrorCodes.Unauthorized, msg = "未登录" });
            }

            _logger.Info("[用户信息] 开始获取用户信息: UserId={UserId}", userId);
            var result = await _loginService.GetUserInfoAsync(long.Parse(userId));
            _logger.Info("[用户信息] 获取成功: UserId={UserId}", userId);
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
                    Data = new
                    {
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
                Data = new
                {
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
            _logger.Info("正在解锁用户: {Username}", username);
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
            _logger.Info("正在获取用户登录尝试剩余次数: {Username}", username);
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
            _logger.Info("正在获取用户账户锁定剩余时间: {Username}", username);
            var remainingSeconds = await _loginPolicy.GetLockoutRemainingSecondsAsync(username);
            return HbtApiResult<int>.Success(remainingSeconds);
        }
    }
}