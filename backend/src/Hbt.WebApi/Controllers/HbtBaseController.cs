//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtBaseController.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-05 10:00
// 版本号 : V.0.0.1
// 描述    : 通用基础控制器
//===================================================================

using Microsoft.AspNetCore.Mvc;
using Hbt.Cur.Common.Models;
using Hbt.Cur.Domain.IServices.Extensions;

namespace Hbt.Cur.WebApi.Controllers
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class HbtBaseController : ControllerBase
    {
        /// <summary>
        /// 本地化服务
        /// </summary>
        protected readonly IHbtLocalizationService _localization;

        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly IHbtLogger _logger;

        /// <summary>
        /// 当前用户服务
        /// </summary>
        protected readonly IHbtCurrentUser _currentUser;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        protected HbtBaseController(IHbtLogger logger, IHbtCurrentUser currentUser,  IHbtLocalizationService localization)
        {
            _logger = logger;
            _currentUser = currentUser;
            _localization = localization;
        }

        /// <summary>
        /// 返回成功结果
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="message">消息</param>
        /// <returns>操作结果</returns>
        protected IActionResult Success(object? data = null, string message = "Common.OperationSuccess")
        {
            return Ok(HbtApiResult.Success(data, _localization.L(message)));
        }

        /// <summary>
        /// 返回失败结果
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="code">错误代码</param>
        /// <returns>操作结果</returns>
        protected IActionResult Error(string message = "Common.OperationFailed", int code = 400)
        {
            return Ok(HbtApiResult.Error(_localization.L(message), code));
        }
    }
} 