//===================================================================
// 项目名 : Lean.Hbt.Application
// 文件名 : HbtQuartzLogDto.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 任务日志数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.Audit
{
    /// <summary>
    /// 任务日志基础DTO
    /// </summary>
    public class HbtQuartzLogDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtQuartzLogDto()
        {
            QuartzName = string.Empty;
            QuartzGroupName = string.Empty;
            QuartzExecuteParams = string.Empty;
            QuartzExecuteMessage = string.Empty;
            QuartzErrorInfo = string.Empty;
            QuartzExecuteIp = string.Empty;
            QuartzExecuteHost = string.Empty;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long QuartzLogId { get; set; }

        /// <summary>
        /// 任务ID
        /// </summary>
        public long QuartzId { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string QuartzName { get; set; }

        /// <summary>
        /// 任务组名
        /// </summary>
        public string QuartzGroupName { get; set; }

        /// <summary>
        /// 任务类型（1.程序集 2.网络请求 3.SQL语句）
        /// </summary>
        public int QuartzType { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime QuartzExecuteTime { get; set; }

        /// <summary>
        /// 执行耗时（毫秒）
        /// </summary>
        public long QuartzExecuteDuration { get; set; }

        /// <summary>
        /// 执行参数
        /// </summary>
        public string QuartzExecuteParams { get; set; }

        /// <summary>
        /// 执行状态（0失败 1成功）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 执行消息
        /// </summary>
        public string QuartzExecuteMessage { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string QuartzErrorInfo { get; set; }

        /// <summary>
        /// 执行机器IP
        /// </summary>
        public string QuartzExecuteIp { get; set; }

        /// <summary>
        /// 执行机器名
        /// </summary>
        public string QuartzExecuteHost { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

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
        /// 是否删除（0未删除 1已删除）
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
    /// 任务日志查询DTO
    /// </summary>
    public class HbtQuartzLogQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtQuartzLogQueryDto()
        {
            QuartzName = string.Empty;
            QuartzGroupName = string.Empty;
        }

        /// <summary>
        /// 任务名称
        /// </summary>
        [MaxLength(100, ErrorMessage = "任务名称长度不能超过100个字符")]
        public string QuartzName { get; set; }

        /// <summary>
        /// 任务组名
        /// </summary>
        [MaxLength(50, ErrorMessage = "任务组名长度不能超过50个字符")]
        public string QuartzGroupName { get; set; }

        /// <summary>
        /// 执行状态（0失败 1成功）
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// 任务日志导出DTO
    /// </summary>
    public class HbtQuartzLogExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtQuartzLogExportDto()
        {
            QuartzName = string.Empty;
            QuartzGroupName = string.Empty;
            QuartzExecuteParams = string.Empty;
            QuartzExecuteMessage = string.Empty;
            QuartzErrorInfo = string.Empty;
            QuartzExecuteIp = string.Empty;
            QuartzExecuteHost = string.Empty;
        }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string QuartzName { get; set; }

        /// <summary>
        /// 任务组名
        /// </summary>
        public string QuartzGroupName { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime QuartzExecuteTime { get; set; }

        /// <summary>
        /// 执行耗时（毫秒）
        /// </summary>
        public long QuartzExecuteDuration { get; set; }

        /// <summary>
        /// 执行参数
        /// </summary>
        public string QuartzExecuteParams { get; set; }

        /// <summary>
        /// 执行状态（0失败 1成功）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 执行消息
        /// </summary>
        public string QuartzExecuteMessage { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string QuartzErrorInfo { get; set; }

        /// <summary>
        /// 执行机器IP
        /// </summary>
        public string QuartzExecuteIp { get; set; }

        /// <summary>
        /// 执行机器名
        /// </summary>
        public string QuartzExecuteHost { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}