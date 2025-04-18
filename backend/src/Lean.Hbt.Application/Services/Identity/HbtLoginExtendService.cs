#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginExtendService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V1.0.0
// 描述    : 登录扩展信息服务实现
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
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Helpers;
using static Lean.Hbt.Common.Helpers.HbtExcelHelper;
using static Lean.Hbt.Common.Extensions.HbtExtensions;

namespace Lean.Hbt.Application.Services.Identity
{
    /// <summary>
    /// 登录扩展信息服务实现
    /// </summary>
    public class HbtLoginExtendService : IHbtLoginExtendService
    {
        private readonly IHbtRepository<HbtLoginExtend> _loginExtendRepository;
        private readonly IHbtLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtLoginExtendService(
            IHbtRepository<HbtLoginExtend> loginExtendRepository,
            IHbtLogger logger)
        {
            _loginExtendRepository = loginExtendRepository;
            _logger = logger;
        }

        /// <summary>
        /// 获取登录扩展信息分页列表
        /// </summary>
        public async Task<HbtPagedResult<HbtLoginExtendDto>> GetListAsync(HbtLoginExtendQueryDto query)
        {
            var exp = Expressionable.Create<HbtLoginExtend>();

            if (query.UserId.HasValue)
                exp.And(x => x.UserId == query.UserId.Value);

            if (query.TenantId.HasValue)
                exp.And(x => x.TenantId == query.TenantId.Value);

            if (query.LoginType.HasValue)
                exp.And(x => x.LoginType == query.LoginType.Value);

            if (query.LoginSource.HasValue)
                exp.And(x => x.LoginSource == query.LoginSource.Value);

            if (query.LoginStatus.HasValue)
                exp.And(x => x.LoginStatus == query.LoginStatus.Value);

            if (query.LastLoginTimeStart.HasValue)
                exp.And(x => x.LastLoginTime >= query.LastLoginTimeStart.Value);

            if (query.LastLoginTimeEnd.HasValue)
                exp.And(x => x.LastLoginTime <= query.LastLoginTimeEnd.Value);

            var result = await _loginExtendRepository.GetPagedListAsync(
                exp.ToExpression(),
                query.PageIndex,
                query.PageSize,
                x => x.LastLoginTime,
                OrderByType.Desc);

            return new HbtPagedResult<HbtLoginExtendDto>
            {
                Rows = result.Rows.Adapt<List<HbtLoginExtendDto>>(),
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize
            };
        }

        /// <summary>
        /// 导出登录扩展信息
        /// </summary>
        public async Task<byte[]> ExportAsync(IEnumerable<HbtLoginExtendDto> data, string sheetName = "登录扩展信息")
        {
            return await HbtExcelHelper.ExportAsync(data, sheetName);
        }

