//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSeedTranslation.cs
// 创建者 : Lean365
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : 工作流本地化资源种子
//===================================================================

using Lean.Hbt.Domain.Entities.Core;
using Lean.Hbt.Infrastructure.Data.Contexts;

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

    /// <summary>
    /// 初始化工作流本地化资源
    /// </summary>
    public async Task<(int insertCount, int updateCount)> InitializeWorkflowTranslationAsync()
    {
        var defaultTranslations = new List<HbtTranslation>
        {
            // 工作流相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Workflow.NotFound", TransValue = "工作流不存在", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Workflow.NotFound", TransValue = "Workflow not found", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Workflow.NotFound", TransValue = "ワークフローが存在しません", ModuleName = "Workflow", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Workflow.CreateFailed", TransValue = "创建工作流失败", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Workflow.CreateFailed", TransValue = "Failed to create workflow", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Workflow.CreateFailed", TransValue = "ワークフローの作成に失敗しました", ModuleName = "Workflow", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Workflow.UpdateFailed", TransValue = "更新工作流失败", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Workflow.UpdateFailed", TransValue = "Failed to update workflow", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Workflow.UpdateFailed", TransValue = "ワークフローの更新に失敗しました", ModuleName = "Workflow", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Workflow.DeleteFailed", TransValue = "删除工作流失败", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Workflow.DeleteFailed", TransValue = "Failed to delete workflow", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Workflow.DeleteFailed", TransValue = "ワークフローの削除に失敗しました", ModuleName = "Workflow", Status = 0 },

            // 工作流节点相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Workflow.Node.NotFound", TransValue = "工作流节点不存在", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Workflow.Node.NotFound", TransValue = "Workflow node not found", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Workflow.Node.NotFound", TransValue = "ワークフローノードが存在しません", ModuleName = "Workflow", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Workflow.Node.CreateFailed", TransValue = "创建工作流节点失败", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Workflow.Node.CreateFailed", TransValue = "Failed to create workflow node", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Workflow.Node.CreateFailed", TransValue = "ワークフローノードの作成に失敗しました", ModuleName = "Workflow", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Workflow.Node.UpdateFailed", TransValue = "更新工作流节点失败", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Workflow.Node.UpdateFailed", TransValue = "Failed to update workflow node", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Workflow.Node.UpdateFailed", TransValue = "ワークフローノードの更新に失敗しました", ModuleName = "Workflow", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Workflow.Node.DeleteFailed", TransValue = "删除工作流节点失败", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Workflow.Node.DeleteFailed", TransValue = "Failed to delete workflow node", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Workflow.Node.DeleteFailed", TransValue = "ワークフローノードの削除に失敗しました", ModuleName = "Workflow", Status = 0 },

            // 工作流连线相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Workflow.Link.NotFound", TransValue = "工作流连线不存在", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Workflow.Link.NotFound", TransValue = "Workflow link not found", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Workflow.Link.NotFound", TransValue = "ワークフローリンクが存在しません", ModuleName = "Workflow", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Workflow.Link.CreateFailed", TransValue = "创建工作流连线失败", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Workflow.Link.CreateFailed", TransValue = "Failed to create workflow link", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Workflow.Link.CreateFailed", TransValue = "ワークフローリンクの作成に失敗しました", ModuleName = "Workflow", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Workflow.Link.UpdateFailed", TransValue = "更新工作流连线失败", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Workflow.Link.UpdateFailed", TransValue = "Failed to update workflow link", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Workflow.Link.UpdateFailed", TransValue = "ワークフローリンクの更新に失敗しました", ModuleName = "Workflow", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Workflow.Link.DeleteFailed", TransValue = "删除工作流连线失败", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Workflow.Link.DeleteFailed", TransValue = "Failed to delete workflow link", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Workflow.Link.DeleteFailed", TransValue = "ワークフローリンクの削除に失敗しました", ModuleName = "Workflow", Status = 0 },

            // 工作流实例相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Workflow.Instance.NotFound", TransValue = "工作流实例不存在", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Workflow.Instance.NotFound", TransValue = "Workflow instance not found", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Workflow.Instance.NotFound", TransValue = "ワークフローインスタンスが存在しません", ModuleName = "Workflow", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Workflow.Instance.CreateFailed", TransValue = "创建工作流实例失败", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Workflow.Instance.CreateFailed", TransValue = "Failed to create workflow instance", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Workflow.Instance.CreateFailed", TransValue = "ワークフローインスタンスの作成に失敗しました", ModuleName = "Workflow", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Workflow.Instance.UpdateFailed", TransValue = "更新工作流实例失败", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Workflow.Instance.UpdateFailed", TransValue = "Failed to update workflow instance", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Workflow.Instance.UpdateFailed", TransValue = "ワークフローインスタンスの更新に失敗しました", ModuleName = "Workflow", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Workflow.Instance.DeleteFailed", TransValue = "删除工作流实例失败", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Workflow.Instance.DeleteFailed", TransValue = "Failed to delete workflow instance", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Workflow.Instance.DeleteFailed", TransValue = "ワークフローインスタンスの削除に失敗しました", ModuleName = "Workflow", Status = 0 },

            // 工作流任务相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Workflow.Task.NotFound", TransValue = "工作流任务不存在", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Workflow.Task.NotFound", TransValue = "Workflow task not found", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Workflow.Task.NotFound", TransValue = "ワークフロータスクが存在しません", ModuleName = "Workflow", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Workflow.Task.CreateFailed", TransValue = "创建工作流任务失败", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Workflow.Task.CreateFailed", TransValue = "Failed to create workflow task", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Workflow.Task.CreateFailed", TransValue = "ワークフロータスクの作成に失敗しました", ModuleName = "Workflow", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Workflow.Task.UpdateFailed", TransValue = "更新工作流任务失败", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Workflow.Task.UpdateFailed", TransValue = "Failed to update workflow task", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Workflow.Task.UpdateFailed", TransValue = "ワークフロータスクの更新に失敗しました", ModuleName = "Workflow", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Workflow.Task.DeleteFailed", TransValue = "删除工作流任务失败", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Workflow.Task.DeleteFailed", TransValue = "Failed to delete workflow task", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Workflow.Task.DeleteFailed", TransValue = "ワークフロータスクの削除に失敗しました", ModuleName = "Workflow", Status = 0 },

            // 工作流日志相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Workflow.Log.NotFound", TransValue = "工作流日志不存在", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Workflow.Log.NotFound", TransValue = "Workflow log not found", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Workflow.Log.NotFound", TransValue = "ワークフローログが存在しません", ModuleName = "Workflow", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Workflow.Log.CreateFailed", TransValue = "创建工作流日志失败", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Workflow.Log.CreateFailed", TransValue = "Failed to create workflow log", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Workflow.Log.CreateFailed", TransValue = "ワークフローログの作成に失敗しました", ModuleName = "Workflow", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Workflow.Log.UpdateFailed", TransValue = "更新工作流日志失败", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Workflow.Log.UpdateFailed", TransValue = "Failed to update workflow log", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Workflow.Log.UpdateFailed", TransValue = "ワークフローログの更新に失敗しました", ModuleName = "Workflow", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "Workflow.Log.DeleteFailed", TransValue = "删除工作流日志失败", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Workflow.Log.DeleteFailed", TransValue = "Failed to delete workflow log", ModuleName = "Workflow", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "Workflow.Log.DeleteFailed", TransValue = "ワークフローログの削除に失敗しました", ModuleName = "Workflow", Status = 0 }
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