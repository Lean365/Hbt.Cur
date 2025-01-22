//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtMessageType.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V1.0.0
// 描述    : 消息类型枚举
//===================================================================

namespace Lean.Hbt.Common.Enums
{
    /// <summary>
    /// 消息类型枚举
    /// </summary>
    public enum HbtMessageType
    {
        /// <summary>
        /// 系统消息
        /// </summary>
        System = 1,

        /// <summary>
        /// 用户消息
        /// </summary>
        User = 2,

        /// <summary>
        /// 群组消息
        /// </summary>
        Group = 3,

        /// <summary>
        /// 通知消息
        /// </summary>
        Notification = 4
    }
} 