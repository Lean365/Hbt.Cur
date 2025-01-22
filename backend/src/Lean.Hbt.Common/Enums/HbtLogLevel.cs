#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLogLevel.cs
// 创建者 : Lean365
// 创建时间: 2024-01-19 10:00
// 版本号 : V.0.0.1
// 描述    : 日志级别枚举
//===================================================================

namespace Lean.Hbt.Common.Enums
{
    /// <summary>
    /// 日志级别枚举
    /// </summary>
    public enum HbtLogLevel
    {
        /// <summary>
        /// 跟踪信息
        /// </summary>
        Trace = 0,

        /// <summary>
        /// 调试信息
        /// </summary>
        Debug = 1,

        /// <summary>
        /// 一般信息
        /// </summary>
        Info = 2,

        /// <summary>
        /// 警告信息
        /// </summary>
        Warn = 3,

        /// <summary>
        /// 错误信息
        /// </summary>
        Error = 4,

        /// <summary>
        /// 致命错误
        /// </summary>
        Fatal = 5
    }
} 