//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSignalRSeedTranslation.cs
// 创建者 : Lean365
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : SignalR本地化资源种子
//===================================================================

using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Core;
using Lean.Hbt.Domain.IServices.Extensions;
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

    private HbtTranslation CreateTranslation(string langCode, string transKey, string transValue)
    {
        return new HbtTranslation
        {
            LangCode = langCode,
            TransKey = transKey,
            TransValue = transValue,
            ModuleName = "SignalR",
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
    /// 初始化SignalR本地化资源
    /// </summary>
    public async Task<(int insertCount, int updateCount)> InitializeSignalRTranslationAsync()
    {
        var defaultTranslations = new List<HbtTranslation>
        {
            // SignalR 基础操作
            CreateTranslation("zh-CN", "SignalR.NotFound", "SignalR连接不存在"),
            CreateTranslation("en-US", "SignalR.NotFound", "SignalR connection not found"),
            CreateTranslation("ja-JP", "SignalR.NotFound", "SignalR接続が存在しません"),

            CreateTranslation("zh-CN", "SignalR.CreateFailed", "创建SignalR连接失败"),
            CreateTranslation("en-US", "SignalR.CreateFailed", "Failed to create SignalR connection"),
            CreateTranslation("ja-JP", "SignalR.CreateFailed", "SignalR接続の作成に失敗しました"),

            CreateTranslation("zh-CN", "SignalR.UpdateFailed", "更新SignalR连接失败"),
            CreateTranslation("en-US", "SignalR.UpdateFailed", "Failed to update SignalR connection"),
            CreateTranslation("ja-JP", "SignalR.UpdateFailed", "SignalR接続の更新に失敗しました"),

            CreateTranslation("zh-CN", "SignalR.DeleteFailed", "删除SignalR连接失败"),
            CreateTranslation("en-US", "SignalR.DeleteFailed", "Failed to delete SignalR connection"),
            CreateTranslation("ja-JP", "SignalR.DeleteFailed", "SignalR接続の削除に失敗しました"),

            // 消息推送相关
            CreateTranslation("zh-CN", "SignalR.Message.NotFound", "消息不存在"),
            CreateTranslation("en-US", "SignalR.Message.NotFound", "Message not found"),
            CreateTranslation("ja-JP", "SignalR.Message.NotFound", "メッセージが存在しません"),

            CreateTranslation("zh-CN", "SignalR.Message.SendFailed", "发送消息失败"),
            CreateTranslation("en-US", "SignalR.Message.SendFailed", "Failed to send message"),
            CreateTranslation("ja-JP", "SignalR.Message.SendFailed", "メッセージの送信に失敗しました"),

            CreateTranslation("zh-CN", "SignalR.Message.ReceiveFailed", "接收消息失败"),
            CreateTranslation("en-US", "SignalR.Message.ReceiveFailed", "Failed to receive message"),
            CreateTranslation("ja-JP", "SignalR.Message.ReceiveFailed", "メッセージの受信に失敗しました"),

            // 连接状态相关
            CreateTranslation("zh-CN", "SignalR.Connection.Connected", "连接已建立"),
            CreateTranslation("en-US", "SignalR.Connection.Connected", "Connection established"),
            CreateTranslation("ja-JP", "SignalR.Connection.Connected", "接続が確立されました"),

            CreateTranslation("zh-CN", "SignalR.Connection.Disconnected", "连接已断开"),
            CreateTranslation("en-US", "SignalR.Connection.Disconnected", "Connection disconnected"),
            CreateTranslation("ja-JP", "SignalR.Connection.Disconnected", "接続が切断されました"),

            CreateTranslation("zh-CN", "SignalR.Connection.Reconnecting", "正在重新连接"),
            CreateTranslation("en-US", "SignalR.Connection.Reconnecting", "Reconnecting"),
            CreateTranslation("ja-JP", "SignalR.Connection.Reconnecting", "再接続中です"),

            // 错误处理相关
            CreateTranslation("zh-CN", "SignalR.Error.ConnectionFailed", "连接失败"),
            CreateTranslation("en-US", "SignalR.Error.ConnectionFailed", "Connection failed"),
            CreateTranslation("ja-JP", "SignalR.Error.ConnectionFailed", "接続に失敗しました"),

            CreateTranslation("zh-CN", "SignalR.Error.MessageFailed", "消息处理失败"),
            CreateTranslation("en-US", "SignalR.Error.MessageFailed", "Message processing failed"),
            CreateTranslation("ja-JP", "SignalR.Error.MessageFailed", "メッセージ処理に失敗しました"),

            CreateTranslation("zh-CN", "SignalR.Error.Timeout", "连接超时"),
            CreateTranslation("en-US", "SignalR.Error.Timeout", "Connection timeout"),
            CreateTranslation("ja-JP", "SignalR.Error.Timeout", "接続がタイムアウトしました")
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