//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtOnlineMessageDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V1.0.0
// 描述    : 在线消息数据传输对象
//===================================================================

using Lean.Hbt.Common.Models;
using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.Application.Dtos.RealTime;

/// <summary>
/// 在线消息查询对象
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
public class HbtOnlineMessageQueryDto : HbtPagedQuery
{
    /// <summary>
    /// 租户ID
    /// </summary>
    public long? TenantId { get; set; }

    /// <summary>
    /// 发送者ID
    /// </summary>
    public long? SenderId { get; set; }

    /// <summary>
    /// 接收者ID
    /// </summary>
    public long? ReceiverId { get; set; }

    /// <summary>
    /// 消息类型
    /// </summary>
    public HbtMessageType? MessageType { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }
}

/// <summary>
/// 在线消息基础传输对象
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
public class HbtOnlineMessageDto
{
    /// <summary>
    /// 租户ID
    /// </summary>
    public long TenantId { get; set; }

    /// <summary>
    /// 发送者ID
    /// </summary>
    public long SenderId { get; set; }

    /// <summary>
    /// 发送者名称
    /// </summary>
    public string SenderName { get; set; }

    /// <summary>
    /// 接收者ID
    /// </summary>
    public long? ReceiverId { get; set; }

    /// <summary>
    /// 接收者名称
    /// </summary>
    public string ReceiverName { get; set; }

    /// <summary>
    /// 群组名称
    /// </summary>
    public string GroupName { get; set; }

    /// <summary>
    /// 消息类型
    /// </summary>
    public HbtMessageType MessageType { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 发送时间
    /// </summary>
    public DateTime SendTime { get; set; }
}

/// <summary>
/// 在线消息导出对象
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
public class HbtOnlineMessageExportDto
{
    /// <summary>
    /// 租户名称
    /// </summary>
    public string TenantName { get; set; }

    /// <summary>
    /// 发送者名称
    /// </summary>
    public string SenderName { get; set; }

    /// <summary>
    /// 接收者名称
    /// </summary>
    public string ReceiverName { get; set; }

    /// <summary>
    /// 群组名称
    /// </summary>
    public string GroupName { get; set; }

    /// <summary>
    /// 消息类型
    /// </summary>
    public string MessageType { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 发送时间
    /// </summary>
    public DateTime SendTime { get; set; }
} 