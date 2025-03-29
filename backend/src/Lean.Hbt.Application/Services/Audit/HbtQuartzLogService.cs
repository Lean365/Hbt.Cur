//===================================================================
// 项目名 : Lean.Hbt.Application 
// 文件名 : HbtQuartzLogService.cs 
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 任务日志服务实现
//===================================================================

using System;
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
    /// 任务日志服务实现
    /// </summary>
    public class HbtQuartzLogService : IHbtQuartzLogService
    {
        private readonly ILogger<HbtQuartzLogService> _logger;
        private readonly IHbtRepository<HbtQuartzLog> _quartzLogRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="quartzLogRepository">任务日志仓储</param>
        public HbtQuartzLogService(
            ILogger<HbtQuartzLogService> logger,
            IHbtRepository<HbtQuartzLog> quartzLogRepository)
        {
            _logger = logger;
            _quartzLogRepository = quartzLogRepository;
        }

        /// <summary>
        /// 获取任务日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtQuartzLogDto>> GetPagedAsync(HbtQuartzLogQueryDto query)
        {
            var exp = Expressionable.Create<HbtQuartzLog>();

            if (!string.IsNullOrEmpty(query?.LogTaskName))
                exp.And(x => x.LogTaskName.Contains(query.LogTaskName));

            if (!string.IsNullOrEmpty(query?.LogGroupName))
                exp.And(x => x.LogGroupName.Contains(query.LogGroupName));

            if (query?.LogStatus.HasValue == true)
                exp.And(x => x.LogStatus == query.LogStatus.Value);

            if (query?.BeginTime.HasValue == true)
                exp.And(x => x.CreateTime >= query.BeginTime.Value);

            if (query?.EndTime.HasValue == true)
                exp.And(x => x.CreateTime <= query.EndTime.Value);


            // 执行分页查询
            var result = await _quartzLogRepository.GetPagedListAsync(
                exp.ToExpression(),
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            // 返回分页结果
            return new HbtPagedResult<HbtQuartzLogDto>
            {
                Rows = result.Rows.Adapt<List<HbtQuartzLogDto>>(),
                TotalNum = result.TotalNum,
                PageIndex = result.PageIndex,
                PageSize = result.PageSize
            };
        }

        /// <summary>
        /// 获取任务日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>返回任务日志详情</returns>
        public async Task<HbtQuartzLogDto> GetByIdAsync(long logId)
        {
            var log = await _quartzLogRepository.GetByIdAsync(logId);
            if (log == null)
                throw new HbtException($"任务日志不存在: {logId}");

            return log.Adapt<HbtQuartzLogDto>();
        }

        /// <summary>
        /// 导出任务日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件字节数组</returns>
        public async Task<byte[]> ExportAsync(HbtQuartzLogQueryDto query, string sheetName)
        {
            // 1.构建查询条件
            var predicate = Expressionable.Create<HbtQuartzLog>();

            if (!string.IsNullOrEmpty(query?.LogTaskName))
                predicate.And(x => x.LogTaskName.Contains(query.LogTaskName));

            if (!string.IsNullOrEmpty(query?.LogGroupName))
                predicate.And(x => x.LogGroupName.Contains(query.LogGroupName));

            if (query?.LogStatus.HasValue == true)
                predicate.And(x => x.LogStatus == query.LogStatus.Value);

            if (query?.BeginTime.HasValue == true)
                predicate.And(x => x.CreateTime >= query.BeginTime.Value);

            if (query?.EndTime.HasValue == true)
                predicate.And(x => x.CreateTime <= query.EndTime.Value);

            // 2.查询数据
            var logs = await _quartzLogRepository.AsQueryable()
                .Where(predicate.ToExpression())
                .OrderByDescending(x => x.CreateTime)
                .ToListAsync();

            // 3.转换并导出
            var exportDtos = logs.Adapt<List<HbtQuartzLogExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 清空任务日志
        /// </summary>
        /// <returns>返回是否清空成功</returns>
        public async Task<bool> ClearAsync()
        {
            var result = await _quartzLogRepository.DeleteAsync((Expression<Func<HbtQuartzLog, bool>>)(x => true));
            return result > 0;
        }
    }
} 