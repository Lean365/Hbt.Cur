#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSqlDiffLogDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : SqlSugar差异日志数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.Audit
{
    /// <summary>
    /// SqlSugar差异日志基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtSqlDiffLogDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSqlDiffLogDto()
        {
            DiffType = string.Empty;
            TableName = string.Empty;
            BusinessName = string.Empty;
            PrimaryKey = string.Empty;
            BeforeData = string.Empty;
            AfterData = string.Empty;
            DiffFields = string.Empty;
            ExecuteSql = string.Empty;
            SqlParameters = string.Empty;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long SqlDiffLogId { get; set; }

        /// <summary>
        /// 差异类型（Insert、Update、Delete）
        /// </summary>
        public string DiffType { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 业务名称
        /// </summary>
        public string? BusinessName { get; set; }

        /// <summary>
        /// 主键值
        /// </summary>
        public string? PrimaryKey { get; set; }

        /// <summary>
        /// 变更前的数据（JSON格式）
        /// </summary>
        public string? BeforeData { get; set; }

        /// <summary>
        /// 变更后的数据（JSON格式）
        /// </summary>
        public string? AfterData { get; set; }

        /// <summary>
        /// 差异字段（JSON格式）
        /// </summary>
        public string? DiffFields { get; set; }

        /// <summary>
        /// 执行的SQL语句
        /// </summary>
        public string? ExecuteSql { get; set; }

        /// <summary>
        /// SQL参数（JSON格式）
        /// </summary>
        public string? SqlParameters { get; set; }

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
    /// SqlSugar差异日志查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtSqlDiffLogQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSqlDiffLogQueryDto()
        {
            DiffType = string.Empty;
            TableName = string.Empty;
            BusinessName = string.Empty;
        }

        /// <summary>
        /// 差异类型
        /// </summary>
        [MaxLength(50, ErrorMessage = "差异类型长度不能超过50个字符")]
        public string DiffType { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        [MaxLength(100, ErrorMessage = "表名长度不能超过100个字符")]
        public string TableName { get; set; }

        /// <summary>
        /// 业务名称
        /// </summary>
        [MaxLength(100, ErrorMessage = "业务名称长度不能超过100个字符")]
        public string BusinessName { get; set; }

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
    /// SqlSugar差异日志导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtSqlDiffLogExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSqlDiffLogExportDto()
        {
            DiffType = string.Empty;
            TableName = string.Empty;
            BusinessName = string.Empty;
            PrimaryKey = string.Empty;
            BeforeData = string.Empty;
            AfterData = string.Empty;
            DiffFields = string.Empty;
            ExecuteSql = string.Empty;
            SqlParameters = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 差异类型
        /// </summary>
        public string DiffType { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 业务名称
        /// </summary>
        public string? BusinessName { get; set; }

        /// <summary>
        /// 主键值
        /// </summary>
        public string? PrimaryKey { get; set; }

        /// <summary>
        /// 变更前的数据
        /// </summary>
        public string? BeforeData { get; set; }

        /// <summary>
        /// 变更后的数据
        /// </summary>
        public string? AfterData { get; set; }

        /// <summary>
        /// 差异字段
        /// </summary>
        public string? DiffFields { get; set; }

        /// <summary>
        /// 执行的SQL语句
        /// </summary>
        public string? ExecuteSql { get; set; }

        /// <summary>
        /// SQL参数
        /// </summary>
        public string? SqlParameters { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}