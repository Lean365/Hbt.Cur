//===================================================================
// 项目名 : Lean.Hbt.Application
// 文件名 : HbtQuartzLogDto.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 任务日志数据传输对象
//===================================================================

using System;
using System.ComponentModel.DataAnnotations;
using Lean.Hbt.Common.Models;
using Mapster;

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
            LogTaskName = string.Empty;
            LogGroupName = string.Empty;
            LogExecuteParams = string.Empty;
            LogErrorInfo = string.Empty;
            LogExecuteIp = string.Empty;
            LogExecuteHost = string.Empty;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long LogId { get; set; }

        /// <summary>
        /// 任务ID
        /// </summary>
        public long LogTaskId { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string LogTaskName { get; set; }

        /// <summary>
        /// 任务组名
        /// </summary>
        public string LogGroupName { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime LogExecuteTime { get; set; }

        /// <summary>
        /// 执行耗时（毫秒）
        /// </summary>
        public long LogExecuteDuration { get; set; }

        /// <summary>
        /// 执行参数
        /// </summary>
        public string LogExecuteParams { get; set; }

        /// <summary>
        /// 执行状态（0失败 1成功）
        /// </summary>
        public int LogStatus { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string LogErrorInfo { get; set; }

        /// <summary>
        /// 执行机器IP
        /// </summary>
        public string LogExecuteIp { get; set; }

        /// <summary>
        /// 执行机器名
        /// </summary>
        public string LogExecuteHost { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
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
            LogTaskName = string.Empty;
            LogGroupName = string.Empty;
        }

        /// <summary>
        /// 任务名称
        /// </summary>
        [MaxLength(100, ErrorMessage = "任务名称长度不能超过100个字符")]
        public string LogTaskName { get; set; }

        /// <summary>
        /// 任务组名
        /// </summary>
        [MaxLength(50, ErrorMessage = "任务组名长度不能超过50个字符")]
        public string LogGroupName { get; set; }

        /// <summary>
        /// 执行状态（0失败 1成功）
        /// </summary>
        public int? LogStatus { get; set; }

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
            LogTaskName = string.Empty;
            LogGroupName = string.Empty;
            LogExecuteParams = string.Empty;
            LogErrorInfo = string.Empty;
            LogExecuteIp = string.Empty;
            LogExecuteHost = string.Empty;
        }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string LogTaskName { get; set; }

        /// <summary>
        /// 任务组名
        /// </summary>
        public string LogGroupName { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime LogExecuteTime { get; set; }

        /// <summary>
        /// 执行耗时（毫秒）
        /// </summary>
        public long LogExecuteDuration { get; set; }

        /// <summary>
        /// 执行参数
        /// </summary>
        public string LogExecuteParams { get; set; }

        /// <summary>
        /// 执行状态（0失败 1成功）
        /// </summary>
        public string LogStatus { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string LogErrorInfo { get; set; }

        /// <summary>
        /// 执行机器IP
        /// </summary>
        public string LogExecuteIp { get; set; }

        /// <summary>
        /// 执行机器名
        /// </summary>
        public string LogExecuteHost { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
} 