//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtApiResult.cs
// 创建者 : Lean365
// 创建时间: 2024-01-05 10:00
// 版本号 : V.0.0.1
// 描述    : API统一响应模型
//===================================================================

using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Lean.Hbt.Common.Models
{
    /// <summary>
    /// API统一响应模型
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-05
    /// </remarks>
    public class HbtApiResult<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; } = string.Empty;

        /// <summary>
        /// 数据
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="message">消息</param>
        /// <returns>成功的响应结果</returns>
        public static HbtApiResult<TData> Success<TData>(TData? data = default, string message = "操作成功")
        {
            return new HbtApiResult<TData>
            {
                Code = 200,
                Msg = message,
                Data = data
            };
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="code">错误码</param>
        /// <returns>失败的响应结果</returns>
        public static HbtApiResult<T> Error(string message = "操作失败", int code = 500)
        {
            return new HbtApiResult<T>
            {
                Code = code,
                Msg = message,
                Data = default
            };
        }
    }

    /// <summary>
    /// API统一响应模型(无数据)
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-05
    /// </remarks>
    public class HbtApiResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; } = string.Empty;

        /// <summary>
        /// 数据
        /// </summary>
        public object? Data { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="message">消息</param>
        /// <returns>成功的响应结果</returns>
        public static HbtApiResult Success(object? data = null, string message = "操作成功")
        {
            return new HbtApiResult
            {
                Code = 200,
                Msg = message ?? "操作成功",
                Data = data
            };
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="code">错误码</param>
        /// <returns>失败的响应结果</returns>
        public static HbtApiResult Error(string message = "操作失败", int code = 500)
        {
            return new HbtApiResult
            {
                Code = code,
                Msg = message ?? "操作失败",
                Data = null
            };
        }
    }
}