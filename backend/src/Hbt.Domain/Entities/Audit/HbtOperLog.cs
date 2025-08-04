#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOperLog.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 15:30
// 版本号 : V.0.0.1
// 描述    : 操作日志实体
//===================================================================

namespace Hbt.Domain.Entities.Audit
{
    /// <summary>
    /// 操作日志实体
    /// </summary>
    [SugarTable("hbt_audit_oper_log", "操作日志")]
    public class HbtOperLog : HbtBaseEntity
    {


        /// <summary>
        /// 操作模块
        /// </summary>
        [SugarColumn(ColumnName = "oper_module", ColumnDescription = "操作模块", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string OperModule { get; set; } = string.Empty;

        /// <summary>
        /// 操作类型（新增、修改、删除）
        /// </summary>
        [SugarColumn(ColumnName = "oper_type", ColumnDescription = "操作类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string OperType { get; set; } = string.Empty;

        /// <summary>
        /// 表名
        /// </summary>
        [SugarColumn(ColumnName = "oper_table_name", ColumnDescription = "表名", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string OperTableName { get; set; } = string.Empty;

        /// <summary>
        /// 业务主键
        /// </summary>
        [SugarColumn(ColumnName = "oper_business_key", ColumnDescription = "业务主键", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string OperBusinessKey { get; set; } = string.Empty;

        /// <summary>
        /// 请求方法
        /// </summary>
        [SugarColumn(ColumnName = "oper_request_method", ColumnDescription = "请求方法", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string OperRequestMethod { get; set; } = string.Empty;

        /// <summary>
        /// 请求参数
        /// </summary>
        [SugarColumn(ColumnName = "oper_request_param", ColumnDescription = "请求参数", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OperRequestParam { get; set; }

        /// <summary>
        /// 返回参数
        /// </summary>
        [SugarColumn(ColumnName = "oper_response_param", ColumnDescription = "返回参数", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OperResponseParam { get; set; }

        /// <summary>
        /// 操作用时（毫秒）
        /// </summary>
        [SugarColumn(ColumnName = "oper_duration", ColumnDescription = "操作用时（毫秒）", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
        public long OperDuration { get; set; } = 0;

        /// <summary>
        /// 错误消息
        /// </summary>
        [SugarColumn(ColumnName = "oper_error_msg", ColumnDescription = "错误消息", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OperErrorMsg { get; set; }
                /// <summary>
        /// IP地址
        /// </summary>
        [SugarColumn(ColumnName = "oper_ip_address", ColumnDescription = "IP地址", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string OperIpAddress { get; set; } = string.Empty;

        /// <summary>
        /// 操作地点
        /// </summary>
        [SugarColumn(ColumnName = "oper_location", ColumnDescription = "操作地点", Length = 255, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OperLocation { get; set; }
        
        /// <summary>
        /// 操作状态（0正常 1异常）
        /// </summary>
        [SugarColumn(ColumnName = "oper_status", ColumnDescription = "操作状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OperStatus { get; set; } = 0;

    }
}