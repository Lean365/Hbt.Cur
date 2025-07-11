//===================================================================
// 项目名 : Lean.Hbt.Application
// 文件名 : HbtMeetingDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-26 14:30
// 版本号 : V1.0.0
// 描述   : 会议数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.Routine.Metting
{
    /// <summary>
    /// 会议基础DTO
    /// </summary>
    public class HbtMeetingDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMeetingDto()
        {
            Title = string.Empty;
            Content = string.Empty;
            Location = string.Empty;
            Participants = string.Empty;
            Host = string.Empty;
            Minutes = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long MeetingId { get; set; }

        /// <summary>
        /// 会议标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 会议内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 会议类型
        /// </summary>
        public int MeetingType { get; set; }

        /// <summary>
        /// 会议状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否全天
        /// </summary>
        public int IsAllDay { get; set; }

        /// <summary>
        /// 提醒时间（分钟）
        /// </summary>
        public int? RemindMinutes { get; set; }

        /// <summary>
        /// 地点
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// 参与人
        /// </summary>
        public string? Participants { get; set; }

        /// <summary>
        /// 主持人
        /// </summary>
        public string? Host { get; set; }

        /// <summary>
        /// 会议纪要
        /// </summary>
        public string? Minutes { get; set; }

        /// <summary>
        /// 是否需要签到
        /// </summary>
        public int NeedSignIn { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string? DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
    }

    /// <summary>
    /// 会议查询DTO
    /// </summary>
    public class HbtMeetingQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMeetingQueryDto()
        {
            Title = string.Empty;
            Location = string.Empty;
            Host = string.Empty;
        }

        /// <summary>
        /// 会议标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 会议类型
        /// </summary>
        public int? MeetingType { get; set; }

        /// <summary>
        /// 会议状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 地点
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// 主持人
        /// </summary>
        public string? Host { get; set; }

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
    /// 会议创建DTO
    /// </summary>
    public class HbtMeetingCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMeetingCreateDto()
        {
            Title = string.Empty;
            Content = string.Empty;
            Location = string.Empty;
            Participants = string.Empty;
            Host = string.Empty;
            Minutes = string.Empty;
        }

        /// <summary>
        /// 会议标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 会议内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 会议类型
        /// </summary>
        public int MeetingType { get; set; }

        /// <summary>
        /// 会议状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否全天
        /// </summary>
        public int IsAllDay { get; set; }

        /// <summary>
        /// 提醒时间（分钟）
        /// </summary>
        public int? RemindMinutes { get; set; }

        /// <summary>
        /// 地点
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// 参与人
        /// </summary>
        public string? Participants { get; set; }

        /// <summary>
        /// 主持人
        /// </summary>
        public string? Host { get; set; }

        /// <summary>
        /// 会议纪要
        /// </summary>
        public string? Minutes { get; set; }

        /// <summary>
        /// 是否需要签到
        /// </summary>
        public int NeedSignIn { get; set; }
    }

    /// <summary>
    /// 会议更新DTO
    /// </summary>
    public class HbtMeetingUpdateDto : HbtMeetingCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public long MeetingId { get; set; }
    }

    /// <summary>
    /// 会议删除DTO
    /// </summary>
    public class HbtMeetingDeleteDto
    {
        /// <summary>主键ID</summary>
        [AdaptMember("Id")]
        public long MeetingId { get; set; }
    }

    /// <summary>
    /// 会议批量删除DTO
    /// </summary>
    public class HbtMeetingBatchDeleteDto
    {
        /// <summary>主键ID列表</summary>
        public List<long> MeetingIds { get; set; } = new List<long>();
    }

    /// <summary>
    /// 会议导入DTO
    /// </summary>
    public class HbtMeetingImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMeetingImportDto()
        {
            Title = string.Empty;
            Content = string.Empty;
            Location = string.Empty;
            Participants = string.Empty;
            Host = string.Empty;
            Minutes = string.Empty;
        }

        /// <summary>
        /// 会议标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 会议内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 会议类型
        /// </summary>
        public int MeetingType { get; set; }

        /// <summary>
        /// 会议状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否全天
        /// </summary>
        public int IsAllDay { get; set; }

        /// <summary>
        /// 提醒时间（分钟）
        /// </summary>
        public int? RemindMinutes { get; set; }

        /// <summary>
        /// 地点
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// 参与人
        /// </summary>
        public string? Participants { get; set; }

        /// <summary>
        /// 主持人
        /// </summary>
        public string? Host { get; set; }

        /// <summary>
        /// 会议纪要
        /// </summary>
        public string? Minutes { get; set; }

        /// <summary>
        /// 是否需要签到
        /// </summary>
        public int NeedSignIn { get; set; }
    }

    /// <summary>
    /// 会议导出DTO
    /// </summary>
    public class HbtMeetingExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMeetingExportDto()
        {
            Title = string.Empty;
            Content = string.Empty;
            Location = string.Empty;
            Participants = string.Empty;
            Host = string.Empty;
            Minutes = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 会议标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 会议内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 会议类型
        /// </summary>
        public int MeetingType { get; set; }

        /// <summary>
        /// 会议状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否全天
        /// </summary>
        public int IsAllDay { get; set; }

        /// <summary>
        /// 提醒时间（分钟）
        /// </summary>
        public int? RemindMinutes { get; set; }

        /// <summary>
        /// 地点
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// 参与人
        /// </summary>
        public string? Participants { get; set; }

        /// <summary>
        /// 主持人
        /// </summary>
        public string? Host { get; set; }

        /// <summary>
        /// 会议纪要
        /// </summary>
        public string? Minutes { get; set; }

        /// <summary>
        /// 是否需要签到
        /// </summary>
        public int NeedSignIn { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 会议导入模板DTO
    /// </summary>
    public class HbtMeetingTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMeetingTemplateDto()
        {
            Title = string.Empty;
            Content = string.Empty;
            Location = string.Empty;
            Participants = string.Empty;
            Host = string.Empty;
            Minutes = string.Empty;
        }

        /// <summary>
        /// 会议标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 会议内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 会议类型(0=普通会议 1=重要会议 2=紧急会议)
        /// </summary>
        public int MeetingType { get; set; }

        /// <summary>
        /// 会议状态(0=未开始 1=进行中 2=已完成 3=已取消)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否全天(0=否 1=是)
        /// </summary>
        public int IsAllDay { get; set; }

        /// <summary>
        /// 提醒时间（分钟）
        /// </summary>
        public int? RemindMinutes { get; set; }

        /// <summary>
        /// 地点
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// 参与人
        /// </summary>
        public string? Participants { get; set; }

        /// <summary>
        /// 主持人
        /// </summary>
        public string? Host { get; set; }

        /// <summary>
        /// 会议纪要
        /// </summary>
        public string? Minutes { get; set; }

        /// <summary>
        /// 是否需要签到(0=否 1=是)
        /// </summary>
        public int NeedSignIn { get; set; }
    }
} 