//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 登录服务实现 - 使用仓储工厂模式
//===================================================================

using System.Net;
using System.Text.Json;
using Lean.Hbt.Common.Constants;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.IServices.Caching;
using Lean.Hbt.Domain.IServices.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Lean.Hbt.Application.Services.Identity;

/// <summary>
/// 登录服务实现
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// 更新: 2024-12-01 - 使用仓储工厂模式支持多库
/// </remarks>
public class HbtAuthService : HbtBaseService, IHbtAuthService
{
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly IHbtRepositoryFactory _repositoryFactory;
    private readonly IHbtCaptchaService _captchaService;
    private readonly IHbtJwtHandler _jwtHandler;
    private readonly IHbtMemoryCache _cache;
    private readonly HbtJwtOptions _jwtOptions;
    private readonly IHbtDeviceIdGenerator _deviceIdGenerator;
    private readonly IConfiguration _configuration;

    private IHbtRepository<HbtUser> UserRepository => _repositoryFactory.GetAuthRepository<HbtUser>();
    private IHbtRepository<HbtLoginLog> LoginLogRepository => _repositoryFactory.GetAuthRepository<HbtLoginLog>();
    private IHbtRepository<HbtOnlineUser> OnlineUserRepository => _repositoryFactory.GetAuthRepository<HbtOnlineUser>();
    private IHbtRepository<HbtLoginEnvLog> LoginEnvLogRepository => _repositoryFactory.GetAuthRepository<HbtLoginEnvLog>();
    private IHbtRepository<HbtLoginDevLog> LoginDevLogRepository => _repositoryFactory.GetAuthRepository<HbtLoginDevLog>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger">日志服务</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="captchaService">验证码服务</param>
    /// <param name="jwtHandler">JWT处理器</param>
    /// <param name="cache">缓存服务</param>
    /// <param name="jwtOptions">JWT配置选项</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    /// <param name="deviceIdGenerator">设备ID生成器</param>
    /// <param name="configuration">配置服务</param>

