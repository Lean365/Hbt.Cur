//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginLogService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 登录日志服务实现
//===================================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.Audit;
using Lean.Hbt.Application.Dtos.Audit;
using Lean.Hbt.Infrastructure.Data;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Domain.Repositories;
using SqlSugar;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Lean.Hbt.Application.Services.Audit
{
    /// <summary>
    /// 登录日志服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtLoginLogService : IHbtLoginLogService
    {
        private readonly ILogger<HbtLoginLogService> _logger;
        private readonly IHbtRepository<HbtLoginLog> _loginLogRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="loginLogRepository">登录日志仓储</param>
        public HbtLoginLogService(
            ILogger<HbtLoginLogService> logger,
            IHbtRepository<HbtLoginLog> loginLogRepository)
        {
            _logger = logger;
            _loginLogRepository = loginLogRepository;
        }

        /// <summary>
        /// 获取登录日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtLoginLogDto>> GetPagedListAsync(HbtLoginLogQueryDto query)
        {
            var exp = Expressionable.Create<HbtLoginLog>();

            if (!string.IsNullOrEmpty(query.UserName))
                exp.And(x => x.UserName.Contains(query.UserName));

            if (!string.IsNullOrEmpty(query.IpAddress))
                exp.And(x => x.IpAddress.Contains(query.IpAddress));

            if (query.Success.HasValue)
                exp.And(x => x.Success == (query.Success.Value ? 1 : 0));

            if (query.StartTime.HasValue)
                exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query.EndTime.HasValue)
                exp.And(x => x.CreateTime <= query.EndTime.Value);

            var result = await _loginLogRepository.GetPagedListAsync(exp.ToExpression(), query.PageIndex, query.PageSize);

            return new HbtPagedResult<HbtLoginLogDto>
            {
                TotalNum = result.total,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.list.Adapt<List<HbtLoginLogDto>>()
            };
        }

        /// <summary>
        /// 获取登录日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>返回登录日志详情</returns>
        public async Task<HbtLoginLogDto> GetAsync(long logId)
        {
            var log = await _loginLogRepository.GetByIdAsync(logId);
            if (log == null)
                throw new HbtException($"登录日志不存在: {logId}");

            return log.Adapt<HbtLoginLogDto>();
        }

        /// <summary>
        /// 导出登录日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<byte[]> ExportAsync(HbtLoginLogQueryDto query, string sheetName)
        {
            try
            {
                // 1.构建查询条件
                var predicate = Expressionable.Create<HbtLoginLog>();

                if (!string.IsNullOrEmpty(query?.UserName))
                    predicate.And(x => x.UserName.Contains(query.UserName));

                if (!string.IsNullOrEmpty(query?.IpAddress))
                    predicate.And(x => x.IpAddress.Contains(query.IpAddress));

                if (query?.Success.HasValue == true)
                    predicate.And(x => x.Success == (query.Success.Value ? 1 : 0));

                if (query?.StartTime.HasValue == true)
                    predicate.And(x => x.CreateTime >= query.StartTime.Value);

                if (query?.EndTime.HasValue == true)
                    predicate.And(x => x.CreateTime <= query.EndTime.Value);

                // 2.查询数据
                var logs = await _loginLogRepository.AsQueryable()
                    .Where(predicate.ToExpression())
                    .OrderByDescending(x => x.CreateTime)
                    .ToListAsync();

                // 3.转换并导出
                var dtos = logs.Adapt<List<HbtLoginLogDto>>();
                return await HbtExcelHelper.ExportAsync(dtos, sheetName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "导出登录日志数据失败");
                return Array.Empty<byte>();
            }
        }

        /// <summary>
        /// 清空登录日志
        /// </summary>
        /// <returns>返回是否清空成功</returns>
        public async Task<bool> ClearAsync()
        {
            var result = await _loginLogRepository.DeleteAsync((Expression<Func<HbtLoginLog, bool>>)(x => true));
            return result > 0;
        }
    }
} 