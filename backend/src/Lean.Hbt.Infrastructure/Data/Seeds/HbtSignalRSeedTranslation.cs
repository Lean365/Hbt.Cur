//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSignalRSeedTranslation.cs
// 创建者 : Lean365
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : SignalR本地化资源种子
//===================================================================

using Lean.Hbt.Domain.Entities.Core;
using Lean.Hbt.Infrastructure.Data.Contexts;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// SignalR本地化资源种子
/// </summary>
public class HbtSignalRSeedTranslation
{
    private readonly HbtDbContext _context;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="context">数据库上下文</param>
    /// <param name="logger">日志记录器</param>
    public HbtSignalRSeedTranslation(HbtDbContext context, IHbtLogger logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 初始化SignalR本地化资源
    /// </summary>
    public async Task<(int insertCount, int updateCount)> InitializeSignalRTranslationAsync()
    {
        var defaultTranslations = new List<HbtTranslation>
        {
            // SignalR 基础操作
            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.NotFound", TransValue = "SignalR连接不存在", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.NotFound", TransValue = "SignalR connection not found", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.NotFound", TransValue = "SignalR接続が存在しません", ModuleName = "SignalR", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.CreateFailed", TransValue = "创建SignalR连接失败", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.CreateFailed", TransValue = "Failed to create SignalR connection", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.CreateFailed", TransValue = "SignalR接続の作成に失敗しました", ModuleName = "SignalR", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.UpdateFailed", TransValue = "更新SignalR连接失败", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.UpdateFailed", TransValue = "Failed to update SignalR connection", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.UpdateFailed", TransValue = "SignalR接続の更新に失敗しました", ModuleName = "SignalR", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.DeleteFailed", TransValue = "删除SignalR连接失败", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.DeleteFailed", TransValue = "Failed to delete SignalR connection", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.DeleteFailed", TransValue = "SignalR接続の削除に失敗しました", ModuleName = "SignalR", Status = 0 },

            // 消息推送相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.Message.NotFound", TransValue = "消息不存在", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.Message.NotFound", TransValue = "Message not found", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.Message.NotFound", TransValue = "メッセージが存在しません", ModuleName = "SignalR", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.Message.SendFailed", TransValue = "发送消息失败", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.Message.SendFailed", TransValue = "Failed to send message", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.Message.SendFailed", TransValue = "メッセージの送信に失敗しました", ModuleName = "SignalR", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.Message.ReceiveFailed", TransValue = "接收消息失败", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.Message.ReceiveFailed", TransValue = "Failed to receive message", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.Message.ReceiveFailed", TransValue = "メッセージの受信に失敗しました", ModuleName = "SignalR", Status = 0 },

            // 连接状态相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.Connection.Connected", TransValue = "连接已建立", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.Connection.Connected", TransValue = "Connection established", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.Connection.Connected", TransValue = "接続が確立されました", ModuleName = "SignalR", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.Connection.Disconnected", TransValue = "连接已断开", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.Connection.Disconnected", TransValue = "Connection disconnected", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.Connection.Disconnected", TransValue = "接続が切断されました", ModuleName = "SignalR", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.Connection.Reconnecting", TransValue = "正在重新连接", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.Connection.Reconnecting", TransValue = "Reconnecting", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.Connection.Reconnecting", TransValue = "再接続中です", ModuleName = "SignalR", Status = 0 },

            // 错误处理相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.Error.ConnectionFailed", TransValue = "连接失败", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.Error.ConnectionFailed", TransValue = "Connection failed", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.Error.ConnectionFailed", TransValue = "接続に失敗しました", ModuleName = "SignalR", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.Error.MessageFailed", TransValue = "消息处理失败", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.Error.MessageFailed", TransValue = "Message processing failed", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.Error.MessageFailed", TransValue = "メッセージ処理に失敗しました", ModuleName = "SignalR", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.Error.Timeout", TransValue = "连接超时", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.Error.Timeout", TransValue = "Connection timeout", ModuleName = "SignalR", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.Error.Timeout", TransValue = "接続がタイムアウトしました", ModuleName = "SignalR", Status = 0 }
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