//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbDiffLogDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 数据库差异日志数据传输对象
//===================================================================

using System;
using System.ComponentModel.DataAnnotations;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Dtos.Audit
{
    /// <summary>
    /// 数据库差异日志基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtDbDiffLogDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDbDiffLogDto()
        {
            TableName = string.Empty;
            ChangeType = string.Empty;
            ColumnName = string.Empty;
            OldDataType = string.Empty;
            NewDataType = string.Empty;
            ChangeDescription = string.Empty;
            ExecuteSql = string.Empty;
            SqlParameters = string.Empty;
            BeforeData = string.Empty;
            AfterData = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 变更类型（新建表、新增列、修改列、删除列）
        /// </summary>
        public string ChangeType { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 原数据类型
        /// </summary>
        public string OldDataType { get; set; }

        /// <summary>
        /// 新数据类型
        /// </summary>
        public string NewDataType { get; set; }

        /// <summary>
        /// 原长度
        /// </summary>
        public int? OldLength { get; set; }

        /// <summary>
        /// 新长度
        /// </summary>
        public int? NewLength { get; set; }

        /// <summary>
        /// 原是否允许为空
        /// </summary>
        public bool? OldIsNullable { get; set; }

        /// <summary>
        /// 新是否允许为空
        /// </summary>
        public bool? NewIsNullable { get; set; }

        /// <summary>
        /// 变更描述
        /// </summary>
        public string ChangeDescription { get; set; }

        /// <summary>
        /// 执行的SQL语句
        /// </summary>
        public string ExecuteSql { get; set; }

        /// <summary>
        /// SQL参数（JSON格式）
        /// </summary>
        public string SqlParameters { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public long? TenantId { get; set; }

        /// <summary>
        /// 变更前的数据（JSON格式）
        /// </summary>
        public string BeforeData { get; set; }

        /// <summary>
        /// 变更后的数据（JSON格式）
        /// </summary>
        public string AfterData { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 数据库差异日志查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtDbDiffLogQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDbDiffLogQueryDto()
        {
            TableName = string.Empty;
            ChangeType = string.Empty;
            ColumnName = string.Empty;
        }

        /// <summary>
        /// 表名
        /// </summary>
        [MaxLength(100, ErrorMessage = "表名长度不能超过100个字符")]
        public string TableName { get; set; }

        /// <summary>
        /// 变更类型
        /// </summary>
        [MaxLength(20, ErrorMessage = "变更类型长度不能超过20个字符")]
        public string ChangeType { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        [MaxLength(100, ErrorMessage = "列名长度不能超过100个字符")]
        public string ColumnName { get; set; }

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
    /// 数据库差异日志导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtDbDiffLogExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDbDiffLogExportDto()
        {
            TableName = string.Empty;
            ChangeType = string.Empty;
            ColumnName = string.Empty;
            OldDataType = string.Empty;
            NewDataType = string.Empty;
            ChangeDescription = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 变更类型
        /// </summary>
        public string ChangeType { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 原数据类型
        /// </summary>
        public string OldDataType { get; set; }

        /// <summary>
        /// 新数据类型
        /// </summary>
        public string NewDataType { get; set; }

        /// <summary>
        /// 原长度
        /// </summary>
        public int? OldLength { get; set; }

        /// <summary>
        /// 新长度
        /// </summary>
        public int? NewLength { get; set; }

        /// <summary>
        /// 原是否允许为空
        /// </summary>
        public bool? OldIsNullable { get; set; }

        /// <summary>
        /// 新是否允许为空
        /// </summary>
        public bool? NewIsNullable { get; set; }

        /// <summary>
        /// 变更描述
        /// </summary>
        public string ChangeDescription { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
} 