//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtMail.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 邮件实体
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Routine.Email
{
    /// <summary>
    /// 邮件实体
    /// </summary>
    [SugarTable("hbt_routine_mail", "邮件")]
    [SugarIndex("index_mail_to", nameof(MailTo), OrderByType.Asc)]
    [SugarIndex("index_mail_status", nameof(MailStatus), OrderByType.Asc)]
    [SugarIndex("index_mail_send_time", nameof(MailSendTime), OrderByType.Desc)]
    public class HbtMail : HbtBaseEntity
    {
        /// <summary>
        /// 发件人
        /// </summary>
        [SugarColumn(ColumnName = "mail_from", ColumnDescription = "发件人", Length = 255, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar")]
        public string MailFrom { get; set; } = string.Empty;

        /// <summary>
        /// 收件人
        /// </summary>
        [SugarColumn(ColumnName = "mail_to", ColumnDescription = "收件人", Length = 255, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar")]
        public string MailTo { get; set; } = string.Empty;

        /// <summary>
        /// 主题
        /// </summary>
        [SugarColumn(ColumnName = "mail_subject", ColumnDescription = "主题", Length = 255, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar")]
        public string MailSubject { get; set; } = string.Empty;

        /// <summary>
        /// 内容
        /// </summary>
        [SugarColumn(ColumnName = "mail_body", ColumnDescription = "内容", IsNullable = false, DefaultValue = "", ColumnDataType = "text")]
        public string MailBody { get; set; } = string.Empty;

        /// <summary>
        /// 是否HTML
        /// </summary>
        [SugarColumn(ColumnName = "mail_is_html", ColumnDescription = "是否HTML", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int MailIsHtml { get; set; }

        /// <summary>
        /// 抄送
        /// </summary>
        [SugarColumn(ColumnName = "mail_cc", ColumnDescription = "抄送", Length = 255, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar")]
        public string? MailCc { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        [SugarColumn(ColumnName = "mail_attachments", ColumnDescription = "附件", Length = 1000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar")]
        public string? MailAttachments { get; set; }

        /// <summary>
        /// 发送状态（0失败 1成功）
        /// </summary>
        [SugarColumn(ColumnName = "mail_status", ColumnDescription = "发送状态（0失败 1成功）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int MailStatus { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        [SugarColumn(ColumnName = "mail_send_time", ColumnDescription = "发送时间", IsNullable = true, ColumnDataType = "datetime")]
        public DateTime? MailSendTime { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [SugarColumn(ColumnName = "mail_error_info", ColumnDescription = "错误信息", Length = 1000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar")]
        public string? MailErrorInfo { get; set; }

        /// <summary>
        /// 收件人ID列表（逗号分隔）
        /// </summary>
        [SugarColumn(ColumnName = "mail_to_ids", ColumnDescription = "收件人ID列表", Length = 1000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar")]
        public string? MailToIds { get; set; }

        /// <summary>
        /// 抄送人ID列表（逗号分隔）
        /// </summary>
        [SugarColumn(ColumnName = "mail_cc_ids", ColumnDescription = "抄送人ID列表", Length = 1000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar")]
        public string? MailCcIds { get; set; }

        /// <summary>
        /// 已读用户ID列表（逗号分隔）
        /// </summary>
        [SugarColumn(ColumnName = "mail_read_ids", ColumnDescription = "已读用户ID列表", Length = 1000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar")]
        public string? MailReadIds { get; set; }

        /// <summary>
        /// 已读数量
        /// </summary>
        [SugarColumn(ColumnName = "mail_read_count", ColumnDescription = "已读数量", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int MailReadCount { get; set; }

        /// <summary>
        /// 最后阅读时间
        /// </summary>
        [SugarColumn(ColumnName = "mail_last_read_time", ColumnDescription = "最后阅读时间", IsNullable = true, ColumnDataType = "datetime")]
        public DateTime? MailLastReadTime { get; set; }


    }
}