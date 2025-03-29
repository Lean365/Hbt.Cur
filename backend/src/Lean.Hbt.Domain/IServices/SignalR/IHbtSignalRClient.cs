//===================================================================
// 项目名 : Lean.Hbt.Domain
// 文件名 : IHbtSignalRClient.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : SignalR客户端接口
//===================================================================

using System;
using System.Threading.Tasks;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Domain.IServices.SignalR
{
    /// <summary>
    /// SignalR客户端接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-24
    /// </remarks>
    public interface IHbtSignalRClient
    {
        /// <summary>
        /// 接收消息
        /// </summary>
        Task ReceiveMessage(string message);

        /// <summary>
        /// 接收邮件状态
        /// </summary>
        Task ReceiveMailStatus(HbtRealTimeNotification notification);

        /// <summary>
        /// 接收通知状态
        /// </summary>
        Task ReceiveNoticeStatus(HbtRealTimeNotification notification);

        /// <summary>
        /// 接收任务状态
        /// </summary>
        Task ReceiveTaskStatus(HbtRealTimeNotification notification);

        /// <summary>
        /// 接收个人通知
        /// </summary>
        Task ReceivePersonalNotice(HbtRealTimeNotification notification);

        /// <summary>
        /// 接收系统广播
        /// </summary>
        Task ReceiveBroadcast(HbtRealTimeNotification notification);

        /// <summary>
        /// 用户上线通知
        /// </summary>
        Task UserOnline(long userId);

        /// <summary>
        /// 用户下线通知
        /// </summary>
        Task UserOffline(long userId);

        /// <summary>
        /// 加入群组通知
        /// </summary>
        Task JoinedGroup(string connectionId, string groupName);

        /// <summary>
        /// 离开群组通知
        /// </summary>
        Task LeftGroup(string connectionId, string groupName);

        /// <summary>
        /// 接收心跳
        /// </summary>
        Task ReceiveHeartbeat(DateTime timestamp);

        /// <summary>
        /// 强制下线通知
        /// </summary>
        /// <param name="message">通知消息</param>
        Task ForceOffline(string message);
    }
} 