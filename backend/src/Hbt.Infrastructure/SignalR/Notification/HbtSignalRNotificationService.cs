//===================================================================
// 项目名: Hbt.Application
// 文件名: HbtBusinessNotificationService.cs
// 创建者: Lean365
// 创建时间: 2024-06-01
// 版本号: V0.0.1
// 描述: 业务通知服务实现
//===================================================================

using Hbt.Application.Services;
using Hbt.Common.Models;
using Hbt.Domain.IServices.SignalR;
using Microsoft.AspNetCore.Http;
using Hbt.Common.Enums;
using Hbt.Application.Services.Routine.Email;
using Hbt.Application.Dtos.Routine.Email;

namespace Hbt.Infrastructure.SignalR.Notification
{
    /// <summary>
    /// 业务通知服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-06-01
    /// </remarks>
    public class HbtSignalRNotificationService : HbtBaseService, IHbtSignalRNotificationService
    {
        /// <summary>
        /// SignalR客户端
        /// </summary>
        private readonly IHbtSignalRClient _signalRClient;

        /// <summary>
        /// 邮件服务
        /// </summary>
        private readonly IHbtMailService _mailService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="mailService">邮件服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtSignalRNotificationService(
            IHbtLogger logger,
            IHbtSignalRClient signalRClient,
            IHbtMailService mailService,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _signalRClient = signalRClient;
            _mailService = mailService;
        }

        /// <summary>
        /// 发送日程提醒通知
        /// </summary>
        public async Task<bool> SendScheduleReminderAsync(long scheduleId, string title, string content, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送日程提醒通知 - 日程ID: {ScheduleId}, 用户数量: {UserCount}", scheduleId, userIds.Count);

                // 发送SignalR实时通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.ScheduleReminder,
                    Title = title,
                    Content = content,
                    Timestamp = DateTime.Now,
                    Data = new { scheduleId, title, content }
                };

                foreach (var userId in userIds)
                {
                    await _signalRClient.ReceiveScheduleReminder(notification);
                }

                // 发送邮件通知（如果需要）
                await SendEmailNotificationsAsync(userIds, title, content, "日程提醒");

