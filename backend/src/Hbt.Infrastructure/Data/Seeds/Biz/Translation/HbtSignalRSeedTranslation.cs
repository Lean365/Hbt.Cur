//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSignalRSeedTranslation.cs
// 创建者 : Lean365
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : SignalR本地化资源数据提供类
//===================================================================

using Hbt.Cur.Domain.Entities.Routine.Core;

namespace Hbt.Cur.Infrastructure.Data.Seeds.Biz.Translation;

/// <summary>
/// SignalR本地化资源数据提供类
/// </summary>
public class HbtSignalRSeedTranslation
{
    /// <summary>
    /// 获取SignalR翻译数据
    /// </summary>
    /// <returns>翻译数据列表</returns>
    public List<HbtTranslation> GetSignalRTranslations()
    {
        return new List<HbtTranslation>
        {
            // SignalR 基础操作
            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.NotFound", TransValue = "SignalR连接不存在", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.NotFound", TransValue = "SignalR connection not found", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.NotFound", TransValue = "SignalR接続が存在しません", ModuleName = "Backend", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.CreateFailed", TransValue = "创建SignalR连接失败", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.CreateFailed", TransValue = "Failed to create SignalR connection", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.CreateFailed", TransValue = "SignalR接続の作成に失敗しました", ModuleName = "Backend", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.UpdateFailed", TransValue = "更新SignalR连接失败", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.UpdateFailed", TransValue = "Failed to update SignalR connection", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.UpdateFailed", TransValue = "SignalR接続の更新に失敗しました", ModuleName = "Backend", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.DeleteFailed", TransValue = "删除SignalR连接失败", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.DeleteFailed", TransValue = "Failed to delete SignalR connection", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.DeleteFailed", TransValue = "SignalR接続の削除に失敗しました", ModuleName = "Backend", Status = 0 },

            // 消息推送相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.Message.NotFound", TransValue = "消息不存在", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.Message.NotFound", TransValue = "Message not found", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.Message.NotFound", TransValue = "メッセージが存在しません", ModuleName = "Backend", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.Message.SendFailed", TransValue = "发送消息失败", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.Message.SendFailed", TransValue = "Failed to send message", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.Message.SendFailed", TransValue = "メッセージの送信に失敗しました", ModuleName = "Backend", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.Message.ReceiveFailed", TransValue = "接收消息失败", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.Message.ReceiveFailed", TransValue = "Failed to receive message", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.Message.ReceiveFailed", TransValue = "メッセージの受信に失敗しました", ModuleName = "Backend", Status = 0 },

            // 连接状态相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.Connection.Connected", TransValue = "连接已建立", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.Connection.Connected", TransValue = "Connection established", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.Connection.Connected", TransValue = "接続が確立されました", ModuleName = "Backend", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.Connection.Disconnected", TransValue = "连接已断开", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.Connection.Disconnected", TransValue = "Connection disconnected", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.Connection.Disconnected", TransValue = "接続が切断されました", ModuleName = "Backend", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.Connection.Reconnecting", TransValue = "正在重新连接", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.Connection.Reconnecting", TransValue = "Reconnecting", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.Connection.Reconnecting", TransValue = "再接続中です", ModuleName = "Backend", Status = 0 },

            // 错误处理相关
            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.Error.ConnectionFailed", TransValue = "连接失败", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.Error.ConnectionFailed", TransValue = "Connection failed", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.Error.ConnectionFailed", TransValue = "接続に失敗しました", ModuleName = "Backend", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.Error.MessageFailed", TransValue = "消息处理失败", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.Error.MessageFailed", TransValue = "Message processing failed", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.Error.MessageFailed", TransValue = "メッセージ処理に失敗しました", ModuleName = "Backend", Status = 0 },

            new HbtTranslation { LangCode = "zh-CN", TransKey = "SignalR.Error.Timeout", TransValue = "连接超时", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "SignalR.Error.Timeout", TransValue = "Connection timeout", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "ja-JP", TransKey = "SignalR.Error.Timeout", TransValue = "接続がタイムアウトしました", ModuleName = "Backend", Status = 0 }
        };
    }
}