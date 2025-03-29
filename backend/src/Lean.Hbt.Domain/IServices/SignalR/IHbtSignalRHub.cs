using System.Threading.Tasks;

namespace Lean.Hbt.Domain.IServices.SignalR
{
    /// <summary>
    /// SignalR Hub 接口
    /// </summary>
    public interface IHbtSignalRHub
    {
        /// <summary>
        /// 断开客户端连接
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        Task DisconnectClientAsync(string connectionId);
    }
} 