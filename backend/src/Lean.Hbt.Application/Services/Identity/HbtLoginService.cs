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
using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices.Security;
using Lean.Hbt.Domain.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;
using Lean.Hbt.Common.Constants;
using Lean.Hbt.Domain.IServices.Caching;

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
        IOptions<HbtJwtOptions> jwtOptions)
    {
        _logger = logger;
        _userRepository = userRepository;
        _tenantRepository = tenantRepository;
        _loginPolicy = loginPolicy;
        _captchaService = captchaService;
        _jwtHandler = jwtHandler;
        _cache = cache;
        _jwtOptions = jwtOptions.Value;
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="loginDto">登录信息</param>
    /// <returns>登录结果</returns>
    public async Task<HbtLoginResultDto> LoginAsync(HbtLoginDto loginDto)
    {
        _logger.LogInformation("开始处理登录请求: {UserName}", loginDto.UserName);
        try
        {
            // 1. 验证用户名密码
            _logger.LogInformation("正在验证用户名密码");
            var user = await _userRepository.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName);
            if (user == null)
            {
                throw new HbtException("用户名或密码错误", HbtConstants.ErrorCodes.ValidationFailed);
            }

            if (user.Status != HbtStatus.Normal)
            {
                throw new HbtException("用户已被禁用", HbtConstants.ErrorCodes.ValidationFailed);
            }

            // 2. 验证租户
            _logger.LogInformation("正在验证租户: {TenantId}", loginDto.TenantId);
            HbtTenant tenant = null;
            if (user.UserType == HbtUserType.Admin)
            {
                // admin用户的tenant_id必须为0
                if (user.TenantId != 0)
                    throw HbtException.Unauthorized("用户名或密码错误");
            }
            else
            {
                // 非admin用户验证租户
                tenant = await _tenantRepository.FirstOrDefaultAsync(x => x.Id == loginDto.TenantId);
                if (tenant == null)
                    throw HbtException.NotFound($"租户不存在: {loginDto.TenantId}");
                if (tenant.Status != HbtStatus.Normal)
                    throw HbtException.Forbidden("租户已被禁用");
                // 验证用户是否属于该租户
                if (user.TenantId != loginDto.TenantId)
                    throw HbtException.Unauthorized("用户名或密码错误");
            }

            // 3. 声明所需变量
            var validationResult = (allowed: false, remainingSeconds: (int?)null);
            int remainingAttempts = 0;
            int maxAttempts = loginDto.UserName.ToLower() == "admin" ? ADMIN_MAX_FAILED_ATTEMPTS : MAX_FAILED_ATTEMPTS;
            int failedAttempts = 0;
            bool isAdmin = loginDto.UserName.ToLower() == "admin";
            int lockoutMinutes = isAdmin ? ADMIN_LOCKOUT_MINUTES : USER_LOCKOUT_MINUTES;

            // 4. 验证密码和验证码
            _logger.LogInformation("正在验证密码和验证码");
            if (!HbtPasswordUtils.VerifyHash(loginDto.Password, user.Password, user.Salt, user.Iterations))
            {
                // 先记录失败
                await _loginPolicy.RecordFailedLoginAsync(loginDto.UserName);
                if (user.UserType == HbtUserType.Admin)
                {
                    _logger.LogWarning("管理员账号登录失败: {UserName}", loginDto.UserName);
                }
                
                // 获取当前失败次数和剩余尝试次数
                remainingAttempts = await _loginPolicy.GetRemainingAttemptsAsync(loginDto.UserName);
                failedAttempts = maxAttempts - remainingAttempts;
                
                // 检查是否需要验证码
                validationResult = await _loginPolicy.ValidateLoginAttemptAsync(loginDto.UserName);
                if (!validationResult.allowed)
                {
                    remainingAttempts = await _loginPolicy.GetRemainingAttemptsAsync(loginDto.UserName);
                    failedAttempts = maxAttempts - remainingAttempts;

                    // 如果有剩余等待时间，说明账号被锁定
                    if (validationResult.remainingSeconds.HasValue && validationResult.remainingSeconds.Value > 0)
                    {
                        var minutes = Math.Ceiling(validationResult.remainingSeconds.Value / 60.0);
                        var message = string.Format("账号已被锁定，请等待{0}分钟后再试\n{1}账号锁定时间为{2}分钟", 
                            minutes, 
                            isAdmin ? "管理员" : "普通用户",
                            lockoutMinutes);
                            
                        var ex = new HbtException(message, HbtConstants.ErrorCodes.ValidationFailed);
                        ex.Data["RemainingSeconds"] = validationResult.remainingSeconds;
                        ex.Data["FailedAttempts"] = failedAttempts;
                        ex.Data["RemainingAttempts"] = remainingAttempts;
                        throw ex;
                    }
                    
                    // 需要验证码
                    if (string.IsNullOrEmpty(loginDto.CaptchaToken))
                    {
                        var ex = new HbtException("请完成验证码验证", HbtConstants.ErrorCodes.ValidationFailed);
                        ex.Data["RemainingSeconds"] = 0;
                        ex.Data["FailedAttempts"] = failedAttempts;
                        ex.Data["RemainingAttempts"] = remainingAttempts;
                        throw ex;
                    }
                    
                    if (!await _captchaService.ValidateSliderAsync(loginDto.CaptchaToken, loginDto.CaptchaOffset))
                    {
                        throw new HbtException("验证码验证失败", HbtConstants.ErrorCodes.ValidationFailed);
                    }
                }
                
                throw HbtException.Unauthorized("用户名或密码错误");
            }

            // 5. 生成令牌
            _logger.LogInformation("正在生成令牌");
            var accessToken = await _jwtHandler.GenerateAccessTokenAsync(user);
            var refreshToken = await _jwtHandler.GenerateRefreshTokenAsync();

            // 6. 记录登录成功
            _logger.LogInformation("正在记录登录成功");
            await _loginPolicy.RecordSuccessfulLoginAsync(loginDto.UserName);

            // 7. 缓存刷新令牌
            _logger.LogInformation("正在缓存刷新令牌");
            var cacheKey = $"refresh_token:{refreshToken}";
            _cache.Set(cacheKey, user.Id.ToString(), 
                TimeSpan.FromDays(_jwtOptions.RefreshTokenExpirationDays));
            _logger.LogInformation("刷新令牌已缓存");

            // 8. 获取用户角色和权限
            _logger.LogInformation("正在获取用户角色和权限");
            var roles = await _userRepository.GetUserRolesAsync(user.Id);
            var permissions = await _userRepository.GetUserPermissionsAsync(user.Id);

            // 9. 返回登录结果
            _logger.LogInformation("登录成功，正在返回结果");
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
                    TenantId = tenant?.Id ?? 0,
                    TenantName = tenant?.TenantName ?? string.Empty,
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
        if (user.Status != HbtStatus.Normal)
            throw new HbtException("用户已被禁用");

        // 3. 获取租户信息
        var tenant = await _tenantRepository.GetByIdAsync(user.TenantId);
        if (tenant == null)
            throw new HbtException("租户不存在");
        if (tenant.Status != HbtStatus.Normal)
            throw new HbtException("租户已被禁用");

        // 4. 生成新令牌
        var accessToken = await _jwtHandler.GenerateAccessTokenAsync(user);
        var newRefreshToken = await _jwtHandler.GenerateRefreshTokenAsync();

        // 5. 更新缓存
        _cache.Remove(cacheKey);
        var newCacheKey = $"refresh_token:{newRefreshToken}";
        _cache.Set(newCacheKey, userId, 
            TimeSpan.FromDays(_jwtOptions.RefreshTokenExpirationDays));

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
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim("tenant_id", user.TenantId.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
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
}