//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtSignalRMessageNotifier.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-24 10:00
// 版本号 : V1.0.0
// 描述    : SignalR消息通知服务
//===================================================================

using Microsoft.AspNetCore.SignalR;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.IServices.SignalR;

namespace Lean.Hbt.Infrastructure.SignalR
{
    /// <summary>
    /// SignalR消息通知服务
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-24
    /// </remarks>
    public class HbtSignalRMessageNotifier : IHbtSignalRMessageNotifier
    {
        private readonly IHubContext<HbtSignalRHub, IHbtSignalRClient> _hubContext;
        private readonly IHbtSignalRUserService _signalRUserService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hubContext">SignalR Hub上下文</param>
        /// <param name="signalRUserService">SignalR用户服务</param>
        public HbtSignalRMessageNotifier(
            IHubContext<HbtSignalRHub, IHbtSignalRClient> hubContext,
            IHbtSignalRUserService signalRUserService)
        {
            _hubContext = hubContext;
            _signalRUserService = signalRUserService;
        }

        /// <summary>
        /// 通知指定用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="message">消息内容</param>
        /// <param name="messageType">消息类型，默认为系统消息</param>
        /// <returns>发送成功返回true，否则返回false</returns>
        public async Task<bool> NotifyUserAsync(long userId, string message, HbtMessageType messageType = HbtMessageType.System)
        {
            try
            {
                var connections = await _signalRUserService.GetConnectionIdsAsync(userId);
                if (connections?.Any() != true)
                    return false;

                await _hubContext.Clients.Clients(connections).ReceiveMessage(message);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 通知指定群组
        /// </summary>
        /// <param name="groupName">群组名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="messageType">消息类型，默认为系统消息</param>
        /// <returns>发送成功返回true，否则返回false</returns>
        public async Task<bool> NotifyGroupAsync(string groupName, string message, HbtMessageType messageType = HbtMessageType.System)
        {
            try
            {
                await _hubContext.Clients.Group(groupName).ReceiveMessage(message);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 通知所有用户
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="messageType">消息类型，默认为系统消息</param>
        /// <returns>发送成功返回true，否则返回false</returns>
        public async Task<bool> NotifyAllAsync(string message, HbtMessageType messageType = HbtMessageType.System)
        {
            try
            {
                await _hubContext.Clients.All.ReceiveMessage(message);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 通知指定租户的所有用户
        /// </summary>
        /// <param name="tenantId">租户ID</param>
        /// <param name="message">消息内容</param>
        /// <param name="messageType">消息类型，默认为系统消息</param>
        /// <returns>发送成功返回true，否则返回false</returns>
        public async Task<bool> NotifyTenantAsync(long tenantId, string message, HbtMessageType messageType = HbtMessageType.System)
        {
            try
            {
                var connections = await _signalRUserService.GetGroupConnectionIdsAsync(tenantId);
                if (connections?.Any() != true)
                    return false;

                await _hubContext.Clients.Clients(connections).ReceiveMessage(message);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 发送消息给指定用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="message">消息内容</param>
        /// <param name="messageType">消息类型</param>
        /// <returns>异步任务</returns>
        public async Task SendMessageToUserAsync(long userId, string message, HbtMessageType messageType)
        {
            await NotifyUserAsync(userId, message, messageType);
        }

        /// <summary>
        /// 发送消息给指定用户组
        /// </summary>
        /// <param name="groupName">群组名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="messageType">消息类型</param>
        /// <returns>异步任务</returns>
        public async Task SendMessageToGroupAsync(string groupName, string message, HbtMessageType messageType)
        {
            await NotifyGroupAsync(groupName, message, messageType);
        }

        /// <summary>
        /// 发送消息给所有用户
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="messageType">消息类型</param>
        /// <returns>异步任务</returns>
        public async Task SendMessageToAllAsync(string message, HbtMessageType messageType)
        {
            await NotifyAllAsync(message, messageType);
        }

        /// <summary>
        /// 发送消息给除指定用户外的所有用户
        /// </summary>
        /// <param name="userId">要排除的用户ID</param>
        /// <param name="message">消息内容</param>
        /// <param name="messageType">消息类型</param>
        /// <returns>异步任务</returns>
        public async Task SendMessageToOthersAsync(long userId, string message, HbtMessageType messageType)
        {
            var connections = await _signalRUserService.GetConnectionIdsAsync(userId);
            if (connections?.Any() == true)
            {
                await _hubContext.Clients.AllExcept(connections).ReceiveMessage(message);
            }
        }
    }
} 