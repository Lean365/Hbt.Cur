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
        MailUnread = 6
    }
}