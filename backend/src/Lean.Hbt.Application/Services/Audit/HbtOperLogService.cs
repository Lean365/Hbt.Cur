//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOperLogService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 操作日志服务实现
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
    /// 操作日志服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtOperLogService : IHbtOperLogService
    {
        private readonly ILogger<HbtOperLogService> _logger;
        private readonly IHbtRepository<HbtOperLog> _operLogRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="operLogRepository">操作日志仓储</param>
        public HbtOperLogService(
            ILogger<HbtOperLogService> logger,
            IHbtRepository<HbtOperLog> operLogRepository)
        {
            _logger = logger;
            _operLogRepository = operLogRepository;
        }

        /// <summary>
        /// 获取操作日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtOperLogDto>> GetPagedListAsync(HbtOperLogQueryDto query)
        {
            var exp = Expressionable.Create<HbtOperLog>();

            if (!string.IsNullOrEmpty(query.UserName))
                exp.And(x => x.UserName.Contains(query.UserName));

            if (!string.IsNullOrEmpty(query.OperationType))
                exp.And(x => x.OperationType.Contains(query.OperationType));

            if (!string.IsNullOrEmpty(query.TableName))
                exp.And(x => x.TableName.Contains(query.TableName));

            if (!string.IsNullOrEmpty(query.IpAddress))
                exp.And(x => x.IpAddress.Contains(query.IpAddress));

            if (query.Status.HasValue)
                exp.And(x => x.Status == query.Status.Value);

            if (query.StartTime.HasValue)
                exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query.EndTime.HasValue)
                exp.And(x => x.CreateTime <= query.EndTime.Value);

            var result = await _operLogRepository.GetPagedListAsync(exp.ToExpression(), query.PageIndex, query.PageSize);

            return new HbtPagedResult<HbtOperLogDto>
            {
                TotalNum = result.total,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.list.Adapt<List<HbtOperLogDto>>()
            };
        }

        /// <summary>
        /// 获取操作日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>返回操作日志详情</returns>
        public async Task<HbtOperLogDto> GetAsync(long logId)
        {
            var log = await _operLogRepository.GetByIdAsync(logId);
            if (log == null)
                throw new HbtBusinessException($"操作日志不存在: {logId}");

            return log.Adapt<HbtOperLogDto>();
        }

        /// <summary>
        /// 导出操作日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回导出的Excel文件字节数组</returns>
        public async Task<byte[]> ExportAsync(HbtOperLogQueryDto query)
        {
            // 1.构建查询条件
            var predicate = Expressionable.Create<HbtOperLog>();

            if (!string.IsNullOrEmpty(query.UserName))
                predicate.And(x => x.UserName.Contains(query.UserName));

            if (!string.IsNullOrEmpty(query.OperationType))
                predicate.And(x => x.OperationType.Contains(query.OperationType));

            if (!string.IsNullOrEmpty(query.TableName))
                predicate.And(x => x.TableName.Contains(query.TableName));

            if (!string.IsNullOrEmpty(query.IpAddress))
                predicate.And(x => x.IpAddress.Contains(query.IpAddress));

            if (query.Status.HasValue)
                predicate.And(x => x.Status == query.Status.Value);

            if (query.StartTime.HasValue)
                predicate.And(x => x.CreateTime >= query.StartTime.Value);

            if (query.EndTime.HasValue)
                predicate.And(x => x.CreateTime <= query.EndTime.Value);

            // 2.查询数据
            var logs = await _operLogRepository.AsQueryable()
                .Where(predicate.ToExpression())
                .OrderByDescending(x => x.CreateTime)
                .ToListAsync();

            // 3.转换并导出
            var exportDtos = logs.Adapt<List<HbtOperLogExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportDtos, "操作日志数据");
        }

        /// <summary>
        /// 清空操作日志
        /// </summary>
        /// <returns>返回是否清空成功</returns>
        public async Task<bool> ClearAsync()
        {
            var result = await _operLogRepository.DeleteAsync((Expression<Func<HbtOperLog, bool>>)(x => true));
            return result > 0;
        }
    }
} 