//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOnlineMessageController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 在线消息控制器
//===================================================================

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Application.Dtos.RealTime;
using Lean.Hbt.Application.Services.RealTime;

namespace Lean.Hbt.WebApi.Controllers.RealTime
{
    /// <summary>
    /// 在线消息控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class HbtOnlineMessageController : HbtBaseController
    {
        private readonly IHbtOnlineMessageService _onlineMessageService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="onlineMessageService">在线消息服务</param>
        public HbtOnlineMessageController(IHbtOnlineMessageService onlineMessageService)
        {
            _onlineMessageService = onlineMessageService;
        }

        /// <summary>
        /// 获取在线消息分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>在线消息分页列表</returns>
        [HttpGet]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] HbtOnlineMessageQueryDto query)
        {
            var result = await _onlineMessageService.GetPagedListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取消息详情
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <returns>消息详情</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            var result = await _onlineMessageService.GetMessageAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 导出在线消息数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>导出数据列表</returns>
        [HttpGet("export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtOnlineMessageQueryDto query)
        {
            var result = await _onlineMessageService.GetExportDataAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 保存消息
        /// </summary>
        /// <param name="input">消息信息</param>
        /// <returns>消息ID</returns>
        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] HbtOnlineMessageDto input)
        {
            var result = await _onlineMessageService.SaveMessageAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _onlineMessageService.DeleteMessageAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 清理过期消息
        /// </summary>
        /// <param name="days">保留天数</param>
        /// <returns>清理数量</returns>
        [HttpPost("cleanup")]
        public async Task<IActionResult> CleanupExpiredMessagesAsync([FromQuery] int days = 7)
        {
            var result = await _onlineMessageService.CleanupExpiredMessagesAsync(days);
            return Success(result);
        }
    }
} 