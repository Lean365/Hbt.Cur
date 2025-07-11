//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLogsSeedTranslation.cs
// 创建者 : Lean365
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : 日志本地化资源数据提供类
//===================================================================

using Lean.Hbt.Domain.Entities.Routine.Core;

namespace Lean.Hbt.Infrastructure.Data.Seeds.Biz.Translation;

/// <summary>
/// 日志本地化资源数据提供类
/// </summary>
public class HbtLogsSeedTranslation
{
    /// <summary>
    /// 获取日志翻译数据
    /// </summary>
    /// <returns>翻译数据列表</returns>
    public List<HbtTranslation> GetLogsTranslations()
    {
        return new List<HbtTranslation>
        {
            // 异常日志
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.ExceptionLog.NotFound", TransValue = "异常日志不存在: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.ExceptionLog.NotFound", TransValue = "Exception log not found: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.ExceptionLog.NotFound", TransValue = "例外ログが存在しません: {0}", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.ExceptionLog.GetListFailed", TransValue = "获取异常日志列表失败", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.ExceptionLog.GetListFailed", TransValue = "Failed to get exception log list", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.ExceptionLog.GetListFailed", TransValue = "例外ログリストの取得に失敗しました", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.ExceptionLog.GetByIdFailed", TransValue = "获取异常日志详情失败: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.ExceptionLog.GetByIdFailed", TransValue = "Failed to get exception log details: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.ExceptionLog.GetByIdFailed", TransValue = "例外ログ詳細の取得に失敗しました: {0}", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.ExceptionLog.ExportTitle", TransValue = "异常日志导出", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.ExceptionLog.ExportTitle", TransValue = "Export Exception Logs", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.ExceptionLog.ExportTitle", TransValue = "例外ログのエクスポート", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.ExceptionLog.ExportFailed", TransValue = "导出异常日志失败", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.ExceptionLog.ExportFailed", TransValue = "Failed to export exception logs", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.ExceptionLog.ExportFailed", TransValue = "例外ログのエクスポートに失敗しました", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.ExceptionLog.ClearFailed", TransValue = "清空异常日志失败", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.ExceptionLog.ClearFailed", TransValue = "Failed to clear exception logs", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.ExceptionLog.ClearFailed", TransValue = "例外ログのクリアに失敗しました", ModuleName = "Audit", Status = 0 },

            // 登录日志
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.LoginLog.NotFound", TransValue = "登录日志不存在: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.LoginLog.NotFound", TransValue = "Login log not found: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.LoginLog.NotFound", TransValue = "ログインログが存在しません: {0}", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.LoginLog.GetListFailed", TransValue = "获取登录日志列表失败", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.LoginLog.GetListFailed", TransValue = "Failed to get login log list", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.LoginLog.GetListFailed", TransValue = "ログインログリストの取得に失敗しました", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.LoginLog.GetByIdFailed", TransValue = "获取登录日志详情失败: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.LoginLog.GetByIdFailed", TransValue = "Failed to get login log details: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.LoginLog.GetByIdFailed", TransValue = "ログインログ詳細の取得に失敗しました: {0}", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.LoginLog.ExportTitle", TransValue = "登录日志导出", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.LoginLog.ExportTitle", TransValue = "Export Login Logs", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.LoginLog.ExportTitle", TransValue = "ログインログのエクスポート", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.LoginLog.ExportFailed", TransValue = "导出登录日志失败", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.LoginLog.ExportFailed", TransValue = "Failed to export login logs", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.LoginLog.ExportFailed", TransValue = "ログインログのエクスポートに失敗しました", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.LoginLog.ClearFailed", TransValue = "清空登录日志失败", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.LoginLog.ClearFailed", TransValue = "Failed to clear login logs", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.LoginLog.ClearFailed", TransValue = "ログインログのクリアに失敗しました", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.LoginLog.UnlockUserFailed", TransValue = "解锁用户失败: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.LoginLog.UnlockUserFailed", TransValue = "Failed to unlock user: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.LoginLog.UnlockUserFailed", TransValue = "ユーザーのロック解除に失敗しました: {0}", ModuleName = "Audit", Status = 0 },

            // 操作日志
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.OperLog.NotFound", TransValue = "操作日志不存在: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.OperLog.NotFound", TransValue = "Operation log not found: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.OperLog.NotFound", TransValue = "操作ログが存在しません: {0}", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.OperLog.GetListFailed", TransValue = "获取操作日志列表失败", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.OperLog.GetListFailed", TransValue = "Failed to get operation log list", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.OperLog.GetListFailed", TransValue = "操作ログリストの取得に失敗しました", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.OperLog.GetByIdFailed", TransValue = "获取操作日志详情失败: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.OperLog.GetByIdFailed", TransValue = "Failed to get operation log details: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.OperLog.GetByIdFailed", TransValue = "操作ログ詳細の取得に失敗しました: {0}", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.OperLog.ExportTitle", TransValue = "操作日志导出", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.OperLog.ExportTitle", TransValue = "Export Operation Logs", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.OperLog.ExportTitle", TransValue = "操作ログのエクスポート", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.OperLog.ExportFailed", TransValue = "导出操作日志失败", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.OperLog.ExportFailed", TransValue = "Failed to export operation logs", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.OperLog.ExportFailed", TransValue = "操作ログのエクスポートに失敗しました", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.OperLog.ClearFailed", TransValue = "清空操作日志失败", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.OperLog.ClearFailed", TransValue = "Failed to clear operation logs", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.OperLog.ClearFailed", TransValue = "操作ログのクリアに失敗しました", ModuleName = "Audit", Status = 0 },

