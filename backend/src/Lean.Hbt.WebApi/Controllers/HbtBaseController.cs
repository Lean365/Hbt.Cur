//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtBaseController.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-05 10:00
// 版本号 : V1.0.0
// 描述    : 通用基础控制器
//===================================================================

using Microsoft.AspNetCore.Mvc;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.WebApi.Controllers
{
    /// <summary>
    /// 通用基础控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-05
    /// </remarks>
    [ApiController]
    [Route("api/hbt/[controller]")]
    public abstract class HbtBaseController : ControllerBase
    {
        /// <summary>
        /// 成功返回
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="data">数据</param>
        /// <param name="message">消息</param>
        /// <returns>统一返回结果</returns>
        protected IActionResult Success<T>(T data = default, string message = "操作成功")
        {
            return Ok(HbtApiResult<T>.Success(data, message));
        }

        /// <summary>
        /// 成功返回(无数据)
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns>统一返回结果</returns>
        protected IActionResult Success(string message = "操作成功")
        {
            return Ok(HbtApiResult.Success(null, message));
        }

        /// <summary>
        /// 失败返回
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="code">错误码</param>
        /// <returns>统一返回结果</returns>
        protected IActionResult Error(string message = "操作失败", int code = 500)
        {
            return Ok(HbtApiResult.Error(message, code));
        }
    }
} 