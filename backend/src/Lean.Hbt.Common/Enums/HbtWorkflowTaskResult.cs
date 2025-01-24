namespace Lean.Hbt.Common.Enums
{
    /// <summary>
    /// 工作流任务处理结果枚举
    /// </summary>
    public enum HbtWorkflowTaskResult
    {
        /// <summary>
        /// 同意
        /// </summary>
        Approved = 1,

        /// <summary>
        /// 拒绝
        /// </summary>
        Rejected = 2,

        /// <summary>
        /// 转交
        /// </summary>
        Transferred = 3,

        /// <summary>
        /// 退回
        /// </summary>
        Returned = 4
    }
} 