//===================================================================
// 项目名 : Lean.Hbt.Domain
// 文件名 : IHbtSignalRClient.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : SignalR相关接口定义
//===================================================================

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.RealTime;
using Lean.Hbt.Common.Enums;

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
        /// 接收邮件状态
        /// </summary>
        Task ReceiveMailStatus(HbtRealTimeNotification notification);

        /// <summary>
        /// 接收通知状态
        /// </summary>
        Task ReceiveNoticeStatus(HbtRealTimeNotification notification);

        /// <summary>
        /// 接收任务状态
        /// </summary>
        Task ReceiveTaskStatus(HbtRealTimeNotification notification);

        /// <summary>
        /// 接收个人通知
        /// </summary>
        Task ReceivePersonalNotice(HbtRealTimeNotification notification);

        /// <summary>
        /// 接收系统广播
        /// </summary>
        Task ReceiveBroadcast(HbtRealTimeNotification notification);

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

        /// <summary>
        /// 接收心跳
        /// </summary>
        Task ReceiveHeartbeat(DateTime timestamp);

        /// <summary>
        /// 强制下线通知
        /// </summary>
        /// <param name="message">通知消息</param>
        Task ForceOffline(string message);
    }

    /// <summary>
    /// SignalR用户服务接口
    /// </summary>
    public interface IHbtSignalRUserService
    {
        /// <summary>
        /// 获取用户设备列表
        /// </summary>
        List<HbtSignalRDevice> GetUserDevices(string userId);

        /// <summary>
        /// 获取用户设备
        /// </summary>
        HbtSignalRDevice GetUserDevice(string userId, string connectionId);

        /// <summary>
        /// 检查用户是否在线
        /// </summary>
        bool IsUserOnline(string userId);

        /// <summary>
        /// 更新设备活动时间
        /// </summary>
        void UpdateDeviceActivity(string userId, string deviceId);

        /// <summary>
        /// 获取所有在线用户
        /// </summary>
        List<HbtSignalRDevice> GetAllOnlineDevices();

        /// <summary>
        /// 保存在线用户
        /// </summary>
        Task SaveOnlineUserAsync(HbtOnlineUser user);

        /// <summary>
        /// 获取在线用户
        /// </summary>
        Task<HbtOnlineUser> GetOnlineUserAsync(string connectionId);

        /// <summary>
        /// 获取在线用户
        /// </summary>
        Task<HbtOnlineUser> GetOnlineUserByDeviceAsync(long userId, string deviceId);

        /// <summary>
        /// 删除在线用户
        /// </summary>
        Task<bool> DeleteOnlineUserAsync(string connectionId);

        /// <summary>
        /// 获取在线用户列表
        /// </summary>
        Task<List<string>> GetConnectionIdsAsync(long userId);

        /// <summary>
        /// 断开用户连接
        /// </summary>
        Task DisconnectUserAsync(string connectionId);

        /// <summary>
        /// 发送消息
        /// </summary>
        Task SendMessageAsync(string connectionId, string method, object[] args);

        /// <summary>
        /// 获取设备信息
        /// </summary>
        Task<string> GetDeviceInfo(string connectionId);

        /// <summary>
        /// 移除用户设备
        /// </summary>
        Task RemoveUserDevice(string userId, string deviceId);

        /// <summary>
        /// 获取邮件接收者列表
        /// </summary>
        Task<List<string>> GetMailReceivers(long userId);

        /// <summary>
        /// 获取通知接收者列表
        /// </summary>
        Task<List<string>> GetNoticeReceivers(long userId);

        /// <summary>
        /// 添加用户设备
        /// </summary>
        Task AddUserDevice(string userId, string deviceId, string connectionId);
    }

    /// <summary>
    /// SignalR Hub 接口
    /// </summary>
    public interface IHbtSignalRHub
    {
        /// <summary>
        /// 断开客户端连接
        /// </summary>
        Task DisconnectClientAsync(string connectionId);
    }
} 