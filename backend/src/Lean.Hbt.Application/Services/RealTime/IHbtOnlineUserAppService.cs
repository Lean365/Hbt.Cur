//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : IHbtOnlineUserAppService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V1.0.0
// 描述    : 在线用户应用服务接口
//===================================================================

using Lean.Hbt.Application.Dtos.RealTime;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Services.RealTime;

/// <summary>
/// 在线用户应用服务接口
/// </summary>
public interface IHbtOnlineUserAppService
{
    /// <summary>
    /// 获取用户连接ID列表
    /// </summary>
    Task<List<string>> GetConnectionIdsAsync(long userId);

    /// <summary>
    /// 获取用户组连接ID列表
    /// </summary>
    Task<List<string>> GetGroupConnectionIdsAsync(long groupId);

    /// <summary>
    /// 获取在线用户分页列表
    /// </summary>
    Task<HbtPagedResult<HbtOnlineUserDto>> GetPagedListAsync(HbtOnlineUserQueryDto query);

    /// <summary>
    /// 获取在线用户导出数据
    /// </summary>
    Task<List<HbtOnlineUserExportDto>> GetExportDataAsync(HbtOnlineUserQueryDto query);

    /// <summary>
    /// 保存在线用户
    /// </summary>
    Task<bool> SaveOnlineUserAsync(HbtOnlineUserDto input);

    /// <summary>
    /// 删除在线用户
    /// </summary>
    Task<bool> DeleteOnlineUserAsync(string connectionId);

    /// <summary>
    /// 清理过期用户
    /// </summary>
    Task<int> CleanupExpiredUsersAsync(int minutes = 20);
} 