//===================================================================
// 项目名 : Lean.Hbt.Application
// 文件名 : HbtQuartzDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-26 14:30
// 版本号 : V1.0.0
// 描述   : 定时任务数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.Routine
{
    /// <summary>
    /// 定时任务基础DTO
    /// </summary>
    public class HbtQuartzDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtQuartzDto()
        {
            TaskName = string.Empty;
            TaskGroupName = string.Empty;
            TaskAssemblyName = string.Empty;
            TaskClassName = string.Empty;
            TaskCronExpression = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long TaskId { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 任务组名
        /// </summary>
        public string TaskGroupName { get; set; }

        /// <summary>
        /// 任务类型（1程序集 2网络请求 3SQL语句）
        /// </summary>
        public int TaskType { get; set; }

        /// <summary>
        /// 程序集名称
        /// </summary>
        public string TaskAssemblyName { get; set; }

        /// <summary>
        /// 任务类名
        /// </summary>
        public string TaskClassName { get; set; }

        /// <summary>
        /// API接口地址
        /// </summary>
        public string? TaskApiUrl { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string? TaskRequestMethod { get; set; }

        /// <summary>
        /// SQL语句
        /// </summary>
        public string? TaskSql { get; set; }

        /// <summary>
        /// 触发器类型（0简单 1Cron）
        /// </summary>
        public int TaskTriggerType { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        public string TaskCronExpression { get; set; }

        /// <summary>
        /// 执行间隔（秒）
        /// </summary>
        public int TaskInterval { get; set; }

        /// <summary>
        /// 是否并发执行
        /// </summary>
        public bool TaskConcurrent { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? TaskStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? TaskEndTime { get; set; }

        /// <summary>
        /// 执行参数
        /// </summary>
        public string? TaskExecuteParams { get; set; }

        /// <summary>
        /// 最后运行时间
        /// </summary>
        public DateTime? TaskLastRunTime { get; set; }

        /// <summary>
        /// 下次运行时间
        /// </summary>
        public DateTime? TaskNextRunTime { get; set; }

        /// <summary>
        /// 执行次数
        /// </summary>
        public int TaskExecuteCount { get; set; }

        /// <summary>
        /// 状态（0停用 1启用）
        /// </summary>
        public int TaskStatus { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 定时任务查询DTO
    /// </summary>
    public class HbtQuartzQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtQuartzQueryDto()
        {
            TaskName = string.Empty;
            TaskGroupName = string.Empty;
        }

        /// <summary>
        /// 任务名称
        /// </summary>
        [MaxLength(100, ErrorMessage = "任务名称长度不能超过100个字符")]
        public string? TaskName { get; set; }

        /// <summary>
        /// 任务组名
        /// </summary>
        [MaxLength(100, ErrorMessage = "任务组名长度不能超过100个字符")]
        public string? TaskGroupName { get; set; }

        /// <summary>
        /// 任务类型（1程序集 2网络请求 3SQL语句）
        /// </summary>
        public int? TaskType { get; set; }

        /// <summary>
        /// 触发器类型（0简单 1Cron）
        /// </summary>
        public int? TaskTriggerType { get; set; }

        /// <summary>
        /// 状态（0停用 1启用）
        /// </summary>
        public int? TaskStatus { get; set; }

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
    /// 定时任务创建DTO
    /// </summary>
    public class HbtQuartzCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtQuartzCreateDto()
        {
            TaskName = string.Empty;
            TaskGroupName = string.Empty;
            TaskAssemblyName = string.Empty;
            TaskClassName = string.Empty;
            TaskCronExpression = string.Empty;
        }

        /// <summary>
        /// 任务名称
        /// </summary>
        [Required(ErrorMessage = "任务名称不能为空")]
        [MaxLength(100, ErrorMessage = "任务名称长度不能超过100个字符")]
        public string TaskName { get; set; }

        /// <summary>
        /// 任务组名
        /// </summary>
        [Required(ErrorMessage = "任务组名不能为空")]
        [MaxLength(100, ErrorMessage = "任务组名长度不能超过100个字符")]
        public string TaskGroupName { get; set; }

        /// <summary>
        /// 任务类型（1程序集 2网络请求 3SQL语句）
        /// </summary>
        [Required(ErrorMessage = "任务类型不能为空")]
        public int TaskType { get; set; }

        /// <summary>
        /// 程序集名称
        /// </summary>
        [MaxLength(255, ErrorMessage = "程序集名称长度不能超过255个字符")]
        public string TaskAssemblyName { get; set; }

        /// <summary>
        /// 任务类名
        /// </summary>
        [MaxLength(255, ErrorMessage = "任务类名长度不能超过255个字符")]
        public string TaskClassName { get; set; }

        /// <summary>
        /// API接口地址
        /// </summary>
        [MaxLength(255, ErrorMessage = "API接口地址长度不能超过255个字符")]
        public string? TaskApiUrl { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        [MaxLength(10, ErrorMessage = "请求方式长度不能超过10个字符")]
        public string? TaskRequestMethod { get; set; }

        /// <summary>
        /// SQL语句
        /// </summary>
        [MaxLength(1000, ErrorMessage = "SQL语句长度不能超过1000个字符")]
        public string? TaskSql { get; set; }

        /// <summary>
        /// 触发器类型（0简单 1Cron）
        /// </summary>
        [Required(ErrorMessage = "触发器类型不能为空")]
        public int TaskTriggerType { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        [Required(ErrorMessage = "Cron表达式不能为空")]
        [MaxLength(100, ErrorMessage = "Cron表达式长度不能超过100个字符")]
        public string TaskCronExpression { get; set; }

        /// <summary>
        /// 执行间隔（秒）
        /// </summary>
        public int TaskInterval { get; set; }

        /// <summary>
        /// 是否并发执行
        /// </summary>
        public bool TaskConcurrent { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? TaskStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? TaskEndTime { get; set; }

        /// <summary>
        /// 执行参数
        /// </summary>
        [MaxLength(500, ErrorMessage = "执行参数长度不能超过500个字符")]
        public string? TaskExecuteParams { get; set; }

        /// <summary>
        /// 状态（0停用 1启用）
        /// </summary>
        public int TaskStatus { get; set; }
    }

    /// <summary>
    /// 定时任务更新DTO
    /// </summary>
    public class HbtQuartzUpdateDto : HbtQuartzCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "任务ID不能为空")]
        public long TaskId { get; set; }
    }

    /// <summary>
    /// 定时任务导入DTO
    /// </summary>
    public class HbtQuartzImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtQuartzImportDto()
        {
            TaskName = string.Empty;
            TaskGroupName = string.Empty;
            TaskAssemblyName = string.Empty;
            TaskClassName = string.Empty;
            TaskCronExpression = string.Empty;
        }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 任务组名
        /// </summary>
        public string TaskGroupName { get; set; }

        /// <summary>
        /// 任务类型（1程序集 2网络请求 3SQL语句）
        /// </summary>
        public int TaskType { get; set; }

        /// <summary>
        /// 程序集名称
        /// </summary>
        public string TaskAssemblyName { get; set; }

        /// <summary>
        /// 任务类名
        /// </summary>
        public string TaskClassName { get; set; }

        /// <summary>
        /// API接口地址
        /// </summary>
        public string? TaskApiUrl { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string? TaskRequestMethod { get; set; }

        /// <summary>
        /// SQL语句
        /// </summary>
        public string? TaskSql { get; set; }

        /// <summary>
        /// 触发器类型（0简单 1Cron）
        /// </summary>
        public int TaskTriggerType { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        public string TaskCronExpression { get; set; }

        /// <summary>
        /// 执行间隔（秒）
        /// </summary>
        public int TaskInterval { get; set; }

        /// <summary>
        /// 是否并发执行
        /// </summary>
        public int TaskConcurrent { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string? TaskStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string? TaskEndTime { get; set; }

        /// <summary>
        /// 执行参数
        /// </summary>
        public string? TaskExecuteParams { get; set; }

        /// <summary>
        /// 状态（0停用 1启用）
        /// </summary>
        public int TaskStatus { get; set; }
    }

    /// <summary>
    /// 定时任务导出DTO
    /// </summary>
    public class HbtQuartzExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtQuartzExportDto()
        {
            TaskName = string.Empty;
            TaskGroupName = string.Empty;
            TaskAssemblyName = string.Empty;
            TaskClassName = string.Empty;
            TaskCronExpression = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 任务组名
        /// </summary>
        public string TaskGroupName { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public int TaskType { get; set; }

        /// <summary>
        /// 程序集名称
        /// </summary>
        public string TaskAssemblyName { get; set; }

        /// <summary>
        /// 任务类名
        /// </summary>
        public string TaskClassName { get; set; }

        /// <summary>
        /// API接口地址
        /// </summary>
        public string? TaskApiUrl { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string? TaskRequestMethod { get; set; }

        /// <summary>
        /// SQL语句
        /// </summary>
        public string? TaskSql { get; set; }

        /// <summary>
        /// 触发器类型
        /// </summary>
        public int TaskTriggerType { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        public string TaskCronExpression { get; set; }

        /// <summary>
        /// 执行间隔（秒）
        /// </summary>
        public int TaskInterval { get; set; }

        /// <summary>
        /// 是否并发执行
        /// </summary>
        public int TaskConcurrent { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? TaskStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? TaskEndTime { get; set; }

        /// <summary>
        /// 执行参数
        /// </summary>
        public string? TaskExecuteParams { get; set; }

        /// <summary>
        /// 最后运行时间
        /// </summary>
        public DateTime? TaskLastRunTime { get; set; }

        /// <summary>
        /// 下次运行时间
        /// </summary>
        public DateTime? TaskNextRunTime { get; set; }

        /// <summary>
        /// 执行次数
        /// </summary>
        public int TaskExecuteCount { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int TaskStatus { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 定时任务导入模板DTO
    /// </summary>
    public class HbtQuartzTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtQuartzTemplateDto()
        {
            TaskName = string.Empty;
            TaskGroupName = string.Empty;
            TaskAssemblyName = string.Empty;
            TaskClassName = string.Empty;
            TaskCronExpression = string.Empty;
        }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 任务组名
        /// </summary>
        public string TaskGroupName { get; set; }

        /// <summary>
        /// 任务类型(1=程序集,2=网络请求,3=SQL语句)
        /// </summary>
        public int TaskType { get; set; }

        /// <summary>
        /// 程序集名称
        /// </summary>
        public string TaskAssemblyName { get; set; }

        /// <summary>
        /// 任务类名
        /// </summary>
        public string TaskClassName { get; set; }

        /// <summary>
        /// API接口地址
        /// </summary>
        public string? TaskApiUrl { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string? TaskRequestMethod { get; set; }

        /// <summary>
        /// SQL语句
        /// </summary>
        public string? TaskSql { get; set; }

        /// <summary>
        /// 触发器类型(0=简单,1=Cron)
        /// </summary>
        public int TaskTriggerType { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        public string TaskCronExpression { get; set; }

        /// <summary>
        /// 执行间隔（秒）
        /// </summary>
        public int TaskInterval { get; set; }

        /// <summary>
        /// 是否并发执行(0=否,1=是)
        /// </summary>
        public int TaskConcurrent { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string? TaskStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string? TaskEndTime { get; set; }

        /// <summary>
        /// 执行参数
        /// </summary>
        public string? TaskExecuteParams { get; set; }

        /// <summary>
        /// 状态(0=停用,1=启用)
        /// </summary>
        public int TaskStatus { get; set; }
    }
}