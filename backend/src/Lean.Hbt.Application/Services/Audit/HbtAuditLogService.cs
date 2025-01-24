//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtAuditLogService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 审计日志服务实现
//===================================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.Audit;
using Lean.Hbt.Application.Dtos.Audit;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Domain.Repositories;
using SqlSugar;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Lean.Hbt.Application.Services.Audit
{
    /// <summary>
    /// 审计日志服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtAuditLogService : IHbtAuditsLogService
    {
        private readonly ILogger<HbtAuditLogService> _logger;
        private readonly IHbtRepository<HbtAuditLog> _auditLogRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="auditLogRepository">审计日志仓储</param>
        public HbtAuditLogService(
            ILogger<HbtAuditLogService> logger,
            IHbtRepository<HbtAuditLog> auditLogRepository)
        {
            _logger = logger;
            _auditLogRepository = auditLogRepository;
        }

        /// <summary>
        /// 获取审计日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtAuditLogDto>> GetPagedListAsync(HbtAuditLogQueryDto query)
        {
            var exp = Expressionable.Create<HbtAuditLog>();

            if (!string.IsNullOrEmpty(query.UserName))
                exp.And(x => x.UserName.Contains(query.UserName));

            if (!string.IsNullOrEmpty(query.Module))
                exp.And(x => x.Module.Contains(query.Module));

            if (!string.IsNullOrEmpty(query.Operation))
                exp.And(x => x.Operation.Contains(query.Operation));

            if (query.StartTime.HasValue)
                exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query.EndTime.HasValue)
                exp.And(x => x.CreateTime <= query.EndTime.Value);

            var result = await _auditLogRepository.GetPagedListAsync(exp.ToExpression(), query.PageIndex, query.PageSize);

            return new HbtPagedResult<HbtAuditLogDto>
            {
                TotalNum = result.total,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.list.Adapt<List<HbtAuditLogDto>>()
            };
        }

        /// <summary>
        /// 获取审计日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>返回审计日志详情</returns>
        public async Task<HbtAuditLogDto> GetAsync(long logId)
        {
            var log = await _auditLogRepository.GetByIdAsync(logId);
            if (log == null)
                throw new HbtException($"审计日志不存在: {logId}");

            return log.Adapt<HbtAuditLogDto>();
        }

        /// <summary>
        /// 导出审计日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件字节数组</returns>
        public async Task<byte[]> ExportAsync(HbtAuditLogQueryDto query, string sheetName)
        {
            // 1.构建查询条件
            var predicate = Expressionable.Create<HbtAuditLog>();

            if (!string.IsNullOrEmpty(query.UserName))
                predicate.And(x => x.UserName.Contains(query.UserName));

            if (!string.IsNullOrEmpty(query.Module))
                predicate.And(x => x.Module.Contains(query.Module));

            if (!string.IsNullOrEmpty(query.Operation))
                predicate.And(x => x.Operation.Contains(query.Operation));

            if (query.StartTime.HasValue)
                predicate.And(x => x.CreateTime >= query.StartTime.Value);

            if (query.EndTime.HasValue)
                predicate.And(x => x.CreateTime <= query.EndTime.Value);

            // 2.查询数据
            var logs = await _auditLogRepository.AsQueryable()
                .Where(predicate.ToExpression())
                .OrderByDescending(x => x.CreateTime)
                .ToListAsync();

            // 3.转换并导出
            var exportDtos = logs.Adapt<List<HbtAuditLogExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 清空审计日志
        /// </summary>
        /// <returns>返回是否清空成功</returns>
        public async Task<bool> ClearAsync()
        {
            var result = await _auditLogRepository.DeleteAsync((Expression<Func<HbtAuditLog, bool>>)(x => true));
            return result > 0;
        }
    }
} 