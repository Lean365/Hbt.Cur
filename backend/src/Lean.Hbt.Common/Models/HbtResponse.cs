//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtResponse.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-05 10:00
// 版本号 : V1.0.0
// 描述    : API统一响应模型
//===================================================================

namespace Lean.Hbt.Common.Models
{
    /// <summary>
    /// API统一响应模型
    /// </summary>
    /// <typeparam name="T">响应数据类型</typeparam>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-05
    /// </remarks>
    public class HbtResponse<T>
    {
        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 响应数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 创建成功响应
        /// </summary>
        /// <param name="data">响应数据</param>
        /// <param name="message">提示信息</param>
        /// <returns>成功的响应对象</returns>
        public static HbtResponse<T> Ok(T data, string message = "操作成功")
        {
            return new HbtResponse<T>
            {
                Success = true,
                Code = "200",
                Message = message,
                Data = data
            };
        }

        /// <summary>
        /// 创建失败响应
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="message">错误信息</param>
        /// <returns>失败的响应对象</returns>
        public static HbtResponse<T> Fail(string code, string message)
        {
            return new HbtResponse<T>
            {
                Success = false,
                Code = code,
                Message = message,
                Data = default
            };
        }
    }

    /// <summary>
    /// API分页响应模型
    /// </summary>
    /// <typeparam name="T">分页数据类型</typeparam>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-05
    /// </remarks>
    public class HbtPagedResponse<T> : HbtResponse<T>
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPreviousPage => PageIndex > 1;

        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNextPage => PageIndex < TotalPages;

        /// <summary>
        /// 创建分页成功响应
        /// </summary>
        /// <param name="data">分页数据</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="message">提示信息</param>
        /// <returns>分页成功的响应对象</returns>
        public static HbtPagedResponse<T> Ok(T data, int pageIndex, int pageSize, int totalCount, string message = "操作成功")
        {
            return new HbtPagedResponse<T>
            {
                Success = true,
                Code = "200",
                Message = message,
                Data = data,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }
    }
} 