//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowSeedTranslation.cs
// 创建者 : Lean365
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : 工作流本地化资源种子
//===================================================================

using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Core;
using Lean.Hbt.Domain.IServices.Extensions;
using Lean.Hbt.Infrastructure.Data.Contexts;
using SqlSugar;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 工作流本地化资源种子
/// </summary>
public class HbtWorkflowSeedTranslation
{
    private readonly HbtDbContext _context;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="context">数据库上下文</param>
    /// <param name="logger">日志记录器</param>
    public HbtWorkflowSeedTranslation(HbtDbContext context, IHbtLogger logger)
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
            ModuleName = "Workflow",
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
    /// 初始化工作流本地化资源
    /// </summary>
    public async Task<(int insertCount, int updateCount)> InitializeWorkflowTranslationAsync()
    {
        var defaultTranslations = new List<HbtTranslation>
        {
            // 工作流相关
            CreateTranslation("zh-CN", "Workflow.NotFound", "工作流不存在"),
            CreateTranslation("en-US", "Workflow.NotFound", "Workflow not found"),
            CreateTranslation("ja-JP", "Workflow.NotFound", "ワークフローが存在しません"),

            CreateTranslation("zh-CN", "Workflow.CreateFailed", "创建工作流失败"),
            CreateTranslation("en-US", "Workflow.CreateFailed", "Failed to create workflow"),
            CreateTranslation("ja-JP", "Workflow.CreateFailed", "ワークフローの作成に失敗しました"),

            CreateTranslation("zh-CN", "Workflow.UpdateFailed", "更新工作流失败"),
            CreateTranslation("en-US", "Workflow.UpdateFailed", "Failed to update workflow"),
            CreateTranslation("ja-JP", "Workflow.UpdateFailed", "ワークフローの更新に失敗しました"),

            CreateTranslation("zh-CN", "Workflow.DeleteFailed", "删除工作流失败"),
            CreateTranslation("en-US", "Workflow.DeleteFailed", "Failed to delete workflow"),
            CreateTranslation("ja-JP", "Workflow.DeleteFailed", "ワークフローの削除に失敗しました"),

            // 工作流节点相关
            CreateTranslation("zh-CN", "Workflow.Node.NotFound", "工作流节点不存在"),
            CreateTranslation("en-US", "Workflow.Node.NotFound", "Workflow node not found"),
            CreateTranslation("ja-JP", "Workflow.Node.NotFound", "ワークフローノードが存在しません"),

            CreateTranslation("zh-CN", "Workflow.Node.CreateFailed", "创建工作流节点失败"),
            CreateTranslation("en-US", "Workflow.Node.CreateFailed", "Failed to create workflow node"),
            CreateTranslation("ja-JP", "Workflow.Node.CreateFailed", "ワークフローノードの作成に失敗しました"),

            CreateTranslation("zh-CN", "Workflow.Node.UpdateFailed", "更新工作流节点失败"),
            CreateTranslation("en-US", "Workflow.Node.UpdateFailed", "Failed to update workflow node"),
            CreateTranslation("ja-JP", "Workflow.Node.UpdateFailed", "ワークフローノードの更新に失敗しました"),

            CreateTranslation("zh-CN", "Workflow.Node.DeleteFailed", "删除工作流节点失败"),
            CreateTranslation("en-US", "Workflow.Node.DeleteFailed", "Failed to delete workflow node"),
            CreateTranslation("ja-JP", "Workflow.Node.DeleteFailed", "ワークフローノードの削除に失敗しました"),

            // 工作流连线相关
            CreateTranslation("zh-CN", "Workflow.Link.NotFound", "工作流连线不存在"),
            CreateTranslation("en-US", "Workflow.Link.NotFound", "Workflow link not found"),
            CreateTranslation("ja-JP", "Workflow.Link.NotFound", "ワークフローリンクが存在しません"),

            CreateTranslation("zh-CN", "Workflow.Link.CreateFailed", "创建工作流连线失败"),
            CreateTranslation("en-US", "Workflow.Link.CreateFailed", "Failed to create workflow link"),
            CreateTranslation("ja-JP", "Workflow.Link.CreateFailed", "ワークフローリンクの作成に失敗しました"),

            CreateTranslation("zh-CN", "Workflow.Link.UpdateFailed", "更新工作流连线失败"),
            CreateTranslation("en-US", "Workflow.Link.UpdateFailed", "Failed to update workflow link"),
            CreateTranslation("ja-JP", "Workflow.Link.UpdateFailed", "ワークフローリンクの更新に失敗しました"),

            CreateTranslation("zh-CN", "Workflow.Link.DeleteFailed", "删除工作流连线失败"),
            CreateTranslation("en-US", "Workflow.Link.DeleteFailed", "Failed to delete workflow link"),
            CreateTranslation("ja-JP", "Workflow.Link.DeleteFailed", "ワークフローリンクの削除に失敗しました"),

            // 工作流实例相关
            CreateTranslation("zh-CN", "Workflow.Instance.NotFound", "工作流实例不存在"),
            CreateTranslation("en-US", "Workflow.Instance.NotFound", "Workflow instance not found"),
            CreateTranslation("ja-JP", "Workflow.Instance.NotFound", "ワークフローインスタンスが存在しません"),

            CreateTranslation("zh-CN", "Workflow.Instance.CreateFailed", "创建工作流实例失败"),
            CreateTranslation("en-US", "Workflow.Instance.CreateFailed", "Failed to create workflow instance"),
            CreateTranslation("ja-JP", "Workflow.Instance.CreateFailed", "ワークフローインスタンスの作成に失敗しました"),

            CreateTranslation("zh-CN", "Workflow.Instance.UpdateFailed", "更新工作流实例失败"),
            CreateTranslation("en-US", "Workflow.Instance.UpdateFailed", "Failed to update workflow instance"),
            CreateTranslation("ja-JP", "Workflow.Instance.UpdateFailed", "ワークフローインスタンスの更新に失敗しました"),

            CreateTranslation("zh-CN", "Workflow.Instance.DeleteFailed", "删除工作流实例失败"),
            CreateTranslation("en-US", "Workflow.Instance.DeleteFailed", "Failed to delete workflow instance"),
            CreateTranslation("ja-JP", "Workflow.Instance.DeleteFailed", "ワークフローインスタンスの削除に失敗しました"),

            // 工作流任务相关
            CreateTranslation("zh-CN", "Workflow.Task.NotFound", "工作流任务不存在"),
            CreateTranslation("en-US", "Workflow.Task.NotFound", "Workflow task not found"),
            CreateTranslation("ja-JP", "Workflow.Task.NotFound", "ワークフロータスクが存在しません"),

            CreateTranslation("zh-CN", "Workflow.Task.CreateFailed", "创建工作流任务失败"),
            CreateTranslation("en-US", "Workflow.Task.CreateFailed", "Failed to create workflow task"),
            CreateTranslation("ja-JP", "Workflow.Task.CreateFailed", "ワークフロータスクの作成に失敗しました"),

            CreateTranslation("zh-CN", "Workflow.Task.UpdateFailed", "更新工作流任务失败"),
            CreateTranslation("en-US", "Workflow.Task.UpdateFailed", "Failed to update workflow task"),
            CreateTranslation("ja-JP", "Workflow.Task.UpdateFailed", "ワークフロータスクの更新に失敗しました"),

            CreateTranslation("zh-CN", "Workflow.Task.DeleteFailed", "删除工作流任务失败"),
            CreateTranslation("en-US", "Workflow.Task.DeleteFailed", "Failed to delete workflow task"),
            CreateTranslation("ja-JP", "Workflow.Task.DeleteFailed", "ワークフロータスクの削除に失敗しました"),

            // 工作流日志相关
            CreateTranslation("zh-CN", "Workflow.Log.NotFound", "工作流日志不存在"),
            CreateTranslation("en-US", "Workflow.Log.NotFound", "Workflow log not found"),
            CreateTranslation("ja-JP", "Workflow.Log.NotFound", "ワークフローログが存在しません"),

            CreateTranslation("zh-CN", "Workflow.Log.CreateFailed", "创建工作流日志失败"),
            CreateTranslation("en-US", "Workflow.Log.CreateFailed", "Failed to create workflow log"),
            CreateTranslation("ja-JP", "Workflow.Log.CreateFailed", "ワークフローログの作成に失敗しました"),

            CreateTranslation("zh-CN", "Workflow.Log.UpdateFailed", "更新工作流日志失败"),
            CreateTranslation("en-US", "Workflow.Log.UpdateFailed", "Failed to update workflow log"),
            CreateTranslation("ja-JP", "Workflow.Log.UpdateFailed", "ワークフローログの更新に失敗しました"),

            CreateTranslation("zh-CN", "Workflow.Log.DeleteFailed", "删除工作流日志失败"),
            CreateTranslation("en-US", "Workflow.Log.DeleteFailed", "Failed to delete workflow log"),
            CreateTranslation("ja-JP", "Workflow.Log.DeleteFailed", "ワークフローログの削除に失敗しました")
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