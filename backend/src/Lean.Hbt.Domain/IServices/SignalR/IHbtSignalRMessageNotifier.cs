//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtSignalRMessageNotifier.cs
// 创建者 : Lean365
// 创建时间: 2024-01-24 10:00
// 版本号 : V1.0.0
// 描述    : SignalR消息通知接口
//===================================================================

using System.Threading.Tasks;
using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.Domain.IServices.SignalR
{
    /// <summary>
    /// SignalR消息通知接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-24
    /// </remarks>
    public interface IHbtSignalRMessageNotifier
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