//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtMessageNotifier.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述    : 消息通知服务接口
//===================================================================

using Hbt.Common.Enums;

namespace Hbt.Application.Services.SignalR
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
        /// <param name="userId">用户ID</param>
        /// <param name="message">消息内容</param>
        /// <param name="messageType">消息类型</param>
        /// <returns>是否发送成功</returns>
        Task<bool> NotifyUserAsync(long userId, string message, int messageType = 0);

        /// <summary>
        /// 通知指定群组
        /// </summary>
        /// <param name="groupName">群组名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="messageType">消息类型</param>
        /// <returns>是否发送成功</returns>
        Task<bool> NotifyGroupAsync(string groupName, string message, int messageType = 0);

        /// <summary>
        /// 通知所有用户
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="messageType">消息类型</param>
        /// <returns>是否发送成功</returns>
        Task<bool> NotifyAllAsync(string message, int messageType = 0);

        /// <summary>
        /// 通知角色
        /// </summary>
        Task<bool> NotifyRoleAsync(long roleId, string message, int messageType = 0);

        /// <summary>
        /// 通知部门
        /// </summary>
        Task<bool> NotifyDeptAsync(long deptId, string message, int messageType = 0);
    }
} 