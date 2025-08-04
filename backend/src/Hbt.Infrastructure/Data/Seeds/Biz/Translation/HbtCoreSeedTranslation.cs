//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtCoreSeedTranslation.cs
// 创建者 : Lean365
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : 核心翻译数据提供类
//===================================================================

using Hbt.Domain.Entities.Routine.Core;

namespace Hbt.Infrastructure.Data.Seeds.Biz.Translation;

/// <summary>
/// 核心翻译数据提供类
/// </summary>
public class HbtCoreSeedTranslation
{
    /// <summary>
    /// 获取核心翻译数据
    /// </summary>
    /// <returns>翻译数据列表</returns>
    public List<HbtTranslation> GetCoreTranslations()
    {
        return new List<HbtTranslation>
        {
            // 系统基础操作
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.NotFound", TransValue = "记录不存在", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.NotFound", TransValue = "Record not found", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.NotFound", TransValue = "レコードが存在しません", ModuleName = "Backend", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.CreateFailed", TransValue = "创建记录失败", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.CreateFailed", TransValue = "Failed to create record", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.CreateFailed", TransValue = "レコードの作成に失敗しました", ModuleName = "Backend", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.UpdateFailed", TransValue = "更新记录失败", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.UpdateFailed", TransValue = "Failed to update record", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.UpdateFailed", TransValue = "レコードの更新に失敗しました", ModuleName = "Backend", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.DeleteFailed", TransValue = "删除记录失败", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.DeleteFailed", TransValue = "Failed to delete record", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.DeleteFailed", TransValue = "レコードの削除に失敗しました", ModuleName = "Backend", Status = 0 },

            // 系统配置相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.Config.NotFound", TransValue = "配置不存在", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.Config.NotFound", TransValue = "Configuration not found", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.Config.NotFound", TransValue = "設定が存在しません", ModuleName = "Backend", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.Config.SaveFailed", TransValue = "保存配置失败", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.Config.SaveFailed", TransValue = "Failed to save configuration", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.Config.SaveFailed", TransValue = "設定の保存に失敗しました", ModuleName = "Backend", Status = 0 },

            // 字典管理相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.Dict.NotFound", TransValue = "字典不存在", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.Dict.NotFound", TransValue = "Dictionary not found", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.Dict.NotFound", TransValue = "辞書が存在しません", ModuleName = "Backend", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.Dict.CreateFailed", TransValue = "创建字典失败", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.Dict.CreateFailed", TransValue = "Failed to create dictionary", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.Dict.CreateFailed", TransValue = "辞書の作成に失敗しました", ModuleName = "Backend", Status = 0 },

            // 系统日志相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.Log.NotFound", TransValue = "日志不存在", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.Log.NotFound", TransValue = "Log not found", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.Log.NotFound", TransValue = "ログが存在しません", ModuleName = "Backend", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.Log.DeleteFailed", TransValue = "删除日志失败", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.Log.DeleteFailed", TransValue = "Failed to delete log", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.Log.DeleteFailed", TransValue = "ログの削除に失敗しました", ModuleName = "Backend", Status = 0 },

            // 系统错误相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.Error.SystemError", TransValue = "系统错误", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.Error.SystemError", TransValue = "System error", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.Error.SystemError", TransValue = "システムエラー", ModuleName = "Backend", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.Error.DatabaseError", TransValue = "数据库错误", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.Error.DatabaseError", TransValue = "Database error", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.Error.DatabaseError", TransValue = "データベースエラー", ModuleName = "Backend", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Core.Error.NetworkError", TransValue = "网络错误", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Core.Error.NetworkError", TransValue = "Network error", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Core.Error.NetworkError", TransValue = "ネットワークエラー", ModuleName = "Backend", Status = 0 }
        };
    }
}
