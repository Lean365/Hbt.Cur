//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtOnlineClient.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述    : SignalR在线客户端接口
//===================================================================

using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.Application.Services.SignalR
{
    /// <summary>
    /// SignalR在线客户端接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public interface IHbtOnlineClient
    {
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <returns>异步任务</returns>
        Task ReceiveMessage(string message);

        /// <summary>
        /// 用户上线通知
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>异步任务</returns>
        Task UserOnline(long userId);

        /// <summary>
        /// 用户下线通知
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>异步任务</returns>
        Task UserOffline(long userId);

        /// <summary>
        /// 加入群组通知
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="groupName">群组名称</param>
        /// <returns>异步任务</returns>
        Task JoinedGroup(string connectionId, string groupName);

        /// <summary>
        /// 离开群组通知
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="groupName">群组名称</param>
        /// <returns>异步任务</returns>
        Task LeftGroup(string connectionId, string groupName);
    }
} 