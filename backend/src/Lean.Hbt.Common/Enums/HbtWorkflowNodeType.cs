namespace Lean.Hbt.Common.Enums
{
    /// <summary>
    /// 工作流节点类型枚举
    /// </summary>
    public enum HbtWorkflowNodeType
    {
        /// <summary>
        /// 开始节点
        /// </summary>
        Start = 0,

        /// <summary>
        /// 审批节点
        /// </summary>
        Approval = 1,

        /// <summary>
        /// 分支节点
        /// </summary>
        Branch = 2,

        /// <summary>
        /// 并行节点
        /// </summary>
        Parallel = 3,

        /// <summary>
        /// 汇聚节点
        /// </summary>
        Join = 4,

        /// <summary>
        /// 结束节点
        /// </summary>
        End = 5
    }
} 