//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtException.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-17 19:00
// 版本号 : V0.0.1
// 描述    : 自定义异常类
//===================================================================

using System;
using Lean.Hbt.Common.Constants;
namespace Lean.Hbt.Common.Exceptions
{
    /// <summary>
    /// 自定义异常类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-17
    /// </remarks>
    public class HbtException : Exception
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int Code { get; }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误消息</param>
        public HbtException(string message) : base(message)
        {
            Code = 500;
        }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="code">错误码</param>
        public HbtException(string message, int code) : base(message)
        {
            Code = code;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="innerException">内部异常</param>
        public HbtException(string message, Exception innerException) : base(message, innerException)
        {
            Code = 500;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="code">错误码</param>
        /// <param name="innerException">内部异常</param>
        public HbtException(string message, int code, Exception innerException) : base(message, innerException)
        {
            Code = code;
        }
    }
}