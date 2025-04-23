//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtIdentitySeedTranslation.cs
// 创建者 : Lean365
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : Identity本地化资源种子
//===================================================================

using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Core;
using Lean.Hbt.Domain.IServices.Extensions;
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

    private HbtTranslation CreateTranslation(string langCode, string transKey, string transValue)
    {
        return new HbtTranslation
        {
            LangCode = langCode,
            TransKey = transKey,
            TransValue = transValue,
            ModuleName = "Identity",
            Status = 0,
            TransBuiltin = 1,
            TenantId = 0,
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
    }

    /// <summary>
    /// 初始化Identity本地化资源
    /// </summary>
    public async Task<(int insertCount, int updateCount)> InitializeIdentityTranslationAsync()
    {
        var defaultTranslations = new List<HbtTranslation>
        {
            // 用户相关
            CreateTranslation("zh-CN", "Identity.User.NotFound", "用户不存在"),
            CreateTranslation("en-US", "Identity.User.NotFound", "User not found"),
            CreateTranslation("ja-JP", "Identity.User.NotFound", "ユーザーが見つかりません"),

            CreateTranslation("zh-CN", "Identity.User.Disabled", "用户已被禁用"),
            CreateTranslation("en-US", "Identity.User.Disabled", "User has been disabled"),
            CreateTranslation("ja-JP", "Identity.User.Disabled", "ユーザーは無効になっています"),

            CreateTranslation("zh-CN", "Identity.User.InvalidCredentials", "用户名或密码错误"),
            CreateTranslation("en-US", "Identity.User.InvalidCredentials", "Invalid username or password"),
            CreateTranslation("ja-JP", "Identity.User.InvalidCredentials", "ユーザー名またはパスワードが間違っています"),

            CreateTranslation("zh-CN", "Identity.User.InvalidCaptcha", "验证码错误"),
            CreateTranslation("en-US", "Identity.User.InvalidCaptcha", "Invalid captcha"),
            CreateTranslation("ja-JP", "Identity.User.InvalidCaptcha", "キャプチャが間違っています"),

            // 租户相关
            CreateTranslation("zh-CN", "Identity.Tenant.NotFound", "租户不存在"),
            CreateTranslation("en-US", "Identity.Tenant.NotFound", "Tenant not found"),
            CreateTranslation("ja-JP", "Identity.Tenant.NotFound", "テナントが見つかりません"),

            CreateTranslation("zh-CN", "Identity.Tenant.Disabled", "租户已被禁用"),
            CreateTranslation("en-US", "Identity.Tenant.Disabled", "Tenant has been disabled"),
            CreateTranslation("ja-JP", "Identity.Tenant.Disabled", "テナントは無効になっています"),

            // 认证相关
            CreateTranslation("zh-CN", "Identity.Auth.LoginStart", "开始处理登录请求: {0}"),
            CreateTranslation("en-US", "Identity.Auth.LoginStart", "Processing login request: {0}"),
            CreateTranslation("ja-JP", "Identity.Auth.LoginStart", "ログインリクエストの処理を開始: {0}"),

            CreateTranslation("zh-CN", "Identity.Auth.ExistingUserLogin", "检测到已在线用户登录请求: UserId={0}"),
            CreateTranslation("en-US", "Identity.Auth.ExistingUserLogin", "Detected existing user login request: UserId={0}"),
            CreateTranslation("ja-JP", "Identity.Auth.ExistingUserLogin", "既存ユーザーのログインリクエストを検出: UserId={0}"),

            CreateTranslation("zh-CN", "Identity.Auth.TenantValidation", "租户验证结果: {0}"),
            CreateTranslation("en-US", "Identity.Auth.TenantValidation", "Tenant validation result: {0}"),
            CreateTranslation("ja-JP", "Identity.Auth.TenantValidation", "テナント検証結果: {0}"),

            CreateTranslation("zh-CN", "Identity.Auth.UserValidationStart", "开始验证用户登录请求: {0}"),
            CreateTranslation("en-US", "Identity.Auth.UserValidationStart", "Starting user validation: {0}"),
            CreateTranslation("ja-JP", "Identity.Auth.UserValidationStart", "ユーザー検証を開始: {0}"),

            CreateTranslation("zh-CN", "Identity.Auth.PasswordValidationStart", "开始验证密码: UserId={0}, PasswordLength={1}"),
            CreateTranslation("en-US", "Identity.Auth.PasswordValidationStart", "Starting password validation: UserId={0}, PasswordLength={1}"),
            CreateTranslation("ja-JP", "Identity.Auth.PasswordValidationStart", "パスワード検証を開始: UserId={0}, PasswordLength={1}"),

            CreateTranslation("zh-CN", "Identity.Auth.PasswordValidationResult", "密码验证结果: {0}"),
            CreateTranslation("en-US", "Identity.Auth.PasswordValidationResult", "Password validation result: {0}"),
            CreateTranslation("ja-JP", "Identity.Auth.PasswordValidationResult", "パスワード検証結果: {0}"),

            CreateTranslation("zh-CN", "Identity.Auth.CaptchaValidationStart", "开始验证验证码: Token={0}, Offset={1}"),
            CreateTranslation("en-US", "Identity.Auth.CaptchaValidationStart", "Starting captcha validation: Token={0}, Offset={1}"),
            CreateTranslation("ja-JP", "Identity.Auth.CaptchaValidationStart", "キャプチャ検証を開始: Token={0}, Offset={1}"),

            CreateTranslation("zh-CN", "Identity.Auth.CaptchaValidationResult", "验证码验证结果: {0}"),
            CreateTranslation("en-US", "Identity.Auth.CaptchaValidationResult", "Captcha validation result: {0}"),
            CreateTranslation("ja-JP", "Identity.Auth.CaptchaValidationResult", "キャプチャ検証結果: {0}"),

            CreateTranslation("zh-CN", "Identity.Auth.GetUserRolesAndPermissions", "开始获取用户角色和权限: UserId={0}"),
            CreateTranslation("en-US", "Identity.Auth.GetUserRolesAndPermissions", "Getting user roles and permissions: UserId={0}"),
            CreateTranslation("ja-JP", "Identity.Auth.GetUserRolesAndPermissions", "ユーザーロールと権限を取得: UserId={0}"),

            CreateTranslation("zh-CN", "Identity.Auth.UserRolesAndPermissionsResult", "用户角色和权限获取完成: RolesCount={0}, PermissionsCount={1}"),
            CreateTranslation("en-US", "Identity.Auth.UserRolesAndPermissionsResult", "User roles and permissions retrieved: RolesCount={0}, PermissionsCount={1}"),
            CreateTranslation("ja-JP", "Identity.Auth.UserRolesAndPermissionsResult", "ユーザーロールと権限を取得完了: RolesCount={0}, PermissionsCount={1}"),

            CreateTranslation("zh-CN", "Identity.Auth.GenerateTokens", "开始生成访问令牌"),
            CreateTranslation("en-US", "Identity.Auth.GenerateTokens", "Generating access tokens"),
            CreateTranslation("ja-JP", "Identity.Auth.GenerateTokens", "アクセストークンを生成中"),

            CreateTranslation("zh-CN", "Identity.Auth.TokensGenerated", "令牌生成完成: AccessTokenLength={0}, RefreshToken={1}"),
            CreateTranslation("en-US", "Identity.Auth.TokensGenerated", "Tokens generated: AccessTokenLength={0}, RefreshToken={1}"),
            CreateTranslation("ja-JP", "Identity.Auth.TokensGenerated", "トークン生成完了: AccessTokenLength={0}, RefreshToken={1}"),

            CreateTranslation("zh-CN", "Identity.Auth.CacheRefreshToken", "开始缓存刷新令牌: Key=refresh_token:{0}"),
            CreateTranslation("en-US", "Identity.Auth.CacheRefreshToken", "Caching refresh token: Key=refresh_token:{0}"),
            CreateTranslation("ja-JP", "Identity.Auth.CacheRefreshToken", "リフレッシュトークンをキャッシュ: Key=refresh_token:{0}"),

            CreateTranslation("zh-CN", "Identity.Auth.RefreshTokenCached", "刷新令牌缓存完成"),
            CreateTranslation("en-US", "Identity.Auth.RefreshTokenCached", "Refresh token cached"),
            CreateTranslation("ja-JP", "Identity.Auth.RefreshTokenCached", "リフレッシュトークンのキャッシュ完了"),

            CreateTranslation("zh-CN", "Identity.Auth.ProcessDeviceInfo", "准备处理设备扩展信息"),
            CreateTranslation("en-US", "Identity.Auth.ProcessDeviceInfo", "Processing device extension information"),
            CreateTranslation("ja-JP", "Identity.Auth.ProcessDeviceInfo", "デバイス拡張情報を処理中"),

            CreateTranslation("zh-CN", "Identity.Auth.DeviceInfoProcessed", "设备扩展信息处理完成"),
            CreateTranslation("en-US", "Identity.Auth.DeviceInfoProcessed", "Device extension information processed"),
            CreateTranslation("ja-JP", "Identity.Auth.DeviceInfoProcessed", "デバイス拡張情報の処理完了"),

            CreateTranslation("zh-CN", "Identity.Auth.ProcessLoginInfo", "准备处理登录扩展信息"),
            CreateTranslation("en-US", "Identity.Auth.ProcessLoginInfo", "Processing login extension information"),
            CreateTranslation("ja-JP", "Identity.Auth.ProcessLoginInfo", "ログイン拡張情報を処理中"),

            CreateTranslation("zh-CN", "Identity.Auth.LoginInfoProcessed", "登录扩展信息处理完成"),
            CreateTranslation("en-US", "Identity.Auth.LoginInfoProcessed", "Login extension information processed"),
            CreateTranslation("ja-JP", "Identity.Auth.LoginInfoProcessed", "ログイン拡張情報の処理完了"),

            CreateTranslation("zh-CN", "Identity.Auth.ProcessLoginLog", "准备处理登录日志"),
            CreateTranslation("en-US", "Identity.Auth.ProcessLoginLog", "Processing login log"),
            CreateTranslation("ja-JP", "Identity.Auth.ProcessLoginLog", "ログインログを処理中"),

            CreateTranslation("zh-CN", "Identity.Auth.LoginLogProcessed", "登录日志处理完成"),
            CreateTranslation("en-US", "Identity.Auth.LoginLogProcessed", "Login log processed"),
            CreateTranslation("ja-JP", "Identity.Auth.LoginLogProcessed", "ログインログの処理完了"),

            CreateTranslation("zh-CN", "Identity.Auth.LoginSuccess", "登录成功: UserId={0}, UserName={1}"),
            CreateTranslation("en-US", "Identity.Auth.LoginSuccess", "Login successful: UserId={0}, UserName={1}"),
            CreateTranslation("ja-JP", "Identity.Auth.LoginSuccess", "ログイン成功: UserId={0}, UserName={1}"),

            CreateTranslation("zh-CN", "Identity.Auth.LoginError", "登录过程中发生错误: {0}"),
            CreateTranslation("en-US", "Identity.Auth.LoginError", "Error during login: {0}"),
            CreateTranslation("ja-JP", "Identity.Auth.LoginError", "ログイン中にエラーが発生: {0}"),

            CreateTranslation("zh-CN", "Identity.Auth.ServerError", "服务器内部错误"),
            CreateTranslation("en-US", "Identity.Auth.ServerError", "Internal server error"),
            CreateTranslation("ja-JP", "Identity.Auth.ServerError", "サーバー内部エラー"),

            CreateTranslation("zh-CN", "Identity.Auth.InvalidRefreshToken", "刷新令牌无效或已过期"),
            CreateTranslation("en-US", "Identity.Auth.InvalidRefreshToken", "Invalid or expired refresh token"),
            CreateTranslation("ja-JP", "Identity.Auth.InvalidRefreshToken", "無効または期限切れのリフレッシュトークン"),

            // 设备相关
            CreateTranslation("zh-CN", "Identity.Device.Unknown", "未知设备"),
            CreateTranslation("en-US", "Identity.Device.Unknown", "Unknown device"),
            CreateTranslation("ja-JP", "Identity.Device.Unknown", "不明なデバイス"),

            CreateTranslation("zh-CN", "Identity.Device.UnknownModel", "未知型号"),
            CreateTranslation("en-US", "Identity.Device.UnknownModel", "Unknown model"),
            CreateTranslation("ja-JP", "Identity.Device.UnknownModel", "不明なモデル"),

            CreateTranslation("zh-CN", "Identity.Device.UnknownVersion", "未知版本"),
            CreateTranslation("en-US", "Identity.Device.UnknownVersion", "Unknown version"),
            CreateTranslation("ja-JP", "Identity.Device.UnknownVersion", "不明なバージョン"),

            CreateTranslation("zh-CN", "Identity.Device.UnknownResolution", "未知分辨率"),
            CreateTranslation("en-US", "Identity.Device.UnknownResolution", "Unknown resolution"),
            CreateTranslation("ja-JP", "Identity.Device.UnknownResolution", "不明な解像度"),

            CreateTranslation("zh-CN", "Identity.Device.UnknownIP", "未知"),
            CreateTranslation("en-US", "Identity.Device.UnknownIP", "Unknown"),
            CreateTranslation("ja-JP", "Identity.Device.UnknownIP", "不明"),

            CreateTranslation("zh-CN", "Identity.Device.UnknownLocation", "未知"),
            CreateTranslation("en-US", "Identity.Device.UnknownLocation", "Unknown"),
            CreateTranslation("ja-JP", "Identity.Device.UnknownLocation", "不明"),

            CreateTranslation("zh-CN", "Identity.Device.Offline", "离线设备"),
            CreateTranslation("en-US", "Identity.Device.Offline", "Offline device"),
            CreateTranslation("ja-JP", "Identity.Device.Offline", "オフラインデバイス"),

            CreateTranslation("zh-CN", "Identity.Device.DefaultIP", "0.0.0.0"),
            CreateTranslation("en-US", "Identity.Device.DefaultIP", "0.0.0.0"),
            CreateTranslation("ja-JP", "Identity.Device.DefaultIP", "0.0.0.0"),

            CreateTranslation("zh-CN", "Identity.Device.InvalidDeviceInfo", "设备ID和连接ID不能为空"),
            CreateTranslation("en-US", "Identity.Device.InvalidDeviceInfo", "Device ID and connection ID cannot be empty"),
            CreateTranslation("ja-JP", "Identity.Device.InvalidDeviceInfo", "デバイスIDと接続IDは空にできません"),

            // 部门相关
            CreateTranslation("zh-CN", "Identity.Dept.NotFound", "部门不存在: {0}"),
            CreateTranslation("en-US", "Identity.Dept.NotFound", "Department not found: {0}"),
            CreateTranslation("ja-JP", "Identity.Dept.NotFound", "部門が見つかりません: {0}"),

            CreateTranslation("zh-CN", "Identity.Dept.HasChildren", "选中的部门中存在子部门，无法删除"),
            CreateTranslation("en-US", "Identity.Dept.HasChildren", "Selected departments have children and cannot be deleted"),
            CreateTranslation("ja-JP", "Identity.Dept.HasChildren", "選択した部門に子部門が存在するため、削除できません"),

            CreateTranslation("zh-CN", "Identity.Dept.ImportEmpty", "导入数据为空"),
            CreateTranslation("en-US", "Identity.Dept.ImportEmpty", "Import data is empty"),
            CreateTranslation("ja-JP", "Identity.Dept.ImportEmpty", "インポートデータが空です"),

            CreateTranslation("zh-CN", "Identity.Dept.ImportFailed", "导入部门数据失败"),
            CreateTranslation("en-US", "Identity.Dept.ImportFailed", "Failed to import department data"),
            CreateTranslation("ja-JP", "Identity.Dept.ImportFailed", "部門データのインポートに失敗しました"),

            CreateTranslation("zh-CN", "Identity.Dept.ExportFailed", "导出部门数据失败"),
            CreateTranslation("en-US", "Identity.Dept.ExportFailed", "Failed to export department data"),
            CreateTranslation("ja-JP", "Identity.Dept.ExportFailed", "部門データのエクスポートに失敗しました"),

            // 部门日志相关
            CreateTranslation("zh-CN", "Identity.Dept.Log.ImportFailed", "导入部门失败：{0}"),
            CreateTranslation("en-US", "Identity.Dept.Log.ImportFailed", "Failed to import department: {0}"),
            CreateTranslation("ja-JP", "Identity.Dept.Log.ImportFailed", "部門のインポートに失敗しました：{0}"),

            CreateTranslation("zh-CN", "Identity.Dept.Log.ImportDataFailed", "导入部门数据失败"),
            CreateTranslation("en-US", "Identity.Dept.Log.ImportDataFailed", "Failed to import department data"),
            CreateTranslation("ja-JP", "Identity.Dept.Log.ImportDataFailed", "部門データのインポートに失敗しました"),

            CreateTranslation("zh-CN", "Identity.Dept.Log.ExportDataFailed", "导出部门数据失败"),
            CreateTranslation("en-US", "Identity.Dept.Log.ExportDataFailed", "Failed to export department data"),
            CreateTranslation("ja-JP", "Identity.Dept.Log.ExportDataFailed", "部門データのエクスポートに失敗しました"),

            // 部门操作相关
            CreateTranslation("zh-CN", "Identity.Dept.Operation.DeleteFailed", "部门ID {0} 不存在"),
            CreateTranslation("en-US", "Identity.Dept.Operation.DeleteFailed", "Department ID {0} does not exist"),
            CreateTranslation("ja-JP", "Identity.Dept.Operation.DeleteFailed", "部門ID {0} は存在しません"),

            CreateTranslation("zh-CN", "Identity.Dept.Operation.UpdateFailed", "部门ID {0} 不存在"),
            CreateTranslation("en-US", "Identity.Dept.Operation.UpdateFailed", "Department ID {0} does not exist"),
            CreateTranslation("ja-JP", "Identity.Dept.Operation.UpdateFailed", "部門ID {0} は存在しません"),

            // 菜单相关
            CreateTranslation("zh-CN", "Identity.Menu.NotFound", "菜单不存在: {0}"),
            CreateTranslation("en-US", "Identity.Menu.NotFound", "Menu not found: {0}"),
            CreateTranslation("ja-JP", "Identity.Menu.NotFound", "メニューが存在しません: {0}"),

            CreateTranslation("zh-CN", "Identity.Menu.CreateFailed", "创建菜单失败"),
            CreateTranslation("en-US", "Identity.Menu.CreateFailed", "Failed to create menu"),
            CreateTranslation("ja-JP", "Identity.Menu.CreateFailed", "メニューの作成に失敗しました"),

            CreateTranslation("zh-CN", "Identity.Menu.CannotBeParentOfItself", "父菜单不能是自己"),
            CreateTranslation("en-US", "Identity.Menu.CannotBeParentOfItself", "Menu cannot be its own parent"),
            CreateTranslation("ja-JP", "Identity.Menu.CannotBeParentOfItself", "メニューは自身の親にすることはできません"),

            CreateTranslation("zh-CN", "Identity.Menu.ParentNotFound", "父菜单不存在"),
            CreateTranslation("en-US", "Identity.Menu.ParentNotFound", "Parent menu not found"),
            CreateTranslation("ja-JP", "Identity.Menu.ParentNotFound", "親メニューが存在しません"),

            CreateTranslation("zh-CN", "Identity.Menu.HasChildren", "存在子菜单,不允许删除"),
            CreateTranslation("en-US", "Identity.Menu.HasChildren", "Cannot delete menu with children"),
            CreateTranslation("ja-JP", "Identity.Menu.HasChildren", "子メニューが存在するため削除できません"),

            CreateTranslation("zh-CN", "Identity.Menu.SelectRequired", "请选择要删除的菜单"),
            CreateTranslation("en-US", "Identity.Menu.SelectRequired", "Please select menus to delete"),
            CreateTranslation("ja-JP", "Identity.Menu.SelectRequired", "削除するメニューを選択してください"),

            CreateTranslation("zh-CN", "Identity.Menu.HasChildrenWithId", "菜单 {0} 存在子菜单,不允许删除"),
            CreateTranslation("en-US", "Identity.Menu.HasChildrenWithId", "Menu {0} has children, cannot be deleted"),
            CreateTranslation("ja-JP", "Identity.Menu.HasChildrenWithId", "メニュー {0} には子メニューが存在するため削除できません"),

            CreateTranslation("zh-CN", "Identity.Menu.ImportEmpty", "导入数据为空"),
            CreateTranslation("en-US", "Identity.Menu.ImportEmpty", "Import data is empty"),
            CreateTranslation("ja-JP", "Identity.Menu.ImportEmpty", "インポートデータが空です"),

            CreateTranslation("zh-CN", "Identity.Menu.Log.ImportFailed", "导入菜单失败：{0}"),
            CreateTranslation("en-US", "Identity.Menu.Log.ImportFailed", "Failed to import menu: {0}"),
            CreateTranslation("ja-JP", "Identity.Menu.Log.ImportFailed", "メニューのインポートに失敗しました：{0}"),

            CreateTranslation("zh-CN", "Identity.Menu.Log.ImportDataFailed", "导入菜单数据失败"),
            CreateTranslation("en-US", "Identity.Menu.Log.ImportDataFailed", "Failed to import menu data"),
            CreateTranslation("ja-JP", "Identity.Menu.Log.ImportDataFailed", "メニューデータのインポートに失敗しました"),

            CreateTranslation("zh-CN", "Identity.Menu.ImportFailed", "导入菜单数据失败"),
            CreateTranslation("en-US", "Identity.Menu.ImportFailed", "Failed to import menu data"),
            CreateTranslation("ja-JP", "Identity.Menu.ImportFailed", "メニューデータのインポートに失敗しました"),

            // 岗位相关翻译
            CreateTranslation("zh-CN", "Identity.Post.NotFound", "岗位不存在"),
            CreateTranslation("en-US", "Identity.Post.NotFound", "Post not found"),
            CreateTranslation("ja-JP", "Identity.Post.NotFound", "ポストが存在しません"),

            CreateTranslation("zh-CN", "Identity.Post.SelectRequired", "请选择要删除的岗位"),
            CreateTranslation("en-US", "Identity.Post.SelectRequired", "Please select posts to delete"),
            CreateTranslation("ja-JP", "Identity.Post.SelectRequired", "削除するポストを選択してください"),

            CreateTranslation("zh-CN", "Identity.Post.HasUsers", "选中的岗位中已有岗位分配,不能删除"),
            CreateTranslation("en-US", "Identity.Post.HasUsers", "Selected posts have user assignments and cannot be deleted"),
            CreateTranslation("ja-JP", "Identity.Post.HasUsers", "選択されたポストにはユーザーが割り当てられているため、削除できません"),

            CreateTranslation("zh-CN", "Identity.Post.DeleteFailed", "删除岗位失败"),
            CreateTranslation("en-US", "Identity.Post.DeleteFailed", "Failed to delete post"),
            CreateTranslation("ja-JP", "Identity.Post.DeleteFailed", "ポストの削除に失敗しました"),

            CreateTranslation("zh-CN", "Identity.Post.Log.ImportEmptyFields", "导入岗位失败: 岗位编码或岗位名称不能为空"),
            CreateTranslation("en-US", "Identity.Post.Log.ImportEmptyFields", "Import failed: Post code or name cannot be empty"),
            CreateTranslation("ja-JP", "Identity.Post.Log.ImportEmptyFields", "インポート失敗: ポストコードまたは名前が空です"),

            CreateTranslation("zh-CN", "Identity.Post.Log.ImportFailed", "导入岗位失败: {0}"),
            CreateTranslation("en-US", "Identity.Post.Log.ImportFailed", "Import failed: {0}"),
            CreateTranslation("ja-JP", "Identity.Post.Log.ImportFailed", "インポート失敗: {0}"),

            CreateTranslation("zh-CN", "Identity.Post.Log.ImportDataFailed", "导入岗位数据失败"),
            CreateTranslation("en-US", "Identity.Post.Log.ImportDataFailed", "Failed to import post data"),
            CreateTranslation("ja-JP", "Identity.Post.Log.ImportDataFailed", "ポストデータのインポートに失敗しました"),

            CreateTranslation("zh-CN", "Identity.Post.ImportFailed", "导入岗位数据失败"),
            CreateTranslation("en-US", "Identity.Post.ImportFailed", "Failed to import post data"),
            CreateTranslation("ja-JP", "Identity.Post.ImportFailed", "ポストデータのインポートに失敗しました")
        };

        await CreateTranslationsAsync(defaultTranslations);
        return (0, 0);
    }

    private async Task CreateTranslationsAsync(List<HbtTranslation> translations)
    {
        int insertCount = 0;
        int updateCount = 0;

        foreach (var translation in translations)
        {
            var existingTranslation = await _translationRepository.GetFirstAsync(t => 
                t.LangCode == translation.LangCode && 
                t.TransKey == translation.TransKey);

            if (existingTranslation == null)
            {
                await _translationRepository.CreateAsync(translation);
                insertCount++;
                _logger.Info($"[创建] 翻译 '{translation.TransKey}' ({translation.LangCode}) 创建成功");
            }
            else
            {
                existingTranslation.TransValue = translation.TransValue;
                existingTranslation.UpdateBy = "Hbt365";
                existingTranslation.UpdateTime = DateTime.Now;

                await _translationRepository.UpdateAsync(existingTranslation);
                updateCount++;
                _logger.Info($"[更新] 翻译 '{existingTranslation.TransKey}' ({existingTranslation.LangCode}) 更新成功");
            }
        }

        _logger.Info($"[操作] 翻译操作完成, 插入: {insertCount}, 更新: {updateCount}");
    }
}