        /// <summary>
        /// 更新用户登录信息
        /// </summary>
        public async Task<HbtLoginExtendDto> UpdateLoginInfoAsync(HbtLoginExtendUpdateDto request)
        {
            var loginExtend = await _loginExtendRepository.GetFirstAsync(x => x.UserId == request.UserId);
            var now = DateTime.Now;

            if (loginExtend == null)
            {
                // 首次登录,创建记录
                loginExtend = new HbtLoginExtend
                {
                    UserId = (long)request.UserId!,
                    TenantId = request.TenantId ?? 0,
                    RoleId = request.RoleId,
                    DeptId = request.DeptId,
                    PostId = request.PostId,
                    LoginType = request.LoginType,
                    LoginSource = request.LoginSource,
                    LoginStatus = 1, // 1 表示在线状态
                    FirstLoginTime = now,
                    FirstLoginIp = request.IpAddress,
                    FirstLoginLocation = request.Location,
                    FirstLoginDeviceId = request.DeviceId,
                    FirstLoginDeviceType = request.DeviceType,
                    FirstLoginBrowser = request.BrowserType,
                    FirstLoginOs = request.OsType,
                    LastLoginTime = now,
                    LastLoginIp = request.IpAddress,
                    LastLoginLocation = request.Location,
                    LastLoginDeviceId = request.DeviceId,
                    LastLoginDeviceType = request.DeviceType,
                    LastLoginBrowser = request.BrowserType,
                    LastLoginOs = request.OsType,
                    LoginCount = 1
                };

                await _loginExtendRepository.CreateAsync(loginExtend);
            }
            else
            {
                // 更新最后登录信息
                loginExtend.TenantId = request.TenantId ?? 0;
                loginExtend.RoleId = request.RoleId;
                loginExtend.DeptId = request.DeptId;
                loginExtend.PostId = request.PostId;
                loginExtend.LoginType = request.LoginType;
                loginExtend.LoginSource = request.LoginSource;
                loginExtend.LoginStatus = 1; // 1 表示在线状态
                loginExtend.LastLoginTime = now;
                loginExtend.LastLoginIp = request.IpAddress;
                loginExtend.LastLoginLocation = request.Location;
                loginExtend.LastLoginDeviceId = request.DeviceId;
                loginExtend.LastLoginDeviceType = request.DeviceType;
                loginExtend.LastLoginBrowser = request.BrowserType;
                loginExtend.LastLoginOs = request.OsType;
                loginExtend.LoginCount++;

                await _loginExtendRepository.UpdateAsync(loginExtend);
            }

            return loginExtend.Adapt<HbtLoginExtendDto>();
        }

        /// <summary>
        /// 更新用户离线信息
        /// </summary>
        public async Task<HbtLoginExtendDto> UpdateOfflineInfoAsync(long userId)
        {
            var loginExtend = await _loginExtendRepository.GetFirstAsync(x => x.UserId == userId);
            if (loginExtend == null)
            {
                throw new InvalidOperationException($"用户{userId}的登录扩展信息不存在");
            }

            loginExtend.LoginStatus = 0; // 0 表示离线状态
            loginExtend.LastOfflineTime = DateTime.Now;

            await _loginExtendRepository.UpdateAsync(loginExtend);
            return loginExtend.Adapt<HbtLoginExtendDto>();
        }

        /// <summary>
        /// 更新用户在线时段
        /// </summary>
        public async Task<HbtLoginExtendDto> UpdateOnlinePeriodAsync(HbtLoginExtendOnlinePeriodUpdateDto request)
        {
            var loginExtend = await _loginExtendRepository.GetFirstAsync(x => x.UserId == request.UserId);
            if (loginExtend == null)
            {
                throw new InvalidOperationException($"用户{request.UserId}的登录扩展信息不存在");
            }

            var periods = loginExtend.TodayOnlinePeriods.FromJson<List<string>>() ?? new List<string>();
            periods.Add(request.OnlinePeriod);
            loginExtend.TodayOnlinePeriods = periods.ToJson();

            await _loginExtendRepository.UpdateAsync(loginExtend);
            return loginExtend.Adapt<HbtLoginExtendDto>();
        }

        /// <summary>
        /// 获取用户登录扩展信息
        /// </summary>
        public async Task<HbtLoginExtendDto?> GetByUserIdAsync(long userId)
        {
            var loginExtend = await _loginExtendRepository.GetFirstAsync(x => x.UserId == userId);
            return loginExtend?.Adapt<HbtLoginExtendDto>();
        }

        /// <summary>
        /// 清除所有用户的会话信息
        /// </summary>
        public async Task<bool> ClearAllUserSessionsAsync()
        {
            try
            {
                var now = DateTime.Now;
                var onlineUsers = await _loginExtendRepository.GetListAsync(x => x.LoginStatus == 1);
                
                foreach (var user in onlineUsers)
                {
                    user.LoginStatus = 0; // 设置为离线状态
                    user.LastOfflineTime = now;
                }

                if (onlineUsers.Any())
                {
                    await _loginExtendRepository.UpdateRangeAsync(onlineUsers);
                    _logger.Info($"已清除 {onlineUsers.Count} 个用户的在线状态");
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("清除用户会话信息时发生错误", ex);
                return false;
            }
        }
    }
} 