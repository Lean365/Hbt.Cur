//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowScheduledTaskStatus.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流定时任务状态枚举
//===================================================================

namespace Lean.Hbt.Common.Enums
{
    /// <summary>
    /// 工作流定时任务状态枚举
    /// </summary>
    public enum HbtWorkflowScheduledTaskStatus
    {
        /// <summary>
        /// 等待执行
        /// </summary>
        Pending = 0,

        /// <summary>
        /// 执行中
        /// </summary>
        Running = 1,

        /// <summary>
        /// 执行成功
        /// </summary>
        Completed = 2,

        /// <summary>
        /// 执行失败
        /// </summary>
        Failed = 3,

        /// <summary>
        /// 已取消
        /// </summary>
        Cancelled = 4,

        /// <summary>
        /// 已过期
        /// </summary>
        Expired = 5
    }
} 