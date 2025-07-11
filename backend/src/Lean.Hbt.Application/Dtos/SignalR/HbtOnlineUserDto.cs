#nullable enable

//===================================================================
// 项目名: Lean.Hbt.Application
// 文件名: HbtOnlineUserDto.cs
// 创建者: Lean365
// 创建时间: 2024-01-20
// 版本号: V0.0.1
// 描述: 在线用户数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.SignalR;

/// <summary>
/// 在线用户基础DTO
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
public class HbtOnlineUserDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtOnlineUserDto()
    {
        ConnectionId = string.Empty;
        DeviceId = string.Empty;
        ClientIp = string.Empty;
        UserAgent = string.Empty;
        LastActivity = DateTime.Now;
        LastHeartbeat = DateTime.Now;
        OnlineStatus = 1;
        CreateTime = DateTime.Now;
        UpdateTime = DateTime.Now;
    }

    /// <summary>
    /// ID
    /// </summary>
    [AdaptMember("Id")]
    public long OnlineUserId { get; set; }



    /// <summary>
    /// 用户ID
    /// </summary>
    [Required(ErrorMessage = "用户ID不能为空")]
    public long UserId { get; set; }

    /// <summary>
    /// 用户组ID
    /// </summary>
    [Required(ErrorMessage = "用户组ID不能为空")]
    public long GroupId { get; set; }

    /// <summary>
    /// 连接ID
    /// </summary>
    [Required(ErrorMessage = "连接ID不能为空")]
    [MaxLength(50, ErrorMessage = "连接ID长度不能超过50个字符")]
    public string ConnectionId { get; set; }

    /// <summary>
    /// 设备ID
    /// </summary>
    [Required(ErrorMessage = "设备ID不能为空")]
    [MaxLength(50, ErrorMessage = "设备ID长度不能超过50个字符")]
    public string DeviceId { get; set; }

    /// <summary>
    /// 客户端IP
    /// </summary>
    [Required(ErrorMessage = "客户端IP不能为空")]
    [MaxLength(50, ErrorMessage = "客户端IP长度不能超过50个字符")]
    public string ClientIp { get; set; }

    /// <summary>
    /// 用户代理
    /// </summary>
    [Required(ErrorMessage = "用户代理不能为空")]
    [MaxLength(500, ErrorMessage = "用户代理长度不能超过500个字符")]
    public string UserAgent { get; set; }

    /// <summary>
    /// 最后活动时间
    /// </summary>
    public DateTime LastActivity { get; set; }

    /// <summary>
    /// 在线状态（0离线 1在线）
    /// </summary>
    public int OnlineStatus { get; set; }

    /// <summary>
    /// 最后心跳时间
    /// </summary>
    public DateTime LastHeartbeat { get; set; }

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
/// 在线用户查询对象
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
public class HbtOnlineUserQueryDto : HbtPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtOnlineUserQueryDto()
    {
    }

    /// <summary>
    /// 用户ID
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 在线状态（0离线 1在线）
    /// </summary>
    public int? OnlineStatus { get; set; }
}

/// <summary>
/// 在线用户创建对象
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
public class HbtOnlineUserCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtOnlineUserCreateDto()
    {
        ConnectionId = string.Empty;
        DeviceId = string.Empty;
        ClientIp = string.Empty;
        UserAgent = string.Empty;
        OnlineStatus = 1;
    }



    /// <summary>
    /// 用户ID
    /// </summary>
    [Required(ErrorMessage = "用户ID不能为空")]
    public long UserId { get; set; }

    /// <summary>
    /// 用户组ID
    /// </summary>
    [Required(ErrorMessage = "用户组ID不能为空")]
    public long GroupId { get; set; }

    /// <summary>
    /// 连接ID
    /// </summary>
    [Required(ErrorMessage = "连接ID不能为空")]
    [MaxLength(50, ErrorMessage = "连接ID长度不能超过50个字符")]
    public string ConnectionId { get; set; }

    /// <summary>
    /// 设备ID
    /// </summary>
    [Required(ErrorMessage = "设备ID不能为空")]
    [MaxLength(50, ErrorMessage = "设备ID长度不能超过50个字符")]
    public string DeviceId { get; set; }

    /// <summary>
    /// 客户端IP
    /// </summary>
    [Required(ErrorMessage = "客户端IP不能为空")]
    [MaxLength(50, ErrorMessage = "客户端IP长度不能超过50个字符")]
    public string ClientIp { get; set; }

    /// <summary>
    /// 用户代理
    /// </summary>
    [Required(ErrorMessage = "用户代理不能为空")]
    [MaxLength(500, ErrorMessage = "用户代理长度不能超过500个字符")]
    public string UserAgent { get; set; }

    /// <summary>
    /// 在线状态（0离线 1在线）
    /// </summary>
    public int OnlineStatus { get; set; }
}

/// <summary>
/// 在线用户更新对象
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
public class HbtOnlineUserUpdateDto : HbtOnlineUserCreateDto
{
    /// <summary>
    /// 最后活动时间
    /// </summary>
    public DateTime LastActivity { get; set; }

    /// <summary>
    /// 最后心跳时间
    /// </summary>
    public DateTime LastHeartbeat { get; set; }
}

/// <summary>
/// 在线用户导出对象
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
public class HbtOnlineUserExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtOnlineUserExportDto()
    {
        ConnectionId = string.Empty;
        DeviceId = string.Empty;
        ClientIp = string.Empty;
        UserAgent = string.Empty;
        LastActivity = DateTime.Now;
        LastHeartbeat = DateTime.Now;
        OnlineStatus = 1;
    }

    /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 用户组ID
    /// </summary>
    public long GroupId { get; set; }

    /// <summary>
    /// 连接ID
    /// </summary>
    public string ConnectionId { get; set; }

    /// <summary>
    /// 设备ID
    /// </summary>
    public string DeviceId { get; set; }

    /// <summary>
    /// 客户端IP
    /// </summary>
    public string ClientIp { get; set; }

    /// <summary>
    /// 用户代理
    /// </summary>
    public string UserAgent { get; set; }

    /// <summary>
    /// 最后活动时间
    /// </summary>
    public DateTime LastActivity { get; set; }

    /// <summary>
    /// 在线状态（0离线 1在线）
    /// </summary>
    public int OnlineStatus { get; set; }

    /// <summary>
    /// 最后心跳时间
    /// </summary>
    public DateTime LastHeartbeat { get; set; }
}