//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtMail.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 邮件实体
//===================================================================

using System;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Routine
{
    /// <summary>
    /// 邮件实体
    /// </summary>
    [SugarTable("hbt_rou_mail", "邮件")]
    [SugarIndex("index_mail_to_email", nameof(MailToEmail), OrderByType.Asc)]
    [SugarIndex("index_mail_status", nameof(MailStatus), OrderByType.Asc)]
    [SugarIndex("index_mail_send_time", nameof(MailSendTime), OrderByType.Desc)]
    public class HbtMail : HbtBaseEntity
    {
        /// <summary>
        /// 收件人
        /// </summary>
        [SugarColumn(ColumnName = "mail_to_email", ColumnDescription = "收件人", Length = 255, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar")]
        public string MailToEmail { get; set; } = string.Empty;

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
        [SugarColumn(ColumnName = "mail_is_html", ColumnDescription = "是否HTML", IsNullable = false, DefaultValue = "0", ColumnDataType = "bit")]
        public bool MailIsHtml { get; set; }

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
    }
} 