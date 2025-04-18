//===================================================================
// 项目名 : Lean.Hbt.Application
// 文件名 : HbtNoticeDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-26 14:30
// 版本号 : V1.0.0
// 描述   : 通知数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.Routine
{
    /// <summary>
    /// 通知基础DTO
    /// </summary>
    public class HbtNoticeDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtNoticeDto()
        {
            NoticeTitle = string.Empty;
            NoticeContent = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long NoticeId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string NoticeTitle { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string NoticeContent { get; set; }

        /// <summary>
        /// 类型（1通知 2公告）
        /// </summary>
        public int NoticeType { get; set; }

        /// <summary>
        /// 状态（0草稿 1发布 2关闭）
        /// </summary>
        public int NoticeStatus { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? NoticePublishTime { get; set; }

        /// <summary>
        /// 截止时间
        /// </summary>
        public DateTime? NoticeDeadline { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string? NoticeAttachments { get; set; }

        /// <summary>
        /// 访问链接
        /// </summary>
        public string? NoticeAccessUrl { get; set; }

        /// <summary>
        /// 已读数量
        /// </summary>
        public int NoticeReadCount { get; set; }

        /// <summary>
        /// 已读用户ID列表
        /// </summary>
        public string? NoticeReadIds { get; set; }

        /// <summary>
        /// 确认数量
        /// </summary>
        public int NoticeConfirmCount { get; set; }

        /// <summary>
        /// 确认用户ID列表
        /// </summary>
        public string? NoticeConfirmIds { get; set; }

        /// <summary>
        /// 最后回执时间
        /// </summary>
        public DateTime? NoticeLastReceiptTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 通知查询DTO
    /// </summary>
    public class HbtNoticeQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtNoticeQueryDto()
        {
            NoticeTitle = string.Empty;
        }

        /// <summary>
        /// 标题
        /// </summary>
        [MaxLength(255, ErrorMessage = "标题长度不能超过255个字符")]
        public string? NoticeTitle { get; set; }

        /// <summary>
        /// 类型（1通知 2公告）
        /// </summary>
        public int? NoticeType { get; set; }

        /// <summary>
        /// 状态（0草稿 1发布 2关闭）
        /// </summary>
        public int? NoticeStatus { get; set; }

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
    /// 通知创建DTO
    /// </summary>
    public class HbtNoticeCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtNoticeCreateDto()
        {
            NoticeTitle = string.Empty;
            NoticeContent = string.Empty;
        }

        /// <summary>
        /// 标题
        /// </summary>
        [Required(ErrorMessage = "标题不能为空")]
        [MaxLength(255, ErrorMessage = "标题长度不能超过255个字符")]
        public string NoticeTitle { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required(ErrorMessage = "内容不能为空")]
        public string NoticeContent { get; set; }

        /// <summary>
        /// 类型（1通知 2公告）
        /// </summary>
        [Required(ErrorMessage = "类型不能为空")]
        public int NoticeType { get; set; }

        /// <summary>
        /// 状态（0草稿 1发布 2关闭）
        /// </summary>
        public int NoticeStatus { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? NoticePublishTime { get; set; }

        /// <summary>
        /// 截止时间
        /// </summary>
        public DateTime? NoticeDeadline { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        [MaxLength(1000, ErrorMessage = "附件长度不能超过1000个字符")]
        public string? NoticeAttachments { get; set; }

        /// <summary>
        /// 访问链接
        /// </summary>
        [MaxLength(255, ErrorMessage = "访问链接长度不能超过255个字符")]
        public string? NoticeAccessUrl { get; set; }
    }

    /// <summary>
    /// 通知更新DTO
    /// </summary>
    public class HbtNoticeUpdateDto : HbtNoticeCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "通知ID不能为空")]
        public long NoticeId { get; set; }
    }

    /// <summary>
    /// 通知导入DTO
    /// </summary>
    public class HbtNoticeImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtNoticeImportDto()
        {
            NoticeTitle = string.Empty;
            NoticeContent = string.Empty;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string NoticeTitle { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string NoticeContent { get; set; }

        /// <summary>
        /// 类型（1通知 2公告）
        /// </summary>
        public int NoticeType { get; set; }

        /// <summary>
        /// 状态（0草稿 1发布 2关闭）
        /// </summary>
        public int NoticeStatus { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public string? NoticePublishTime { get; set; }

        /// <summary>
        /// 截止时间
        /// </summary>
        public string? NoticeDeadline { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string? NoticeAttachments { get; set; }

        /// <summary>
        /// 访问链接
        /// </summary>
        public string? NoticeAccessUrl { get; set; }
    }

    /// <summary>
    /// 通知导出DTO
    /// </summary>
    public class HbtNoticeExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtNoticeExportDto()
        {
            NoticeTitle = string.Empty;
            NoticeContent = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string NoticeTitle { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string NoticeContent { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int NoticeType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int NoticeStatus { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? NoticePublishTime { get; set; }

        /// <summary>
        /// 截止时间
        /// </summary>
        public DateTime? NoticeDeadline { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string? NoticeAttachments { get; set; }

        /// <summary>
        /// 访问链接
        /// </summary>
        public string? NoticeAccessUrl { get; set; }

        /// <summary>
        /// 已读数量
        /// </summary>
        public int NoticeReadCount { get; set; }

        /// <summary>
        /// 确认数量
        /// </summary>
        public int NoticeConfirmCount { get; set; }

        /// <summary>
        /// 最后回执时间
        /// </summary>
        public DateTime? NoticeLastReceiptTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 通知导入模板DTO
    /// </summary>
    public class HbtNoticeTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtNoticeTemplateDto()
        {
            NoticeTitle = string.Empty;
            NoticeContent = string.Empty;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string NoticeTitle { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string NoticeContent { get; set; }

        /// <summary>
        /// 类型(1=通知,2=公告)
        /// </summary>
        public int NoticeType { get; set; }

        /// <summary>
        /// 状态(0=草稿,1=发布,2=关闭)
        /// </summary>
        public int NoticeStatus { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public string? NoticePublishTime { get; set; }

        /// <summary>
        /// 截止时间
        /// </summary>
        public string? NoticeDeadline { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string? NoticeAttachments { get; set; }

        /// <summary>
        /// 访问链接
        /// </summary>
        public string? NoticeAccessUrl { get; set; }
    }
}