//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtBusinessException.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-17 16:30
// 版本号 : V.0.0.1
// 描述    : 业务异常类
//===================================================================

using Lean.Hbt.Common.Constants;

namespace Lean.Hbt.Common.Exceptions
{
    /// <summary>
    /// 业务异常类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-17
    /// </remarks>
    public class HbtBusinessException : HbtException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误消息</param>
        public HbtBusinessException(string message) 
            : base(message, 500)
        {
        }
    }
} 