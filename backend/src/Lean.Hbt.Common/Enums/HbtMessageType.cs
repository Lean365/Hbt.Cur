namespace Lean.Hbt.Common.Enums
{
    /// <summary>
    /// 消息类型枚举
    /// </summary>
    public enum HbtMessageType
    {
        /// <summary>
        /// 系统消息
        /// </summary>
        System = 0,

        /// <summary>
        /// 用户消息
        /// </summary>
        User = 1,

        /// <summary>
        /// 通知消息
        /// </summary>
        Notification = 2,

        /// <summary>
        /// 警告消息
        /// </summary>
        Warning = 3,

        /// <summary>
        /// 错误消息
        /// </summary>
        Error = 4,

        /// <summary>
        /// 邮件已读
        /// </summary>
        MailRead = 5,

        /// <summary>
        /// 邮件未读
        /// </summary>
        MailUnread = 6,

        /// <summary>
        /// 日程提醒
        /// </summary>
        ScheduleReminder = 7,

        /// <summary>
        /// 会议通知
        /// </summary>
        MeetingNotification = 8,

        /// <summary>
        /// 用车通知
        /// </summary>
        VehicleNotification = 9,

        /// <summary>
        /// ISO文档通知
        /// </summary>
        IsoDocumentNotification = 10,

        /// <summary>
        /// 日程状态更新
        /// </summary>
        ScheduleStatusUpdate = 11,

        /// <summary>
        /// 会议状态更新
        /// </summary>
        MeetingStatusUpdate = 12,

        /// <summary>
        /// 用车状态更新
        /// </summary>
        VehicleStatusUpdate = 13,

        /// <summary>
        /// ISO文档状态更新
        /// </summary>
        IsoDocumentStatusUpdate = 14,

        /// <summary>
        /// 邮件发送通知
        /// </summary>
        MailSent = 15,

        /// <summary>
        /// 邮件接收通知
        /// </summary>
        MailReceived = 16,

        /// <summary>
        /// 邮件状态更新
        /// </summary>
        MailStatusUpdate = 17,

        /// <summary>
        /// 消息发送通知
        /// </summary>
        MessageSent = 18,

        /// <summary>
        /// 消息接收通知
        /// </summary>
        MessageReceived = 19,

        /// <summary>
        /// 消息状态更新
        /// </summary>
        MessageStatusUpdate = 20,

        /// <summary>
        /// 通知公告发布
        /// </summary>
        NoticePublished = 21,

        /// <summary>
        /// 通知公告更新
        /// </summary>
        NoticeUpdated = 22,

        /// <summary>
        /// 通知公告状态更新
        /// </summary>
        NoticeStatusUpdate = 23,

        /// <summary>
        /// 通知公告阅读通知
        /// </summary>
        NoticeRead = 24,

        /// <summary>
        /// 通知公告未读提醒
        /// </summary>
        NoticeUnread = 25,

        /// <summary>
        /// 邮件模板更新通知
        /// </summary>
        MailTemplateUpdate = 26,

        /// <summary>
        /// 邮件发送失败通知
        /// </summary>
        MailSendFailed = 27,

        /// <summary>
        /// 消息发送失败通知
        /// </summary>
        MessageSendFailed = 28,

        /// <summary>
        /// 通知公告发布失败通知
        /// </summary>
        NoticePublishFailed = 29
    }
}