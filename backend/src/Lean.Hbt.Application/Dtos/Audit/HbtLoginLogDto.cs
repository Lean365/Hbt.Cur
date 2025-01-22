//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginLogDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 登录日志数据传输对象
//===================================================================

using System;
using System.ComponentModel.DataAnnotations;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Dtos.Audit
{
    /// <summary>
    /// 登录日志基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtLoginLogDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtLoginLogDto()
        {
            UserName = string.Empty;
            IpAddress = string.Empty;
            UserAgent = string.Empty;
            Message = string.Empty;
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
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 登录日志查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtLoginLogQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtLoginLogQueryDto()
        {
            UserName = string.Empty;
            IpAddress = string.Empty;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(50, ErrorMessage = "用户名长度不能超过50个字符")]
        public string UserName { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [MaxLength(50, ErrorMessage = "IP地址长度不能超过50个字符")]
        public string IpAddress { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool? Success { get; set; }

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
    /// 登录日志导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtLoginLogExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtLoginLogExportDto()
        {
            UserName = string.Empty;
            IpAddress = string.Empty;
            UserAgent = string.Empty;
            Message = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
} 