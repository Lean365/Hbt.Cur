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
using Lean.Hbt.Domain.Entities.RealTime;

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
        Task<bool> DeleteOnlineUserAsync(string connectionId, string deleteBy);

        /// <summary>
        /// 获取用户的连接ID列表
        /// </summary>
        Task<List<string>> GetConnectionIdsAsync(long userId);

        /// <summary>
        /// 获取租户组的连接ID列表
        /// </summary>
        Task<List<string>> GetGroupConnectionIdsAsync(long tenantId);

        /// <summary>
        /// 更新用户最后活动时间
        /// </summary>
        Task UpdateUserLastActiveTimeAsync(string connectionId);

        /// <summary>
        /// 获取邮件接收者列表
        /// </summary>
        Task<List<long>> GetMailReceivers(long mailId);

        /// <summary>
        /// 获取通知接收者列表
        /// </summary>
        Task<List<long>> GetNoticeReceivers(long noticeId);

        /// <summary>
        /// 断开用户连接
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        Task DisconnectUserAsync(string connectionId);

        /// <summary>
        /// 发送消息给指定连接
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="method">方法名</param>
        /// <param name="args">参数</param>
        Task SendMessageAsync(string connectionId, string method, object[] args);
    }
} 