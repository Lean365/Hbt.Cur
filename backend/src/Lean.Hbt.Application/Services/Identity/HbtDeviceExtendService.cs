#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDeviceExtendService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V1.0.0
// 描述    : 设备扩展信息服务实现
//===================================================================

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Mapster;
using SqlSugar;
using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Common.Extensions;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Common.Helpers;
using System.Linq;
using Microsoft.Extensions.Logging;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Utils;
using System.Linq.Expressions;
using Lean.Hbt.Domain.IServices.Extensions;
using Lean.Hbt.Domain.Utils;
using Lean.Hbt.Domain.IServices.Extensions;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Lean.Hbt.Application.Services.Identity
{
    /// <summary>
    /// 设备扩展服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtDeviceExtendService : HbtBaseService, IHbtDeviceExtendService
    {
        // 设备扩展仓储接口
        private readonly IHbtRepository<HbtDeviceExtend> _deviceExtendRepository;

        /// <summary>
        /// 构造函数，注入依赖服务
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="deviceExtendRepository">设备扩展仓库</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtDeviceExtendService(
            IHbtLogger logger,
            IHbtRepository<HbtDeviceExtend> deviceExtendRepository,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _deviceExtendRepository = deviceExtendRepository;
        }

        /// <summary>
        /// 获取设备扩展信息分页列表
        /// </summary>
        public async Task<HbtPagedResult<HbtDeviceExtendDto>> GetListAsync(HbtDeviceExtendQueryDto query)
        {
            var exp = HbtDeviceExtendQueryExpression(query);

            var result = await _deviceExtendRepository.GetPagedListAsync(
                exp,
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            return new HbtPagedResult<HbtDeviceExtendDto>
            {
                Rows = result.Rows.Adapt<List<HbtDeviceExtendDto>>(),
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize
            };
        }

        /// <summary>
        /// 获取设备扩展详情
        /// </summary>
        /// <param name="deviceId">设备ID</param>
        /// <returns>返回设备扩展详细信息</returns>
        /// <exception cref="HbtException">当设备扩展不存在时抛出异常</exception>
        public async Task<HbtDeviceExtendDto> GetByIdAsync(long deviceId)
        {
            var deviceExtend = await _deviceExtendRepository.GetByIdAsync(deviceId);
            if (deviceExtend == null)
                throw new HbtException(L("Identity.DeviceExtend.NotFound", deviceId));

            return deviceExtend.Adapt<HbtDeviceExtendDto>();
        }

        /// <summary>
        /// 创建设备扩展
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>设备ID</returns>
        public async Task<string> CreateAsync(HbtDeviceExtendCreateDto input)
        {
            // 验证设备ID是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(_deviceExtendRepository, "DeviceId", input.DeviceId);

            var deviceExtend = input.Adapt<HbtDeviceExtend>();
            return await _deviceExtendRepository.CreateAsync(deviceExtend) > 0 ? deviceExtend.DeviceId : throw new HbtException(L("Identity.DeviceExtend.CreateFailed"));
        }

        /// <summary>
        /// 更新设备扩展
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(HbtDeviceExtendUpdateDto input)
        {
            var deviceExtend = await _deviceExtendRepository.GetByIdAsync(input.DeviceId)
                ?? throw new HbtException(L("Identity.DeviceExtend.NotFound", input.DeviceId));

            input.Adapt(deviceExtend);
            return await _deviceExtendRepository.UpdateAsync(deviceExtend) > 0;
        }

        /// <summary>
        /// 更新设备在线时段
        /// </summary>
        public async Task<HbtDeviceExtendDto> UpdateOnlinePeriodAsync(HbtDeviceOnlinePeriodUpdateDto request)
        {
            var deviceExtend = await _deviceExtendRepository.GetFirstAsync(
                x => x.UserId == request.UserId && x.DeviceId == request.DeviceId);
            if (deviceExtend == null)
            {
                throw new InvalidOperationException($"用户{request.UserId}的设备{request.DeviceId}扩展信息不存在");
            }

            deviceExtend.TodayOnlinePeriods = request.OnlinePeriod;
            await _deviceExtendRepository.UpdateAsync(deviceExtend);

            return deviceExtend.Adapt<HbtDeviceExtendDto>();
        }

        /// <summary>
        /// 导出设备扩展数据
        /// </summary>
        /// <param name="data">要导出的数据</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(IEnumerable<HbtDeviceExtendDto> data, string sheetName = "HbtDeviceExtend")
        {
            return await HbtExcelHelper.ExportAsync(data, sheetName, "设备扩展数据");
        }

        /// <summary>
        /// 更新设备扩展状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(HbtDeviceExtendStatusDto input)
        {
            var deviceExtend = await _deviceExtendRepository.GetByIdAsync(input.DeviceId)
                ?? throw new HbtException(L("Identity.DeviceExtend.NotFound", input.DeviceId));

            input.Adapt(deviceExtend);
            return await _deviceExtendRepository.UpdateAsync(deviceExtend) > 0;
        }

        /// <summary>
        /// 删除设备扩展
        /// </summary>
        /// <param name="deviceId">设备ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long deviceId)
        {
            var deviceExtend = await _deviceExtendRepository.GetByIdAsync(deviceId)
                ?? throw new HbtException(L("Identity.DeviceExtend.NotFound", deviceId));

            return await _deviceExtendRepository.DeleteAsync(deviceId) > 0;
        }

        /// <summary>
        /// 批量删除设备扩展
        /// </summary>
        /// <param name="deviceIds">设备ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] deviceIds)
        {
            if (deviceIds == null || deviceIds.Length == 0)
                throw new HbtException(L("Identity.DeviceExtend.SelectRequired"));

            return await _deviceExtendRepository.DeleteRangeAsync(deviceIds.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 更新设备信息
        /// </summary>
        public async Task<HbtDeviceExtendDto> UpdateDeviceInfoAsync(HbtDeviceExtendUpdateDto request)
        {
            var now = DateTime.Now;
            var deviceExtend = await _deviceExtendRepository.GetFirstAsync(x =>
                x.UserId == request.UserId &&
                x.DeviceId == request.DeviceId);

            if (deviceExtend == null)
            {
                deviceExtend = new HbtDeviceExtend
                {
                    UserId = request.UserId,
                    TenantId = request.TenantId,
                    DeviceId = request.DeviceId,
                    DeviceName = request.DeviceName,
                    DeviceType = request.DeviceType,
                    BrowserType = request.BrowserType,
                    BrowserVersion = request.BrowserVersion,
                    Resolution = request.Resolution,
                    DeviceStatus = (int)HbtDeviceStatus.Online,
                    LastOnlineTime = now
                };

                await _deviceExtendRepository.CreateAsync(deviceExtend);
            }
            else
            {
                deviceExtend.DeviceName = request.DeviceName;
                deviceExtend.DeviceType = request.DeviceType;
                deviceExtend.BrowserType = request.BrowserType;
                deviceExtend.BrowserVersion = request.BrowserVersion;
                deviceExtend.Resolution = request.Resolution;
                deviceExtend.DeviceStatus = (int)HbtDeviceStatus.Online;
                deviceExtend.LastOnlineTime = now;

                await _deviceExtendRepository.UpdateAsync(deviceExtend);
            }

            return deviceExtend.Adapt<HbtDeviceExtendDto>();
        }

        /// <summary>
        /// 更新设备离线信息
        /// </summary>
        public async Task<HbtDeviceExtendDto> UpdateOfflineInfoAsync(long userId, string deviceId)
        {
            var deviceExtend = await _deviceExtendRepository.GetFirstAsync(x =>
                x.UserId == userId &&
                x.DeviceId == deviceId);

            if (deviceExtend == null)
            {
                _logger.Warn($"设备扩展信息不存在: userId={userId}, deviceId={deviceId}");
                throw new InvalidOperationException($"设备扩展信息不存在: userId={userId}, deviceId={deviceId}");
            }

            deviceExtend.DeviceStatus = (int)HbtDeviceStatus.Offline;
            deviceExtend.LastOfflineTime = DateTime.Now;

            await _deviceExtendRepository.UpdateAsync(deviceExtend);
            return deviceExtend.Adapt<HbtDeviceExtendDto>();
        }

        /// <summary>
        /// 获取用户的设备扩展信息
        /// </summary>
        public async Task<HbtDeviceExtendDto?> GetByUserIdAndDeviceIdAsync(long userId, string deviceId)
        {
            var deviceExtend = await _deviceExtendRepository.GetFirstAsync(
                x => x.UserId == userId && x.DeviceId == deviceId);
            return deviceExtend?.Adapt<HbtDeviceExtendDto>();
        }

        /// <summary>
        /// 获取用户的所有设备扩展信息
        /// </summary>
        public async Task<List<HbtDeviceExtendDto>> GetByUserIdAsync(long userId)
        {
            var deviceExtends = await _deviceExtendRepository.GetListAsync(x => x.UserId == userId);
            return deviceExtends.Adapt<List<HbtDeviceExtendDto>>();
        }

        /// <summary>
        /// 构建设备扩展信息查询条件
        /// </summary>
        private Expression<Func<HbtDeviceExtend, bool>> HbtDeviceExtendQueryExpression(HbtDeviceExtendQueryDto query)
        {
            var exp = Expressionable.Create<HbtDeviceExtend>();

            if (query.UserId.HasValue)
                exp.And(x => x.UserId == query.UserId.Value);

            if (query.TenantId.HasValue)
                exp.And(x => x.TenantId == query.TenantId.Value);

            if (query.DeviceType.HasValue)
                exp.And(x => x.DeviceType == query.DeviceType.Value);

            if (!string.IsNullOrEmpty(query.DeviceId))
                exp.And(x => x.DeviceId.Contains(query.DeviceId));

            if (!string.IsNullOrEmpty(query.DeviceName))
                exp.And(x => x.DeviceName.Contains(query.DeviceName));

            if (query.DeviceStatus.HasValue)
                exp.And(x => x.DeviceStatus == query.DeviceStatus.Value);

            if (query.LastOnlineTimeStart.HasValue)
                exp.And(x => x.LastOnlineTime >= query.LastOnlineTimeStart.Value);

            if (query.LastOnlineTimeEnd.HasValue)
                exp.And(x => x.LastOnlineTime <= query.LastOnlineTimeEnd.Value);

            return exp.ToExpression();
        }
    }
} 