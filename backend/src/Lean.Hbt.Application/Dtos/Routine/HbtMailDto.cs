//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtMailDto.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 邮件数据传输对象
//===================================================================

using System;
using System.ComponentModel.DataAnnotations;
using Lean.Hbt.Common.Models;
using Mapster;

namespace Lean.Hbt.Application.Dtos.Routine
{
    /// <summary>
    /// 邮件基础DTO
    /// </summary>
    public class HbtMailDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMailDto()
        {
            MailToEmail = string.Empty;
            MailSubject = string.Empty;
            MailBody = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long MailId { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public string MailToEmail { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string MailSubject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string MailBody { get; set; }

        /// <summary>
        /// 是否HTML
        /// </summary>
        public bool MailIsHtml { get; set; }

        /// <summary>
        /// 抄送
        /// </summary>
        public string? MailCc { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string? MailAttachments { get; set; }

        /// <summary>
        /// 发送状态（0失败 1成功）
        /// </summary>
        public int MailStatus { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? MailSendTime { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string? MailErrorInfo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 邮件查询DTO
    /// </summary>
    public class HbtMailQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMailQueryDto()
        {
            MailToEmail = string.Empty;
            MailSubject = string.Empty;
        }

        /// <summary>
        /// 收件人
        /// </summary>
        [MaxLength(255, ErrorMessage = "收件人长度不能超过255个字符")]
        public string? MailToEmail { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        [MaxLength(255, ErrorMessage = "主题长度不能超过255个字符")]
        public string? MailSubject { get; set; }

        /// <summary>
        /// 发送状态（0失败 1成功）
        /// </summary>
        public int? MailStatus { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// 邮件创建DTO
    /// </summary>
    public class HbtMailCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMailCreateDto()
        {
            MailToEmail = string.Empty;
            MailSubject = string.Empty;
            MailBody = string.Empty;
        }

        /// <summary>
        /// 收件人
        /// </summary>
        [Required(ErrorMessage = "收件人不能为空")]
        [MaxLength(255, ErrorMessage = "收件人长度不能超过255个字符")]
        [EmailAddress(ErrorMessage = "收件人邮箱格式不正确")]
        public string MailToEmail { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        [Required(ErrorMessage = "主题不能为空")]
        [MaxLength(255, ErrorMessage = "主题长度不能超过255个字符")]
        public string MailSubject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required(ErrorMessage = "内容不能为空")]
        public string MailBody { get; set; }

        /// <summary>
        /// 是否HTML
        /// </summary>
        public bool MailIsHtml { get; set; }

        /// <summary>
        /// 抄送
        /// </summary>
        [MaxLength(255, ErrorMessage = "抄送长度不能超过255个字符")]
        public string? MailCc { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        [MaxLength(1000, ErrorMessage = "附件长度不能超过1000个字符")]
        public string? MailAttachments { get; set; }
    }

    /// <summary>
    /// 邮件更新DTO
    /// </summary>
    public class HbtMailUpdateDto : HbtMailCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "邮件ID不能为空")]
        public long MailId { get; set; }
    }

    /// <summary>
    /// 邮件发送DTO
    /// </summary>
    public class HbtMailSendDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMailSendDto()
        {
            MailToEmail = string.Empty;
            MailSubject = string.Empty;
            MailBody = string.Empty;
        }

        /// <summary>
        /// 收件人
        /// </summary>
        [Required(ErrorMessage = "收件人不能为空")]
        [MaxLength(255, ErrorMessage = "收件人长度不能超过255个字符")]
        [EmailAddress(ErrorMessage = "收件人邮箱格式不正确")]
        public string MailToEmail { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        [Required(ErrorMessage = "主题不能为空")]
        [MaxLength(255, ErrorMessage = "主题长度不能超过255个字符")]
        public string MailSubject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required(ErrorMessage = "内容不能为空")]
        public string MailBody { get; set; }

        /// <summary>
        /// 是否HTML
        /// </summary>
        public bool MailIsHtml { get; set; }

        /// <summary>
        /// 抄送
        /// </summary>
        [MaxLength(255, ErrorMessage = "抄送长度不能超过255个字符")]
        public string? MailCc { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        [MaxLength(1000, ErrorMessage = "附件长度不能超过1000个字符")]
        public string? MailAttachments { get; set; }
    }

    /// <summary>
    /// 邮件导出DTO
    /// </summary>
    public class HbtMailExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMailExportDto()
        {
            MailToEmail = string.Empty;
            MailSubject = string.Empty;
            MailBody = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 收件人
        /// </summary>
        public string MailToEmail { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string MailSubject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string MailBody { get; set; }

        /// <summary>
        /// 是否HTML
        /// </summary>
        public string MailIsHtml { get; set; }

        /// <summary>
        /// 抄送
        /// </summary>
        public string? MailCc { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string? MailAttachments { get; set; }

        /// <summary>
        /// 发送状态
        /// </summary>
        public string MailStatus { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? MailSendTime { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string? MailErrorInfo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
} 