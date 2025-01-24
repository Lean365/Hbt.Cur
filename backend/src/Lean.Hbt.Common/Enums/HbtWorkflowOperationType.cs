//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtWorkflowOperationType.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流操作类型枚举
//===================================================================

namespace Lean.Hbt.Common.Enums
{
    /// <summary>
    /// 工作流操作类型枚举
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public enum HbtWorkflowOperationType
    {
        /// <summary>
        /// 创建
        /// </summary>
        Create = 0,

        /// <summary>
        /// 提交
        /// </summary>
        Submit = 1,

        /// <summary>
        /// 审批
        /// </summary>
        Approve = 2,

        /// <summary>
        /// 驳回
        /// </summary>
        Reject = 3,

        /// <summary>
        /// 转办
        /// </summary>
        Transfer = 4,

        /// <summary>
        /// 退回
        /// </summary>
        Return = 5,

        /// <summary>
        /// 撤销
        /// </summary>
        Cancel = 6,

        /// <summary>
        /// 终止
        /// </summary>
        Terminate = 7,

        /// <summary>
        /// 删除
        /// </summary>
        Delete = 8
    }
} 