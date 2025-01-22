using System.Threading.Tasks;

namespace Lean.Hbt.Domain.Interfaces.SignalR
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
        Task ReceiveMessage(string message);

        /// <summary>
        /// 加入群组
        /// </summary>
        Task JoinedGroup(string connectionId, string groupName);

        /// <summary>
        /// 离开群组
        /// </summary>
        Task LeftGroup(string connectionId, string groupName);
    }
} 