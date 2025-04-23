//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtRoutineSeedTranslation.cs
// 创建者 : Lean365
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : 定时任务本地化资源种子
//===================================================================

using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Core;
using Lean.Hbt.Domain.IServices.Extensions;
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

    private HbtTranslation CreateTranslation(string langCode, string transKey, string transValue)
    {
        return new HbtTranslation
        {
            LangCode = langCode,
            TransKey = transKey,
            TransValue = transValue,
            ModuleName = "Routine",
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
    /// 初始化定时任务本地化资源
    /// </summary>
    public async Task<(int insertCount, int updateCount)> InitializeRoutineTranslationAsync()
    {
        var defaultTranslations = new List<HbtTranslation>
        {
            // 日常办公基础操作
            CreateTranslation("zh-CN", "Routine.NotFound", "日常办公记录不存在"),
            CreateTranslation("en-US", "Routine.NotFound", "Routine record not found"),
            CreateTranslation("ja-JP", "Routine.NotFound", "日常業務記録が存在しません"),

            CreateTranslation("zh-CN", "Routine.CreateFailed", "创建日常办公记录失败"),
            CreateTranslation("en-US", "Routine.CreateFailed", "Failed to create routine record"),
            CreateTranslation("ja-JP", "Routine.CreateFailed", "日常業務記録の作成に失敗しました"),

            CreateTranslation("zh-CN", "Routine.UpdateFailed", "更新日常办公记录失败"),
            CreateTranslation("en-US", "Routine.UpdateFailed", "Failed to update routine record"),
            CreateTranslation("ja-JP", "Routine.UpdateFailed", "日常業務記録の更新に失敗しました"),

            CreateTranslation("zh-CN", "Routine.DeleteFailed", "删除日常办公记录失败"),
            CreateTranslation("en-US", "Routine.DeleteFailed", "Failed to delete routine record"),
            CreateTranslation("ja-JP", "Routine.DeleteFailed", "日常業務記録の削除に失敗しました"),

            // 日程安排相关
            CreateTranslation("zh-CN", "Routine.Schedule.NotFound", "日程安排不存在"),
            CreateTranslation("en-US", "Routine.Schedule.NotFound", "Schedule not found"),
            CreateTranslation("ja-JP", "Routine.Schedule.NotFound", "スケジュールが存在しません"),

            CreateTranslation("zh-CN", "Routine.Schedule.CreateFailed", "创建日程安排失败"),
            CreateTranslation("en-US", "Routine.Schedule.CreateFailed", "Failed to create schedule"),
            CreateTranslation("ja-JP", "Routine.Schedule.CreateFailed", "スケジュールの作成に失敗しました"),

            CreateTranslation("zh-CN", "Routine.Schedule.UpdateFailed", "更新日程安排失败"),
            CreateTranslation("en-US", "Routine.Schedule.UpdateFailed", "Failed to update schedule"),
            CreateTranslation("ja-JP", "Routine.Schedule.UpdateFailed", "スケジュールの更新に失敗しました"),

            CreateTranslation("zh-CN", "Routine.Schedule.DeleteFailed", "删除日程安排失败"),
            CreateTranslation("en-US", "Routine.Schedule.DeleteFailed", "Failed to delete schedule"),
            CreateTranslation("ja-JP", "Routine.Schedule.DeleteFailed", "スケジュールの削除に失敗しました"),

            // 待办事项相关
            CreateTranslation("zh-CN", "Routine.Todo.NotFound", "待办事项不存在"),
            CreateTranslation("en-US", "Routine.Todo.NotFound", "Todo item not found"),
            CreateTranslation("ja-JP", "Routine.Todo.NotFound", "ToDo項目が存在しません"),

            CreateTranslation("zh-CN", "Routine.Todo.CreateFailed", "创建待办事项失败"),
            CreateTranslation("en-US", "Routine.Todo.CreateFailed", "Failed to create todo item"),
            CreateTranslation("ja-JP", "Routine.Todo.CreateFailed", "ToDo項目の作成に失敗しました"),

            CreateTranslation("zh-CN", "Routine.Todo.UpdateFailed", "更新待办事项失败"),
            CreateTranslation("en-US", "Routine.Todo.UpdateFailed", "Failed to update todo item"),
            CreateTranslation("ja-JP", "Routine.Todo.UpdateFailed", "ToDo項目の更新に失敗しました"),

            CreateTranslation("zh-CN", "Routine.Todo.DeleteFailed", "删除待办事项失败"),
            CreateTranslation("en-US", "Routine.Todo.DeleteFailed", "Failed to delete todo item"),
            CreateTranslation("ja-JP", "Routine.Todo.DeleteFailed", "ToDo項目の削除に失敗しました"),

            // 会议管理相关
            CreateTranslation("zh-CN", "Routine.Meeting.NotFound", "会议记录不存在"),
            CreateTranslation("en-US", "Routine.Meeting.NotFound", "Meeting record not found"),
            CreateTranslation("ja-JP", "Routine.Meeting.NotFound", "会議記録が存在しません"),

            CreateTranslation("zh-CN", "Routine.Meeting.CreateFailed", "创建会议记录失败"),
            CreateTranslation("en-US", "Routine.Meeting.CreateFailed", "Failed to create meeting record"),
            CreateTranslation("ja-JP", "Routine.Meeting.CreateFailed", "会議記録の作成に失敗しました"),

            CreateTranslation("zh-CN", "Routine.Meeting.UpdateFailed", "更新会议记录失败"),
            CreateTranslation("en-US", "Routine.Meeting.UpdateFailed", "Failed to update meeting record"),
            CreateTranslation("ja-JP", "Routine.Meeting.UpdateFailed", "会議記録の更新に失敗しました"),

            CreateTranslation("zh-CN", "Routine.Meeting.DeleteFailed", "删除会议记录失败"),
            CreateTranslation("en-US", "Routine.Meeting.DeleteFailed", "Failed to delete meeting record"),
            CreateTranslation("ja-JP", "Routine.Meeting.DeleteFailed", "会議記録の削除に失敗しました"),

            // 公告通知相关
            CreateTranslation("zh-CN", "Routine.Notice.NotFound", "公告通知不存在"),
            CreateTranslation("en-US", "Routine.Notice.NotFound", "Notice not found"),
            CreateTranslation("ja-JP", "Routine.Notice.NotFound", "お知らせが存在しません"),

            CreateTranslation("zh-CN", "Routine.Notice.CreateFailed", "创建公告通知失败"),
            CreateTranslation("en-US", "Routine.Notice.CreateFailed", "Failed to create notice"),
            CreateTranslation("ja-JP", "Routine.Notice.CreateFailed", "お知らせの作成に失敗しました"),

            CreateTranslation("zh-CN", "Routine.Notice.UpdateFailed", "更新公告通知失败"),
            CreateTranslation("en-US", "Routine.Notice.UpdateFailed", "Failed to update notice"),
            CreateTranslation("ja-JP", "Routine.Notice.UpdateFailed", "お知らせの更新に失敗しました"),

            CreateTranslation("zh-CN", "Routine.Notice.DeleteFailed", "删除公告通知失败"),
            CreateTranslation("en-US", "Routine.Notice.DeleteFailed", "Failed to delete notice"),
            CreateTranslation("ja-JP", "Routine.Notice.DeleteFailed", "お知らせの削除に失敗しました"),

            // 文件管理相关
            CreateTranslation("zh-CN", "Routine.File.NotFound", "文件不存在"),
            CreateTranslation("en-US", "Routine.File.NotFound", "File not found"),
            CreateTranslation("ja-JP", "Routine.File.NotFound", "ファイルが存在しません"),

            CreateTranslation("zh-CN", "Routine.File.UploadFailed", "文件上传失败"),
            CreateTranslation("en-US", "Routine.File.UploadFailed", "Failed to upload file"),
            CreateTranslation("ja-JP", "Routine.File.UploadFailed", "ファイルのアップロードに失敗しました"),

            CreateTranslation("zh-CN", "Routine.File.DownloadFailed", "文件下载失败"),
            CreateTranslation("en-US", "Routine.File.DownloadFailed", "Failed to download file"),
            CreateTranslation("ja-JP", "Routine.File.DownloadFailed", "ファイルのダウンロードに失敗しました"),

            CreateTranslation("zh-CN", "Routine.File.DeleteFailed", "文件删除失败"),
            CreateTranslation("en-US", "Routine.File.DeleteFailed", "Failed to delete file"),
            CreateTranslation("ja-JP", "Routine.File.DeleteFailed", "ファイルの削除に失敗しました")
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