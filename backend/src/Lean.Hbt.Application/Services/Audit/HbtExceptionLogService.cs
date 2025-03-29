//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtExceptionLogService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 异常日志服务实现
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
    /// 异常日志服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtExceptionLogService : IHbtExceptionLogService
    {
        private readonly ILogger<HbtExceptionLogService> _logger;
        private readonly IHbtRepository<HbtExceptionLog> _exceptionLogRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="exceptionLogRepository">异常日志仓储</param>
        public HbtExceptionLogService(
            ILogger<HbtExceptionLogService> logger,
            IHbtRepository<HbtExceptionLog> exceptionLogRepository)
        {
            _logger = logger;
            _exceptionLogRepository = exceptionLogRepository;
        }

        /// <summary>
        /// 获取异常日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtExceptionLogDto>> GetListAsync(HbtExceptionLogQueryDto query)
        {
            var exp = Expressionable.Create<HbtExceptionLog>();

            if (!string.IsNullOrEmpty(query.UserName))
                exp.And(x => x.UserName.Contains(query.UserName));

            if (!string.IsNullOrEmpty(query.Method))
                exp.And(x => x.Method.Contains(query.Method));

            if (!string.IsNullOrEmpty(query.ExceptionType))
                exp.And(x => x.ExceptionType.Contains(query.ExceptionType));

            if (query.StartTime.HasValue)
                exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query.EndTime.HasValue)
                exp.And(x => x.CreateTime <= query.EndTime.Value);

            // 执行分页查询
            var result = await _exceptionLogRepository.GetPagedListAsync(
                exp.ToExpression(),
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            // 返回分页结果
            return new HbtPagedResult<HbtExceptionLogDto>
            {
                Rows = result.Rows.Adapt<List<HbtExceptionLogDto>>(),
                TotalNum = result.TotalNum,
                PageIndex = result.PageIndex,
                PageSize = result.PageSize
            };
        }


        /// <summary>
        /// 获取异常日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>返回异常日志详情</returns>
        public async Task<HbtExceptionLogDto> GetByIdAsync(long logId)
        {
            var log = await _exceptionLogRepository.GetByIdAsync(logId);
            if (log == null)
                throw new HbtException($"异常日志不存在: {logId}");

            return log.Adapt<HbtExceptionLogDto>();
        }

        /// <summary>
        /// 导出异常日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">Excel工作表名称</param>
        /// <returns>返回导出的Excel文件字节数组</returns>
        public async Task<byte[]> ExportAsync(HbtExceptionLogQueryDto query, string sheetName = "异常日志数据")
        {
            // 1.构建查询条件
            var predicate = Expressionable.Create<HbtExceptionLog>();

            if (!string.IsNullOrEmpty(query.UserName))
                predicate.And(x => x.UserName.Contains(query.UserName));

            if (!string.IsNullOrEmpty(query.Method))
                predicate.And(x => x.Method.Contains(query.Method));

            if (!string.IsNullOrEmpty(query.ExceptionType))
                predicate.And(x => x.ExceptionType.Contains(query.ExceptionType));

            if (query.StartTime.HasValue)
                predicate.And(x => x.CreateTime >= query.StartTime.Value);

            if (query.EndTime.HasValue)
                predicate.And(x => x.CreateTime <= query.EndTime.Value);

            // 2.查询数据
            var logs = await _exceptionLogRepository.AsQueryable()
                .Where(predicate.ToExpression())
                .OrderByDescending(x => x.CreateTime)
                .ToListAsync();

            // 3.转换并导出
            var exportDtos = logs.Adapt<List<HbtExceptionLogExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 清空异常日志
        /// </summary>
        /// <returns>返回是否清空成功</returns>
        public async Task<bool> ClearAsync()
        {
            var result = await _exceptionLogRepository.DeleteAsync((Expression<Func<HbtExceptionLog, bool>>)(x => true));
            return result > 0;
        }
    }
} 