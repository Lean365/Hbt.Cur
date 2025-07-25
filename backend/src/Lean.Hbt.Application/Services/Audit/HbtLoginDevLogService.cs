#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginDevLogService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 登录设备日志服务实现
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Common.Enums;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Audit
{
    /// <summary>
    /// 登录设备服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtLoginDevLogService : HbtBaseService, IHbtLoginDevLogService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly IHbtRepositoryFactory _repositoryFactory;
        private IHbtRepository<HbtLoginDevLog> DeviceExtendRepository => _repositoryFactory.GetAuthRepository<HbtLoginDevLog>();

        /// <summary>
        /// 构造函数，注入依赖服务
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtLoginDevLogService(
            IHbtLogger logger,
            IHbtRepositoryFactory repositoryFactory,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// 获取登录设备日志分页列表
        /// </summary>
        public async Task<HbtPagedResult<HbtLoginDevLogDto>> GetListAsync(HbtLoginDevLogQueryDto query)
        {
            var exp = QueryExpression(query);

            var result = await DeviceExtendRepository.GetPagedListAsync(
                exp,
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Asc);

            return new HbtPagedResult<HbtLoginDevLogDto>
            {
                Rows = result.Rows.Adapt<List<HbtLoginDevLogDto>>(),
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize
            };
        }

        /// <summary>
        /// 获取登录设备详情
        /// </summary>
        /// <param name="deviceId">设备ID</param>
        /// <returns>返回登录设备详细信息</returns>
        /// <exception cref="HbtException">当登录设备不存在时抛出异常</exception>
        public async Task<HbtLoginDevLogDto> GetByIdAsync(long deviceId)
        {
            var deviceExtend = await DeviceExtendRepository.GetByIdAsync(deviceId);
            if (deviceExtend == null)
                throw new HbtException(L("Identity.DeviceExtend.NotFound", deviceId));

            return deviceExtend.Adapt<HbtLoginDevLogDto>();
        }

        /// <summary>
        /// 创建登录设备
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>设备ID</returns>
        public async Task<string> CreateAsync(HbtLoginDevLogCreateDto input)
        {
            // 验证设备ID是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(DeviceExtendRepository, "DeviceId", input.DeviceId);

            var deviceExtend = input.Adapt<HbtLoginDevLog>();
            return await DeviceExtendRepository.CreateAsync(deviceExtend) > 0 ? deviceExtend.DeviceId : throw new HbtException(L("Identity.DeviceExtend.CreateFailed"));
        }

        /// <summary>
        /// 更新登录设备
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(HbtLoginDevLogUpdateDto input)
        {
            var deviceExtend = await DeviceExtendRepository.GetByIdAsync(input.DeviceId)
                ?? throw new HbtException(L("Identity.DeviceExtend.NotFound", input.DeviceId));

            input.Adapt(deviceExtend);
            return await DeviceExtendRepository.UpdateAsync(deviceExtend) > 0;
        }

        /// <summary>
        /// 更新设备在线时段
        /// </summary>
        public async Task<HbtLoginDevLogDto> UpdateOnlinePeriodAsync(HbtDeviceOnlinePeriodUpdateDto request)
        {
            var deviceExtend = await DeviceExtendRepository.GetFirstAsync(
                x => x.UserId == request.UserId && x.DeviceId == request.DeviceId);
            if (deviceExtend == null)
            {
                throw new InvalidOperationException($"用户{request.UserId}的设备{request.DeviceId}扩展信息不存在");
            }

            deviceExtend.TodayOnlinePeriods = request.OnlinePeriod;
            await DeviceExtendRepository.UpdateAsync(deviceExtend);

            return deviceExtend.Adapt<HbtLoginDevLogDto>();
        }

        /// <summary>
        /// 导出登录设备数据
        /// </summary>
        /// <param name="data">要导出的数据</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(IEnumerable<HbtLoginDevLogDto> data, string sheetName = "HbtLoginDevLog")
        {
            return await HbtExcelHelper.ExportAsync(data, sheetName, "登录设备数据");
        }

        /// <summary>
        /// 更新登录设备状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(HbtLoginDevLogStatusDto input)
        {
            var deviceExtend = await DeviceExtendRepository.GetByIdAsync(input.DeviceId)
                ?? throw new HbtException(L("Identity.DeviceExtend.NotFound", input.DeviceId));

            input.Adapt(deviceExtend);
            return await DeviceExtendRepository.UpdateAsync(deviceExtend) > 0;
        }

        /// <summary>
        /// 删除登录设备
        /// </summary>
        /// <param name="deviceId">设备ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long deviceId)
        {
            var deviceExtend = await DeviceExtendRepository.GetByIdAsync(deviceId)
                ?? throw new HbtException(L("Identity.DeviceExtend.NotFound", deviceId));

            return await DeviceExtendRepository.DeleteAsync(deviceId) > 0;
        }

        /// <summary>
        /// 批量删除登录设备
        /// </summary>
        /// <param name="deviceIds">设备ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] deviceIds)
        {
            if (deviceIds == null || deviceIds.Length == 0)
                throw new HbtException(L("Identity.DeviceExtend.SelectRequired"));

            return await DeviceExtendRepository.DeleteRangeAsync(deviceIds.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 更新设备信息
        /// </summary>
        public async Task<HbtLoginDevLogDto> UpdateDeviceInfoAsync(HbtLoginDevLogUpdateDto request)
        {
            var now = DateTime.Now;
            var deviceExtend = await DeviceExtendRepository.GetFirstAsync(x =>
                x.UserId == request.UserId &&
                x.DeviceId == request.DeviceId);

            if (deviceExtend == null)
            {
                deviceExtend = new HbtLoginDevLog
                {
                    UserId = request.UserId,
                    DeviceId = request.DeviceId,
                    DeviceName = request.DeviceName,
                    DeviceType = request.DeviceType,
                    DeviceStatus = (int)HbtDeviceStatus.Online,
                    LastOnlineTime = now
                };

                await DeviceExtendRepository.CreateAsync(deviceExtend);
            }
            else
            {
                deviceExtend.DeviceName = request.DeviceName;
                deviceExtend.DeviceType = request.DeviceType;
                deviceExtend.DeviceStatus = (int)HbtDeviceStatus.Online;
                deviceExtend.LastOnlineTime = now;

                await DeviceExtendRepository.UpdateAsync(deviceExtend);
            }

            return deviceExtend.Adapt<HbtLoginDevLogDto>();
        }

        /// <summary>
        /// 更新设备离线信息
        /// </summary>
        public async Task<HbtLoginDevLogDto> UpdateOfflineInfoAsync(long userId, string deviceId)
        {
            var deviceExtend = await DeviceExtendRepository.GetFirstAsync(x =>
                x.UserId == userId &&
                x.DeviceId == deviceId);

            if (deviceExtend == null)
            {
                _logger.Warn($"登录设备日志不存在: userId={userId}, deviceId={deviceId}");
                throw new InvalidOperationException($"登录设备日志不存在: userId={userId}, deviceId={deviceId}");
            }

            deviceExtend.DeviceStatus = (int)HbtDeviceStatus.Offline;
            deviceExtend.LastOfflineTime = DateTime.Now;

            await DeviceExtendRepository.UpdateAsync(deviceExtend);
            return deviceExtend.Adapt<HbtLoginDevLogDto>();
        }

        /// <summary>
        /// 获取用户的登录设备日志
        /// </summary>
        public async Task<HbtLoginDevLogDto?> GetByUserIdAndDeviceIdAsync(long userId, string deviceId)
        {
            var deviceExtend = await DeviceExtendRepository.GetFirstAsync(
                x => x.UserId == userId && x.DeviceId == deviceId);
            return deviceExtend?.Adapt<HbtLoginDevLogDto>();
        }

        /// <summary>
        /// 获取用户的所有登录设备日志
        /// </summary>
        public async Task<List<HbtLoginDevLogDto>> GetByUserIdAsync(long userId)
        {
            var deviceExtends = await DeviceExtendRepository.GetListAsync(x => x.UserId == userId);
            return deviceExtends.Adapt<List<HbtLoginDevLogDto>>();
        }

        /// <summary>
        /// 构建登录设备日志查询条件
        /// </summary>
        private Expression<Func<HbtLoginDevLog, bool>> QueryExpression(HbtLoginDevLogQueryDto query)
        {
            var exp = Expressionable.Create<HbtLoginDevLog>();

            if (query.UserId.HasValue)
                exp.And(x => x.UserId == query.UserId.Value);


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