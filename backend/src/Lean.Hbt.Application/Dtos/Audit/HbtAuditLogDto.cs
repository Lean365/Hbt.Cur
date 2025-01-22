//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtAuditLogDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 审计日志数据传输对象
//===================================================================

using System;
using System.ComponentModel.DataAnnotations;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Dtos.Audit
{
    /// <summary>
    /// 审计日志基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtAuditLogDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtAuditLogDto()
        {
            UserName = string.Empty;
            Module = string.Empty;
            Operation = string.Empty;
            Method = string.Empty;
            Parameters = string.Empty;
            Result = string.Empty;
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
        /// 模块
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 耗时(毫秒)
        /// </summary>
        public long Elapsed { get; set; }

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
    /// 审计日志查询对象
    /// </summary>
    public class HbtAuditLogQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        public string? Module { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string? Operation { get; set; }

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
    /// 审计日志导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtAuditLogExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtAuditLogExportDto()
        {
            UserName = string.Empty;
            Module = string.Empty;
            Operation = string.Empty;
            Method = string.Empty;
            Parameters = string.Empty;
            Result = string.Empty;
            IpAddress = string.Empty;
            UserAgent = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 耗时(毫秒)
        /// </summary>
        public long Elapsed { get; set; }

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