            // Quartz日志
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.QuartzLog.NotFound", TransValue = "Quartz日志不存在: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.QuartzLog.NotFound", TransValue = "Quartz log not found: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.QuartzLog.NotFound", TransValue = "Quartzログが存在しません: {0}", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.QuartzLog.GetListFailed", TransValue = "获取Quartz日志列表失败", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.QuartzLog.GetListFailed", TransValue = "Failed to get Quartz log list", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.QuartzLog.GetListFailed", TransValue = "Quartzログリストの取得に失敗しました", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.QuartzLog.GetByIdFailed", TransValue = "获取Quartz日志详情失败: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.QuartzLog.GetByIdFailed", TransValue = "Failed to get Quartz log details: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.QuartzLog.GetByIdFailed", TransValue = "Quartzログ詳細の取得に失敗しました: {0}", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.QuartzLog.ExportTitle", TransValue = "Quartz日志导出", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.QuartzLog.ExportTitle", TransValue = "Export Quartz Logs", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.QuartzLog.ExportTitle", TransValue = "Quartzログのエクスポート", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.QuartzLog.ExportFailed", TransValue = "导出Quartz日志失败", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.QuartzLog.ExportFailed", TransValue = "Failed to export Quartz logs", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.QuartzLog.ExportFailed", TransValue = "Quartzログのエクスポートに失敗しました", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.QuartzLog.ClearFailed", TransValue = "清空Quartz日志失败", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.QuartzLog.ClearFailed", TransValue = "Failed to clear Quartz logs", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.QuartzLog.ClearFailed", TransValue = "Quartzログのクリアに失敗しました", ModuleName = "Audit", Status = 0 },

            // 服务器监控
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.ServerMonitor.GetServerInfoFailed", TransValue = "获取服务器信息失败: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.ServerMonitor.GetServerInfoFailed", TransValue = "Failed to get server information: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.ServerMonitor.GetServerInfoFailed", TransValue = "サーバー情報の取得に失敗しました: {0}", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.ServerMonitor.GetNetworkInfoFailed", TransValue = "获取网络信息失败: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.ServerMonitor.GetNetworkInfoFailed", TransValue = "Failed to get network information: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.ServerMonitor.GetNetworkInfoFailed", TransValue = "ネットワーク情報の取得に失敗しました: {0}", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.ServerMonitor.GetLocationFailed", TransValue = "获取地理位置失败: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.ServerMonitor.GetLocationFailed", TransValue = "Failed to get location: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.ServerMonitor.GetLocationFailed", TransValue = "位置情報の取得に失敗しました: {0}", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.ServerMonitor.UnknownLocation", TransValue = "未知位置", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.ServerMonitor.UnknownLocation", TransValue = "Unknown location", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.ServerMonitor.UnknownLocation", TransValue = "不明な場所", ModuleName = "Audit", Status = 0 },

            // 差异日志
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.SqlDiffLog.NotFound", TransValue = "差异日志不存在: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.SqlDiffLog.NotFound", TransValue = "SQL diff log not found: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.SqlDiffLog.NotFound", TransValue = "SQL差分ログが存在しません: {0}", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.SqlDiffLog.GetListFailed", TransValue = "获取差异日志列表失败", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.SqlDiffLog.GetListFailed", TransValue = "Failed to get SQL diff log list", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.SqlDiffLog.GetListFailed", TransValue = "SQL差分ログリストの取得に失敗しました", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.SqlDiffLog.GetByIdFailed", TransValue = "获取差异日志详情失败: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.SqlDiffLog.GetByIdFailed", TransValue = "Failed to get SQL diff log details: {0}", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.SqlDiffLog.GetByIdFailed", TransValue = "SQL差分ログ詳細の取得に失敗しました: {0}", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.SqlDiffLog.ExportTitle", TransValue = "差异日志导出", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.SqlDiffLog.ExportTitle", TransValue = "Export SQL Diff Logs", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.SqlDiffLog.ExportTitle", TransValue = "SQL差分ログのエクスポート", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.SqlDiffLog.ExportFailed", TransValue = "导出差异日志失败", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.SqlDiffLog.ExportFailed", TransValue = "Failed to export SQL diff logs", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.SqlDiffLog.ExportFailed", TransValue = "SQL差分ログのエクスポートに失敗しました", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Audit.SqlDiffLog.ClearFailed", TransValue = "清空差异日志失败", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Audit.SqlDiffLog.ClearFailed", TransValue = "Failed to clear SQL diff logs", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Audit.SqlDiffLog.ClearFailed", TransValue = "SQL差分ログのクリアに失敗しました", ModuleName = "Audit", Status = 0 }
        };
    }
}
