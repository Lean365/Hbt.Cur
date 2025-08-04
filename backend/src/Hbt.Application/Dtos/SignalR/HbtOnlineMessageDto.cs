#nullable enable

//===================================================================
// 项目名: Hbt.Cur.Application
// 文件名: HbtOnlineMessageDto.cs
// 创建者: Lean365
// 创建时间: 2024-01-20
// 版本号: V0.0.1
// 描述: 在线消息数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Hbt.Cur.Application.Dtos.SignalR;

/// <summary>
/// 在线消息基础DTO
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
public class HbtOnlineMessageDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtOnlineMessageDto()
    {
        Content = string.Empty;
        IsRead = 0;
        ReadTime = DateTime.Now;
    }



    /// <summary>
    /// 发送者ID
    /// </summary>
    [Required(ErrorMessage = "发送者ID不能为空")]
    [Range(1, long.MaxValue, ErrorMessage = "发送者ID必须大于0")]
    public long SenderId { get; set; }

    /// <summary>
    /// 接收者ID
    /// </summary>
    [Required(ErrorMessage = "接收者ID不能为空")]
    [Range(1, long.MaxValue, ErrorMessage = "接收者ID必须大于0")]
    public long ReceiverId { get; set; }

    /// <summary>
    /// 消息类型
    /// </summary>
    [Required(ErrorMessage = "消息类型不能为空")]
    [Range(0, int.MaxValue, ErrorMessage = "消息类型必须大于等于0")]
    public int MessageType { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>
    [Required(ErrorMessage = "消息内容不能为空")]
    [MaxLength(500, ErrorMessage = "消息内容长度不能超过500个字符")]
    public string Content { get; set; }

    /// <summary>
    /// 是否已读（0未读 1已读）
    /// </summary>
    [Range(0, 1, ErrorMessage = "是否已读只能是0或1")]
    public int IsRead { get; set; }

    /// <summary>
    /// 阅读时间
    /// </summary>
    public DateTime ReadTime { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建者
    /// </summary>
    public string? CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新者
    /// </summary>
    public string? UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 是否删除（0未删除 1已删除）
    /// </summary>
    public int IsDeleted { get; set; }

    /// <summary>
    /// 删除者
    /// </summary>
    public string? DeleteBy { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    public DateTime? DeleteTime { get; set; }
}

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
    /// 构造函数
    /// </summary>
    public HbtOnlineMessageQueryDto()
    {
        // 不需要初始化 MessageType，因为它已经是可空类型
    }

    /// <summary>
    /// 发送者ID
    /// </summary>
    [Range(1, long.MaxValue, ErrorMessage = "发送者ID必须大于0")]
    public long? SenderId { get; set; }

    /// <summary>
    /// 接收者ID
    /// </summary>
    [Range(1, long.MaxValue, ErrorMessage = "接收者ID必须大于0")]
    public long? ReceiverId { get; set; }

    /// <summary>
    /// 消息类型
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "消息类型必须大于等于0")]
    public int? MessageType { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 是否已读（0未读 1已读）
    /// </summary>
    [Range(0, 1, ErrorMessage = "是否已读只能是0或1")]
    public int? IsRead { get; set; }
}

/// <summary>
/// 在线消息创建对象
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
public class HbtOnlineMessageCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtOnlineMessageCreateDto()
    {
        Content = string.Empty;
        IsRead = 0;
    }



    /// <summary>
    /// 发送者ID
    /// </summary>
    [Required(ErrorMessage = "发送者ID不能为空")]
    [Range(1, long.MaxValue, ErrorMessage = "发送者ID必须大于0")]
    public long SenderId { get; set; }

    /// <summary>
    /// 接收者ID
    /// </summary>
    [Required(ErrorMessage = "接收者ID不能为空")]
    [Range(1, long.MaxValue, ErrorMessage = "接收者ID必须大于0")]
    public long ReceiverId { get; set; }

    /// <summary>
    /// 消息类型
    /// </summary>
    [Required(ErrorMessage = "消息类型不能为空")]
    [Range(0, int.MaxValue, ErrorMessage = "消息类型必须大于等于0")]
    public int MessageType { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>
    [Required(ErrorMessage = "消息内容不能为空")]
    [MaxLength(500, ErrorMessage = "消息内容长度不能超过500个字符")]
    public string Content { get; set; }

    /// <summary>
    /// 是否已读（0未读 1已读）
    /// </summary>
    [Range(0, 1, ErrorMessage = "是否已读只能是0或1")]
    public int IsRead { get; set; }
}

/// <summary>
/// 在线消息更新对象
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
public class HbtOnlineMessageUpdateDto : HbtOnlineMessageCreateDto
{
    /// <summary>
    /// 阅读时间
    /// </summary>
    public DateTime ReadTime { get; set; }
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
    /// 构造函数
    /// </summary>
    public HbtOnlineMessageExportDto()
    {
        Content = string.Empty;
        IsRead = 0;
        ReadTime = DateTime.Now;
    }



    /// <summary>
    /// 发送者ID
    /// </summary>
    public long SenderId { get; set; }

    /// <summary>
    /// 接收者ID
    /// </summary>
    public long ReceiverId { get; set; }

    /// <summary>
    /// 消息类型
    /// </summary>
    public int MessageType { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 是否已读（0未读 1已读）
    /// </summary>
    public int IsRead { get; set; }

    /// <summary>
    /// 阅读时间
    /// </summary>
    public DateTime ReadTime { get; set; }
}