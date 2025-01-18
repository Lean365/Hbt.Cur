//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtExceptionLogEntity.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 17:00
// 版本号 : V.0.0.1
// 描述    : 异常日志实体
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Audit
{
    /// <summary>
    /// 异常日志实体
    /// </summary>
    [SugarTable("hbt_exception_log", "异常日志表")]
    public class HbtExceptionLogEntity : HbtBaseEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string UserName { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        [SugarColumn(ColumnName = "method", ColumnDescription = "方法", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Method { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        [SugarColumn(ColumnName = "parameters", ColumnDescription = "参数", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string Parameters { get; set; }

        /// <summary>
        /// 异常类型
        /// </summary>
        [SugarColumn(ColumnName = "exception_type", ColumnDescription = "异常类型", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ExceptionType { get; set; }

        /// <summary>
        /// 异常消息
        /// </summary>
        [SugarColumn(ColumnName = "exception_message", ColumnDescription = "异常消息", Length = -1, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// 堆栈跟踪
        /// </summary>
        [SugarColumn(ColumnName = "stack_trace", ColumnDescription = "堆栈跟踪", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string StackTrace { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [SugarColumn(ColumnName = "ip_address", ColumnDescription = "IP地址", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string IpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        [SugarColumn(ColumnName = "user_agent", ColumnDescription = "用户代理", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
        public string UserAgent { get; set; }
    }
} 