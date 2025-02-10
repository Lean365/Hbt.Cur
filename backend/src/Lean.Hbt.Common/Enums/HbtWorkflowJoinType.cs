//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtWorkflowJoinType.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流汇聚类型枚举
//===================================================================

namespace Lean.Hbt.Common.Enums
{
    /// <summary>
    /// 工作流汇聚类型枚举
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public enum HbtWorkflowJoinType
    {
        /// <summary>
        /// 全部分支完成
        /// </summary>
        All = 1,

        /// <summary>
        /// 任意分支完成
        /// </summary>
        Any = 2,

        /// <summary>
        /// 满足条件数量
        /// </summary>
        Count = 3,

        /// <summary>
        /// 满足自定义条件
        /// </summary>
        Condition = 4
    }
} 