//===================================================================
// 项目名: Hbt.Cur.Application
// 文件名: IHbtBusinessNotificationService.cs
// 创建者: Lean365
// 创建时间: 2024-06-01
// 版本号: V0.0.1
// 描述: 业务通知服务接口
//===================================================================


//===================================================================
// 项目名: Hbt.Cur.Application
// 文件名: IHbtBusinessNotificationService.cs
// 创建者: Lean365
// 创建时间: 2024-06-01
// 版本号: V0.0.1
// 描述: 业务通知服务接口
//===================================================================


//===================================================================
// 项目名: Hbt.Cur.Application
// 文件名: IHbtBusinessNotificationService.cs
// 创建者: Lean365
// 创建时间: 2024-06-01
// 版本号: V0.0.1
// 描述: 业务通知服务接口
//===================================================================


//===================================================================
// 项目名: Hbt.Cur.Application
// 文件名: IHbtBusinessNotificationService.cs
// 创建者: Lean365
// 创建时间: 2024-06-01
// 版本号: V0.0.1
// 描述: 业务通知服务接口
//===================================================================

using Hbt.Cur.Common.Models;

namespace Hbt.Cur.Infrastructure.SignalR.Notification
{
    /// <summary>
    /// 业务通知服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-06-01
    /// </remarks>
    public interface IHbtSignalRNotificationService
    {
        /// <summary>
        /// 发送日程提醒通知
        /// </summary>
        /// <param name="scheduleId">日程ID</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> SendScheduleReminderAsync(long scheduleId, string title, string content, List<long> userIds);

        /// <summary>
        /// 发送会议通知
        /// </summary>
        /// <param name="meetingId">会议ID</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="participantIds">参与者ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> SendMeetingNotificationAsync(long meetingId, string title, string content, List<long> participantIds);

        /// <summary>
        /// 发送用车通知
        /// </summary>
        /// <param name="vehicleBookingId">用车预约ID</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> SendVehicleNotificationAsync(long vehicleBookingId, string title, string content, List<long> userIds);

        /// <summary>
        /// 发送ISO文档通知
        /// </summary>
        /// <param name="isoDocumentId">ISO文档ID</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> SendIsoDocumentNotificationAsync(long isoDocumentId, string title, string content, List<long> userIds);

        /// <summary>
        /// 发送日程状态更新通知
        /// </summary>
        /// <param name="scheduleId">日程ID</param>
        /// <param name="status">状态</param>
        /// <param name="message">消息</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> SendScheduleStatusUpdateAsync(long scheduleId, string status, string message, List<long> userIds);

        /// <summary>
        /// 发送会议状态更新通知
        /// </summary>
        /// <param name="meetingId">会议ID</param>
        /// <param name="status">状态</param>
        /// <param name="message">消息</param>
        /// <param name="participantIds">参与者ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> SendMeetingStatusUpdateAsync(long meetingId, string status, string message, List<long> participantIds);

        /// <summary>
        /// 发送用车状态更新通知
        /// </summary>
        /// <param name="vehicleBookingId">用车预约ID</param>
        /// <param name="status">状态</param>
        /// <param name="message">消息</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> SendVehicleStatusUpdateAsync(long vehicleBookingId, string status, string message, List<long> userIds);

        /// <summary>
        /// 发送ISO文档状态更新通知
        /// </summary>
        /// <param name="isoDocumentId">ISO文档ID</param>
        /// <param name="status">状态</param>
        /// <param name="message">消息</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> SendIsoDocumentStatusUpdateAsync(long isoDocumentId, string status, string message, List<long> userIds);

        /// <summary>
        /// 发送通用业务通知
        /// </summary>
        /// <param name="notification">通知对象</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> SendBusinessNotificationAsync(HbtRealTimeNotification notification, List<long> userIds);

        /// <summary>
        /// 发送邮件发送通知
        /// </summary>
        /// <param name="mailId">邮件ID</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> SendMailSentAsync(long mailId, string title, string content, List<long> userIds);

        /// <summary>
        /// 发送邮件接收通知
        /// </summary>
        /// <param name="mailId">邮件ID</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> SendMailReceivedAsync(long mailId, string title, string content, List<long> userIds);

        /// <summary>
        /// 发送邮件状态更新通知
        /// </summary>
        /// <param name="mailId">邮件ID</param>
        /// <param name="status">状态</param>
        /// <param name="message">消息</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> SendMailStatusUpdateAsync(long mailId, string status, string message, List<long> userIds);

        /// <summary>
        /// 发送消息发送通知
        /// </summary>
        /// <param name="messageId">消息ID</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> SendMessageSentAsync(long messageId, string title, string content, List<long> userIds);

        /// <summary>
        /// 发送消息接收通知
        /// </summary>
        /// <param name="messageId">消息ID</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> SendMessageReceivedAsync(long messageId, string title, string content, List<long> userIds);

        /// <summary>
        /// 发送消息状态更新通知
        /// </summary>
        /// <param name="messageId">消息ID</param>
        /// <param name="status">状态</param>
        /// <param name="message">消息</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> SendMessageStatusUpdateAsync(long messageId, string status, string message, List<long> userIds);

        /// <summary>
        /// 发送通知公告发布通知
        /// </summary>
        /// <param name="noticeId">通知公告ID</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> SendNoticePublishedAsync(long noticeId, string title, string content, List<long> userIds);

        /// <summary>
        /// 发送通知公告更新通知
        /// </summary>
        /// <param name="noticeId">通知公告ID</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> SendNoticeUpdatedAsync(long noticeId, string title, string content, List<long> userIds);

        /// <summary>
        /// 发送通知公告状态更新通知
        /// </summary>
        /// <param name="noticeId">通知公告ID</param>
        /// <param name="status">状态</param>
        /// <param name="message">消息</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> SendNoticeStatusUpdateAsync(long noticeId, string status, string message, List<long> userIds);

        /// <summary>
        /// 发送通知公告阅读通知
        /// </summary>
        /// <param name="noticeId">通知公告ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="isRead">是否已读</param>
        /// <returns>是否成功</returns>
        Task<bool> SendNoticeReadAsync(long noticeId, long userId, bool isRead);

        /// <summary>
        /// 发送通知公告未读提醒
        /// </summary>
        /// <param name="noticeId">通知公告ID</param>
        /// <param name="title">标题</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> SendNoticeUnreadAsync(long noticeId, string title, List<long> userIds);

        /// <summary>
        /// 发送邮件模板更新通知
        /// </summary>
        /// <param name="templateId">模板ID</param>
        /// <param name="templateName">模板名称</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> SendMailTemplateUpdateAsync(long templateId, string templateName, List<long> userIds);

        /// <summary>
        /// 发送邮件发送失败通知
        /// </summary>
        /// <param name="mailId">邮件ID</param>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> SendMailSendFailedAsync(long mailId, string errorMessage, List<long> userIds);

        /// <summary>
        /// 发送消息发送失败通知
        /// </summary>
        /// <param name="messageId">消息ID</param>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> SendMessageSendFailedAsync(long messageId, string errorMessage, List<long> userIds);

        /// <summary>
        /// 发送通知公告发布失败通知
        /// </summary>
        /// <param name="noticeId">通知公告ID</param>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> SendNoticePublishFailedAsync(long noticeId, string errorMessage, List<long> userIds);
    }
}