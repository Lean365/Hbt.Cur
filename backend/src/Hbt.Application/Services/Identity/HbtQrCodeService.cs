//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtQrCodeService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 二维码服务实现
//===================================================================

using System.Text.Json;
using Hbt.Application.Dtos.Identity;
using Hbt.Common.Constants;
using Hbt.Common.Helpers;
using Hbt.Common.Options;
using Hbt.Common.Utils;
using Hbt.Domain.Entities.Identity;
using Hbt.Domain.Entities.SignalR;
using Hbt.Domain.IServices.Caching;
using Hbt.Domain.IServices.Security;
using Hbt.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QRCoder;

namespace Hbt.Application.Services.Identity;

/// <summary>
/// 二维码服务实现
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public class HbtQrCodeService : HbtBaseService, IHbtQrCodeService
{
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly IHbtRepositoryFactory _repositoryFactory;
    private readonly IHbtJwtHandler _jwtHandler;
    private readonly IHbtMemoryCache _cache;
    private readonly HbtJwtOptions _jwtOptions;
    private readonly HbtQrCodeAuthOptions _qrCodeOptions;
    private readonly HbtOAuthOptions _oauthOptions;
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
    /// <param name="jwtHandler">JWT处理器</param>
    /// <param name="cache">缓存服务</param>
    /// <param name="jwtOptions">JWT配置</param>
    /// <param name="deviceIdGenerator">设备ID生成器</param>
    /// <param name="configuration">配置服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public HbtQrCodeService(
        IHbtRepositoryFactory repositoryFactory,
        IHbtJwtHandler jwtHandler,
        IHbtMemoryCache cache,
        IOptions<HbtJwtOptions> jwtOptions,
        IOptions<HbtQrCodeAuthOptions> qrCodeOptions,
        IOptions<HbtOAuthOptions> oauthOptions,
        IHbtDeviceIdGenerator deviceIdGenerator,
        IConfiguration configuration,
        IHbtLogger logger,
        IHttpContextAccessor httpContextAccessor,
        IHbtCurrentUser currentUser,
        IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        _jwtHandler = jwtHandler;
        _cache = cache;
        _jwtOptions = jwtOptions.Value;
        _qrCodeOptions = qrCodeOptions.Value;
        _oauthOptions = oauthOptions.Value;
        _deviceIdGenerator = deviceIdGenerator;
        _configuration = configuration;
    }

    /// <summary>
    /// 生成二维码
    /// </summary>
    /// <param name="request">生成二维码请求</param>
    /// <returns>二维码信息</returns>
    public async Task<HbtGenerateQrCodeResponse> GenerateQrCodeAsync(HbtGenerateQrCodeRequest request)
    {
        _logger.Info("[二维码服务] 开始生成二维码: QrCodeType={QrCodeType}", request.QrCodeType);

        try
        {
            // 生成二维码ID
            var qrCodeId = Guid.NewGuid().ToString("N");
            
            // 根据二维码类型生成不同的二维码内容
            string qrCodeContent;
            string qrCodeImage;
            
            switch (request.QrCodeType)
            {
                case HbtQrCodeType.Login:
                case HbtQrCodeType.BindDevice:
                case HbtQrCodeType.Authorize:
                    // 普通二维码登录
                    qrCodeContent = GenerateQrCodeContent(qrCodeId, request.QrCodeType);
                    qrCodeImage = GenerateQrCodeImage(qrCodeContent);
                    break;
                    
                case HbtQrCodeType.WeChatLogin:
                    if (!_qrCodeOptions.EnableWeChatLogin)
                        throw new HbtException(L("Identity.QrCode.WeChatNotEnabled"), HbtConstants.ErrorCodes.InvalidParameter);
                        
                    // 使用二维码认证配置中的微信配置
                    var weChatAppId = _configuration["QrCodeAuth:WeChat:AppId"] ?? string.Empty;
                    var weChatRedirectUri = _configuration["QrCodeAuth:WeChat:RedirectUri"] ?? string.Empty;
                    var weChatScope = _configuration["QrCodeAuth:WeChat:Scope"] ?? "snsapi_login";
                    
                    if (string.IsNullOrEmpty(weChatAppId) || string.IsNullOrEmpty(weChatRedirectUri))
                        throw new HbtException(L("Identity.QrCode.WeChatConfigMissing"), HbtConstants.ErrorCodes.InvalidParameter);
                        
                    var weChatState = HbtQrCodeHelper.GenerateWeChatState(qrCodeId, "WeChat");
                    
                    qrCodeContent = HbtQrCodeHelper.GenerateWeChatQrCodeUrl(weChatAppId, weChatRedirectUri, weChatState, weChatScope);
                    qrCodeImage = HbtQrCodeHelper.GenerateWeChatQrCodeImage(weChatAppId, weChatRedirectUri, weChatState, weChatScope, _qrCodeOptions.PixelsPerModule);
                    break;
                    
                case HbtQrCodeType.AlipayLogin:
                    if (!_qrCodeOptions.EnableAlipayLogin)
                        throw new HbtException(L("Identity.QrCode.AlipayNotEnabled"), HbtConstants.ErrorCodes.InvalidParameter);
                        
                    // 使用二维码认证配置中的支付宝配置
                    var alipayAppId = _configuration["QrCodeAuth:Alipay:AppId"] ?? string.Empty;
                    var alipayRedirectUri = _configuration["QrCodeAuth:Alipay:RedirectUri"] ?? string.Empty;
                    var alipayScope = _configuration["QrCodeAuth:Alipay:Scope"] ?? "auth_user";
                    
                    if (string.IsNullOrEmpty(alipayAppId) || string.IsNullOrEmpty(alipayRedirectUri))
                        throw new HbtException(L("Identity.QrCode.AlipayConfigMissing"), HbtConstants.ErrorCodes.InvalidParameter);
                        
                    var alipayState = HbtQrCodeHelper.GenerateAlipayState(qrCodeId, "Alipay");
                    
                    qrCodeContent = HbtQrCodeHelper.GenerateAlipayQrCodeUrl(alipayAppId, alipayRedirectUri, alipayState, alipayScope);
                    qrCodeImage = HbtQrCodeHelper.GenerateAlipayQrCodeImage(alipayAppId, alipayRedirectUri, alipayState, alipayScope, _qrCodeOptions.PixelsPerModule);
                    break;
                    
                default:
                    throw new HbtException($"不支持的二维码类型: {request.QrCodeType}", HbtConstants.ErrorCodes.InvalidParameter);
            }
            
            // 存储二维码信息到缓存
            var qrCodeData = new QrCodeData
            {
                QrCodeId = qrCodeId,
                QrCodeType = request.QrCodeType,
                Status = HbtQrCodeStatus.Waiting,
                CreateTime = DateTime.Now,
                ExpireTime = DateTime.Now.AddMinutes(_qrCodeOptions.ExpirationMinutes),
                DeviceInfo = request.DeviceInfo,
                EnvironmentInfo = request.EnvironmentInfo,
                ScannedUserId = null,
                ConfirmedUserId = null
            };
            
            var cacheKey = $"qrcode:{qrCodeId}";
            await _cache.SetAsync(cacheKey, JsonSerializer.Serialize(qrCodeData), TimeSpan.FromMinutes(_qrCodeOptions.ExpirationMinutes));

            _logger.Info("[二维码服务] 二维码生成成功: QrCodeId={QrCodeId}, Type={QrCodeType}", qrCodeId, request.QrCodeType);

            return new HbtGenerateQrCodeResponse
            {
                QrCodeId = qrCodeId,
                QrCodeContent = qrCodeContent,
                QrCodeImage = qrCodeImage,
                ExpiresIn = _qrCodeOptions.ExpirationMinutes * 60, // 转换为秒
                CreateTime = DateTime.Now
            };
        }
        catch (Exception ex)
        {
            _logger.Error("[二维码服务] 生成二维码失败: Error={Error}", ex.Message);
            throw new HbtException(L("Identity.QrCode.GenerateFailed"), HbtConstants.ErrorCodes.ServerError);
        }
    }



    /// <summary>
    /// 检查二维码状态
    /// </summary>
    /// <param name="request">检查状态请求</param>
    /// <returns>二维码状态</returns>
    public async Task<HbtCheckQrCodeStatusResponse> CheckQrCodeStatusAsync(HbtCheckQrCodeStatusRequest request)
    {
        _logger.Info("[二维码服务] 开始检查二维码状态: QrCodeId={QrCodeId}", request.QrCodeId);

        try
        {
            var cacheKey = $"qrcode:{request.QrCodeId}";
            var qrCodeDataJson = await _cache.GetAsync<string>(cacheKey);

            if (string.IsNullOrEmpty(qrCodeDataJson))
            {
                _logger.Warn("[二维码服务] 二维码不存在或已过期: QrCodeId={QrCodeId}", request.QrCodeId);
                return new HbtCheckQrCodeStatusResponse
                {
                    Status = HbtQrCodeStatus.Expired,
                    Message = L("Identity.QrCode.Expired")
                };
            }

            var qrCodeData = JsonSerializer.Deserialize<QrCodeData>(qrCodeDataJson);
            if (qrCodeData == null)
            {
                _logger.Warn("[二维码服务] 二维码数据解析失败: QrCodeId={QrCodeId}", request.QrCodeId);
                return new HbtCheckQrCodeStatusResponse
                {
                    Status = HbtQrCodeStatus.Expired,
                    Message = L("Identity.QrCode.Expired")
                };
            }

            var response = new HbtCheckQrCodeStatusResponse
            {
                Status = qrCodeData.Status,
                Message = GetStatusMessage(qrCodeData.Status)
            };

            // 如果已确认，返回用户信息和令牌
            if (qrCodeData.Status == HbtQrCodeStatus.Confirmed && qrCodeData.ConfirmedUserId.HasValue)
            {
                var user = await UserRepository.GetByIdAsync(qrCodeData.ConfirmedUserId.Value);
                if (user != null)
                {
                    var roles = await GetUserRolesAsync(user.Id);
                    var permissions = await GetUserPermissionsAsync(user.Id);

                    var accessToken = await _jwtHandler.GenerateAccessTokenAsync(user, roles.ToArray(), permissions.ToArray());
                    var refreshToken = await _jwtHandler.GenerateRefreshTokenAsync();

                    // 缓存刷新令牌
                    await _cache.SetAsync($"refresh_token:{refreshToken}", user.Id.ToString(),
                        TimeSpan.FromDays(_jwtOptions.RefreshTokenExpirationDays));

                    response.UserInfo = new HbtUserInfoDto
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
                    };
                    response.AccessToken = accessToken;
                    response.RefreshToken = refreshToken;
                    response.ExpiresIn = _jwtOptions.ExpirationMinutes * 60;

                    // 清理二维码缓存
                    await _cache.RemoveAsync(cacheKey);
                }
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.Error("[二维码服务] 检查二维码状态失败: QrCodeId={QrCodeId}, Error={Error}", request.QrCodeId, ex.Message);
            throw new HbtException(L("Identity.QrCode.CheckStatusFailed"), HbtConstants.ErrorCodes.ServerError);
        }
    }

    /// <summary>
    /// 扫描二维码
    /// </summary>
    /// <param name="qrCodeId">二维码ID</param>
    /// <param name="userId">扫描用户ID</param>
    /// <returns>是否成功</returns>
    public async Task<bool> ScanQrCodeAsync(string qrCodeId, long userId)
    {
        _logger.Info("[二维码服务] 开始扫描二维码: QrCodeId={QrCodeId}, UserId={UserId}", qrCodeId, userId);

        try
        {
            var cacheKey = $"qrcode:{qrCodeId}";
            var qrCodeDataJson = await _cache.GetAsync<string>(cacheKey);

            if (string.IsNullOrEmpty(qrCodeDataJson))
            {
                _logger.Warn("[二维码服务] 二维码不存在或已过期: QrCodeId={QrCodeId}", qrCodeId);
                return false;
            }

            var qrCodeData = JsonSerializer.Deserialize<QrCodeData>(qrCodeDataJson);
            if (qrCodeData == null)
            {
                _logger.Warn("[二维码服务] 二维码数据解析失败: QrCodeId={QrCodeId}", qrCodeId);
                return false;
            }

            if (qrCodeData.Status != HbtQrCodeStatus.Waiting)
            {
                _logger.Warn("[二维码服务] 二维码状态异常: QrCodeId={QrCodeId}, Status={Status}", qrCodeId, qrCodeData.Status);
                return false;
            }

            // 更新状态为已扫描
            qrCodeData.Status = HbtQrCodeStatus.Scanned;
            qrCodeData.ScannedUserId = userId;

            await _cache.SetAsync(cacheKey, JsonSerializer.Serialize(qrCodeData), TimeSpan.FromMinutes(5));

            _logger.Info("[二维码服务] 二维码扫描成功: QrCodeId={QrCodeId}, UserId={UserId}", qrCodeId, userId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error("[二维码服务] 扫描二维码失败: QrCodeId={QrCodeId}, UserId={UserId}, Error={Error}", qrCodeId, userId, ex.Message);
            return false;
        }
    }

    /// <summary>
    /// 确认二维码登录
    /// </summary>
    /// <param name="request">确认登录请求</param>
    /// <returns>确认结果</returns>
    public async Task<HbtConfirmQrCodeLoginResponse> ConfirmQrCodeLoginAsync(HbtConfirmQrCodeLoginRequest request)
    {
        _logger.Info("[二维码服务] 开始确认二维码登录: QrCodeId={QrCodeId}, UserId={UserId}", request.QrCodeId, request.UserId);

        try
        {
            var cacheKey = $"qrcode:{request.QrCodeId}";
            var qrCodeDataJson = await _cache.GetAsync<string>(cacheKey);

            if (string.IsNullOrEmpty(qrCodeDataJson))
            {
                _logger.Warn("[二维码服务] 二维码不存在或已过期: QrCodeId={QrCodeId}", request.QrCodeId);
                return new HbtConfirmQrCodeLoginResponse
                {
                    Success = false,
                    Message = L("Identity.QrCode.Expired")
                };
            }

            var qrCodeData = JsonSerializer.Deserialize<QrCodeData>(qrCodeDataJson);
            if (qrCodeData == null)
            {
                _logger.Warn("[二维码服务] 二维码数据解析失败: QrCodeId={QrCodeId}", request.QrCodeId);
                return new HbtConfirmQrCodeLoginResponse
                {
                    Success = false,
                    Message = L("Identity.QrCode.Expired")
                };
            }

            if (qrCodeData.Status != HbtQrCodeStatus.Scanned)
            {
                _logger.Warn("[二维码服务] 二维码状态异常: QrCodeId={QrCodeId}, Status={Status}", request.QrCodeId, qrCodeData.Status);
                return new HbtConfirmQrCodeLoginResponse
                {
                    Success = false,
                    Message = L("Identity.QrCode.InvalidStatus")
                };
            }

            if (qrCodeData.ScannedUserId != request.UserId)
            {
                _logger.Warn("[二维码服务] 用户ID不匹配: QrCodeId={QrCodeId}, ExpectedUserId={ExpectedUserId}, ActualUserId={ActualUserId}", 
                    request.QrCodeId, qrCodeData.ScannedUserId, request.UserId);
                return new HbtConfirmQrCodeLoginResponse
                {
                    Success = false,
                    Message = L("Identity.QrCode.UserMismatch")
                };
            }

            // 获取用户信息
            var user = await UserRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                _logger.Warn("[二维码服务] 用户不存在: UserId={UserId}", request.UserId);
                return new HbtConfirmQrCodeLoginResponse
                {
                    Success = false,
                    Message = L("Identity.User.NotFound")
                };
            }

            // 检查用户状态
            if (user.Status != 0)
            {
                _logger.Warn("[二维码服务] 用户状态异常: UserId={UserId}, Status={Status}", request.UserId, user.Status);
                return new HbtConfirmQrCodeLoginResponse
                {
                    Success = false,
                    Message = L("Identity.User.Disabled")
                };
            }

            // 更新状态为已确认
            qrCodeData.Status = HbtQrCodeStatus.Confirmed;
            qrCodeData.ConfirmedUserId = request.UserId;

            await _cache.SetAsync(cacheKey, JsonSerializer.Serialize(qrCodeData), TimeSpan.FromMinutes(5));

            // 记录登录日志
            var deviceInfoJson = JsonSerializer.Serialize(qrCodeData.DeviceInfo);
            var environmentInfoJson = JsonSerializer.Serialize(qrCodeData.EnvironmentInfo);
            var deviceId = _deviceIdGenerator.GenerateDeviceId(deviceInfoJson, user.Id.ToString());
            var environmentId = _deviceIdGenerator.GenerateEnvironmentId(environmentInfoJson, user.Id.ToString());

            await LoginLogAsync(user.Id, user.UserName, qrCodeData.DeviceInfo, qrCodeData.EnvironmentInfo, DateTime.Now, deviceId, environmentId);
            await LoginDevLogAsync(user.Id, qrCodeData.DeviceInfo, DateTime.Now, deviceId);
            await LoginEnvLogAsync(user.Id, qrCodeData.DeviceInfo, DateTime.Now, true, environmentId, deviceId);

            _logger.Info("[二维码服务] 二维码登录确认成功: QrCodeId={QrCodeId}, UserId={UserId}", request.QrCodeId, request.UserId);

            return new HbtConfirmQrCodeLoginResponse
            {
                Success = true,
                Message = L("Identity.QrCode.ConfirmSuccess")
            };
        }
        catch (Exception ex)
        {
            _logger.Error("[二维码服务] 确认二维码登录失败: QrCodeId={QrCodeId}, UserId={UserId}, Error={Error}", 
                request.QrCodeId, request.UserId, ex.Message);
            return new HbtConfirmQrCodeLoginResponse
            {
                Success = false,
                Message = L("Identity.QrCode.ConfirmFailed")
            };
        }
    }

    /// <summary>
    /// 拒绝二维码登录
    /// </summary>
    /// <param name="qrCodeId">二维码ID</param>
    /// <param name="userId">用户ID</param>
    /// <returns>是否成功</returns>
    public async Task<bool> RejectQrCodeLoginAsync(string qrCodeId, long userId)
    {
        _logger.Info("[二维码服务] 开始拒绝二维码登录: QrCodeId={QrCodeId}, UserId={UserId}", qrCodeId, userId);

        try
        {
            var cacheKey = $"qrcode:{qrCodeId}";
            var qrCodeDataJson = await _cache.GetAsync<string>(cacheKey);

            if (string.IsNullOrEmpty(qrCodeDataJson))
            {
                _logger.Warn("[二维码服务] 二维码不存在或已过期: QrCodeId={QrCodeId}", qrCodeId);
                return false;
            }

            var qrCodeData = JsonSerializer.Deserialize<QrCodeData>(qrCodeDataJson);
            if (qrCodeData == null)
            {
                _logger.Warn("[二维码服务] 二维码数据解析失败: QrCodeId={QrCodeId}", qrCodeId);
                return false;
            }

            if (qrCodeData.Status != HbtQrCodeStatus.Scanned)
            {
                _logger.Warn("[二维码服务] 二维码状态异常: QrCodeId={QrCodeId}, Status={Status}", qrCodeId, qrCodeData.Status);
                return false;
            }

            if (qrCodeData.ScannedUserId != userId)
            {
                _logger.Warn("[二维码服务] 用户ID不匹配: QrCodeId={QrCodeId}, ExpectedUserId={ExpectedUserId}, ActualUserId={ActualUserId}", 
                    qrCodeId, qrCodeData.ScannedUserId, userId);
                return false;
            }

            // 更新状态为已拒绝
            qrCodeData.Status = HbtQrCodeStatus.Rejected;

            await _cache.SetAsync(cacheKey, JsonSerializer.Serialize(qrCodeData), TimeSpan.FromMinutes(5));

            _logger.Info("[二维码服务] 二维码登录拒绝成功: QrCodeId={QrCodeId}, UserId={UserId}", qrCodeId, userId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error("[二维码服务] 拒绝二维码登录失败: QrCodeId={QrCodeId}, UserId={UserId}, Error={Error}", qrCodeId, userId, ex.Message);
            return false;
        }
    }

    /// <summary>
    /// 取消二维码
    /// </summary>
    /// <param name="qrCodeId">二维码ID</param>
    /// <returns>是否成功</returns>
    public async Task<bool> CancelQrCodeAsync(string qrCodeId)
    {
        _logger.Info("[二维码服务] 开始取消二维码: QrCodeId={QrCodeId}", qrCodeId);

        try
        {
            var cacheKey = $"qrcode:{qrCodeId}";
            var qrCodeDataJson = await _cache.GetAsync<string>(cacheKey);

            if (string.IsNullOrEmpty(qrCodeDataJson))
            {
                _logger.Warn("[二维码服务] 二维码不存在或已过期: QrCodeId={QrCodeId}", qrCodeId);
                return false;
            }

            var qrCodeData = JsonSerializer.Deserialize<QrCodeData>(qrCodeDataJson);
            if (qrCodeData == null)
            {
                _logger.Warn("[二维码服务] 二维码数据解析失败: QrCodeId={QrCodeId}", qrCodeId);
                return false;
            }

            if (qrCodeData.Status == HbtQrCodeStatus.Confirmed || qrCodeData.Status == HbtQrCodeStatus.Rejected)
            {
                _logger.Warn("[二维码服务] 二维码状态异常，无法取消: QrCodeId={QrCodeId}, Status={Status}", qrCodeId, qrCodeData.Status);
                return false;
            }

            // 更新状态为已取消
            qrCodeData.Status = HbtQrCodeStatus.Cancelled;

            await _cache.SetAsync(cacheKey, JsonSerializer.Serialize(qrCodeData), TimeSpan.FromMinutes(5));

            _logger.Info("[二维码服务] 二维码取消成功: QrCodeId={QrCodeId}", qrCodeId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error("[二维码服务] 取消二维码失败: QrCodeId={QrCodeId}, Error={Error}", qrCodeId, ex.Message);
            return false;
        }
    }

    /// <summary>
    /// 处理第三方登录回调
    /// </summary>
    /// <param name="code">授权码</param>
    /// <param name="state">状态参数</param>
    /// <param name="loginType">登录类型</param>
    /// <returns>登录结果</returns>
    public async Task<HbtLoginResultDto> HandleThirdPartyCallbackAsync(string code, string state, string loginType)
    {
        _logger.Info("[二维码服务] 开始处理第三方登录回调: LoginType={LoginType}, Code={Code}", loginType, code);

        try
        {
            // 解析状态参数
            var stateData = HbtQrCodeHelper.ParseLoginState(state);
            if (stateData == null)
            {
                _logger.Warn("[二维码服务] 状态参数解析失败: State={State}", state);
                throw new HbtException("无效的状态参数", HbtConstants.ErrorCodes.InvalidParameter);
            }

            // 验证状态参数
            if (stateData.LoginType != loginType)
            {
                _logger.Warn("[二维码服务] 登录类型不匹配: Expected={Expected}, Actual={Actual}", stateData.LoginType, loginType);
                throw new HbtException("登录类型不匹配", HbtConstants.ErrorCodes.InvalidParameter);
            }

            // 根据登录类型处理
            HbtUser? user = null;
            switch (loginType.ToLower())
            {
                case "wechat":
                    user = await HandleWeChatCallbackAsync(code, stateData);
                    break;
                case "alipay":
                    user = await HandleAlipayCallbackAsync(code, stateData);
                    break;
                default:
                    throw new HbtException($"不支持的登录类型: {loginType}", HbtConstants.ErrorCodes.InvalidParameter);
            }

            if (user == null)
            {
                throw new HbtException("用户信息获取失败", HbtConstants.ErrorCodes.UserNotFound);
            }

            // 生成登录令牌
            var roles = await GetUserRolesAsync(user.Id);
            var permissions = await GetUserPermissionsAsync(user.Id);

            var accessToken = await _jwtHandler.GenerateAccessTokenAsync(user, roles.ToArray(), permissions.ToArray());
            var refreshToken = await _jwtHandler.GenerateRefreshTokenAsync();

            // 缓存刷新令牌
            await _cache.SetAsync($"refresh_token:{refreshToken}", user.Id.ToString(),
                TimeSpan.FromDays(_jwtOptions.RefreshTokenExpirationDays));

            _logger.Info("[二维码服务] 第三方登录成功: LoginType={LoginType}, UserId={UserId}", loginType, user.Id);

            return new HbtLoginResultDto
            {
                Success = true,
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
                },
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = _jwtOptions.ExpirationMinutes * 60
            };
        }
        catch (Exception ex)
        {
            _logger.Error("[二维码服务] 处理第三方登录回调失败: LoginType={LoginType}, Error={Error}", loginType, ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 清理过期的二维码
    /// </summary>
    /// <returns>清理数量</returns>
    public async Task<int> CleanupExpiredQrCodesAsync()
    {
        _logger.Info("[二维码服务] 开始清理过期二维码");
        // 由于缓存会自动过期，这里主要清理数据库中的记录
        return 0;
    }

    #region 私有方法

    /// <summary>
    /// 生成二维码内容
    /// </summary>
    /// <param name="qrCodeId">二维码ID</param>
    /// <param name="qrCodeType">二维码类型</param>
    /// <returns>二维码内容</returns>
    private string GenerateQrCodeContent(string qrCodeId, HbtQrCodeType qrCodeType)
    {
        // 生成二维码内容，包含二维码ID和类型
        var baseUrl = _qrCodeOptions.BaseUrl;
        return $"{baseUrl}?id={qrCodeId}&type={(int)qrCodeType}";
    }

    /// <summary>
    /// 生成二维码图片
    /// </summary>
    /// <param name="content">二维码内容</param>
    /// <returns>Base64编码的二维码图片</returns>
    private string GenerateQrCodeImage(string content)
    {
        try
        {
            // 使用帮助类生成二维码
            var eccLevel = _qrCodeOptions.EccLevel switch
            {
                "L" => QRCoder.QRCodeGenerator.ECCLevel.L,
                "M" => QRCoder.QRCodeGenerator.ECCLevel.M,
                "Q" => QRCoder.QRCodeGenerator.ECCLevel.Q,
                "H" => QRCoder.QRCodeGenerator.ECCLevel.H,
                _ => QRCoder.QRCodeGenerator.ECCLevel.M
            };

            return HbtQrCodeHelper.GenerateQrCodePng(
                content: content,
                pixelsPerModule: _qrCodeOptions.PixelsPerModule,
                eccLevel: eccLevel
            );
        }
        catch (Exception ex)
        {
            _logger.Error("[二维码服务] 生成二维码图片失败: Error={Error}", ex.Message);
            return string.Empty;
        }
    }

    /// <summary>
    /// 获取状态消息
    /// </summary>
    /// <param name="status">二维码状态</param>
    /// <returns>状态消息</returns>
    private string GetStatusMessage(HbtQrCodeStatus status)
    {
        return status switch
        {
            HbtQrCodeStatus.Waiting => L("Identity.QrCode.Waiting"),
            HbtQrCodeStatus.Scanned => L("Identity.QrCode.Scanned"),
            HbtQrCodeStatus.Confirmed => L("Identity.QrCode.Confirmed"),
            HbtQrCodeStatus.Rejected => L("Identity.QrCode.Rejected"),
            HbtQrCodeStatus.Expired => L("Identity.QrCode.Expired"),
            HbtQrCodeStatus.Cancelled => L("Identity.QrCode.Cancelled"),
            _ => L("Identity.QrCode.Unknown")
        };
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
    /// 处理微信回调
    /// </summary>
    /// <param name="code">授权码</param>
    /// <param name="stateData">状态数据</param>
    /// <returns>用户信息</returns>
    private async Task<HbtUser?> HandleWeChatCallbackAsync(string code, ThirdPartyLoginState stateData)
    {
        // TODO: 实现微信回调处理逻辑
        // 1. 使用授权码获取微信用户信息
        // 2. 根据微信用户信息查找或创建本地用户
        // 3. 返回用户信息
        _logger.Info("[二维码服务] 处理微信回调: Code={Code}, QrCodeId={QrCodeId}", code, stateData.QrCodeId);
        return null;
    }

    /// <summary>
    /// 处理支付宝回调
    /// </summary>
    /// <param name="code">授权码</param>
    /// <param name="stateData">状态数据</param>
    /// <returns>用户信息</returns>
    private async Task<HbtUser?> HandleAlipayCallbackAsync(string code, ThirdPartyLoginState stateData)
    {
        // TODO: 实现支付宝回调处理逻辑
        // 1. 使用授权码获取支付宝用户信息
        // 2. 根据支付宝用户信息查找或创建本地用户
        // 3. 返回用户信息
        _logger.Info("[二维码服务] 处理支付宝回调: Code={Code}, QrCodeId={QrCodeId}", code, stateData.QrCodeId);
        return null;
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
            LoginType = HbtLoginType.QRCode,
            LoginStatus = HbtLoginStatus.Success,
            LoginTime = loginTime,
            IpAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
            UserAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString(),
            DeviceInfo = deviceInfo,
            LoginSuccess = 1,
            LoginMessage = "二维码登录成功",
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
            LoginType = 0, // 普通登录
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

/// <summary>
/// 二维码数据模型
/// </summary>
public class QrCodeData
{
    /// <summary>
    /// 二维码ID
    /// </summary>
    public string QrCodeId { get; set; } = string.Empty;

    /// <summary>
    /// 二维码类型
    /// </summary>
    public HbtQrCodeType QrCodeType { get; set; }

    /// <summary>
    /// 二维码状态
    /// </summary>
    public HbtQrCodeStatus Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 过期时间
    /// </summary>
    public DateTime ExpireTime { get; set; }

    /// <summary>
    /// 设备信息
    /// </summary>
    public HbtSignalRDevice? DeviceInfo { get; set; }

    /// <summary>
    /// 环境信息
    /// </summary>
    public HbtSignalREnvironment? EnvironmentInfo { get; set; }

    /// <summary>
    /// 扫描用户ID
    /// </summary>
    public long? ScannedUserId { get; set; }

    /// <summary>
    /// 确认用户ID
    /// </summary>
    public long? ConfirmedUserId { get; set; }
} 