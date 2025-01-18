//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtBaseController.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-05 10:00
// 版本号 : V.0.0.1
// 描述    : 通用基础控制器
//===================================================================

using Microsoft.AspNetCore.Mvc;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.WebApi.Controllers
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class HbtBaseController : ControllerBase
    {
        /// <summary>
        /// 返回成功结果
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="message">消息</param>
        /// <returns>操作结果</returns>
        protected IActionResult Success(object? data = null, string message = "操作成功")
        {
            return Ok(HbtApiResult.Success(data, message));
        }

        /// <summary>
        /// 返回失败结果
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="code">错误代码</param>
        /// <returns>操作结果</returns>
        protected IActionResult Error(string message = "操作失败", int code = 400)
        {
            return Ok(HbtApiResult.Error(message, code));
        }
    }
} 