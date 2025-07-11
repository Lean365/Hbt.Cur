//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtIdentitySeedTranslation.cs
// 创建者 : Lean365
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : Identity本地化资源数据提供类
//===================================================================

using Lean.Hbt.Domain.Entities.Routine.Core;

namespace Lean.Hbt.Infrastructure.Data.Seeds.Biz.Translation;

/// <summary>
/// Identity本地化资源数据提供类
/// </summary>
public class HbtIdentitySeedTranslation
{
    /// <summary>
    /// 获取Identity翻译数据
    /// </summary>
    /// <returns>翻译数据列表</returns>
    public List<HbtTranslation> GetIdentityTranslations()
    {
        return new List<HbtTranslation>
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
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Identity.Device.Offline", TransValue = "オフラインデバイス", ModuleName = "Identity", Status = 0 }
        };
    }
}
