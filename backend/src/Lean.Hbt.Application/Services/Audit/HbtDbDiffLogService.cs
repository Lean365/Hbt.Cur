//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbDiffLogService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 数据库差异日志服务实现
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
    /// 数据库差异日志服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtDbDiffLogService : IHbtDbDiffLogService
    {
        private readonly ILogger<HbtDbDiffLogService> _logger;
        private readonly IHbtRepository<HbtDbDiffLog> _dbDiffLogRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="dbDiffLogRepository">数据库差异日志仓储</param>
        public HbtDbDiffLogService(
            ILogger<HbtDbDiffLogService> logger,
            IHbtRepository<HbtDbDiffLog> dbDiffLogRepository)
        {
            _logger = logger;
            _dbDiffLogRepository = dbDiffLogRepository;
        }

        /// <summary>
        /// 获取数据库差异日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtDbDiffLogDto>> GetListAsync(HbtDbDiffLogQueryDto query)
        {
            var exp = Expressionable.Create<HbtDbDiffLog>();

            if (!string.IsNullOrEmpty(query?.TableName))
                exp.And(x => x.TableName.Contains(query.TableName));

            if (!string.IsNullOrEmpty(query?.ChangeType))
                exp.And(x => x.ChangeType.Contains(query.ChangeType));

            if (!string.IsNullOrEmpty(query?.ColumnName))
                exp.And(x => x.ColumnName.Contains(query.ColumnName));

            if (query?.StartTime.HasValue == true)
                exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query?.EndTime.HasValue == true)
                exp.And(x => x.CreateTime <= query.EndTime.Value);

            
            // 执行分页查询
            var result = await _dbDiffLogRepository.GetPagedListAsync(
                exp.ToExpression(),
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            // 返回分页结果
            return new HbtPagedResult<HbtDbDiffLogDto>
            {
                Rows = result.Rows.Adapt<List<HbtDbDiffLogDto>>(),
                TotalNum = result.TotalNum,
                PageIndex = result.PageIndex,
                PageSize = result.PageSize
            };
        }

        /// <summary>
        /// 获取数据库差异日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>返回数据库差异日志详情</returns>
        public async Task<HbtDbDiffLogDto> GetByIdAsync(long logId)
        {
            var log = await _dbDiffLogRepository.GetByIdAsync(logId);
            if (log == null)
                throw new HbtException($"数据库差异日志不存在: {logId}");

            return log.Adapt<HbtDbDiffLogDto>();
        }

        /// <summary>
        /// 导出数据库差异日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件字节数组</returns>
        public async Task<byte[]> ExportAsync(HbtDbDiffLogQueryDto query, string sheetName)
        {
            // 1.构建查询条件
            var predicate = Expressionable.Create<HbtDbDiffLog>();

            if (!string.IsNullOrEmpty(query?.TableName))
                predicate.And(x => x.TableName.Contains(query.TableName));

            if (!string.IsNullOrEmpty(query?.ChangeType))
                predicate.And(x => x.ChangeType.Contains(query.ChangeType));

            if (!string.IsNullOrEmpty(query?.ColumnName))
                predicate.And(x => x.ColumnName.Contains(query.ColumnName));

            if (query?.StartTime.HasValue == true)
                predicate.And(x => x.CreateTime >= query.StartTime.Value);

            if (query?.EndTime.HasValue == true)
                predicate.And(x => x.CreateTime <= query.EndTime.Value);

            // 2.查询数据
            var logs = await _dbDiffLogRepository.AsQueryable()
                .Where(predicate.ToExpression())
                .OrderByDescending(x => x.CreateTime)
                .ToListAsync();

            // 3.转换并导出
            var exportDtos = logs.Adapt<List<HbtDbDiffLogExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 清空数据库差异日志
        /// </summary>
        /// <returns>返回是否清空成功</returns>
        public async Task<bool> ClearAsync()
        {
            var result = await _dbDiffLogRepository.DeleteAsync((Expression<Func<HbtDbDiffLog, bool>>)(x => true));
            return result > 0;
        }
    }
} 