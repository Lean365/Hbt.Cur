using System.Threading.Tasks;
using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.Domain.Interfaces.SignalR
{
    /// <summary>
    /// 消息通知服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public interface IHbtMessageNotifier
    {
        /// <summary>
        /// 通知指定用户
        /// </summary>
        Task<bool> NotifyUserAsync(long userId, string message, HbtMessageType messageType = HbtMessageType.System);

        /// <summary>
        /// 通知指定群组
        /// </summary>
        Task<bool> NotifyGroupAsync(string groupName, string message, HbtMessageType messageType = HbtMessageType.System);

        /// <summary>
        /// 通知所有用户
        /// </summary>
        Task<bool> NotifyAllAsync(string message, HbtMessageType messageType = HbtMessageType.System);

        /// <summary>
        /// 通知指定租户的所有用户
        /// </summary>
        Task<bool> NotifyTenantAsync(long tenantId, string message, HbtMessageType messageType = HbtMessageType.System);
    }
} 