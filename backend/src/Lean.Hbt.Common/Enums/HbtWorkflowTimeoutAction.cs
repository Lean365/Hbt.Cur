//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtWorkflowTimeoutAction.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流超时处理方式枚举
//===================================================================

namespace Lean.Hbt.Common.Enums
{
    /// <summary>
    /// 工作流超时处理方式枚举
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public enum HbtWorkflowTimeoutAction
    {
        /// <summary>
        /// 仅提醒
        /// </summary>
        Notify = 1,

        /// <summary>
        /// 自动通过
        /// </summary>
        AutoApprove = 2,

        /// <summary>
        /// 自动拒绝
        /// </summary>
        AutoReject = 3,

        /// <summary>
        /// 转交上级
        /// </summary>
        EscalateToSuperior = 4,

        /// <summary>
        /// 转交管理员
        /// </summary>
        EscalateToAdmin = 5
    }
} 