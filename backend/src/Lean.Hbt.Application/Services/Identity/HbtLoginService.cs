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
using System.Security.Cryptography;
using System.Text;
using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Common.Constants;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Audit;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices.Admin;
using Lean.Hbt.Domain.IServices.Caching;
using Lean.Hbt.Domain.IServices.Security;
using Lean.Hbt.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Lean.Hbt.Application.Services.Identity;

/// <summary>
/// 登录服务实现
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public class HbtLoginService : IHbtLoginService
{
    private readonly ILogger<HbtLoginService> _logger;
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

    // 登录策略常量
    private const int ADMIN_MAX_FAILED_ATTEMPTS = 3;  // 管理员最大失败次数

    private const int MAX_FAILED_ATTEMPTS = 5;        // 普通用户最大失败次数
    private const int ADMIN_LOCKOUT_MINUTES = 30;     // 管理员锁定时间(分钟)
    private const int USER_LOCKOUT_MINUTES = 10;      // 普通用户锁定时间(分钟)

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtLoginService(
        ILogger<HbtLoginService> logger,
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
        IHbtRepository<HbtUserPost> userPostRepository)
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
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="loginDto">登录信息</param>
    /// <returns>登录结果</returns>
    public async Task<HbtLoginResultDto> LoginAsync(HbtLoginDto loginDto)
    {
        _logger.LogInformation("开始处理登录请求: {UserName}", loginDto.UserName);
        _logger.LogInformation("登录参数详情: {@LoginParams}", new
        {
            TenantId = loginDto.TenantId,
            UserName = loginDto.UserName,
            PasswordLength = loginDto.Password?.Length ?? 0,
            CaptchaToken = !string.IsNullOrEmpty(loginDto.CaptchaToken),
            CaptchaOffset = loginDto.CaptchaOffset,
            LoginSource = loginDto.LoginSource,
            DeviceInfo = loginDto.DeviceInfo != null ? new
            {
                DeviceId = loginDto.DeviceInfo.DeviceId,
                DeviceType = loginDto.DeviceInfo.DeviceType,
                DeviceName = loginDto.DeviceInfo.DeviceName,
                DeviceModel = loginDto.DeviceInfo.DeviceModel,
                OsType = loginDto.DeviceInfo.OsType,
                OsVersion = loginDto.DeviceInfo.OsVersion,
                BrowserType = loginDto.DeviceInfo.BrowserType,
                BrowserVersion = loginDto.DeviceInfo.BrowserVersion,
                Resolution = loginDto.DeviceInfo.Resolution,
                Location = loginDto.DeviceInfo.Location,
                IpAddress = loginDto.DeviceInfo.IpAddress
            } : null
        });

        try
        {
            // 设置设备信息的租户ID
            if (loginDto.DeviceInfo != null)
            {
                loginDto.DeviceInfo.TenantId = loginDto.TenantId;
            }

            // 验证租户
            var tenant = await _tenantRepository.FirstOrDefaultAsync(x => x.TenantId == loginDto.TenantId);
            _logger.LogInformation("租户验证结果: {TenantResult}", tenant != null ? "找到租户" : "租户不存在");

            if (tenant == null)
                throw new HbtException(_localization.L("Tenant.NotFound"), HbtConstants.ErrorCodes.NotFound);

            if (tenant.Status != 0) // 正常状态
            {
                throw new HbtException(_localization.L("Tenant.Disabled"));
            }

            // 验证用户
            var user = await _userRepository.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName && x.TenantId == loginDto.TenantId);
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
                var loginLog = new HbtLoginLog
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
                loginLog = HbtStringHelper.EnsureEntityStringLength(loginLog);
                await _loginLogRepository.InsertAsync(loginLog);

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
                    var loginLog = new HbtLoginLog
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
                    loginLog = HbtStringHelper.EnsureEntityStringLength(loginLog);
                    await _loginLogRepository.InsertAsync(loginLog);

                    throw new HbtException(_localization.L("User.InvalidCaptcha"), HbtConstants.ErrorCodes.InvalidCaptcha);
                }
            }

            // 获取用户角色和权限
            _logger.LogInformation("开始获取用户角色和权限: UserId={UserId}", user.Id);
            var roles = await _userRepository.GetUserRolesAsync(user.Id, user.TenantId);
            var permissions = await _userRepository.GetUserPermissionsAsync(user.Id, user.TenantId);
            _logger.LogInformation("用户角色和权限获取完成: RolesCount={RolesCount}, PermissionsCount={PermissionsCount}",
                roles?.Count ?? 0, permissions?.Count ?? 0);

            // 获取用户角色、部门和岗位信息
            var userRoles = await _userRoleRepository.GetListAsync(x => x.UserId == user.Id && x.TenantId == user.TenantId);
            var userDepts = await _userDeptRepository.GetListAsync(x => x.UserId == user.Id && x.TenantId == user.TenantId);
            var userPosts = await _userPostRepository.GetListAsync(x => x.UserId == user.Id && x.TenantId == user.TenantId);

            var roleId = userRoles?.FirstOrDefault()?.RoleId ?? 0;
            var deptId = userDepts?.FirstOrDefault()?.DeptId ?? 0;
            var postId = userPosts?.FirstOrDefault()?.PostId ?? 0;

            // 标记验证码为已使用
            if (!string.IsNullOrEmpty(loginDto.CaptchaToken))
            {
                _logger.LogInformation("标记验证码为已使用: Token={Token}", loginDto.CaptchaToken);
                await _captchaService.MarkAsUsedAsync(loginDto.CaptchaToken);
            }

            // 生成令牌
            _logger.LogInformation("开始生成访问令牌");
            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();
            _logger.LogInformation("令牌生成完成: AccessTokenLength={AccessTokenLength}, RefreshToken={RefreshToken}",
                accessToken?.Length ?? 0, refreshToken);

            // 缓存刷新令牌
            var cacheKey = $"refresh_token:{refreshToken}";
            _logger.LogInformation("开始缓存刷新令牌: Key={Key}", cacheKey);
            await _cache.SetAsync(cacheKey, user.Id, TimeSpan.FromDays(7));
            _logger.LogInformation("刷新令牌缓存完成");

            // 更新登录信息
            var deviceInfo = loginDto.DeviceInfo ?? new HbtDeviceInfo
            {
                DeviceId = "default",
                DeviceType = HbtDeviceType.PC, // PC
                DeviceName = "未知设备",
                DeviceModel = "未知型号",
                OsType = HbtOsType.Other, // Other
                OsVersion = "未知版本",
                BrowserType = HbtBrowserType.Other, // Other
                BrowserVersion = "未知版本",
                IpAddress = "0.0.0.0"
            };

            // 补充 IP 和位置信息
            deviceInfo.IpAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0";
            if (string.IsNullOrEmpty(deviceInfo.Location) && !string.IsNullOrEmpty(deviceInfo.IpAddress))
            {
                deviceInfo.Location = HbtIpLocationUtils.GetIpLocation(deviceInfo.IpAddress);
            }

            _logger.LogInformation("准备更新登录信息: UserId={UserId}, DeviceInfo={@DeviceInfo}",
                user.Id, new
                {
                    DeviceId = deviceInfo.DeviceId,
                    DeviceType = deviceInfo.DeviceType,
                    DeviceName = deviceInfo.DeviceName,
                    IpAddress = deviceInfo.IpAddress,
                    Location = deviceInfo.Location,
                    LoginSource = loginDto.LoginSource
                });

            await UpdateLoginSuccessAsync(user.Id, user.UserName, loginDto.LoginSource, deviceInfo);
            _logger.LogInformation("登录信息更新完成");

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
        var tenant = await _tenantRepository.GetByIdAsync(user.TenantId);
        if (tenant == null)
            throw new HbtException("租户不存在");
        if (tenant.Status != 0)
            throw new HbtException("租户已被禁用");

        // 4. 生成新令牌
        var accessToken = await _jwtHandler.GenerateAccessTokenAsync(user);
        var newRefreshToken = await _jwtHandler.GenerateRefreshTokenAsync();

        // 5. 更新缓存
        await _cache.RemoveAsync(cacheKey);
        var newCacheKey = $"refresh_token:{newRefreshToken}";
        await _cache.SetAsync(newCacheKey, userId, TimeSpan.FromDays(_jwtOptions.RefreshTokenExpirationDays));

        // 6. 获取用户角色和权限
        var roles = await _userRepository.GetUserRolesAsync(user.Id);
        var permissions = await _userRepository.GetUserPermissionsAsync(user.Id);

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
                TenantId = tenant.Id,
                TenantName = tenant.TenantName,
                Roles = roles,
                Permissions = permissions
            }
        };
    }

    /// <summary>
    /// 登出
    /// </summary>
    public async Task<bool> LogoutAsync(long userId)
    {
        // 由于使用内存缓存,登出时不需要特别处理缓存
        return await Task.FromResult(true);
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

            // 获取用户权限
            var permissions = _userRepository.GetUserPermissionsAsync(user.Id).Result;
            //_logger.LogInformation("用户权限列表: {@Permissions}", permissions);

            var claims = new List<Claim>
            {
                new Claim("user_id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("tenant_id", user.TenantId.ToString())
            };

            // 添加权限claims
            foreach (var permission in permissions)
            {
                claims.Add(new Claim("permissions", permission));
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
    /// 获取用户信息
    /// </summary>
    public async Task<HbtUserInfoDto> GetUserInfoAsync(long userId)
    {
        var user = await _userRepository.GetByIdAsync(userId)
            ?? throw new HbtException(_localization.L("User.NotFound"), HbtConstants.ErrorCodes.NotFound);

        var tenant = await _tenantRepository.FirstOrDefaultAsync(x => x.TenantId == user.TenantId);
        if (tenant == null)
            throw new HbtException(_localization.L("Tenant.NotFound"), HbtConstants.ErrorCodes.NotFound);

        if (tenant.Status != 0)
            throw new HbtException(_localization.L("Tenant.Disabled"), HbtConstants.ErrorCodes.Forbidden);

        // 获取用户角色和权限,加入租户过滤
        var roles = await _userRepository.GetUserRolesAsync(user.Id, user.TenantId);
        var permissions = await _userRepository.GetUserPermissionsAsync(user.Id, user.TenantId);

        return new HbtUserInfoDto
        {
            UserId = user.Id,
            UserName = user.UserName,
            NickName = user.NickName,
            TenantId = user.TenantId,
            TenantName = tenant.TenantName,
            Roles = roles,
            Permissions = permissions
        };
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

        var user = await _userRepository.FirstOrDefaultAsync(x => x.UserName == username);
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
        var ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0";
        var location = HbtIpLocationUtils.GetIpLocation(ipAddress);

        var loginExtend = await _loginExtendRepository.FirstOrDefaultAsync(x => x.UserId == userId);
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
            await _loginExtendRepository.InsertAsync(loginExtend);
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
    /// 更新登录成功信息
    /// </summary>
    private async Task UpdateLoginSuccessAsync(long userId, string userName, int loginSource, HbtDeviceInfo deviceInfo)
    {
        _logger.LogInformation("开始更新登录成功信息: userId={UserId}, loginSource={LoginSource}", userId, loginSource);
        try
        {
            var loginExtend = await _loginExtendRepository.FirstOrDefaultAsync(x => x.UserId == userId);
            var now = DateTime.Now;

            // 获取用户信息
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException($"用户不存在: userId={userId}");
            }

            // 获取用户角色、部门和岗位信息
            var userRoles = await _userRoleRepository.GetListAsync(x => x.UserId == userId && x.TenantId == user.TenantId);
            var userDepts = await _userDeptRepository.GetListAsync(x => x.UserId == userId && x.TenantId == user.TenantId);
            var userPosts = await _userPostRepository.GetListAsync(x => x.UserId == userId && x.TenantId == user.TenantId);

            var roleId = userRoles?.FirstOrDefault()?.RoleId ?? 0;
            var deptId = userDepts?.FirstOrDefault()?.DeptId ?? 0;
            var postId = userPosts?.FirstOrDefault()?.PostId ?? 0;

            // 获取当前IP地址
            var ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0";
            var location = HbtIpLocationUtils.GetIpLocation(ipAddress);

            // 更新设备信息的IP和位置
            deviceInfo.IpAddress = ipAddress;
            deviceInfo.Location = location;

            if (loginExtend == null)
            {
                _logger.LogInformation("创建新的登录扩展信息");
                loginExtend = new HbtLoginExtend
                {
                    UserId = userId,
                    TenantId = user.TenantId,
                    RoleId = roleId,
                    DeptId = deptId,
                    PostId = postId,
                    LoginCount = 1,
                    LoginStatus = (int)HbtLoginStatus.Success,
                    LoginSource = loginSource,
                    LoginType = (int)HbtLoginType.Password,
                    FirstLoginTime = now,
                    FirstLoginIp = ipAddress,
                    FirstLoginLocation = location,
                    FirstLoginDeviceId = deviceInfo.DeviceId ?? "default",
                    FirstLoginDeviceType = (int)deviceInfo.DeviceType,
                    FirstLoginBrowser = (int)deviceInfo.BrowserType,
                    FirstLoginOs = (int)deviceInfo.OsType,
                    LastLoginTime = now,
                    LastLoginIp = ipAddress,
                    LastLoginLocation = location,
                    LastLoginDeviceId = deviceInfo.DeviceId ?? "default",
                    LastLoginDeviceType = (int)deviceInfo.DeviceType,
                    LastLoginBrowser = (int)deviceInfo.BrowserType,
                    LastLoginOs = (int)deviceInfo.OsType,
                    ContinuousLoginDays = 1,
                    CreateBy = userId.ToString(),
                    CreateTime = now,
                    UpdateBy = userId.ToString(),
                    UpdateTime = now
                };

                // 处理登录扩展信息字段长度
                loginExtend = HbtStringHelper.EnsureEntityStringLength(loginExtend);

                _logger.LogInformation("准备插入登录扩展信息");
                await _loginExtendRepository.InsertAsync(loginExtend);
                _logger.LogInformation("登录扩展信息插入完成");
            }
            else
            {
                _logger.LogInformation("更新现有登录扩展信息");
                loginExtend.LoginCount++;
                loginExtend.LoginStatus = (int)HbtLoginStatus.Success;
                loginExtend.LoginSource = loginSource;
                loginExtend.LoginType = (int)HbtLoginType.Password;
                loginExtend.RoleId = roleId;
                loginExtend.DeptId = deptId;
                loginExtend.PostId = postId;
                loginExtend.LastLoginTime = now;
                loginExtend.LastLoginIp = ipAddress;
                loginExtend.LastLoginLocation = location;
                loginExtend.LastLoginDeviceId = deviceInfo.DeviceId ?? "default";
                loginExtend.LastLoginDeviceType = (int)deviceInfo.DeviceType;
                loginExtend.LastLoginBrowser = (int)deviceInfo.BrowserType;
                loginExtend.LastLoginOs = (int)deviceInfo.OsType;
                loginExtend.UpdateBy = userId.ToString();
                loginExtend.UpdateTime = now;

                // 如果是第一次登录，更新首次登录信息
                if (loginExtend.FirstLoginTime == null)
                {
                    loginExtend.FirstLoginTime = now;
                    loginExtend.FirstLoginIp = ipAddress;
                    loginExtend.FirstLoginLocation = location;
                    loginExtend.FirstLoginDeviceId = deviceInfo.DeviceId ?? "default";
                    loginExtend.FirstLoginDeviceType = (int)deviceInfo.DeviceType;
                    loginExtend.FirstLoginBrowser = (int)deviceInfo.BrowserType;
                    loginExtend.FirstLoginOs = (int)deviceInfo.OsType;
                }

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

                // 处理登录扩展信息字段长度
                loginExtend = HbtStringHelper.EnsureEntityStringLength(loginExtend);

                _logger.LogInformation("准备更新登录扩展信息");
                await _loginExtendRepository.UpdateAsync(loginExtend);
                _logger.LogInformation("登录扩展信息更新完成");
            }

            // 更新设备信息
            _logger.LogInformation("准备更新设备信息");
            var deviceExtend = await UpdateDeviceInfoAsync(userId, deviceInfo);
            _logger.LogInformation("设备信息更新完成");

            // 记录登录日志
            var loginLog = new HbtLoginLog
            {
                UserId = userId,
                TenantId = user.TenantId,
                UserName = userName ?? "未知",
                LoginType = HbtLoginType.Password,
                LoginStatus = HbtLoginStatus.Success,
                LoginTime = DateTime.Now,
                IpAddress = deviceInfo.IpAddress ?? "未知",
                UserAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString(),
                DeviceInfo = deviceInfo,
                DeviceExtendId = deviceExtend?.Id,
                LoginExtendId = loginExtend.Id,
                Message = $"用户 {userName} 登录成功",
                CreateBy = userId.ToString(),
                CreateTime = now,
                UpdateBy = userId.ToString(),
                UpdateTime = now
            };

            // 处理登录日志字段长度
            loginLog = HbtStringHelper.EnsureEntityStringLength(loginLog);

            await _loginLogRepository.InsertAsync(loginLog);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "更新登录信息时发生错误: {Message}", ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 更新设备信息
    /// </summary>
    private async Task<HbtDeviceExtend> UpdateDeviceInfoAsync(long userId, HbtDeviceInfo deviceInfo)
    {
        _logger.LogInformation("开始更新设备信息: userId={UserId}, deviceId={DeviceId}, tenantId={TenantId}",
            userId, deviceInfo.DeviceId, deviceInfo.TenantId);
        try
        {
            var now = DateTime.Now;
            // 获取用户信息以获取正确的租户ID
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException($"用户不存在: userId={userId}");
            }

            // 查找现有设备记录，使用设备指纹进行匹配
            var existingDevice = await _deviceExtendRepository.FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                x.TenantId == user.TenantId);

            if (existingDevice != null)
            {
                _logger.LogInformation("更新现有设备信息");
                // 更新可能变化的信息
                existingDevice.DeviceName = deviceInfo.DeviceName ?? existingDevice.DeviceName;
                existingDevice.DeviceType = (int)deviceInfo.DeviceType;
                existingDevice.BrowserType = (int)deviceInfo.BrowserType;
                existingDevice.BrowserVersion = deviceInfo.BrowserVersion;
                existingDevice.Resolution = deviceInfo.Resolution;
                existingDevice.DeviceStatus = (int)HbtDeviceStatus.Online;
                existingDevice.LastOnlineTime = now;
                existingDevice.UpdateBy = userId.ToString();
                existingDevice.UpdateTime = now;

                await _deviceExtendRepository.UpdateAsync(existingDevice);
                _logger.LogInformation("设备信息更新完成");
                return existingDevice;
            }
            else
            {
                _logger.LogInformation("创建新的设备信息");
                var newDevice = new HbtDeviceExtend
                {
                    UserId = userId,
                    TenantId = user.TenantId,
                    DeviceId = Guid.NewGuid().ToString("N"),  // 生成新的设备ID
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

                await _deviceExtendRepository.InsertAsync(newDevice);
                _logger.LogInformation("设备信息创建完成");
                return newDevice;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "更新设备信息时发生错误: {Message}", ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 记录登录日志
    /// </summary>
    private async Task LogLoginAsync(long userId, string userName, string ipAddress, string userAgent, int loginSource, bool success, string? message = null)
    {
        var log = new HbtLoginLog
        {
            UserId = userId,
            UserName = userName,
            IpAddress = ipAddress,
            UserAgent = userAgent,
            LoginSource = loginSource,
            Success = success ? 1 : 0,
            Message = message
        };

        await _loginLogRepository.InsertAsync(log);
    }

    private async Task<HbtLoginLog> CreateLoginLogAsync(HbtLoginDto loginDto, long userId, int loginStatus, string message = null)
    {
        var log = new HbtLoginLog
        {
            UserId = userId,
            UserName = loginDto.UserName,
            IpAddress = loginDto.IpAddress,
            UserAgent = loginDto.UserAgent,
            LoginType = loginDto.LoginType,
            LoginStatus = (HbtLoginStatus)loginStatus,
            LoginSource = loginDto.LoginSource,
            Success = loginStatus == (int)HbtLoginStatus.Success ? 1 : 0,
            Message = message,
            LoginTime = DateTime.Now,
            DeviceInfo = loginDto.DeviceInfo
        };

        await _loginLogRepository.InsertAsync(log);
        return log;
    }
}