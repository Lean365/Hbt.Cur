//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : IHbtOnlineUserService.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述    : 在线用户服务接口
//===================================================================

using Hbt.Application.Dtos.SignalR;
using Hbt.Common.Models;
using System.Linq.Expressions;

namespace Hbt.Application.Services.SignalR;

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
    Task<HbtPagedResult<HbtOnlineUserDto>> GetListAsync(HbtOnlineUserQueryDto query);

    /// <summary>
    /// 导出在线用户数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件字节数组</returns>
    Task<(string fileName, byte[] content)> ExportAsync(HbtOnlineUserQueryDto query, string sheetName = "在线用户信息");

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
    /// <param name="deleteBy">删除人</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteOnlineUserAsync(string connectionId, string deleteBy);

    /// <summary>
    /// 清理过期用户
    /// </summary>
    /// <param name="minutes">超时时间(分钟)</param>
    /// <returns>清理数量</returns>
    Task<int> CleanupExpiredUsersAsync(int minutes = 20);

    /// <summary>
    /// 根据连接ID获取在线用户信息
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    /// <returns>在线用户信息</returns>
    Task<HbtOnlineUserDto> GetUserByConnectionIdAsync(string connectionId);

    /// <summary>
    /// 根据条件获取在线用户信息
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <returns>在线用户信息</returns>
    Task<HbtOnlineUserDto> GetFirstAsync(Expression<Func<HbtOnlineUserDto, bool>> predicate);

    /// <summary>
    /// 更新在线用户信息
    /// </summary>
    /// <param name="entity">在线用户信息</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateAsync(HbtOnlineUserDto entity);

    /// <summary>
    /// 获取所有在线用户
    /// </summary>
    /// <returns>在线用户列表</returns>
    Task<List<HbtOnlineUserDto>> GetAllAsync();

    /// <summary>
    /// 更新所有在线用户的心跳时间
    /// </summary>
    /// <returns>更新的记录数</returns>
    Task<int> UpdateHeartbeatAsync();
} 