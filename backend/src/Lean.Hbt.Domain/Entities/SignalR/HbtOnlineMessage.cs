#nullable enable

using SqlSugar;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOnlineMessage.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述    : 在线消息实体
//===================================================================

namespace Lean.Hbt.Domain.Entities.SignalR
{
    /// <summary>
    /// 在线消息实体
    /// </summary>
    [SugarTable("hbt_signalr_message", "在线消息表")]
    public class HbtOnlineMessage : HbtBaseEntity
    {
        /// <summary>
        /// 发送者ID
        /// </summary>
        [SugarColumn(ColumnName = "sender_id", ColumnDescription = "发送者ID", ColumnDataType = "bigint", IsNullable = false)]
        public long SenderId { get; set; }

        /// <summary>
        /// 接收者ID
        /// </summary>
        [SugarColumn(ColumnName = "receiver_id", ColumnDescription = "接收者ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ReceiverId { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [SugarColumn(ColumnName = "message_type", ColumnDescription = "消息类型", ColumnDataType = "int", IsNullable = false)]
        public int MessageType { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        [SugarColumn(ColumnName = "content", ColumnDescription = "消息内容", Length = 2000, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Content { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        [SugarColumn(ColumnName = "is_read", ColumnDescription = "是否已读", ColumnDataType = "int", IsNullable = false)]
        public int IsRead { get; set; }

        /// <summary>
        /// 读取时间
        /// </summary>
        [SugarColumn(ColumnName = "read_time", ColumnDescription = "读取时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ReadTime { get; set; }
    }
}