//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDeviceExtendController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V1.0.0
// 描述    : 设备扩展信息控制器
//===================================================================

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Application.Services.Identity;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.IServices.Admin;

namespace Lean.Hbt.WebApi.Controllers.Identity
{
    /// <summary>
    /// 设备扩展信息控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-22
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class HbtDeviceExtendController : HbtBaseController
    {
        private readonly IHbtDeviceExtendService _deviceExtendService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="deviceExtendService">设备扩展信息服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtDeviceExtendController(IHbtDeviceExtendService deviceExtendService, IHbtLocalizationService localization) : base(localization)
        {
            _deviceExtendService = deviceExtendService;
        }

        /// <summary>
        /// 获取设备扩展信息分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页列表</returns>
        [HttpGet]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] HbtDeviceExtendPageRequest query)
        {
            var result = await _deviceExtendService.GetPagedListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 导出设备扩展信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>导出数据列表</returns>
        [HttpGet("export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtDeviceExtendExportRequest query)
        {
            var result = await _deviceExtendService.ExportAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 更新设备信息
        /// </summary>
        /// <param name="request">更新请求</param>
        /// <returns>更新后的信息</returns>
        [HttpPut("device")]
        public async Task<IActionResult> UpdateDeviceInfoAsync([FromBody] HbtDeviceExtendUpdateRequest request)
        {
            var result = await _deviceExtendService.UpdateDeviceInfoAsync(request);
            return Success(result);
        }

        /// <summary>
        /// 更新设备离线信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deviceId">设备ID</param>
        /// <returns>更新后的信息</returns>
        [HttpPut("{userId}/{deviceId}/offline")]
        public async Task<IActionResult> UpdateOfflineInfoAsync(long userId, string deviceId)
        {
            var result = await _deviceExtendService.UpdateOfflineInfoAsync(userId, deviceId);
            return Success(result);
        }

        /// <summary>
        /// 更新设备在线时段
        /// </summary>
        /// <param name="request">在线时段更新请求</param>
        /// <returns>更新后的信息</returns>
        [HttpPut("online-period")]
        public async Task<IActionResult> UpdateOnlinePeriodAsync([FromBody] HbtDeviceOnlinePeriodUpdateRequest request)
        {
            var result = await _deviceExtendService.UpdateOnlinePeriodAsync(request);
            return Success(result);
        }

        /// <summary>
        /// 获取用户的设备扩展信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deviceId">设备ID</param>
        /// <returns>设备扩展信息</returns>
        [HttpGet("{userId}/{deviceId}")]
        public async Task<IActionResult> GetByUserIdAndDeviceIdAsync(long userId, string deviceId)
        {
            var result = await _deviceExtendService.GetByUserIdAndDeviceIdAsync(userId, deviceId);
            if (result == null)
                return NotFound();
            return Success(result);
        }

        /// <summary>
        /// 获取用户的所有设备扩展信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>设备扩展信息列表</returns>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserIdAsync(long userId)
        {
            var result = await _deviceExtendService.GetByUserIdAsync(userId);
            return Success(result);
        }
    }
} 