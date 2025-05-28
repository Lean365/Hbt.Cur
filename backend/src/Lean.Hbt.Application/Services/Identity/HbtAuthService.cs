//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V1.0.0
// 描述    : 登录服务实现
//===================================================================

using System.Text.Json;
using Lean.Hbt.Common.Constants;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.IServices.Caching;
using Lean.Hbt.Domain.IServices.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Lean.Hbt.Application.Services.Identity;

/// <summary>
/// 登录服务实现
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public class HbtAuthService : HbtBaseService, IHbtAuthService
{
    private readonly IHbtRepository<HbtUser> _userRepository;
    private readonly IHbtRepository<HbtTenant> _tenantRepository;
    private readonly IHbtRepository<HbtUserTenant> _userTenantRepository;
    private readonly IHbtCaptchaService _captchaService;
    private readonly IHbtJwtHandler _jwtHandler;
    private readonly IHbtMemoryCache _cache;
    private readonly HbtJwtOptions _jwtOptions;
    private readonly IHbtRepository<HbtLoginEnvLog> _loginExtendRepository;
    private readonly IHbtRepository<HbtLoginDevLog> _deviceExtendRepository;
    private readonly IHbtRepository<HbtLoginLog> _loginLogRepository;
    private readonly IHbtRepository<HbtOnlineUser> _onlineUserRepository;
    private readonly IHbtDeviceIdGenerator _deviceIdGenerator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger">日志服务</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="tenantRepository">租户仓储</param>
    /// <param name="userTenantRepository">用户租户仓储</param>
    /// <param name="captchaService">验证码服务</param>
    /// <param name="jwtHandler">JWT处理器</param>
    /// <param name="cache">缓存服务</param>
    /// <param name="jwtOptions">JWT配置选项</param>
    /// <param name="loginExtendRepository">登录环境日志仓储</param>
    /// <param name="deviceExtendRepository">设备日志仓储</param>
    /// <param name="loginLogRepository">登录日志仓储</param>
    /// <param name="onlineUserRepository">在线用户仓储</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="currentTenant">当前租户服务</param>
    /// <param name="localization">本地化服务</param>
    /// <param name="deviceIdGenerator">设备ID生成器</param>
    public HbtAuthService(
        IHbtLogger logger,
        IHttpContextAccessor httpContextAccessor,
        IHbtRepository<HbtUser> userRepository,
        IHbtRepository<HbtTenant> tenantRepository,
        IHbtRepository<HbtUserTenant> userTenantRepository,
        IHbtCaptchaService captchaService,
        IHbtJwtHandler jwtHandler,
        IHbtMemoryCache cache,
        IOptions<HbtJwtOptions> jwtOptions,
        IHbtRepository<HbtLoginEnvLog> loginExtendRepository,
        IHbtRepository<HbtLoginDevLog> deviceExtendRepository,
        IHbtRepository<HbtLoginLog> loginLogRepository,
        IHbtRepository<HbtOnlineUser> onlineUserRepository,
        IHbtCurrentUser currentUser,
        IHbtCurrentTenant currentTenant,
        IHbtLocalizationService localization,
        IHbtDeviceIdGenerator deviceIdGenerator) : base(logger, httpContextAccessor, currentUser, currentTenant, localization)
    {
        _userRepository = userRepository;
        _tenantRepository = tenantRepository;
        _userTenantRepository = userTenantRepository;
        _captchaService = captchaService;
        _jwtHandler = jwtHandler;
        _cache = cache;
        _jwtOptions = jwtOptions.Value;
        _loginExtendRepository = loginExtendRepository;
        _deviceExtendRepository = deviceExtendRepository;
        _loginLogRepository = loginLogRepository;
        _onlineUserRepository = onlineUserRepository;
        _deviceIdGenerator = deviceIdGenerator;
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
            var onlineUser = await _onlineUserRepository.GetFirstAsync(u => u.UserId == user.Id);
            if (onlineUser != null && onlineUser.OnlineStatus == 1)
            {
                isPageRefresh = true;
                _logger.Info(L("Identity.Auth.ExistingUserLogin", user.Id));
            }

            // 第三步：验证用户租户关系
            // 先再次检查用户是否存在（防止用户被删除但租户关系未清理的情况）
            var userExists = await _userRepository.GetFirstAsync(u => u.Id == user.Id && u.Status != -1 && u.IsDeleted == 0);
            if (userExists == null)
            {
                _logger.Warn(L("Identity.Auth.UserDeletedButTenantRelationExists", loginDto.UserName, loginDto.TenantId));
                // 清理无效的租户关系
                await _userRepository.SqlSugarClient.Deleteable<HbtUserTenant>()
                    .Where(ut => ut.UserId == user.Id)
                    .ExecuteCommandAsync();
                throw new HbtException(L("Identity.User.InvalidCredentials"), HbtConstants.ErrorCodes.Unauthorized);
            }

            // 检查用户是否属于指定的租户，以及在该租户中的状态
            var userTenant = await _userTenantRepository.GetFirstAsync(ut => 
                ut.UserId == user.Id && 
                ut.TenantId == loginDto.TenantId && 
                ut.IsDeleted == 0);

            // 如果用户不属于该租户，返回错误
            if (userTenant == null)
            {
                _logger.Warn(L("Identity.Auth.UserTenantNotFound", loginDto.UserName, loginDto.TenantId));
                throw new HbtException(L("Identity.User.InvalidCredentials"), HbtConstants.ErrorCodes.Unauthorized);
            }

            // 如果用户在该租户中已被禁用，返回错误
            if (userTenant.Status != 0)
            {
                _logger.Warn(L("Identity.Auth.UserTenantDisabled", loginDto.UserName, loginDto.TenantId));
                throw new HbtException(L("Identity.User.Disabled"), HbtConstants.ErrorCodes.Unauthorized);
            }

            // 第四步：设置设备信息
            // 将租户ID添加到设备信息中，用于后续的日志记录
            if (loginDto.DeviceInfo != null)
            {
                loginDto.DeviceInfo.TenantId = loginDto.TenantId;
            }

            // 第五步：验证租户状态
            // 检查租户是否存在且状态正常
            var tenant = await GetTenantAsync(loginDto.TenantId);
            _logger.Info(L("Identity.Auth.TenantValidation", tenant != null ? "Found" : "NotFound"));

            if (tenant == null)
                throw new HbtException(L("Identity.Tenant.NotFound"), HbtConstants.ErrorCodes.NotFound);

            if (tenant.Status != 0)
            {
                throw new HbtException(L("Identity.Tenant.Disabled"));
            }

            // 第六步：验证用户状态
            // 检查用户是否被禁用
            _logger.Info(L("Identity.Auth.UserValidationStart", loginDto.UserName));
            if (user.Status != 0)
            {
                throw new HbtException(L("Identity.User.Disabled"));
            }

            // 第七步：验证密码
            // 使用盐值和迭代次数验证密码
            _logger.Info(L("Identity.Auth.PasswordValidationStart", user.Id, loginDto.Password?.Length ?? 0));
            var passwordValid = HbtPasswordUtils.VerifyHash(loginDto.Password, user.Password, user.Salt, user.Iterations);
            _logger.Info(L("Identity.Auth.PasswordValidationResult", passwordValid ? "Success" : "Failed"));

            if (!passwordValid)
            {
                // 密码验证失败，更新登录失败记录
                await UpdateLoginFailedAsync(user.Id, "", "");
                // 记录登录失败日志
                var loginLog1 = new HbtLoginLog
                {
                    UserId = user.Id,
                    UserName = loginDto.UserName,
                    LoginType = HbtLoginType.Password,
                    LoginStatus = HbtLoginStatus.Failed,
                    LoginTime = DateTime.Now,
                    IpAddress = loginDto.DeviceInfo?.IpAddress ?? _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0",
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
                await _loginLogRepository.CreateAsync(loginLog1);

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
                        IpAddress = loginDto.DeviceInfo?.IpAddress ?? _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0",
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
                    await _loginLogRepository.CreateAsync(loginLog2);

                    throw new HbtException(L("Identity.User.InvalidCaptcha"), HbtConstants.ErrorCodes.InvalidCaptcha);
                }
            }

            // 第九步：获取用户权限
            // 获取用户在租户中的角色和权限
            _logger.Info(L("Identity.Auth.GetUserRolesAndPermissions", user.Id));
            var roles = await _userRepository.GetUserRolesAsync(user.Id);
            var permissions = await _userRepository.GetUserPermissionsAsync(user.Id);
            _logger.Info(L("Identity.Auth.UserRolesAndPermissionsResult", roles.Count, permissions.Count));

            // 第十步：生成令牌
            // 生成访问令牌和刷新令牌
            _logger.Info(L("Identity.Auth.GenerateTokens"));
            var accessToken = await _jwtHandler.GenerateAccessTokenAsync(user, tenant, roles.ToArray(), permissions.ToArray());
            var refreshToken = await _jwtHandler.GenerateRefreshTokenAsync();
            _logger.Info(L("Identity.Auth.TokensGenerated", accessToken.Length, refreshToken));

            // 第十一步：缓存刷新令牌
            // 将刷新令牌存入缓存，用于后续的令牌刷新
            _logger.Info(L("Identity.Auth.CacheRefreshToken", refreshToken));
            await _cache.SetAsync($"refresh_token:{refreshToken}", user.Id.ToString(),
                TimeSpan.FromDays(_jwtOptions.RefreshTokenExpirationDays));
            _logger.Info(L("Identity.Auth.RefreshTokenCached"));

            // 第十二步：生成设备ID和环境ID
            // 根据设备信息和环境信息生成唯一标识
            var deviceInfoJson = JsonSerializer.Serialize(loginDto.DeviceInfo);
            var environmentInfoJson = JsonSerializer.Serialize(loginDto.EnvironmentInfo);
            var deviceId = _deviceIdGenerator.GenerateDeviceId(deviceInfoJson, user.Id.ToString());
            var environmentId = _deviceIdGenerator.GenerateEnvironmentId(environmentInfoJson, user.Id.ToString());
            _logger.Info(L("Identity.Auth.GeneratedIds", deviceId, environmentId));

            // 第十三步：记录登录日志
            // 记录完整的登录信息
            _logger.Info(L("Identity.Auth.ProcessLoginLog"));
            HbtLoginLog loginLog;
            loginLog = await LoginLogAsync(user.Id, user.UserName,
                loginDto.DeviceInfo, loginDto.EnvironmentInfo, DateTime.Now, deviceId, environmentId);
            _logger.Info(L("Identity.Auth.LoginLogProcessed"));

            // 第十四步：记录设备日志
            // 记录用户的设备信息
            _logger.Info(L("Identity.Auth.ProcessDeviceInfo"));
            var loginDevLog = await LoginDevLogAsync(user.Id, loginDto.DeviceInfo, DateTime.Now, deviceId);
            _logger.Info(L("Identity.Auth.DeviceInfoProcessed"));

            // 第十五步：记录环境日志
            // 记录用户的登录环境信息
            _logger.Info(L("Identity.Auth.ProcessLoginInfo"));
            var loginEnvLog = await LoginEnvLogAsync(user.Id, loginDto.DeviceInfo, DateTime.Now, !isPageRefresh, environmentId);
            _logger.Info(L("Identity.Auth.LoginInfoProcessed"));

            _logger.Info(L("Identity.Auth.LoginSuccess", user.Id, user.UserName));

            // 第十六步：返回登录结果
            // 返回包含令牌和用户信息的登录结果
            return new HbtLoginResultDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = _jwtOptions.ExpirationMinutes * 60,
                UserInfo = new HbtUserInfoDto
                {
                    TenantId = tenant.Id,
                    TenantName = tenant.TenantName,
                    UserId = user.Id,
                    UserName = user.UserName,
                    NickName = user.NickName ?? string.Empty,
                    EnglishName = user.EnglishName ?? string.Empty,
                    FullName = user.FullName ?? string.Empty,
                    RealName = user.RealName ?? string.Empty,
                    UserType = user.UserType,

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
        // 1. 验证刷新令牌
        var cacheKey = $"refresh_token:{refreshToken}";
        var userId = _cache.Get<string>(cacheKey);
        if (string.IsNullOrEmpty(userId))
            throw new HbtException(L("Identity.Auth.InvalidRefreshToken"));

        // 2. 获取用户信息
        var user = await _userRepository.GetByIdAsync(long.Parse(userId));
        if (user == null)
            throw new HbtException(L("Identity.User.NotFound"));
        if (user.Status != 0)
            throw new HbtException(L("Identity.User.Disabled"));

        // 3. 获取租户信息
        var tenant = await _tenantRepository.GetFirstAsync(x => x.Id == _currentTenant.TenantId);
        if (tenant == null)
            throw new HbtException(L("Identity.Tenant.NotFound"));
        if (tenant.Status != 0)
            throw new HbtException(L("Identity.Tenant.Disabled"));

        // 4. 获取用户角色和权限
        var roles = await _userRepository.GetUserRolesAsync(user.Id);
        var permissions = await _userRepository.GetUserPermissionsAsync(user.Id);

        // 5. 生成新令牌
        var accessToken = await _jwtHandler.GenerateAccessTokenAsync(user, tenant, roles.ToArray(), permissions.ToArray());
        var newRefreshToken = await _jwtHandler.GenerateRefreshTokenAsync();

        // 6. 更新缓存
        await _cache.RemoveAsync(cacheKey);
        var newCacheKey = $"refresh_token:{newRefreshToken}";
        await _cache.SetAsync(newCacheKey, userId, TimeSpan.FromDays(_jwtOptions.RefreshTokenExpirationDays));

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
                TenantId = tenant.Id,
                TenantName = tenant.TenantName,
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
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException(L("Identity.User.NotFound", userId));
            }

            // 获取当前用户的环境信息
            var loginEnvLog = await _loginExtendRepository.GetFirstAsync(x => x.UserId == userId);
            if (loginEnvLog != null)
            {
                // 获取当前用户的设备信息
                var loginDevLog = await _deviceExtendRepository.GetFirstAsync(x => x.UserId == userId);
                if (loginDevLog != null)
                {
                    // 更新设备状态为离线
                    loginDevLog.DeviceStatus = (int)HbtDeviceStatus.Offline;
                    loginDevLog.LastOfflineTime = now;
                    await _deviceExtendRepository.UpdateAsync(loginDevLog);
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
            var onlineUser = await _onlineUserRepository.GetFirstAsync(u => u.UserId == userId);
            if (onlineUser != null)
            {
                onlineUser.OnlineStatus = 1; // 离线
                onlineUser.LastActivity = now;
                onlineUser.UpdateTime = now;
                await _onlineUserRepository.UpdateAsync(onlineUser);
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
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new HbtException(L("Identity.User.NotFound", userId), HbtConstants.ErrorCodes.NotFound);
        }

        // 获取用户租户关联信息
        var userTenant = await _userTenantRepository.GetFirstAsync(ut => 
            ut.UserId == userId && 
            ut.TenantId == _currentTenant.TenantId && 
            ut.IsDeleted == 0);

        if (userTenant == null)
        {
            throw new HbtException(L("Identity.User.InvalidTenant"), HbtConstants.ErrorCodes.Unauthorized);
        }

        // 获取租户信息
        var tenant = await _tenantRepository.GetByIdAsync(userTenant.TenantId);
        if (tenant == null)
        {
            throw new HbtException(L("Identity.Tenant.NotFound"), HbtConstants.ErrorCodes.NotFound);
        }

        // 获取用户角色和权限
        var roles = await _userRepository.GetUserRolesAsync(userId);
        var permissions = await _userRepository.GetUserPermissionsAsync(userId);

        return new HbtUserInfoDto
        {
            UserId = user.Id,
            UserName = user.UserName,
            NickName = user.NickName ?? string.Empty,
            EnglishName = user.EnglishName ?? string.Empty,
            FullName = user.FullName ?? string.Empty,
            RealName = user.RealName ?? string.Empty,
            UserType = user.UserType,
            TenantId = userTenant.TenantId,
            TenantName = tenant.TenantName,
            Roles = roles,
            Permissions = permissions
        };
    }

    /// <summary>
    /// 根据用户名获取用户信息
    /// </summary>
    /// <param name="username">用户名</param>
    /// <returns>用户信息及其租户信息</returns>
    public async Task<(HbtUser User, List<HbtUserTenant> UserTenants)?> GetUserByUsernameAsync(string username)
    {
        if (string.IsNullOrEmpty(username))
            return null;

        // 获取用户信息，检查状态、删除标记和锁定状态
        var user = await _userRepository.GetFirstAsync(x => 
            x.UserName == username && 
            x.Status == 0 && // 0 表示正常状态
            x.IsDeleted == 0 && // 0 表示未删除
            x.IsLock == 0 && // 0 表示未锁定
            (x.LockEndTime == null || x.LockEndTime < DateTime.Now)); // 未锁定或锁定已过期

        if (user == null)
            return null;

        // 获取用户的所有租户关系
        var userTenants = await _userTenantRepository.GetListAsync(ut => 
            ut.UserId == user.Id && 
            ut.IsDeleted == 0 && 
            ut.Status == 0); // 0 表示正常状态

        return (user, userTenants);
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

        var user = await _userRepository.GetFirstAsync(x => x.UserName == username);
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
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new InvalidOperationException(L("Identity.User.NotFound", userId));
        }

        // 获取当前IP地址
        var ipAddress = GetClientIpAddress();
        var location = await HbtIpLocationUtils.GetLocationAsync(ipAddress);

        var loginExtend = await _loginExtendRepository.GetFirstAsync(x => x.UserId == userId);
        if (loginExtend == null)
        {
            loginExtend = new HbtLoginEnvLog
            {
                UserId = userId,
                DeviceId = deviceExtendId,
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
            await _loginExtendRepository.CreateAsync(loginExtend);
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
            await _loginExtendRepository.UpdateAsync(loginExtend);
        }
    }

    /// <summary>
    /// 处理登录日志（新增或更新）
    /// </summary>
    private async Task<HbtLoginLog> LoginLogAsync(long userId, string userName, HbtSignalRDevice deviceInfo, HbtSignalREnvironment environmentInfo, DateTime now, string deviceId, string? environmentId)
    {
        _logger.Info("创建新的登录日志: UserId={0}", userId);
        HbtLoginLog loginLog;
        loginLog = new HbtLoginLog
        {
            UserId = userId,
            UserName = userName,
            LoginType = HbtLoginType.Password,
            LoginStatus = HbtLoginStatus.Success,
            LoginTime = now,
            IpAddress = deviceInfo.IpAddress ?? "未知",
            Location = await HbtIpLocationUtils.GetLocationAsync(deviceInfo.IpAddress),
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
        await _loginLogRepository.CreateAsync(loginLog);
        _logger.Info("登录日志创建完成");

        return loginLog;
    }

    /// <summary>
    /// 处理登录环境日志信息（新增或更新）
    /// </summary>
    private async Task<HbtLoginEnvLog> LoginEnvLogAsync(long userId, HbtSignalRDevice deviceInfo, DateTime now, bool incrementLoginCount = true, string environmentId = "")
    {
        // 获取用户名
        var user = await _userRepository.GetByIdAsync(userId);
        var userName = user?.UserName ?? throw new InvalidOperationException($"用户 {userId} 不存在");

        // 查询现有登录环境日志信息
        var loginEnvLog = await _loginExtendRepository.GetFirstAsync(x => x.UserId == userId);

        if (loginEnvLog == null)
        {
            _logger.Info("创建新的登录环境日志信息: UserId={0}", userId);
            loginEnvLog = new HbtLoginEnvLog
            {
                UserId = userId,
                DeviceId = deviceInfo.DeviceId ?? "unknown",
                EnvironmentId = environmentId ?? "unknown",
                LoginCount = 1,
                LoginStatus = (int)HbtLoginStatus.Success,
                LoginType = (int)HbtLoginType.Password,
                FirstLoginTime = now,
                FirstLoginIp = deviceInfo.IpAddress ?? "unknown",
                FirstLoginLocation = deviceInfo.Location ?? "unknown",
                FirstLoginDeviceId = deviceInfo.DeviceId ?? "unknown",
                FirstLoginDeviceType = (int)deviceInfo.DeviceType,
                FirstLoginBrowser = (int)deviceInfo.BrowserType,
                FirstLoginOs = (int)deviceInfo.OsType,
                LastLoginTime = now,
                LastLoginIp = deviceInfo.IpAddress ?? "unknown",
                LastLoginLocation = deviceInfo.Location ?? "unknown",
                LastLoginDeviceId = deviceInfo.DeviceId ?? "unknown",
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

            loginEnvLog = HbtStringHelper.EnsureEntityStringLength(loginEnvLog);
            await _loginExtendRepository.CreateAsync(loginEnvLog);
            _logger.Info("登录环境日志信息创建完成");
        }
        else
        {
            _logger.Info("更新现有登录环境日志信息: UserId={0}", userId);

            // 只有在非页面刷新时才增加登录次数
            if (incrementLoginCount)
            {
                loginEnvLog.LoginCount++;
                _logger.Info("增加登录次数: UserId={0}, LoginCount={1}", userId, loginEnvLog.LoginCount);
            }
            else
            {
                _logger.Info("页面刷新，不增加登录次数: UserId={0}", userId);
            }

            loginEnvLog.LoginStatus = (int)HbtLoginStatus.Success;
            loginEnvLog.LoginType = (int)HbtLoginType.Password;
            loginEnvLog.LastLoginTime = now;
            loginEnvLog.LastLoginIp = deviceInfo.IpAddress ?? "unknown";
            loginEnvLog.LastLoginLocation = deviceInfo.Location ?? "unknown";
            loginEnvLog.LastLoginDeviceId = deviceInfo.DeviceId ?? "unknown";
            loginEnvLog.LastLoginDeviceType = (int)deviceInfo.DeviceType;
            loginEnvLog.LastLoginBrowser = (int)deviceInfo.BrowserType;
            loginEnvLog.LastLoginOs = (int)deviceInfo.OsType;
            loginEnvLog.Language = loginEnvLog.Language ?? "zh-CN";
            loginEnvLog.UpdateTime = now;
            loginEnvLog.UpdateBy = userName;

            // 更新连续登录天数
            if (loginEnvLog.LastLoginTime.HasValue)
            {
                var lastLoginDate = loginEnvLog.LastLoginTime.Value.Date;
                var today = now.Date;
                if (lastLoginDate.AddDays(1) == today)
                {
                    loginEnvLog.ContinuousLoginDays++;
                }
                else if (lastLoginDate != today)
                {
                    loginEnvLog.ContinuousLoginDays = 1;
                }
            }

            loginEnvLog = HbtStringHelper.EnsureEntityStringLength(loginEnvLog);
            await _loginExtendRepository.UpdateAsync(loginEnvLog);
            _logger.Info("登录环境日志信息更新完成");
        }

        return loginEnvLog;
    }

    /// <summary>
    /// 处理登录设备日志（新增或更新）
    /// </summary>
    private async Task<HbtLoginDevLog> LoginDevLogAsync(long userId, HbtSignalRDevice deviceInfo, DateTime now, string deviceId)
    {
        // 获取用户名
        var user = await _userRepository.GetByIdAsync(userId);
        var userName = user?.UserName ?? throw new InvalidOperationException($"用户 {userId} 不存在");

        // 使用已生成的设备ID和连接ID
        var connectionId = deviceInfo.DeviceToken;

        if (string.IsNullOrEmpty(deviceId) || string.IsNullOrEmpty(connectionId))
        {
            throw new InvalidOperationException(L("Identity.Device.InvalidDeviceInfo"));
        }

        // 查询现有设备记录
        var loginDevLog = await _deviceExtendRepository.GetFirstAsync(x =>
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

            await _deviceExtendRepository.CreateAsync(loginDevLog);
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

            await _deviceExtendRepository.UpdateAsync(loginDevLog);
            _logger.Info("登录设备日志更新完成");
        }

        return loginDevLog;
    }
    /// <summary>
    /// 获取租户信息
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    private async Task<HbtTenant?> GetTenantAsync(long tenantId)
    {
        if (tenantId < 0)
            return null;

        var tenant = await _tenantRepository.GetFirstAsync(x => x.Id == tenantId);
        if (tenant == null)
            return null;

        return tenant;
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

        var user = await _userRepository.GetFirstAsync(x => x.UserName == username);
        if (user == null)
            return null;

        return user;
    }
    /// <summary>
    /// 获取客户端IP地址
    /// </summary>
    /// <returns></returns>
    private string GetClientIpAddress()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
            return L("Identity.Device.DefaultIP");

        // 优先从X-Forwarded-For获取
        var forwardedFor = httpContext.Request.Headers["X-Forwarded-For"].ToString();
        if (!string.IsNullOrEmpty(forwardedFor))
        {
            // 取第一个IP（最原始的客户端IP）
            var ips = forwardedFor.Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (ips.Length > 0)
                return ips[0].Trim();
        }

        // 其次从X-Real-IP获取
        var realIp = httpContext.Request.Headers["X-Real-IP"].ToString();
        if (!string.IsNullOrEmpty(realIp))
            return realIp;

        // 最后从RemoteIpAddress获取
        var remoteIp = httpContext.Connection.RemoteIpAddress?.ToString();
        if (!string.IsNullOrEmpty(remoteIp))
            return remoteIp;

        return L("Identity.Device.DefaultIP");
    }
}