    public HbtAuthService(
        IHbtLogger logger,
        IHttpContextAccessor httpContextAccessor,
        IHbtRepositoryFactory repositoryFactory,
        IHbtCaptchaService captchaService,
        IHbtJwtHandler jwtHandler,
        IHbtMemoryCache cache,
        IOptions<HbtJwtOptions> jwtOptions,
        IHbtCurrentUser currentUser,
        IHbtLocalizationService localization,
        IHbtDeviceIdGenerator deviceIdGenerator,
        IConfiguration configuration) : base(logger, httpContextAccessor, currentUser, localization)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        _captchaService = captchaService;
        _jwtHandler = jwtHandler;
        _cache = cache;
        _jwtOptions = jwtOptions.Value;
        _deviceIdGenerator = deviceIdGenerator;
        _configuration = configuration;
    }

    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="loginDto">登录信息</param>
    /// <returns>登录结果</returns>
    public async Task<HbtLoginResultDto> LoginAsync(HbtAuthDto loginDto)
    {
        _logger.Info(L("Identity.Auth.LoginStart", loginDto.UserName));

        try
        {
            // 第一步：验证用户是否存在
            // 通过用户名查询用户信息，如果用户不存在则直接返回错误
            var user = await GetUserAsync(loginDto.UserName);
            if (user == null)
            {
                _logger.Warn(L("Identity.Auth.UserNotFound", loginDto.UserName));
                throw new HbtException(L("Identity.User.InvalidCredentials"), HbtConstants.ErrorCodes.Unauthorized);
            }

            // 第二步：检查是否是页面刷新
            // 如果是页面刷新，则直接使用现有会话
            bool isPageRefresh = false;
            var onlineUser = await OnlineUserRepository.GetFirstAsync(u => u.UserId == user.Id);
            if (onlineUser != null && onlineUser.OnlineStatus == 1)
            {
                isPageRefresh = true;
                _logger.Info(L("Identity.Auth.ExistingUserLogin", user.Id));
            }

            // 第五步：验证用户状态
            // 检查用户是否被禁用
            _logger.Info(L("Identity.Auth.UserValidationStart", loginDto.UserName));
            if (user.Status != 0)
            {
                throw new HbtException(L("Identity.User.Disabled"));
            }

            // 第六步：验证密码
            // 使用盐值和迭代次数验证密码
            _logger.Info(L("Identity.Auth.PasswordValidationStart", user.Id, loginDto.Password?.Length ?? 0));
            var passwordValid = HbtPasswordUtils.VerifyHash(loginDto.Password, user.Password, user.Salt, user.Iterations);
            _logger.Info(L("Identity.Auth.PasswordValidationResult", passwordValid ? "Success" : "Failed"));

            if (!passwordValid)
            {
                // 密码验证失败，更新登录失败记录
                var loginLog1 = new HbtLoginLog
                {
                    UserId = user.Id,
                    UserName = loginDto.UserName,
                    LoginType = HbtLoginType.Password,
                    LoginStatus = HbtLoginStatus.Failed,
                    LoginTime = DateTime.Now,
                    IpAddress = GetClientIpAddress(),
                    UserAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString(),
                    DeviceInfo = loginDto.DeviceInfo,
                    LoginSuccess = 0,
                    LoginMessage = "密码错误",
                    CreateBy = loginDto.UserName,
                    CreateTime = DateTime.Now,
                    UpdateBy = loginDto.UserName,
                    UpdateTime = DateTime.Now
                };
                loginLog1 = HbtStringHelper.EnsureEntityStringLength(loginLog1);
                await LoginLogRepository.CreateAsync(loginLog1);

                throw new HbtException(L("Identity.User.InvalidCredentials"), HbtConstants.ErrorCodes.Unauthorized);
            }

            // 第八步：验证验证码（如果需要）
            // 如果提供了验证码，则进行验证
            if (!string.IsNullOrEmpty(loginDto.CaptchaToken))
            {
                _logger.Info(L("Identity.Auth.CaptchaValidationStart", loginDto.CaptchaToken, loginDto.CaptchaOffset));
                var captchaValid = await _captchaService.ValidateSliderAsync(loginDto.CaptchaToken, loginDto.CaptchaOffset);
                _logger.Info(L("Identity.Auth.CaptchaValidationResult", captchaValid ? "Success" : "Failed"));

                if (!captchaValid)
                {
                    // 验证码验证失败，记录日志
                    var loginLog2 = new HbtLoginLog
                    {
                        UserId = user.Id,
                        UserName = loginDto.UserName,
                        LoginType = HbtLoginType.Password,
                        LoginStatus = HbtLoginStatus.Failed,
                        LoginTime = DateTime.Now,
                        IpAddress = GetClientIpAddress(),
                        UserAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString(),
                        DeviceInfo = loginDto.DeviceInfo,
                        LoginSuccess = 0,
                        LoginMessage = "验证码错误",
                        CreateBy = loginDto.UserName,
                        CreateTime = DateTime.Now,
                        UpdateBy = loginDto.UserName,
                        UpdateTime = DateTime.Now
                    };
                    loginLog2 = HbtStringHelper.EnsureEntityStringLength(loginLog2);
                    await LoginLogRepository.CreateAsync(loginLog2);

                    throw new HbtException(L("Identity.User.InvalidCaptcha"), HbtConstants.ErrorCodes.InvalidCaptcha);
                }
            }

            // 第七步：获取用户权限
            // 获取用户在租户中的角色和权限
            _logger.Info(L("Identity.Auth.GetUserRolesAndPermissions", user.Id));
            var roles = await UserRepository.GetUserRolesAsync(user.Id);
            var permissions = await UserRepository.GetUserPermissionsAsync(user.Id);
            _logger.Info(L("Identity.Auth.UserRolesAndPermissionsResult", roles.Count, permissions.Count));

            // 第八步：生成令牌
            // 生成访问令牌和刷新令牌
            _logger.Info(L("Identity.Auth.GenerateTokens"));
            var accessToken = await _jwtHandler.GenerateAccessTokenAsync(user, roles.ToArray(), permissions.ToArray());
            var refreshToken = await _jwtHandler.GenerateRefreshTokenAsync();
            _logger.Info(L("Identity.Auth.TokensGenerated", accessToken.Length, refreshToken));

            // 第九步：缓存刷新令牌
            // 将刷新令牌存入缓存，用于后续的令牌刷新
            _logger.Info(L("Identity.Auth.CacheRefreshToken", refreshToken));
            await _cache.SetAsync($"refresh_token:{refreshToken}", user.Id.ToString(),
                TimeSpan.FromDays(_jwtOptions.RefreshTokenExpirationDays));
            _logger.Info(L("Identity.Auth.RefreshTokenCached"));

            // 第十步：生成设备ID和环境ID
            // 根据设备信息和环境信息生成唯一标识
            var deviceInfoJson = JsonSerializer.Serialize(loginDto.DeviceInfo);
            var environmentInfoJson = JsonSerializer.Serialize(loginDto.EnvironmentInfo);
            var deviceId = _deviceIdGenerator.GenerateDeviceId(deviceInfoJson, user.Id.ToString());
            var environmentId = _deviceIdGenerator.GenerateEnvironmentId(environmentInfoJson, user.Id.ToString());
            _logger.Info(L("Identity.Auth.GeneratedIds", deviceId, environmentId));

            // 第十一步：记录登录日志
            // 记录完整的登录信息
            _logger.Info(L("Identity.Auth.ProcessLoginLog"));
            HbtLoginLog loginLog;
            loginLog = await LoginLogAsync(user.Id, user.UserName,
                loginDto.DeviceInfo, loginDto.EnvironmentInfo, DateTime.Now, deviceId, environmentId);
            _logger.Info(L("Identity.Auth.LoginLogProcessed"));

            // 第十二步：记录设备日志
            // 记录用户的设备信息
            _logger.Info(L("Identity.Auth.ProcessDeviceInfo"));
            var loginDevLog = await LoginDevLogAsync(user.Id, loginDto.DeviceInfo, DateTime.Now, deviceId);
            _logger.Info(L("Identity.Auth.DeviceInfoProcessed"));

            // 第十三步：记录环境日志
            // 记录用户的登录环境信息
            _logger.Info(L("Identity.Auth.ProcessLoginInfo"));
            var loginEnvLog = await LoginEnvLogAsync(user.Id, loginDto.DeviceInfo, DateTime.Now, !isPageRefresh, environmentId, deviceId);
            _logger.Info(L("Identity.Auth.LoginInfoProcessed"));

            _logger.Info(L("Identity.Auth.LoginSuccess", user.Id, user.UserName));

            // 第十四步：记录租户日志（仅在启用多租户时）
            // 租户日志记录已移除

            // 第十五步：返回登录结果
            // 返回包含令牌和用户信息的登录结果
            return new HbtLoginResultDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = _jwtOptions.ExpirationMinutes * 60,
                UserInfo = new HbtUserInfoDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    NickName = user.NickName ?? string.Empty,
                    EnglishName = user.EnglishName ?? string.Empty,
                    FullName = user.FullName ?? string.Empty,
                    RealName = user.RealName ?? string.Empty,
                    UserType = user.UserType,
                    Avatar = user.Avatar ?? string.Empty,
                    Roles = roles,
                    Permissions = permissions
                }
            };
        }
        catch (HbtException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.Error(L("Identity.Auth.LoginError", ex.Message), ex);
            throw new HbtException(L("Identity.Auth.ServerError"), HbtConstants.ErrorCodes.ServerError, ex);
        }
    }

    /// <summary>
    /// 刷新令牌
    /// </summary>
    public async Task<HbtLoginResultDto> RefreshTokenAsync(string refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
        {
            _logger.Warn("刷新令牌为空");
            throw new HbtException(L("Identity.Auth.InvalidRefreshToken"));
        }

        _logger.Info("开始刷新令牌: Token={Token}", refreshToken.Substring(0, Math.Min(10, refreshToken.Length)) + "...");

        // 0. 检查缓存服务是否可用
        try
        {
            var testKey = "refresh_token_test";
            await _cache.SetAsync(testKey, "test", TimeSpan.FromSeconds(10));
            var testValue = await _cache.GetAsync<string>(testKey);
            if (testValue != "test")
            {
                _logger.Error("缓存服务异常：写入和读取的值不匹配");
                throw new HbtException("缓存服务异常，请稍后重试");
            }
            await _cache.RemoveAsync(testKey);
            _logger.Info("缓存服务检查通过");
        }
        catch (Exception ex)
        {
            _logger.Error("缓存服务检查失败: {Message}", ex.Message);
            throw new HbtException("缓存服务不可用，请稍后重试");
        }

        // 1. 验证刷新令牌 - 使用异步方法
        var cacheKey = $"refresh_token:{refreshToken}";
        _logger.Info("尝试从缓存获取刷新令牌: CacheKey={CacheKey}", cacheKey);

        var userId = await _cache.GetAsync<string>(cacheKey);
        if (string.IsNullOrEmpty(userId))
        {
            _logger.Warn("刷新令牌在缓存中不存在或已过期: CacheKey={CacheKey}", cacheKey);
            throw new HbtException(L("Identity.Auth.InvalidRefreshToken"));
        }

        _logger.Info("从缓存获取到用户ID: UserId={UserId}", userId);

        // 2. 获取用户信息
        var user = await UserRepository.GetByIdAsync(long.Parse(userId));
        if (user == null)
        {
            _logger.Warn("用户不存在: UserId={UserId}", userId);
            throw new HbtException(L("Identity.User.NotFound"));
        }
        if (user.Status != 0)
        {
            _logger.Warn("用户已被禁用: UserId={UserId}, Status={Status}", userId, user.Status);
            throw new HbtException(L("Identity.User.Disabled"));
        }

        _logger.Info("用户验证通过: UserId={UserId}, UserName={UserName}", user.Id, user.UserName);

        // 4. 获取用户角色和权限
        var roles = await UserRepository.GetUserRolesAsync(user.Id);
        var permissions = await UserRepository.GetUserPermissionsAsync(user.Id);

        _logger.Info("获取用户角色和权限完成: RolesCount={RolesCount}, PermissionsCount={PermissionsCount}",
            roles.Count, permissions.Count);

        // 5. 生成新令牌
        var accessToken = await _jwtHandler.GenerateAccessTokenAsync(user, roles.ToArray(), permissions.ToArray());
        var newRefreshToken = await _jwtHandler.GenerateRefreshTokenAsync();

        _logger.Info("新令牌生成完成: AccessTokenLength={AccessTokenLength}, NewRefreshToken={NewRefreshToken}",
            accessToken.Length, newRefreshToken.Substring(0, Math.Min(10, newRefreshToken.Length)) + "...");

        // 6. 更新缓存 - 先设置新令牌，再删除旧令牌，避免竞态条件
        var newCacheKey = $"refresh_token:{newRefreshToken}";
        await _cache.SetAsync(newCacheKey, userId, TimeSpan.FromDays(_jwtOptions.RefreshTokenExpirationDays));
        await _cache.RemoveAsync(cacheKey);

        _logger.Info("缓存更新完成: 新令牌已缓存，旧令牌已删除");

        // 7. 返回登录结果
        return new HbtLoginResultDto
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken,
            ExpiresIn = _jwtOptions.ExpirationMinutes * 60,
            UserInfo = new HbtUserInfoDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                NickName = user.NickName ?? string.Empty,
                EnglishName = user.EnglishName ?? string.Empty,
                FullName = user.FullName ?? string.Empty,
                RealName = user.RealName ?? string.Empty,
                UserType = user.UserType,
                Avatar = user.Avatar ?? string.Empty,
                Roles = roles,
                Permissions = permissions
            }
        };
    }

    /// <summary>
    /// 用户登出
    /// </summary>
    public async Task<bool> LogoutAsync(long userId)
    {
        try
        {
            _logger.Info("开始处理用户登出: UserId={0}", userId);
            var now = DateTime.Now;

            // 获取用户信息
            var user = await UserRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException(L("Identity.User.NotFound", userId));
            }

            // 获取当前用户的环境信息
            var loginEnvLog = await LoginEnvLogRepository.GetFirstAsync(x => x.UserId == userId);
            if (loginEnvLog != null)
            {
                // 获取当前用户的设备信息
                var loginDevLog = await LoginDevLogRepository.GetFirstAsync(x => x.UserId == userId);
                if (loginDevLog != null)
                {
                    // 更新设备状态为离线
                    loginDevLog.DeviceStatus = (int)HbtDeviceStatus.Offline;
                    loginDevLog.LastOfflineTime = now;
                    await LoginDevLogRepository.UpdateAsync(loginDevLog);
                    _logger.Info("已更新设备状态为离线: UserId={0}, DeviceId={1}", userId, loginDevLog.DeviceId);

                    // 创建离线设备信息用于记录日志
                    var deviceInfo = new HbtSignalRDevice
                    {
                        DeviceId = loginDevLog.DeviceId,
                        DeviceToken = loginDevLog.DeviceToken,
                        DeviceType = (HbtDeviceType)loginDevLog.DeviceType,
                        DeviceName = loginDevLog.DeviceName ?? "离线设备",
                        IpAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0",
                        BrowserType = (int?)loginEnvLog.LastLoginBrowser,
                        OsType = (int?)loginEnvLog.LastLoginOs,
                        DeviceModel = loginDevLog.DeviceModel
                    };

                    var environmentInfo = new HbtSignalREnvironment
                    {
                        EnvironmentId = loginEnvLog.EnvironmentId,
                        Timezone = loginEnvLog.TimeZone,
                        Language = loginEnvLog.Language ?? "zh-CN",
                        BrowserInfo = loginEnvLog.LastLoginBrowser.ToString(),
                        OsInfo = loginEnvLog.LastLoginOs.ToString()
                    };

                    // 记录登出日志
                    await LoginLogAsync(userId, user.UserName, deviceInfo, environmentInfo, now, loginDevLog.DeviceId, loginEnvLog.EnvironmentId);
                }
            }

            // 删除在线用户记录
            var onlineUser = await OnlineUserRepository.GetFirstAsync(u => u.UserId == userId);
            if (onlineUser != null)
            {
                onlineUser.OnlineStatus = 1; // 离线
                onlineUser.LastActivity = now;
                onlineUser.UpdateTime = now;
                await OnlineUserRepository.UpdateAsync(onlineUser);
                _logger.Info("已更新用户状态为离线: UserId={0}, ConnectionId={1}", userId, onlineUser.ConnectionId);
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.Error("处理用户登出时发生错误: UserId={0}", new[] { userId.ToString() }, ex);
            return false;
        }
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>

    public async Task<HbtUserInfoDto> GetUserInfoAsync(long userId)
    {
        // 获取用户基本信息
        var user = await UserRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new HbtException(L("Identity.User.NotFound", userId), HbtConstants.ErrorCodes.NotFound);
        }

        // 获取用户角色和权限
        var roles = await UserRepository.GetUserRolesAsync(userId);
        var permissions = await UserRepository.GetUserPermissionsAsync(userId);

        // 构建用户信息（不包含租户信息）
        var userInfo = new HbtUserInfoDto
        {
            UserId = user.Id,
            UserName = user.UserName,
            NickName = user.NickName ?? string.Empty,
            EnglishName = user.EnglishName ?? string.Empty,
            FullName = user.FullName ?? string.Empty,
            RealName = user.RealName ?? string.Empty,
            UserType = user.UserType,
            Avatar = user.Avatar ?? string.Empty,
            Roles = roles,
            Permissions = permissions,
        };

        _logger.Info("多租户未启用，获取用户信息成功: UserId={UserId}", userId);

        return userInfo;
    }


    /// <summary>
    /// 获取用户盐值
    /// </summary>
    /// <param name="username">用户名</param>
    /// <returns>用户盐值信息</returns>
    public async Task<(string Salt, int Iterations)?> GetUserSaltAsync(string username)
    {
        if (string.IsNullOrEmpty(username))
            return null;

        var user = await UserRepository.GetFirstAsync(x => x.UserName == username);
        if (user == null)
            return null;

        return (user.Salt, user.Iterations);
    }



    /// <summary>
    /// 更新登录失败信息
    /// </summary>
    private async Task UpdateLoginFailedAsync(long userId, string? deviceExtendId, string? loginExtendId)
    {
        var now = DateTime.Now;
        var user = await UserRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new InvalidOperationException(L("Identity.User.NotFound", userId));
        }

        // 获取当前IP地址
        var ipAddress = GetClientIpAddress();
        var location = await HbtIpLocationUtils.GetLocationAsync(ipAddress);

        var loginExtend = await LoginEnvLogRepository.GetFirstAsync(x => x.UserId == userId);
        if (loginExtend == null)
        {
            loginExtend = new HbtLoginEnvLog
            {
                UserId = userId,
                EnvironmentId = loginExtendId,
                LoginCount = 1,
                LoginStatus = (int)HbtLoginStatus.Failed,
                FirstLoginTime = now,
                FirstLoginIp = ipAddress,
                FirstLoginLocation = location,
                LastLoginTime = now,
                LastLoginIp = ipAddress,
                LastLoginLocation = location,
                CreateBy = user.UserName,
                CreateTime = now,
                UpdateBy = user.UserName,
                UpdateTime = now
            };
            await LoginEnvLogRepository.CreateAsync(loginExtend);
        }
        else
        {
            loginExtend.LoginCount++;
            loginExtend.LoginStatus = (int)HbtLoginStatus.Failed;
            loginExtend.LastLoginTime = now;
            loginExtend.LastLoginIp = ipAddress;
            loginExtend.LastLoginLocation = location;
            loginExtend.UpdateBy = userId.ToString();
            loginExtend.UpdateTime = now;
            await LoginEnvLogRepository.UpdateAsync(loginExtend);
        }
    }

    /// <summary>
    /// 处理登录日志（新增或更新）
    /// </summary>
    private async Task<HbtLoginLog> LoginLogAsync(long userId, string userName, HbtSignalRDevice deviceInfo, HbtSignalREnvironment environmentInfo, DateTime now, string deviceId, string? environmentId)
    {
        _logger.Info("创建新的登录日志: UserId={0}", userId);
        HbtLoginLog loginLog;
        // 获取IP地址和位置信息
        var backendIpAddress = GetClientIpAddress();
        var frontendIpAddress = deviceInfo.IpAddress;

        // 智能选择IP地址：优先使用前端获取的公网IP，否则使用后端IP
        var finalIpAddress = !string.IsNullOrEmpty(frontendIpAddress) &&
                            frontendIpAddress != "unknown" &&
                            frontendIpAddress != "0.0.0.0" &&
                            frontendIpAddress != "localhost" &&
                            !frontendIpAddress.Contains("127.0.0.1") &&
                            !IsPrivateIp(frontendIpAddress) // 如果是公网IP，优先使用
                            ? frontendIpAddress : backendIpAddress;

        // 优先使用前端位置信息，否则根据IP获取
        var finalLocation = !string.IsNullOrEmpty(deviceInfo.Location) &&
                           deviceInfo.Location != "unknown"
                           ? deviceInfo.Location
                           : await HbtIpLocationUtils.GetLocationAsync(finalIpAddress);

        loginLog = new HbtLoginLog
        {
            UserId = userId,
            UserName = userName,
            LoginType = HbtLoginType.Password,
            LoginStatus = HbtLoginStatus.Success,
            LoginTime = now,
            IpAddress = finalIpAddress,
            Location = finalLocation,
            UserAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString(),
            DeviceInfo = deviceInfo,
            DeviceId = deviceId?.ToString(),
            EnvironmentInfo = environmentInfo,
            EnvironmentId = environmentId,
            LoginSuccess = 1,
            LoginMessage = $"用户 {userName} 登录成功",
            CreateBy = userName,
            CreateTime = now,
            UpdateBy = userName,
            UpdateTime = now
        };

        loginLog = HbtStringHelper.EnsureEntityStringLength(loginLog);
        await LoginLogRepository.CreateAsync(loginLog);
        _logger.Info("登录日志创建完成");

        return loginLog;
    }

    /// <summary>
    /// 处理登录环境日志信息（新增或更新）
    /// </summary>
    private async Task<HbtLoginEnvLog> LoginEnvLogAsync(long userId, HbtSignalRDevice deviceInfo, DateTime now, bool incrementLoginCount = true, string environmentId = "", string deviceId = "")
    {
        // 获取用户名
        var user = await UserRepository.GetByIdAsync(userId);
        var userName = user?.UserName ?? throw new InvalidOperationException($"用户 {userId} 不存在");

        // 查询现有登录环境日志信息
        var loginExtend = await LoginEnvLogRepository.GetFirstAsync(x => x.UserId == userId);

        if (loginExtend == null)
        {
            _logger.Info("创建新的登录环境日志信息: UserId={0}", userId);
            // 获取IP地址和位置信息
            var backendIpAddress = GetClientIpAddress();
            var frontendIpAddress = deviceInfo.IpAddress;

            // 智能选择IP地址：优先使用前端获取的公网IP，否则使用后端IP
            var finalIpAddress = !string.IsNullOrEmpty(frontendIpAddress) &&
                                frontendIpAddress != "unknown" &&
                                frontendIpAddress != "0.0.0.0" &&
                                frontendIpAddress != "localhost" &&
                                !frontendIpAddress.Contains("127.0.0.1") &&
                                !IsPrivateIp(frontendIpAddress) // 如果是公网IP，优先使用
                                ? frontendIpAddress : backendIpAddress;

            // 优先使用前端位置信息，否则根据IP获取
            var finalLocation = !string.IsNullOrEmpty(deviceInfo.Location) &&
                               deviceInfo.Location != "unknown"
                               ? deviceInfo.Location
                               : await HbtIpLocationUtils.GetLocationAsync(finalIpAddress);

            loginExtend = new HbtLoginEnvLog
            {
                UserId = userId,
                EnvironmentId = environmentId ?? "unknown",
                LoginCount = 1,
                LoginStatus = (int)HbtLoginStatus.Success,
                LoginType = (int)HbtLoginType.Password,
                FirstLoginTime = now,
                FirstLoginIp = finalIpAddress,
                FirstLoginLocation = finalLocation,
                FirstLoginDeviceId = !string.IsNullOrEmpty(deviceId) ? deviceId : (deviceInfo.DeviceId ?? "unknown"),
                FirstLoginDeviceType = (int)deviceInfo.DeviceType,
                FirstLoginBrowser = (int)deviceInfo.BrowserType,
                FirstLoginOs = (int)deviceInfo.OsType,
                LastLoginTime = now,
                LastLoginIp = finalIpAddress,
                LastLoginLocation = finalLocation,
                LastLoginDeviceId = !string.IsNullOrEmpty(deviceId) ? deviceId : (deviceInfo.DeviceId ?? "unknown"),
                LastLoginDeviceType = (int)deviceInfo.DeviceType,
                LastLoginBrowser = (int)deviceInfo.BrowserType,
                LastLoginOs = (int)deviceInfo.OsType,
                ContinuousLoginDays = 1,
                Language = "zh-CN",
                CreateBy = userName,
                CreateTime = now,
                UpdateBy = userName,
                UpdateTime = now
            };

            loginExtend = HbtStringHelper.EnsureEntityStringLength(loginExtend);
            await LoginEnvLogRepository.CreateAsync(loginExtend);
            _logger.Info("登录环境日志信息创建完成");
        }
        else
        {
            _logger.Info("更新现有登录环境日志信息: UserId={0}", userId);

            // 只有在非页面刷新时才增加登录次数
            if (incrementLoginCount)
            {
                loginExtend.LoginCount++;
                _logger.Info("增加登录次数: UserId={0}, LoginCount={1}", userId, loginExtend.LoginCount);
            }
            else
            {
                _logger.Info("页面刷新，不增加登录次数: UserId={0}", userId);
            }

            // 获取IP地址和位置信息
            var backendIpAddress = GetClientIpAddress();
            var frontendIpAddress = deviceInfo.IpAddress;

            // 智能选择IP地址：优先使用前端获取的公网IP，否则使用后端IP
            var finalIpAddress = !string.IsNullOrEmpty(frontendIpAddress) &&
                                frontendIpAddress != "unknown" &&
                                frontendIpAddress != "0.0.0.0" &&
                                frontendIpAddress != "localhost" &&
                                !frontendIpAddress.Contains("127.0.0.1") &&
                                !IsPrivateIp(frontendIpAddress) // 如果是公网IP，优先使用
                                ? frontendIpAddress : backendIpAddress;

            // 优先使用前端位置信息，否则根据IP获取
            var finalLocation = !string.IsNullOrEmpty(deviceInfo.Location) &&
                               deviceInfo.Location != "unknown"
                               ? deviceInfo.Location
                               : await HbtIpLocationUtils.GetLocationAsync(finalIpAddress);

            loginExtend.LoginStatus = (int)HbtLoginStatus.Success;
            loginExtend.LoginType = (int)HbtLoginType.Password;
            loginExtend.LastLoginTime = now;
            loginExtend.LastLoginIp = finalIpAddress;
            loginExtend.LastLoginLocation = finalLocation;
            loginExtend.LastLoginDeviceId = !string.IsNullOrEmpty(deviceId) ? deviceId : (deviceInfo.DeviceId ?? "unknown");
            loginExtend.LastLoginDeviceType = (int)deviceInfo.DeviceType;
            loginExtend.LastLoginBrowser = (int)deviceInfo.BrowserType;
            loginExtend.LastLoginOs = (int)deviceInfo.OsType;
            loginExtend.Language = loginExtend.Language ?? "zh-CN";
            loginExtend.UpdateTime = now;
            loginExtend.UpdateBy = userName;

            // 更新连续登录天数
            if (loginExtend.LastLoginTime.HasValue)
            {
                var lastLoginDate = loginExtend.LastLoginTime.Value.Date;
                var today = now.Date;
                if (lastLoginDate.AddDays(1) == today)
                {
                    loginExtend.ContinuousLoginDays++;
                }
                else if (lastLoginDate != today)
                {
                    loginExtend.ContinuousLoginDays = 1;
                }
            }

            loginExtend = HbtStringHelper.EnsureEntityStringLength(loginExtend);
            await LoginEnvLogRepository.UpdateAsync(loginExtend);
            _logger.Info("登录环境日志信息更新完成");
        }

        return loginExtend;
    }

    /// <summary>
    /// 处理登录设备日志（新增或更新）
    /// </summary>
    private async Task<HbtLoginDevLog> LoginDevLogAsync(long userId, HbtSignalRDevice deviceInfo, DateTime now, string deviceId)
    {
        // 获取用户名
        var user = await UserRepository.GetByIdAsync(userId);
        var userName = user?.UserName ?? throw new InvalidOperationException($"用户 {userId} 不存在");

        // 使用已生成的设备ID和连接ID
        var connectionId = deviceInfo.DeviceToken;

        // 如果设备ID或连接ID为空，生成默认值
        if (string.IsNullOrEmpty(deviceId))
        {
            deviceId = $"web_{userId}_{DateTime.Now:yyyyMMddHHmmss}";
            _logger.Warn("设备ID为空，生成默认设备ID: {DeviceId}", deviceId);
        }

        if (string.IsNullOrEmpty(connectionId))
        {
            connectionId = $"conn_{userId}_{DateTime.Now:yyyyMMddHHmmss}";
            _logger.Warn("连接ID为空，生成默认连接ID: {ConnectionId}", connectionId);
        }

        // 查询现有设备记录
        var loginDevLog = await LoginDevLogRepository.GetFirstAsync(x =>
            x.UserId == userId &&
            x.DeviceId == deviceId);

        if (loginDevLog == null)
        {
            _logger.Info("创建新的登录设备日志: UserId={0}, DeviceId={1}", userId, deviceId);
            loginDevLog = new HbtLoginDevLog
            {
                UserId = userId,
                DeviceId = deviceId,
                DeviceToken = connectionId,
                DeviceType = (int)deviceInfo.DeviceType,
                DeviceName = deviceInfo.DeviceName ?? "未知设备",
                DeviceModel = deviceInfo.DeviceModel ?? "未知型号",
                DeviceStatus = (int)HbtDeviceStatus.Online,
                LastOnlineTime = now,
                CreateBy = userName,
                CreateTime = now,
                UpdateBy = userName,
                UpdateTime = now
            };

            await LoginDevLogRepository.CreateAsync(loginDevLog);
            _logger.Info("登录设备日志创建完成");
        }
        else
        {
            _logger.Info("更新现有登录设备日志: DeviceId={0}", deviceId);
            loginDevLog.DeviceToken = connectionId;
            loginDevLog.DeviceName = deviceInfo.DeviceName ?? loginDevLog.DeviceName;
            loginDevLog.DeviceType = (int)deviceInfo.DeviceType;
            loginDevLog.DeviceStatus = (int)HbtDeviceStatus.Online;
            loginDevLog.LastOnlineTime = now;
            loginDevLog.UpdateTime = now;
            loginDevLog.UpdateBy = userName;

            await LoginDevLogRepository.UpdateAsync(loginDevLog);
            _logger.Info("登录设备日志更新完成");
        }

        return loginDevLog;
    }
    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    private async Task<HbtUser?> GetUserAsync(string username)
    {
        if (string.IsNullOrEmpty(username))
            return null;

        var user = await UserRepository.GetFirstAsync(x => x.UserName == username);
        if (user == null)
            return null;

        return user;
    }
    /// <summary>
    /// 判断是否为内网IP地址
    /// </summary>
    /// <param name="ipAddress">IP地址</param>
    /// <returns></returns>
    private bool IsPrivateIp(string ipAddress)
    {
        if (string.IsNullOrEmpty(ipAddress))
            return false;

        // 处理IPv6回环地址
        if (ipAddress == "::1")
            return true;

        if (!System.Net.IPAddress.TryParse(ipAddress, out var ip))
            return false;

        // 检查是否为回环地址
        if (IPAddress.IsLoopback(ip))
            return true;

        var bytes = ip.GetAddressBytes();

        // 10.0.0.0 - 10.255.255.255
        if (bytes[0] == 10) return true;

        // 172.16.0.0 - 172.31.255.255
        if (bytes[0] == 172 && bytes[1] >= 16 && bytes[1] <= 31) return true;

        // 192.168.0.0 - 192.168.255.255
        if (bytes[0] == 192 && bytes[1] == 168) return true;

        // 127.0.0.0 - 127.255.255.255 (回环地址)
        if (bytes[0] == 127) return true;

        return false;
    }

    /// <summary>
    /// 获取客户端IP地址
    /// </summary>
    /// <returns></returns>
    private string GetClientIpAddress()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            _logger.Warn("HTTP上下文为空，返回默认IP");
            return L("Identity.Device.DefaultIP");
        }

        // 优先从X-Forwarded-For获取
        var forwardedFor = httpContext.Request.Headers["X-Forwarded-For"].ToString();
        if (!string.IsNullOrEmpty(forwardedFor))
        {
            _logger.Debug("从X-Forwarded-For获取IP: {ForwardedFor}", forwardedFor);
            // 取第一个IP（最原始的客户端IP）
            var ips = forwardedFor.Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (ips.Length > 0)
            {
                var ip = ips[0].Trim();
                _logger.Debug("解析X-Forwarded-For第一个IP: {IP}", ip);
                // 处理IPv6地址
                if (ip == "::1")
                {
                    _logger.Debug("检测到IPv6回环地址::1，转换为127.0.0.1");
                    return "127.0.0.1";
                }
                return ip;
            }
        }

        // 其次从X-Real-IP获取
        var realIp = httpContext.Request.Headers["X-Real-IP"].ToString();
        if (!string.IsNullOrEmpty(realIp))
        {
            _logger.Debug("从X-Real-IP获取IP: {RealIP}", realIp);
            // 处理IPv6地址
            if (realIp == "::1")
            {
                _logger.Debug("检测到IPv6回环地址::1，转换为127.0.0.1");
                return "127.0.0.1";
            }
            return realIp;
        }

        // 最后从RemoteIpAddress获取
        var remoteIp = httpContext.Connection.RemoteIpAddress?.ToString();
        if (!string.IsNullOrEmpty(remoteIp))
        {
            _logger.Debug("从RemoteIpAddress获取IP: {RemoteIP}", remoteIp);
            // 处理IPv6地址
            if (remoteIp == "::1")
            {
                _logger.Debug("检测到IPv6回环地址::1，尝试获取本机内网IP");
                var localIp = HbtLocalIpUtils.GetPreferredLocalIpAddress();
                if (!string.IsNullOrEmpty(localIp))
                {
                    _logger.Debug("获取到本机内网IP: {LocalIP}", localIp);
                    return localIp;
                }
                _logger.Debug("未获取到本机内网IP，转换为127.0.0.1");
                return "127.0.0.1";
            }

            // 如果是127.0.0.1，也尝试获取本机内网IP
            if (remoteIp == "127.0.0.1")
            {
                _logger.Debug("检测到IPv4回环地址127.0.0.1，尝试获取本机内网IP");
                var localIp = HbtLocalIpUtils.GetPreferredLocalIpAddress();
                if (!string.IsNullOrEmpty(localIp))
                {
                    _logger.Debug("获取到本机内网IP: {LocalIP}", localIp);
                    return localIp;
                }
            }

            return remoteIp;
        }

        _logger.Warn("无法获取客户端IP，尝试获取本机内网IP");
        var fallbackLocalIp = HbtLocalIpUtils.GetPreferredLocalIpAddress();
        if (!string.IsNullOrEmpty(fallbackLocalIp))
        {
            _logger.Debug("获取到本机内网IP作为备选: {LocalIP}", fallbackLocalIp);
            return fallbackLocalIp;
        }

        _logger.Warn("无法获取任何IP地址，返回默认IP");
        return L("Identity.Device.DefaultIP");
    }
}