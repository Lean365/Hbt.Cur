//===================================================================
// 项目名 : Lean.Hbt.Common
// 文件名 : HbtRealTimeNotification.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 实时通知模型
//===================================================================

using System;
using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.Common.Models
{
    /// <summary>
    /// 实时通知模型
    /// </summary>
    public class HbtRealTimeNotification
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public HbtMessageType Type { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object? Data { get; set; }
    }
} 