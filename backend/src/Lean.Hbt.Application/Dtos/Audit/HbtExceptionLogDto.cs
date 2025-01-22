//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtExceptionLogDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 异常日志数据传输对象
//===================================================================

using System;
using System.ComponentModel.DataAnnotations;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Dtos.Audit
{
    /// <summary>
    /// 异常日志基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtExceptionLogDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtExceptionLogDto()
        {
            UserName = string.Empty;
            Method = string.Empty;
            Parameters = string.Empty;
            ExceptionType = string.Empty;
            ExceptionMessage = string.Empty;
            StackTrace = string.Empty;
            IpAddress = string.Empty;
            UserAgent = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public long? TenantId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// 异常类型
        /// </summary>
        public string ExceptionType { get; set; }

        /// <summary>
        /// 异常消息
        /// </summary>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// 堆栈跟踪
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 异常日志查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtExceptionLogQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtExceptionLogQueryDto()
        {
            UserName = string.Empty;
            Method = string.Empty;
            ExceptionType = string.Empty;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(50, ErrorMessage = "用户名长度不能超过50个字符")]
        public string UserName { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        [MaxLength(100, ErrorMessage = "方法长度不能超过100个字符")]
        public string Method { get; set; }

        /// <summary>
        /// 异常类型
        /// </summary>
        [MaxLength(200, ErrorMessage = "异常类型长度不能超过200个字符")]
        public string ExceptionType { get; set; }

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
    /// 异常日志导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtExceptionLogExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtExceptionLogExportDto()
        {
            UserName = string.Empty;
            Method = string.Empty;
            ExceptionType = string.Empty;
            ExceptionMessage = string.Empty;
            IpAddress = string.Empty;
            UserAgent = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 异常类型
        /// </summary>
        public string ExceptionType { get; set; }

        /// <summary>
        /// 异常消息
        /// </summary>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
} 