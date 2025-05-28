#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOperLog.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 15:30
// 版本号 : V.0.0.1
// 描述    : 操作日志实体
//===================================================================

namespace Lean.Hbt.Domain.Entities.Audit
{
    /// <summary>
    /// 操作日志实体
    /// </summary>
    [SugarTable("hbt_audit_oper_log", "操作日志")]
    public class HbtOperLog : HbtBaseEntity
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        [SugarColumn(ColumnName = "log_level", ColumnDescription = "日志级别", ColumnDataType = "int", IsNullable = false)]
        public int LogLevel { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 操作类型（新增、修改、删除）
        /// </summary>
        [SugarColumn(ColumnName = "operation_type", ColumnDescription = "操作类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string OperationType { get; set; } = string.Empty;

        /// <summary>
        /// 表名
        /// </summary>
        [SugarColumn(ColumnName = "table_name", ColumnDescription = "表名", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string TableName { get; set; } = string.Empty;

        /// <summary>
        /// 业务主键
        /// </summary>
        [SugarColumn(ColumnName = "business_key", ColumnDescription = "业务主键", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string BusinessKey { get; set; } = string.Empty;

        /// <summary>
        /// 请求方法
        /// </summary>
        [SugarColumn(ColumnName = "request_method", ColumnDescription = "请求方法", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string RequestMethod { get; set; } = string.Empty;

        /// <summary>
        /// 请求参数
        /// </summary>
        [SugarColumn(ColumnName = "request_param", ColumnDescription = "请求参数", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RequestParam { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [SugarColumn(ColumnName = "ip_address", ColumnDescription = "IP地址", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string IpAddress { get; set; } = string.Empty;

        /// <summary>
        /// 操作地点
        /// </summary>
        [SugarColumn(ColumnName = "location", ColumnDescription = "操作地点", Length = 255, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Location { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        [SugarColumn(ColumnName = "error_msg", ColumnDescription = "错误消息", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ErrorMsg { get; set; }
        
        /// <summary>
        /// 操作状态（0正常 1异常）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "操作状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;

    }
}