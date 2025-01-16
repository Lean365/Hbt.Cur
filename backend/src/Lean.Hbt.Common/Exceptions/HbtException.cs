//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtException.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-05 10:00
// 版本号 : V1.0.0
// 描述    : 系统基础异常类
//===================================================================

using Lean.Hbt.Common.Constants;

namespace Lean.Hbt.Common.Exceptions
{
    /// <summary>
    /// 系统基础异常类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-05
    /// </remarks>
    public class HbtException : Exception
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="code">错误代码</param>
        public HbtException(string message, string code = HbtConstants.ErrorCodes.ServerError) 
            : base(message)
        {
            Code = code;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="innerException">内部异常</param>
        /// <param name="code">错误代码</param>
        public HbtException(string message, Exception innerException, string code = HbtConstants.ErrorCodes.ServerError) 
            : base(message, innerException)
        {
            Code = code;
        }
    }

    /// <summary>
    /// 未授权异常
    /// </summary>
    public class HbtUnauthorizedException : HbtException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误消息</param>
        public HbtUnauthorizedException(string message = "未授权访问") 
            : base(message, HbtConstants.ErrorCodes.Unauthorized)
        {
        }
    }

    /// <summary>
    /// 禁止访问异常
    /// </summary>
    public class HbtForbiddenException : HbtException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误消息</param>
        public HbtForbiddenException(string message = "禁止访问") 
            : base(message, HbtConstants.ErrorCodes.Forbidden)
        {
        }
    }

    /// <summary>
    /// 资源未找到异常
    /// </summary>
    public class HbtNotFoundException : HbtException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误消息</param>
        public HbtNotFoundException(string message = "资源未找到") 
            : base(message, HbtConstants.ErrorCodes.NotFound)
        {
        }
    }

    /// <summary>
    /// 数据验证异常
    /// </summary>
    public class HbtValidationException : HbtException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误消息</param>
        public HbtValidationException(string message = "数据验证失败") 
            : base(message, HbtConstants.ErrorCodes.ValidationFailed)
        {
        }
    }
} 