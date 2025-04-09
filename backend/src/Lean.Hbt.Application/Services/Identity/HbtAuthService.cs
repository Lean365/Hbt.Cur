//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V1.0.0
// 描述    : 登录服务实现
//===================================================================

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Application.Services.RealTime;
using Lean.Hbt.Common.Constants;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Audit;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.Entities.RealTime;
using Lean.Hbt.Domain.IServices.Caching;
using Lean.Hbt.Domain.IServices.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Services.Identity;

/// <summary>
/// 登录服务实现
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public class HbtAuthService : IHbtAuthService
{
    private readonly ILogger<HbtAuthService> _logger;
    private readonly IHbtRepository<HbtUser> _userRepository;
    private readonly IHbtRepository<HbtTenant> _tenantRepository;
    private readonly IHbtLoginPolicy _loginPolicy;
    private readonly IHbtCaptchaService _captchaService;
    private readonly IHbtJwtHandler _jwtHandler;
    private readonly IHbtMemoryCache _cache;
    private readonly HbtJwtOptions _jwtOptions;
    private readonly IHbtRepository<HbtLoginExtend> _loginExtendRepository;
    private readonly IHbtRepository<HbtDeviceExtend> _deviceExtendRepository;
    private readonly IHbtRepository<HbtLoginLog> _loginLogRepository;
    private readonly IHbtLocalizationService _localization;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHbtRepository<HbtUserRole> _userRoleRepository;
    private readonly IHbtRepository<HbtUserDept> _userDeptRepository;
    private readonly IHbtRepository<HbtUserPost> _userPostRepository;
    private readonly IHbtOnlineUserService _onlineUserService;
    private readonly IHbtRepository<HbtOnlineUser> _onlineUserRepository;
    private readonly IHbtDeviceIdGenerator _deviceIdGenerator;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtAuthService(
        ILogger<HbtAuthService> logger,
        IHbtRepository<HbtUser> userRepository,
        IHbtRepository<HbtTenant> tenantRepository,
        IHbtLoginPolicy loginPolicy,
        IHbtCaptchaService captchaService,
        IHbtJwtHandler jwtHandler,
        IHbtMemoryCache cache,
        IOptions<HbtJwtOptions> jwtOptions,
        IHbtRepository<HbtLoginExtend> loginExtendRepository,
        IHbtRepository<HbtDeviceExtend> deviceExtendRepository,
        IHbtRepository<HbtLoginLog> loginLogRepository,
        IHbtLocalizationService localization,
        IHttpContextAccessor httpContextAccessor,
        IHbtRepository<HbtUserRole> userRoleRepository,
        IHbtRepository<HbtUserDept> userDeptRepository,
        IHbtRepository<HbtUserPost> userPostRepository,
        IHbtOnlineUserService onlineUserService,
        IHbtRepository<HbtOnlineUser> onlineUserRepository,
        IHbtDeviceIdGenerator deviceIdGenerator)
    {
        _logger = logger;
        _userRepository = userRepository;
        _tenantRepository = tenantRepository;
        _loginPolicy = loginPolicy;
        _captchaService = captchaService;
        _jwtHandler = jwtHandler;
        _cache = cache;
        _jwtOptions = jwtOptions.Value;
        _loginExtendRepository = loginExtendRepository;
        _deviceExtendRepository = deviceExtendRepository;
        _loginLogRepository = loginLogRepository;
        _localization = localization;
        _httpContextAccessor = httpContextAccessor;
        _userRoleRepository = userRoleRepository;
        _userDeptRepository = userDeptRepository;
        _userPostRepository = userPostRepository;
        _onlineUserService = onlineUserService;
        _onlineUserRepository = onlineUserRepository;
        _deviceIdGenerator = deviceIdGenerator;
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="loginDto">登录信息</param>
    /// <returns>登录结果</returns>
    public async Task<HbtLoginResultDto> LoginAsync(HbtAuthDto loginDto)
    {
        _logger.LogInformation("开始处理登录请求: {UserName}", loginDto.UserName);

        try
        {
            // 设置设备信息的租户ID
            if (loginDto.DeviceInfo != null)
            {
                loginDto.DeviceInfo.TenantId = loginDto.TenantId;
            }

            // 验证租户
            var tenant = await GetTenantAsync(loginDto.TenantId);
            _logger.LogInformation("租户验证结果: {TenantResult}", tenant != null ? "找到租户" : "租户不存在");

            if (tenant == null)
                throw new HbtException(_localization.L("Tenant.NotFound"), HbtConstants.ErrorCodes.NotFound);

            if (tenant.Status != 0) // 正常状态
            {
                throw new HbtException(_localization.L("Tenant.Disabled"));
            }

            // 验证用户
            var user = await GetUserAsync(loginDto.UserName);
            _logger.LogInformation("用户验证结果: {UserResult}", user != null ? "找到用户" : "用户不存在");

            if (user == null)
                throw new HbtException(_localization.L("User.NotFound"), HbtConstants.ErrorCodes.NotFound);

            if (user.Status != 0) // 正常状态
            {
                throw new HbtException(_localization.L("User.Disabled"));
            }

            // 验证密码
            _logger.LogInformation("开始验证密码: UserId={UserId}, PasswordLength={Length}", user.Id, loginDto.Password?.Length ?? 0);
            var passwordValid = HbtPasswordUtils.VerifyHash(loginDto.Password, user.Password, user.Salt, user.Iterations);
            _logger.LogInformation("密码验证结果: {Result}", passwordValid ? "验证通过" : "验证失败");

            if (!passwordValid)
            {
                await UpdateLoginFailedAsync(user.Id);
                // 记录登录失败日志
                var loginLog1 = new HbtLoginLog
                {
                    UserId = user.Id,
                    TenantId = user.TenantId,
                    UserName = loginDto.UserName,
                    LoginType = HbtLoginType.Password,
                    LoginStatus = HbtLoginStatus.Failed,
                    LoginTime = DateTime.Now,
                    IpAddress = loginDto.DeviceInfo?.IpAddress ?? _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0",
                    UserAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString(),
                    DeviceInfo = loginDto.DeviceInfo,
                    Message = "密码验证失败",
                    CreateBy = user.Id.ToString(),
                    CreateTime = DateTime.Now,
                    UpdateBy = user.Id.ToString(),
                    UpdateTime = DateTime.Now
                };
                loginLog1 = HbtStringHelper.EnsureEntityStringLength(loginLog1);
                await _loginLogRepository.CreateAsync(loginLog1);

                throw new HbtException(_localization.L("User.InvalidPassword"), HbtConstants.ErrorCodes.Unauthorized);
            }

            // 验证验证码
            if (!string.IsNullOrEmpty(loginDto.CaptchaToken))
            {
                _logger.LogInformation("开始验证验证码: Token={Token}, Offset={Offset}",
                    loginDto.CaptchaToken, loginDto.CaptchaOffset);

                var captchaValid = await _captchaService.ValidateSliderAsync(loginDto.CaptchaToken, loginDto.CaptchaOffset);
                _logger.LogInformation("验证码验证结果: {Result}", captchaValid ? "验证通过" : "验证失败");

                if (!captchaValid)
                {
                    // 记录验证码验证失败日志
                    var loginLog2 = new HbtLoginLog
                    {
                        UserId = user.Id,
                        TenantId = user.TenantId,
                        UserName = loginDto.UserName,
                        LoginType = HbtLoginType.Password,
                        LoginStatus = HbtLoginStatus.Failed,
                        LoginTime = DateTime.Now,
                        IpAddress = loginDto.DeviceInfo?.IpAddress ?? _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0",
                        UserAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString(),
                        DeviceInfo = loginDto.DeviceInfo,
                        Message = "验证码验证失败",
                        CreateBy = user.Id.ToString(),
                        CreateTime = DateTime.Now,
                        UpdateBy = user.Id.ToString(),
                        UpdateTime = DateTime.Now
                    };
                    loginLog2 = HbtStringHelper.EnsureEntityStringLength(loginLog2);
                    await _loginLogRepository.CreateAsync(loginLog2);

                    throw new HbtException(_localization.L("User.InvalidCaptcha"), HbtConstants.ErrorCodes.InvalidCaptcha);
                }
            }

            // 获取用户角色和权限
            _logger.LogInformation("开始获取用户角色和权限: UserId={UserId}", user.Id);
            var roles = await _userRepository.GetUserRolesAsync(user.Id, user.TenantId);
            var permissions = await _userRepository.GetUserPermissionsAsync(user.Id, user.TenantId);
            _logger.LogInformation("用户角色和权限获取完成: RolesCount={RolesCount}, PermissionsCount={PermissionsCount}", 
                roles.Count, permissions.Count);

            // 生成访问令牌
            _logger.LogInformation("开始生成访问令牌");
            var accessToken = await _jwtHandler.GenerateAccessTokenAsync(user, tenant, roles.ToArray(), permissions.ToArray());
            var refreshToken = await _jwtHandler.GenerateRefreshTokenAsync();
            _logger.LogInformation("令牌生成完成: AccessTokenLength={AccessTokenLength}, RefreshToken={RefreshToken}", 
                accessToken.Length, refreshToken);

            // 缓存刷新令牌
            _logger.LogInformation("开始缓存刷新令牌: Key=refresh_token:{RefreshToken}", refreshToken);
            await _cache.SetAsync($"refresh_token:{refreshToken}", user.Id.ToString(), 
                TimeSpan.FromDays(_jwtOptions.RefreshTokenExpirationDays));
            _logger.LogInformation("刷新令牌缓存完成");

            // 处理设备扩展信息
            _logger.LogInformation("准备处理设备扩展信息");
            var deviceExtend = await DeviceExtendAsync(user.Id, user.TenantId, loginDto.DeviceInfo, DateTime.Now);
            _logger.LogInformation("设备扩展信息处理完成");

            // 处理登录扩展信息
            _logger.LogInformation("准备处理登录扩展信息");
            var loginExtend = await LoginExtendAsync(user.Id, user.TenantId, loginDto.DeviceInfo, DateTime.Now);
            _logger.LogInformation("登录扩展信息处理完成");

            // 处理登录日志
            _logger.LogInformation("准备处理登录日志");
            var loginLog = await LoginLogAsync(user.Id, user.TenantId, user.UserName, 
                loginDto.DeviceInfo, deviceExtend?.Id, loginExtend?.Id, DateTime.Now);
            _logger.LogInformation("登录日志处理完成");

            _logger.LogInformation("登录成功: UserId={UserId}, UserName={UserName}", user.Id, user.UserName);

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
                    UserType = user.UserType,
                    TenantId = tenant.Id,
                    TenantName = tenant.TenantName,
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
            _logger.LogError(ex, "登录过程中发生错误: {Message}", ex.Message);
            throw new HbtException("服务器内部错误", HbtConstants.ErrorCodes.ServerError, ex);
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
            throw new HbtException("刷新令牌无效或已过期");

        // 2. 获取用户信息
        var user = await _userRepository.GetByIdAsync(long.Parse(userId));
        if (user == null)
            throw new HbtException("用户不存在");
        if (user.Status != 0)
            throw new HbtException("用户已被禁用");

        // 3. 获取租户信息
        var tenant = await _tenantRepository.GetInfoAsync(x => x.TenantId == user.TenantId);
        if (tenant == null)
            throw new HbtException("租户不存在");
        if (tenant.Status != 0)
            throw new HbtException("租户已被禁用");

        // 4. 获取用户角色和权限
        var roles = await _userRepository.GetUserRolesAsync(user.Id, user.TenantId);
        var permissions = await _userRepository.GetUserPermissionsAsync(user.Id, user.TenantId);

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
            _logger.LogInformation("开始处理用户登出: UserId={UserId}", userId);
            var now = DateTime.Now;

            // 获取用户信息
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException($"用户不存在: userId={userId}");
            }

            // 获取当前用户的设备信息
            var deviceExtend = await _deviceExtendRepository.GetInfoAsync(x => x.UserId == userId);
            if (deviceExtend != null)
            {
                // 更新设备状态为离线
                deviceExtend.DeviceStatus = (int)HbtDeviceStatus.Offline;
                deviceExtend.LastOfflineTime = now;
                await _deviceExtendRepository.UpdateAsync(deviceExtend);
                _logger.LogInformation("已更新设备状态为离线: UserId={UserId}, DeviceId={DeviceId}", userId, deviceExtend.DeviceId);

                // 创建离线设备信息用于记录日志
                var deviceInfo = new HbtSignalRDevice
                {
                    DeviceId = deviceExtend.DeviceId,
                    DeviceToken = deviceExtend.DeviceToken,
                    DeviceType = (HbtDeviceType)deviceExtend.DeviceType,
                    DeviceName = deviceExtend.DeviceName ?? "离线设备",
                    IpAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0"
                };

                // 记录登出日志
                await LoginLogAsync(userId, user.TenantId, user.UserName, deviceInfo, deviceExtend.Id, null, now);
            }

            // 删除在线用户记录
            var onlineUser = await _onlineUserRepository.GetInfoAsync(u => u.UserId == userId);
            if (onlineUser != null)
            {
                onlineUser.OnlineStatus = 1; // 离线
                onlineUser.LastActivity = now;
                onlineUser.UpdateTime = now;
                await _onlineUserRepository.UpdateAsync(onlineUser);
                _logger.LogInformation("已更新用户状态为离线: UserId={UserId}, ConnectionId={ConnectionId}", userId, onlineUser.ConnectionId);
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "处理用户登出时发生错误: UserId={UserId}", userId);
            return false;
        }
    }

    /// <summary>
    /// 生成访问令牌
    /// </summary>
    private string GenerateAccessToken(HbtUser user)
    {
        try
        {
            _logger.LogInformation("开始生成访问令牌，JWT配置信息: {@JwtConfig}", new
            {
                SecretKeyLength = _jwtOptions?.SecretKey?.Length ?? 0,
                Issuer = _jwtOptions?.Issuer,
                Audience = _jwtOptions?.Audience,
                ExpirationMinutes = _jwtOptions?.ExpirationMinutes
            });

            if (string.IsNullOrEmpty(_jwtOptions?.SecretKey))
            {
                _logger.LogError("JWT SecretKey 未配置");
                throw new HbtException("JWT配置错误：SecretKey未配置", HbtConstants.ErrorCodes.ServerError);
            }

            var claims = new List<Claim>
            {
                new Claim("uid", user.Id.ToString()),
                new Claim("unm", user.UserName),
                new Claim("tid", user.TenantId.ToString()),
                new Claim("typ", user.UserType.ToString())
            };

            // 只添加角色信息
            var roles = _userRepository.GetUserRolesAsync(user.Id).Result;
            foreach (var role in roles)
            {
                claims.Add(new Claim("rol", role));
            }

            // 添加管理员标记
            if (roles.Contains("admin"))
            {
                claims.Add(new Claim("adm", "true"));
            }

            //_logger.LogInformation("生成令牌Claims: {@Claims}", claims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes),
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            _logger.LogInformation("访问令牌生成成功: Length={Length}", tokenString?.Length ?? 0);

            return tokenString;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "生成访问令牌时发生错误: {Message}", ex.Message);
            throw new HbtException("生成访问令牌失败", HbtConstants.ErrorCodes.ServerError, ex);
        }
    }

    /// <summary>
    /// 生成刷新令牌
    /// </summary>
    private string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString("N");
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    public async Task<HbtUserInfoDto> GetUserInfoAsync(long userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new HbtException(_localization.L("User.NotFound"), HbtConstants.ErrorCodes.NotFound);
        }

        var roles = await _userRepository.GetUserRolesAsync(userId, user.TenantId);
        var permissions = await _userRepository.GetUserPermissionsAsync(userId, user.TenantId);

        return new HbtUserInfoDto
        {
            UserId = user.Id,
            UserName = user.UserName,
            NickName = user.NickName ?? string.Empty,
            EnglishName = user.EnglishName ?? string.Empty,
            UserType = user.UserType,
            TenantId = user.TenantId,
            Roles = roles,
            Permissions = permissions
        };
    }

    /// <summary>
    /// 根据用户名获取用户信息
    /// </summary>
    /// <param name="username">用户名</param>
    /// <returns>用户信息</returns>
    public async Task<HbtUser?> GetUserByUsernameAsync(string username)
    {
        return await _userRepository.GetInfoAsync(x => x.UserName == username);
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

        var user = await _userRepository.GetInfoAsync(x => x.UserName == username);
        if (user == null)
            return null;

        return (user.Salt, user.Iterations);
    }

    /// <summary>
    /// 更新登录失败信息
    /// </summary>
    private async Task UpdateLoginFailedAsync(long userId)
    {
        var now = DateTime.Now;
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new InvalidOperationException($"用户不存在: userId={userId}");
        }

        // 获取当前IP地址
        var ipAddress = GetClientIpAddress();
        var location = await HbtIpLocationUtils.GetLocationAsync(ipAddress);

        var loginExtend = await _loginExtendRepository.GetInfoAsync(x => x.UserId == userId);
        if (loginExtend == null)
        {
            loginExtend = new HbtLoginExtend
            {
                UserId = userId,
                TenantId = user.TenantId,
                LoginCount = 1,
                LoginStatus = (int)HbtLoginStatus.Failed,
                FirstLoginTime = now,
                FirstLoginIp = ipAddress,
                FirstLoginLocation = location,
                LastLoginTime = now,
                LastLoginIp = ipAddress,
                LastLoginLocation = location,
                CreateBy = userId.ToString(),
                CreateTime = now,
                UpdateBy = userId.ToString(),
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
    private async Task<HbtLoginLog> LoginLogAsync(long userId, long tenantId, string userName, HbtSignalRDevice deviceInfo, long? deviceExtendId, long? loginExtendId, DateTime now)
    {
        // 查询当前用户最近的登录日志
        var loginLog = await _loginLogRepository.GetInfoAsync(x =>
            x.UserId == userId &&
            x.LoginTime.Date == now.Date);

        if (loginLog == null)
        {
            _logger.LogInformation("创建新的登录日志: UserId={UserId}", userId);
            loginLog = new HbtLoginLog
            {
                UserId = userId,
                TenantId = tenantId,
                UserName = userName ?? "未知",
                LoginType = HbtLoginType.Password,
                LoginStatus = HbtLoginStatus.Success,
                LoginTime = now,
                IpAddress = deviceInfo.IpAddress ?? "未知",
                Location = deviceInfo.Location ?? "未知",
                UserAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString(),
                DeviceInfo = deviceInfo,
                DeviceExtendId = deviceExtendId,
                LoginExtendId = loginExtendId,
                Message = $"用户 {userName} 登录成功",
                CreateBy = userId.ToString(),
                CreateTime = now,
                UpdateBy = userId.ToString(),
                UpdateTime = now
            };

            loginLog = HbtStringHelper.EnsureEntityStringLength(loginLog);
            await _loginLogRepository.CreateAsync(loginLog);
            _logger.LogInformation("登录日志创建完成");
        }
        else
        {
            _logger.LogInformation("更新现有登录日志: UserId={UserId}", userId);
            loginLog.LoginTime = now;
            loginLog.IpAddress = deviceInfo.IpAddress ?? loginLog.IpAddress;
            loginLog.Location = deviceInfo.Location ?? loginLog.Location;
            loginLog.UserAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString() ?? loginLog.UserAgent;
            loginLog.DeviceInfo = deviceInfo;
            loginLog.DeviceExtendId = deviceExtendId ?? loginLog.DeviceExtendId;
            loginLog.LoginExtendId = loginExtendId ?? loginLog.LoginExtendId;
            loginLog.Message = $"用户 {userName} 登录成功";
            loginLog.UpdateTime = now;
            loginLog.UpdateBy = userId.ToString();

            loginLog = HbtStringHelper.EnsureEntityStringLength(loginLog);
            await _loginLogRepository.UpdateAsync(loginLog);
            _logger.LogInformation("登录日志更新完成");
        }

        return loginLog;
    }

    /// <summary>
    /// 处理登录扩展信息（新增或更新）
    /// </summary>
    private async Task<HbtLoginExtend> LoginExtendAsync(long userId, long tenantId, HbtSignalRDevice deviceInfo, DateTime now)
    {
        // 查询现有登录扩展信息
        var loginExtend = await _loginExtendRepository.GetInfoAsync(x => x.UserId == userId);

        if (loginExtend == null)
        {
            _logger.LogInformation("创建新的登录扩展信息: UserId={UserId}", userId);
            loginExtend = new HbtLoginExtend
            {
                UserId = userId,
                TenantId = tenantId,
                LoginCount = 1,
                LoginStatus = (int)HbtLoginStatus.Success,
                LoginType = (int)HbtLoginType.Password,
                FirstLoginTime = now,
                FirstLoginIp = deviceInfo.IpAddress ?? "未知",
                FirstLoginLocation = deviceInfo.Location ?? "未知",
                FirstLoginDeviceId = deviceInfo.DeviceId,
                FirstLoginDeviceType = (int)deviceInfo.DeviceType,
                FirstLoginBrowser = (int)deviceInfo.BrowserType,
                FirstLoginOs = (int)deviceInfo.OsType,
                LastLoginTime = now,
                LastLoginIp = deviceInfo.IpAddress ?? "未知",
                LastLoginLocation = deviceInfo.Location ?? "未知",
                LastLoginDeviceId = deviceInfo.DeviceId,
                LastLoginDeviceType = (int)deviceInfo.DeviceType,
                LastLoginBrowser = (int)deviceInfo.BrowserType,
                LastLoginOs = (int)deviceInfo.OsType,
                ContinuousLoginDays = 1,
                CreateBy = userId.ToString(),
                CreateTime = now,
                UpdateBy = userId.ToString(),
                UpdateTime = now
            };

            loginExtend = HbtStringHelper.EnsureEntityStringLength(loginExtend);
            await _loginExtendRepository.CreateAsync(loginExtend);
            _logger.LogInformation("登录扩展信息创建完成");
        }
        else
        {
            _logger.LogInformation("更新现有登录扩展信息: UserId={UserId}", userId);
            loginExtend.LoginCount++;
            loginExtend.LoginStatus = (int)HbtLoginStatus.Success;
            loginExtend.LoginType = (int)HbtLoginType.Password;
            loginExtend.LastLoginTime = now;
            loginExtend.LastLoginIp = deviceInfo.IpAddress ?? "未知";
            loginExtend.LastLoginLocation = deviceInfo.Location ?? "未知";
            loginExtend.LastLoginDeviceId = deviceInfo.DeviceId;
            loginExtend.LastLoginDeviceType = (int)deviceInfo.DeviceType;
            loginExtend.LastLoginBrowser = (int)deviceInfo.BrowserType;
            loginExtend.LastLoginOs = (int)deviceInfo.OsType;
            loginExtend.UpdateTime = now;
            loginExtend.UpdateBy = userId.ToString();

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
            await _loginExtendRepository.UpdateAsync(loginExtend);
            _logger.LogInformation("登录扩展信息更新完成");
        }

        return loginExtend;
    }

    /// <summary>
    /// 处理设备扩展信息（新增或更新）
    /// </summary>
    private async Task<HbtDeviceExtend> DeviceExtendAsync(long userId, long tenantId, HbtSignalRDevice deviceInfo, DateTime now)
    {
        // 使用已生成的设备ID和连接ID
        var deviceId = deviceInfo.DeviceId;
        var connectionId = deviceInfo.DeviceToken;

        if (string.IsNullOrEmpty(deviceId) || string.IsNullOrEmpty(connectionId))
        {
            throw new InvalidOperationException("设备ID和连接ID不能为空");
        }

        // 查询现有设备记录
        var deviceExtend = await _deviceExtendRepository.GetInfoAsync(x =>
            x.UserId == userId &&
            x.DeviceId == deviceId);

        if (deviceExtend == null)
        {
            _logger.LogInformation("创建新的设备扩展信息: UserId={UserId}, DeviceId={DeviceId}", userId, deviceId);
            deviceExtend = new HbtDeviceExtend
            {
                UserId = userId,
                TenantId = tenantId,
                DeviceId = deviceId,
                DeviceToken = connectionId,
                DeviceType = (int)deviceInfo.DeviceType,
                DeviceName = deviceInfo.DeviceName ?? "未知设备",
                DeviceModel = deviceInfo.DeviceModel ?? "未知型号",
                OsType = (int)deviceInfo.OsType,
                OsVersion = deviceInfo.OsVersion ?? "未知版本",
                BrowserType = (int)deviceInfo.BrowserType,
                BrowserVersion = deviceInfo.BrowserVersion ?? "未知版本",
                Resolution = deviceInfo.Resolution ?? "未知分辨率",
                DeviceStatus = (int)HbtDeviceStatus.Online,
                LastOnlineTime = now,
                CreateBy = userId.ToString(),
                CreateTime = now,
                UpdateBy = userId.ToString(),
                UpdateTime = now
            };

            await _deviceExtendRepository.CreateAsync(deviceExtend);
            _logger.LogInformation("设备扩展信息创建完成");
        }
        else
        {
            _logger.LogInformation("更新现有设备扩展信息: DeviceId={DeviceId}", deviceId);
            deviceExtend.DeviceToken = connectionId;
            deviceExtend.DeviceName = deviceInfo.DeviceName ?? deviceExtend.DeviceName;
            deviceExtend.DeviceType = (int)deviceInfo.DeviceType;
            deviceExtend.BrowserType = (int)deviceInfo.BrowserType;
            deviceExtend.BrowserVersion = deviceInfo.BrowserVersion ?? deviceExtend.BrowserVersion;
            deviceExtend.Resolution = deviceInfo.Resolution ?? deviceExtend.Resolution;
            deviceExtend.DeviceStatus = (int)HbtDeviceStatus.Online;
            deviceExtend.LastOnlineTime = now;
            deviceExtend.UpdateTime = now;
            deviceExtend.UpdateBy = userId.ToString();

            await _deviceExtendRepository.UpdateAsync(deviceExtend);
            _logger.LogInformation("设备扩展信息更新完成");
        }

        return deviceExtend;
    }

    private async Task<HbtTenant?> GetTenantAsync(long tenantId)
    {
        if (tenantId < 0)
            return null;

        var tenant = await _tenantRepository.GetInfoAsync(x => x.TenantId == tenantId);
        if (tenant == null)
            return null;

        return tenant;
    }

    private async Task<HbtUser?> GetUserAsync(string username)
    {
        if (string.IsNullOrEmpty(username))
            return null;

        var user = await _userRepository.GetInfoAsync(x => x.UserName == username);
        if (user == null)
            return null;

        return user;
    }

    private string GetClientIpAddress()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
            return "0.0.0.0";

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

        return "0.0.0.0";
    }
}