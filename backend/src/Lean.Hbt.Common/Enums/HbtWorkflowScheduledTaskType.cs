//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowScheduledTaskType.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流定时任务类型枚举
//===================================================================

namespace Lean.Hbt.Common.Enums
{
    /// <summary>
    /// 工作流定时任务类型枚举
    /// </summary>
    public enum HbtWorkflowScheduledTaskType
    {
        /// <summary>
        /// 超时提醒
        /// </summary>
        TimeoutReminder = 0,

        /// <summary>
        /// 自动执行
        /// </summary>
        AutoExecution = 1,

        /// <summary>
        /// 定时触发
        /// </summary>
        TimedTrigger = 2,

        /// <summary>
        /// 延迟执行
        /// </summary>
        DelayedExecution = 3,

        /// <summary>
        /// 周期执行
        /// </summary>
        PeriodicExecution = 4
    }
} 