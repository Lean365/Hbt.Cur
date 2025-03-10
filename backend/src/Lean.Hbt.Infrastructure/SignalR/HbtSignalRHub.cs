//===================================================================
// 项目名 : Lean.Hbt.Infrastructure
// 文件名 : HbtSignalRHub.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : SignalR集线器
//===================================================================

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.IServices.SignalR;

namespace Lean.Hbt.Infrastructure.SignalR
{
    /// <summary>
    /// SignalR集线器
    /// </summary>
    public class HbtSignalRHub : Hub<IHbtSignalRClient>
    {
        /// <summary>
        /// 连接建立时
        /// </summary>
        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"User_{userId}");
            }
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// 连接断开时
        /// </summary>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"User_{userId}");
            }
            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// 发送邮件状态更新
        /// </summary>
        public async Task SendMailStatus(long mailId, string status, string message)
        {
            var notification = new HbtRealTimeNotification
            {
                Type = HbtMessageType.System,
                Title = "邮件状态更新",
                Content = $"邮件 {mailId} {status}: {message}",
                Timestamp = DateTime.Now
            };
            await Clients.All.ReceiveMailStatus(notification);
        }

        /// <summary>
        /// 发送通知状态更新
        /// </summary>
        public async Task SendNoticeStatus(long noticeId, string status, string message)
        {
            var notification = new HbtRealTimeNotification
            {
                Type = HbtMessageType.Notification,
                Title = "通知状态更新",
                Content = $"通知 {noticeId} {status}: {message}",
                Timestamp = DateTime.Now
            };
            await Clients.All.ReceiveNoticeStatus(notification);
        }

        /// <summary>
        /// 发送定时任务状态更新
        /// </summary>
        public async Task SendTaskStatus(long taskId, string status, string message)
        {
            var notification = new HbtRealTimeNotification
            {
                Type = HbtMessageType.System,
                Title = "定时任务状态更新",
                Content = $"任务 {taskId} {status}: {message}",
                Timestamp = DateTime.Now
            };
            await Clients.All.ReceiveTaskStatus(notification);
        }

        /// <summary>
        /// 发送个人通知
        /// </summary>
        public async Task SendPersonalNotice(string userId, string title, string content)
        {
            var notification = new HbtRealTimeNotification
            {
                Type = HbtMessageType.User,
                Title = title,
                Content = content,
                Timestamp = DateTime.Now
            };
            await Clients.Group($"User_{userId}").ReceivePersonalNotice(notification);
        }

        /// <summary>
        /// 发送系统广播
        /// </summary>
        public async Task SendBroadcast(string title, string content)
        {
            var notification = new HbtRealTimeNotification
            {
                Type = HbtMessageType.System,
                Title = title,
                Content = content,
                Timestamp = DateTime.Now
            };
            await Clients.All.ReceiveBroadcast(notification);
        }
    }
} 