//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtOnlineUserDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V1.0.0
// 描述    : 在线用户传输对象
//===================================================================

using Lean.Hbt.Common.Models;

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
    public long? TenantId { get; set; }

    /// <summary>
    /// 用户ID
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// 客户端IP
    /// </summary>
    public string ClientIp { get; set; }

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
public class HbtOnlineUserDto
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