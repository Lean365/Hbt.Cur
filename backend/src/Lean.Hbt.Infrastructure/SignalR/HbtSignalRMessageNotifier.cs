//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtSignalRMessageNotifier.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V1.0.0
// 描述    : SignalR消息通知服务
//===================================================================

using Microsoft.AspNetCore.SignalR;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.Interfaces.SignalR;

namespace Lean.Hbt.Infrastructure.SignalR
{
    /// <summary>
    /// SignalR消息通知服务
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtSignalRMessageNotifier : IHbtMessageNotifier
    {
        private readonly IHubContext<HbtOnlineHub, IHbtOnlineClient> _hubContext;
        private readonly IHbtOnlineUserService _onlineUserService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSignalRMessageNotifier(
            IHubContext<HbtOnlineHub, IHbtOnlineClient> hubContext,
            IHbtOnlineUserService onlineUserService)
        {
            _hubContext = hubContext;
            _onlineUserService = onlineUserService;
        }

        /// <summary>
        /// 通知指定用户
        /// </summary>
        public async Task<bool> NotifyUserAsync(long userId, string message, HbtMessageType messageType = HbtMessageType.System)
        {
            try
            {
                var connections = await _onlineUserService.GetConnectionIdsAsync(userId);
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
        public async Task<bool> NotifyTenantAsync(long tenantId, string message, HbtMessageType messageType = HbtMessageType.System)
        {
            try
            {
                var connections = await _onlineUserService.GetGroupConnectionIdsAsync(tenantId);
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
    }
} 