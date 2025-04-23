//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLogsSeedTranslation.cs
// 创建者 : Lean365
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : 日志本地化资源种子
//===================================================================

using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Core;
using Lean.Hbt.Domain.IServices.Extensions;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 日志本地化资源种子
/// </summary>
public class HbtLogsSeedTranslation
{
    private readonly IHbtRepository<HbtTranslation> _translationRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="translationRepository">翻译仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtLogsSeedTranslation(
        IHbtRepository<HbtTranslation> translationRepository,
        IHbtLogger logger)
    {
        _translationRepository = translationRepository;
        _logger = logger;
    }

    private HbtTranslation CreateTranslation(string langCode, string transKey, string transValue)
    {
        return new HbtTranslation
        {
            LangCode = langCode,
            TransKey = transKey,
            TransValue = transValue,
            ModuleName = "Audit",
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
    /// 初始化日志本地化资源
    /// </summary>
    public async Task<(int, int)> InitializeLogsTranslationAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var defaultTranslations = new List<HbtTranslation>
        {
            // 异常日志
            CreateTranslation("zh-CN", "Audit.ExceptionLog.NotFound", "异常日志不存在: {0}"),
            CreateTranslation("en-US", "Audit.ExceptionLog.NotFound", "Exception log not found: {0}"),
            CreateTranslation("ja-JP", "Audit.ExceptionLog.NotFound", "例外ログが存在しません: {0}"),

            CreateTranslation("zh-CN", "Audit.ExceptionLog.GetListFailed", "获取异常日志列表失败"),
            CreateTranslation("en-US", "Audit.ExceptionLog.GetListFailed", "Failed to get exception log list"),
            CreateTranslation("ja-JP", "Audit.ExceptionLog.GetListFailed", "例外ログリストの取得に失敗しました"),

            CreateTranslation("zh-CN", "Audit.ExceptionLog.GetByIdFailed", "获取异常日志详情失败: {0}"),
            CreateTranslation("en-US", "Audit.ExceptionLog.GetByIdFailed", "Failed to get exception log details: {0}"),
            CreateTranslation("ja-JP", "Audit.ExceptionLog.GetByIdFailed", "例外ログ詳細の取得に失敗しました: {0}"),

            CreateTranslation("zh-CN", "Audit.ExceptionLog.ExportTitle", "异常日志导出"),
            CreateTranslation("en-US", "Audit.ExceptionLog.ExportTitle", "Export Exception Logs"),
            CreateTranslation("ja-JP", "Audit.ExceptionLog.ExportTitle", "例外ログのエクスポート"),

            CreateTranslation("zh-CN", "Audit.ExceptionLog.ExportFailed", "导出异常日志失败"),
            CreateTranslation("en-US", "Audit.ExceptionLog.ExportFailed", "Failed to export exception logs"),
            CreateTranslation("ja-JP", "Audit.ExceptionLog.ExportFailed", "例外ログのエクスポートに失敗しました"),

            CreateTranslation("zh-CN", "Audit.ExceptionLog.ClearFailed", "清空异常日志失败"),
            CreateTranslation("en-US", "Audit.ExceptionLog.ClearFailed", "Failed to clear exception logs"),
            CreateTranslation("ja-JP", "Audit.ExceptionLog.ClearFailed", "例外ログのクリアに失敗しました"),

            // 登录日志
            CreateTranslation("zh-CN", "Audit.LoginLog.NotFound", "登录日志不存在: {0}"),
            CreateTranslation("en-US", "Audit.LoginLog.NotFound", "Login log not found: {0}"),
            CreateTranslation("ja-JP", "Audit.LoginLog.NotFound", "ログインログが存在しません: {0}"),

            CreateTranslation("zh-CN", "Audit.LoginLog.GetListFailed", "获取登录日志列表失败"),
            CreateTranslation("en-US", "Audit.LoginLog.GetListFailed", "Failed to get login log list"),
            CreateTranslation("ja-JP", "Audit.LoginLog.GetListFailed", "ログインログリストの取得に失敗しました"),

            CreateTranslation("zh-CN", "Audit.LoginLog.GetByIdFailed", "获取登录日志详情失败: {0}"),
            CreateTranslation("en-US", "Audit.LoginLog.GetByIdFailed", "Failed to get login log details: {0}"),
            CreateTranslation("ja-JP", "Audit.LoginLog.GetByIdFailed", "ログインログ詳細の取得に失敗しました: {0}"),

            CreateTranslation("zh-CN", "Audit.LoginLog.ExportTitle", "登录日志导出"),
            CreateTranslation("en-US", "Audit.LoginLog.ExportTitle", "Export Login Logs"),
            CreateTranslation("ja-JP", "Audit.LoginLog.ExportTitle", "ログインログのエクスポート"),

            CreateTranslation("zh-CN", "Audit.LoginLog.ExportFailed", "导出登录日志失败"),
            CreateTranslation("en-US", "Audit.LoginLog.ExportFailed", "Failed to export login logs"),
            CreateTranslation("ja-JP", "Audit.LoginLog.ExportFailed", "ログインログのエクスポートに失敗しました"),

            CreateTranslation("zh-CN", "Audit.LoginLog.ClearFailed", "清空登录日志失败"),
            CreateTranslation("en-US", "Audit.LoginLog.ClearFailed", "Failed to clear login logs"),
            CreateTranslation("ja-JP", "Audit.LoginLog.ClearFailed", "ログインログのクリアに失敗しました"),

            CreateTranslation("zh-CN", "Audit.LoginLog.UnlockUserFailed", "解锁用户失败: {0}"),
            CreateTranslation("en-US", "Audit.LoginLog.UnlockUserFailed", "Failed to unlock user: {0}"),
            CreateTranslation("ja-JP", "Audit.LoginLog.UnlockUserFailed", "ユーザーのロック解除に失敗しました: {0}"),

            // 操作日志
            CreateTranslation("zh-CN", "Audit.OperLog.NotFound", "操作日志不存在: {0}"),
            CreateTranslation("en-US", "Audit.OperLog.NotFound", "Operation log not found: {0}"),
            CreateTranslation("ja-JP", "Audit.OperLog.NotFound", "操作ログが存在しません: {0}"),

            CreateTranslation("zh-CN", "Audit.OperLog.GetListFailed", "获取操作日志列表失败"),
            CreateTranslation("en-US", "Audit.OperLog.GetListFailed", "Failed to get operation log list"),
            CreateTranslation("ja-JP", "Audit.OperLog.GetListFailed", "操作ログリストの取得に失敗しました"),

            CreateTranslation("zh-CN", "Audit.OperLog.GetByIdFailed", "获取操作日志详情失败: {0}"),
            CreateTranslation("en-US", "Audit.OperLog.GetByIdFailed", "Failed to get operation log details: {0}"),
            CreateTranslation("ja-JP", "Audit.OperLog.GetByIdFailed", "操作ログ詳細の取得に失敗しました: {0}"),

            CreateTranslation("zh-CN", "Audit.OperLog.ExportTitle", "操作日志导出"),
            CreateTranslation("en-US", "Audit.OperLog.ExportTitle", "Export Operation Logs"),
            CreateTranslation("ja-JP", "Audit.OperLog.ExportTitle", "操作ログのエクスポート"),

            CreateTranslation("zh-CN", "Audit.OperLog.ExportFailed", "导出操作日志失败"),
            CreateTranslation("en-US", "Audit.OperLog.ExportFailed", "Failed to export operation logs"),
            CreateTranslation("ja-JP", "Audit.OperLog.ExportFailed", "操作ログのエクスポートに失敗しました"),

            CreateTranslation("zh-CN", "Audit.OperLog.ClearFailed", "清空操作日志失败"),
            CreateTranslation("en-US", "Audit.OperLog.ClearFailed", "Failed to clear operation logs"),
            CreateTranslation("ja-JP", "Audit.OperLog.ClearFailed", "操作ログのクリアに失敗しました"),

            // Quartz日志
            CreateTranslation("zh-CN", "Audit.QuartzLog.NotFound", "Quartz日志不存在: {0}"),
            CreateTranslation("en-US", "Audit.QuartzLog.NotFound", "Quartz log not found: {0}"),
            CreateTranslation("ja-JP", "Audit.QuartzLog.NotFound", "Quartzログが存在しません: {0}"),

            CreateTranslation("zh-CN", "Audit.QuartzLog.GetListFailed", "获取Quartz日志列表失败"),
            CreateTranslation("en-US", "Audit.QuartzLog.GetListFailed", "Failed to get Quartz log list"),
            CreateTranslation("ja-JP", "Audit.QuartzLog.GetListFailed", "Quartzログリストの取得に失敗しました"),

            CreateTranslation("zh-CN", "Audit.QuartzLog.GetByIdFailed", "获取Quartz日志详情失败: {0}"),
            CreateTranslation("en-US", "Audit.QuartzLog.GetByIdFailed", "Failed to get Quartz log details: {0}"),
            CreateTranslation("ja-JP", "Audit.QuartzLog.GetByIdFailed", "Quartzログ詳細の取得に失敗しました: {0}"),

            CreateTranslation("zh-CN", "Audit.QuartzLog.ExportTitle", "Quartz日志导出"),
            CreateTranslation("en-US", "Audit.QuartzLog.ExportTitle", "Export Quartz Logs"),
            CreateTranslation("ja-JP", "Audit.QuartzLog.ExportTitle", "Quartzログのエクスポート"),

            CreateTranslation("zh-CN", "Audit.QuartzLog.ExportFailed", "导出Quartz日志失败"),
            CreateTranslation("en-US", "Audit.QuartzLog.ExportFailed", "Failed to export Quartz logs"),
            CreateTranslation("ja-JP", "Audit.QuartzLog.ExportFailed", "Quartzログのエクスポートに失敗しました"),

            CreateTranslation("zh-CN", "Audit.QuartzLog.ClearFailed", "清空Quartz日志失败"),
            CreateTranslation("en-US", "Audit.QuartzLog.ClearFailed", "Failed to clear Quartz logs"),
            CreateTranslation("ja-JP", "Audit.QuartzLog.ClearFailed", "Quartzログのクリアに失敗しました"),

            // 服务器监控
            CreateTranslation("zh-CN", "Audit.ServerMonitor.GetServerInfoFailed", "获取服务器信息失败: {0}"),
            CreateTranslation("en-US", "Audit.ServerMonitor.GetServerInfoFailed", "Failed to get server information: {0}"),
            CreateTranslation("ja-JP", "Audit.ServerMonitor.GetServerInfoFailed", "サーバー情報の取得に失敗しました: {0}"),

            CreateTranslation("zh-CN", "Audit.ServerMonitor.GetNetworkInfoFailed", "获取网络信息失败: {0}"),
            CreateTranslation("en-US", "Audit.ServerMonitor.GetNetworkInfoFailed", "Failed to get network information: {0}"),
            CreateTranslation("ja-JP", "Audit.ServerMonitor.GetNetworkInfoFailed", "ネットワーク情報の取得に失敗しました: {0}"),

            CreateTranslation("zh-CN", "Audit.ServerMonitor.GetLocationFailed", "获取地理位置失败: {0}"),
            CreateTranslation("en-US", "Audit.ServerMonitor.GetLocationFailed", "Failed to get location: {0}"),
            CreateTranslation("ja-JP", "Audit.ServerMonitor.GetLocationFailed", "位置情報の取得に失敗しました: {0}"),

            CreateTranslation("zh-CN", "Audit.ServerMonitor.UnknownLocation", "未知位置"),
            CreateTranslation("en-US", "Audit.ServerMonitor.UnknownLocation", "Unknown location"),
            CreateTranslation("ja-JP", "Audit.ServerMonitor.UnknownLocation", "不明な場所"),

            // 差异日志
            CreateTranslation("zh-CN", "Audit.SqlDiffLog.NotFound", "差异日志不存在: {0}"),
            CreateTranslation("en-US", "Audit.SqlDiffLog.NotFound", "SQL diff log not found: {0}"),
            CreateTranslation("ja-JP", "Audit.SqlDiffLog.NotFound", "SQL差分ログが存在しません: {0}"),

            CreateTranslation("zh-CN", "Audit.SqlDiffLog.GetListFailed", "获取差异日志列表失败"),
            CreateTranslation("en-US", "Audit.SqlDiffLog.GetListFailed", "Failed to get SQL diff log list"),
            CreateTranslation("ja-JP", "Audit.SqlDiffLog.GetListFailed", "SQL差分ログリストの取得に失敗しました"),

            CreateTranslation("zh-CN", "Audit.SqlDiffLog.GetByIdFailed", "获取差异日志详情失败: {0}"),
            CreateTranslation("en-US", "Audit.SqlDiffLog.GetByIdFailed", "Failed to get SQL diff log details: {0}"),
            CreateTranslation("ja-JP", "Audit.SqlDiffLog.GetByIdFailed", "SQL差分ログ詳細の取得に失敗しました: {0}"),

            CreateTranslation("zh-CN", "Audit.SqlDiffLog.ExportTitle", "差异日志导出"),
            CreateTranslation("en-US", "Audit.SqlDiffLog.ExportTitle", "Export SQL Diff Logs"),
            CreateTranslation("ja-JP", "Audit.SqlDiffLog.ExportTitle", "SQL差分ログのエクスポート"),

            CreateTranslation("zh-CN", "Audit.SqlDiffLog.ExportFailed", "导出差异日志失败"),
            CreateTranslation("en-US", "Audit.SqlDiffLog.ExportFailed", "Failed to export SQL diff logs"),
            CreateTranslation("ja-JP", "Audit.SqlDiffLog.ExportFailed", "SQL差分ログのエクスポートに失敗しました"),

            CreateTranslation("zh-CN", "Audit.SqlDiffLog.ClearFailed", "清空差异日志失败"),
            CreateTranslation("en-US", "Audit.SqlDiffLog.ClearFailed", "Failed to clear SQL diff logs"),
            CreateTranslation("ja-JP", "Audit.SqlDiffLog.ClearFailed", "SQL差分ログのクリアに失敗しました")
        };

        foreach (var translation in defaultTranslations)
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

        return (insertCount, updateCount);
    }
}
