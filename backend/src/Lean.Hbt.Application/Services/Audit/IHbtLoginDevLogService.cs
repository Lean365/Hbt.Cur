#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtLoginDevLogService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V1.0.0
// 描述    : 登录设备日志服务接口
//===================================================================

using System.Threading.Tasks;
using System.Collections.Generic;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Application.Dtos.Audit;

namespace Lean.Hbt.Application.Services.Audit
{
    /// <summary>
    /// 登录设备日志服务接口
    /// </summary>
    public interface IHbtLoginDevLogService
    {
        /// <summary>
        /// 获取登录设备日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页列表</returns>
        Task<HbtPagedResult<HbtLoginDevLogDto>> GetListAsync(HbtLoginDevLogQueryDto query);

        /// <summary>
        /// 导出登录设备日志
        /// </summary>
        /// <param name="data">要导出的数据</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(IEnumerable<HbtLoginDevLogDto> data, string sheetName = "登录设备日志");

        /// <summary>
        /// 更新设备信息
        /// </summary>
        /// <param name="request">更新请求</param>
        /// <returns>更新后的登录设备日志</returns>
        Task<HbtLoginDevLogDto> UpdateDeviceInfoAsync(HbtLoginDevLogUpdateDto request);

        /// <summary>
        /// 更新设备离线信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deviceId">设备ID</param>
        /// <returns>更新后的登录设备日志</returns>
        Task<HbtLoginDevLogDto> UpdateOfflineInfoAsync(long userId, string deviceId);

        /// <summary>
        /// 更新设备在线时段
        /// </summary>
        /// <param name="request">在线时段更新请求</param>
        /// <returns>更新后的登录设备日志</returns>
        Task<HbtLoginDevLogDto> UpdateOnlinePeriodAsync(HbtDeviceOnlinePeriodUpdateDto request);

        /// <summary>
        /// 获取用户的登录设备日志
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deviceId">设备ID</param>
        /// <returns>登录设备日志</returns>
        Task<HbtLoginDevLogDto?> GetByUserIdAndDeviceIdAsync(long userId, string deviceId);

        /// <summary>
        /// 获取用户的所有登录设备日志
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>登录设备日志列表</returns>
        Task<List<HbtLoginDevLogDto>> GetByUserIdAsync(long userId);
    }
} 