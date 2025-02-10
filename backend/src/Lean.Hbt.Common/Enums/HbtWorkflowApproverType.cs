#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowApproverType.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流审批人类型枚举
//===================================================================

namespace Lean.Hbt.Common.Enums
{
    /// <summary>
    /// 工作流审批人类型枚举
    /// </summary>
    public enum HbtWorkflowApproverType
    {
        /// <summary>
        /// 指定人员
        /// </summary>
        Specified = 1,

        /// <summary>
        /// 指定角色
        /// </summary>
        Role = 2,

        /// <summary>
        /// 指定部门
        /// </summary>
        Department = 3,

        /// <summary>
        /// 发起人
        /// </summary>
        Initiator = 4,

        /// <summary>
        /// 发起人上级
        /// </summary>
        InitiatorSuperior = 5,

        /// <summary>
        /// 发起人部门负责人
        /// </summary>
        InitiatorDeptManager = 6
    }
} 