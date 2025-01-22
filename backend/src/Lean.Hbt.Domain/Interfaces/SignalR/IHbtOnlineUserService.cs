using System.Threading.Tasks;
using System.Collections.Generic;
using Lean.Hbt.Domain.Models.SignalR;

namespace Lean.Hbt.Domain.Interfaces.SignalR
{
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