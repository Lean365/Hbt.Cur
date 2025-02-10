#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowTaskType.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流任务类型枚举
//===================================================================

namespace Lean.Hbt.Common.Enums
{
    /// <summary>
    /// 工作流任务类型枚举
    /// </summary>
    public enum HbtWorkflowTaskType
    {
        /// <summary>
        /// 审批
        /// </summary>
        Approval = 1,

        /// <summary>
        /// 会签
        /// </summary>
        Countersign = 2,

        /// <summary>
        /// 传阅
        /// </summary>
        Circulation = 3,

        /// <summary>
        /// 处理
        /// </summary>
        Handle = 4
    }
} 