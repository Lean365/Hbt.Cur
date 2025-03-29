//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOnlineUserDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V1.0.0
// 描述    : 在线用户传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.RealTime;

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
    /// 租户ID
    /// </summary>
    public long TenantId { get; set; }

    /// <summary>
    /// 用户ID
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// 客户端IP
    /// </summary>
    [MaxLength(50, ErrorMessage = "客户端IP长度不能超过50个字符")]
    public string? ClientIp { get; set; }

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
/// 在线用户传输对象
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
public class HbtOnlineUserDto : HbtBaseEntity
{
    /// <summary>
    /// 租户ID
    /// </summary>
    public long TenantId { get; set; }

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
    /// 最后心跳时间
    /// </summary>
    public DateTime LastHeartbeat { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public string CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新人
    /// </summary>
    public string UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 删除人
    /// </summary>
    public string DeleteBy { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    public DateTime? DeleteTime { get; set; }
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
    /// 租户ID
    /// </summary>
    public long TenantId { get; set; }

    /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 连接ID
    /// </summary>
    public string ConnectionId { get; set; }

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
}