                _logger.Info("日程提醒通知发送成功 - 日程ID: {ScheduleId}", scheduleId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送日程提醒通知失败 - 日程ID: {ScheduleId}", scheduleId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送会议通知
        /// </summary>
        public async Task<bool> SendMeetingNotificationAsync(long meetingId, string title, string content, List<long> participantIds)
        {
            try
            {
                _logger.Info("开始发送会议通知 - 会议ID: {MeetingId}, 参与者数量: {ParticipantCount}", meetingId, participantIds.Count);

                // 发送SignalR实时通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.MeetingNotification,
                    Title = title,
                    Content = content,
                    Timestamp = DateTime.Now,
                    Data = new { meetingId, title, content }
                };

                foreach (var userId in participantIds)
                {
                    await _signalRClient.ReceiveMeetingNotification(notification);
                }

                // 发送邮件通知
                await SendEmailNotificationsAsync(participantIds, title, content, "会议通知");

                _logger.Info("会议通知发送成功 - 会议ID: {MeetingId}", meetingId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送会议通知失败 - 会议ID: {MeetingId}", meetingId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送用车通知
        /// </summary>
        public async Task<bool> SendVehicleNotificationAsync(long vehicleBookingId, string title, string content, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送用车通知 - 用车预约ID: {VehicleBookingId}, 用户数量: {UserCount}", vehicleBookingId, userIds.Count);

                // 发送SignalR实时通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.VehicleNotification,
                    Title = title,
                    Content = content,
                    Timestamp = DateTime.Now,
                    Data = new { vehicleBookingId, title, content }
                };

                foreach (var userId in userIds)
                {
                    await _signalRClient.ReceiveVehicleNotification(notification);
                }

                // 发送邮件通知
                await SendEmailNotificationsAsync(userIds, title, content, "用车通知");

                _logger.Info("用车通知发送成功 - 用车预约ID: {VehicleBookingId}", vehicleBookingId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送用车通知失败 - 用车预约ID: {VehicleBookingId}", vehicleBookingId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送ISO文档通知
        /// </summary>
        public async Task<bool> SendIsoDocumentNotificationAsync(long isoDocumentId, string title, string content, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送ISO文档通知 - ISO文档ID: {IsoDocumentId}, 用户数量: {UserCount}", isoDocumentId, userIds.Count);

                // 发送SignalR实时通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.IsoDocumentNotification,
                    Title = title,
                    Content = content,
                    Timestamp = DateTime.Now,
                    Data = new { isoDocumentId, title, content }
                };

                foreach (var userId in userIds)
                {
                    await _signalRClient.ReceiveIsoDocumentNotification(notification);
                }

                // 发送邮件通知
                await SendEmailNotificationsAsync(userIds, title, content, "ISO文档通知");

                _logger.Info("ISO文档通知发送成功 - ISO文档ID: {IsoDocumentId}", isoDocumentId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送ISO文档通知失败 - ISO文档ID: {IsoDocumentId}", isoDocumentId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送日程状态更新通知
        /// </summary>
        public async Task<bool> SendScheduleStatusUpdateAsync(long scheduleId, string status, string message, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送日程状态更新通知 - 日程ID: {ScheduleId}, 状态: {Status}", scheduleId, status);

                // 发送SignalR实时通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.ScheduleStatusUpdate,
                    Title = "日程状态更新",
                    Content = $"日程 {scheduleId} {status}: {message}",
                    Timestamp = DateTime.Now,
                    Data = new { scheduleId, status, message }
                };

                foreach (var userId in userIds)
                {
                    await _signalRClient.ReceiveScheduleStatusUpdate(notification);
                }

                _logger.Info("日程状态更新通知发送成功 - 日程ID: {ScheduleId}", scheduleId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送日程状态更新通知失败 - 日程ID: {ScheduleId}", scheduleId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送会议状态更新通知
        /// </summary>
        public async Task<bool> SendMeetingStatusUpdateAsync(long meetingId, string status, string message, List<long> participantIds)
        {
            try
            {
                _logger.Info("开始发送会议状态更新通知 - 会议ID: {MeetingId}, 状态: {Status}", meetingId, status);

                // 发送SignalR实时通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.MeetingStatusUpdate,
                    Title = "会议状态更新",
                    Content = $"会议 {meetingId} {status}: {message}",
                    Timestamp = DateTime.Now,
                    Data = new { meetingId, status, message }
                };

                foreach (var userId in participantIds)
                {
                    await _signalRClient.ReceiveMeetingStatusUpdate(notification);
                }

                _logger.Info("会议状态更新通知发送成功 - 会议ID: {MeetingId}", meetingId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送会议状态更新通知失败 - 会议ID: {MeetingId}", meetingId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送用车状态更新通知
        /// </summary>
        public async Task<bool> SendVehicleStatusUpdateAsync(long vehicleBookingId, string status, string message, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送用车状态更新通知 - 用车预约ID: {VehicleBookingId}, 状态: {Status}", vehicleBookingId, status);

                // 发送SignalR实时通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.VehicleStatusUpdate,
                    Title = "用车状态更新",
                    Content = $"用车预约 {vehicleBookingId} {status}: {message}",
                    Timestamp = DateTime.Now,
                    Data = new { vehicleBookingId, status, message }
                };

                foreach (var userId in userIds)
                {
                    await _signalRClient.ReceiveVehicleStatusUpdate(notification);
                }

                _logger.Info("用车状态更新通知发送成功 - 用车预约ID: {VehicleBookingId}", vehicleBookingId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送用车状态更新通知失败 - 用车预约ID: {VehicleBookingId}", vehicleBookingId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送ISO文档状态更新通知
        /// </summary>
        public async Task<bool> SendIsoDocumentStatusUpdateAsync(long isoDocumentId, string status, string message, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送ISO文档状态更新通知 - ISO文档ID: {IsoDocumentId}, 状态: {Status}", isoDocumentId, status);

                // 发送SignalR实时通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.IsoDocumentStatusUpdate,
                    Title = "ISO文档状态更新",
                    Content = $"ISO文档 {isoDocumentId} {status}: {message}",
                    Timestamp = DateTime.Now,
                    Data = new { isoDocumentId, status, message }
                };

                foreach (var userId in userIds)
                {
                    await _signalRClient.ReceiveIsoDocumentStatusUpdate(notification);
                }

                _logger.Info("ISO文档状态更新通知发送成功 - ISO文档ID: {IsoDocumentId}", isoDocumentId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送ISO文档状态更新通知失败 - ISO文档ID: {IsoDocumentId}", isoDocumentId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送通用业务通知
        /// </summary>
        public async Task<bool> SendBusinessNotificationAsync(HbtRealTimeNotification notification, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送通用业务通知 - 类型: {Type}, 用户数量: {UserCount}", notification.Type, userIds.Count);

                // 发送SignalR实时通知
                foreach (var userId in userIds)
                {
                    await _signalRClient.ReceivePersonalNotice(notification);
                }

                _logger.Info("通用业务通知发送成功");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送通用业务通知失败", ex);
                return false;
            }
        }

        /// <summary>
        /// 发送邮件发送通知
        /// </summary>
        public async Task<bool> SendMailSentAsync(long mailId, string title, string content, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送邮件发送通知 - 邮件ID: {MailId}, 用户数量: {UserCount}", mailId, userIds.Count);

                // 发送SignalR实时通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.MailSent,
                    Title = "邮件发送通知",
                    Content = $"邮件已发送: {title}",
                    Timestamp = DateTime.Now,
                    Data = new { mailId, title, content }
                };

                foreach (var userId in userIds)
                {
                    await _signalRClient.ReceiveMailSent(notification);
                }

                _logger.Info("邮件发送通知发送成功 - 邮件ID: {MailId}", mailId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送邮件发送通知失败 - 邮件ID: {MailId}", mailId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送邮件接收通知
        /// </summary>
        public async Task<bool> SendMailReceivedAsync(long mailId, string title, string content, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送邮件接收通知 - 邮件ID: {MailId}, 用户数量: {UserCount}", mailId, userIds.Count);

                // 发送SignalR实时通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.MailReceived,
                    Title = "新邮件通知",
                    Content = $"收到新邮件: {title}",
                    Timestamp = DateTime.Now,
                    Data = new { mailId, title, content }
                };

                foreach (var userId in userIds)
                {
                    await _signalRClient.ReceiveMailReceived(notification);
                }

                _logger.Info("邮件接收通知发送成功 - 邮件ID: {MailId}", mailId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送邮件接收通知失败 - 邮件ID: {MailId}", mailId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送邮件状态更新通知
        /// </summary>
        public async Task<bool> SendMailStatusUpdateAsync(long mailId, string status, string message, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送邮件状态更新通知 - 邮件ID: {MailId}, 状态: {Status}", mailId, status);

                // 发送SignalR实时通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.MailStatusUpdate,
                    Title = "邮件状态更新",
                    Content = $"邮件 {mailId} {status}: {message}",
                    Timestamp = DateTime.Now,
                    Data = new { mailId, status, message }
                };

                foreach (var userId in userIds)
                {
                    await _signalRClient.ReceiveMailStatusUpdate(notification);
                }

                _logger.Info("邮件状态更新通知发送成功 - 邮件ID: {MailId}", mailId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送邮件状态更新通知失败 - 邮件ID: {MailId}", mailId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送消息发送通知
        /// </summary>
        public async Task<bool> SendMessageSentAsync(long messageId, string title, string content, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送消息发送通知 - 消息ID: {MessageId}, 用户数量: {UserCount}", messageId, userIds.Count);

                // 发送SignalR实时通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.MessageSent,
                    Title = "消息发送通知",
                    Content = $"消息已发送: {title}",
                    Timestamp = DateTime.Now,
                    Data = new { messageId, title, content }
                };

                foreach (var userId in userIds)
                {
                    await _signalRClient.ReceiveMessageSent(notification);
                }

                _logger.Info("消息发送通知发送成功 - 消息ID: {MessageId}", messageId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送消息发送通知失败 - 消息ID: {MessageId}", messageId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送消息接收通知
        /// </summary>
        public async Task<bool> SendMessageReceivedAsync(long messageId, string title, string content, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送消息接收通知 - 消息ID: {MessageId}, 用户数量: {UserCount}", messageId, userIds.Count);

                // 发送SignalR实时通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.MessageReceived,
                    Title = "新消息通知",
                    Content = $"收到新消息: {title}",
                    Timestamp = DateTime.Now,
                    Data = new { messageId, title, content }
                };

                foreach (var userId in userIds)
                {
                    await _signalRClient.ReceiveMessageReceived(notification);
                }

                _logger.Info("消息接收通知发送成功 - 消息ID: {MessageId}", messageId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送消息接收通知失败 - 消息ID: {MessageId}", messageId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送消息状态更新通知
        /// </summary>
        public async Task<bool> SendMessageStatusUpdateAsync(long messageId, string status, string message, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送消息状态更新通知 - 消息ID: {MessageId}, 状态: {Status}", messageId, status);

                // 发送SignalR实时通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.MessageStatusUpdate,
                    Title = "消息状态更新",
                    Content = $"消息 {messageId} {status}: {message}",
                    Timestamp = DateTime.Now,
                    Data = new { messageId, status, message }
                };

                foreach (var userId in userIds)
                {
                    await _signalRClient.ReceiveMessageStatusUpdate(notification);
                }

                _logger.Info("消息状态更新通知发送成功 - 消息ID: {MessageId}", messageId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送消息状态更新通知失败 - 消息ID: {MessageId}", messageId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送通知公告发布通知
        /// </summary>
        public async Task<bool> SendNoticePublishedAsync(long noticeId, string title, string content, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送通知公告发布通知 - 通知公告ID: {NoticeId}, 用户数量: {UserCount}", noticeId, userIds.Count);

                // 发送SignalR实时通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.NoticePublished,
                    Title = "通知公告发布",
                    Content = $"新通知公告: {title}",
                    Timestamp = DateTime.Now,
                    Data = new { noticeId, title, content }
                };

                foreach (var userId in userIds)
                {
                    await _signalRClient.ReceiveNoticePublished(notification);
                }

                _logger.Info("通知公告发布通知发送成功 - 通知公告ID: {NoticeId}", noticeId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送通知公告发布通知失败 - 通知公告ID: {NoticeId}", noticeId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送通知公告更新通知
        /// </summary>
        public async Task<bool> SendNoticeUpdatedAsync(long noticeId, string title, string content, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送通知公告更新通知 - 通知公告ID: {NoticeId}, 用户数量: {UserCount}", noticeId, userIds.Count);

                // 发送SignalR实时通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.NoticeUpdated,
                    Title = "通知公告更新",
                    Content = $"通知公告已更新: {title}",
                    Timestamp = DateTime.Now,
                    Data = new { noticeId, title, content }
                };

                foreach (var userId in userIds)
                {
                    await _signalRClient.ReceiveNoticeUpdated(notification);
                }

                _logger.Info("通知公告更新通知发送成功 - 通知公告ID: {NoticeId}", noticeId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送通知公告更新通知失败 - 通知公告ID: {NoticeId}", noticeId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送通知公告状态更新通知
        /// </summary>
        public async Task<bool> SendNoticeStatusUpdateAsync(long noticeId, string status, string message, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送通知公告状态更新通知 - 通知公告ID: {NoticeId}, 状态: {Status}", noticeId, status);

                // 发送SignalR实时通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.NoticeStatusUpdate,
                    Title = "通知公告状态更新",
                    Content = $"通知公告 {noticeId} {status}: {message}",
                    Timestamp = DateTime.Now,
                    Data = new { noticeId, status, message }
                };

                foreach (var userId in userIds)
                {
                    await _signalRClient.ReceiveNoticeStatusUpdate(notification);
                }

                _logger.Info("通知公告状态更新通知发送成功 - 通知公告ID: {NoticeId}", noticeId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送通知公告状态更新通知失败 - 通知公告ID: {NoticeId}", noticeId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送通知公告阅读通知
        /// </summary>
        public async Task<bool> SendNoticeReadAsync(long noticeId, long userId, bool isRead)
        {
            try
            {
                _logger.Info("开始发送通知公告阅读通知 - 通知公告ID: {NoticeId}, 用户ID: {UserId}, 已读: {IsRead}", noticeId, userId, isRead);

                // 发送SignalR实时通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.NoticeRead,
                    Title = "通知公告阅读状态",
                    Content = $"通知公告 {noticeId} 已{(isRead ? "读" : "未读")}",
                    Timestamp = DateTime.Now,
                    Data = new { noticeId, userId, isRead }
                };

                await _signalRClient.ReceiveNoticeRead(notification);

                _logger.Info("通知公告阅读通知发送成功 - 通知公告ID: {NoticeId}", noticeId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送通知公告阅读通知失败 - 通知公告ID: {NoticeId}", noticeId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送通知公告未读提醒
        /// </summary>
        public async Task<bool> SendNoticeUnreadAsync(long noticeId, string title, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送通知公告未读提醒 - 通知公告ID: {NoticeId}, 用户数量: {UserCount}", noticeId, userIds.Count);

                // 发送SignalR实时通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.NoticeUnread,
                    Title = "未读通知提醒",
                    Content = $"您有未读通知: {title}",
                    Timestamp = DateTime.Now,
                    Data = new { noticeId, title }
                };

                foreach (var userId in userIds)
                {
                    await _signalRClient.ReceiveNoticeUnread(notification);
                }

                _logger.Info("通知公告未读提醒发送成功 - 通知公告ID: {NoticeId}", noticeId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送通知公告未读提醒失败 - 通知公告ID: {NoticeId}", noticeId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送邮件模板更新通知
        /// </summary>
        public async Task<bool> SendMailTemplateUpdateAsync(long templateId, string templateName, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送邮件模板更新通知 - 模板ID: {TemplateId}, 用户数量: {UserCount}", templateId, userIds.Count);

                // 发送SignalR实时通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.MailTemplateUpdate,
                    Title = "邮件模板更新",
                    Content = $"邮件模板已更新: {templateName}",
                    Timestamp = DateTime.Now,
                    Data = new { templateId, templateName }
                };

                foreach (var userId in userIds)
                {
                    await _signalRClient.ReceiveMailTemplateUpdate(notification);
                }

                _logger.Info("邮件模板更新通知发送成功 - 模板ID: {TemplateId}", templateId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送邮件模板更新通知失败 - 模板ID: {TemplateId}", templateId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送邮件发送失败通知
        /// </summary>
        public async Task<bool> SendMailSendFailedAsync(long mailId, string errorMessage, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送邮件发送失败通知 - 邮件ID: {MailId}, 用户数量: {UserCount}", mailId, userIds.Count);

                // 发送SignalR实时通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.MailSendFailed,
                    Title = "邮件发送失败",
                    Content = $"邮件发送失败: {errorMessage}",
                    Timestamp = DateTime.Now,
                    Data = new { mailId, errorMessage }
                };

                foreach (var userId in userIds)
                {
                    await _signalRClient.ReceiveMailSendFailed(notification);
                }

                _logger.Info("邮件发送失败通知发送成功 - 邮件ID: {MailId}", mailId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送邮件发送失败通知失败 - 邮件ID: {MailId}", mailId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送消息发送失败通知
        /// </summary>
        public async Task<bool> SendMessageSendFailedAsync(long messageId, string errorMessage, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送消息发送失败通知 - 消息ID: {MessageId}, 用户数量: {UserCount}", messageId, userIds.Count);

                // 发送SignalR实时通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.MessageSendFailed,
                    Title = "消息发送失败",
                    Content = $"消息发送失败: {errorMessage}",
                    Timestamp = DateTime.Now,
                    Data = new { messageId, errorMessage }
                };

                foreach (var userId in userIds)
                {
                    await _signalRClient.ReceiveMessageSendFailed(notification);
                }

                _logger.Info("消息发送失败通知发送成功 - 消息ID: {MessageId}", messageId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送消息发送失败通知失败 - 消息ID: {MessageId}", messageId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送通知公告发布失败通知
        /// </summary>
        public async Task<bool> SendNoticePublishFailedAsync(long noticeId, string errorMessage, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送通知公告发布失败通知 - 通知公告ID: {NoticeId}, 用户数量: {UserCount}", noticeId, userIds.Count);

                // 发送SignalR实时通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.NoticePublishFailed,
                    Title = "通知公告发布失败",
                    Content = $"通知公告发布失败: {errorMessage}",
                    Timestamp = DateTime.Now,
                    Data = new { noticeId, errorMessage }
                };

                foreach (var userId in userIds)
                {
                    await _signalRClient.ReceiveNoticePublishFailed(notification);
                }

                _logger.Info("通知公告发布失败通知发送成功 - 通知公告ID: {NoticeId}", noticeId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送通知公告发布失败通知失败 - 通知公告ID: {NoticeId}", noticeId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送邮件通知
        /// </summary>
        /// <param name="userIds">用户ID列表</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="notificationType">通知类型</param>
        private async Task SendEmailNotificationsAsync(List<long> userIds, string title, string content, string notificationType)
        {
            try
            {
                // TODO: 根据用户ID获取用户邮箱地址
                // 这里需要实现获取用户邮箱的逻辑
                var userEmails = await GetUserEmailsAsync(userIds);

                if (userEmails.Any())
                {
                    foreach (var email in userEmails)
                    {
                        // 发送邮件通知
                        var mailSendDto = new HbtMailSendDto
                        {
                            MailTo = email,
                            MailSubject = $"[{notificationType}] {title}",
                            MailBody = content,
                            MailIsHtml = 1
                        };

                        var result = await _mailService.SendAsync(mailSendDto);
                    }

                    _logger.Info("邮件通知发送成功 - 邮件数量: {EmailCount}", userEmails.Count);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("发送邮件通知失败", ex);
            }
        }

        /// <summary>
        /// 获取用户邮箱地址
        /// </summary>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>邮箱地址列表</returns>
        private async Task<List<string>> GetUserEmailsAsync(List<long> userIds)
        {
            // TODO: 实现从用户服务获取邮箱地址的逻辑
            // 这里需要注入用户服务来获取用户邮箱
            return new List<string>();
        }
    }
}