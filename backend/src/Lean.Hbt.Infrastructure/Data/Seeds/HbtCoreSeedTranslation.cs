//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLogsSeedTranslation.cs
// 创建者 : Lean365
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : 日志本地化资源种子
//===================================================================

using Lean.Hbt.Domain.Entities.Core;
using Lean.Hbt.Infrastructure.Data.Contexts;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 日志本地化资源种子
/// </summary>
public class HbtCoreSeedTranslation
{
    private readonly HbtDbContext _context;
    private readonly IHbtLogger _logger;
    private readonly IHbtRepository<HbtTranslation> _translationRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="context">数据库上下文</param>
    /// <param name="logger">日志记录器</param>
    /// <param name="translationRepository">翻译仓储</param>
    public HbtCoreSeedTranslation(
        HbtDbContext context,
        IHbtLogger logger,
        IHbtRepository<HbtTranslation> translationRepository)
    {
        _context = context;
        _logger = logger;
        _translationRepository = translationRepository;
    }

    /// <summary>
    /// 初始化日志本地化资源
    /// </summary>
    public async Task<(int insertCount, int updateCount)> InitializeCoreTranslationAsync(long tenantId)
    {
        var defaultTranslations = new List<HbtTranslation>
        {
            // 系统基础操作
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.NotFound", TransValue = "记录不存在", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.NotFound", TransValue = "Record not found", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.NotFound", TransValue = "レコードが存在しません", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.CreateFailed", TransValue = "创建记录失败", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.CreateFailed", TransValue = "Failed to create record", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.CreateFailed", TransValue = "レコードの作成に失敗しました", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.UpdateFailed", TransValue = "更新记录失败", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.UpdateFailed", TransValue = "Failed to update record", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.UpdateFailed", TransValue = "レコードの更新に失敗しました", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.DeleteFailed", TransValue = "删除记录失败", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.DeleteFailed", TransValue = "Failed to delete record", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.DeleteFailed", TransValue = "レコードの削除に失敗しました", ModuleName = "Audit", Status = 0 },

            // 系统配置相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.Config.NotFound", TransValue = "配置不存在", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.Config.NotFound", TransValue = "Configuration not found", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.Config.NotFound", TransValue = "設定が存在しません", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.Config.SaveFailed", TransValue = "保存配置失败", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.Config.SaveFailed", TransValue = "Failed to save configuration", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.Config.SaveFailed", TransValue = "設定の保存に失敗しました", ModuleName = "Audit", Status = 0 },

            // 字典管理相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.Dict.NotFound", TransValue = "字典不存在", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.Dict.NotFound", TransValue = "Dictionary not found", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.Dict.NotFound", TransValue = "辞書が存在しません", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.Dict.CreateFailed", TransValue = "创建字典失败", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.Dict.CreateFailed", TransValue = "Failed to create dictionary", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.Dict.CreateFailed", TransValue = "辞書の作成に失敗しました", ModuleName = "Audit", Status = 0 },

            // 系统日志相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.Log.NotFound", TransValue = "日志不存在", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.Log.NotFound", TransValue = "Log not found", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.Log.NotFound", TransValue = "ログが存在しません", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.Log.DeleteFailed", TransValue = "删除日志失败", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.Log.DeleteFailed", TransValue = "Failed to delete log", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.Log.DeleteFailed", TransValue = "ログの削除に失敗しました", ModuleName = "Audit", Status = 0 },

            // 系统错误相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.Error.SystemError", TransValue = "系统错误", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.Error.SystemError", TransValue = "System error", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.Error.SystemError", TransValue = "システムエラー", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.Error.DatabaseError", TransValue = "数据库错误", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.Error.DatabaseError", TransValue = "Database error", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.Error.DatabaseError", TransValue = "データベースエラー", ModuleName = "Audit", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.Error.NetworkError", TransValue = "网络错误", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.Error.NetworkError", TransValue = "Network error", ModuleName = "Audit", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.Error.NetworkError", TransValue = "ネットワークエラー", ModuleName = "Audit", Status = 0 }
        };

        return await CreateTranslationsAsync(defaultTranslations, tenantId);
    }

    private async Task<(int insertCount, int updateCount)> CreateTranslationsAsync(List<HbtTranslation> translations, long tenantId)
    {
        int insertCount = 0;
        int updateCount = 0;

        foreach (var translation in translations)
        {
            var existingTranslation = await _translationRepository.GetFirstAsync(t =>
                t.LangCode == translation.LangCode && t.TransKey == translation.TransKey);

            if (existingTranslation == null)
            {
                // 统一处理审计字段和租户
                translation.CreateBy = "Hbt365";
                translation.CreateTime = DateTime.Now;
                translation.UpdateBy = "Hbt365";
                translation.UpdateTime = DateTime.Now;

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
        return (insertCount, updateCount);
    }
}
