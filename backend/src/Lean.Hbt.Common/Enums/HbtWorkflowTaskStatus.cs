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
        Pending = 0,

        /// <summary>
        /// 处理中
        /// </summary>
        Processing = 1,

        /// <summary>
        /// 已完成
        /// </summary>
        Completed = 2,

        /// <summary>
        /// 已取消
        /// </summary>
        Cancelled = 3,

        /// <summary>
        /// 已转交
        /// </summary>
        Transferred = 4,

        /// <summary>
        /// 已退回
        /// </summary>
        Returned = 5
    }
} 