//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSmsService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 短信服务实现
//===================================================================

using System.Text.Json;
using Hbt.Cur.Application.Dtos.Identity;
using Hbt.Cur.Common.Constants;
using Hbt.Cur.Common.Helpers;
using Hbt.Cur.Common.Options;
using Hbt.Cur.Common.Utils;
using Hbt.Cur.Domain.Entities.Identity;
using Hbt.Cur.Domain.Entities.SignalR;
using Hbt.Cur.Domain.IServices.Caching;
using Hbt.Cur.Domain.IServices.Security;
using Hbt.Cur.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Hbt.Cur.Application.Services.Identity;

/// <summary>
/// 短信服务实现
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public class HbtSmsService : HbtBaseService, IHbtSmsService
{
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
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="captchaService">验证码服务</param>
    /// <param name="jwtHandler">JWT处理器</param>
    /// <param name="cache">缓存服务</param>
    /// <param name="jwtOptions">JWT配置</param>
    /// <param name="deviceIdGenerator">设备ID生成器</param>
    /// <param name="configuration">配置服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public HbtSmsService(
        IHbtRepositoryFactory repositoryFactory,
        IHbtCaptchaService captchaService,
        IHbtJwtHandler jwtHandler,
        IHbtMemoryCache cache,
        IOptions<HbtJwtOptions> jwtOptions,
        IHbtDeviceIdGenerator deviceIdGenerator,
        IConfiguration configuration,
        IHbtLogger logger,
        IHttpContextAccessor httpContextAccessor,
        IHbtCurrentUser currentUser,
        IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
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
    /// 发送短信验证码
    /// </summary>
    /// <param name="request">发送验证码请求</param>
    /// <returns>发送结果</returns>
    public async Task<HbtSendSmsCodeResponse> SendVerificationCodeAsync(HbtSendSmsCodeRequest request)
    {
        _logger.Info("[短信服务] 开始发送验证码: Phone={Phone}, CodeType={CodeType}", request.Phone, request.CodeType);

        try
        {
            // 检查发送频率限制
            var (canSend, remainingAttempts, nextSendTime) = await CheckSendLimitAsync(request.Phone, request.CodeType);
            if (!canSend)
            {
                _logger.Warn("[短信服务] 发送频率超限: Phone={Phone}, RemainingAttempts={RemainingAttempts}, NextSendTime={NextSendTime}",
                    request.Phone, remainingAttempts, nextSendTime);
                return new HbtSendSmsCodeResponse
                {
                    Success = false,
                    Message = L("Identity.Sms.SendLimitExceeded"),
                    RemainingAttempts = remainingAttempts,
                    NextSendTime = nextSendTime
                };
            }

            // 验证图形验证码（如果需要）
            if (!string.IsNullOrEmpty(request.CaptchaToken))
            {
                var captchaValid = await _captchaService.ValidateSliderAsync(request.CaptchaToken, request.CaptchaOffset);
                if (!captchaValid)
                {
                    _logger.Warn("[短信服务] 图形验证码验证失败: Phone={Phone}", request.Phone);
                    return new HbtSendSmsCodeResponse
                    {
                        Success = false,
                        Message = L("Identity.Captcha.ValidationFailed"),
                        RemainingAttempts = remainingAttempts,
                        NextSendTime = nextSendTime
                    };
                }
            }

            // 生成6位数字验证码
            var verificationCode = GenerateVerificationCode();
            _logger.Info("[短信服务] 生成验证码: Phone={Phone}, Code={Code}", request.Phone, verificationCode);

            // 存储验证码到缓存（5分钟有效期）
            var cacheKey = $"sms_code:{request.Phone}:{request.CodeType}";
            var codeData = new
            {
                Code = verificationCode,
                CreateTime = DateTime.Now,
                ExpireTime = DateTime.Now.AddMinutes(5),
                Attempts = 0
            };
            await _cache.SetAsync(cacheKey, JsonSerializer.Serialize(codeData), TimeSpan.FromMinutes(5));

            // 记录发送次数
            var sendCountKey = $"sms_send_count:{request.Phone}:{request.CodeType}";
            var sendCount = await _cache.GetAsync<int>(sendCountKey);
            await _cache.SetAsync(sendCountKey, sendCount + 1, TimeSpan.FromHours(1));

            // TODO: 调用实际的短信发送服务
            // 这里应该调用第三方短信服务API
            await SendSmsAsync(request.Phone, verificationCode, request.CodeType);

            _logger.Info("[短信服务] 验证码发送成功: Phone={Phone}", request.Phone);

            return new HbtSendSmsCodeResponse
            {
                Success = true,
                Message = L("Identity.Sms.CodeSentSuccessfully"),
                RemainingAttempts = Math.Max(0, 10 - (sendCount + 1)), // 每小时最多10次
                NextSendTime = 0
            };
        }
        catch (Exception ex)
        {
            _logger.Error("[短信服务] 发送验证码失败: Phone={Phone}, Error={Error}", request.Phone, ex.Message);
            return new HbtSendSmsCodeResponse
            {
                Success = false,
                Message = L("Identity.Sms.SendFailed"),
                RemainingAttempts = 0,
                NextSendTime = 0
            };
        }
    }

    /// <summary>
    /// 验证短信验证码
    /// </summary>
    /// <param name="phone">手机号</param>
    /// <param name="code">验证码</param>
    /// <param name="codeType">验证码类型</param>
    /// <returns>验证结果</returns>
    public async Task<bool> VerifyCodeAsync(string phone, string code, HbtSmsCodeType codeType)
    {
        _logger.Info("[短信服务] 开始验证验证码: Phone={Phone}, Code={Code}, CodeType={CodeType}", phone, code, codeType);

        try
        {
            var cacheKey = $"sms_code:{phone}:{codeType}";
            var codeDataJson = await _cache.GetAsync<string>(cacheKey);

            if (string.IsNullOrEmpty(codeDataJson))
            {
                _logger.Warn("[短信服务] 验证码不存在或已过期: Phone={Phone}", phone);
                return false;
            }

            var codeData = JsonSerializer.Deserialize<dynamic>(codeDataJson);
            var storedCode = codeData.GetProperty("Code").GetString();
            var attempts = codeData.GetProperty("Attempts").GetInt32();

            // 检查尝试次数
            if (attempts >= 5)
            {
                _logger.Warn("[短信服务] 验证码尝试次数超限: Phone={Phone}, Attempts={Attempts}", phone, attempts);
                await _cache.RemoveAsync(cacheKey);
                return false;
            }

            // 更新尝试次数
            codeData.GetProperty("Attempts").SetValue(attempts + 1);
            await _cache.SetAsync(cacheKey, JsonSerializer.Serialize(codeData), TimeSpan.FromMinutes(5));

            // 验证验证码
            if (code == storedCode)
            {
                _logger.Info("[短信服务] 验证码验证成功: Phone={Phone}", phone);
                await _cache.RemoveAsync(cacheKey); // 验证成功后删除验证码
                return true;
            }

            _logger.Warn("[短信服务] 验证码验证失败: Phone={Phone}, InputCode={InputCode}, StoredCode={StoredCode}", 
                phone, code, storedCode);
            return false;
        }
        catch (Exception ex)
        {
            _logger.Error("[短信服务] 验证验证码异常: Phone={Phone}, Error={Error}", phone, ex.Message);
            return false;
        }
    }

    /// <summary>
    /// 短信验证码登录
    /// </summary>
    /// <param name="request">登录请求</param>
    /// <returns>登录结果</returns>
    public async Task<HbtLoginResultDto> LoginAsync(HbtSmsLoginRequest request)
    {
        _logger.Info("[短信服务] 开始短信登录: Phone={Phone}", request.Phone);

        try
        {
            // 验证短信验证码
            var codeValid = await VerifyCodeAsync(request.Phone, request.VerificationCode, HbtSmsCodeType.Login);
            if (!codeValid)
            {
                _logger.Warn("[短信服务] 验证码验证失败: Phone={Phone}", request.Phone);
                throw new HbtException(L("Identity.Sms.InvalidCode"), HbtConstants.ErrorCodes.Unauthorized);
            }

            // 查找用户（通过手机号）
            var user = await UserRepository.GetFirstAsync(u => u.PhoneNumber == request.Phone);
            if (user == null)
            {
                _logger.Warn("[短信服务] 用户不存在: Phone={Phone}", request.Phone);
                throw new HbtException(L("Identity.User.NotFound"), HbtConstants.ErrorCodes.NotFound);
            }

            // 检查用户状态
            if (user.Status != 0)
            {
                _logger.Warn("[短信服务] 用户状态异常: Phone={Phone}, Status={Status}", request.Phone, user.Status);
                throw new HbtException(L("Identity.User.Disabled"));
            }

            // 检查是否是页面刷新
            bool isPageRefresh = false;
            var onlineUser = await OnlineUserRepository.GetFirstAsync(u => u.UserId == user.Id);
            if (onlineUser != null && onlineUser.OnlineStatus == 1)
            {
                isPageRefresh = true;
                _logger.Info("[短信服务] 用户已在线，页面刷新: UserId={UserId}", user.Id);
            }

            // 获取用户角色和权限
            var roles = await GetUserRolesAsync(user.Id);
            var permissions = await GetUserPermissionsAsync(user.Id);

            // 生成令牌
            var accessToken = await _jwtHandler.GenerateAccessTokenAsync(user, roles.ToArray(), permissions.ToArray());
            var refreshToken = await _jwtHandler.GenerateRefreshTokenAsync();

            // 缓存刷新令牌
            await _cache.SetAsync($"refresh_token:{refreshToken}", user.Id.ToString(),
                TimeSpan.FromDays(_jwtOptions.RefreshTokenExpirationDays));

            // 生成设备ID和环境ID
            var deviceInfoJson = JsonSerializer.Serialize(request.DeviceInfo);
            var environmentInfoJson = JsonSerializer.Serialize(request.EnvironmentInfo);
            var deviceId = _deviceIdGenerator.GenerateDeviceId(deviceInfoJson, user.Id.ToString());
            var environmentId = _deviceIdGenerator.GenerateEnvironmentId(environmentInfoJson, user.Id.ToString());

            // 记录登录日志
            var loginLog = await LoginLogAsync(user.Id, user.UserName,
                request.DeviceInfo, request.EnvironmentInfo, DateTime.Now, deviceId, environmentId);

            // 记录设备日志
            var loginDevLog = await LoginDevLogAsync(user.Id, request.DeviceInfo, DateTime.Now, deviceId);

            // 记录环境日志
            var loginEnvLog = await LoginEnvLogAsync(user.Id, request.DeviceInfo, DateTime.Now, !isPageRefresh, environmentId, deviceId);

            _logger.Info("[短信服务] 短信登录成功: UserId={UserId}, Phone={Phone}", user.Id, request.Phone);

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
            _logger.Error("[短信服务] 短信登录异常: Phone={Phone}, Error={Error}", request.Phone, ex.Message);
            throw new HbtException(L("Identity.Sms.LoginFailed"), HbtConstants.ErrorCodes.ServerError);
        }
    }

    /// <summary>
    /// 检查手机号发送频率限制
    /// </summary>
    /// <param name="phone">手机号</param>
    /// <param name="codeType">验证码类型</param>
    /// <returns>检查结果</returns>
    public async Task<(bool CanSend, int RemainingAttempts, int NextSendTime)> CheckSendLimitAsync(string phone, HbtSmsCodeType codeType)
    {
        var sendCountKey = $"sms_send_count:{phone}:{codeType}";
        var sendCount = await _cache.GetAsync<int>(sendCountKey);

        // 每小时最多10次
        if (sendCount >= 10)
        {
            return (false, 0, 3600); // 1小时后可以重新发送
        }

        return (true, 10 - sendCount, 0);
    }

    /// <summary>
    /// 清理过期的验证码
    /// </summary>
    /// <returns>清理数量</returns>
    public async Task<int> CleanupExpiredCodesAsync()
    {
        // 这里应该实现清理过期验证码的逻辑
        // 由于缓存会自动过期，这里主要清理数据库中的记录
        _logger.Info("[短信服务] 开始清理过期验证码");
        return 0;
    }

    #region 私有方法

    /// <summary>
    /// 生成6位数字验证码
    /// </summary>
    /// <returns>验证码</returns>
    private string GenerateVerificationCode()
    {
        // 使用帮助类生成验证码
        return HbtSmsHelper.GenerateSecureCode(6);
    }

    /// <summary>
    /// 发送短信
    /// </summary>
    /// <param name="phone">手机号</param>
    /// <param name="code">验证码</param>
    /// <param name="codeType">验证码类型</param>
    /// <returns>发送结果</returns>
    private async Task<bool> SendSmsAsync(string phone, string code, HbtSmsCodeType codeType)
    {
        // TODO: 实现实际的短信发送逻辑
        // 这里应该调用第三方短信服务API，如阿里云、腾讯云等
        _logger.Info("[短信服务] 模拟发送短信: Phone={Phone}, Code={Code}, CodeType={CodeType}", phone, code, codeType);
        
        // 模拟发送延迟
        await Task.Delay(100);
        
        return true;
    }

    /// <summary>
    /// 获取用户角色
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>角色列表</returns>
    private async Task<List<string>> GetUserRolesAsync(long userId)
    {
        // TODO: 实现获取用户角色的逻辑
        return new List<string> { "user" };
    }

    /// <summary>
    /// 获取用户权限
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>权限列表</returns>
    private async Task<List<string>> GetUserPermissionsAsync(long userId)
    {
        // TODO: 实现获取用户权限的逻辑
        return new List<string>();
    }

    /// <summary>
    /// 记录登录日志
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="userName">用户名</param>
    /// <param name="deviceInfo">设备信息</param>
    /// <param name="environmentInfo">环境信息</param>
    /// <param name="loginTime">登录时间</param>
    /// <param name="deviceId">设备ID</param>
    /// <param name="environmentId">环境ID</param>
    /// <returns>登录日志</returns>
    private async Task<HbtLoginLog> LoginLogAsync(long userId, string userName, HbtSignalRDevice? deviceInfo, 
        HbtSignalREnvironment? environmentInfo, DateTime loginTime, string deviceId, string environmentId)
    {
        var loginLog = new HbtLoginLog
        {
            UserId = userId,
            UserName = userName,
            LoginType = HbtLoginType.Sms,
            LoginStatus = HbtLoginStatus.Success,
            LoginTime = loginTime,
            IpAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
            UserAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString(),
            DeviceInfo = deviceInfo,
            LoginSuccess = 1,
            LoginMessage = "短信验证码登录成功",
            CreateBy = userName,
            CreateTime = DateTime.Now,
            UpdateBy = userName,
            UpdateTime = DateTime.Now
        };

        loginLog = HbtStringHelper.EnsureEntityStringLength(loginLog);
        await LoginLogRepository.CreateAsync(loginLog);
        return loginLog;
    }

    /// <summary>
    /// 记录设备日志
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="deviceInfo">设备信息</param>
    /// <param name="loginTime">登录时间</param>
    /// <param name="deviceId">设备ID</param>
    /// <returns>设备日志</returns>
    private async Task<HbtLoginDevLog> LoginDevLogAsync(long userId, HbtSignalRDevice? deviceInfo, DateTime loginTime, string deviceId)
    {
        var loginDevLog = new HbtLoginDevLog
        {
            UserId = userId,
            DeviceId = deviceId,
            DeviceType = (int)(deviceInfo?.DeviceType ?? 0),
            DeviceName = deviceInfo?.DeviceName,
            DeviceModel = deviceInfo?.DeviceModel,
            DeviceStatus = 1, // 在线状态
            LastOnlineTime = loginTime,
            CreateBy = userId.ToString(),
            CreateTime = DateTime.Now,
            UpdateBy = userId.ToString(),
            UpdateTime = DateTime.Now
        };

        loginDevLog = HbtStringHelper.EnsureEntityStringLength(loginDevLog);
        await LoginDevLogRepository.CreateAsync(loginDevLog);
        return loginDevLog;
    }

    /// <summary>
    /// 记录环境日志
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="deviceInfo">设备信息</param>
    /// <param name="loginTime">登录时间</param>
    /// <param name="isNewLogin">是否新登录</param>
    /// <param name="environmentId">环境ID</param>
    /// <param name="deviceId">设备ID</param>
    /// <returns>环境日志</returns>
    private async Task<HbtLoginEnvLog> LoginEnvLogAsync(long userId, HbtSignalRDevice? deviceInfo, DateTime loginTime, 
        bool isNewLogin, string environmentId, string deviceId)
    {
        var loginEnvLog = new HbtLoginEnvLog
        {
            UserId = userId,
            EnvironmentId = environmentId,
            LoginType = 1, // 短信登录
            LoginSource = 0, // Web
            LoginStatus = 1, // 在线
            LoginProvider = 0, // 系统
            ProviderKey = string.Empty,
            ProviderDisplayName = string.Empty,
            Status = 0, // 正常
            FirstLoginTime = isNewLogin ? loginTime : null,
            FirstLoginIp = isNewLogin ? _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() : null,
            FirstLoginDeviceId = isNewLogin ? deviceId : null,
            LastLoginTime = loginTime,
            LastLoginIp = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString(),
            LastLoginDeviceId = deviceId,
            LoginCount = 1,
            ContinuousLoginDays = 1,
            CreateBy = userId.ToString(),
            CreateTime = DateTime.Now,
            UpdateBy = userId.ToString(),
            UpdateTime = DateTime.Now
        };

        loginEnvLog = HbtStringHelper.EnsureEntityStringLength(loginEnvLog);
        await LoginEnvLogRepository.CreateAsync(loginEnvLog);
        return loginEnvLog;
    }

    #endregion
} 