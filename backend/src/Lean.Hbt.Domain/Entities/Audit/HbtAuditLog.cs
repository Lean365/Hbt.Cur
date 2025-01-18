//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtAuditLogEntity.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 17:00
// 版本号 : V.0.0.1
// 描述    : 审计日志实体
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Audit
{
    /// <summary>
    /// 审计日志实体
    /// </summary>
    [SugarTable("hbt_audit_log", "审计日志表")]
    public class HbtAuditLog : HbtBaseEntity
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
        /// 模块
        /// </summary>
        [SugarColumn(ColumnName = "module", ColumnDescription = "模块", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Module { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        [SugarColumn(ColumnName = "operation", ColumnDescription = "操作", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Operation { get; set; }

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
        /// 结果
        /// </summary>
        [SugarColumn(ColumnName = "result", ColumnDescription = "结果", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string Result { get; set; }

        /// <summary>
        /// 耗时(毫秒)
        /// </summary>
        [SugarColumn(ColumnName = "elapsed", ColumnDescription = "耗时(毫秒)", ColumnDataType = "bigint", IsNullable = false)]
        public long Elapsed { get; set; }

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