//===================================================================
// 项目名 : Lean.Hbt.Domain.Entities.Routine
// 文件名 : HbtQuartzTask.cs
// 创建者 : Lean365
// 创建时间: 2024-01-26 14:30
// 版本号 : V1.0.0
// 描述   : 定时任务实体
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Routine
{
    /// <summary>
    /// 定时任务实体
    /// </summary>
    [SugarTable("hbt_rou_quartz_task", "定时任务")]
    [SugarIndex("index_task_name", nameof(TaskName), OrderByType.Asc)]
    [SugarIndex("index_task_group", nameof(TaskGroupName), OrderByType.Asc)]
    [SugarIndex("index_task_status", nameof(TaskStatus), OrderByType.Asc)]
    public class HbtQuartzTask : HbtBaseEntity
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        [SugarColumn(ColumnName = "task_name", ColumnDescription = "任务名称", Length = 100, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string TaskName { get; set; } = string.Empty;

        /// <summary>
        /// 任务组名
        /// </summary>
        [SugarColumn(ColumnName = "task_group_name", ColumnDescription = "任务组名", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string TaskGroupName { get; set; } = string.Empty;

        /// <summary>
        /// 任务类型（1.程序集 2.网络请求 3.SQL语句）
        /// </summary>
        [SugarColumn(ColumnName = "task_type", ColumnDescription = "任务类型（1.程序集 2.网络请求 3.SQL语句）", IsNullable = false, DefaultValue = "1", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int TaskType { get; set; }

        /// <summary>
        /// 程序集名称
        /// </summary>
        [SugarColumn(ColumnName = "task_assembly_name", ColumnDescription = "程序集名称", Length = 255, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string TaskAssemblyName { get; set; } = string.Empty;

        /// <summary>
        /// 任务类名
        /// </summary>
        [SugarColumn(ColumnName = "task_class_name", ColumnDescription = "任务类名", Length = 255, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string TaskClassName { get; set; } = string.Empty;

        /// <summary>
        /// API执行地址
        /// </summary>
        [SugarColumn(ColumnName = "task_api_url", ColumnDescription = "API执行地址", Length = 255, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? TaskApiUrl { get; set; }

        /// <summary>
        /// 网络请求方式
        /// </summary>
        [SugarColumn(ColumnName = "task_request_method", ColumnDescription = "网络请求方式", Length = 10, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? TaskRequestMethod { get; set; }

        /// <summary>
        /// SQL语句
        /// </summary>
        [SugarColumn(ColumnName = "task_sql", ColumnDescription = "SQL语句", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? TaskSql { get; set; }

        /// <summary>
        /// 触发器类型（0.simple 1.cron）
        /// </summary>
        [SugarColumn(ColumnName = "task_trigger_type", ColumnDescription = "触发器类型（0.simple 1.cron）", IsNullable = false, DefaultValue = "1", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int TaskTriggerType { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        [SugarColumn(ColumnName = "task_cron_expression", ColumnDescription = "Cron表达式", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string TaskCronExpression { get; set; } = string.Empty;

        /// <summary>
        /// 执行间隔时间（单位秒）
        /// </summary>
        [SugarColumn(ColumnName = "task_interval", ColumnDescription = "执行间隔时间（单位秒）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int TaskInterval { get; set; }

        /// <summary>
        /// 执行参数
        /// </summary>
        [SugarColumn(ColumnName = "task_execute_params", ColumnDescription = "执行参数", Length = 1000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? TaskExecuteParams { get; set; }

        /// <summary>
        /// 是否并发执行
        /// </summary>
        [SugarColumn(ColumnName = "task_concurrent", ColumnDescription = "是否并发执行", IsNullable = false, DefaultValue = "0", ColumnDataType = "bit", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public bool TaskConcurrent { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [SugarColumn(ColumnName = "task_start_time", ColumnDescription = "开始时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? TaskStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [SugarColumn(ColumnName = "task_end_time", ColumnDescription = "结束时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? TaskEndTime { get; set; }

        /// <summary>
        /// 最近执行时间
        /// </summary>
        [SugarColumn(ColumnName = "task_last_run_time", ColumnDescription = "最近执行时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? TaskLastRunTime { get; set; }

        /// <summary>
        /// 下次执行时间
        /// </summary>
        [SugarColumn(ColumnName = "task_next_run_time", ColumnDescription = "下次执行时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? TaskNextRunTime { get; set; }

        /// <summary>
        /// 执行次数
        /// </summary>
        [SugarColumn(ColumnName = "task_execute_count", ColumnDescription = "执行次数", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int TaskExecuteCount { get; set; }

        /// <summary>
        /// 状态（0停用 1启用）
        /// </summary>
        [SugarColumn(ColumnName = "task_status", ColumnDescription = "状态（0停用 1启用）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int TaskStatus { get; set; }

    }
} 