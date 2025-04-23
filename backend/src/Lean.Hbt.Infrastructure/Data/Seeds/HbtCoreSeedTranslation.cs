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
using Lean.Hbt.Infrastructure.Data.Contexts;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 日志本地化资源种子
/// </summary>
public class HbtCoreSeedTranslation
{
    private readonly HbtDbContext _context;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="context">数据库上下文</param>
    /// <param name="logger">日志记录器</param>
    public HbtCoreSeedTranslation(HbtDbContext context, IHbtLogger logger)
    {
        _context = context;
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
    public async Task<(int insertCount, int updateCount)> InitializeCoreTranslationAsync()
    {
        var defaultTranslations = new List<HbtTranslation>
        {
            // 系统基础操作
            CreateTranslation("zh-CN", "Core.NotFound", "记录不存在"),
            CreateTranslation("en-US", "Core.NotFound", "Record not found"),
            CreateTranslation("ja-JP", "Core.NotFound", "レコードが存在しません"),

            CreateTranslation("zh-CN", "Core.CreateFailed", "创建记录失败"),
            CreateTranslation("en-US", "Core.CreateFailed", "Failed to create record"),
            CreateTranslation("ja-JP", "Core.CreateFailed", "レコードの作成に失敗しました"),

            CreateTranslation("zh-CN", "Core.UpdateFailed", "更新记录失败"),
            CreateTranslation("en-US", "Core.UpdateFailed", "Failed to update record"),
            CreateTranslation("ja-JP", "Core.UpdateFailed", "レコードの更新に失敗しました"),

            CreateTranslation("zh-CN", "Core.DeleteFailed", "删除记录失败"),
            CreateTranslation("en-US", "Core.DeleteFailed", "Failed to delete record"),
            CreateTranslation("ja-JP", "Core.DeleteFailed", "レコードの削除に失敗しました"),

            // 系统配置相关
            CreateTranslation("zh-CN", "Core.Config.NotFound", "配置不存在"),
            CreateTranslation("en-US", "Core.Config.NotFound", "Configuration not found"),
            CreateTranslation("ja-JP", "Core.Config.NotFound", "設定が存在しません"),

            CreateTranslation("zh-CN", "Core.Config.SaveFailed", "保存配置失败"),
            CreateTranslation("en-US", "Core.Config.SaveFailed", "Failed to save configuration"),
            CreateTranslation("ja-JP", "Core.Config.SaveFailed", "設定の保存に失敗しました"),

            // 字典管理相关
            CreateTranslation("zh-CN", "Core.Dict.NotFound", "字典不存在"),
            CreateTranslation("en-US", "Core.Dict.NotFound", "Dictionary not found"),
            CreateTranslation("ja-JP", "Core.Dict.NotFound", "辞書が存在しません"),

            CreateTranslation("zh-CN", "Core.Dict.CreateFailed", "创建字典失败"),
            CreateTranslation("en-US", "Core.Dict.CreateFailed", "Failed to create dictionary"),
            CreateTranslation("ja-JP", "Core.Dict.CreateFailed", "辞書の作成に失敗しました"),

            // 系统日志相关
            CreateTranslation("zh-CN", "Core.Log.NotFound", "日志不存在"),
            CreateTranslation("en-US", "Core.Log.NotFound", "Log not found"),
            CreateTranslation("ja-JP", "Core.Log.NotFound", "ログが存在しません"),

            CreateTranslation("zh-CN", "Core.Log.DeleteFailed", "删除日志失败"),
            CreateTranslation("en-US", "Core.Log.DeleteFailed", "Failed to delete log"),
            CreateTranslation("ja-JP", "Core.Log.DeleteFailed", "ログの削除に失敗しました"),

            // 系统错误相关
            CreateTranslation("zh-CN", "Core.Error.SystemError", "系统错误"),
            CreateTranslation("en-US", "Core.Error.SystemError", "System error"),
            CreateTranslation("ja-JP", "Core.Error.SystemError", "システムエラー"),

            CreateTranslation("zh-CN", "Core.Error.DatabaseError", "数据库错误"),
            CreateTranslation("en-US", "Core.Error.DatabaseError", "Database error"),
            CreateTranslation("ja-JP", "Core.Error.DatabaseError", "データベースエラー"),

            CreateTranslation("zh-CN", "Core.Error.NetworkError", "网络错误"),
            CreateTranslation("en-US", "Core.Error.NetworkError", "Network error"),
            CreateTranslation("ja-JP", "Core.Error.NetworkError", "ネットワークエラー")
        };

        return await CreateTranslationsAsync(defaultTranslations);
    }

    private async Task<(int insertCount, int updateCount)> CreateTranslationsAsync(List<HbtTranslation> translations)
    {
        int insertCount = 0;
        int updateCount = 0;

        foreach (var translation in translations)
        {
            var existingTranslation = await _context.Client.Queryable<HbtTranslation>()
                .FirstAsync(t => t.LangCode == translation.LangCode && t.TransKey == translation.TransKey);

            if (existingTranslation == null)
            {
                await _context.Client.Insertable(translation).ExecuteCommandAsync();
                insertCount++;
                _logger.Info($"[创建] 翻译 '{translation.TransKey}' ({translation.LangCode}) 创建成功");
            }
            else
            {
                existingTranslation.TransValue = translation.TransValue;
                existingTranslation.UpdateBy = "Hbt365";
                existingTranslation.UpdateTime = DateTime.Now;

                await _context.Client.Updateable(existingTranslation).ExecuteCommandAsync();
                updateCount++;
                _logger.Info($"[更新] 翻译 '{existingTranslation.TransKey}' ({existingTranslation.LangCode}) 更新成功");
            }
        }

        _logger.Info($"[操作] 翻译操作完成, 插入: {insertCount}, 更新: {updateCount}");
        return (insertCount, updateCount);
    }
}
