//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginLogController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 登录日志控制器
//===================================================================

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Application.Dtos.Audit;
using Lean.Hbt.Application.Services.Audit;

namespace Lean.Hbt.WebApi.Controllers.Audit
{
    /// <summary>
    /// 登录日志控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class HbtLoginLogController : HbtBaseController
    {
        private readonly IHbtLoginLogService _loginLogService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="loginLogService">登录日志服务</param>
        public HbtLoginLogController(IHbtLoginLogService loginLogService)
        {
            _loginLogService = loginLogService;
        }

        /// <summary>
        /// 获取登录日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>登录日志分页列表</returns>
        [HttpGet]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] HbtLoginLogQueryDto query)
        {
            var result = await _loginLogService.GetPagedListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取登录日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>登录日志详情</returns>
        [HttpGet("{logId}")]
        public async Task<IActionResult> GetAsync(long logId)
        {
            var result = await _loginLogService.GetAsync(logId);
            return Success(result);
        }

        /// <summary>
        /// 导出登录日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtLoginLogQueryDto query)
        {
            var result = await _loginLogService.ExportAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 清空登录日志
        /// </summary>
        /// <returns>是否成功</returns>
        [HttpDelete("clear")]
        public async Task<IActionResult> ClearAsync()
        {
            var result = await _loginLogService.ClearAsync();
            return Success(result);
        }
    }
} 