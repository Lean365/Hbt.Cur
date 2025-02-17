//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtSignalRUserService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-24 10:00
// 版本号 : V1.0.0
// 描述    : SignalR用户服务接口
//===================================================================

using System.Threading.Tasks;
using System.Collections.Generic;
using Lean.Hbt.Domain.Models.SignalR;

namespace Lean.Hbt.Domain.IServices.SignalR
{
    /// <summary>
    /// SignalR用户服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-24
    /// </remarks>
    public interface IHbtSignalRUserService
    {
        /// <summary>
        /// 保存在线用户
        /// </summary>
        Task SaveOnlineUserAsync(HbtOnlineUser user);

        /// <summary>
        /// 删除在线用户
        /// </summary>
        Task DeleteOnlineUserAsync(string connectionId);

        /// <summary>
        /// 获取用户的连接ID列表
        /// </summary>
        Task<List<string>> GetConnectionIdsAsync(long userId);

        /// <summary>
        /// 获取租户组的连接ID列表
        /// </summary>
        Task<List<string>> GetGroupConnectionIdsAsync(long tenantId);
    }
} 