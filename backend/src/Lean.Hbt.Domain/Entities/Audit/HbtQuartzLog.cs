//===================================================================
// 项目名 : Lean.Hbt.Domain.Entities.Audit
// 文件名 : HbtQuartzLog.cs
// 创建者 : Lean365
// 创建时间: 2024-01-26 14:30
// 版本号 : V1.0.0
// 描述   : 定时任务日志实体
//===================================================================

namespace Lean.Hbt.Domain.Entities.Audit
{
    /// <summary>
    /// 定时任务日志实体
    /// </summary>
    [SugarTable("hbt_audit_quartz_log", "任务日志")]
    public class HbtQuartzLog : HbtBaseEntity
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        [SugarColumn(ColumnName = "log_task_id", ColumnDescription = "任务ID", IsNullable = false, DefaultValue = "0", ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long LogTaskId { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        [SugarColumn(ColumnName = "log_task_name", ColumnDescription = "任务名称", Length = 100, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string LogTaskName { get; set; } = string.Empty;

        /// <summary>
        /// 任务组名
        /// </summary>
        [SugarColumn(ColumnName = "log_group_name", ColumnDescription = "任务组名", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string LogGroupName { get; set; } = string.Empty;

        /// <summary>
        /// 执行时间
        /// </summary>
        [SugarColumn(ColumnName = "log_execute_time", ColumnDescription = "执行时间", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime LogExecuteTime { get; set; }

        /// <summary>
        /// 执行耗时（毫秒）
        /// </summary>
        [SugarColumn(ColumnName = "log_execute_duration", ColumnDescription = "执行耗时（毫秒）", IsNullable = false, DefaultValue = "0", ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long LogExecuteDuration { get; set; }

        /// <summary>
        /// 执行参数
        /// </summary>
        [SugarColumn(ColumnName = "log_execute_params", ColumnDescription = "执行参数", Length = 1000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? LogExecuteParams { get; set; }

        /// <summary>
        /// 执行状态（0失败 1成功）
        /// </summary>
        [SugarColumn(ColumnName = "log_status", ColumnDescription = "执行状态（0失败 1成功）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int LogStatus { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [SugarColumn(ColumnName = "log_error_info", ColumnDescription = "错误信息", Length = 2000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? LogErrorInfo { get; set; }

        /// <summary>
        /// 执行机器IP
        /// </summary>
        [SugarColumn(ColumnName = "log_execute_ip", ColumnDescription = "执行机器IP", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? LogExecuteIp { get; set; }

        /// <summary>
        /// 执行机器名
        /// </summary>
        [SugarColumn(ColumnName = "log_execute_host", ColumnDescription = "执行机器名", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? LogExecuteHost { get; set; }
    }
}