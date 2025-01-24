namespace Lean.Hbt.Common.Enums
{
    /// <summary>
    /// 工作流实例状态枚举
    /// </summary>
    public enum HbtWorkflowInstanceStatus
    {
        /// <summary>
        /// 草稿
        /// </summary>
        Draft = 0,

        /// <summary>
        /// 运行中
        /// </summary>
        Running = 1,

        /// <summary>
        /// 已暂停
        /// </summary>
        Suspended = 2,

        /// <summary>
        /// 已完成
        /// </summary>
        Completed = 3,

        /// <summary>
        /// 已终止
        /// </summary>
        Terminated = 4,

        /// <summary>
        /// 已删除
        /// </summary>
        Deleted = 5
    }
} 