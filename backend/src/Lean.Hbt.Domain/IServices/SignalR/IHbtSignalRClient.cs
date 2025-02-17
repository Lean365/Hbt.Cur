//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtSignalRClient.cs
// 创建者 : Lean365
// 创建时间: 2024-01-24 10:00
// 版本号 : V1.0.0
// 描述    : SignalR客户端接口
//===================================================================

using System.Threading.Tasks;

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
    }
} 