//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : IHbtOnlineUserService.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V1.0.0
// 描述    : 在线用户服务接口
//===================================================================

using Lean.Hbt.Application.Dtos.RealTime;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Services.RealTime;

/// <summary>
/// 在线用户服务接口
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
public interface IHbtOnlineUserService
{
    /// <summary>
    /// 获取在线用户分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    Task<HbtPagedResult<HbtOnlineUserDto>> GetPagedListAsync(HbtOnlineUserQueryDto query);

    /// <summary>
    /// 导出在线用户数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件字节数组</returns>
    Task<byte[]> ExportAsync(HbtOnlineUserQueryDto query, string sheetName = "在线用户信息");

    /// <summary>
    /// 获取用户连接ID列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>连接ID列表</returns>
    Task<List<string>> GetConnectionIdsAsync(long userId);

    /// <summary>
    /// 获取用户组连接ID列表
    /// </summary>
    /// <param name="groupId">用户组ID</param>
    /// <returns>连接ID列表</returns>
    Task<List<string>> GetGroupConnectionIdsAsync(long groupId);

    /// <summary>
    /// 保存在线用户
    /// </summary>
    /// <param name="input">在线用户信息</param>
    /// <returns>是否成功</returns>
    Task<bool> SaveOnlineUserAsync(HbtOnlineUserDto input);

    /// <summary>
    /// 删除在线用户
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteOnlineUserAsync(string connectionId);

    /// <summary>
    /// 清理过期用户
    /// </summary>
    /// <param name="minutes">超时时间(分钟)</param>
    /// <returns>清理数量</returns>
    Task<int> CleanupExpiredUsersAsync(int minutes = 20);
} 