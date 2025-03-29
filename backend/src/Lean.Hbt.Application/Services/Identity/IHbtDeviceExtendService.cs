#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtDeviceExtendService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V1.0.0
// 描述    : 设备扩展信息服务接口
//===================================================================

using System.Threading.Tasks;
using System.Collections.Generic;
using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Services.Identity
{
    /// <summary>
    /// 设备扩展信息服务接口
    /// </summary>
    public interface IHbtDeviceExtendService
    {
        /// <summary>
        /// 获取设备扩展信息分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页列表</returns>
        Task<HbtPagedResult<HbtDeviceExtendDto>> GetListAsync(HbtDeviceExtendQueryDto query);

        /// <summary>
        /// 导出设备扩展信息
        /// </summary>
        /// <param name="data">要导出的数据</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<byte[]> ExportAsync(IEnumerable<HbtDeviceExtendDto> data, string sheetName = "设备扩展信息");

        /// <summary>
        /// 更新设备信息
        /// </summary>
        /// <param name="request">更新请求</param>
        /// <returns>更新后的设备扩展信息</returns>
        Task<HbtDeviceExtendDto> UpdateDeviceInfoAsync(HbtDeviceExtendUpdateDto request);

        /// <summary>
        /// 更新设备离线信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deviceId">设备ID</param>
        /// <returns>更新后的设备扩展信息</returns>
        Task<HbtDeviceExtendDto> UpdateOfflineInfoAsync(long userId, string deviceId);

        /// <summary>
        /// 更新设备在线时段
        /// </summary>
        /// <param name="request">在线时段更新请求</param>
        /// <returns>更新后的设备扩展信息</returns>
        Task<HbtDeviceExtendDto> UpdateOnlinePeriodAsync(HbtDeviceOnlinePeriodUpdateDto request);

        /// <summary>
        /// 获取用户的设备扩展信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deviceId">设备ID</param>
        /// <returns>设备扩展信息</returns>
        Task<HbtDeviceExtendDto?> GetByUserIdAndDeviceIdAsync(long userId, string deviceId);

        /// <summary>
        /// 获取用户的所有设备扩展信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>设备扩展信息列表</returns>
        Task<List<HbtDeviceExtendDto>> GetByUserIdAsync(long userId);
    }
} 