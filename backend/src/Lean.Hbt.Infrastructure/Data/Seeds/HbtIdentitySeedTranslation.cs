//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtIdentitySeedTranslation.cs
// 创建者 : Lean365
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : Identity本地化资源种子
//===================================================================

using Lean.Hbt.Domain.Entities.Core;
using Lean.Hbt.Infrastructure.Data.Contexts;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// Identity本地化资源种子
/// </summary>
public class HbtIdentitySeedTranslation
{
    private readonly IHbtRepository<HbtTranslation> _translationRepository;
    private readonly IHbtLogger _logger;
    private readonly HbtDbContext _context;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="translationRepository">翻译仓储</param>
    /// <param name="logger">日志记录器</param>
    /// <param name="context">数据库上下文</param>
    public HbtIdentitySeedTranslation(
        IHbtRepository<HbtTranslation> translationRepository,
        IHbtLogger logger,
        HbtDbContext context)
    {
        _translationRepository = translationRepository;
        _logger = logger;
        _context = context;
    }

    /// <summary>
    /// 初始化Identity本地化资源
    /// </summary>
    public async Task<(int insertCount, int updateCount)> InitializeIdentityTranslationAsync(long tenantId)
    {
        int insertCount = 0;
        int updateCount = 0;
        var defaultTranslations = new List<HbtTranslation>
        {
            // 用户相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.User.NotFound", TransValue = "用户不存在", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.User.NotFound", TransValue = "User not found", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.User.NotFound", TransValue = "ユーザーが見つかりません", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.User.Disabled", TransValue = "用户已被禁用", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.User.Disabled", TransValue = "User has been disabled", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.User.Disabled", TransValue = "ユーザーは無効になっています", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.User.InvalidCredentials", TransValue = "用户名或密码错误", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.User.InvalidCredentials", TransValue = "Invalid username or password", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.User.InvalidCredentials", TransValue = "ユーザー名またはパスワードが間違っています", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.User.InvalidCaptcha", TransValue = "验证码错误", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.User.InvalidCaptcha", TransValue = "Invalid captcha", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.User.InvalidCaptcha", TransValue = "キャプチャが間違っています", ModuleName = "Identity", Status = 0 },

            // 租户相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Tenant.NotFound", TransValue = "租户不存在", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Tenant.NotFound", TransValue = "Tenant not found", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Tenant.NotFound", TransValue = "テナントが見つかりません", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Tenant.Disabled", TransValue = "租户已被禁用", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Tenant.Disabled", TransValue = "Tenant has been disabled", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Tenant.Disabled", TransValue = "テナントは無効になっています", ModuleName = "Identity", Status = 0 },

            // 认证相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.LoginStart", TransValue = "开始处理登录请求: {0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Auth.LoginStart", TransValue = "Processing login request: {0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.LoginStart", TransValue = "ログインリクエストの処理を開始: {0}", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.ExistingUserLogin", TransValue = "检测到已在线用户登录请求: UserId={0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Auth.ExistingUserLogin", TransValue = "Detected existing user login request: UserId={0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.ExistingUserLogin", TransValue = "既存ユーザーのログインリクエストを検出: UserId={0}", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.TenantValidation", TransValue = "租户验证结果: {0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Auth.TenantValidation", TransValue = "Tenant validation result: {0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.TenantValidation", TransValue = "テナント検証結果: {0}", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.UserValidationStart", TransValue = "开始验证用户登录请求: {0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Auth.UserValidationStart", TransValue = "Starting user validation: {0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.UserValidationStart", TransValue = "ユーザー検証を開始: {0}", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.PasswordValidationStart", TransValue = "开始验证密码: UserId={0}, PasswordLength={1}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Auth.PasswordValidationStart", TransValue = "Starting password validation: UserId={0}, PasswordLength={1}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.PasswordValidationStart", TransValue = "パスワード検証を開始: UserId={0}, PasswordLength={1}", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.PasswordValidationResult", TransValue = "密码验证结果: {0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Auth.PasswordValidationResult", TransValue = "Password validation result: {0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.PasswordValidationResult", TransValue = "パスワード検証結果: {0}", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.CaptchaValidationStart", TransValue = "开始验证验证码: Token={0}, Offset={1}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Auth.CaptchaValidationStart", TransValue = "Starting captcha validation: Token={0}, Offset={1}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.CaptchaValidationStart", TransValue = "キャプチャ検証を開始: Token={0}, Offset={1}", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.CaptchaValidationResult", TransValue = "验证码验证结果: {0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Auth.CaptchaValidationResult", TransValue = "Captcha validation result: {0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.CaptchaValidationResult", TransValue = "キャプチャ検証結果: {0}", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.GetUserRolesAndPermissions", TransValue = "开始获取用户角色和权限: UserId={0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Auth.GetUserRolesAndPermissions", TransValue = "Getting user roles and permissions: UserId={0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.GetUserRolesAndPermissions", TransValue = "ユーザーロールと権限を取得: UserId={0}", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.UserRolesAndPermissionsResult", TransValue = "用户角色和权限获取完成: RolesCount={0}, PermissionsCount={1}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Auth.UserRolesAndPermissionsResult", TransValue = "User roles and permissions retrieved: RolesCount={0}, PermissionsCount={1}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.UserRolesAndPermissionsResult", TransValue = "ユーザーロールと権限を取得完了: RolesCount={0}, PermissionsCount={1}", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.GenerateTokens", TransValue = "开始生成访问令牌", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Auth.GenerateTokens", TransValue = "Generating access tokens", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.GenerateTokens", TransValue = "アクセストークンを生成中", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.TokensGenerated", TransValue = "令牌生成完成: AccessTokenLength={0}, RefreshToken={1}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Auth.TokensGenerated", TransValue = "Tokens generated: AccessTokenLength={0}, RefreshToken={1}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.TokensGenerated", TransValue = "トークン生成完了: AccessTokenLength={0}, RefreshToken={1}", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.CacheRefreshToken", TransValue = "开始缓存刷新令牌: Key=refresh_token:{0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Auth.CacheRefreshToken", TransValue = "Caching refresh token: Key=refresh_token:{0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.CacheRefreshToken", TransValue = "リフレッシュトークンをキャッシュ: Key=refresh_token:{0}", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.RefreshTokenCached", TransValue = "刷新令牌缓存完成", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Auth.RefreshTokenCached", TransValue = "Refresh token cached", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.RefreshTokenCached", TransValue = "リフレッシュトークンのキャッシュ完了", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.ProcessDeviceInfo", TransValue = "准备处理登录设备日志", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Auth.ProcessDeviceInfo", TransValue = "Processing device extension information", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.ProcessDeviceInfo", TransValue = "デバイス拡張情報を処理中", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.DeviceInfoProcessed", TransValue = "登录设备日志处理完成", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Auth.DeviceInfoProcessed", TransValue = "Device extension information processed", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.DeviceInfoProcessed", TransValue = "デバイス拡張情報の処理完了", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.ProcessLoginInfo", TransValue = "准备处理登录环境日志信息", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Auth.ProcessLoginInfo", TransValue = "Processing login extension information", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.ProcessLoginInfo", TransValue = "ログイン拡張情報を処理中", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.LoginInfoProcessed", TransValue = "登录环境日志信息处理完成", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Auth.LoginInfoProcessed", TransValue = "Login extension information processed", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.LoginInfoProcessed", TransValue = "ログイン拡張情報の処理完了", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.ProcessLoginLog", TransValue = "准备处理登录日志", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Auth.ProcessLoginLog", TransValue = "Processing login log", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.ProcessLoginLog", TransValue = "ログインログを処理中", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.LoginLogProcessed", TransValue = "登录日志处理完成", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Auth.LoginLogProcessed", TransValue = "Login log processed", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.LoginLogProcessed", TransValue = "ログインログの処理完了", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.LoginSuccess", TransValue = "登录成功: UserId={0}, UserName={1}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Auth.LoginSuccess", TransValue = "Login successful: UserId={0}, UserName={1}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.LoginSuccess", TransValue = "ログイン成功: UserId={0}, UserName={1}", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.LoginError", TransValue = "登录过程中发生错误: {0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Auth.LoginError", TransValue = "Error during login: {0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.LoginError", TransValue = "ログイン中にエラーが発生: {0}", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.ServerError", TransValue = "服务器内部错误", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Auth.ServerError", TransValue = "Internal server error", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.ServerError", TransValue = "サーバー内部エラー", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.InvalidRefreshToken", TransValue = "刷新令牌无效或已过期", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Auth.InvalidRefreshToken", TransValue = "Invalid or expired refresh token", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.InvalidRefreshToken", TransValue = "無効または期限切れのリフレッシュトークン", ModuleName = "Identity", Status = 0 },

            // 设备相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Device.Unknown", TransValue = "未知设备", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Device.Unknown", TransValue = "Unknown device", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Device.Unknown", TransValue = "不明なデバイス", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Device.UnknownModel", TransValue = "未知型号", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Device.UnknownModel", TransValue = "Unknown model", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Device.UnknownModel", TransValue = "不明なモデル", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Device.UnknownVersion", TransValue = "未知版本", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Device.UnknownVersion", TransValue = "Unknown version", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Device.UnknownVersion", TransValue = "不明なバージョン", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Device.UnknownResolution", TransValue = "未知分辨率", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Device.UnknownResolution", TransValue = "Unknown resolution", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Device.UnknownResolution", TransValue = "不明な解像度", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Device.UnknownIP", TransValue = "未知", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Device.UnknownIP", TransValue = "Unknown", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Device.UnknownIP", TransValue = "不明", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Device.UnknownLocation", TransValue = "未知", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Device.UnknownLocation", TransValue = "Unknown", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Device.UnknownLocation", TransValue = "不明", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Device.Offline", TransValue = "离线设备", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Device.Offline", TransValue = "Offline device", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Device.Offline", TransValue = "オフラインデバイス", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Device.DefaultIP", TransValue = "0.0.0.0", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Device.DefaultIP", TransValue = "0.0.0.0", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Device.DefaultIP", TransValue = "0.0.0.0", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Device.InvalidDeviceInfo", TransValue = "设备ID和连接ID不能为空", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Device.InvalidDeviceInfo", TransValue = "Device ID and connection ID cannot be empty", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Device.InvalidDeviceInfo", TransValue = "デバイスIDと接続IDは空にできません", ModuleName = "Identity", Status = 0 },

            // 部门相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Dept.NotFound", TransValue = "部门不存在: {0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Dept.NotFound", TransValue = "Department not found: {0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Dept.NotFound", TransValue = "部門が見つかりません: {0}", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Dept.HasChildren", TransValue = "选中的部门中存在子部门，无法删除", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Dept.HasChildren", TransValue = "Selected departments have children and cannot be deleted", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Dept.HasChildren", TransValue = "選択した部門に子部門が存在するため、削除できません", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Dept.ImportEmpty", TransValue = "导入数据为空", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Dept.ImportEmpty", TransValue = "Import data is empty", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Dept.ImportEmpty", TransValue = "インポートデータが空です", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Dept.ImportFailed", TransValue = "导入部门数据失败", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Dept.ImportFailed", TransValue = "Failed to import department data", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Dept.ImportFailed", TransValue = "部門データのインポートに失敗しました", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Dept.ExportFailed", TransValue = "导出部门数据失败", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Dept.ExportFailed", TransValue = "Failed to export department data", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Dept.ExportFailed", TransValue = "部門データのエクスポートに失敗しました", ModuleName = "Identity", Status = 0 },

            // 部门日志相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Dept.Log.ImportFailed", TransValue = "导入部门失败：{0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Dept.Log.ImportFailed", TransValue = "Failed to import department: {0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Dept.Log.ImportFailed", TransValue = "部門のインポートに失敗しました：{0}", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Dept.Log.ImportDataFailed", TransValue = "导入部门数据失败", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Dept.Log.ImportDataFailed", TransValue = "Failed to import department data", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Dept.Log.ImportDataFailed", TransValue = "部門データのインポートに失敗しました", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Dept.Log.ExportDataFailed", TransValue = "导出部门数据失败", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Dept.Log.ExportDataFailed", TransValue = "Failed to export department data", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Dept.Log.ExportDataFailed", TransValue = "部門データのエクスポートに失敗しました", ModuleName = "Identity", Status = 0 },

            // 部门操作相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Dept.Operation.DeleteFailed", TransValue = "部门ID {0} 不存在", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Dept.Operation.DeleteFailed", TransValue = "Department ID {0} does not exist", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Dept.Operation.DeleteFailed", TransValue = "部門ID {0} は存在しません", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Dept.Operation.UpdateFailed", TransValue = "部门ID {0} 不存在", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Dept.Operation.UpdateFailed", TransValue = "Department ID {0} does not exist", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Dept.Operation.UpdateFailed", TransValue = "部門ID {0} は存在しません", ModuleName = "Identity", Status = 0 },

            // 菜单相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Menu.NotFound", TransValue = "菜单不存在: {0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Menu.NotFound", TransValue = "Menu not found: {0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Menu.NotFound", TransValue = "メニューが存在しません: {0}", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Menu.CreateFailed", TransValue = "创建菜单失败", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Menu.CreateFailed", TransValue = "Failed to create menu", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Menu.CreateFailed", TransValue = "メニューの作成に失敗しました", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Menu.CannotBeParentOfItself", TransValue = "父菜单不能是自己", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Menu.CannotBeParentOfItself", TransValue = "Menu cannot be its own parent", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Menu.CannotBeParentOfItself", TransValue = "メニューは自身の親にすることはできません", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Menu.ParentNotFound", TransValue = "父菜单不存在", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Menu.ParentNotFound", TransValue = "Parent menu not found", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Menu.ParentNotFound", TransValue = "親メニューが存在しません", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Menu.HasChildren", TransValue = "存在子菜单,不允许删除", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Menu.HasChildren", TransValue = "Cannot delete menu with children", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Menu.HasChildren", TransValue = "子メニューが存在するため削除できません", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Menu.SelectRequired", TransValue = "请选择要删除的菜单", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Menu.SelectRequired", TransValue = "Please select menus to delete", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Menu.SelectRequired", TransValue = "削除するメニューを選択してください", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Menu.HasChildrenWithId", TransValue = "菜单 {0} 存在子菜单,不允许删除", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Menu.HasChildrenWithId", TransValue = "Menu {0} has children, cannot be deleted", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Menu.HasChildrenWithId", TransValue = "メニュー {0} には子メニューが存在するため削除できません", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Menu.ImportEmpty", TransValue = "导入数据为空", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Menu.ImportEmpty", TransValue = "Import data is empty", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Menu.ImportEmpty", TransValue = "インポートデータが空です", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Menu.Log.ImportFailed", TransValue = "导入菜单失败：{0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Menu.Log.ImportFailed", TransValue = "Failed to import menu: {0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Menu.Log.ImportFailed", TransValue = "メニューのインポートに失敗しました：{0}", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Menu.Log.ImportDataFailed", TransValue = "导入菜单数据失败", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Menu.Log.ImportDataFailed", TransValue = "Failed to import menu data", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Menu.Log.ImportDataFailed", TransValue = "メニューデータのインポートに失敗しました", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Menu.ImportFailed", TransValue = "导入菜单数据失败", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Menu.ImportFailed", TransValue = "Failed to import menu data", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Menu.ImportFailed", TransValue = "メニューデータのインポートに失敗しました", ModuleName = "Identity", Status = 0 },

            // 岗位相关翻译
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Post.NotFound", TransValue = "岗位不存在", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Post.NotFound", TransValue = "Post not found", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Post.NotFound", TransValue = "ポストが存在しません", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Post.SelectRequired", TransValue = "请选择要删除的岗位", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Post.SelectRequired", TransValue = "Please select posts to delete", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Post.SelectRequired", TransValue = "削除するポストを選択してください", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Post.HasUsers", TransValue = "选中的岗位中已有岗位分配,不能删除", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Post.HasUsers", TransValue = "Selected posts have user assignments and cannot be deleted", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Post.HasUsers", TransValue = "選択されたポストにはユーザーが割り当てられているため、削除できません", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Post.DeleteFailed", TransValue = "删除岗位失败", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Post.DeleteFailed", TransValue = "Failed to delete post", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Post.DeleteFailed", TransValue = "ポストの削除に失敗しました", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Post.Log.ImportEmptyFields", TransValue = "导入岗位失败: 岗位编码或岗位名称不能为空", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Post.Log.ImportEmptyFields", TransValue = "Import failed: Post code or name cannot be empty", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Post.Log.ImportEmptyFields", TransValue = "インポート失敗: ポストコードまたは名前が空です", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Post.Log.ImportFailed", TransValue = "导入岗位失败: {0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Post.Log.ImportFailed", TransValue = "Import failed: {0}", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Post.Log.ImportFailed", TransValue = "インポート失敗: {0}", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Post.Log.ImportDataFailed", TransValue = "导入岗位数据失败", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Post.Log.ImportDataFailed", TransValue = "Failed to import post data", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Post.Log.ImportDataFailed", TransValue = "ポストデータのインポートに失敗しました", ModuleName = "Identity", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Identity.Post.ImportFailed", TransValue = "导入岗位数据失败", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Identity.Post.ImportFailed", TransValue = "Failed to import post data", ModuleName = "Identity", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Post.ImportFailed", TransValue = "ポストデータのインポートに失敗しました", ModuleName = "Identity", Status = 0 }
        };

        foreach (var translation in defaultTranslations)
        {
            var existingTranslation = await _translationRepository.GetFirstAsync(x =>
                x.LangCode == translation.LangCode &&
                x.TransKey == translation.TransKey);

            if (existingTranslation == null)
            {

                translation.CreateBy = "Hbt365";
                translation.CreateTime = DateTime.Now;
                translation.UpdateBy = "Hbt365";
                translation.UpdateTime = DateTime.Now;

                await _translationRepository.CreateAsync(translation);
                insertCount++;
            }
            else
            {
                existingTranslation.TransValue = translation.TransValue;

                existingTranslation.UpdateBy = "Hbt365";
                existingTranslation.UpdateTime = DateTime.Now;
                await _translationRepository.UpdateAsync(existingTranslation);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }
}
