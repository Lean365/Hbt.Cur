#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowTaskStatus.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流任务状态枚举
//===================================================================

namespace Lean.Hbt.Common.Enums
{
    /// <summary>
    /// 工作流任务状态枚举
    /// </summary>
    public enum HbtWorkflowTaskStatus
    {
        /// <summary>
        /// 待处理
        /// </summary>
        Pending = 1,

        /// <summary>
        /// 处理中
        /// </summary>
        Processing = 2,

        /// <summary>
        /// 已完成
        /// </summary>
        Completed = 3,

        /// <summary>
        /// 已取消
        /// </summary>
        Cancelled = 4,

        /// <summary>
        /// 已超时
        /// </summary>
        Timeout = 5,

        /// <summary>
        /// 已转交
        /// </summary>
        Transferred = 6
    }
} 