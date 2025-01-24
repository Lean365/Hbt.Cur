namespace Lean.Hbt.Common.Enums
{
    /// <summary>
    /// 工作流任务类型枚举
    /// </summary>
    public enum HbtWorkflowTaskType
    {
        /// <summary>
        /// 审批任务
        /// </summary>
        Approval = 0,

        /// <summary>
        /// 会签任务
        /// </summary>
        Countersign = 1,

        /// <summary>
        /// 传阅任务
        /// </summary>
        Circulate = 2,

        /// <summary>
        /// 抄送任务
        /// </summary>
        Copy = 3
    }
} 