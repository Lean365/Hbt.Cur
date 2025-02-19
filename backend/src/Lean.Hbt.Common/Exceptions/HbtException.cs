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
        /// 构造函数 - 默认为服务器错误
        /// </summary>
        /// <param name="message">错误消息</param>
        public HbtException(string message) 
            : this(message, HbtConstants.ErrorCodes.ServerError)
        {
        }

        /// <summary>
        /// 构造函数 - 指定错误码
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="code">错误码</param>
        public HbtException(string message, string code) : base(message)
        {
            Code = int.Parse(code);
        }

        /// <summary>
        /// 构造函数 - 包含内部异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="innerException">内部异常</param>
        public HbtException(string message, Exception innerException) : base(message, innerException)
        {
            Code = int.Parse(HbtConstants.ErrorCodes.ServerError);
        }

        /// <summary>
        /// 构造函数 - 完整参数
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="code">错误码</param>
        /// <param name="innerException">内部异常</param>
        public HbtException(string message, string code, Exception innerException) : base(message, innerException)
        {
            Code = int.Parse(code);
        }

        /// <summary>
        /// 创建验证错误异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <returns>异常实例</returns>
        public static HbtException ValidationError(string message)
        {
            return new HbtException(message, HbtConstants.ErrorCodes.ValidationFailed);
        }

        /// <summary>
        /// 创建未授权异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <returns>异常实例</returns>
        public static HbtException Unauthorized(string message)
        {
            return new HbtException(message, HbtConstants.ErrorCodes.Unauthorized);
        }

        /// <summary>
        /// 创建禁止访问异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <returns>异常实例</returns>
        public static HbtException Forbidden(string message)
        {
            return new HbtException(message, HbtConstants.ErrorCodes.Forbidden);
        }

        /// <summary>
        /// 创建资源未找到异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <returns>异常实例</returns>
        public static HbtException NotFound(string message)
        {
            return new HbtException(message, HbtConstants.ErrorCodes.NotFound);
        }

        /// <summary>
        /// 创建业务错误异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <returns>异常实例</returns>
        public static HbtException BusinessError(string message)
        {
            return new HbtException(message, HbtConstants.ErrorCodes.ServerError);
        }
    }
}