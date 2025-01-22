//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOperLogDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 操作日志数据传输对象
//===================================================================

using System;
using System.ComponentModel.DataAnnotations;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Dtos.Audit
{
    /// <summary>
    /// 操作日志基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtOperLogDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtOperLogDto()
        {
            UserName = string.Empty;
            OperationType = string.Empty;
            TableName = string.Empty;
            BusinessKey = string.Empty;
            RequestMethod = string.Empty;
            RequestParam = string.Empty;
            IpAddress = string.Empty;
            Location = string.Empty;
            ErrorMsg = string.Empty;
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
        /// 操作类型
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 业务主键
        /// </summary>
        public string BusinessKey { get; set; }

        /// <summary>
        /// 请求方法
        /// </summary>
        public string RequestMethod { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public string RequestParam { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 操作地点
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 操作状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 操作日志查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtOperLogQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtOperLogQueryDto()
        {
            UserName = string.Empty;
            OperationType = string.Empty;
            TableName = string.Empty;
            IpAddress = string.Empty;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(50, ErrorMessage = "用户名长度不能超过50个字符")]
        public string UserName { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        [MaxLength(20, ErrorMessage = "操作类型长度不能超过20个字符")]
        public string OperationType { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        [MaxLength(100, ErrorMessage = "表名长度不能超过100个字符")]
        public string TableName { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [MaxLength(50, ErrorMessage = "IP地址长度不能超过50个字符")]
        public string IpAddress { get; set; }

        /// <summary>
        /// 操作状态
        /// </summary>
        public int? Status { get; set; }

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
    /// 操作日志导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtOperLogExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtOperLogExportDto()
        {
            UserName = string.Empty;
            OperationType = string.Empty;
            TableName = string.Empty;
            BusinessKey = string.Empty;
            RequestMethod = string.Empty;
            RequestParam = string.Empty;
            IpAddress = string.Empty;
            Location = string.Empty;
            ErrorMsg = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 业务主键
        /// </summary>
        public string BusinessKey { get; set; }

        /// <summary>
        /// 请求方法
        /// </summary>
        public string RequestMethod { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public string RequestParam { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 操作地点
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 操作状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
} 