//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtWorkflowVariableType.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流变量类型枚举
//===================================================================

namespace Lean.Hbt.Common.Enums
{
    /// <summary>
    /// 工作流变量类型枚举
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public enum HbtWorkflowVariableType
    {
        /// <summary>
        /// 字符串
        /// </summary>
        String = 0,

        /// <summary>
        /// 整数
        /// </summary>
        Integer = 1,

        /// <summary>
        /// 小数
        /// </summary>
        Decimal = 2,

        /// <summary>
        /// 布尔值
        /// </summary>
        Boolean = 3,

        /// <summary>
        /// 日期时间
        /// </summary>
        DateTime = 4,

        /// <summary>
        /// JSON对象
        /// </summary>
        JsonObject = 5,

        /// <summary>
        /// JSON数组
        /// </summary>
        JsonArray = 6
    }
} 