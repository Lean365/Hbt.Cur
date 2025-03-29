using System.Threading.Tasks;

namespace Lean.Hbt.Domain.IServices.Security
{
    /// <summary>
    /// 单点登录服务接口
    /// </summary>
    public interface IHbtSingleSignOnService
    {
        /// <summary>
        /// 处理新的登录
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="connectionId">连接ID</param>
        Task HandleNewLoginAsync(string userId, string userName, string connectionId);

        /// <summary>
        /// 处理用户登出
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="connectionId">连接ID</param>
        Task HandleLogoutAsync(string userId, string connectionId);

        /// <summary>
        /// 检查用户是否已在其他设备登录
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="connectionId">当前连接ID</param>
        /// <returns>是否已在其他设备登录</returns>
        Task<bool> IsUserLoggedInElsewhereAsync(string userId, string connectionId);

        /// <summary>
        /// 获取用户的活跃连接数
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>活跃连接数</returns>
        Task<int> GetActiveConnectionCountAsync(string userId);
    }
} 