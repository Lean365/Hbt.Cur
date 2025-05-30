//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtRoutineSeedTranslation.cs
// 创建者 : Lean365
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : 定时任务本地化资源种子
//===================================================================

using Lean.Hbt.Domain.Entities.Core;
using Lean.Hbt.Infrastructure.Data.Contexts;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 定时任务本地化资源种子
/// </summary>
public class HbtRoutineSeedTranslation
{
    private readonly HbtDbContext _context;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="context">数据库上下文</param>
    /// <param name="logger">日志记录器</param>
    public HbtRoutineSeedTranslation(HbtDbContext context, IHbtLogger logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 初始化定时任务本地化资源
    /// </summary>
    public async Task<(int insertCount, int updateCount)> InitializeRoutineTranslationAsync()
    {
        var defaultTranslations = new List<HbtTranslation>
        {
            // 日常办公基础操作
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Routine.NotFound", TransValue = "日常办公记录不存在", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Routine.NotFound", TransValue = "Routine record not found", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Routine.NotFound", TransValue = "日常業務記録が存在しません", ModuleName = "Routine", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Routine.CreateFailed", TransValue = "创建日常办公记录失败", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Routine.CreateFailed", TransValue = "Failed to create routine record", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Routine.CreateFailed", TransValue = "日常業務記録の作成に失敗しました", ModuleName = "Routine", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Routine.UpdateFailed", TransValue = "更新日常办公记录失败", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Routine.UpdateFailed", TransValue = "Failed to update routine record", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Routine.UpdateFailed", TransValue = "日常業務記録の更新に失敗しました", ModuleName = "Routine", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Routine.DeleteFailed", TransValue = "删除日常办公记录失败", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Routine.DeleteFailed", TransValue = "Failed to delete routine record", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Routine.DeleteFailed", TransValue = "日常業務記録の削除に失敗しました", ModuleName = "Routine", Status = 0 },

            // 日程安排相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Routine.Schedule.NotFound", TransValue = "日程安排不存在", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Routine.Schedule.NotFound", TransValue = "Schedule not found", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Routine.Schedule.NotFound", TransValue = "スケジュールが存在しません", ModuleName = "Routine", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Routine.Schedule.CreateFailed", TransValue = "创建日程安排失败", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Routine.Schedule.CreateFailed", TransValue = "Failed to create schedule", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Routine.Schedule.CreateFailed", TransValue = "スケジュールの作成に失敗しました", ModuleName = "Routine", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Routine.Schedule.UpdateFailed", TransValue = "更新日程安排失败", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Routine.Schedule.UpdateFailed", TransValue = "Failed to update schedule", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Routine.Schedule.UpdateFailed", TransValue = "スケジュールの更新に失敗しました", ModuleName = "Routine", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Routine.Schedule.DeleteFailed", TransValue = "删除日程安排失败", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Routine.Schedule.DeleteFailed", TransValue = "Failed to delete schedule", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Routine.Schedule.DeleteFailed", TransValue = "スケジュールの削除に失敗しました", ModuleName = "Routine", Status = 0 },

            // 待办事项相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Routine.Todo.NotFound", TransValue = "待办事项不存在", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Routine.Todo.NotFound", TransValue = "Todo item not found", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Routine.Todo.NotFound", TransValue = "ToDo項目が存在しません", ModuleName = "Routine", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Routine.Todo.CreateFailed", TransValue = "创建待办事项失败", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Routine.Todo.CreateFailed", TransValue = "Failed to create todo item", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Routine.Todo.CreateFailed", TransValue = "ToDo項目の作成に失敗しました", ModuleName = "Routine", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Routine.Todo.UpdateFailed", TransValue = "更新待办事项失败", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Routine.Todo.UpdateFailed", TransValue = "Failed to update todo item", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Routine.Todo.UpdateFailed", TransValue = "ToDo項目の更新に失敗しました", ModuleName = "Routine", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Routine.Todo.DeleteFailed", TransValue = "删除待办事项失败", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Routine.Todo.DeleteFailed", TransValue = "Failed to delete todo item", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Routine.Todo.DeleteFailed", TransValue = "ToDo項目の削除に失敗しました", ModuleName = "Routine", Status = 0 },

            // 会议管理相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Routine.Meeting.NotFound", TransValue = "会议记录不存在", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Routine.Meeting.NotFound", TransValue = "Meeting record not found", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Routine.Meeting.NotFound", TransValue = "会議記録が存在しません", ModuleName = "Routine", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Routine.Meeting.CreateFailed", TransValue = "创建会议记录失败", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Routine.Meeting.CreateFailed", TransValue = "Failed to create meeting record", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Routine.Meeting.CreateFailed", TransValue = "会議記録の作成に失敗しました", ModuleName = "Routine", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Routine.Meeting.UpdateFailed", TransValue = "更新会议记录失败", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Routine.Meeting.UpdateFailed", TransValue = "Failed to update meeting record", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Routine.Meeting.UpdateFailed", TransValue = "会議記録の更新に失敗しました", ModuleName = "Routine", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Routine.Meeting.DeleteFailed", TransValue = "删除会议记录失败", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Routine.Meeting.DeleteFailed", TransValue = "Failed to delete meeting record", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Routine.Meeting.DeleteFailed", TransValue = "会議記録の削除に失敗しました", ModuleName = "Routine", Status = 0 },

            // 公告通知相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Routine.Notice.NotFound", TransValue = "公告通知不存在", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Routine.Notice.NotFound", TransValue = "Notice not found", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Routine.Notice.NotFound", TransValue = "お知らせが存在しません", ModuleName = "Routine", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Routine.Notice.CreateFailed", TransValue = "创建公告通知失败", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Routine.Notice.CreateFailed", TransValue = "Failed to create notice", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Routine.Notice.CreateFailed", TransValue = "お知らせの作成に失敗しました", ModuleName = "Routine", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Routine.Notice.UpdateFailed", TransValue = "更新公告通知失败", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Routine.Notice.UpdateFailed", TransValue = "Failed to update notice", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Routine.Notice.UpdateFailed", TransValue = "お知らせの更新に失敗しました", ModuleName = "Routine", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Routine.Notice.DeleteFailed", TransValue = "删除公告通知失败", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Routine.Notice.DeleteFailed", TransValue = "Failed to delete notice", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Routine.Notice.DeleteFailed", TransValue = "お知らせの削除に失敗しました", ModuleName = "Routine", Status = 0 },

            // 文件管理相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Routine.File.NotFound", TransValue = "文件不存在", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Routine.File.NotFound", TransValue = "File not found", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Routine.File.NotFound", TransValue = "ファイルが存在しません", ModuleName = "Routine", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Routine.File.UploadFailed", TransValue = "文件上传失败", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Routine.File.UploadFailed", TransValue = "Failed to upload file", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Routine.File.UploadFailed", TransValue = "ファイルのアップロードに失敗しました", ModuleName = "Routine", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Routine.File.DownloadFailed", TransValue = "文件下载失败", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Routine.File.DownloadFailed", TransValue = "Failed to download file", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Routine.File.DownloadFailed", TransValue = "ファイルのダウンロードに失敗しました", ModuleName = "Routine", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Routine.File.DeleteFailed", TransValue = "文件删除失败", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Routine.File.DeleteFailed", TransValue = "Failed to delete file", ModuleName = "Routine", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Routine.File.DeleteFailed", TransValue = "ファイルの削除に失敗しました", ModuleName = "Routine", Status = 0 }
        };

        int insertCount = 0;
        int updateCount = 0;

        foreach (var translation in defaultTranslations)
        {
            var existingTranslation = await _context.Client.Queryable<HbtTranslation>()
                .FirstAsync(t => t.LangCode == translation.LangCode && t.TransKey == translation.TransKey);

            if (existingTranslation == null)
            {

                translation.CreateBy = "Hbt365";
                translation.CreateTime = DateTime.Now;
                translation.UpdateBy = "Hbt365";
                translation.UpdateTime = DateTime.Now;

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