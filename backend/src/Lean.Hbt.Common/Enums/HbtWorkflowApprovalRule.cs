//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtWorkflowApprovalRule.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流审批规则枚举
//===================================================================

namespace Lean.Hbt.Common.Enums
{
    /// <summary>
    /// 工作流审批规则枚举
    /// </summary>
    public enum HbtWorkflowApprovalRule
    {
        /// <summary>
        /// 所有审批人必须同意
        /// </summary>
        All = 1,

        /// <summary>
        /// 任意一个审批人同意即可
        /// </summary>
        Any = 2,

        /// <summary>
        /// 按指定数量审批人同意
        /// </summary>
        Count = 3,

        /// <summary>
        /// 按自定义条件审批
        /// </summary>
        Custom = 4
    }
} 